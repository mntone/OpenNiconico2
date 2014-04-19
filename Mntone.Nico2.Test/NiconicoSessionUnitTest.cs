using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;

namespace Mntone.Nico2.Test
{
	[TestClass]
	public sealed class NiconicoSessionUnitTest
	{
		[TestMethod]
		public void Session_0Ctor()
		{
			var ret = new NiconicoSession();
			Assert.AreEqual( null, ret.Key );
			Assert.AreEqual( DateTimeOffset.MinValue, ret.Expires );
		}

		[TestMethod]
		public void Session_1CtorWithParameters()
		{
			var key = "key sample";
			var expires = DateTimeOffset.Now + TimeSpan.FromSeconds( 1 );
			var ret = new NiconicoSession( key, expires );
			Assert.AreEqual( key, ret.Key );
			Assert.AreEqual( expires, ret.Expires );
		}

		[TestMethod]
		public void Session_2SetParameters()
		{
			var key = "key sample";
			var expires = DateTimeOffset.Now + TimeSpan.FromSeconds( 1 );
			var ret = new NiconicoSession();
			ret.Key = key;
			ret.Expires = expires;
			Assert.AreEqual( key, ret.Key );
			Assert.AreEqual( expires, ret.Expires );
		}

		[TestMethod]
		public void Session_3SetExpiresOutOfRange()
		{
			Assert.ThrowsException<Exception>( () =>
				{
					var key = "key sample";
					var expires = DateTimeOffset.MinValue;
					var ret = new NiconicoSession();
					ret.Key = key;
					ret.Expires = expires;
					Assert.AreEqual( key, ret.Key );
					Assert.AreEqual( expires, ret.Expires );
				} );
		}
	}
}