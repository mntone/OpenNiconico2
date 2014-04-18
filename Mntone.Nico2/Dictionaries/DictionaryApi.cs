using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;

namespace Mntone.Nico2.Dictionaries
{
	/// <summary>
	/// ニコニコ大百科 API 群
	/// </summary>
	public sealed class DictionaryApi
	{
		internal DictionaryApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログイン可] 非同期操作として大百科に単語記事が存在するかを確認します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> WordExistAsync( string targetWord )
		{
			return WordExist.WordExistClient.WordExistAsync( _context, targetWord );
		}

		/// <summary>
		/// [非ログイン可] 非同期操作として大百科に単語記事の概要を要求します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Summary.SummaryResponse> GetSummaryAsync( string targetWord )
		{
			return Summary.SummaryClient.GetSummaryAsync( _context, targetWord );
		}

		/// <summary>
		/// [非ログイン可] 非同期操作として大百科の指定したカテゴリーに単語が存在するかを確認します
		/// </summary>
		/// <param name="targetCategory">目的のカテゴリー</param>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<bool> ExistAsync( Category targetCategory, string targetWord )
		{
			return Exist.ExistClient.ExistAsync( _context, targetCategory, targetWord );
		}

		/// <summary>
		/// [非ログイン可] 非同期操作として大百科に最近登録された単語の一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public IAsyncOperation<Recent.RecentResponse> GetRecentAsync()
		{
			return Recent.RecentClient.GetRecentAsync( _context );
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}