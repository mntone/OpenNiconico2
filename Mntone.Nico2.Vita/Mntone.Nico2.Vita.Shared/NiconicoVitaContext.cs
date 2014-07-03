using System;

#if WINDOWS_APP
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
#else
using System.Collections;
using System.Net;
using System.Net.Http;
#endif

namespace Mntone.Nico2.Vita
{
	/// <summary>
	/// ニコニコの Vita API コンテクスト
	/// </summary>
	public sealed class NiconicoVitaContext
		: IDisposable
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		public NiconicoVitaContext()
		{ }

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

#if WINDOWS_APP
				if( this._httpBaseProtocolFilter != null )
				{
					this._httpBaseProtocolFilter.Dispose();
					this._httpBaseProtocolFilter = null;
				}
#else
				if( this._httpClientHandler != null )
				{
					this._httpClientHandler.Dispose();
					this._httpClientHandler = null;
				}
#endif
			}
		}

		internal HttpClient GetClient()
		{
			if( this._httpClient == null )
			{
#if WINDOWS_APP
				this._httpBaseProtocolFilter = new HttpBaseProtocolFilter();
				this._httpBaseProtocolFilter.AllowAutoRedirect = false;
				this._httpBaseProtocolFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
				this._httpClient = new HttpClient( this._httpBaseProtocolFilter );
				this._httpClient.DefaultRequestHeaders["user-agent"] = !string.IsNullOrEmpty( _AdditionalUserAgent )
					? NiconicoContext.DefaultUserAgent + " (" + _AdditionalUserAgent + ')'
					: NiconicoContext.DefaultUserAgent;
#else
				this._httpClientHandler = new HttpClientHandler();
				this._httpClientHandler.AllowAutoRedirect = false;
				this._httpClientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				this._httpClient = new HttpClient( this._httpClientHandler, false );
				this._httpClient.DefaultRequestHeaders.Add( "user-agent", this._AdditionalUserAgent != null
					? NiconicoContext.DefaultUserAgent + " (" + this._AdditionalUserAgent + ')'
					: NiconicoContext.DefaultUserAgent );
#endif
			}
			return this._httpClient;
		}


		#region API

		/// <summary>
		/// ニコニコ生放送の API 群
		/// </summary>
		public Live.LiveApi Live
		{
			get { return this._Live ?? ( this._Live = new Live.LiveApi( this ) ); }
		}
		private Live.LiveApi _Live = null;

		#endregion


		#region property (and related field)

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

#if WINDOWS_APP
		private HttpBaseProtocolFilter _httpBaseProtocolFilter = null;
#else
		private HttpClientHandler _httpClientHandler = null;
#endif
		private HttpClient _httpClient = null;

		#endregion
	}
}