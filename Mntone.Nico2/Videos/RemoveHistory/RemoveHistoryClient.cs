using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Videos.RemoveHistory
{
	internal sealed class RemoveHistoryClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> RemoveHistoryDataAsync(
			NiconicoContext context, string token, string requestID )
		{
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoRemoveUrl + token + "&video_id=" + requestID ) );
		}

		public static IAsyncOperationWithProgress<string, HttpProgress> RemoveAllHistoriesDataAsync( NiconicoContext context, string token )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoRemoveUrl + token + "&video_id=all" ) );
		}

		public static RemoveHistoryResponse ParseRemoveHistoryData( string historiesData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( historiesData ) ) )
			{
				return ( RemoveHistoryResponse )new DataContractJsonSerializer( typeof( RemoveHistoryResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<RemoveHistoryResponse> RemoveHistoryAsync( NiconicoContext context, string token, string requestID )
		{
			return RemoveHistoryDataAsync( context, token, requestID )
				.AsTask()
				.ContinueWith( prevTask => ParseRemoveHistoryData( prevTask.Result ) )
				.AsAsyncOperation();
		}

		public static IAsyncOperation<RemoveHistoryResponse> RemoveAllHistoriesAsync( NiconicoContext context, string token )
		{
			return RemoveAllHistoriesDataAsync( context, token )
				.AsTask()
				.ContinueWith( prevTask => ParseRemoveHistoryData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}