using Mntone.Nico2.Live.TagRevision;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class TagRevisionUnitTest
	{
		[TestMethod]
		public void TagRevisionTest()
		{
			var data = "{'rev':0}";
			var ret = TagRevisionClient.ParseTagRevisionData( data );
			Assert.AreEqual( ( ushort )0u, ret );
		}
	}
}