using Mntone.Nico2.Dictionaries.WordExist;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class WordExistUnitTest
	{
		[TestMethod]
		public void WordExist_1通常データ()
		{
			var ret = WordExistClient.ParseWordExistData( TestHelper.Load( @"Dictionaries/WordExist/default.json" ) );
			Assert.IsTrue( ret );
		}

		[TestMethod]
		public void WordExist_2エラーデータ()
		{
			var ret = WordExistClient.ParseWordExistData( TestHelper.Load( @"Dictionaries/WordExist/error.json" ) );
			Assert.IsFalse( ret );
		}
	}
}