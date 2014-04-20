using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Images.Illusts.Clip
{
	internal sealed class ClipClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetClipDataAsync( NiconicoContext context, uint requestClipID )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.ImageBlogPartsUrl + "clip&key=" + requestClipID ) );
		}

		public static ClipResponse ParseClipData( string clipData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( clipData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new ClipResponse( responseXml );
		}

		public static IAsyncOperation<ClipResponse> GetClipAsync( NiconicoContext context, uint requestClipID )
		{
			return GetClipDataAsync( context, requestClipID )
				.AsTask()
				.ContinueWith( prevTask => ParseClipData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}