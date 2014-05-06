using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;

namespace Mntone.Nico2.Live.PlayerStatus
{
	internal sealed class PlayerStatusClient
	{
		public static Task<string> GetPlayerStatusDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient()
				.GetBufferAsync( new Uri( NiconicoUrls.LivePlayerStatustUrl + requestID ) )
				.AsTask()
				.ContinueWith( buffer => new StreamReader( buffer.Result.AsStream(), Encoding.UTF8 ).ReadToEnd() );
		}

		public static PlayerStatusResponse ParsePlayerStatusData( string playerStatusData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( playerStatusData, new XmlLoadSettings { MaxElementDepth = 6 } );

			var getPlayerStatusXml = xml.ChildNodes[1];
			if( getPlayerStatusXml.NodeName != "getplayerstatus" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( getPlayerStatusXml.GetNamedAttribute( "status" ).InnerText != "ok" )
			{
				var error = getPlayerStatusXml.FirstChild;
				var code = error.GetNamedChildNode( "code" ).InnerText;
				throw new Exception( "Parse Error: " + code );
			}

			return new PlayerStatusResponse( getPlayerStatusXml );
		}

		public static IAsyncOperation<PlayerStatusResponse> GetPlayerStatusAsync( NiconicoContext context, string requestID )
		{
			return GetPlayerStatusDataAsync( context, requestID )
				.ContinueWith( prevTask => ParsePlayerStatusData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}