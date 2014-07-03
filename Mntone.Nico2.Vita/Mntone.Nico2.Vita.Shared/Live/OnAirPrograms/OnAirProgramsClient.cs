using Mntone.Nico2.Live;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	internal sealed class OnAirProgramsClient
	{
		public static Task<string> GetOnAirProgramsDataAsync(
			NiconicoVitaContext context, CommunityType type, SortDirection sortDirection, SortType sortType, Range range )
		{
			range.CheckMaximumLength( 149 );

			return context.GetClient().GetString2Async(
				NiconicoUrls.LiveVideoOnAirListUrl
				+ '&' + range.ToFromLimitString()
				+ "&order=" + sortDirection.ToChar()
				+ "&pt=" + type.ToCommunityTypeString()
				+ "&sort=" + sortType.ToSortTypeString() );
		}

		public static OnAirProgramsResponse ParseOnAirProgramsData( string onAirProgramsData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( onAirProgramsData ) ) )
			{
				return ( ( OnAirProgramsResponseWrapper )new DataContractJsonSerializer( typeof( OnAirProgramsResponseWrapper ) ).ReadObject( ms ) ).Response;
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<OnAirProgramsResponse> GetOnAirProgramsAsync(
			NiconicoVitaContext context, CommunityType type, SortDirection sortDirection, SortType sortType, Range range )
		{
			return GetOnAirProgramsDataAsync( context, type, sortDirection, sortType, range )
				.ContinueWith( prevTask => ParseOnAirProgramsData( prevTask.Result ) );
		}
	}
}