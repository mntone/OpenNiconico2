using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
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
			using( var ms = new MemoryStream( Encoding.Unicode.GetBytes( videoData ) ) )
			{
				var ret = ( ( VideoResponseWrapper )new DataContractJsonSerializer( typeof( VideoResponseWrapper ) ).ReadObject( ms ) ).Response;
				if( ret.Program.Video.IsOfficial )
				{
					ret.Program.Community = null;
				}
				return ret;
			}
			throw new Exception( "Parse Error" );
		}

		public static Task<VideoResponse> GetVideoAsync( NiconicoVitaContext context, string requestID )
		{
			return GetVideoDataAsync( context, requestID )
				.ContinueWith( prevTask => ParseVideoData( prevTask.Result ) );
		}
	}
}