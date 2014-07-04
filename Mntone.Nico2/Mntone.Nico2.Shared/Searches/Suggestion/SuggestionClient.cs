using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Suggestion
{
	internal sealed class SuggestionClient
	{
		public static Task<string> GetSuggestionDataAsync( NiconicoContext context, string targetWord )
		{
			return context
				.GetClient()
				.GetConvertedString2Async( NiconicoUrls.SearchSuggestionUrl + Uri.EscapeUriString( targetWord ) );
		}

		public static SuggestionResponse ParseSuggestionData( string suggestionData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( suggestionData ) ) )
			{
				return ( SuggestionResponse )new DataContractJsonSerializer( typeof( SuggestionResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<SuggestionResponse> GetSuggestionAsync( NiconicoContext context, string targetWord )
		{
			return GetSuggestionDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSuggestionData( prevTask.Result ) );
		}
	}
}