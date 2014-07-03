using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Dictionaries.Summary
{
	internal sealed class SummaryClient
	{
		public static Task<string> GetSummaryDataAsync( NiconicoContext context, string targetWord )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.DictionarySummarytUrl + Uri.EscapeUriString( targetWord ) );
		}

		public static SummaryResponse ParseSummaryData( string summaryData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( summaryData ) ) )
			{
				return ( SummaryResponse )new DataContractJsonSerializer( typeof( SummaryResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<SummaryResponse> GetSummaryAsync( NiconicoContext context, string targetWord )
		{
			return GetSummaryDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSummaryData( prevTask.Result ) );
		}
	}
}
