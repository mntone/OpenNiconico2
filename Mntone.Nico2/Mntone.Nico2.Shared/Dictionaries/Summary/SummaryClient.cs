using System;
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
			return JsonSerializerExtensions.Load<SummaryResponse>( summaryData );
		}

		public static Task<SummaryResponse> GetSummaryAsync( NiconicoContext context, string targetWord )
		{
			return GetSummaryDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseSummaryData( prevTask.Result ) );
		}
	}
}
