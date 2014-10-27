using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.Video
{
	internal sealed class VideoClient
	{
		public static Task<string> GetVideoDataAsync( NiconicoVitaContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringWithoutHttpRequestExceptionAsync( NiconicoUrls.VideoUrl + requestId );
		}

		public static VideoResponse ParseVideoData( string videoData )
		{
			return JsonSerializerExtensions.Load<VideoResponseWrapper>( ProgramsResponseWrapper.PatchJson( videoData ) ).Response;
		}

		public static Task<VideoResponse> GetVideoAsync( NiconicoVitaContext context, string requestId )
		{
			return GetVideoDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseVideoData( prevTask.Result ) );
		}
	}
}