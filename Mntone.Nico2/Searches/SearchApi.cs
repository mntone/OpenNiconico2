using System.Collections.Generic;
using Windows.Foundation;

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
		public IAsyncOperation<IReadOnlyList<string>> GetSuggestionAsync( string targetWord )
		{
			return Suggestion.SuggestionClient.GetSuggestionAsync( _context, targetWord );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}