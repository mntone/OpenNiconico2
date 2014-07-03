using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.CKey
{
	internal sealed class CKeyClient
	{
		public static Task<string> GetCKeyDataAsync( NiconicoContext context, string refererId, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( refererId ) )
			{
				throw new ArgumentException();
			}
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.LiveCKeyUrl + "?referer_id=" + refererId + "&id=" + requestID );
		}

		public static string ParseCKeyData( string cKeyData )
		{
			if( cKeyData.Length > 5 && cKeyData.StartsWith( "ckey=" ) )
			{
				return cKeyData.Substring( 5 );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<string> GetCKeyAsync( NiconicoContext context, string refererId, string requestID )
		{
			return GetCKeyDataAsync( context, refererId, requestID )
				.ContinueWith( prevTask => ParseCKeyData( prevTask.Result ) );
		}
	}
}