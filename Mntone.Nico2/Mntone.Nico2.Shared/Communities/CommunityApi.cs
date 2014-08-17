using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Communities
{
	/// <summary>
	/// ニコニコ コミュニティーAPI 群
	/// </summary>
	public sealed class CommunityApi
	{
		internal CommunityApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてコミュニティー アイコンを取得します
		/// </summary>
		/// <param name="requestCommunityID">目的のコミュニティー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<IBuffer> GetIconAsync( uint requestCommunityID )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestCommunityID ).AsAsyncOperation();
		}
#else
		public Task<byte[]> GetIconAsync( uint requestUserID )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestUserID );
		}
#endif


		#region field

		private NiconicoContext _context;

		#endregion
	}
}