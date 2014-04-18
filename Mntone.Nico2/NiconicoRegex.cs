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

		private const string VideoIdRegexBase = @"(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\d{1,14}"; // cd/fx/sd
		private const string LiveIdRegexBase = @"lv\d{1,14}";

		private const string ImageIdRegexBase = @"(?:[sm]g|im|bk)\d{1,14}";
		private const string IllustrationIdRegexBase = @"im\d{1,14}";
		private const string ElectronicBookIdRegexBase = @"bk\d{1,14}";
		private const string MangaIdRegexBase = @"mg\d{1,14}";
		private const string ThemeIdRegexBase = @"sg\d{1,14}";

		private const string CommunityIdRegexBase = @"co\d{1,14}";
		private const string ChannelIdRegexBase = @"ch\d{1,14}";

		private const string ArticleIdRegexBase = @"ar\d{1,14}";
		private const string CommonIdRegexBase = @"nc\d{1,14}";

		private const string WatchIdRegexBase = @"watch/\d{1,10}";
		private const string UserIdRegexBase = @"user/\d{1,10}";
		private const string MylistRegexBase = @"mylist/\d{1,10}";
		private const string MyvideoRegexBase = @"myvideo/\d{1,10}";
		private const string ClipIdRegexBase = @"clip/\d{1,10}";
		private const string ComicIdRegexBase = @"comic/\d{1,10}";
		private const string AdsRegexBase = @"(?:dw\d+|az[A-Z0-9]{10}|ys[a-zA-Z0-9-]+_[a-zA-Z0-9-]+|ga\d+|ip[\d_]+|gg[a-zA-Z0-9]+-[a-zA-Z0-9-]+)"; // ys/it

		/// <summary>
		/// 動画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">動画 ID (と思われるもの)</param>
		/// <returns>動画 ID として適切か</returns>
		public static bool IsVideoID( string id )
		{
			return Regex.IsMatch( id, '^' + VideoIdRegexBase + '$' );
		}

		/// <summary>
		/// 生放送 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">生放送 ID (と思われるもの)</param>
		/// <returns>生放送 ID として適切か</returns>
		public static bool IsLiveID( string id )
		{
			return Regex.IsMatch( id, '^' + LiveIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画 ID (と思われるもの)</param>
		/// <returns>静画 ID として適切か</returns>
		public static bool IsImageID( string id )
		{
			return Regex.IsMatch( id, '^' + ImageIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画のイラスト ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のイラスト ID (と思われるもの)</param>
		/// <returns>静画のイラスト ID として適切か</returns>
		public static bool IsIllustrationID( string id )
		{
			return Regex.IsMatch( id, '^' + IllustrationIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画の電子書籍 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の電子書籍 ID (と思われるもの)</param>
		/// <returns>静画の電子書籍 ID として適切か</returns>
		public static bool IsElectronicBookID( string id )
		{
			return Regex.IsMatch( id, '^' + ElectronicBookIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画の漫画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の漫画 ID (と思われるもの)</param>
		/// <returns>静画の漫画 ID として適切か</returns>
		public static bool IsMangaID( string id )
		{
			return Regex.IsMatch( id, '^' + MangaIdRegexBase + '$' );
		}

		/// <summary>
		/// 静画のお題 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のお題 ID (と思われるもの)</param>
		/// <returns>静画のお題 ID として適切か</returns>
		public static bool IsThemeID( string id )
		{
			return Regex.IsMatch( id, '^' + ThemeIdRegexBase + '$' );
		}

		/// <summary>
		/// コミュニティー ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">コミュニティー  ID (と思われるもの)</param>
		/// <returns>コミュニティー  ID として適切か</returns>
		public static bool IsCommunityID( string id )
		{
			return Regex.IsMatch( id, '^' + CommunityIdRegexBase + '$' );
		}

		/// <summary>
		/// チャンネル ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">チャンネル ID (と思われるもの)</param>
		/// <returns>チャンネル ID として適切か</returns>
		public static bool IsChannelID( string id )
		{
			return Regex.IsMatch( id, '^' + ChannelIdRegexBase + '$' );
		}
	}
}
