﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Foundation;
#endif

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

				if( this._httpClientHandler != null )
				{
					this._httpClientHandler.Dispose();
					this._httpClientHandler = null;
				}
			}
		}


		/// <summary>
		/// 非同期操作としてログイン要求を送信します。
		/// ログイン完了後、ログインが正常にできているかをチェックし、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<NiconicoSignInStatus> SignInAsync()
#else
		public Task<NiconicoSignInStatus> SignInAsync()
#endif
		{
			var request = new Dictionary<string, string>();
			request.Add( MailTelName, this.AuthenticationToken.MailOrTelephone );
			request.Add( PasswordName, this.AuthenticationToken.Password );

			return this.GetClient()
				.Post2Async( NiconicoUrls.LogOnUrl, request )
				.ContinueWith( prevTask => this.GetIsSignedInOnInternalAsync() )
				.Unwrap()
#if WINDOWS_APP
				.AsAsyncOperation()
#endif
;
		}

		/// <summary>
		/// 非同期操作としてログイン確認のための要求を送信します。
		/// ログインが正常にできている場合、その状態をセッションに記録します。
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<NiconicoSignInStatus> GetIsSignedInAsync()
		{
			return this.GetIsSignedInOnInternalAsync().AsAsyncOperation();
		}
#else
		public Task<NiconicoSignInStatus> GetIsSignedInAsync()
		{
			return this.GetIsSignedInOnInternalAsync();
		}
#endif

		internal Task<NiconicoSignInStatus> GetIsSignedInOnInternalAsync()
		{
			return this.GetClient()
				.Head2Async( NiconicoUrls.TopPageUrl )
				.ContinueWith( prevTask =>
				{
					var response = prevTask.Result;
					if( response.StatusCode == HttpStatusCode.OK )
					{
						this.CurrentSession.AccountAuthority = ( NiconicoAccountAuthority )response.Headers.GetValues( XNiconicoAuthflag ).Single().ToInt();
						if( this.CurrentSession.AccountAuthority != NiconicoAccountAuthority.NotSignedIn )
						{
							this.CurrentSession.UserId = uint.Parse( response.Headers.GetValues( XNiconicoId ).Single() );
							try
							{
								var cookie = this._httpClientHandler
									.CookieContainer
									.GetCookies( NiconicoCookieUrl )
									.Cast<Cookie>()
									.Where( c => c.Name == UserSessionName && c.Path == "/" )
									.OrderByDescending( c => c.Expires.Ticks )
									.First();
								if( cookie != null && cookie.Expires != null )
								{
									this.CurrentSession.Key = cookie.Value;
									this.CurrentSession.Expires = cookie.Expires;
									return NiconicoSignInStatus.Success;
								}
							}
							catch( InvalidOperationException )
							{ }
						}
					}
					else if( response.StatusCode == HttpStatusCode.ServiceUnavailable )
					{
						return NiconicoSignInStatus.ServiceUnavailable;
					}
					return NiconicoSignInStatus.Failed;
				} );
		}

		/// <summary>
		/// 非同期操作としてログオフ要求を送信します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<NiconicoSignInStatus> SignOutOffAsync()
#else
		public Task<NiconicoSignInStatus> SignOutOffAsync()
#endif
		{
			return this.GetClient()
				.Head2Async( NiconicoUrls.LogOffUrl )
				.ContinueWith( prevTask =>
				{
					this.CurrentSession = null;
					return this.GetIsSignedInOnInternalAsync();
				} )
				.Unwrap()
#if WINDOWS_APP
				.AsAsyncOperation()
#endif
;
		}

		internal HttpClient GetClient()
		{
			if( this._httpClient == null )
			{
				this._httpClientHandler = new HttpClientHandler();
				this._httpClientHandler.AllowAutoRedirect = false;
				this._httpClientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				this._httpClient = new HttpClient( this._httpClientHandler, false );
				this._httpClient.DefaultRequestHeaders.Add( "user-agent", this._AdditionalUserAgent != null
					? NiconicoContext.DefaultUserAgent + " (" + this._AdditionalUserAgent + ')'
					: NiconicoContext.DefaultUserAgent );
				this._httpClient.DefaultRequestHeaders.Add( "accept-language", "ja, en;q=0.5" );

				if( this.CurrentSession != null )
				{
					var cookie = string.Format(
						"{0}={1}; expires={2}",
						UserSessionName,
						this.CurrentSession.Key,
						this.CurrentSession.Expires.ToUniversalTime().ToString( "ddd, dd-MMM-yyyy HH:mm:ss' GMT'", System.Globalization.CultureInfo.InvariantCulture ) );
					this._httpClientHandler.CookieContainer.SetCookies( NiconicoCookieUrl, cookie );
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

		/// <summary>
		/// ニコニコ コミュニティー API 群
		/// </summary>
		public Communities.CommunityApi Community
		{
			get { return this._Community ?? ( this._Community = new Communities.CommunityApi( this ) ); }
		}
		private Communities.CommunityApi _Community = null;

		/// <summary>
		/// ニコニコ チャンネル API 群
		/// </summary>
		public Channels.ChannelApi Channel
		{
			get { return this._Channel ?? ( this._Channel = new Channels.ChannelApi( this ) ); }
		}
		private Channels.ChannelApi _Channel = null;

		/// <summary>
		/// ニコニコ ユーザー API 群
		/// </summary>
		public Users.UserApi User
		{
			get { return this._User ?? ( this._User = new Users.UserApi( this ) ); }
		}
		private Users.UserApi _User = null;

		#endregion


		#region property (and related field)

		/// <summary>
		/// ニコニコ　トークン
		/// </summary>
		public NiconicoAuthenticationToken AuthenticationToken { get; set; }

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
			get { return this._AdditionalUserAgent; }
			set { this._AdditionalUserAgent = value; }
		}
		private string _AdditionalUserAgent = null;

		#endregion


		#region field

		private const string XNiconicoId = "x-niconico-id";
		private const string XNiconicoAuthflag = "x-niconico-authflag";
		private const string MailTelName = "mail_tel";
		private const string PasswordName = "password";
		private const string UserSessionName = "user_session";
		internal const string DefaultUserAgent = "OpenNiconico/2.0";
		private readonly Uri NiconicoCookieUrl = new Uri( "http://nicovideo.jp/" );

		private HttpClientHandler _httpClientHandler = null;
		private HttpClient _httpClient = null;

		#endregion
	}
}