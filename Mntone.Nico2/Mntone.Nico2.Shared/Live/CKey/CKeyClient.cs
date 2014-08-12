using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.CKey
{
	internal sealed class CKeyClient
	{
		public static Task<string> GetCKeyDataAsync( NiconicoContext context, string refererID, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( refererID ) )
			{
				throw new ArgumentException();
			}
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.LiveCKeyUrl + "?referer_id=" + refererID + "&id=" + requestID );
		}

		public static string ParseCKeyData( string cKeyData )
		{
			if( cKeyData.Length > 5 && cKeyData.StartsWith( "ckey=" ) )
			{
				return cKeyData.Substring( 5 );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<string> GetCKeyAsync( NiconicoContext context, string refererID, string requestID )
		{
			return GetCKeyDataAsync( context, refererID, requestID )
				.ContinueWith( prevTask => ParseCKeyData( prevTask.Result ) );
		}
	}
}