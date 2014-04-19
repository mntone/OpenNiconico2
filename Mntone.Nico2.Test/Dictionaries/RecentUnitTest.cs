using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Dictionaries;
using Mntone.Nico2.Dictionaries.Recent;

namespace Mntone.Nico2.Test.Dictionaries
{
	[TestClass]
	public sealed class RecentUnitTest
	{
		[TestMethod]
		public void Recent_0通常データ()
		{
			var ret = RecentClient.ParseRecentData( TestHelper.Load( @"Dictionaries/Recent/default.jsonp" ) );

			Assert.AreEqual( 5, ret.Words.Count );

			Assert.AreEqual( Category.User, ret.Words[0].Category );
			Assert.AreEqual( "39921148", ret.Words[0].Title );
			Assert.AreEqual( "rumsyrup", ret.Words[0].ViewTitle );
			Assert.AreEqual( "至らない点もあると思いますがよろしくお願いします。", ret.Words[0].Summary );

			Assert.AreEqual( Category.Word, ret.Words[1].Category );
			Assert.AreEqual( "マリー(カゲロウプロジェクト)", ret.Words[1].Title );
			Assert.AreEqual( "マリー(カゲロウプロジェクト)", ret.Words[1].ViewTitle );
			Assert.AreEqual( "マリーとは、カゲロウプロジェクトの登場人物である。アニメ版『メカクシティーアクターズ』のCVは花澤香菜。\n概要\nエプロンドレスを着た少女。元々は森に住んでいた。\n楽曲『空想フォレスト』などで登場する。", ret.Words[1].Summary );

			Assert.AreEqual( Category.Word, ret.Words[2].Category );
			Assert.AreEqual( "塩音ソル", ret.Words[2].Title );
			Assert.AreEqual( "塩音ソル", ret.Words[2].ViewTitle );
			Assert.AreEqual( "塩音ソルとは、UTAU向けに作成された音声ライブラリおよび、そのイメージキャラクターである。\n概要\n UTAU音声ライブラリ\n 塩音ソル\n \n 基本情報\n 音声提供者\n 塩分\n キャラデザイン\n 塩分", ret.Words[2].Summary );

			Assert.AreEqual( Category.User, ret.Words[3].Category );
			Assert.AreEqual( "23009197", ret.Words[3].Title );
			Assert.AreEqual( "クワ09", ret.Words[3].ViewTitle );
			Assert.AreEqual( "よろしくお願いします。", ret.Words[3].Summary );

			Assert.AreEqual( Category.Word, ret.Words[4].Category );
			Assert.AreEqual( "ゴーストサプリ", ret.Words[4].Title );
			Assert.AreEqual( "ゴーストサプリ", ret.Words[4].ViewTitle );
			Assert.AreEqual( null, ret.Words[4].Summary );
		}
	}
}