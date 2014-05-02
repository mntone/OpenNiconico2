using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.Reservations
{
	internal sealed class ReservationsClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetReservationsInDetailDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveWatchingReservationListUrl ) );
		}

		public static IReadOnlyList<string> ParseReservationsData( string reservationsInDatailData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( reservationsInDatailData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 5 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "nicolive_video_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var listXml = responseXml.FirstChild;
			if( listXml.NodeName != "timeshift_reserved_list" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( listXml.FirstChild != null )
			{
				return listXml.ChildNodes.Select( vidXml => "lv" + vidXml.InnerText ).ToList();
			}
			return new List<string>();
		}

		public static IAsyncOperation<IReadOnlyList<string>> GetReservationsInDetailAsync( NiconicoContext context )
		{
			return GetReservationsInDetailDataAsync( context )
				.AsTask()
				.ContinueWith( prevTask => ParseReservationsData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}