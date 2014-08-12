using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Mntone.Nico2
{
	internal static class HtmlDocumentExtensions
	{
		public static IEnumerable<HtmlNode> GetElementsByClassName( this HtmlNode node, string className )
		{
			return node.ChildNodes.Where( n => n.GetAttributeValue( "class", string.Empty ) == className );
		}

		public static HtmlNode GetElementByClassName( this HtmlNode node, string className )
		{
			return node.GetElementsByClassName( className ).SingleOrDefault();
		}
	}
}