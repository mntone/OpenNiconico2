using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Searches
{
	/// <summary>
	/// ニコニコ検索 API 群
	/// </summary>
	public sealed class SearchApi
	{
		internal SearchApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として検索のサジェストを取得します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<IReadOnlyList<string>> GetSuggestionAsync( string targetWord )
		{
			return Suggestion.SuggestionClient.GetSuggestionAsync( _context, targetWord ).AsAsyncOperation();
		}
#else
		public Task<IReadOnlyList<string>> GetSuggestionAsync( string targetWord )
		{
			return Suggestion.SuggestionClient.GetSuggestionAsync( _context, targetWord );
		}
#endif



		#region field

		private NiconicoContext _context;

		#endregion
	}
}