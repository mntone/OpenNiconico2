using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Histories
{
	internal sealed class HistoriesClient
	{
		public static Task<string> GetHistoriesDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.VideoHistoryUrl );
		}

		public static HistoriesResponse ParseHistoriesData( string historiesData )
		{
			return JsonSerializerExtensions.Load<HistoriesResponse>( historiesData );
		}

		public static Task<HistoriesResponse> GetHistoriesAsync( NiconicoContext context )
		{
			return GetHistoriesDataAsync( context )
				.ContinueWith( prevTask => ParseHistoriesData( prevTask.Result ) );
		}
	}
}