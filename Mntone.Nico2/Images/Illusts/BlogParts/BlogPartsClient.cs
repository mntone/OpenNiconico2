using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	internal sealed class BlogPartsClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetClipDataAsync( NiconicoContext context, uint requestClipID )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.ImageBlogPartsUrl + "clip&key=" + requestClipID ) );
		}

		public static IAsyncOperationWithProgress<string, HttpProgress> GetUserDataAsync( NiconicoContext context, uint requestUserID )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.ImageBlogPartsUrl + "user&key=" + requestUserID ) );
		}

		public static BlogPartsResponse ParseBlogPartsData( string clipData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( clipData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new BlogPartsResponse( responseXml );
		}

		public static IAsyncOperation<BlogPartsResponse> GetClipAsync( NiconicoContext context, uint requestClipID )
		{
			return GetClipDataAsync( context, requestClipID )
				.AsTask()
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) )
				.AsAsyncOperation();
		}

		public static IAsyncOperation<BlogPartsResponse> GetUserAsync( NiconicoContext context, uint requestUserID )
		{
			return GetUserDataAsync( context, requestUserID )
				.AsTask()
				.ContinueWith( prevTask => ParseBlogPartsData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}