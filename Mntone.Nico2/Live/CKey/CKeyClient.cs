using System;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.CKey
{
	internal sealed class CKeyClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetCKeyDataAsync(
			NiconicoContext context, string refererId, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( refererId ) )
			{
				throw new ArgumentException();
			}
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveCKeyUrl + "?referer_id=" + refererId + "&id=" + requestID ) );
		}

		public static string ParseCKeyData( string cKeyData )
		{
			if( cKeyData.Length > 5 && cKeyData.StartsWith( "ckey=" ) )
			{
				return cKeyData.Substring( 5 );
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<string> GetCKeyAsync( NiconicoContext context, string refererId, string requestID )
		{
			return GetCKeyDataAsync( context, refererId, requestID )
				.AsTask()
				.ContinueWith( prevTask => ParseCKeyData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}