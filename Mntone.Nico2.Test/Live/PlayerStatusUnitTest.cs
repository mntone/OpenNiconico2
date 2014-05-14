using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live;
using Mntone.Nico2.Live.PlayerStatus;
using System;

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class PlayerStatusUnitTest
	{
		[TestMethod]
		public void PlayerStatus_0不正なID()
		{
			Assert.ThrowsException<ArgumentException>( () =>
			{
				PlayerStatusClient.GetPlayerStatusDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "llv1131" ).GetAwaiter().GetResult();
			} );
		}

		[TestMethod]
		public void PlayerStatus_1通常データ()
		{
			var ret = PlayerStatusClient.ParsePlayerStatusData( TestHelper.Load( @"Live/PlayerStatus/default.xml" ) );

			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 8, 8, 2, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.AreEqual( "lv168235211", ret.Program.ID );
			Assert.AreEqual( "ゼルダの伝説トワイライトプリンセス", ret.Program.Title );
			Assert.AreEqual( "いい加減クリアしますー！５人で色々な放送をやってます！コミュニティをどうぞよろしく！！→co1342124サブコミュ→co1842118", ret.Program.Description );

			Assert.AreEqual( 13u, ret.Program.WatchCount );
			Assert.AreEqual( 13u, ret.Program.CommentCount );

			Assert.IsFalse( ret.Program.IsOfficial );
			Assert.IsFalse( ret.Program.IsChannel );
			Assert.IsTrue( ret.Program.IsCommunity );
			Assert.AreEqual( CommunityType.Community, ret.Program.CommunityType );
			Assert.AreEqual( "co1342124", ret.Program.CommunityID );
			Assert.AreEqual( 7051053u, ret.Program.BroadcasterID );
			Assert.AreEqual( "ひろやす＠ピュアホワイト", ret.Program.BroadcasterName );

			Assert.AreEqual( 13u, ret.Program.International );

			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 7, 58, 27, TimeSpan.FromHours( 9 ) ), ret.Program.BaseAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 7, 58, 27, TimeSpan.FromHours( 9 ) ), ret.Program.OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 7, 58, 29, TimeSpan.FromHours( 9 ) ), ret.Program.StartedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 8, 28, 29, TimeSpan.FromHours( 9 ) ), ret.Program.EndedAt );
			Assert.AreEqual( DateTimeOffset.MinValue, ret.Program.TimeshiftAt );

			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv168235211?sec=nicolive_crowded&sub=watch_crowded_0_community_lv168235211_onair", ret.Program.BourbonUrl.ToString() );
			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv168235211?sec=nicolive_crowded&sub=watch_crowded_0_community_lv168235211_onair", ret.Program.CrowdedUrl.ToString() );
			Assert.IsNull( ret.Program.BeforeUrl );
			Assert.IsNull( ret.Program.AfterUrl );
			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv168235211?sec=nicolive_oidashi&sub=watchplayer_oidashialert_0_community_lv168235211_onair", ret.Program.KickOutUrl.ToString() );
			Assert.AreEqual( "http://nl.simg.jp/img/201311/281696.a29344.png", ret.Program.KickOutImageUrl.ToString() );
			Assert.IsNull( ret.Program.CommunityImageUrl );
			Assert.IsNull( ret.Program.CommunitySmallImageUrl );
			Assert.IsNull( ret.Program.TicketUrl );
			Assert.IsNull( ret.Program.BannerUrl );
			Assert.IsNull( ret.Program.ShutterUrl );

			Assert.IsFalse( ret.Program.IsRerun );
			Assert.IsFalse( ret.Program.IsArchive );
			Assert.IsTrue( ret.Program.IsLive );

			Assert.IsFalse( ret.Program.IsNewComer );
			Assert.IsFalse( ret.Program.IsCruise );
			Assert.IsFalse( ret.Program.IsNsen );
			Assert.AreEqual( ProgramExtendedType.None, ret.Program.ExtendedType );

			Assert.IsFalse( ret.Program.IsInfinity );
			Assert.IsFalse( ret.Program.IsReserved );
			Assert.IsFalse( ret.Program.IsArchivePlayServer );
			Assert.IsTrue( ret.Program.IsTimeshiftEnabled );

			Assert.IsFalse( ret.Program.IsProductEnabled );
			Assert.IsFalse( ret.Program.IsTrialEnabled );
			Assert.IsFalse( ret.Program.IsBannerForced );

			Assert.IsTrue( ret.Program.IsNoticeBalloonEnabled );
			Assert.IsTrue( ret.Program.IsErrorReportEnabled );

			Assert.AreEqual( string.Empty, ret.Program.NsenType );
			Assert.AreEqual( string.Empty, ret.Program.NsenCommand );

			Assert.IsFalse( ret.Program.Twitter.IsEnabled );
			Assert.AreEqual( "#co1342124", ret.Program.Twitter.Hashtag );
			Assert.AreEqual( 10000u, ret.Program.Twitter.VipModeCount );

			Assert.AreEqual( "co1342124", ret.Room.Name );
			Assert.AreEqual( 8u, ret.Room.SeatID );
			Assert.AreEqual( string.Empty, ret.Room.SeatToken );

			Assert.IsTrue( ret.Stream.IsFlashMediaServer );
			Assert.AreEqual( 80u, ret.Stream.RtmptPort );
			Assert.AreEqual( "rtmp://nleu12.live.nicovideo.jp:1935/liveedge/live_140205_08_1", ret.Stream.RtmpUrl.ToString() );
			Assert.AreEqual( "20929324:lv168235211:0:1391555282:0417f76b5298cce3", ret.Stream.Ticket );
			Assert.IsNull( ret.Stream.Tickets );

			Assert.AreEqual( 1, ret.Stream.Contents.Count );
			Assert.AreEqual( "main", ret.Stream.Contents[0].ID );
			Assert.IsFalse( ret.Stream.Contents[0].IsAudioDisabled );
			Assert.IsFalse( ret.Stream.Contents[0].IsVideoDisabled );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 7, 58, 28, TimeSpan.FromHours( 9 ) ), ret.Stream.Contents[0].StartedAt );
			Assert.AreEqual( string.Empty, ret.Stream.Contents[0].Title );
			Assert.AreEqual( "rtmp:rtmp://nlpoca112.live.nicovideo.jp:1935/publicorigin/140205_07_1/,lv168235211?1391555282:30:009412a24371baf8", ret.Stream.Contents[0].Value );

			Assert.AreEqual( VideoPosition.Default, ret.Stream.Position );
			Assert.AreEqual( VideoAspect.Auto, ret.Stream.Aspect );
			Assert.AreEqual( string.Empty, ret.Stream.BroadcastToken );
			Assert.IsFalse( ret.Stream.IsQualityOfServiceAnalyticsEnabled );

			Assert.IsFalse( ret.Comment.IsLocked );
			Assert.AreEqual( 1.0f, ret.Comment.Scale );
			Assert.AreEqual( string.Empty, ret.Comment.Perm );
			Assert.AreEqual( CommentPosition.Default, ret.Comment.Position );
			Assert.AreEqual( CommentFilteringLevel.None, ret.Comment.FilteringLevel );
			Assert.AreEqual( CommentSexMode.Disable, ret.Comment.SexMode );

			Assert.IsNull( ret.Comment.Commands );

			Assert.IsFalse( ret.Comment.IsRestrict );
			Assert.AreEqual( CommentLimitMode.Allow, ret.Comment.LimitMode );

			Assert.AreEqual( "msg102.live.nicovideo.jp", ret.Comment.Server.Host.ToString() );
			Assert.AreEqual( 2806u, ret.Comment.Server.Port );
			Assert.AreEqual( 1, ret.Comment.Server.ThreadIDs.Count );
			Assert.AreEqual( 1329132457u, ret.Comment.Server.ThreadIDs[0] );

			Assert.IsFalse( ret.Telop.IsEnabled );
			Assert.AreEqual( string.Empty, ret.Telop.Mail );
			Assert.AreEqual( string.Empty, ret.Telop.Value );

			Assert.IsTrue( ret.NetDuetto.IsEnabled );
			Assert.AreEqual( "8165285e634cbdf46745295a2d3659a32c975893", ret.NetDuetto.Token );

			Assert.AreEqual( "ゲーム", ret.Marquee.Category );
			Assert.AreEqual( "04678e23", ret.Marquee.GameKey );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 8, 8, 2, TimeSpan.FromHours( 9 ) ), ret.Marquee.GameTime );
			Assert.IsFalse( ret.Marquee.IsNotInterruptionForced );

			Assert.AreEqual( 20929324u, ret.User.ID );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.User.Name );
			Assert.IsTrue( ret.User.IsPremium );
			Assert.AreEqual( 21u, ret.User.Age );
			Assert.IsTrue( ret.User.IsMale );
			Assert.IsFalse( ret.User.IsFemale );
			Assert.AreEqual( Sex.Male, ret.User.Sex );
			Assert.AreEqual( "jp", ret.User.Domain );
			Assert.AreEqual( Prefecture.Osaka, ret.User.Prefecture );
			Assert.AreEqual( "ja-jp", ret.User.Language );
			Assert.AreEqual( string.Empty, ret.User.HKey );

			Assert.IsFalse( ret.User.IsOwner );
			Assert.IsFalse( ret.User.IsJoin );
			Assert.IsFalse( ret.User.IsReserved );
			Assert.IsFalse( ret.User.IsPrefecturePreferential );
			Assert.IsFalse( ret.User.IsPurchased );
			Assert.IsFalse( ret.User.IsSerialUsing );

			Assert.IsTrue( ret.User.Twitter.IsEnabled );
			Assert.AreEqual( "mntone", ret.User.Twitter.ScreenName );
			Assert.AreEqual( 366u, ret.User.Twitter.FollowersCount );
			Assert.IsFalse( ret.User.Twitter.IsVip );
			Assert.AreEqual( "http://a0.twimg.com/profile_images/2420265266/rrtyjcvhu7z5idxo8y49_normal.png", ret.User.Twitter.ProfileImageUrl.ToString() );
			Assert.IsFalse( ret.User.Twitter.IsAuthenticationRequired );
			Assert.AreEqual( "0e7db53d6e0f54c74f2f8ab210acec5f5f91c9eb", ret.User.Twitter.Token );
		}

		[TestMethod]
		public void PlayerStatus_2公式データ()
		{
			var ret = PlayerStatusClient.ParsePlayerStatusData( TestHelper.Load( @"Live/PlayerStatus/official.xml" ) );

			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 16, 14, 40, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.AreEqual( "lv169798840", ret.Program.ID );
			Assert.AreEqual( "【衆議院 国会生中継】 予算委員会", ret.Program.Title );
			Assert.AreEqual( "～平成26年2月19日 予算委員会～<br>", ret.Program.Description );

			Assert.AreEqual( 50187u, ret.Program.WatchCount );
			Assert.AreEqual( 23020u, ret.Program.CommentCount );

			Assert.IsTrue( ret.Program.IsOfficial );
			Assert.IsFalse( ret.Program.IsChannel );
			Assert.IsFalse( ret.Program.IsCommunity );
			Assert.AreEqual( CommunityType.Official, ret.Program.CommunityType );
			Assert.AreEqual( string.Empty, ret.Program.CommunityID );
			Assert.AreEqual( 394u, ret.Program.BroadcasterID );
			Assert.AreEqual( string.Empty, ret.Program.BroadcasterName );

			Assert.AreEqual( 1u, ret.Program.International );

			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 8, 55, 0, TimeSpan.FromHours( 9 ) ), ret.Program.BaseAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 8, 55, 0, TimeSpan.FromHours( 9 ) ), ret.Program.OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 9, 0, 0, TimeSpan.FromHours( 9 ) ), ret.Program.StartedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 17, 0, 0, TimeSpan.FromHours( 9 ) ), ret.Program.EndedAt );
			Assert.AreEqual( DateTimeOffset.MinValue, ret.Program.TimeshiftAt );

			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv169798840?sec=nicolive_crowded&sub=watch_crowded_0_official_lv169798840_onair", ret.Program.BourbonUrl.ToString() );
			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv169798840?sec=nicolive_crowded&sub=watch_crowded_0_official_lv169798840_onair", ret.Program.CrowdedUrl.ToString() );
			Assert.IsNull( ret.Program.BeforeUrl );
			Assert.IsNull( ret.Program.AfterUrl );
			Assert.AreEqual( "http://live.nicovideo.jp/gate/lv169798840?sec=nicolive_oidashi&sub=watchplayer_oidashialert_0_official_lv169798840_onair", ret.Program.KickOutUrl.ToString() );
			Assert.AreEqual( "http://nl.simg.jp/img/201311/281696.a29344.png", ret.Program.KickOutImageUrl.ToString() );
			Assert.AreEqual( "http://nl.simg.jp/img/a35/102457.bba76a.jpg", ret.Program.CommunityImageUrl.ToString() );
			Assert.AreEqual( "http://nl.simg.jp/img/a2/4539.198de4.jpg", ret.Program.CommunitySmallImageUrl.ToString() );
			Assert.IsNull( ret.Program.TicketUrl );
			Assert.IsNull( ret.Program.BannerUrl );
			Assert.IsNull( ret.Program.ShutterUrl );

			Assert.IsFalse( ret.Program.IsRerun );
			Assert.IsFalse( ret.Program.IsArchive );
			Assert.IsTrue( ret.Program.IsLive );

			Assert.IsFalse( ret.Program.IsNewComer );
			Assert.IsFalse( ret.Program.IsCruise );
			Assert.IsFalse( ret.Program.IsNsen );
			Assert.AreEqual( ProgramExtendedType.None, ret.Program.ExtendedType );

			Assert.IsFalse( ret.Program.IsInfinity );
			Assert.IsFalse( ret.Program.IsReserved );
			Assert.IsTrue( ret.Program.IsArchivePlayServer );
			Assert.IsTrue( ret.Program.IsTimeshiftEnabled );

			Assert.IsFalse( ret.Program.IsProductEnabled );
			Assert.IsFalse( ret.Program.IsTrialEnabled );
			Assert.IsFalse( ret.Program.IsBannerForced );

			Assert.IsTrue( ret.Program.IsNoticeBalloonEnabled );
			Assert.IsTrue( ret.Program.IsErrorReportEnabled );

			Assert.AreEqual( string.Empty, ret.Program.NsenType );
			Assert.AreEqual( string.Empty, ret.Program.NsenCommand );

			Assert.IsTrue( ret.Program.Twitter.IsEnabled );
			Assert.AreEqual( "#kokkai #衆議院", ret.Program.Twitter.Hashtag );
			Assert.AreEqual( 10000u, ret.Program.Twitter.VipModeCount );

			Assert.AreEqual( "アリーナ 最前列", ret.Room.Name );
			Assert.AreEqual( 147u, ret.Room.SeatID );
			Assert.AreEqual( string.Empty, ret.Room.SeatToken );

			Assert.IsFalse( ret.Stream.IsFlashMediaServer );
			Assert.AreEqual( 0u, ret.Stream.RtmptPort );
			Assert.IsNull( ret.Stream.RtmpUrl );
			Assert.AreEqual( "20929324:lv169798840:0:1392794080:60edb778c359af03", ret.Stream.Ticket );

			Assert.IsNotNull( ret.Stream.Tickets );
			Assert.AreEqual( 10, ret.Stream.Tickets.Count );
			Assert.AreEqual( "uid=20929324&h=3b9fe2bb8fd6fdb8c8e6eda0358f4380", ret.Stream.Tickets["s_lv169798840"] );
			Assert.AreEqual( "uid=20929324&h=5933268c1c7fb1ef7df58ed8784c1bf3", ret.Stream.Tickets["s_lv169798840_sub1"] );
			Assert.AreEqual( "uid=20929324&h=46fb37a19274166b64d224d7b50d1d28", ret.Stream.Tickets["s_lv169798840_sub2"] );
			Assert.AreEqual( "uid=20929324&h=504e1df898ccf4ad2bd610e91584373a", ret.Stream.Tickets["s_lv169798840_sub3"] );
			Assert.AreEqual( "uid=20929324&h=fbad73282574dfb3074e8c152bab22d2", ret.Stream.Tickets["s_lv169798840_sub4"] );
			Assert.AreEqual( "uid=20929324&h=6263b485edf9b1a41ffb6f73bc2bbdaf", ret.Stream.Tickets["s_lv169798840_sub5"] );
			Assert.AreEqual( "uid=20929324&h=b4ac1e6781e625f81c0fb7a4211aa80e", ret.Stream.Tickets["s_lv169798840_sub6"] );
			Assert.AreEqual( "uid=20929324&h=e6920037631c1e7d45ad415b1e76437b", ret.Stream.Tickets["s_lv169798840_sub7"] );
			Assert.AreEqual( "uid=20929324&h=e3a812e20981337c2af79be2b3de2044", ret.Stream.Tickets["s_lv169798840_sub8"] );
			Assert.AreEqual( "uid=20929324&h=7ebea07abe44d4afcc5b8e2278285519", ret.Stream.Tickets["s_lv169798840_sub9"] );

			Assert.AreEqual( 1, ret.Stream.Contents.Count );
			Assert.AreEqual( "main", ret.Stream.Contents[0].ID );
			Assert.IsFalse( ret.Stream.Contents[0].IsAudioDisabled );
			Assert.IsFalse( ret.Stream.Contents[0].IsVideoDisabled );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 18, 19, 19, 57, TimeSpan.FromHours( 9 ) ), ret.Stream.Contents[0].StartedAt );
			Assert.AreEqual( string.Empty, ret.Stream.Contents[0].Title );
			Assert.AreEqual( "limelight:rtmp://smilevideo.fc.llnwd.net:1935/smilevideo,s_lv169798840", ret.Stream.Contents[0].Value );

			Assert.AreEqual( VideoPosition.Default, ret.Stream.Position );
			Assert.AreEqual( VideoAspect.Auto, ret.Stream.Aspect );
			Assert.AreEqual( string.Empty, ret.Stream.BroadcastToken );
			Assert.IsFalse( ret.Stream.IsQualityOfServiceAnalyticsEnabled );

			Assert.IsFalse( ret.Comment.IsLocked );
			Assert.AreEqual( 1.0f, ret.Comment.Scale );
			Assert.AreEqual( string.Empty, ret.Comment.Perm );
			Assert.AreEqual( CommentPosition.Default, ret.Comment.Position );
			Assert.AreEqual( CommentFilteringLevel.None, ret.Comment.FilteringLevel );
			Assert.AreEqual( CommentSexMode.Disable, ret.Comment.SexMode );

			Assert.IsNull( ret.Comment.Commands );

			Assert.IsFalse( ret.Comment.IsRestrict );
			Assert.AreEqual( CommentLimitMode.Allow, ret.Comment.LimitMode );

			Assert.AreEqual( "omsg101.live.nicovideo.jp", ret.Comment.Server.Host.ToString() );
			Assert.AreEqual( 2805u, ret.Comment.Server.Port );
			Assert.AreEqual( 1, ret.Comment.Server.ThreadIDs.Count );
			Assert.AreEqual( 1332290557u, ret.Comment.Server.ThreadIDs[0] );

			Assert.IsFalse( ret.Telop.IsEnabled );
			Assert.AreEqual( string.Empty, ret.Telop.Mail );
			Assert.AreEqual( string.Empty, ret.Telop.Value );

			Assert.IsFalse( ret.NetDuetto.IsEnabled );
			Assert.AreEqual( "039b7e91347da7fc04fa708d8cf596922fa9fdcf", ret.NetDuetto.Token );

			Assert.AreEqual( string.Empty, ret.Marquee.Category );
			Assert.AreEqual( "7b86bc0b", ret.Marquee.GameKey );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 19, 16, 14, 40, TimeSpan.FromHours( 9 ) ), ret.Marquee.GameTime );
			Assert.IsTrue( ret.Marquee.IsNotInterruptionForced );

			Assert.AreEqual( 20929324u, ret.User.ID );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.User.Name );
			Assert.IsTrue( ret.User.IsPremium );
			Assert.AreEqual( 21u, ret.User.Age );
			Assert.IsTrue( ret.User.IsMale );
			Assert.IsFalse( ret.User.IsFemale );
			Assert.AreEqual( Sex.Male, ret.User.Sex );
			Assert.AreEqual( "jp", ret.User.Domain );
			Assert.AreEqual( Prefecture.Osaka, ret.User.Prefecture );
			Assert.AreEqual( "ja-jp", ret.User.Language );
			Assert.AreEqual( string.Empty, ret.User.HKey );

			Assert.IsFalse( ret.User.IsOwner );
			Assert.IsFalse( ret.User.IsJoin );
			Assert.IsFalse( ret.User.IsReserved );
			Assert.IsFalse( ret.User.IsPrefecturePreferential );
			Assert.IsFalse( ret.User.IsPurchased );
			Assert.IsFalse( ret.User.IsSerialUsing );

			Assert.IsTrue( ret.User.Twitter.IsEnabled );
			Assert.AreEqual( "mntone", ret.User.Twitter.ScreenName );
			Assert.AreEqual( 366u, ret.User.Twitter.FollowersCount );
			Assert.IsFalse( ret.User.Twitter.IsVip );
			Assert.AreEqual( "http://a0.twimg.com/profile_images/2420265266/rrtyjcvhu7z5idxo8y49_normal.png", ret.User.Twitter.ProfileImageUrl.ToString() );
			Assert.IsFalse( ret.User.Twitter.IsAuthenticationRequired );
			Assert.AreEqual( "16439bec560b987c1c51950c05c7210f359920f7", ret.User.Twitter.Token );
		}
	}
}