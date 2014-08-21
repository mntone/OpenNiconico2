using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Users.Info
{
	internal sealed class InfoClient
	{
		public static Task<string> GetInfoDataAsync( NiconicoContext context, uint requestUserId )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.ImageUserInfoUrl + requestUserId );
		}

		public static InfoResponse ParseInfoData( string infoData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( infoData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 3 } );
#else
			var xml = XDocument.Parse( infoData );
#endif

			var responseXml = xml.GetDocumentRootNode();
			if( responseXml.GetName() != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var userXml = responseXml.GetFirstChildNode();
			if( userXml.GetName() != "user" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new InfoResponse( userXml );
		}

		public static Task<InfoResponse> GetInfoAsync( NiconicoContext context, uint requestUserId )
		{
			return GetInfoDataAsync( context, requestUserId )
				.ContinueWith( prevTask => ParseInfoData( prevTask.Result ) );
		}
	}
}