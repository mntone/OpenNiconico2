using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Videos.Flv;
using System;

namespace Mntone.Nico2.Test.Videos
{
	[TestClass]
	public sealed class FlvUnitTest
	{
		[TestMethod]
		public void Flv_0不正なID()
		{
			Assert.ThrowsException<ArgumentException>( () =>
			{
				FlvClient.GetFlvDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "ssm9" ).GetResults();
			} );
		}

		[TestMethod]
		public void Flv_1通常データ()
		{
			var ret = FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/default.txt" ) );
			Assert.AreEqual( 1173108780u, ret.ThreadId );
			Assert.AreEqual( TimeSpan.FromSeconds( 319 ), ret.Length );
			Assert.AreEqual( "http://smile-pcm42.nicovideo.jp/smile?v=9.0468", ret.VideoUrl.ToString() );
			Assert.AreEqual( "http://www.smilevideo.jp/view/9/20929324", ret.ReportUrl.ToString() );
			Assert.AreEqual( "http://msg.nicovideo.jp/10/api/", ret.CommentServerUrl.ToString() );
			Assert.AreEqual( "http://sub.msg.nicovideo.jp/10/api/", ret.SubCommentServerUrl.ToString() );
			Assert.IsFalse( ret.IsDeleted );

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( new DateTimeOffset( 2014, 4, 17, 12, 13, 6, 129, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 106u, ret.NgRv );

			Assert.AreEqual( "http://hiroba.nicovideo.jp:2549/", ret.AppsUrl.ToString() );
			Assert.AreEqual( 250u, ret.AppsT );
			Assert.AreEqual( "1397704446.HZrBfeSKH3gNQvworR_e4Ku6ZG0", ret.AppsTicket );
		}

		[TestMethod]
		public void Flv_2削除データ()
		{
			var ret = FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/delete.txt" ) );
			Assert.AreEqual( 1176659997u, ret.ThreadId );
			Assert.AreEqual( TimeSpan.FromSeconds( 0 ), ret.Length );
			Assert.AreEqual( "http://smile-cln21.nicovideo.jp/smile?m=19668547.66392", ret.VideoUrl.ToString() );
			Assert.AreEqual( "http://www.smilevideo.jp/view/8/20929324", ret.ReportUrl.ToString() );
			Assert.AreEqual( "http://msg.nicovideo.jp/7/api/", ret.CommentServerUrl.ToString() );
			Assert.AreEqual( "http://sub.msg.nicovideo.jp/7/api/", ret.SubCommentServerUrl.ToString() );
			Assert.IsTrue( ret.IsDeleted );

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( new DateTimeOffset( 2014, 1, 29, 8, 14, 54, 411, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 101u, ret.NgRv );

			Assert.AreEqual( "http://hiroba.nicovideo.jp:2536/", ret.AppsUrl.ToString() );
			Assert.AreEqual( 120u, ret.AppsT );
			Assert.AreEqual( "1390950954.cKivrN_GX-eojwaYCp_TinG9nVQ", ret.AppsTicket );
		}

		[TestMethod]
		public void Flv_3エラーデータ()
		{
			Assert.ThrowsException<Exception>( () =>
				{
					FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/error.txt" ) );
				} );
		}
	}
}
