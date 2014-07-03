using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Dictionaries.Recent
{
	internal sealed class RecentClient
	{
		public static Task<string> GetRecentDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.DictionaryRecentUrl );
		}

		public static RecentResponse ParseRecentData( string summaryData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( summaryData ) ) )
			{
				return ( RecentResponse )new DataContractJsonSerializer( typeof( RecentResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<RecentResponse> GetRecentAsync( NiconicoContext context )
		{
			return GetRecentDataAsync( context )
				.ContinueWith( prevTask => ParseRecentData( prevTask.Result ) );
		}
	}
}