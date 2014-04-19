using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Images.Users.Info
{
	internal sealed class InfoClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetInfoDataAsync( NiconicoContext context, uint requestUserID )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.ImageUserInfoUrl + requestUserID ) );
		}

		public static InfoResponse ParseInfoData( string infoData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( infoData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 3 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var userXml = responseXml.FirstChild;
			if( userXml.NodeName != "user" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new InfoResponse( userXml );
		}

		public static IAsyncOperation<InfoResponse> GetInfoAsync( NiconicoContext context, uint requestUserID )
		{
			return GetInfoDataAsync( context, requestUserID )
				.AsTask()
				.ContinueWith( prevTask => ParseInfoData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}