#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

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
		/// <param name="type">提供元の種類</param>
		/// <param name="sortDirection">整列方向</param>
		/// <param name="sortType">整列方法</param>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<OnAirPrograms.OnAirProgramsResponse> GetOnAirProgramsAsync(
			Nico2.Live.CommunityType type, SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( _context, type, sortDirection, sortType, range ).AsAsyncOperation();
		}
#else
		public Task<OnAirPrograms.OnAirProgramsResponse> GetOnAirProgramsAsync(
			Nico2.Live.CommunityType type, SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( _context, type, sortDirection, sortType, range );
		}
#endif

		#region field

		private NiconicoVitaContext _context;

		#endregion
	}
}