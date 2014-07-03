using Mntone.Nico2.Live.OnAirStreams;
using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
using System.Threading.Tasks;
#endif

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
#if WINDOWS_APP
		public IAsyncOperation<string> GetCKeyAsync( string refererId, string requestID )
		{
			return CKey.CKeyClient.GetCKeyAsync( _context, refererId, requestID ).AsAsyncOperation();
		}
#else
		public Task<string> GetCKeyAsync( string refererId, string requestID )
		{
			return CKey.CKeyClient.GetCKeyAsync( _context, refererId, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作としてハートビートを行います
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Heartbeat.HeartbeatResponse> HeartbeatAsync( string requestID )
		{
			return Heartbeat.HeartbeatClient.HeartbeatAsync( _context, requestID ).AsAsyncOperation();
		}
#else
		public Task<Heartbeat.HeartbeatResponse> HeartbeatAsync( string requestID )
		{
			return Heartbeat.HeartbeatClient.HeartbeatAsync( _context, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作としてプレイヤー情報を取得します
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<PlayerStatus.PlayerStatusResponse> GetPlayerStatusAsync( string requestID )
		{
			return PlayerStatus.PlayerStatusClient.GetPlayerStatusAsync( _context, requestID ).AsAsyncOperation();
		}
#else
		public Task<PlayerStatus.PlayerStatusResponse> GetPlayerStatusAsync( string requestID )
		{
			return PlayerStatus.PlayerStatusClient.GetPlayerStatusAsync( _context, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作として放送を退出する要求を行います
		/// </summary>
		/// <param name="requestID">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<bool> LeaveAsync( string requestID )
		{
			return Leave.LeaveClient.LeaveAsync( _context, requestID ).AsAsyncOperation();
		}
#else
		public Task<bool> LeaveAsync( string requestID )
		{
			return Leave.LeaveClient.LeaveAsync( _context, requestID );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirStreamsIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirStreamsIndexWithPageIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context, pageIndex ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( _context, pageIndex );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirStreamsRecentAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync( ushort pageIndex, Category category )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync( ushort pageIndex, Category category )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <param name="direction">ソートの方向</param>
		/// <param name="type">ソートの種類</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirStreamsRecentWithSortMethodAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			ushort pageIndex, Category category, SortDirection direction, SortType type )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category, direction, type ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			ushort pageIndex, Category category, SortDirection direction, SortType type )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( _context, pageIndex, category, direction, type );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として指定した状態の番組一覧を取得します
		/// </summary>
		/// <param name="status">目的の状態</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOtherStreamsAsync" )]
		public IAsyncOperation<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( OtherStreams.StatusType status )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( _context, status, 1 ).AsAsyncOperation();
		}
#else
		public Task<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( OtherStreams.StatusType status )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( _context, status, 1 );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として指定した状態の番組一覧を取得します
		/// </summary>
		/// <param name="status">目的の状態</param>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOtherStreamsWithPageIndexAsync" )]
		public IAsyncOperation<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync(
			OtherStreams.StatusType status, ushort pageIndex )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( _context, status, pageIndex ).AsAsyncOperation();
		}
#else
		public Task<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync(
			OtherStreams.StatusType status, ushort pageIndex )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( _context, status, pageIndex );
		}
#endif

		/// <summary>
		/// 非同期操作としてタイムシフト予約している一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<IReadOnlyList<string>> GetReservationsAsync()
		{
			return Reservations.ReservationsClient.GetReservationsAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<IReadOnlyList<string>> GetReservationsAsync()
		{
			return Reservations.ReservationsClient.GetReservationsAsync( _context );
		}
#endif

		/// <summary>
		/// 非同期操作としてタイムシフト予約している一覧 (詳細) を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<ReservationsInDetail.ReservationsInDetailResponse> GetReservationsInDetailAsync()
		{
			return ReservationsInDetail.ReservationsInDetailClient.GetReservationsInDetailAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<ReservationsInDetail.ReservationsInDetailResponse> GetReservationsInDetailAsync()
		{
			return ReservationsInDetail.ReservationsInDetailClient.GetReservationsInDetailAsync( _context );
		}
#endif


		#region field

		private NiconicoContext _context;

		#endregion
	}
}