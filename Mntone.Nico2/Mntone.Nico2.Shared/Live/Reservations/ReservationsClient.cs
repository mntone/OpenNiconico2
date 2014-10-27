using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.Reservations
{
	internal sealed class ReservationsClient
	{
		public static Task<string> GetReservationsDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetStringAsync( NiconicoUrls.LiveWatchingReservationListUrl );
		}

		public static IReadOnlyList<string> ParseReservationsData( string reservationsInDatailData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( reservationsInDatailData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 5 } );
#else
			var xml = XDocument.Parse( reservationsInDatailData );
#endif

			var responseXml = xml.GetDocumentRootNode();
			if( responseXml.GetName() != "nicolive_video_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var listXml = responseXml.GetFirstChildNode();
			if( listXml.GetName() != "timeshift_reserved_list" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( listXml.GetFirstChildNode() != null )
			{
				return listXml.GetChildNodes().Select( vidXml => "lv" + vidXml.GetText() ).ToList();
			}
			return new List<string>();
		}

		public static Task<IReadOnlyList<string>> GetReservationsAsync( NiconicoContext context )
		{
			return GetReservationsDataAsync( context )
				.ContinueWith( prevTask => ParseReservationsData( prevTask.Result ) );
		}
	}
}