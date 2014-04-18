using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Dictionaries.WordExist;

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class WordExistUnitTest
	{
		[TestMethod]
		public void WordExist_1通常データ()
		{
			var ret = WordExistClient.ParseWordExistData( TestHelper.Load( @"Dictionaries/WordExist/default.jsonp" ) );
			Assert.IsTrue( ret );
		}

		[TestMethod]
		public void WordExist_2エラーデータ()
		{
			var ret = WordExistClient.ParseWordExistData( TestHelper.Load( @"Dictionaries/WordExist/error.jsonp" ) );
			Assert.IsFalse( ret );
		}
	}
}