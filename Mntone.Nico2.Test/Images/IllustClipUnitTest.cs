using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Images.Illusts.Clip;

namespace Mntone.Nico2.Test.Images
{
	[TestClass]
	public sealed class IllustClipUnitTest
	{
		[TestMethod]
		public void IllustClip_0通常データ()
		{
			var ret = ClipClient.ParseClipData( TestHelper.Load( @"Images/Illusts/Clip/default.xml" ) );
			Assert.AreEqual( "http://seiga.nicovideo.jp/clip/565392", ret.PageUrl.ToString() );

			Assert.AreEqual( 3, ret.Images.Count );

			Assert.AreEqual( "im2032391", ret.Images[0].ID );
			Assert.AreEqual( "【MMD-OMF2】D.Gray-man　クロウリー", ret.Images[0].Title );
			Assert.AreEqual( "とらみ", ret.Images[0].UserName );

			Assert.AreEqual( "im2032390", ret.Images[1].ID );
			Assert.AreEqual( "【MMD-OMF2】加湿器的なもの", ret.Images[1].Title );
			Assert.AreEqual( "過給機24", ret.Images[1].UserName );

			Assert.AreEqual( "im2032386", ret.Images[2].ID );
			Assert.AreEqual( "【MMD-OMF2】ねんどろ風けいおんモデル(3/3)", ret.Images[2].Title );
			Assert.AreEqual( "くろぱん", ret.Images[2].UserName );
		}

		[TestMethod]
		public void IllustClip_1エラーデータ()
		{
			var ret = ClipClient.ParseClipData( TestHelper.Load( @"Images/Illusts/Clip/zero.xml" ) );
			Assert.AreEqual( "http://seiga.nicovideo.jp/clip/2", ret.PageUrl.ToString() );
			Assert.AreEqual( 0, ret.Images.Count );
		}
	}
}