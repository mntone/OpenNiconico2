using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
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
				PlayerStatusClient.GetPlayerStatusDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "llv1131" ).GetResults();
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

			Assert.IsNull( ret.Program.NsenType );
			Assert.IsNull( ret.Program.NsenCommand );

			Assert.IsFalse( ret.Program.Twitter.IsEnabled );
			Assert.AreEqual( "#co1342124", ret.Program.Twitter.Hashtag );
			Assert.AreEqual( 10000u, ret.Program.Twitter.VipModeCount );

			Assert.AreEqual( "co1342124", ret.Room.Name );
			Assert.AreEqual( 8u, ret.Room.SeatID );
			Assert.IsNull( ret.Room.SeatToken );

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
			Assert.IsNull( ret.Stream.Contents[0].Title );
			Assert.AreEqual( "rtmp:rtmp://nlpoca112.live.nicovideo.jp:1935/publicorigin/140205_07_1/,lv168235211?1391555282:30:009412a24371baf8", ret.Stream.Contents[0].Value );
			
			Assert.IsNull( ret.Stream.Commands );

			Assert.AreEqual( VideoPosition.Default, ret.Stream.Position );
			Assert.AreEqual( VideoAspect.Auto, ret.Stream.Aspect );
			Assert.IsNull( ret.Stream.BroadcastToken );
			Assert.IsFalse( ret.Stream.IsQualityOfServiceAnalyticsEnabled );

			Assert.IsFalse( ret.Comment.IsLocked );
			Assert.AreEqual( 1.0f, ret.Comment.Scale );
			Assert.IsNull( ret.Comment.Perm );
			Assert.AreEqual( CommentPosition.Default, ret.Comment.Position );
			Assert.AreEqual( CommentFilteringLevel.None, ret.Comment.FilteringLevel );
			Assert.AreEqual( CommentSexMode.Disable, ret.Comment.SexMode );
			Assert.IsFalse( ret.Comment.IsRestrict );
			Assert.AreEqual( CommentLimitMode.Allow, ret.Comment.LimitMode );

			Assert.AreEqual( "msg102.live.nicovideo.jp", ret.Comment.Server.Host.ToString() );
			Assert.AreEqual( 2806u, ret.Comment.Server.Port );
			Assert.AreEqual( 1, ret.Comment.Server.ThreadIDs.Count );
			Assert.AreEqual( 1329132457u, ret.Comment.Server.ThreadIDs[0] );

			Assert.IsFalse( ret.Telop.IsEnabled );
			Assert.IsNull( ret.Telop.Mail );
			Assert.IsNull( ret.Telop.Value );

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
			Assert.AreEqual( Sex.Male, ret.User.Sex );
			Assert.AreEqual( "jp", ret.User.Domain );
			Assert.AreEqual( Prefecture.Osaka, ret.User.Prefecture );
			Assert.AreEqual( "ja-jp", ret.User.Language );
			Assert.IsNull( ret.User.HKey );

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
	}
}