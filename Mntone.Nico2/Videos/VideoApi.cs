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
		/// <param name="targetId">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string targetId )
		{
			return Flv.FlvClient.GetFlvAsync( _context, targetId );
		}

		/// <summary>
		/// 非同期操作として flv 情報を取得します
		/// </summary>
		/// <param name="targetId">目的の動画 ID</param>
		/// <param name="cKey">CKey</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		[Overload( "GetFlvWithCKeyAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string targetId, string cKey )
		{
			return Flv.FlvClient.GetFlvAsync( _context, targetId, cKey );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}