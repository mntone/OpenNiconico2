using System;

namespace Mntone.Nico2
{
	internal static class NiconicoUrls
	{
		private const string LiveApiForCommunityUrlBase = "http://watch.live.nicovideo.jp/api/";
		private const string LiveApiForOfficialOrChannelUrlBase = "http://ow.live.nicovideo.jp/api/";
		private const string LiveApiForExternalUrlBase = "http://ext.live.nicovideo.jp/api/";

		public static string NiconicoTopUrl { get { return VideoUrlBase; } }


		#region Authentication

		public static string LogOnUrl { get { return "https://secure.nicovideo.jp/secure/LogOn?site=niconico"; } }
		public static string LogOffUrl { get { return "https://secure.nicovideo.jp/secure/LogOff"; } }

		#endregion


		#region Videos

		private const string VideoUrlBase = "http://www.nicovideo.jp/";

		public static string VideoTopUrl { get { return VideoUrlBase + "video_top"; } }
		public static string VideoFlvUrl { get { return "http://flapi.nicovideo.jp/api/getflv/"; } }
		public static string VideoThumbInfoUrl { get { return "http://ext.nicovideo.jp/api/getthumbinfo/"; } }

		#endregion


		#region Live

		private const string LiveUrlBase = "http://live.nicovideo.jp/";
		private const string LiveWatchUrlBase = "http://live.nicovideo.jp/watch/";
		private const string LiveApiUrlBase = "http://live.nicovideo.jp/api/";

		public static string LiveTopUrl { get { return LiveUrlBase; } }
		public static string LiveCKeyUrl { get { return LiveApiUrlBase + "getckey"; } }
		public static string LiveHeartbeatUrl { get { return LiveApiUrlBase + "heartbeat"; } }

		#endregion


		#region Images

		private const string ImageUrlBase = "http://seiga.nicovideo.jp/";
		private const string ImageApiUrlBase = "http://seiga.nicovideo.jp/api/";

		public static string ImageTopUrl { get { return ImageUrlBase; } }
		public static string ImageUserInfoUrl { get { return ImageApiUrlBase + "user/info?id="; } }

		#endregion


		#region Searches

		private const string SearchApiUrlBase = "http://api.search.nicovideo.jp/api/";

		public static string SearchSuggestionUrl { get { return "http://search.nicovideo.jp/suggestion/complete/"; } }

		#endregion

		#region Dictionaries

		private const string DictionaryUrlBase = "http://dic.nicovideo.jp/";
		private const string DictionaryApiUrlBase = "http://api.nicodic.jp/";

		public static string DictionaryTopUrl { get { return DictionaryUrlBase; } }
		public static string DictionaryWordExistUrl { get { return DictionaryApiUrlBase + "e/z/"; } }
		public static string DictionarySummarytUrl { get { return DictionaryApiUrlBase + "page.summary/z/a/"; } }
		public static string DictionaryExistUrl { get { return DictionaryApiUrlBase + "page.exist/z/"; } }
		public static string DictionaryRecentUrl { get { return DictionaryApiUrlBase + "page.created/z"; } }

		#endregion
	}
}