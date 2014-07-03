using Mntone.Nico2.Dictionaries.Summary;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class SummaryUnitTest
	{
		[TestMethod]
		public void Summary_0通常データ()
		{
			var ret = SummaryClient.ParseSummaryData( TestHelper.Load( @"Dictionaries/Summary/default.json" ) );
			Assert.AreEqual( "sm9", ret.Title );
			Assert.AreEqual( "sm9", ret.ViewTitle );
			Assert.AreEqual( "概要\n1000万再生達成\n 400万コメント達成\n β時代の英雄\n ニコニコ動画に現存するSMILEVIDEOの最古の動画で、ニコニコ動画を代表する動画。公式動画第一号である。レッツゴー！陰陽師、陰陽", ret.Summary );
		}

		[TestMethod]
		public void Summary_1エラーデータ()
		{
			var ret = SummaryClient.ParseSummaryData( TestHelper.Load( @"Dictionaries/Summary/null.json" ) );
			Assert.IsNull( ret );
		}
	}
}