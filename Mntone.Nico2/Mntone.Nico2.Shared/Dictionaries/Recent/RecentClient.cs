using System.Threading.Tasks;

namespace Mntone.Nico2.Dictionaries.Recent
{
	internal sealed class RecentClient
	{
		public static Task<string> GetRecentDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.DictionaryRecentUrl );
		}

		public static RecentResponse ParseRecentData( string recentData )
		{
			return JsonSerializerExtensions.Load<RecentResponse>( recentData );
		}

		public static Task<RecentResponse> GetRecentAsync( NiconicoContext context )
		{
			return GetRecentDataAsync( context )
				.ContinueWith( prevTask => ParseRecentData( prevTask.Result ) );
		}
	}
}