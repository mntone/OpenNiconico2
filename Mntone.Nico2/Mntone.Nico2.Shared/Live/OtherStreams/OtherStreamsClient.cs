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
			return JsonSerializerExtensions.Load<OtherStreamsResponse>( otherStreamsData );
		}

		public static Task<OtherStreamsResponse> GetOtherStreamsAsync( NiconicoContext context, StatusType status, ushort pageIndex )
		{
			return GetOtherStreamsDataAsync( context, status, pageIndex )
				.ContinueWith( prevTask => ParseOtherStreamsData( prevTask.Result ) );
		}
	}
}