using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.Videos
{
	internal sealed class VideosClient
	{
		public static Task<string> GetVideosDataAsync( NiconicoVitaContext context, IReadOnlyList<string> requestIDs )
		{
			foreach( var requestID in requestIDs )
			{
				if( !NiconicoRegex.IsLiveID( requestID ) )
				{
					throw new ArgumentException();
				}
			}

			return context.GetClient().GetString2Async( NiconicoUrls.VideosUrl + string.Join( ",", requestIDs ) );
		}

		public static VideosResponse ParseVideosData( string videosData )
		{
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( videosData ) ) )
			{
				var ret = ( ( VideosResponseWrapper )new DataContractJsonSerializer( typeof( VideosResponseWrapper ) ).ReadObject( ms ) ).Response;
				foreach( var program in ret.Programs )
				{
					if( program.Video.IsOfficial )
					{
						program.Community = null;
					}
				}
				return ret;
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<VideosResponse> GetVideosAsync( NiconicoVitaContext context, IReadOnlyList<string> requestIDs )
		{
			return GetVideosDataAsync( context, requestIDs )
				.ContinueWith( prevTask => ParseVideosData( prevTask.Result ) );
		}
	}
}