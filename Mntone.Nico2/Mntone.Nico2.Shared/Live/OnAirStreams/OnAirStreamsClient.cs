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
		public static IAsyncOperationWithProgress<string, HttpProgress> GetOnAirStreamsIndexDataAsync(
			NiconicoContext context, ushort pageIndex )
		{
			var sb = new StringBuilder( NiconicoUrls.LiveZappingListIndexUrl );
			if( pageIndex != 1 )
			{
				sb.Append( "&zpage=" );
				sb.Append( pageIndex );
			}
			return context.GetClient().GetStringAsync( new Uri( sb.ToString() ) );
		}

		public static IAsyncOperationWithProgress<string, HttpProgress> GetOnAirStreamsRecentDataAsync(
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
			return context.GetClient().GetStringAsync( new Uri( sb.ToString() ) );
		}

		public static OnAirStreamsResponse ParseOnAirStreamsData( string onAirStreamsData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( onAirStreamsData ) ) )
			{
				return ( OnAirStreamsResponse )new DataContractJsonSerializer( typeof( OnAirStreamsResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<OnAirStreamsResponse> GetOnAirStreamsIndexAsync( NiconicoContext context, ushort pageIndex = 1 )
		{
			return GetOnAirStreamsIndexDataAsync( context, pageIndex )
				.AsTask()
				.ContinueWith( prevTask => ParseOnAirStreamsData( prevTask.Result ) )
				.AsAsyncOperation();
		}

		public static IAsyncOperation<OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			NiconicoContext context, ushort pageIndex, Category category, SortDirection direction = SortDirection.Ascending, SortType type = SortType.StartTime )
		{
			return GetOnAirStreamsRecentDataAsync( context, pageIndex, category, direction, type )
				.AsTask()
				.ContinueWith( prevTask => ParseOnAirStreamsData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}