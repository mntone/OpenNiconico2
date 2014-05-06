using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコの API コンテクスト
	/// </summary>
	public sealed class NiconicoContext
		: IDisposable
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <remarks>
		/// 非ログイン API 用に使用できます
		/// </remarks>
		public NiconicoContext()
		{ }

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="token">認証トークン</param>
		public NiconicoContext( NiconicoAuthenticationToken token )
		{
			this.AuthenticationToken = token;
		}

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="token">認証トークン</param>
		/// <param name="session">ログオン セッション</param>
		public NiconicoContext( NiconicoAuthenticationToken token, NiconicoSession session )
			: this( token )
		{
			this.CurrentSession = session;
		}

		/// <summary>
		/// デストラクター
		/// </summary>
		public void Dispose()
		{
			this.DisposeImpl();
		}

		private void DisposeImpl()
		{
			if( this._httpClient != null )
			{
				this._httpClient.Dispose();
				this._httpClient = null;

				if( this._httpBaseProtocolFilter != null )
				{
					this._httpBaseProtocolFilter.Dispose();
					this._httpBaseProtocolFilter = null;
				}
			}
		}

		/// <summary>
		/// 非同期操作としてログオン要求を送信します。ログオン完了後、ログオンが正常にできているかをチェックし、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> LogOnAsync()
		{
			var request = new Dictionary<string, string>();
			request.Add( MailTelName, this.AuthenticationToken.MailOrTelephone );
			request.Add( PasswordName, this.AuthenticationToken.Password );

			return this.GetClient()
				.PostAsync( new Uri( NiconicoUrls.LogOnUrl ), new HttpFormUrlEncodedContent( request ) )
				.AsTask()
				.ContinueWith( prevTask => this.GetIsLoggedOnInternalAsync() )
				.Unwrap()
				.AsAsyncOperation();
		}

		/// <summary>
		/// 非同期操作としてログオン確認のための要求を送信します。ログオンが正常にできている場合、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> GetIsLoggedOnAsync()
		{
			return this.GetIsLoggedOnInternalAsync().AsAsyncOperation();
		}

		internal Task<bool> GetIsLoggedOnInternalAsync()
		{
			return this.GetClient()
				.HeadAsync( new Uri( NiconicoUrls.TopPageUrl ) )
				.AsTask()
				.ContinueWith( prevTask =>
				{
					var response = prevTask.Result;

					this.CurrentSession.AccountAuthority = ( NiconicoAccountAuthority )response.Headers[XNiconicoAuthflag].ToInt();
					if( this.CurrentSession.AccountAuthority != NiconicoAccountAuthority.NotLoggedOn )
					{
						this.CurrentSession.UserId = uint.Parse( response.Headers[XNiconicoId] );

						try
						{
							var cookie = this._httpBaseProtocolFilter
								.CookieManager
								.GetCookies( new Uri( "http://nicovideo.jp/" ) )
								.Where( c => c.Name == UserSessionName && c.Path == "/" )
								.SingleOrDefault();
							if( cookie != null && cookie.Expires.HasValue )
							{
								this.CurrentSession.Key = cookie.Value;
								this.CurrentSession.Expires = cookie.Expires.Value;
								return true;
							}
						}
						catch( InvalidOperationException )
						{ }
					}
					return false;
				} );
		}

		/// <summary>
		/// 非同期操作としてログオフ要求を送信します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> LogOffAsync()
		{
			return this.GetClient()
				.HeadAsync( new Uri( NiconicoUrls.LogOffUrl ) )
				.AsTask()
				.ContinueWith( prevTask =>
				{
					this.CurrentSession = null;
					return this.GetIsLoggedOnInternalAsync();
				} )
				.Unwrap()
				.AsAsyncOperation();
		}

		internal HttpClient GetClient()
		{
			if( this._httpClient == null )
			{
				this._httpBaseProtocolFilter = new HttpBaseProtocolFilter();
				this._httpBaseProtocolFilter.AllowAutoRedirect = false;
				this._httpBaseProtocolFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
				this._httpClient = new HttpClient( this._httpBaseProtocolFilter );
				this._httpClient.DefaultRequestHeaders["user-agent"] = _AdditionalUserAgent != null
					? DefaultUserAgent + " (" + _AdditionalUserAgent + ')'
					: DefaultUserAgent;

				if( this.CurrentSession != null )
				{
					var cookie = new HttpCookie( UserSessionName, "nicovideo.jp", "/" );
					cookie.Value = this.CurrentSession.Key;
					cookie.Expires = this.CurrentSession.Expires;
					this._httpBaseProtocolFilter.CookieManager.SetCookie( cookie );
				}
				else
				{
					this._CurrentSession = new NiconicoSession();
				}
			}
			return this._httpClient;
		}


		#region APIs

		/// <summary>
		/// ニコニコ動画の API 群
		/// </summary>
		public Videos.VideoApi Video
		{
			get { return this._Video ?? ( this._Video = new Videos.VideoApi( this ) ); }
		}
		private Videos.VideoApi _Video = null;

		/// <summary>
		/// ニコニコ生放送の API 群
		/// </summary>
		public Live.LiveApi Live
		{
			get { return this._Live ?? ( this._Live = new Live.LiveApi( this ) ); }
		}
		private Live.LiveApi _Live = null;

		/// <summary>
		/// ニコニコ静画の API 群
		/// </summary>
		public Images.ImageApi Image
		{
			get { return this._Image ?? ( this._Image = new Images.ImageApi( this ) ); }
		}
		private Images.ImageApi _Image = null;

		/// <summary>
		/// ニコニコ検索の API 群
		/// </summary>
		public Searches.SearchApi Search
		{
			get { return this._Search ?? ( this._Search = new Searches.SearchApi( this ) ); }
		}
		private Searches.SearchApi _Search = null;

		/// <summary>
		/// ニコニコ大百科の API 群
		/// </summary>
		public Dictionaries.DictionaryApi Dictionary
		{
			get { return this._Dictionary ?? ( this._Dictionary = new Dictionaries.DictionaryApi( this ) ); }
		}
		private Dictionaries.DictionaryApi _Dictionary = null;

		#endregion


		#region property (and related field)

		/// <summary>
		/// ニコニコ　トークン
		/// </summary>
		public NiconicoAuthenticationToken AuthenticationToken
		{
			get { return this._AuthenticationToken; }
			set
			{
				if( value == null )
				{
					throw new ArgumentNullException();
				}
				this._AuthenticationToken = value;
			}
		}
		private NiconicoAuthenticationToken _AuthenticationToken = null;

		/// <summary>
		/// ニコニコ セッション
		/// </summary>
		public NiconicoSession CurrentSession
		{
			get { return this._CurrentSession; }
			set
			{
				this._CurrentSession = value;
				this.DisposeImpl();
			}
		}
		private NiconicoSession _CurrentSession = null;

		/// <summary>
		/// 追加のユーザー エージェント
		/// </summary>
		/// <remarks>
		/// 特に事情がない限り、各アプリ名を指定するなどしてください
		/// </remarks>
		public string AdditionalUserAgent
		{
			get { return _AdditionalUserAgent; }
			set { _AdditionalUserAgent = value; }
		}
		private string _AdditionalUserAgent = null;

		#endregion


		#region field

		private const string XNiconicoId = "x-niconico-id";
		private const string XNiconicoAuthflag = "x-niconico-authflag";
		private const string MailTelName = "mail_tel";
		private const string PasswordName = "password";
		private const string UserSessionName = "user_session";
		private const string DefaultUserAgent = "OpenNiconico/2.0";

		private HttpBaseProtocolFilter _httpBaseProtocolFilter = null;
		private HttpClient _httpClient = null;

		#endregion
	}
}