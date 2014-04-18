using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Videos.Flv;
using System;

namespace Mntone.Nico2.Test.Videos
{
	[TestClass]
	public sealed class FlvUnitTest
	{
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

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( "2014-04-17T03:13:06", ret.LoadedAt.ToUniversalTime().ToString( "s" ) );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 106u, ret.NgRv );

			Assert.AreEqual( "http://hiroba.nicovideo.jp:2549/", ret.AppsUrl.ToString() );
			Assert.AreEqual( 250u, ret.AppsT );
			Assert.AreEqual( "1397704446.HZrBfeSKH3gNQvworR_e4Ku6ZG0", ret.AppsTicket );
		}

		[TestMethod]
		public void Flv_2エラーデータ()
		{
			Assert.ThrowsException<Exception>( () =>
				{
					FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/error.txt" ) );
				} );
		}
	}
}
