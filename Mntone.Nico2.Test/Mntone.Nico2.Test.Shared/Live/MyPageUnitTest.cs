using Mntone.Nico2.Live;
using Mntone.Nico2.Live.MyPage;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class MyPageUnitTest
	{
		[TestMethod]
		public void MyPage_JAJP()
		{
			var ret = MyPageClient.ParseMyPageData( TestHelper.Load( @"Live/MyPage/ja-jp.html" ) );

			Assert.AreEqual( "lv88690983", ret.TimeshiftPrograms[0].ID );
			Assert.AreEqual( "超会議特番～実証！２時間でニコ動プロトタイプを運営長は作ったのか？エンジニア編～", ret.TimeshiftPrograms[0].Title );

			Assert.AreEqual( "lv197347514", ret.TimeshiftPrograms[1].ID );
			Assert.AreEqual( "宮本 茂(任天堂)×川上量生(ドワンゴ)スペシャル対談生中継 /第27回東京国際映画祭 3日目", ret.TimeshiftPrograms[1].Title );

			Assert.AreEqual( "lv156937562", ret.TimeshiftPrograms[2].ID );
			Assert.AreEqual( "日本マイクロソフト Surface 新製品発表会 生中継", ret.TimeshiftPrograms[2].Title );

			Assert.AreEqual( "lv177583897", ret.TimeshiftPrograms[3].ID );
			Assert.AreEqual( "月曜ニコラジ★アニソンシンガー「佐咲紗花」さん初生登場！", ret.TimeshiftPrograms[3].Title );

			Assert.AreEqual( "lv184087160", ret.TimeshiftPrograms[4].ID );
			Assert.AreEqual( "木曜ニコラジ★北海道から生中継！ミスター「鈴井貴之」登場！", ret.TimeshiftPrograms[4].Title );

			Assert.AreEqual( "lv196673210", ret.TimeshiftPrograms[5].ID );
			Assert.AreEqual( "国産初のジェット旅客機「MRJ」ロールアウト式典 収録放送", ret.TimeshiftPrograms[5].Title );

			Assert.AreEqual( "lv195100599", ret.TimeshiftPrograms[6].ID );
			Assert.AreEqual( "ニコラジ月曜日", ret.TimeshiftPrograms[6].Title );

			Assert.AreEqual( "lv196674947", ret.TimeshiftPrograms[7].ID );
			Assert.AreEqual( "【TV同時放送】ニコニコアニメスペシャル「憑物語」一挙放送", ret.TimeshiftPrograms[7].Title );

			Assert.AreEqual( "lv188158963", ret.TimeshiftPrograms[8].ID );
			Assert.AreEqual( "【WEB最速】ニコニコアニメスペシャル「花物語」全5話一挙放送", ret.TimeshiftPrograms[8].Title );


			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 0, 15, 0, TimeSpan.Zero ), ret.OnAirPrograms[0].OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 0, 15, 0, TimeSpan.Zero ), ret.OnAirPrograms[0].StartedAt );
			Assert.AreEqual( DetailCategoryType.Adult, ret.OnAirPrograms[0].Category );
			Assert.IsFalse( ret.OnAirPrograms[0].IsFaceOut );
			Assert.IsFalse( ret.OnAirPrograms[0].IsEnconter );
			Assert.IsFalse( ret.OnAirPrograms[0].IsCruise );
			Assert.IsFalse( ret.OnAirPrograms[0].IsOfficial );
			Assert.IsFalse( ret.OnAirPrograms[0].IsChannel );
			Assert.IsTrue( ret.OnAirPrograms[0].IsCommunity );
			Assert.AreEqual( CommunityType.Community, ret.OnAirPrograms[0].CommunityType );
			Assert.AreEqual( "co2158245", ret.OnAirPrograms[0].CommunityID );
			Assert.AreEqual( "lv199055928", ret.OnAirPrograms[0].ID );
			Assert.AreEqual( "エロゲソング垂れ流し", ret.OnAirPrograms[0].Title );
			Assert.AreEqual( "自分が聞いてるだけの配信", ret.OnAirPrograms[0].CommunityName );


			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 10, 57, 0, TimeSpan.Zero ), ret.ReservedPrograms[0].OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 11, 0, 0, TimeSpan.Zero ), ret.ReservedPrograms[0].StartedAt );
			Assert.IsFalse( ret.ReservedPrograms[0].IsOfficial );
			Assert.IsTrue( ret.ReservedPrograms[0].IsChannel );
			Assert.IsFalse( ret.ReservedPrograms[0].IsCommunity );
			Assert.AreEqual( CommunityType.Channel, ret.ReservedPrograms[0].CommunityType );
			Assert.AreEqual( "ch2573781", ret.ReservedPrograms[0].CommunityID );
			Assert.AreEqual( "lv199055087", ret.ReservedPrograms[0].ID );
			Assert.AreEqual( "高橋名人の16Shot TV Vol.108", ret.ReservedPrograms[0].Title );
			Assert.AreEqual( "高橋名人の16SHOTチャンネル", ret.ReservedPrograms[0].CommunityName );

			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 12, 27, 0, TimeSpan.Zero ), ret.ReservedPrograms[1].OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 11, 5, 12, 30, 0, TimeSpan.Zero ), ret.ReservedPrograms[1].StartedAt );
			Assert.IsFalse( ret.ReservedPrograms[1].IsOfficial );
			Assert.IsTrue( ret.ReservedPrograms[1].IsChannel );
			Assert.IsFalse( ret.ReservedPrograms[1].IsCommunity );
			Assert.AreEqual( CommunityType.Channel, ret.ReservedPrograms[1].CommunityType );
			Assert.AreEqual( "ch203", ret.ReservedPrograms[1].CommunityID );
			Assert.AreEqual( "lv198089616", ret.ReservedPrograms[1].ID );
			Assert.AreEqual( "遠藤正明　FIRST ACOUSTIC ALBUM「Present of the Voice」発売記念生放送", ret.ReservedPrograms[1].Title );
			Assert.AreEqual( "Lantisちゃんねる", ret.ReservedPrograms[1].CommunityName );

			Assert.AreEqual( new DateTimeOffset( 2014, 11, 6, 12, 57, 0, TimeSpan.Zero ), ret.ReservedPrograms[2].OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 11, 6, 13, 0, 0, TimeSpan.Zero ), ret.ReservedPrograms[2].StartedAt );
			Assert.IsFalse( ret.ReservedPrograms[2].IsOfficial );
			Assert.IsTrue( ret.ReservedPrograms[2].IsChannel );
			Assert.IsFalse( ret.ReservedPrograms[2].IsCommunity );
			Assert.AreEqual( CommunityType.Channel, ret.ReservedPrograms[2].CommunityType );
			Assert.AreEqual( "ch2595349", ret.ReservedPrograms[2].CommunityID );
			Assert.AreEqual( "lv196800761", ret.ReservedPrograms[2].ID );
			Assert.AreEqual( "たかはし智秋のLADY LUCKチャンネル生放送", ret.ReservedPrograms[2].Title );
			Assert.AreEqual( "たかはし智秋のLADY LUCKチャンネル", ret.ReservedPrograms[2].CommunityName );

			Assert.AreEqual( new DateTimeOffset( 2014, 11, 8, 10, 57, 0, TimeSpan.Zero ), ret.ReservedPrograms[3].OpenedAt );
			Assert.AreEqual( new DateTimeOffset( 2014, 11, 8, 11, 0, 0, TimeSpan.Zero ), ret.ReservedPrograms[3].StartedAt );
			Assert.IsFalse( ret.ReservedPrograms[3].IsOfficial );
			Assert.IsTrue( ret.ReservedPrograms[3].IsChannel );
			Assert.IsFalse( ret.ReservedPrograms[3].IsCommunity );
			Assert.AreEqual( CommunityType.Channel, ret.ReservedPrograms[3].CommunityType );
			Assert.AreEqual( "ch2595349", ret.ReservedPrograms[3].CommunityID );
			Assert.AreEqual( "lv196801632", ret.ReservedPrograms[3].ID );
			Assert.AreEqual( "LADY LUCK 会議", ret.ReservedPrograms[3].Title );
			Assert.AreEqual( "たかはし智秋のLADY LUCKチャンネル", ret.ReservedPrograms[3].CommunityName );

			bool f = false;
		}
	}
}