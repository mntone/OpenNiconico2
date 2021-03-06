﻿#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

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
		/// <param name="requestClipId">目的のクリップ ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<BlogParts.BlogPartsResponse> GetClipAsync( uint requestClipId )
		{
			return BlogParts.BlogPartsClient.GetClipAsync( _context, requestClipId ).AsAsyncOperation();
		}
#else
		public Task<BlogParts.BlogPartsResponse> GetClipAsync( uint requestClipId )
		{
			return BlogParts.BlogPartsClient.GetClipAsync( _context, requestClipId );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作としてユーザーのイラスト リストの一部 (最大 25 件) を取得します
		/// </summary>
		/// <param name="requestUserId">目的のユーザー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<BlogParts.BlogPartsResponse> GetUserAsync( uint requestUserId )
		{
			return BlogParts.BlogPartsClient.GetUserAsync( _context, requestUserId ).AsAsyncOperation();
		}
#else
		public Task<BlogParts.BlogPartsResponse> GetUserAsync( uint requestUserId )
		{
			return BlogParts.BlogPartsClient.GetUserAsync( _context, requestUserId );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作としてユーザーのイラスト リストの一部 (最大 25 件) を取得します
		/// </summary>
		/// <param name="targetDuration">目的の期間</param>
		/// <param name="targetGenreOrCategory">目的のジャンル または カテゴリー</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<BlogPartsRanking.BlogPartsRankingResponse> GetRankingAsync(
			BlogPartsRanking.DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return BlogPartsRanking.BlogPartsRankingClient.GetRankingAsync( _context, targetDuration, targetGenreOrCategory ).AsAsyncOperation();
		}
#else
		public Task<BlogPartsRanking.BlogPartsRankingResponse> GetRankingAsync(
			BlogPartsRanking.DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return BlogPartsRanking.BlogPartsRankingClient.GetRankingAsync( _context, targetDuration, targetGenreOrCategory );
		}
#endif


		#region field

		private NiconicoContext _context;

		#endregion
	}
}