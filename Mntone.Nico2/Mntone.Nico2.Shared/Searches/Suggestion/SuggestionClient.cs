using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if WINDOWS_APP
using System.Linq;
using Windows.Data.Json;
#else
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
#endif

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

		public static IReadOnlyList<string> ParseSuggestionData( string suggestionData )
		{
#if WINDOWS_APP
			return JsonValue.Parse( suggestionData )
				.GetObject()
				.GetNamedArray( "candidates" )
				.Select( candidate => candidate.GetString() )
				.ToList();
#else
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( suggestionData ) ) )
			{
				return ( IReadOnlyList<string> )new DataContractJsonSerializer( typeof( List<string> ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
#endif
		}

		public static Task<IReadOnlyList<string>> GetSuggestionAsync( NiconicoContext context, string targetWord )
		{
			return GetSuggestionDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSuggestionData( prevTask.Result ) );
		}
	}
}