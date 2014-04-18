using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live.CKey;
using System;

namespace Mntone.Nico2.Test.Live.CKey
{
	[TestClass]
	public sealed class CKeyUnitTest
	{
		[TestMethod]
		public void CKey_1通常データ()
		{
			var ret = CKeyClient.ParseCKeyData( TestHelper.Load( @"Live/CKey/default.txt" ) );
			Assert.AreEqual( "1397703237.4a7a231c2fff5bbc8c1fa27776acdc9d23f82682", ret );
		}

		[TestMethod]
		public void CKey_2エラーデータ()
		{
			Assert.ThrowsException<Exception>( () =>
				{
					var ret = CKeyClient.ParseCKeyData( TestHelper.Load( @"Live/CKey/error.txt" ) );
				} );
		}
	}
}