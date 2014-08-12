using Mntone.Nico2.Live.Description;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class DescriptionUnitTest
	{
		[TestMethod]
		public void Description_1now非ログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/now_notlogin.htm" ) );
			Assert.AreEqual( @"R藤本の水曜はじけてまざれ 過去配信 
名作集", ret.Title );
			Assert.IsTrue( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 11, 23, 50, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://live.nicovideo.jp/thumb/226105.jpg", ret.ThumbnailUrl.ToString() );
		}

		[TestMethod]
		public void Description_2nowログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/now_premium.htm" ) );
			Assert.AreEqual( @"R藤本の水曜はじけてまざれ 過去配信 
名作集", ret.Title );
			Assert.IsTrue( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 11, 23, 50, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://live.nicovideo.jp/thumb/226105.jpg", ret.ThumbnailUrl.ToString() );
		}

		[TestMethod]
		public void Description_3future非ログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/future_notlogin.htm" ) );
			Assert.AreEqual( @"火曜ニコラジ★ゲストは『GOROman』が生登場！", ret.Title );
			Assert.IsTrue( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 12, 21, 50, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://live.nicovideo.jp/thumb/227027.jpg", ret.ThumbnailUrl.ToString() );
		}

		[TestMethod]
		public void Description_4future非ログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/future_premium.htm" ) );
			Assert.AreEqual( @"火曜ニコラジ★ゲストは『GOROman』が生登場！", ret.Title );
			Assert.IsTrue( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 12, 21, 50, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://live.nicovideo.jp/thumb/227027.jpg", ret.ThumbnailUrl.ToString() );
		}

		[TestMethod]
		public void Description_5timeshift非ログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/timeshift_notlogin.htm" ) );
			Assert.AreEqual( @"PS4 FIFA14 
プロクラブ参加者募集中!!　パブリックです！", ret.Title );
			Assert.IsFalse( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 12, 19, 57, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://icon.nimg.jp/community/180/co1800688.jpg?1403054150", ret.ThumbnailUrl.ToString() );
		}

		[TestMethod]
		public void Description_6timeshiftログイン()
		{
			var ret = DescriptionClient.ParseDescriptionData( TestHelper.Load( @"Live/Description/timeshift_premium.htm" ) );
			Assert.AreEqual( @"PS4 FIFA14 
プロクラブ参加者募集中!!　パブリックです！", ret.Title );
			Assert.IsFalse( ret.IsHighQuality );
			Assert.AreEqual( new DateTimeOffset( 2014, 8, 12, 19, 57, 0, TimeSpan.FromHours( 9 ) ), ret.OpenedAt );
			Assert.AreEqual( "http://icon.nimg.jp/community/180/co1800688.jpg?1403054150", ret.ThumbnailUrl.ToString() );
		}
	}
}