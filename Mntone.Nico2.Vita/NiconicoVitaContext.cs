using System;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

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

				if( this._httpBaseProtocolFilter != null )
				{
					this._httpBaseProtocolFilter.Dispose();
					this._httpBaseProtocolFilter = null;
				}
			}
		}

		internal HttpClient GetClient()
		{
			if( this._httpClient == null )
			{
				this._httpBaseProtocolFilter = new HttpBaseProtocolFilter();
				this._httpBaseProtocolFilter.AllowAutoRedirect = false;
				this._httpBaseProtocolFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
				this._httpClient = new HttpClient( this._httpBaseProtocolFilter );
				this._httpClient.DefaultRequestHeaders["user-agent"] = !string.IsNullOrEmpty( _AdditionalUserAgent )
					? NiconicoContext.DefaultUserAgent + " (" + _AdditionalUserAgent + ')'
					: NiconicoContext.DefaultUserAgent;
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

		private HttpBaseProtocolFilter _httpBaseProtocolFilter = null;
		private HttpClient _httpClient = null;

		#endregion
	}
}