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
				throw new ParseException( "Parse Error: Node name is invalid." );
			}

			if( getPlayerStatusXml.GetNamedAttributeText( "status" ) != "ok" )
			{
				var error = getPlayerStatusXml.GetFirstChildNode();
				var code = error.GetNamedChildNodeText( "code" );
				switch( code )
				{
				case "not_found":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_NOT_FOUND );

				case "closed":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_CLOSED );

				case "maintenance":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_MAINTENANCE );

				case "require_community_member":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_COMMUNITY_MEMBER_ONLY );

				case "full":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_FULL );

				case "premium_only":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_PREMIUM_ONLY );

				default:
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_UNKNOWN );
				}
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