using System.Text.RegularExpressions;

namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコに関する正規表現群
	/// </summary>
	public sealed class NiconicoRegex
	{
		internal NiconicoRegex()
		{ }

		internal const string VideoIdRegexBase = @"(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\d{1,14}"; // cd/fx/sd
		internal const string LiveIdRegexBase = @"lv\d{1,14}";

		internal const string ImageIdRegexBase = @"(?:[sm]g|im|bk)\d{1,14}";
		internal const string ThemeIdRegexBase = @"sg\d{1,14}";
		internal const string IllustIdRegexBase = @"im\d{1,14}";
		internal const string ElectronicBookIdRegexBase = @"bk\d{1,14}";
		internal const string MangaIdRegexBase = @"mg\d{1,14}";

		internal const string CommunityIdRegexBase = @"co\d{1,14}";
		internal const string ChannelIdRegexBase = @"ch\d{1,14}";

		internal const string ArticleIdRegexBase = @"ar\d{1,14}";
		internal const string NewsIdRegexBase = @"nw\d{1,14}";
		internal const string CommonIdRegexBase = @"nc\d{1,14}";
		internal const string AppsIdRegexBase = @"ap\d{1,14}";

		internal const string WatchIdRegexBase = @"watch/\d{1,10}";
		internal const string UserIdRegexBase = @"user/\d{1,10}";
		internal const string MyListRegexBase = @"mylist/\d{1,10}";
		internal const string MyVideoRegexBase = @"myvideo/\d{1,10}";
		internal const string ClipIdRegexBase = @"clip/\d{1,10}";
		internal const string ComicIdRegexBase = @"comic/\d{1,10}";

		internal const string AdsIdRegexBase = @"(?:dw\d+|az[A-Z0-9]{10}|ys[a-zA-Z0-9-]+_[a-zA-Z0-9-]+|ga\d+|ip[\d_]+|gg[a-zA-Z0-9]+-[a-zA-Z0-9-]+)"; // it/an/nd

		/// <summary>
		/// 動画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">動画 ID (と思われるもの)</param>
		/// <returns>動画 ID として適切か</returns>
		public static bool IsVideoId( string id )
		{
			return Regex.IsMatch( id, '^' + VideoIdRegexBase + '$' );
		}

		/// <summary>
		/// 生放送 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">生放送 ID (と思われるもの)</param>
		/// <returns>生放送 ID として適切か</returns>
		public static bool IsLiveId( string id )
		{
			return Regex.IsMatch( id, '^' + LiveIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画 ID (と思われるもの)</param>
		/// <returns>静画 ID として適切か</returns>
		public static bool IsImageId( string id )
		{
			return Regex.IsMatch( id, '^' + ImageIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画のお題 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のお題 ID (と思われるもの)</param>
		/// <returns>静画のお題 ID として適切か</returns>
		public static bool IsThemeId( string id )
		{
			return Regex.IsMatch( id, '^' + ThemeIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画のイラスト ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のイラスト ID (と思われるもの)</param>
		/// <returns>静画のイラスト ID として適切か</returns>
		public static bool IsIllustrationId( string id )
		{
			return Regex.IsMatch( id, '^' + IllustIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画の漫画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の漫画 ID (と思われるもの)</param>
		/// <returns>静画の漫画 ID として適切か</returns>
		public static bool IsMangaId( string id )
		{
			return Regex.IsMatch( id, '^' + MangaIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画の電子書籍 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の電子書籍 ID (と思われるもの)</param>
		/// <returns>静画の電子書籍 ID として適切か</returns>
		public static bool IsElectronicBookId( string id )
		{
			return Regex.IsMatch( id, '^' + ElectronicBookIdRegexBase + '$' );
		}

		/// <summary>
		/// コミュニティー ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">コミュニティー  ID (と思われるもの)</param>
		/// <returns>コミュニティー  ID として適切か</returns>
		public static bool IsCommunityId( string id )
		{
			return Regex.IsMatch( id, '^' + CommunityIdRegexBase + '$' );
		}

		/// <summary>
		/// チャンネル ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">チャンネル ID (と思われるもの)</param>
		/// <returns>チャンネル ID として適切か</returns>
		public static bool IsChannelId( string id )
		{
			return Regex.IsMatch( id, '^' + ChannelIdRegexBase + '$' );
		}
	}
}
