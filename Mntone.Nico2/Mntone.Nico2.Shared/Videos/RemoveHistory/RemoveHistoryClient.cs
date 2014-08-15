using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.RemoveHistory
{
	internal sealed class RemoveHistoryClient
	{
		public static Task<string> RemoveHistoryDataAsync(
			NiconicoContext context, string token, string requestID )
		{
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.VideoRemoveUrl + token + "&video_id=" + requestID );
		}

		public static Task<string> RemoveAllHistoriesDataAsync( NiconicoContext context, string token )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.VideoRemoveUrl + token + "&video_id=all" );
		}

		public static RemoveHistoryResponse ParseRemoveHistoryData( string historiesData )
		{
			return JsonSerializerExtensions.Load<RemoveHistoryResponse>( historiesData );
		}

		public static Task<RemoveHistoryResponse> RemoveHistoryAsync( NiconicoContext context, string token, string requestID )
		{
			return RemoveHistoryDataAsync( context, token, requestID )
				.ContinueWith( prevTask => ParseRemoveHistoryData( prevTask.Result ) );
		}

		public static Task<RemoveHistoryResponse> RemoveAllHistoriesAsync( NiconicoContext context, string token )
		{
			return RemoveAllHistoriesDataAsync( context, token )
				.ContinueWith( prevTask => ParseRemoveHistoryData( prevTask.Result ) );
		}
	}
}