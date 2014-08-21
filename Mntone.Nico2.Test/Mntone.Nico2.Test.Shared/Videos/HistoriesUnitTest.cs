using Mntone.Nico2.Videos.Histories;
using Newtonsoft.Json.Linq;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Videos
{
	[TestClass]
	public sealed class HistoriesUnitTest
	{
		[TestMethod]
		public void Histories_0通常データ()
		{
			var data = TestHelper.Load( @"Videos/Histories/default.json" );
			var ret = HistoriesClient.ParseHistoriesData( data );
			var ret2 = JObject.Parse( data );

			Assert.AreEqual( ret2["token"].Value<string>(), ret.Token );

			var ret2Histories = ret2["history"].AsJEnumerable();
			for( var i = 0; i < ret.Histories.Count; ++i )
			{
				var retHistory = ret.Histories[i];
				var ret2History = ret2Histories[i];
				Assert.AreEqual( ret2History["deleted"].Value<bool>(), retHistory.IsDeleted );
				Assert.AreEqual( ret2History["device"].Value<ushort>(), retHistory.Device );
				Assert.AreEqual( ret2History["item_id"].Value<string>(), retHistory.ItemId );
				Assert.AreEqual( ret2History["length"].Value<string>().ToTimeSpan(), retHistory.Length );
				Assert.AreEqual( ret2History["thumbnail_url"].Value<string>().ToUri(), retHistory.ThumbnailUrl );
				Assert.AreEqual( ret2History["title"].Value<string>(), retHistory.Title );
				Assert.AreEqual( ret2History["video_id"].Value<string>(), retHistory.Id );
				Assert.AreEqual( ret2History["watch_count"].Value<uint>(), retHistory.WatchCount );
				Assert.AreEqual( ret2History["watch_date"].Value<long>().ToDateTimeOffsetFromUnixTime(), retHistory.WatchedAt );
			}
		}

		[TestMethod]
		public void Histories_1ゼロデータ()
		{
			var data = TestHelper.Load( @"Videos/Histories/zero.json" );
			var ret = HistoriesClient.ParseHistoriesData( data );
			Assert.AreEqual( 0, ret.Histories.Count );
		}
	}
}