using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Searches.Suggestion;

namespace Mntone.Nico2.Test.Searches
{
	[TestClass]
	public sealed class SuggestionUnitTest
	{
		[TestMethod]
		public void Suggestion_0通常データ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/default.json" ) );
			Assert.AreEqual( 10, ret.Count );
			Assert.AreEqual( "kaito", ret[0] );
			Assert.AreEqual( "k-pop", ret[1] );
			Assert.AreEqual( "k.k.", ret[2] );
			Assert.AreEqual( "kotoko", ret[3] );
			Assert.AreEqual( "konami", ret[4] );
			Assert.AreEqual( "kanon", ret[5] );
			Assert.AreEqual( "knight", ret[6] );
			Assert.AreEqual( "kaitoオリジナル曲", ret[7] );
			Assert.AreEqual( "kof2002", ret[8] );
			Assert.AreEqual( "kenji", ret[9] );
		}

		[TestMethod]
		public void Suggestion_1日本語データ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/japanese.json" ) );
			Assert.AreEqual( 10, ret.Count );
			Assert.AreEqual( 10, ret.Count );
			Assert.AreEqual( "ゆっくり", ret[0] );
			Assert.AreEqual( "ゆっくり実況", ret[1] );
			Assert.AreEqual( "ゆっくり実況プレイ", ret[2] );
			Assert.AreEqual( "ゆっくりしていってね", ret[3] );
			Assert.AreEqual( "ゆっくりしていってね!", ret[4] );
			Assert.AreEqual( "ゆっくり実況プレイpart1リンク", ret[5] );
			Assert.AreEqual( "ゆっくりロボット物実況", ret[6] );
			Assert.AreEqual( "ゆっくりボイス", ret[7] );
			Assert.AreEqual( "ゆっくりしていってね！！！", ret[8] );
			Assert.AreEqual( "ゆっくりしていってね！！", ret[9] );
		}

		[TestMethod]
		public void Suggestion_2ゼロデータ()
		{
			var ret = SuggestionClient.ParseSuggestionData( TestHelper.Load( @"Searches/Suggestion/zero.json" ) );
			Assert.AreEqual( 0, ret.Count );
		}
	}
}