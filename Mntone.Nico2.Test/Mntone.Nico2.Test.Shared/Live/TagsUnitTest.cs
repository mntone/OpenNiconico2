using HtmlAgilityPack;
using Mntone.Nico2.Live;
using Mntone.Nico2.Live.Tags;
using System;
using System.Linq;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class TagsUnitTest
	{
		[TestMethod]
		public void TagsTest()
		{
			var data = TestHelper.Load( @"Live/Tags/tags.html" );
			var ulHtmlDoc = new HtmlDocument();
			ulHtmlDoc.LoadHtml( data );
			var ulHtml = ulHtmlDoc.DocumentNode;

			var ret = TagsClient.ParseTagsData( data );
			Assert.AreEqual( ulHtml.ChildNodes.Any( child => child.GetElementByClassName( "edit" ) != null ), ret.IsEditable );

			foreach( var tag in ulHtml.ChildNodes.Where( child => child.GetElementByClassName( "nicopedia" ) != null ) )
			{
				var tagValue = tag.GetElementByClassName( "nicopedia" ).InnerText;
				var retTag = ret.Tags.Where( t => t.Value == tagValue ).Single();
				Assert.AreEqual( tagValue, retTag.Value );
				Assert.AreEqual( tag.GetElementByClassName( "category" ) != null, retTag.IsCategoryTag );
				Assert.AreEqual( tag.GetElementByClassName( "npit" ).InnerText.Trim( new char[] { '(', ')' } ).ToUShort(), retTag.Count );
			}
		}
	}
}