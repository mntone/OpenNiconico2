using Mntone.Nico2.Live.ReservationsInDetail;
using System;
using System.Xml.Linq;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class ReservationsInDetailUnitTest
	{
		[TestMethod]
		public void ReservationsInDetail_0正常データ()
		{
			var data = TestHelper.Load( @"Live/ReservationsInDetail/default.xml" );
			var ret = ReservationsInDetailClient.ParseReservationsInDetailData( data );
			var ret2 = XDocument.Parse( data ).Element( "nicolive_video_response" ).Element( "timeshift_reserved_detail_list" );

			var list = ret2.Elements().GetEnumerator();
			list.MoveNext();
			for( var i = 0; i < ret.ReservedProgram.Count; ++i )
			{
				Assert.AreEqual( "lv" + list.Current.Element( "vid" ).Value, ret.ReservedProgram[i].Id );
				Assert.AreEqual( list.Current.Element( "title" ).Value, ret.ReservedProgram[i].Title );
				Assert.AreEqual( list.Current.Element( "status" ).Value, ret.ReservedProgram[i].Status );
				Assert.AreEqual( list.Current.Element( "unwatch" ).Value.ToBooleanFrom1(), ret.ReservedProgram[i].IsUnwatched );

				var expire = list.Current.Element( "expire" ).Value;
				Assert.AreEqual( expire != "0" ? expire.ToDateTimeOffsetFromUnixTime() : DateTimeOffset.MaxValue, ret.ReservedProgram[i].ExpiredAt );
				list.MoveNext();
			}
		}

		[TestMethod]
		public void ReservationsInDetail_1ゼロデータ()
		{
			var data = TestHelper.Load( @"Live/ReservationsInDetail/zero.xml" );
			var ret = ReservationsInDetailClient.ParseReservationsInDetailData( data );
			Assert.AreEqual( 0, ret.ReservedProgram.Count );
		}
	}
}