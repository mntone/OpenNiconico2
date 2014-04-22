using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Dictionaries.Summary
{
	internal sealed class SummaryClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetSummaryDataAsync( NiconicoContext context, string targetWord )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.DictionarySummarytUrl + Uri.EscapeUriString( targetWord ) ) );
		}

		public static SummaryResponse ParseSummaryData( string summaryData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( summaryData ) ) )
			{
				return ( SummaryResponse )new DataContractJsonSerializer( typeof( SummaryResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<SummaryResponse> GetSummaryAsync( NiconicoContext context, string targetWord )
		{
			return GetSummaryDataAsync( context, targetWord )
				.AsTask()
				.ContinueWith( prevTask => ParseSummaryData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}
