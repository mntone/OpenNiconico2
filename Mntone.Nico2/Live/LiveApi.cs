using Windows.Foundation;

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
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsAsync( _context, pageIndex );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}