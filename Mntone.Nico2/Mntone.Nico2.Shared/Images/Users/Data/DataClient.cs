using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Images.Users.Data
{
	internal sealed class DataClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetDataDataAsync( NiconicoContext context, uint requestUserID )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.ImageUserDataUrl + requestUserID ) );
		}

		public static DataResponse ParseDataData( string dataData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( dataData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new DataResponse( responseXml );
		}

		public static IAsyncOperation<DataResponse> GetDataAsync( NiconicoContext context, uint requestUserID )
		{
			return GetDataDataAsync( context, requestUserID )
				.AsTask()
				.ContinueWith( prevTask => ParseDataData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}