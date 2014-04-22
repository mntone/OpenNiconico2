using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.OnAirStreams
{
	internal sealed class OnAirStreamsClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetOnAirStreamsDataAsync( NiconicoContext context, ushort pageIndex )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveZappingListUrl + pageIndex ) );
		}

		public static OnAirStreamsResponse ParseOnAirStreamsData( string onAirStreamsData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( onAirStreamsData ) ) )
			{
				return ( OnAirStreamsResponse )new DataContractJsonSerializer( typeof( OnAirStreamsResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<OnAirStreamsResponse> GetOnAirStreamsAsync( NiconicoContext context, ushort pageIndex )
		{
			return GetOnAirStreamsDataAsync( context, pageIndex )
				.AsTask()
				.ContinueWith( prevTask => ParseOnAirStreamsData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}