using System;
using System.Net;
using System.Net.Http;

#if WINDOWS_APP
using Windows.Foundation;
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

				if( this._httpClientHandler != null )
				{
					this._httpClientHandler.Dispose();
					this._httpClientHandler = null;
				}
			}
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

		private HttpClientHandler _httpClientHandler = null;
		private HttpClient _httpClient = null;

		#endregion
	}
}