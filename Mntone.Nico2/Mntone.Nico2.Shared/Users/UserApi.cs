using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Users
{
	/// <summary>
	/// ニコニコ ユーザー API 群
	/// </summary>
	public sealed class UserApi
	{
		internal UserApi( NiconicoContext context )
		{
			this._context = context;
		}


		/// <summary>
		/// 非同期操作としてユーザー情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<UserInfo.UserInfoResponse> GetUserInfoAsync()
		{
			return UserInfo.UserInfoClient.GetUserInfoAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<UserInfo.UserInfoResponse> GetUserInfoAsync()
		{
			return UserInfo.UserInfoClient.GetUserInfoAsync( this._context );
		}
#endif



		#region field

		private NiconicoContext _context;

		#endregion
	}
}