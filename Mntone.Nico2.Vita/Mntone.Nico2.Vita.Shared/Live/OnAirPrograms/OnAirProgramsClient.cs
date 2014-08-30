using Mntone.Nico2.Live;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	internal sealed class OnAirProgramsClient
	{
		public static Task<string> GetOnAirProgramsDataAsync(
			NiconicoVitaContext context, Nullable<CommunityType> type, SortDirection sortDirection, SortType sortType, Range range )
		{
			range.CheckMaximumLength( 149, "range" );

			var sb = new StringBuilder( NiconicoUrls.LiveVideoOnAirListUrl );
			sb.Append( '&' );
			sb.Append( range.ToFromLimitString() );
			if( sortDirection == SortDirection.Descending )
			{
				sb.Append( "&order=" );
				sb.Append( sortDirection.ToShortString() );
			}
			if( type.HasValue )
			{
				sb.Append( "&pt=" );
				sb.Append( type.Value.ToCommunityTypeString() );
			}
			if( sortType != SortType.StartTime )
			{
				sb.Append( "&sort=" );
				sb.Append( sortType.ToSortTypeString() );
			}
			return context.GetClient().GetString2Async( sb.ToString() );
		}

		public static ProgramsResponse ParseOnAirProgramsData( string onAirProgramsData )
		{
			return JsonSerializerExtensions.Load<ProgramsResponseWrapper>( ProgramsResponseWrapper.PatchJson2( onAirProgramsData ) ).Response;
		}

		public static Task<ProgramsResponse> GetOnAirProgramsAsync(
			NiconicoVitaContext context, Nullable<CommunityType> type, SortDirection sortDirection, SortType sortType, Range range )
		{
			return GetOnAirProgramsDataAsync( context, type, sortDirection, sortType, range )
				.ContinueWith( prevTask => ParseOnAirProgramsData( prevTask.Result ) );
		}
	}
}