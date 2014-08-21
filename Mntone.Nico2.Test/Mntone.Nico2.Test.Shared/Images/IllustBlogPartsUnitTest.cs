using Mntone.Nico2.Images.Illusts.BlogParts;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Images
{
	[TestClass]
	public sealed class IllustBlogPartsUnitTest
	{
		[TestMethod]
		public void IllustBlogParts_0clipデータ()
		{
			var ret = BlogPartsClient.ParseBlogPartsData( TestHelper.Load( @"Images/Illusts/BlogParts/clip.xml" ) );
			Assert.AreEqual( "http://seiga.nicovideo.jp/", ret.BaseUrl.ToString() );
			Assert.AreEqual( "http://seiga.nicovideo.jp/clip/565392", ret.PageUrl.ToString() );
			Assert.AreEqual( "http://lohas.nicoseiga.jp/", ret.ImageBaseUrl.ToString() );

			Assert.AreEqual( 3, ret.Images.Count );

			Assert.AreEqual( "im2032391", ret.Images[0].Id );
			Assert.AreEqual( "【MMD-OMF2】D.Gray-man　クロウリー", ret.Images[0].Title );
			Assert.AreEqual( "とらみ", ret.Images[0].UserName );

			Assert.AreEqual( "im2032390", ret.Images[1].Id );
			Assert.AreEqual( "【MMD-OMF2】加湿器的なもの", ret.Images[1].Title );
			Assert.AreEqual( "過給機24", ret.Images[1].UserName );

			Assert.AreEqual( "im2032386", ret.Images[2].Id );
			Assert.AreEqual( "【MMD-OMF2】ねんどろ風けいおんモデル(3/3)", ret.Images[2].Title );
			Assert.AreEqual( "くろぱん", ret.Images[2].UserName );
		}

		[TestMethod]
		public void IllustBlogParts_1userデータ()
		{
			var ret = BlogPartsClient.ParseBlogPartsData( TestHelper.Load( @"Images/Illusts/BlogParts/user.xml" ) );
			Assert.AreEqual( "http://seiga.nicovideo.jp/", ret.BaseUrl.ToString() );
			Assert.AreEqual( "http://seiga.nicovideo.jp/user/illust/25012877", ret.PageUrl.ToString() );
			Assert.AreEqual( "http://lohas.nicoseiga.jp/", ret.ImageBaseUrl.ToString() );

			Assert.AreEqual( 3, ret.Images.Count );

			Assert.AreEqual( "im3961132", ret.Images[0].Id );
			Assert.AreEqual( "私の兵装に、何か？", ret.Images[0].Title );
			Assert.AreEqual( "なっぱ＠砲雷Ｇ－２１", ret.Images[0].UserName );

			Assert.AreEqual( "im3952384", ret.Images[1].Id );
			Assert.AreEqual( "オクトーバーフェストin鎮守府", ret.Images[1].Title );
			Assert.AreEqual( "なっぱ＠砲雷Ｇ－２１", ret.Images[1].UserName );

			Assert.AreEqual( "im3918678", ret.Images[2].Id );
			Assert.AreEqual( "メイドづほ", ret.Images[2].Title );
			Assert.AreEqual( "なっぱ＠砲雷Ｇ－２１", ret.Images[2].UserName );
		}

		[TestMethod]
		public void IllustBlogParts_2エラーデータ()
		{
			var ret = BlogPartsClient.ParseBlogPartsData( TestHelper.Load( @"Images/Illusts/BlogParts/zero.xml" ) );
			Assert.AreEqual( "http://seiga.nicovideo.jp/", ret.BaseUrl.ToString() );
			Assert.AreEqual( "http://seiga.nicovideo.jp/clip/2", ret.PageUrl.ToString() );
			Assert.AreEqual( "http://lohas.nicoseiga.jp/", ret.ImageBaseUrl.ToString() );
			Assert.AreEqual( 0, ret.Images.Count );
		}
	}
}