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
			Assert.AreEqual( 1173108780u, ret.ThreadID );
			Assert.AreEqual( TimeSpan.FromSeconds( 319 ), ret.Length );
			Assert.AreEqual( "http://smile-pcm42.nicovideo.jp/smile?v=9.0468", ret.VideoUrl.ToString() );
			Assert.AreEqual( "http://www.smilevideo.jp/view/9/20929324", ret.ReportUrl.ToString() );
			Assert.AreEqual( "http://msg.nicovideo.jp/10/api/", ret.CommentServerUrl.ToString() );
			Assert.AreEqual( "http://sub.msg.nicovideo.jp/10/api/", ret.SubCommentServerUrl.ToString() );
			Assert.AreEqual( PrivateReasonType.None, ret.PrivateReason );
			Assert.IsFalse( ret.IsDeleted );

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( new DateTimeOffset( 2014, 4, 17, 12, 13, 6, 129, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.IsFalse( ret.IsKeyRequired );
			Assert.AreEqual( 0u, ret.OptionalThreadID );
			Assert.IsNull( ret.ChannelFilter );
			Assert.IsNull( ret.FlashMediaServerToken );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 106u, ret.NgRv );

			Assert.AreEqual( "hiroba.nicovideo.jp", ret.AppsHost.ToString() );
			Assert.AreEqual( 2549u, ret.AppsPort );
			Assert.AreEqual( 250u, ret.AppsThreadID );
			Assert.AreEqual( "1397704446.HZrBfeSKH3gNQvworR_e4Ku6ZG0", ret.AppsTicket );
		}

		[TestMethod]
		public void Flv_2チャンネルデータ()
		{
			var ret = FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/channel.txt" ) );
			Assert.AreEqual( 1398234206u, ret.ThreadID );
			Assert.AreEqual( TimeSpan.FromSeconds( 1419 ), ret.Length );
			Assert.AreEqual( "http://smile-pso00.nicovideo.jp/smile?m=23392110.73632", ret.VideoUrl.ToString() );
			Assert.AreEqual( "http://www.smilevideo.jp/view/23392110/20929324", ret.ReportUrl.ToString() );
			Assert.AreEqual( "http://msg.nicovideo.jp/13/api/", ret.CommentServerUrl.ToString() );
			Assert.AreEqual( "http://sub.msg.nicovideo.jp/13/api/", ret.SubCommentServerUrl.ToString() );
			Assert.AreEqual( PrivateReasonType.None, ret.PrivateReason );
			Assert.IsFalse( ret.IsDeleted );

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( new DateTimeOffset( 2014, 4, 26, 19, 12, 46, 771, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.IsTrue( ret.IsKeyRequired );
			Assert.AreEqual( 1398234205u, ret.OptionalThreadID );
			Assert.AreEqual( "%2F%2Afeir.tk=&%2F%2A%5BwW%EF%BD%97%EF%BC%B7%5D%5B.%2C%E3%80%81%E3%80%82%2F%E3%83%BB%5C%EF%BF%A5%3B%EF%BC%9B%3A%EF%BC%9A%5D=&%2F%2A%5BfF%EF%BD%86%EF%BC%A6%5D%5BeE%EF%BD%85%EF%BC%A5%5D%5BiI%EF%BD%89%EF%BC%A9%5D=&%2F%2A%5BhH%EF%BD%88%EF%BC%A8%5D%5BtT%EF%BD%94%EF%BC%B4%5D%5BtT%EF%BD%94%EF%BC%B4%5D%5BpP%EF%BD%90%EF%BC%B0%5D%5B%3A%EF%BC%9A%5D=&%2F%2A%5BfF%EF%BD%86%EF%BC%A6%5D%5B+%E3%80%80%5D%2B%5BeE%EF%BD%85%EF%BC%A5%5D=&%2F%2A%5BeE%EF%BD%85%EF%BC%A5%5D%5B+%E3%80%80%5D%2B%5BiI%EF%BD%89%EF%BC%A9%5D=&%2F%2A%5BwW%EF%BD%97%EF%BC%B7%5D%5B+%E3%80%80%5D%2B%5B.%2C%E3%80%81%E3%80%82%2F%E3%83%BB%5C%EF%BF%A5%3B%EF%BC%9B%3A%EF%BC%9A%5D=&%2F%2A%5B.%2C%E3%80%81%E3%80%82%2F%E3%83%BB%5C%EF%BF%A5%3B%EF%BC%9B%3A%EF%BC%9A%5D%5BtT%EF%BD%94%EF%BC%B4cC%EF%BD%83%EF%BC%A3jJ%EF%BD%8A%EF%BC%AAuU%EF%BD%95%EF%BC%B5iI%EF%BD%89%EF%BC%A9uU%EF%BD%95%EF%BC%B5nN%EF%BD%8E%EF%BC%AEbB%EF%BD%82%EF%BC%A2%5D=&%2F%2A%5B.%2C%E3%80%81%E3%80%82%2F%E3%83%BB%5C%EF%BF%A5%3B%EF%BC%9B%3A%EF%BC%9A%5D%5B+%E3%80%80%5D%2B%5BtT%EF%BD%94%EF%BC%B4cC%EF%BD%83%EF%BC%A3jJ%EF%BD%8A%EF%BC%AAuU%EF%BD%95%EF%BC%B5iI%EF%BD%89%EF%BC%A9uU%EF%BD%95%EF%BC%B5nN%EF%BD%8E%EF%BC%AEbB%EF%BD%82%EF%BC%A2%5D=&%2F%2A.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BtT%EF%BD%94%EF%BC%B4%CE%A4%CF%84%5D.%2A%5BkK%EF%BD%8B%EF%BC%AB%CE%9A%CE%BA%5D.%2A=&%2F%2A.%2A%5BaA%EF%BD%81%EF%BC%A1eE%EF%BD%85%EF%BC%A5%5D.%2A%5BtT%EF%BD%94%EF%BC%B4%5D.%2A%5BkK%EF%BD%8B%EF%BC%AB%5D.%2A=&%2F%2A%5BeE%EF%BD%85%EF%BC%A5%5D%5BrR%EF%BD%92%EF%BC%B2%5D%5BqQ%EF%BD%91%EF%BC%B1%5D=&%2F%2A%5BeE%EF%BD%85%EF%BC%A5%5D%5B+%E3%80%80%5D%2B%5BrR%EF%BD%92%EF%BC%B2%5D=&%2F%2A%5BrR%EF%BD%92%EF%BC%B2%5D%5B+%E3%80%80%5D%2B%5BqQ%EF%BD%91%EF%BC%B1%5D=&%2F%2A.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BwW%EF%BD%97%EF%BC%B7%5D.%2A%5BtT%EF%BD%94%EF%BC%B4cC%EF%BD%83%EF%BC%A3jJ%EF%BD%8A%EF%BC%AAuU%EF%BD%95%EF%BC%B5iI%EF%BD%89%EF%BC%A9uU%EF%BD%95%EF%BC%B5nN%EF%BD%8E%EF%BC%AEbB%EF%BD%82%EF%BC%A2%5D.%2A=&%2F%2A.%2A%5BbB%EF%BD%82%EF%BC%A2%5D.%2A%5BiI%EF%BD%89%EF%BC%A9%5D.%2A%5BtT%EF%BD%94%EF%BC%B4%CE%A4%CF%84%5D.%2A%5BlL%EF%BD%8C%EF%BC%AC%5D.%2A%5ByY%EF%BD%99%EF%BC%B9%5D.%2A=&%2F%2A.%2A%5BpP%EF%BD%90%EF%BC%B0%5D.%2A%5BoO%EF%BD%8F%EF%BC%AF%5D.%2A%5BoO%EF%BD%8F%EF%BC%AF%5D.%2A%5BtT%EF%BD%94%EF%BC%B4%CE%A4%CF%84%5D.%2A=&%2F%2A.%2A%5BpP%EF%BD%90%EF%BC%B0%5D.%2A%5BoO%EF%BD%8F%EF%BC%AF%5D.%2A%5BoO%EF%BD%8F%EF%BC%AF%5D.%2A%5BsS%EF%BD%93%EF%BC%B3%5D.%2A=", ret.ChannelFilter );
			Assert.IsNull( ret.FlashMediaServerToken );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 106u, ret.NgRv );

			Assert.AreEqual( "hiroba.nicovideo.jp", ret.AppsHost.ToString() );
			Assert.AreEqual( 2573u, ret.AppsPort );
			Assert.AreEqual( 490u, ret.AppsThreadID );
			Assert.AreEqual( "1398507226.9euEDhk4r0NhQ9InHjfFUI0yUsI", ret.AppsTicket );
		}

		[TestMethod]
		public void Flv_3非公開データ()
		{
			var ret = FlvClient.ParseFlvData( TestHelper.Load( @"Videos/Flv/delete.txt" ) );
			Assert.AreEqual( 1176659997u, ret.ThreadID );
			Assert.AreEqual( TimeSpan.FromSeconds( 0 ), ret.Length );
			Assert.AreEqual( "http://smile-cln21.nicovideo.jp/smile?m=19668547.66392", ret.VideoUrl.ToString() );
			Assert.AreEqual( "http://www.smilevideo.jp/view/8/20929324", ret.ReportUrl.ToString() );
			Assert.AreEqual( "http://msg.nicovideo.jp/7/api/", ret.CommentServerUrl.ToString() );
			Assert.AreEqual( "http://sub.msg.nicovideo.jp/7/api/", ret.SubCommentServerUrl.ToString() );
			Assert.AreEqual( PrivateReasonType.Private, ret.PrivateReason );
			Assert.IsFalse( ret.IsDeleted );

			Assert.AreEqual( 20929324u, ret.UserId );
			Assert.IsTrue( ret.IsPremium );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.UserName );

			Assert.AreEqual( new DateTimeOffset( 2014, 1, 29, 8, 14, 54, 411, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );

			Assert.IsFalse( ret.IsKeyRequired );
			Assert.AreEqual( 0u, ret.OptionalThreadID );
			Assert.IsNull( ret.ChannelFilter );
			Assert.IsNull( ret.FlashMediaServerToken );

			Assert.IsTrue( ret.Done );
			Assert.AreEqual( 101u, ret.NgRv );

			Assert.AreEqual( "hiroba.nicovideo.jp", ret.AppsHost.ToString() );
			Assert.AreEqual( 2536u, ret.AppsPort );
			Assert.AreEqual( 120u, ret.AppsThreadID );
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
