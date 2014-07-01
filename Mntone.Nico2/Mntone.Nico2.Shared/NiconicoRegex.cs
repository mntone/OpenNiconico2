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

		private const string VideoIDRegexBase = @"(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\d{1,14}"; // cd/fx/sd
		private const string LiveIDRegexBase = @"lv\d{1,14}";

		private const string ImageIDRegexBase = @"(?:[sm]g|im|bk)\d{1,14}";
		private const string ThemeIDRegexBase = @"sg\d{1,14}";
		private const string IllustIDRegexBase = @"im\d{1,14}";
		private const string ElectronicBookIDRegexBase = @"bk\d{1,14}";
		private const string MangaIDRegexBase = @"mg\d{1,14}";

		private const string CommunityIDRegexBase = @"co\d{1,14}";
		private const string ChannelIDRegexBase = @"ch\d{1,14}";

		private const string ArticleIDRegexBase = @"ar\d{1,14}";
		private const string NewsIDRegexBase = @"nw\d{1,14}";
		private const string CommonIDRegexBase = @"nc\d{1,14}";
		private const string AppsIDRegexBase = @"ap\d{1,14}";

		private const string WatchIDRegexBase = @"watch/\d{1,10}";
		private const string UserIDRegexBase = @"user/\d{1,10}";
		private const string MyListRegexBase = @"mylist/\d{1,10}";
		private const string MyVideoRegexBase = @"myvideo/\d{1,10}";
		private const string ClipIDRegexBase = @"clip/\d{1,10}";
		private const string ComicIDRegexBase = @"comic/\d{1,10}";

		private const string AdsIDRegexBase = @"(?:dw\d+|az[A-Z0-9]{10}|ys[a-zA-Z0-9-]+_[a-zA-Z0-9-]+|ga\d+|ip[\d_]+|gg[a-zA-Z0-9]+-[a-zA-Z0-9-]+)"; // it/an/nd

		/// <summary>
		/// 動画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">動画 ID (と思われるもの)</param>
		/// <returns>動画 ID として適切か</returns>
		public static bool IsVideoID( string id )
		{
			return Regex.IsMatch( id, '^' + VideoIDRegexBase + '$' );
		}

		/// <summary>
		/// 生放送 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">生放送 ID (と思われるもの)</param>
		/// <returns>生放送 ID として適切か</returns>
		public static bool IsLiveID( string id )
		{
			return Regex.IsMatch( id, '^' + LiveIDRegexBase + '$' );
		}

		/// <summary>
		/// 静画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画 ID (と思われるもの)</param>
		/// <returns>静画 ID として適切か</returns>
		public static bool IsImageID( string id )
		{
			return Regex.IsMatch( id, '^' + ImageIDRegexBase + '$' );
		}

		/// <summary>
		/// 静画のお題 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のお題 ID (と思われるもの)</param>
		/// <returns>静画のお題 ID として適切か</returns>
		public static bool IsThemeID( string id )
		{
			return Regex.IsMatch( id, '^' + ThemeIDRegexBase + '$' );
		}

		/// <summary>
		/// 静画のイラスト ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画のイラスト ID (と思われるもの)</param>
		/// <returns>静画のイラスト ID として適切か</returns>
		public static bool IsIllustrationID( string id )
		{
			return Regex.IsMatch( id, '^' + IllustIDRegexBase + '$' );
		}

		/// <summary>
		/// 静画の漫画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の漫画 ID (と思われるもの)</param>
		/// <returns>静画の漫画 ID として適切か</returns>
		public static bool IsMangaID( string id )
		{
			return Regex.IsMatch( id, '^' + MangaIDRegexBase + '$' );
		}

		/// <summary>
		/// 静画の電子書籍 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">静画の電子書籍 ID (と思われるもの)</param>
		/// <returns>静画の電子書籍 ID として適切か</returns>
		public static bool IsElectronicBookID( string id )
		{
			return Regex.IsMatch( id, '^' + ElectronicBookIDRegexBase + '$' );
		}

		/// <summary>
		/// コミュニティー ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">コミュニティー  ID (と思われるもの)</param>
		/// <returns>コミュニティー  ID として適切か</returns>
		public static bool IsCommunityID( string id )
		{
			return Regex.IsMatch( id, '^' + CommunityIDRegexBase + '$' );
		}

		/// <summary>
		/// チャンネル ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">チャンネル ID (と思われるもの)</param>
		/// <returns>チャンネル ID として適切か</returns>
		public static bool IsChannelID( string id )
		{
			return Regex.IsMatch( id, '^' + ChannelIDRegexBase + '$' );
		}
	}
}
