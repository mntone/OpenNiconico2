using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Dictionaries.Exist;

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class ExistUnitTest
	{
		[TestMethod]
		public void Exist_1通常データ()
		{
			var ret = ExistClient.ParseExistData( TestHelper.Load( @"Dictionaries/Exist/default.jsonp" ) );
			Assert.IsTrue( ret );
		}

		[TestMethod]
		public void Exist_2エラーデータ()
		{
			var ret = ExistClient.ParseExistData( TestHelper.Load( @"Dictionaries/Exist/error.jsonp" ) );
			Assert.IsFalse( ret );
		}
	}
}