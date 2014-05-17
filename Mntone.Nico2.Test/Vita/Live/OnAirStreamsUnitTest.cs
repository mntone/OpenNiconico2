using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live;
using Mntone.Nico2.Vita;
using Mntone.Nico2.Vita.Live.OnAirPrograms;
using Newtonsoft.Json.Linq;
using System;

namespace Mntone.Nico2.Test.Vita.Live
{
	[TestClass]
	public sealed class OnAirProgramsUnitTest
	{
		private void CheckMethod( string data )
		{
			var ret = OnAirProgramsClient.ParseOnAirProgramsData( data );
			var ret2 = JObject.Parse( data )["nicolive_video_response"];

			var ret2VideoInfoList = ret2["video_info"].AsJEnumerable();
			for( var i = 0; i < ret.Programs.Count; ++i )
			{
				var retVideoInfo = ret.Programs[i];
				var ret2VideoInfo = ret2VideoInfoList[i];

				var retVideo = retVideoInfo.Video;
				var ret2Video = ret2VideoInfo["video"];
				Assert.AreEqual( ret2Video["id"].Value<string>(), retVideo.ID );
				Assert.AreEqual( ret2Video["title"].Value<string>(), retVideo.Title );
				Assert.AreEqual( ret2Video["open_time"].Value<string>().ToDateTimeOffsetFromIso8601(), retVideo.OpenedAt );
				Assert.AreEqual( ret2Video["start_time"].Value<string>().ToDateTimeOffsetFromIso8601(), retVideo.StartedAt );
				Assert.AreEqual( ret2Video["end_time"].Value<string>().ToDateTimeOffsetFromIso8601(), retVideo.EndedAt );

				var comType = ret2Video["provider_type"].Value<string>().ToCommunityType();
				Assert.AreEqual( comType == CommunityType.Official, retVideo.IsOfficial );
				Assert.AreEqual( comType == CommunityType.Channel, retVideo.IsChannel );
				Assert.AreEqual( comType == CommunityType.Community, retVideo.IsCommunity );
				Assert.AreEqual( comType, retVideo.CommunityType );

				Assert.AreEqual( ret2Video["related_channel_id"].Value<string>(), retVideo.RelatedChannelID );
				Assert.AreEqual( ret2Video["_picture_url"] != null ? ret2Video["_picture_url"].Value<string>().ToUri() : null, retVideo.ThumbnailUrl );
				Assert.AreEqual( ret2Video["_thumbnail_url"] != null ? ret2Video["_thumbnail_url"].Value<string>().ToUri() : null, retVideo.SmallThumbnailUrl );
				Assert.AreEqual( ret2Video["hidescore_online"].Value<ushort>(), retVideo.HidescoreOnline );
				Assert.AreEqual( ret2Video["hidescore_comment"].Value<ushort>(), retVideo.HidescoreComment );
				Assert.AreEqual( ret2Video["community_only"].Value<string>().ToBooleanFrom1() || ret2Video["channel_only"].Value<string>().ToBooleanFrom1(), retVideo.IsMemberOnly );
				Assert.AreEqual( ret2Video["view_counter"].Value<uint>(), retVideo.ViewCount );
				Assert.AreEqual( ret2Video["comment_count"].Value<uint>(), retVideo.CommentCount );
				Assert.AreEqual( ret2Video["_ts_reserved_count"].Value<uint>(), retVideo.TimeshiftReservedCount );
				Assert.AreEqual( ret2Video["timeshift_enabled"].Value<ushort>(), retVideo.TimeshiftEnabled );
				Assert.AreEqual( ret2Video["is_hq"].Value<string>().ToBooleanFrom1(), retVideo.IsHighQuality );

				var retCommunity = retVideoInfo.Community;
				var ret2Community = ret2VideoInfo["community"] as JObject;
				if( ret2Community != null )
				{
					Assert.AreEqual( ret2Community["id"].Value<uint>(), retCommunity.RawID );
					Assert.AreEqual( ret2Community["channel_id"].Value<string>(), retCommunity.ChannelID );
					Assert.AreEqual( ret2Community["global_id"].Value<string>(), retCommunity.ID );
					Assert.AreEqual( ret2Community["name"].Value<string>(), retCommunity.Name );
					Assert.AreEqual( ret2Community["thumbnail"].Value<string>().ToUri(), retCommunity.ThumbnailUrl );
					Assert.AreEqual( ret2Community["thumbnail_small"].Value<string>().ToUri(), retCommunity.SmallThumbnailUrl );
				}
			}

			Assert.AreEqual( ret2["count"].Value<ushort>(), ret.ParticalCount );
			Assert.AreEqual( ret2["total_count"].Value<ushort>(), ret.TotalCount );
		}

		[TestMethod]
		public void OnAirPrograms_0不正なRange()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>( () =>
			{
				OnAirProgramsClient.GetOnAirProgramsAsync( new NiconicoVitaContext(), CommunityType.Official, SortDirection.Ascending, SortType.StartTime, Range.FromFor( 0, 150 ) ).GetAwaiter().GetResult();
			} );
		}

		[TestMethod]
		public void OnAirPrograms_1officialデータ()
		{
			var data = TestHelper.Load( @"Vita/Live/OnAirPrograms/official.json" );
			CheckMethod( data );
		}

		[TestMethod]
		public void OnAirPrograms_2channelデータ()
		{
			var data = TestHelper.Load( @"Vita/Live/OnAirPrograms/channel.json" );
			CheckMethod( data );
		}

		[TestMethod]
		public void OnAirPrograms_3communityデータ()
		{
			var data = TestHelper.Load( @"Vita/Live/OnAirPrograms/community.json" );
			CheckMethod( data );
		}
	}
}