using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコに関する正規表現群
	/// </summary>
	public sealed class NiconicoRegex
	{
		private const string VideoIdRegexBase = @"(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\d{1,14}";
		private const string LiveIdRegexBase = @"lv\d{1,14}";
		private const string CommunityIdRegexBase = @"co\d{1,14}";
		private const string ChannelIdRegexBase = @"ch\d{1,14}";
		private const string ArticleIdRegexBase = @"ar\d{1,14}";
		private const string CommonIdRegexBase = @"nc\d{1,14}";

		private const string UserIdRegexBase = @"user/\d{1,10}";
		private const string MyListRegexBase = @"mylist/\d{1,10}";

		/// <summary>
		/// 動画 ID として適切かどうかを調べます
		/// </summary>
		/// <param name="id">動画 ID (と思われるもの)</param>
		/// <returns>動画 ID として適切かどうか</returns>
		public static bool IsVideoID( string id )
		{
			return Regex.IsMatch( id, '^' + VideoIdRegexBase + '$' );
		}
	}
}
