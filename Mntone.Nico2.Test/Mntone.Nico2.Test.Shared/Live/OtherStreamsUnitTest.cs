using Mntone.Nico2.Live;
using Mntone.Nico2.Live.OtherStreams;
using Newtonsoft.Json.Linq;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class OtherStreamsUnitTest
	{
		[TestMethod]
		public void OtherStreams_0未来の一覧データ()
		{
			var data = TestHelper.Load( @"Live/OtherStreams/comingsoon.json" );
			var ret = OtherStreamsClient.ParseOtherStreamsData( data );
			var ret2 = JObject.Parse( data );

			var ret2Streams = ret2["reserved_stream_list"].AsJEnumerable();
			for( var i = 0; i < ret.Streams.Count; ++i )
			{
				var retStream = ret.Streams[i];
				var ret2Stream = ret2Streams[i];
				Assert.AreEqual( "lv" + ret2Stream["id"].Value<string>(), retStream.Id );
				Assert.AreEqual( ret2Stream["currentstatus"].Value<string>().ToStatusType(), retStream.Status );
				Assert.AreEqual( ret2Stream["title"].Value<string>(), retStream.Title );
				Assert.AreEqual( ret2Stream["description"].Value<string>(), retStream.Description );
				Assert.AreEqual( ret2Stream["is_exclude_non_display"].Value<bool>(), retStream.IsHidden );
				Assert.AreEqual( ret2Stream["is_exclude_private"].Value<bool>(), retStream.IsPrivate );
				Assert.AreEqual( ret2Stream["is_product"].Value<bool>(), retStream.IsProduct );
				Assert.AreEqual( ret2Stream["timeshift_enabled"].Value<bool>() ? 1 : 0, retStream.TimeshiftEnabled );
				Assert.AreEqual( ret2Stream["is_timeshift_already_closed"].Value<bool>(), retStream.IsTimeshiftClosed );
				Assert.AreEqual( ret2Stream["is_timeshift_preparing"].Value<bool>(), retStream.IsTimeshiftPreparing );
				Assert.AreEqual( ret2Stream["picture_url"].Value<string>().ToUri(), retStream.ThumbnailUrl );
				Assert.AreEqual( ret2Stream["ticket_url"] != null ? ret2Stream["ticket_url"].Value<string>().ToUri() : null, retStream.TicketPageUrl );
				Assert.AreEqual( ret2Stream["twitter_disabled"].Value<bool>(), retStream.IsTwitterDisabled );
				Assert.AreEqual( ret2Stream["twitter_tag"].Value<string>(), retStream.TwitterHashtag );
				Assert.AreEqual( ret2Stream["view_counter"].Value<uint>(), retStream.ViewCount );
				Assert.AreEqual( ret2Stream["comment_count"].Value<uint>(), retStream.CommentCount );
				Assert.AreEqual( ret2Stream["timeshift_reserved_count"].Value<uint>(), retStream.TimeshiftReservedCount );
				Assert.AreEqual( ret2Stream["start_date_timestamp_sec"].Value<long>().ToDateTimeOffsetFromUnixTime(), retStream.StartedAt );
				Assert.AreEqual( ret2Stream["end_date_timestamp_sec"].Value<long>().ToDateTimeOffsetFromUnixTime(), retStream.EndedAt );

				var comType = ret2Stream["provider_type"].Value<string>().ToCommunityType();
				Assert.AreEqual( comType == CommunityType.Official, retStream.IsOfficial );
				Assert.AreEqual( comType == CommunityType.Channel, retStream.IsChannel );
				Assert.AreEqual( comType == CommunityType.Community, retStream.IsCommunity );
				Assert.AreEqual( comType, retStream.CommunityType );

				Assert.AreEqual( ret2Stream["view_channel_icon"] != null ? ret2Stream["view_channel_icon"].Value<bool>() : false, retStream.IsChannelIconEnabled );
				Assert.AreEqual( ret2Stream["closed_total_template"].Value<string>(), retStream.ClosedTotalTemplate );
			}

			Assert.AreEqual( ret2["total"].Value<ushort>(), ret.TotalCount );
		}

		[TestMethod]
		public void OtherStreams_1過去の一覧データ()
		{
			var data = TestHelper.Load( @"Live/OtherStreams/closed.json" );
			var ret = OtherStreamsClient.ParseOtherStreamsData( data );
			var ret2 = JObject.Parse( data );

			var ret2Streams = ret2["reserved_stream_list"].AsJEnumerable();
			for( var i = 0; i < ret.Streams.Count; ++i )
			{
				var retStream = ret.Streams[i];
				var ret2Stream = ret2Streams[i];
				Assert.AreEqual( "lv" + ret2Stream["id"].Value<string>(), retStream.Id );
				Assert.AreEqual( ret2Stream["currentstatus"].Value<string>().ToStatusType(), retStream.Status );
				Assert.AreEqual( ret2Stream["title"].Value<string>(), retStream.Title );
				Assert.AreEqual( ret2Stream["description"].Value<string>(), retStream.Description );
				Assert.AreEqual( ret2Stream["is_exclude_non_display"].Value<bool>(), retStream.IsHidden );
				Assert.AreEqual( ret2Stream["is_exclude_private"].Value<bool>(), retStream.IsPrivate );
				Assert.AreEqual( ret2Stream["is_product"].Value<bool>(), retStream.IsProduct );
				Assert.AreEqual( ret2Stream["timeshift_enabled"].Value<ushort>(), retStream.TimeshiftEnabled );
				Assert.AreEqual( ret2Stream["is_timeshift_already_closed"].Value<bool>(), retStream.IsTimeshiftClosed );
				Assert.AreEqual( ret2Stream["is_timeshift_preparing"].Value<bool>(), retStream.IsTimeshiftPreparing );
				Assert.AreEqual( ret2Stream["picture_url"].Value<string>().ToUri(), retStream.ThumbnailUrl );
				Assert.AreEqual( ret2Stream["ticket_url"] != null ? ret2Stream["ticket_url"].Value<string>().ToUri() : null, retStream.TicketPageUrl );
				Assert.AreEqual( ret2Stream["twitter_disabled"].Value<bool>(), retStream.IsTwitterDisabled );
				Assert.AreEqual( ret2Stream["twitter_tag"].Value<string>(), retStream.TwitterHashtag );
				Assert.AreEqual( ret2Stream["view_counter"].Value<uint>(), retStream.ViewCount );
				Assert.AreEqual( ret2Stream["comment_count"].Value<uint>(), retStream.CommentCount );
				Assert.AreEqual( ret2Stream["timeshift_reserved_count"].Value<uint>(), retStream.TimeshiftReservedCount );
				Assert.AreEqual( ret2Stream["start_date_timestamp_sec"].Value<long>().ToDateTimeOffsetFromUnixTime(), retStream.StartedAt );
				Assert.AreEqual( ret2Stream["end_date_timestamp_sec"].Value<long>().ToDateTimeOffsetFromUnixTime(), retStream.EndedAt );

				var comType = ret2Stream["provider_type"].Value<string>().ToCommunityType();
				Assert.AreEqual( comType == CommunityType.Official, retStream.IsOfficial );
				Assert.AreEqual( comType == CommunityType.Channel, retStream.IsChannel );
				Assert.AreEqual( comType == CommunityType.Community, retStream.IsCommunity );
				Assert.AreEqual( comType, retStream.CommunityType );

				Assert.AreEqual( ret2Stream["view_channel_icon"] != null ? ret2Stream["view_channel_icon"].Value<bool>() : false, retStream.IsChannelIconEnabled );
				Assert.AreEqual( ret2Stream["closed_total_template"].Value<string>(), retStream.ClosedTotalTemplate );
			}

			Assert.AreEqual( ret2["total"].Value<ushort>(), ret.TotalCount );
		}

		[TestMethod]
		public void OtherStreams_2ゼロデータ()
		{
			var data = TestHelper.Load( @"Live/OtherStreams/zero.json" );
			var ret = OtherStreamsClient.ParseOtherStreamsData( data );
			Assert.AreEqual( 0, ret.Streams.Count );
			Assert.AreEqual( 0, ret.TotalCount );
		}
	}
}