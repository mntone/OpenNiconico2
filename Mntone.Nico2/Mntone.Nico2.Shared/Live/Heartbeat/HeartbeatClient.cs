using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.Heartbeat
{
	internal sealed class HeartbeatClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> HeartbeatDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveHeartbeatUrl + "?v=" + requestID ) );
		}

		public static HeartbeatResponse ParseHeartbeatData( string heartbeatData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( heartbeatData, new XmlLoadSettings { MaxElementDepth = 3 } );

			var heartbeatXml = xml.ChildNodes[1];
			if( heartbeatXml.NodeName != "heartbeat" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( heartbeatXml.GetNamedAttribute( "status" ).InnerText != "ok" )
			{
				var error = heartbeatXml.FirstChild;
				var code = error.GetNamedChildNode( "code" ).InnerText;
				var description = error.GetNamedChildNode( "description" ).InnerText;
				var reject = error.GetNamedChildNode( "reject" ).InnerText.ToBooleanFromString();

				throw new Exception( "Parse Error: " + description + " (" + code + ')' );
			}

			return new HeartbeatResponse( heartbeatXml );
		}

		public static IAsyncOperation<HeartbeatResponse> HeartbeatAsync( NiconicoContext context, string requestID )
		{
			return HeartbeatDataAsync( context, requestID )
				.AsTask()
				.ContinueWith( prevTask => ParseHeartbeatData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}