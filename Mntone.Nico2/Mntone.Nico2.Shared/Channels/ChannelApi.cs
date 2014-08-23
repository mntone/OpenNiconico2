using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Channels
{
	/// <summary>
	/// ニコニコ チャンネル API 群
	/// </summary>
	public sealed class ChannelApi
	{
		internal ChannelApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてチャンネル アイコンを取得します
		/// </summary>
		/// <param name="requestChannelId">目的のチャンネル ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<IBuffer> GetIconAsync( string requestChannelId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestChannelId ).AsAsyncOperation();
		}
#else
		public Task<byte[]> GetIconAsync( string requestChannelId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestChannelId );
		}
#endif


		#region field

		private NiconicoContext _context;

		#endregion
	}
}