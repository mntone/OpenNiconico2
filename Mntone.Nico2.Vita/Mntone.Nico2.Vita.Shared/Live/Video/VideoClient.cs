using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.Video
{
	internal sealed class VideoClient
	{
		public static Task<string> GetVideoDataAsync( NiconicoVitaContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.VideoUrl + requestID );
		}

		public static VideoResponse ParseVideoData( string videoData )
		{
			var ret = JsonSerializerExtensions.Load<VideoResponseWrapper>( videoData ).Response;
			if( ret.Program.Video.IsOfficial )
			{
				ret.Program.Community = null;
			}
			return ret;
		}

		public static Task<VideoResponse> GetVideoAsync( NiconicoVitaContext context, string requestID )
		{
			return GetVideoDataAsync( context, requestID )
				.ContinueWith( prevTask => ParseVideoData( prevTask.Result ) );
		}
	}
}