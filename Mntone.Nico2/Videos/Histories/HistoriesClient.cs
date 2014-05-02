using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Videos.Histories
{
	internal sealed class HistoriesClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetHistoriesDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoHistoryUrl ) );
		}

		public static HistoriesResponse ParseHistoriesData( string historiesData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( historiesData ) ) )
			{
				return ( HistoriesResponse )new DataContractJsonSerializer( typeof( HistoriesResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<HistoriesResponse> GetHistoriesAsync( NiconicoContext context )
		{
			return GetHistoriesDataAsync( context )
				.AsTask()
				.ContinueWith( prevTask => ParseHistoriesData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}