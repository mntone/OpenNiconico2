using Mntone.Nico2.Videos.RemoveHistory;
using Newtonsoft.Json.Linq;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Videos
{
	[TestClass]
	public sealed class RemoveHistoryUnitTest
	{
		[TestMethod]
		public void RemoveHistory_0通常データ()
		{
			var data = TestHelper.Load( @"Videos/RemoveHistory/default.json" );
			var ret = RemoveHistoryClient.ParseRemoveHistoryData( data );
			var ret2 = JObject.Parse( data );

			Assert.AreEqual( ret2["removed"].Value<string>(), ret.RemovedId );
			Assert.AreEqual( ret2["count"].Value<ushort>(), ret.HistoryCount );
		}

		[TestMethod]
		public void RemoveHistory_1ゼロデータ()
		{
			Assert2.ThrowsException<Exception>( () =>
			{
				RemoveHistoryClient.ParseRemoveHistoryData( TestHelper.Load( @"Videos/RemoveHistory/error.json" ) );
			} );
		}
	}
}