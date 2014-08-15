using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.OnAirStreams
{
	internal sealed class OnAirStreamsClient
	{
		public static Task<string> GetOnAirStreamsIndexDataAsync( NiconicoContext context, ushort pageIndex )
		{
			var sb = new StringBuilder( NiconicoUrls.LiveZappingListIndexUrl );
			if( pageIndex != 1 )
			{
				sb.Append( "&zpage=" );
				sb.Append( pageIndex );
			}
			return context.GetClient().GetString2Async( sb.ToString() );
		}

		public static Task<string> GetOnAirStreamsRecentDataAsync(
			NiconicoContext context, ushort pageIndex, Category category, SortDirection direction, SortType type )
		{
			var sb = new StringBuilder( NiconicoUrls.LiveZappingListRecentUrl );
			if( pageIndex != 1 )
			{
				sb.Append( "&zpage=" );
				sb.Append( pageIndex );
			}
			sb.Append( "&tab=" );
			sb.Append( category.ToCategoryString() );
			if( direction == SortDirection.Descending )
			{
				sb.Append( "&order=desc" );
			}
			sb.Append( "&sort=" );
			sb.Append( type.ToSortTypeString() );
			return context.GetClient().GetString2Async( sb.ToString() );
		}

		public static OnAirStreamsResponse ParseOnAirStreamsData( string onAirStreamsData )
		{
			return JsonSerializerExtensions.Load<OnAirStreamsResponse>( onAirStreamsData );
		}

		public static Task<OnAirStreamsResponse> GetOnAirStreamsIndexAsync( NiconicoContext context, ushort pageIndex = 1 )
		{
			return GetOnAirStreamsIndexDataAsync( context, pageIndex )
				.ContinueWith( prevTask => ParseOnAirStreamsData( prevTask.Result ) );
		}

		public static Task<OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			NiconicoContext context, ushort pageIndex, Category category, SortDirection direction = SortDirection.Ascending, SortType type = SortType.StartTime )
		{
			return GetOnAirStreamsRecentDataAsync( context, pageIndex, category, direction, type )
				.ContinueWith( prevTask => ParseOnAirStreamsData( prevTask.Result ) );
		}
	}
}