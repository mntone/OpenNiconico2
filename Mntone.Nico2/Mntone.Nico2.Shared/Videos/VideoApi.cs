#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Videos
{
	/// <summary>
	/// ニコニコ動画 API 群
	/// </summary>
	public sealed class VideoApi
	{
		internal VideoApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として flv 情報を取得します
		/// </summary>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetFlvAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string requestID )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID ).AsAsyncOperation();
		}
#else
		public Task<Flv.FlvResponse> GetFlvAsync( string requestID )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作として flv 情報を取得します
		/// </summary>
		/// <param name="requestID">目的の動画 ID</param>
		/// <param name="cKey">CKey</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetFlvWithCKeyAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string requestID, string cKey )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID, cKey ).AsAsyncOperation();
		}
#else
		public Task<Flv.FlvResponse> GetFlvAsync( string requestID, string cKey )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID, cKey );
		}
#endif

		/// <summary>
		/// 非同期操作として thumbnail 情報を取得します
		/// </summary>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Thumbnail.ThumbnailResponse> GetThumbnailAsync( string requestID )
		{
			return Thumbnail.ThumbnailClient.GetThumbnailAsync( _context, requestID ).AsAsyncOperation();
		}
#else
		public Task<Thumbnail.ThumbnailResponse> GetThumbnailAsync( string requestID )
		{
			return Thumbnail.ThumbnailClient.GetThumbnailAsync( _context, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作として videoviewhistory/list 情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Histories.HistoriesResponse> GetHistoriesAsync()
		{
			return Histories.HistoriesClient.GetHistoriesAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<Histories.HistoriesResponse> GetHistoriesAsync()
		{
			return Histories.HistoriesClient.GetHistoriesAsync( _context );
		}
#endif

		/// <summary>
		/// 非同期操作として videoviewhistory/remove で履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveHistoryAsync( string token, string requestID )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveHistoryAsync( _context, token, requestID ).AsAsyncOperation();
		}
#else
		public Task<RemoveHistory.RemoveHistoryResponse> RemoveHistoryAsync( string token, string requestID )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveHistoryAsync( _context, token, requestID );
		}
#endif


		/// <summary>
		/// 非同期操作として videoviewhistory/remove ですべての履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveAllHistoriesAsync( string token )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveAllHistoriesAsync( _context, token ).AsAsyncOperation();
		}
#else
		public Task<RemoveHistory.RemoveHistoryResponse> RemoveAllHistoriesAsync( string token )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveAllHistoriesAsync( _context, token );
		}
#endif

		#region field

		private NiconicoContext _context;

		#endregion
	}
}