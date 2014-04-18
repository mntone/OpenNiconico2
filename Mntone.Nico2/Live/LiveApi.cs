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
		/// <param name="targetId">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<string> GetCKeyAsync( string refererId, string targetId )
		{
			return CKey.CKeyClient.GetCKeyAsync( _context, refererId, targetId );
		}

		/// <summary>
		/// 非同期操作としてハートビートを行います
		/// </summary>
		/// <param name="targetId">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Heartbeat.HeartbeatResponse> HeartbeatAsync( string targetId )
		{
			return Heartbeat.HeartbeatClient.HeartbeatAsync( _context, targetId );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}