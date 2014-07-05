using Mntone.Nico2.Live;
using Mntone.Nico2.Vita.Live;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Vita.Live
{
	public sealed class LiveAssert
	{
		public static void CheckSimpleVideo( IJEnumerable<JToken> expected, VideoInfo actual )
		{
			CheckVideo( expected, actual );

			Assert.AreEqual( string.Empty, actual.Description );
			Assert.AreEqual( ( ushort )0u, actual.UserID );

			Assert.AreEqual( StatusType.Invalid, actual.Status );

			Assert.AreEqual( 0, actual.TimeshiftLimit );
			Assert.AreEqual( DateTimeOffset.MinValue, actual.TimeshiftArchiveReleasedAt );
			Assert.IsFalse( actual.IsTimeshiftUsed );
			Assert.AreEqual( DateTimeOffset.MinValue, actual.TimeshiftArchiveStartedAt );
			Assert.AreEqual( DateTimeOffset.MinValue, actual.TimeshiftArchiveEndedAt );
			Assert.AreEqual( 0u, actual.TimeshiftViewLimitCount );
			Assert.IsFalse( actual.IsTimeshiftEndless );
		}

		public static void CheckDetailVideo( IJEnumerable<JToken> expected, VideoInfo actual )
		{
			CheckVideo( expected, actual );

			Assert.AreEqual( expected["description"].Value<string>(), actual.Description );
			Assert.AreEqual( expected["user_id"].Value<uint>(), actual.UserID );

			Assert.AreEqual( expected["_currentstatus"].Value<string>().ToStatusType(), actual.Status );

			Assert.AreEqual( expected["_timeshift_limit"].Value<int>(), actual.TimeshiftLimit );
			Assert.AreEqual( expected["_ts_archive_released_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.TimeshiftArchiveReleasedAt );
			Assert.AreEqual( expected["_use_tsarchive"].Value<string>().ToBooleanFrom1(), actual.IsTimeshiftUsed );
			Assert.AreEqual( expected["_ts_archive_start_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.TimeshiftArchiveStartedAt );
			Assert.AreEqual( expected["_ts_archive_end_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.TimeshiftArchiveEndedAt );
			Assert.AreEqual( expected["_ts_view_limit_num"].Value<ushort>(), actual.TimeshiftViewLimitCount );
			Assert.AreEqual( expected["_ts_is_endless"].Value<string>().ToBooleanFrom1(), actual.IsTimeshiftEndless );
		}

		private static void CheckVideo( IJEnumerable<JToken> expected, VideoInfo actual )
		{
			Assert.AreEqual( expected["id"].Value<string>(), actual.ID );
			Assert.AreEqual( expected["title"].Value<string>(), actual.Title );
			Assert.AreEqual( expected["open_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.OpenedAt );
			Assert.AreEqual( expected["start_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.StartedAt );
			Assert.AreEqual( expected["schedule_end_time"] != null ? expected["schedule_end_time"].Value<string>().ToDateTimeOffsetFromIso8601() : DateTimeOffset.MinValue, actual.EndedAtInPlan );
			Assert.AreEqual( expected["end_time"].Value<string>().ToDateTimeOffsetFromIso8601(), actual.EndedAt );

			var comType = expected["provider_type"].Value<string>().ToCommunityType();
			Assert.AreEqual( comType == CommunityType.Official, actual.IsOfficial );
			Assert.AreEqual( comType == CommunityType.Channel, actual.IsChannel );
			Assert.AreEqual( comType == CommunityType.Community, actual.IsCommunity );
			Assert.AreEqual( comType, actual.CommunityType );

			Assert.AreEqual( expected["related_channel_id"].Value<string>(), actual.RelatedChannelID );
			Assert.AreEqual( expected["_picture_url"] != null ? expected["_picture_url"].Value<string>().ToUri() : null, actual.ThumbnailUrl );
			Assert.AreEqual( expected["_thumbnail_url"] != null ? expected["_thumbnail_url"].Value<string>().ToUri() : null, actual.SmallThumbnailUrl );
			Assert.AreEqual( expected["hidescore_online"].Value<ushort>(), actual.HidescoreOnline );
			Assert.AreEqual( expected["hidescore_comment"].Value<ushort>(), actual.HidescoreComment );
			Assert.AreEqual( expected["community_only"].Value<string>().ToBooleanFrom1() || expected["channel_only"].Value<string>().ToBooleanFrom1(), actual.IsMemberOnly );
			Assert.AreEqual( expected["view_counter"].Value<uint>(), actual.ViewCount );
			Assert.AreEqual( expected["comment_count"].Value<uint>(), actual.CommentCount );
			Assert.AreEqual( expected["_ts_reserved_count"].Value<uint>(), actual.TimeshiftReservedCount );

			Assert.AreEqual( expected["timeshift_enabled"].Value<ushort>(), actual.TimeshiftEnabled );
			Assert.AreEqual( expected["is_hq"].Value<string>().ToBooleanFrom1(), actual.IsHighQuality );
		}

		public static void CheckSimpleCommunity( IJEnumerable<JToken> expected, CommunityInfo actual )
		{
			if( CheckCommunity( expected, actual ) )
			{
				Assert.AreEqual( 0u, actual.UserCount );
				Assert.AreEqual( ( ushort )0u, actual.Level );
			}
		}

		public static void CheckDetailCommunity( IJEnumerable<JToken> expected, CommunityInfo actual )
		{
			if( CheckCommunity( expected, actual ) )
			{
				Assert.AreEqual( expected["user_count"].Value<uint>(), actual.UserCount );
				Assert.AreEqual( expected["level"].Value<ushort>(), actual.Level );
			}
		}

		private static bool CheckCommunity( IJEnumerable<JToken> expected, CommunityInfo actual )
		{
			var expected2 = expected as JObject;
			if( expected2 != null )
			{
				Assert.AreEqual( expected["id"].Value<uint>(), actual.RawID );
				Assert.AreEqual( expected["channel_id"] != null ? expected["channel_id"].Value<string>() : null, actual.ChannelID );
				Assert.AreEqual( expected["global_id"].Value<string>(), actual.ID );
				Assert.AreEqual( expected["name"].Value<string>(), actual.Name );
				Assert.AreEqual( expected["thumbnail"].Value<string>().ToUri(), actual.ThumbnailUrl );
				Assert.AreEqual( expected["thumbnail_small"].Value<string>().ToUri(), actual.SmallThumbnailUrl );
				return true;
			}
			else
			{
				Assert.IsNull( actual );
				return false;
			}
		}

		public static void CheckTags( IJEnumerable<JToken> expected, TagsInfo actual )
		{
			var expectedCategoryTags = expected["category"]; if( expectedCategoryTags != null ) expectedCategoryTags = expectedCategoryTags["livetag"]; var ci = 0;
			var expectedLockedTags = expected["locked"]; if( expectedLockedTags != null ) expectedLockedTags = expectedLockedTags["livetag"]; var li = 0;
			var expectedTags = expected["free"]; if( expectedTags != null ) expectedTags = expectedTags["livetag"]; var fi = 0;

			for( var i = 0; i < actual.Tags.Count; ++i )
			{
				var actualTag = actual.Tags[i];
				if( actualTag.IsCategoryTag )
				{
					var expectedTag = expectedCategoryTags.Value<string>();
					if( expectedTag == null )
					{
						expectedTag = expectedCategoryTags[ci++].Value<string>();
					}
					Assert.AreEqual( expectedTag, actualTag.Value );
				}
				else if( actualTag.IsLocked )
				{
					var expectedTag = expectedLockedTags[li++].Value<string>();
					Assert.AreEqual( expectedTag, actualTag.Value );
				}
				else
				{
					var expectedTag = expectedTags[fi++].Value<string>();
					Assert.AreEqual( expectedTag, actualTag.Value );
				}
			}
		}
	}
}