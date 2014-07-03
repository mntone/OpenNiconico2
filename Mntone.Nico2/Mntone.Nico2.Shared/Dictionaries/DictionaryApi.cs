using System.Collections.Generic;
using System.Collections.ObjectModel;

#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

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
		/// [非ログオン可] 非同期操作として大百科に単語記事が存在するかを確認します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<bool> WordExistAsync( string targetWord )
		{
			return WordExist.WordExistClient.WordExistAsync( _context, targetWord ).AsAsyncOperation();
		}
#else
		public Task<bool> WordExistAsync( string targetWord )
		{
			return WordExist.WordExistClient.WordExistAsync( _context, targetWord );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として大百科に単語記事の概要を要求します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Summary.SummaryResponse> GetSummaryAsync( string targetWord )
		{
			return Summary.SummaryClient.GetSummaryAsync( _context, targetWord ).AsAsyncOperation();
		}
#else
		public Task<Summary.SummaryResponse> GetSummaryAsync( string targetWord )
		{
			return Summary.SummaryClient.GetSummaryAsync( _context, targetWord );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として大百科の指定したカテゴリーに単語が存在するかを確認します
		/// </summary>
		/// <param name="targetCategory">目的のカテゴリー</param>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<bool> ExistAsync( Category targetCategory, string targetWord )
		{
			return Exist.ExistClient.ExistAsync( _context, targetCategory, targetWord ).AsAsyncOperation();
		}
#else
		public Task<bool> ExistAsync( Category targetCategory, string targetWord )
		{
			return Exist.ExistClient.ExistAsync( _context, targetCategory, targetWord );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として大百科に最近登録された単語の一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Recent.RecentResponse> GetRecentAsync()
		{
			return Recent.RecentClient.GetRecentAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<Recent.RecentResponse> GetRecentAsync()
		{
			return Recent.RecentClient.GetRecentAsync( _context );
		}
#endif


		#region field

		private NiconicoContext _context;

		#endregion
	}
}