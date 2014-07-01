using System;

namespace Mntone.Nico2.Vita
{
	/// <summary>
	/// ニコニコに関する URL 群
	/// </summary>
	public static class NiconicoUrls
	{
		private const string UrlBase = "http://api.ce.nicovideo.jp/";
		private const string FormatJson = "__format=json";
		

		#region Live

		private const string LiveUrlBase = UrlBase + "liveapi/";
		private const string LiveVersion1UrlBase = LiveUrlBase + "v1/";

		internal static string LiveVideoOnAirListUrl { get { return LiveVersion1UrlBase + "video.onairlist?" + FormatJson; } }

		#endregion
	}
}