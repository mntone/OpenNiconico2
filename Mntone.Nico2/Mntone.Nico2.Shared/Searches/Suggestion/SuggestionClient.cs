using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;

namespace Mntone.Nico2.Searches.Suggestion
{
	internal sealed class SuggestionClient
	{
		public static Task<string> GetSuggestionDataAsync( NiconicoContext context, string targetWord )
		{
			return context.GetClient()
				.GetInputStreamAsync( new Uri( NiconicoUrls.SearchSuggestionUrl + Uri.EscapeUriString( targetWord ) ) )
				.AsTask()
				.ContinueWith( buffer => new StreamReader( buffer.Result.AsStreamForRead(), Encoding.UTF8 ).ReadToEnd() );
		}

		public static IReadOnlyList<string> ParseSuggestionData( string suggestionData )
		{
			return JsonValue.Parse( suggestionData )
				.GetObject()
				.GetNamedArray( "candidates" )
				.Select( candidate => candidate.GetString() )
				.ToList();
		}

		public static IAsyncOperation<IReadOnlyList<string>> GetSuggestionAsync( NiconicoContext context, string targetWord )
		{
			return GetSuggestionDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSuggestionData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}