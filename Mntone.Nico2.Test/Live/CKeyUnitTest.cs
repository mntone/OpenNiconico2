using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live.CKey;
using System;

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class CKeyUnitTest
	{
		[TestMethod]
		public void CKey_0不正なID1()
		{
			Assert.ThrowsException<ArgumentException>( () =>
			{
				CKeyClient.GetCKeyDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "llv1131", "sm9" ).GetResults();
			} );
		}

		[TestMethod]
		public void CKey_1不正なID2()
		{
			Assert.ThrowsException<ArgumentException>( () =>
			{
				CKeyClient.GetCKeyDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "lv1", "ssm9" ).GetResults();
			} );
		}

		[TestMethod]
		public void CKey_2通常データ()
		{
			var ret = CKeyClient.ParseCKeyData( TestHelper.Load( @"Live/CKey/default.txt" ) );
			Assert.AreEqual( "1397703237.4a7a231c2fff5bbc8c1fa27776acdc9d23f82682", ret );
		}

		[TestMethod]
		public void CKey_3エラーデータ()
		{
			Assert.ThrowsException<Exception>( () =>
				{
					var ret = CKeyClient.ParseCKeyData( TestHelper.Load( @"Live/CKey/error.txt" ) );
				} );
		}
	}
}