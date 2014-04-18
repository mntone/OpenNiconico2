using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.Heartbeat
{
	internal sealed class HeartbeatClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> HeartbeatDataAsync(
			NiconicoContext context, string targetId )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveHeartbeatUrl + "?v=" + targetId ) );
		}

		public static HeartbeatResponse ParseHeartbeatData( string heartbeatData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( heartbeatData, new XmlLoadSettings { MaxElementDepth = 3 } );

			var heartbeat = xml.ChildNodes[1];
			if( heartbeat.NodeName != "heartbeat" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( heartbeat.GetNamedAttribute( "status" ).InnerText != "ok" )
			{
				var error = heartbeat.FirstChild;
				var code = error.GetNamedChildNode( "code" ).InnerText;
				var description = error.GetNamedChildNode( "description" ).InnerText;
				var reject = error.GetNamedChildNode( "reject" ).InnerText.ToBooleanFromString();

				throw new Exception( "Parse Error: " + description + " (" + code + ')' );
			}

			return new HeartbeatResponse()
			{
				Time = heartbeat.GetNamedAttribute( "time" ).InnerText.ToDateTimeOffsetFromUnixTime(),
				WatchCount = heartbeat.GetNamedChildNode( "watchCount" ).InnerText.ToUInt(),
				CommentCount = heartbeat.GetNamedChildNode( "commentCount" ).InnerText.ToUInt(),
				IsRestrict = heartbeat.GetNamedChildNode( "is_restrict" ).InnerText.ToBooleanFrom1(),
				Ticket = heartbeat.GetNamedChildNode( "ticket" ).InnerText,
				WaitTime = heartbeat.GetNamedChildNode( "waitTime" ).InnerText.ToUShort(),
			};
		}

		public static IAsyncOperation<HeartbeatResponse> HeartbeatAsync( NiconicoContext context, string targetId )
		{
			return HeartbeatDataAsync( context, targetId )
				.AsTask()
				.ContinueWith( prevTask => ParseHeartbeatData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}