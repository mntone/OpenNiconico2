﻿
namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコ HResult
	/// </summary>
	/// <remarks>
	/// 下 4 桁のうちの上位 1 桁
	/// - 0x0: 全般
	/// - 0x1: 動画
	/// - 0x2: 生放送
	/// - 0x3: 静画
	/// - 以後、予約
	/// 
	/// 下 4 桁のうちの下位 3 桁: 任意
	/// </remarks>
	public sealed class NiconicoHResult
	{
		/// <summary>
		/// パース失敗
		/// </summary>
		public static int EParse
		{
			get { return E_PARSE; }
		}
		internal const int E_PARSE = unchecked( ( int )0xc0040000 );

		/// <summary>
		/// 不明な生放送のエラー
		/// </summary>
		public static int ELiveUnknown
		{
			get { return E_LIVE_UNKNOWN; }
		}
		internal const int E_LIVE_UNKNOWN = unchecked( ( int )0xc0042000 );

		/// <summary>
		/// 放送が見つからない
		/// </summary>
		public static int ELiveNotFound
		{
			get { return E_LIVE_NOT_FOUND; }
		}
		internal const int E_LIVE_NOT_FOUND = unchecked( ( int )0xc0042001 );

		/// <summary>
		/// 放送終了
		/// </summary>
		public static int ELiveClosed
		{
			get { return E_LIVE_CLOSED; }
		}
		internal const int E_LIVE_CLOSED = unchecked( ( int )0xc0042002 );

		/// <summary>
		/// メンテナンス中
		/// </summary>
		public static int ELiveMaintenance
		{
			get { return E_LIVE_MAINTENANCE; }
		}
		internal const int E_LIVE_MAINTENANCE = unchecked( ( int )0xc0042003 );

		/// <summary>
		/// コミュニティー限定
		/// </summary>
		public static int ELiveCommunityMemberOnly
		{
			get { return E_LIVE_COMMUNITY_MEMBER_ONLY; }
		}
		internal const int E_LIVE_COMMUNITY_MEMBER_ONLY = unchecked( ( int )0xc0042010 );

		/// <summary>
		/// 満席
		/// </summary>
		public static int ELiveFull
		{
			get { return E_LIVE_FULL; }
		}
		internal const int E_LIVE_FULL = unchecked( ( int )0xc0042011 );

		/// <summary>
		/// プレミアムだけで満席
		/// </summary>
		public static int ELivePremiumOnly
		{
			get { return E_LIVE_PREMIUM_ONLY; }
		}
		internal const int E_LIVE_PREMIUM_ONLY = unchecked( ( int )0xc0042012 );
	}
}