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
	{
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
		/// <param name="session">ログイン セッション</param>
		public NiconicoContext( NiconicoAuthenticationToken token, NiconicoSession session )
			: this( token )
		{
			this.CurrentSession = session;
		}

		/// <summary>
		/// 非同期操作としてログイン要求を送信します。ログイン完了後、ログインが正常にできているかをチェックし、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> LoginAsync()
		{
			return Task.Run( async () =>
			{
				var request = new Dictionary<string, string>();
				request.Add( MailTelName, this.AuthenticationToken.MailOrTelephone );
				request.Add( PasswordName, this.AuthenticationToken.Password );

				await this.GetClient().PostAsync( new Uri( NiconicoUrls.LoginUrl ), new HttpFormUrlEncodedContent( request ) );
				return await this._GetIsLoggedInAsync();
			} ).AsAsyncOperation();
		}

		/// <summary>
		/// 非同期操作としてログイン確認のための要求を送信します。ログインが正常にできている場合、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> GetIsLoggedInAsync()
		{
			return this._GetIsLoggedInAsync().AsAsyncOperation();
		}

		internal Task<bool> _GetIsLoggedInAsync()
		{
			return Task.Run( async () =>
			{
				var response = await this.GetClient().HeadAsync( new Uri( NiconicoUrls.NiconicoTopUrl ) );

				try
				{
					this.CurrentSession.AuthenticationFlag = ( NiconicoAccountAuthority )int.Parse( response.Headers[XNiconicoAuthflag] );
					if( this.CurrentSession.AuthenticationFlag != NiconicoAccountAuthority.NotLoggedIn )
					{
						this.CurrentSession.UserId = uint.Parse( response.Headers[XNiconicoId] );

						var cookie = this._httpBaseProtocolFilter
							.CookieManager
							.GetCookies( new Uri( "http://nicovideo.jp/" ) )
							.Where( c => { return c.Name == UserSessionName && c.Path == "/"; } )
							.SingleOrDefault();
						if( cookie != null )
						{
							if( cookie.Expires.HasValue )
							{
								this.CurrentSession.Key = cookie.Value;
								this.CurrentSession.Expires = cookie.Expires.Value;
								return true;
							}
						}
					}
				}
				catch( InvalidOperationException )
				{ }
				return false;
			} );
		}

		/// <summary>
		/// 非同期操作としてログアウト要求を送信します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> LogoutAsync()
		{
			return Task.Run( async () =>
			{
				await this.GetClient().HeadAsync( new Uri( NiconicoUrls.LogoutUrl ) );
				this.CurrentSession = null;
				return await this._GetIsLoggedInAsync();
			} ).AsAsyncOperation();
		}

		internal HttpClient GetClient()
		{
			if( this._httpClient == null )
			{
				this._httpBaseProtocolFilter = new HttpBaseProtocolFilter();
				this._httpBaseProtocolFilter.AllowAutoRedirect = false;
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
		}
		private NiconicoSession _CurrentSession = null;

		/// <summary>
		/// ニコニコ動画の API 群
		/// </summary>
		public Videos.VideoApi Video
		{
			get
			{
				if( this._Video == null )
				{
					this._Video = new Videos.VideoApi( this );
				}
				return this._Video;
			}
		}
		private Videos.VideoApi _Video = null;

		/// <summary>
		/// ニコニコ生放送の API 群
		/// </summary>
		public Live.LiveApi Live
		{
			get
			{
				if( this._Live == null )
				{
					this._Live = new Live.LiveApi( this );
				}
				return this._Live;
			}
		}
		private Live.LiveApi _Live = null;

		/// <summary>
		/// ニコニコ静画の API 群
		/// </summary>
		public Images.ImageApi Image
		{
			get
			{
				if( this._Image == null )
				{
					this._Image = new Images.ImageApi( this );
				}
				return this._Image;
			}
		}
		private Images.ImageApi _Image = null;

		/// <summary>
		/// ニコニコ検索の API 群
		/// </summary>
		public Searches.SearchApi Search
		{
			get
			{
				if( this._Search == null )
				{
					this._Search = new Searches.SearchApi( this );
				}
				return this._Search;
			}
		}
		private Searches.SearchApi _Search = null;

		/// <summary>
		/// ニコニコ大百科の API 群
		/// </summary>
		public Dictionaries.DictionaryApi Dictionary
		{
			get
			{
				if( this._Dictionary == null )
				{
					this._Dictionary = new Dictionaries.DictionaryApi( this );
				}
				return this._Dictionary;
			}
		}
		private Dictionaries.DictionaryApi _Dictionary = null;

		/// <summary>
		/// 追加のユーザー エージェント。
		/// 特に事情がない限り、各アプリ名を指定するなどしてください。
		/// </summary>
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