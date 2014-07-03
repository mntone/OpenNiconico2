using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Histories
{
	internal sealed class HistoriesClient
	{
		public static Task<string> GetHistoriesDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.VideoHistoryUrl );
		}

		public static HistoriesResponse ParseHistoriesData( string historiesData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( historiesData ) ) )
			{
				return ( HistoriesResponse )new DataContractJsonSerializer( typeof( HistoriesResponse ) ).ReadObject( ms );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<HistoriesResponse> GetHistoriesAsync( NiconicoContext context )
		{
			return GetHistoriesDataAsync( context )
				.ContinueWith( prevTask => ParseHistoriesData( prevTask.Result ) );
		}
	}
}