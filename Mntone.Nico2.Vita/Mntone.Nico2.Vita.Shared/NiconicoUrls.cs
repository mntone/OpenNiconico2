using System;

namespace Mntone.Nico2.Vita
{
	/// <summary>
	/// ニコニコに関する URL 群
	/// </summary>
	public static class NiconicoUrls
	{
		private const string UrlBase = "http://api.ce.nicovideo.jp/";
		private const string FormatJson = "?__format=json";
		

		#region Live

		private const string LiveUrlBase = UrlBase + "liveapi/";
		private const string LiveVersion1UrlBase = LiveUrlBase + "v1/";

		internal static string VideoUrl { get { return LiveVersion1UrlBase + "video.info" + FormatJson + "&v="; } }
		internal static string VideosUrl { get { return LiveVersion1UrlBase + "video.array" + FormatJson; } }
		internal static string LiveVideoOnAirListUrl { get { return LiveVersion1UrlBase + "video.onairlist" + FormatJson; } }
		internal static string LiveVideoComingSoonListUrl { get { return LiveVersion1UrlBase + "video.comingsoon" + FormatJson; } }
		internal static string LiveVideoSearchUrl { get { return LiveVersion1UrlBase + "video.search.solr" + FormatJson; } }

		#endregion
	}
}