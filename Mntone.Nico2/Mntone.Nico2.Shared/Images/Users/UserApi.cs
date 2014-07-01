using Windows.Foundation;

namespace Mntone.Nico2.Images.Users
{
	/// <summary>
	/// ニコニコ静画のユーザー API 群
	/// </summary>
	public sealed class UserApi
	{
		internal UserApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作として user/info を取得します
		/// </summary>
		/// <param name="requestUserID">目的のユーザー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Info.InfoResponse> GetInfoAsync( uint requestUserID )
		{
			return Info.InfoClient.GetInfoAsync( _context, requestUserID );
		}

		/// <summary>
		/// 非同期操作として user/data を取得します
		/// </summary>
		/// <param name="requestUserID">目的のユーザー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Data.DataResponse> GetDataAsync( uint requestUserID )
		{
			return Data.DataClient.GetDataAsync( _context, requestUserID );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}