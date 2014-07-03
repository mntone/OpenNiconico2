using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	internal sealed class ThumbnailClient
	{
		public static Task<string> GetThumbnailDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsVideoID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.VideoThumbInfoUrl + requestID );
		}

		public static ThumbnailResponse ParseThumbnailData( string thumbnailData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( thumbnailData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 5 } );
#else
			var xml = XDocument.Parse( thumbnailData );
#endif

			var thumbRes = xml.GetDocumentRootNode();
			if( thumbRes.GetName() != "nicovideo_thumb_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( thumbRes.GetNamedAttributeText( "status" ) != "ok" )
			{
				var error = thumbRes.GetFirstChildNode();
				var code = error.GetNamedChildNodeText( "code" );
				var description = error.GetNamedChildNodeText( "description" );

				throw new Exception( "Parse Error: " + description + " (" + code + ')' );
			}

			return new ThumbnailResponse( thumbRes.GetFirstChildNode() );
		}

		public static Task<ThumbnailResponse> GetThumbnailAsync( NiconicoContext context, string requestID )
		{
			return GetThumbnailDataAsync( context, requestID )
				.ContinueWith( prevTask => ParseThumbnailData( prevTask.Result ) );
		}
	}
}