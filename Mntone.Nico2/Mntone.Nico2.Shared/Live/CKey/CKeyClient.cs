using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.CKey
{
	internal sealed class CKeyClient
	{
		public static Task<string> GetCKeyDataAsync( NiconicoContext context, string refererId, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( refererId ) )
			{
				throw new ArgumentException();
			}
			if( !NiconicoRegex.IsVideoId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync( NiconicoUrls.LiveCKeyUrl + "?referer_id=" + refererId + "&id=" + requestId );
		}

		public static string ParseCKeyData( string cKeyData )
		{
			if( cKeyData.Length > 5 && cKeyData.StartsWith( "ckey=" ) )
			{
				return cKeyData.Substring( 5 );
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<string> GetCKeyAsync( NiconicoContext context, string refererId, string requestId )
		{
			return GetCKeyDataAsync( context, refererId, requestId )
				.ContinueWith( prevTask => ParseCKeyData( prevTask.Result ) );
		}
	}
}