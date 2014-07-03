using Mntone.Nico2.Dictionaries.Exist;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class ExistUnitTest
	{
		[TestMethod]
		public void Exist_0通常データ()
		{
			var ret = ExistClient.ParseExistData( TestHelper.Load( @"Dictionaries/Exist/default.json" ) );
			Assert.IsTrue( ret );
		}

		[TestMethod]
		public void Exist_1エラーデータ()
		{
			var ret = ExistClient.ParseExistData( TestHelper.Load( @"Dictionaries/Exist/error.json" ) );
			Assert.IsFalse( ret );
		}
	}
}