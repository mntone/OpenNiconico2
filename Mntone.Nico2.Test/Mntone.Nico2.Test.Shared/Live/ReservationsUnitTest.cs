using Mntone.Nico2.Live.Reservations;
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
	public sealed class ReservationsUnitTest
	{
		[TestMethod]
		public void Reservations_0正常データ()
		{
			var data = TestHelper.Load( @"Live/Reservations/default.xml" );
			var ret = ReservationsClient.ParseReservationsData( data );
			var ret2 = XDocument.Parse( data ).Element( "nicolive_video_response" ).Element( "timeshift_reserved_list" );

			var list = ret2.Elements().GetEnumerator();
			list.MoveNext();
			for( var i = 0; i < ret.Count; ++i )
			{
				Assert.AreEqual( "lv" + list.Current.Value, ret[i] );
				list.MoveNext();
			}
		}

		[TestMethod]
		public void Reservations_1ゼロデータ()
		{
			var data = TestHelper.Load( @"Live/Reservations/zero.xml" );
			var ret = ReservationsClient.ParseReservationsData( data );
			Assert.AreEqual( 0, ret.Count );
		}
	}
}