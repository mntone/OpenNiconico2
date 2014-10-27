using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	internal sealed class ReservationsInDetailClient
	{
		public static Task<string> GetReservationsInDetailDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.LiveWatchingReservationDetailListUrl );
		}

		public static ReservationsInDetailResponse ParseReservationsInDetailData( string reservationsInDatailData )
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
			if( listXml.GetName() != "timeshift_reserved_detail_list" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new ReservationsInDetailResponse( listXml );
		}

		public static Task<ReservationsInDetailResponse> GetReservationsInDetailAsync( NiconicoContext context )
		{
			return GetReservationsInDetailDataAsync( context )
				.ContinueWith( prevTask => ParseReservationsInDetailData( prevTask.Result ) );
		}
	}
}