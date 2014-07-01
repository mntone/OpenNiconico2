using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.OtherStreams
{
	internal sealed class OtherStreamsClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetOtherStreamsDataAsync(
			NiconicoContext context, StatusType status, ushort pageIndex )
		{
			return context.GetClient().GetStringAsync( new Uri( pageIndex > 1
				? NiconicoUrls.LiveIndexZeroStreamListUrl + status.ToStatusTypeString() + "&zpage=" + pageIndex
				: NiconicoUrls.LiveIndexZeroStreamListUrl + status.ToStatusTypeString() ) );
		}

		public static OtherStreamsResponse ParseOtherStreamsData( string otherStreamsData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( otherStreamsData ) ) )
			{
				return ( OtherStreamsResponse )new DataContractJsonSerializer( typeof( OtherStreamsResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<OtherStreamsResponse> GetOtherStreamsAsync( NiconicoContext context, StatusType status, ushort pageIndex )
		{
			return GetOtherStreamsDataAsync( context, status, pageIndex )
				.AsTask()
				.ContinueWith( prevTask => ParseOtherStreamsData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}