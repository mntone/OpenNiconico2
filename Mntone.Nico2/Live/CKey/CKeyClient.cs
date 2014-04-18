using System;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.CKey
{
	internal sealed class CKeyClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetCKeyDataAsync(
			NiconicoContext context, string refererId, string targetId )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveCKeyUrl + "?referer_id=" + refererId + "&id=" + targetId ) );
		}

		public static string ParseCKeyData( string cKeyData )
		{
			if( cKeyData.Length > 5 && cKeyData.StartsWith( "ckey=" ) )
			{
				return cKeyData.Substring( 5 );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<string> GetCKeyAsync( NiconicoContext context, string refererId, string targetId )
		{
			return GetCKeyDataAsync( context, refererId, targetId )
				.AsTask()
				.ContinueWith( prevTask => ParseCKeyData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}