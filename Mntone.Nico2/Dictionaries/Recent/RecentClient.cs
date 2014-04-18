using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Dictionaries.Recent
{
	internal sealed class RecentClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetRecentDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.DictionaryRecentUrl ) );
		}

		public static RecentResponse ParseRecentData( string summaryData )
		{
			if( summaryData.Length > 7 && summaryData.StartsWith( "z({" ) && summaryData.EndsWith( "});" ) )
			{
				using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( summaryData.Substring( 2, summaryData.Length - 4 ) ) ) )
				{
					return ( RecentResponse )new DataContractJsonSerializer( typeof( RecentResponse ) ).ReadObject( ms );
				}
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<RecentResponse> GetRecentAsync( NiconicoContext context )
		{
			return GetRecentDataAsync( context )
				.AsTask()
				.ContinueWith( prevTask => ParseRecentData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}
