#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test
{
	[TestClass]
	public sealed class NiconicoAuthenticationTokenUnitTest
	{
		[TestMethod]
		public void AuthenticationToken_0Ctor()
		{
			var ret = new NiconicoAuthenticationToken();
			Assert.AreEqual( null, ret.MailOrTelephone );
			Assert.AreEqual( null, ret.Password );
		}

		[TestMethod]
		public void AuthenticationToken_1CtorWithParameters()
		{
			var mail = "test@test.jp";
			var pass = "test@test";
			var ret = new NiconicoAuthenticationToken( mail, pass );
			Assert.AreEqual( mail, ret.MailOrTelephone );
			Assert.AreEqual( pass, ret.Password );
		}

		[TestMethod]
		public void AuthenticationToken_2SetParameters()
		{
			var mail = "test@test.jp";
			var pass = "test@test";
			var ret = new NiconicoAuthenticationToken();
			ret.MailOrTelephone = mail;
			ret.Password = pass;
			Assert.AreEqual( mail, ret.MailOrTelephone );
			Assert.AreEqual( pass, ret.Password );
		}
	}
}