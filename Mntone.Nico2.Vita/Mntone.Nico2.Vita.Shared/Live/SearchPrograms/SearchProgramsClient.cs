using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
    internal sealed class SearchProgramsClient
	{
		public static Task<string> GetSearchProgramsDataAsync( NiconicoVitaContext context, SearchProgramsRequest request )
		{
			return context.GetClient().GetStringWithoutHttpRequestExceptionAsync( request.ToRequestString() );
		}

		public static SearchProgramsResponse ParseSearchProgramsData( string searchProgramsData )
		{
			return JsonSerializerExtensions.Load<SearchProgramsResponseWrapper>( ProgramsResponseWrapper.PatchJson2( searchProgramsData ) ).Response;
		}

		public static Task<SearchProgramsResponse> GetSearchProgramsAsync( NiconicoVitaContext context, SearchProgramsRequest request )
		{
			return GetSearchProgramsDataAsync( context, request )
				.ContinueWith( prevTask => ParseSearchProgramsData( prevTask.Result ) );
		}
	}
}