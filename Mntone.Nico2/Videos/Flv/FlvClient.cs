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

			return new FlvResponse()
			{
				ThreadId = response["thread_id"].ToUInt(),
				Length = TimeSpan.FromSeconds( ushort.Parse( response["l"] ) ),
				VideoUrl = response["url"].ToUri(),
				ReportUrl = response["link"].ToUri(),
				MessageServerUrl = response["ms"].ToUri(),
				SubMessageServerUrl = response["ms_sub"].ToUri(),
				UserId = response["user_id"].ToUInt(),
				IsPremium = response["is_premium"].ToBooleanFrom1(),
				UserName = response["nickname"],
				LoadedAt = DateTimeOffset.FromFileTime( 10000 * long.Parse( response["time"] ) + 116444736000000000 ),

#if DEBUG
				Done = response["done"].ToBooleanFromString(),
				NgRv = response["ng_rv"].ToUShort(),
				AppsUrl = ( "http://" + response["hms"] + ':' + response["hmsp"] + '/' ).ToUri(),
				AppsT = response["hmst"].ToUShort(),
				AppsTicket = response["hmstk"],
#endif
			};
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