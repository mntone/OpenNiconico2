using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	internal sealed class ReservationsInDetailClient
	{
		public static Task<string> GetReservationsInDetailDataAsync( NiconicoContext context )
		{
			return context.GetClient()
				.GetBufferAsync( new Uri( NiconicoUrls.LiveWatchingReservationDetailListUrl ) )
				.AsTask()
				.ContinueWith( buffer => new StreamReader( buffer.Result.AsStream(), Encoding.UTF8 ).ReadToEnd() );
		}

		public static ReservationsInDetailResponse ParseReservationsInDetailData( string reservationsInDatailData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( reservationsInDatailData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 5 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "nicolive_video_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var listXml = responseXml.FirstChild;
			if( listXml.NodeName != "timeshift_reserved_detail_list" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new ReservationsInDetailResponse( listXml );
		}

		public static IAsyncOperation<ReservationsInDetailResponse> GetReservationsInDetailAsync( NiconicoContext context )
		{
			return GetReservationsInDetailDataAsync( context )
				.ContinueWith( prevTask => ParseReservationsInDetailData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}