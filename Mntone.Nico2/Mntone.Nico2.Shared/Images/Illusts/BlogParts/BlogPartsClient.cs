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
		public static Task<string> GetClipDataAsync( NiconicoContext context, uint requestClipId )
		{
			return context.GetClient().GetStringAsync( NiconicoUrls.ImageBlogPartsUrl + "clip&key=" + requestClipId );
		}

		public static Task<string> GetUserDataAsync( NiconicoContext context, uint requestUserId )
		{
			return context.GetClient().GetStringAsync( NiconicoUrls.ImageBlogPartsUrl + "user&key=" + requestUserId );
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

		public static Task<BlogPartsResponse> GetClipAsync( NiconicoContext context, uint requestClipId )
		{
			return GetClipDataAsync( context, requestClipId )
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) );
		}

		public static Task<BlogPartsResponse> GetUserAsync( NiconicoContext context, uint requestUserId )
		{
			return GetUserDataAsync( context, requestUserId )
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) );
		}
	}
}