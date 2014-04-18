using System;

namespace Mntone.Nico2
{
	internal static class NiconicoUrls
	{
		private const string VideoUrlBase = "http://www.nicovideo.jp/";
		private const string LiveUrlBase = "http://live.nicovideo.jp/";
		private const string LiveWatchUrlBase = "http://live.nicovideo.jp/watch/";
		private const string LiveApiUrlBase = "http://live.nicovideo.jp/api/";
		private const string ImageUrlBase = "http://seiga.nicovideo.jp/";

		private const string LiveApiForCommunityUrlBase = "http://watch.live.nicovideo.jp/api/";
		private const string LiveApiForOfficialOrChannelUrlBase = "http://ow.live.nicovideo.jp/api/";
		private const string LiveApiForExternalUrlBase = "http://ext.live.nicovideo.jp/api/";

		private const string SearchApiUrlBase = "http://api.search.nicovideo.jp/api/";

		public static string NiconicoTopUrl { get { return VideoUrlBase; } }


		#region Authentication

		public static string LoginUrl { get { return "https://secure.nicovideo.jp/secure/login?site=niconico"; } }
		public static string LogoutUrl { get { return "https://secure.nicovideo.jp/secure/logout"; } }

		#endregion


		#region Videos

		public static string VideoTopUrl { get { return VideoUrlBase + "video_top"; } }
		public static string VideoFlvUrl { get { return "http://flapi.nicovideo.jp/api/getflv"; } }
		public static string VideoThumbInfoUrl { get { return "http://ext.nicovideo.jp/api/getthumbinfo/"; } }

		#endregion


		#region Live

		public static string LiveTopUrl { get { return LiveUrlBase; } }
		public static string LiveCKeyUrl { get { return LiveApiUrlBase + "getckey"; } }
		public static string LiveHeartbeatUrl { get { return LiveApiUrlBase + "heartbeat"; } }

		#endregion


		#region Images

		public static string ImageTopUrl { get { return ImageUrlBase; } }

		#endregion

		#region Searches

		public static string SearchSuggestionUrl { get { return "http://search.nicovideo.jp/suggestion/complete/"; } }

		#endregion
	}
}