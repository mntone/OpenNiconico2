using Mntone.Nico2.Live.Heartbeat;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class HeartbeatUnitTest
	{
		[TestMethod]
		public void Heartbeat_0不正なID()
		{
			Assert2.ThrowsException<ArgumentException>( () =>
			{
				HeartbeatClient.HeartbeatDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "llv1131" ).GetAwaiter().GetResult();
			} );
		}

		[TestMethod]
		public void Heartbeat_1通常データ()
		{
			var ret = HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/default.xml" ) );
			Assert.AreEqual( new DateTimeOffset( 2014, 2, 5, 8, 9, 35, TimeSpan.FromHours( 9 ) ), ret.LoadedAt );
			Assert.AreEqual( 15u, ret.WatchCount );
			Assert.AreEqual( 13u, ret.CommentCount );
			Assert.IsFalse( ret.IsRestrict );
			Assert.AreEqual( "20929324:lv168235211:0:1391555375:1af6f72ef86eb766", ret.Ticket );
			Assert.AreEqual( TimeSpan.FromSeconds( 90 ), ret.WaitDuration );
		}

		[TestMethod]
		public void Heartbeat_2エラーデータ_スロットが存在しない()
		{
			try
			{
				HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/not_exist_slot.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: slot doesn't exist (NOTEXIST_SLOT)", ex.Message );
			}
		}

		[TestMethod]
		public void Heartbeat_3エラーデータ_スロットが見つからない()
		{
			try
			{
				HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/not_found_slot.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: lost slot (NOTFOUND_SLOT)", ex.Message );
			}
		}

		[TestMethod]
		public void Heartbeat_4エラーデータ_ストリームが見つからない()
		{
			try
			{
				HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/not_found_stream.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: stream not found (NOTFOUND_STREAM)", ex.Message );
			}
		}

		[TestMethod]
		public void Heartbeat_5エラーデータ_ログオンしていない()
		{
			try
			{
				HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/not_login.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: not login (NOTLOGIN)", ex.Message );
			}
		}

		[TestMethod]
		public void Heartbeat_6エラーデータ_わからない()
		{
			try
			{
				HeartbeatClient.ParseHeartbeatData( TestHelper.Load( @"Live/Heartbeat/unknown.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: unknown error, retry later (UNKNOWN)", ex.Message );
			}
		}
	}
}