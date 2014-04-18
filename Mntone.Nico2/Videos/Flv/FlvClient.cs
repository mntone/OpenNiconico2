using System;
using System.Linq;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Videos.Flv
{
	internal sealed class FlvClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetFlvDataAsync(
			NiconicoContext context, string targetId )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoFlvUrl + "/" + targetId + "?as3=1" ) );
		}

		public static IAsyncOperationWithProgress<string, HttpProgress> GetFlvDataAsync(
			NiconicoContext context, string targetId, string cKey )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoFlvUrl + "/" + targetId + "?as3=1&ckey=" + cKey ) );
		}

		public static FlvResponse ParseFlvData( string flvData )
		{
			var response = flvData.Split( new char[] { '&' } ).ToDictionary(
				source => source.Substring( 0, source.IndexOf( '=' ) ),
				source => Uri.UnescapeDataString( source.Substring( source.IndexOf( '=' ) + 1 ) ) );

			if( response.ContainsKey( "error" ) )
			{
				throw new Exception( "Parse Error: " + response["error"] );
			}

			return new FlvResponse( response );
		}

		public static IAsyncOperation<FlvResponse> GetFlvAsync( NiconicoContext context, string targetId )
		{
			return GetFlvDataAsync( context, targetId )
				.AsTask()
				.ContinueWith( prevTask => ParseFlvData( prevTask.Result ) )
				.AsAsyncOperation();
		}

		public static IAsyncOperation<FlvResponse> GetFlvAsync( NiconicoContext context, string targetId, string cKey )
		{
			return GetFlvDataAsync( context, targetId, cKey )
				.AsTask()
				.ContinueWith( prevTask => ParseFlvData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}