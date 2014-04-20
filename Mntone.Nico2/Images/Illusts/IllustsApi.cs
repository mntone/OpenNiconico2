using Windows.Foundation;

namespace Mntone.Nico2.Images.Illusts
{
	/// <summary>
	/// ニコニコ静画のイラスト API 群
	/// </summary>
	public sealed class IllustApi
	{
		internal IllustApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてクリップの一部 (最大 25 件) を取得します
		/// </summary>
		/// <param name="requestClipID">目的のクリップ ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Clip.ClipResponse> GetClipAsync( uint requestClipID )
		{
			return Clip.ClipClient.GetClipAsync( _context, requestClipID );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}