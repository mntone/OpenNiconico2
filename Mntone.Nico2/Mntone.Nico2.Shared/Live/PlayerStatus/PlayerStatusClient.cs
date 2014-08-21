using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	internal sealed class PlayerStatusClient
	{
		public static Task<string> GetPlayerStatusDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetConvertedString2Async( NiconicoUrls.LivePlayerStatustUrl + requestId );
		}

		public static PlayerStatusResponse ParsePlayerStatusData( string playerStatusData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( playerStatusData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 6 } );
#else
			var xml = XDocument.Parse( playerStatusData );
#endif

			var getPlayerStatusXml = xml.GetDocumentRootNode();
			if( getPlayerStatusXml.GetName() != "getplayerstatus" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( getPlayerStatusXml.GetNamedAttributeText( "status" ) != "ok" )
			{
				var error = getPlayerStatusXml.GetFirstChildNode();
				var code = error.GetNamedChildNodeText( "code" );
				throw new Exception( "Parse Error: " + code );
			}

			return new PlayerStatusResponse( getPlayerStatusXml );
		}

		public static Task<PlayerStatusResponse> GetPlayerStatusAsync( NiconicoContext context, string requestId )
		{
			return GetPlayerStatusDataAsync( context, requestId )
				.ContinueWith( prevTask => ParsePlayerStatusData( prevTask.Result ) );
		}
	}
}