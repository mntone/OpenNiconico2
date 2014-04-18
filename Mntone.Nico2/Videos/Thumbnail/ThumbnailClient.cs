using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Videos.Thumbnail
{
	internal sealed class ThumbnailClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetThumbInfoDataAsync( NiconicoContext context, string targetId )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.VideoThumbInfoUrl + targetId ) );
		}

		public static ThumbnailResponse ParseThumbnailData( string thumbnailData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( thumbnailData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 5 } );

			var thumbRes = xml.ChildNodes[1];
			if( thumbRes.NodeName != "nicovideo_thumb_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( thumbRes.GetNamedAttribute( "status" ).InnerText != "ok" )
			{
				var error = thumbRes.FirstChild;
				var code = error.GetNamedChildNode( "code" ).InnerText;
				var description = error.GetNamedChildNode( "description" ).InnerText;

				throw new Exception( "Parse Error: " + description + " (" + code + ')' );
			}

			return new ThumbnailResponse( thumbRes.FirstChild );
		}

	}
}