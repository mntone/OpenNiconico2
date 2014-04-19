using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Images.Users.Info;

namespace Mntone.Nico2.Test.Images
{
	[TestClass]
	public sealed class UserInfoUnitTest
	{
		[TestMethod]
		public void UserInfo_0通常データ()
		{
			var ret = InfoClient.ParseInfoData( TestHelper.Load( @"Images/Users/Info/default.xml" ) );
			Assert.AreEqual( 2u, ret.UserID );
			Assert.AreEqual( "戀塚", ret.UserName );
		}
	}
}