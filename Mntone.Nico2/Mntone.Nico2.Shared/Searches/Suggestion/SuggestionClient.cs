using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Suggestion
{
	internal sealed class SuggestionClient
	{
		public static Task<string> GetSuggestionDataAsync( NiconicoContext context, string targetWord )
		{
			return context
				.GetClient()
				.GetConvertedStringAsync( NiconicoUrls.SearchSuggestionUrl + Uri.EscapeUriString( targetWord ) );
		}

		public static SuggestionResponse ParseSuggestionData( string suggestionData )
		{
			return JsonSerializerExtensions.Load<SuggestionResponse>( suggestionData );
		}

		public static Task<SuggestionResponse> GetSuggestionAsync( NiconicoContext context, string targetWord )
		{
			return GetSuggestionDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSuggestionData( prevTask.Result ) );
		}
	}
}