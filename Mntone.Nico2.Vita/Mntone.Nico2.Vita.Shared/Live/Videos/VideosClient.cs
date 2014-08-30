using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.Videos
{
	internal sealed class VideosClient
	{
		public static Task<string> GetVideosDataAsync( NiconicoVitaContext context, IReadOnlyList<string> requestIds )
		{
			foreach( var requestId in requestIds )
			{
				if( !NiconicoRegex.IsLiveId( requestId ) )
				{
					throw new ArgumentException();
				}
			}

			return context.GetClient().GetString2Async( NiconicoUrls.VideosUrl + string.Join( ",", requestIds ) );
		}

		public static VideosResponse ParseVideosData( string videosData )
		{
			return JsonSerializerExtensions.Load<VideosResponseWrapper>( ProgramsResponseWrapper.PatchJson2( videosData ) ).Response;
		}

		public static Task<VideosResponse> GetVideosAsync( NiconicoVitaContext context, IReadOnlyList<string> requestIds )
		{
			return GetVideosDataAsync( context, requestIds )
				.ContinueWith( prevTask => ParseVideosData( prevTask.Result ) );
		}
	}
}