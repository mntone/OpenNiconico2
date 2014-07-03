using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.OtherStreams
{
	internal sealed class OtherStreamsClient
	{
		public static Task<string> GetOtherStreamsDataAsync(
			NiconicoContext context, StatusType status, ushort pageIndex )
		{
			return context.GetClient().GetString2Async( pageIndex > 1
				? NiconicoUrls.LiveIndexZeroStreamListUrl + status.ToStatusTypeString() + "&zpage=" + pageIndex
				: NiconicoUrls.LiveIndexZeroStreamListUrl + status.ToStatusTypeString() );
		}

		public static OtherStreamsResponse ParseOtherStreamsData( string otherStreamsData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( otherStreamsData ) ) )
			{
				return ( OtherStreamsResponse )new DataContractJsonSerializer( typeof( OtherStreamsResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<OtherStreamsResponse> GetOtherStreamsAsync( NiconicoContext context, StatusType status, ushort pageIndex )
		{
			return GetOtherStreamsDataAsync( context, status, pageIndex )
				.ContinueWith( prevTask => ParseOtherStreamsData( prevTask.Result ) );
		}
	}
}