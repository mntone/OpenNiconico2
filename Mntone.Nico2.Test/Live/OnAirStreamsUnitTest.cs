using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live.OnAirStreams;
using Newtonsoft.Json.Linq;

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class OnAirStreamsUnitTest
	{
		[TestMethod]
		public void OnAirStreams_0通常データ()
		{
			var data = TestHelper.Load( @"Live/OnAirStreams/default.json" );
			var ret = OnAirStreamsClient.ParseOnAirStreamsData( data );
			var ret2 = JObject.Parse( data );

			var ret2OnAirStreams = ret2["onair_stream_list"].AsJEnumerable();
			for( var i = 0; i < ret.OnAirStreams.Count; ++i )
			{
				var retOnAirStream = ret.OnAirStreams[i];
				var ret2OnAirStream = ret2OnAirStreams[i];
				Assert.AreEqual( ret2OnAirStream["hide_zapping"].Value<bool>(), retOnAirStream.IsHidden );
				Assert.AreEqual( "lv" + ret2OnAirStream["id"].Value<string>(), retOnAirStream.ID );
				Assert.AreEqual( ret2OnAirStream["is_nsen"].Value<bool>(), retOnAirStream.IsNsen );
				Assert.AreEqual( ret2OnAirStream["is_product"].Value<bool>(), retOnAirStream.IsProduct );
				Assert.AreEqual( ret2OnAirStream["is_zapping_mode_enabled"].Value<bool>(), retOnAirStream.IsZappingModeEnabled );
				Assert.AreEqual( ret2OnAirStream["thumbnail_small_url"].Value<string>(), retOnAirStream.SmallThumbnailUrl.ToString() );
				Assert.AreEqual( ret2OnAirStream["title"].Value<string>(), retOnAirStream.Title );
				Assert.AreEqual( ret2OnAirStream["view_counter"].Value<uint>(), retOnAirStream.ViewCount );
			}

			var ret2ReservedStreams = ret2["reserved_stream_list"].AsJEnumerable();
			for( var i = 0; i < ret.ReservedStreams.Count; ++i )
			{
				var retResevedStream = ret.ReservedStreams[i];
				var ret2ReservedStream = ret2ReservedStreams[i];
				Assert.AreEqual( ret2ReservedStream["gauge_level"].Value<ushort>(), retResevedStream.GaugeLevel );
				Assert.AreEqual( ret2ReservedStream["hide_zapping"].Value<bool>(), retResevedStream.IsHidden );
				Assert.AreEqual( "lv" + ret2ReservedStream["id"].Value<string>(), retResevedStream.ID );
				Assert.AreEqual( ret2ReservedStream["is_nsen"].Value<bool>(), retResevedStream.IsNsen );
				Assert.AreEqual( ret2ReservedStream["is_product"].Value<bool>(), retResevedStream.IsProduct );
				Assert.AreEqual( ret2ReservedStream["is_zapping_mode_enabled"].Value<bool>(), retResevedStream.IsZappingModeEnabled );
				Assert.AreEqual( ret2ReservedStream["open_time"].Value<long>().ToDateTimeOffsetFromUnixTime(), retResevedStream.OpenedAt );
				Assert.AreEqual( ret2ReservedStream["thumbnail_small_url"].Value<string>(), retResevedStream.SmallThumbnailUrl.ToString() );
				Assert.AreEqual( ret2ReservedStream["title"].Value<string>(), retResevedStream.Title );
			}
		}
		[TestMethod]
		public void OnAirStreams_1最近の一覧データ()
		{
			var data = TestHelper.Load( @"Live/OnAirStreams/recent.json" );
			var ret = OnAirStreamsClient.ParseOnAirStreamsData( data );
			var ret2 = JObject.Parse( data );

			var ret2OnAirStreams = ret2["onair_stream_list"].AsJEnumerable();
			for( var i = 0; i < ret.OnAirStreams.Count; ++i )
			{
				var retOnAirStream = ret.OnAirStreams[i];
				var ret2OnAirStream = ret2OnAirStreams[i];
				Assert.AreEqual( ret2OnAirStream["hide_zapping"].Value<bool>(), retOnAirStream.IsHidden );
				Assert.AreEqual( "lv" + ret2OnAirStream["id"].Value<string>(), retOnAirStream.ID );
				Assert.AreEqual( ret2OnAirStream["is_nsen"].Value<bool>(), retOnAirStream.IsNsen );
				Assert.AreEqual( ret2OnAirStream["is_product"].Value<bool>(), retOnAirStream.IsProduct );
				Assert.AreEqual( ret2OnAirStream["is_zapping_mode_enabled"].Value<bool>(), retOnAirStream.IsZappingModeEnabled );
				Assert.AreEqual( ret2OnAirStream["thumbnail_small_url"].Value<string>(), retOnAirStream.SmallThumbnailUrl.ToString() );
				Assert.AreEqual( ret2OnAirStream["title"].Value<string>(), retOnAirStream.Title );
				Assert.AreEqual( ret2OnAirStream["view_counter"].Value<uint>(), retOnAirStream.ViewCount );
			}

			Assert.IsNull( ret.ReservedStreams );
		}

		[TestMethod]
		public void OnAirStreams_2ゼロデータ()
		{
			var data = TestHelper.Load( @"Live/OnAirStreams/zero.json" );
			var ret = OnAirStreamsClient.ParseOnAirStreamsData( data );
			Assert.AreEqual( 0, ret.OnAirStreams.Count );
			Assert.AreEqual( 0, ret.ReservedStreams.Count );
		}
	}
}