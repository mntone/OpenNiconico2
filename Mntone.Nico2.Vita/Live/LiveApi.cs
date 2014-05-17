using Windows.Foundation;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// ニコニコ生放送 API 群
	/// </summary>
	public sealed class LiveApi
	{
		internal LiveApi( NiconicoVitaContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として番組一覧を取得します
		/// </summary>
		/// <param name="status">目的の状態</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<OnAirPrograms.OnAirProgramsResponse> GetOnAirProgramsAsync(
			Nico2.Live.CommunityType type, SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( _context, type, sortDirection, sortType, range );
		}

		#region field

		private NiconicoVitaContext _context;

		#endregion
	}
}