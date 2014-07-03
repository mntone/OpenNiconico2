using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	internal sealed class BlogPartsClient
	{
		public static Task<string> GetClipDataAsync( NiconicoContext context, uint requestClipID )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.ImageBlogPartsUrl + "clip&key=" + requestClipID );
		}

		public static Task<string> GetUserDataAsync( NiconicoContext context, uint requestUserID )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.ImageBlogPartsUrl + "user&key=" + requestUserID );
		}

		public static BlogPartsResponse ParseBlogPartsData( string blogPartsData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( blogPartsData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );
#else
			var xml = XDocument.Parse( blogPartsData );
#endif

			var responseXml = xml.GetDocumentRootNode();
			if( responseXml.GetName() != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new BlogPartsResponse( responseXml );
		}

		public static Task<BlogPartsResponse> GetClipAsync( NiconicoContext context, uint requestClipID )
		{
			return GetClipDataAsync( context, requestClipID )
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) );
		}

		public static Task<BlogPartsResponse> GetUserAsync( NiconicoContext context, uint requestUserID )
		{
			return GetUserDataAsync( context, requestUserID )
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) );
		}
	}
}