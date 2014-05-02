using Mntone.Nico2.Live.OnAirStreams;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace Mntone.Nico2.Live
{
	/// <summary>
	/// ニコニコ生放送 API 群
	/// </summary>
	public sealed class LiveApi
	{
		internal LiveApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として CKey を取得します
		/// </summary>
		/// <param name="refererId">生放送リファラー ID</param>
		/// <param name="requestID">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<string> GetCKeyAsync( string refererId, string requestID )
		{
			return CKey.CKeyClient.GetCKeyAsync( _context, refererId, requestID );
		}

		/// <summary>
		/// 非同期操作としてハートビートを行います
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Heartbeat.HeartbeatResponse> HeartbeatAsync( string requestID )
		{
			return Heartbeat.HeartbeatClient.HeartbeatAsync( _context, requestID );
		}

		/// <summary>
		/// 非同期操作としてプレイヤー情報を取得します
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<PlayerStatus.PlayerStatusResponse> GetPlayerStatusAsync( string requestID )
		{
			return PlayerStatus.PlayerStatusClient.GetPlayerStatusAsync( _context, requestID );
		}

		/// <summary>
		/// 非同期操作として放送を退出する要求を行います
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> LeaveAsync( string requestID )
		{
			return Leave.LeaveClient.LeaveAsync( _context, requestID );
		}

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetOnAirStreamsIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context );
		}

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetOnAirStreamsIndexWithPageIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context, pageIndex );
		}

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetOnAirStreamsRecentAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync( ushort pageIndex, Category category )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category );
		}

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <param name="direction">ソートの方向</param>
		/// <param name="type">ソートの種類</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetOnAirStreamsRecentWithSortMethodAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			ushort pageIndex, Category category, SortDirection direction, SortType type )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category, direction, type );
		}

		/// <summary>
		/// 非同期操作としてタイムシフト予約している一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<ReservationsInDetail.ReservationsInDetailResponse> GetReservationsInDetailAsync()
		{
			return ReservationsInDetail.ReservationsInDetailClient.GetReservationsInDetailAsync( _context );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}