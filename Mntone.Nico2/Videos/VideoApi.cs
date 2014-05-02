using Windows.Foundation;
using Windows.Foundation.Metadata;

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
		[Overload( "GetFlvAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string requestID )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID );
		}

		/// <summary>
		/// 非同期操作として flv 情報を取得します
		/// </summary>
		/// <param name="requestID">目的の動画 ID</param>
		/// <param name="cKey">CKey</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetFlvWithCKeyAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string requestID, string cKey )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestID, cKey );
		}

		/// <summary>
		/// 非同期操作として thumbnail 情報を取得します
		/// </summary>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Thumbnail.ThumbnailResponse> GetThumbnailAsync( string requestID )
		{
			return Thumbnail.ThumbnailClient.GetThumbnailAsync( _context, requestID );
		}

		/// <summary>
		/// 非同期操作として videoviewhistory/list 情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Histories.HistoriesResponse> GetHistoriesAsync()
		{
			return Histories.HistoriesClient.GetHistoriesAsync( _context );
		}

		/// <summary>
		/// 非同期操作として videoviewhistory/remove で履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveHistoryAsync( string token, string requestID )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveHistoryAsync( _context, token, requestID );
		}

		/// <summary>
		/// 非同期操作として videoviewhistory/remove ですべての履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveAllHistoriesAsync( string token )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveAllHistoriesAsync( _context, token );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}