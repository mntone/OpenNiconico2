using Mntone.Nico2.Images.Illusts.BlogPartsRanking;
using System;
using System.Xml.Linq;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Images
{
	[TestClass]
	public sealed class IllustBlogPartsRankingUnitTest
	{
		[TestMethod]
		public void IllustBlogPartsRanking_0正常データ()
		{
			var data = TestHelper.Load( @"Images/Illusts/BlogPartsRanking/default.xml" );
			var ret = BlogPartsRankingClient.ParseRankingData( data );
			var ret2 = XDocument.Parse( data ).Element( "response" );

			Assert.AreEqual( ret2.Element( "base_url" ).Value, ret.BaseUrl.ToString() );
			Assert.AreEqual( ret2.Element( "icon_url" ).Value, ret.PageUrl.ToString() );
			Assert.AreEqual( ret2.Element( "image_url" ).Value, ret.ImageBaseUrl.ToString() );

			var list = ret2.Element( "image_list" ).Elements().GetEnumerator();
			list.MoveNext();
			for( var i = 0; i < ret.Images.Count; ++i )
			{
				Assert.AreEqual( "im" + list.Current.Element( "id" ).Value, ret.Images[i].Id );
				Assert.AreEqual( list.Current.Element( "title" ).Value, ret.Images[i].Rank + "位 " + ret.Images[i].Title );
				Assert.AreEqual( list.Current.Element( "nickname" ).Value, ret.Images[i].UserName );
				list.MoveNext();
			}
		}

		[TestMethod]
		public void IllustBlogPartsRanking_1異常文字列データ()
		{
			Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
			{
				BlogPartsRankingClient.ParseRankingData( TestHelper.Load( @"Images/Illusts/BlogPartsRanking/default2.xml" ) );
			} );
		}
	}
}