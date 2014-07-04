using Mntone.Nico2.Searches.Suggestion;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Searches
{
	[TestClass]
	public sealed class SuggestionUnitTest
	{
		[TestMethod]
		public void Suggestion_0通常データ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/default.json" ) );
			Assert.AreEqual( 10, ret.Candidates.Count );
			Assert.AreEqual( "kaito", ret.Candidates[0] );
			Assert.AreEqual( "k-pop", ret.Candidates[1] );
			Assert.AreEqual( "k.k.", ret.Candidates[2] );
			Assert.AreEqual( "kotoko", ret.Candidates[3] );
			Assert.AreEqual( "konami", ret.Candidates[4] );
			Assert.AreEqual( "kanon", ret.Candidates[5] );
			Assert.AreEqual( "knight", ret.Candidates[6] );
			Assert.AreEqual( "kaitoオリジナル曲", ret.Candidates[7] );
			Assert.AreEqual( "kof2002", ret.Candidates[8] );
			Assert.AreEqual( "kenji", ret.Candidates[9] );
		}

		[TestMethod]
		public void Suggestion_1日本語データ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/japanese.json" ) );
			Assert.AreEqual( 10, ret.Candidates.Count );
			Assert.AreEqual( 10, ret.Candidates.Count );
			Assert.AreEqual( "ゆっくり", ret.Candidates[0] );
			Assert.AreEqual( "ゆっくり実況", ret.Candidates[1] );
			Assert.AreEqual( "ゆっくり実況プレイ", ret.Candidates[2] );
			Assert.AreEqual( "ゆっくりしていってね", ret.Candidates[3] );
			Assert.AreEqual( "ゆっくりしていってね!", ret.Candidates[4] );
			Assert.AreEqual( "ゆっくり実況プレイpart1リンク", ret.Candidates[5] );
			Assert.AreEqual( "ゆっくりロボット物実況", ret.Candidates[6] );
			Assert.AreEqual( "ゆっくりボイス", ret.Candidates[7] );
			Assert.AreEqual( "ゆっくりしていってね！！！", ret.Candidates[8] );
			Assert.AreEqual( "ゆっくりしていってね！！", ret.Candidates[9] );
		}

		[TestMethod]
		public void Suggestion_2ゼロデータ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/zero.json" ) );
			Assert.AreEqual( 0, ret.Candidates.Count );
		}
	}
}