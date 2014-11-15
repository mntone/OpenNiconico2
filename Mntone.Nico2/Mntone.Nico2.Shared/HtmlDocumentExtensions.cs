using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Mntone.Nico2
{
	internal static class HtmlDocumentExtensions
	{
		public static IEnumerable<HtmlNode> GetElementsByClassName( this HtmlNode node, string className )
		{
			return node.ChildNodes.Where( n => n.GetAttributeValue( "class", string.Empty ).Split( new char[] { ' ' } ).Any( s => s == className ) );
		}

		public static IEnumerable<HtmlNode> GetElementsByTagName( this HtmlNode node, string tagName )
		{
			return node.ChildNodes.Where( n => n.Name == tagName );
		}

		public static HtmlNode GetElementByClassName( this HtmlNode node, string className )
		{
			return node.GetElementsByClassName( className ).SingleOrDefault();
		}

		public static HtmlNode GetElementByTagName( this HtmlNode node, string tagName )
		{
			return node.GetElementsByTagName( tagName ).SingleOrDefault();
		}

		public static HtmlNode GetElementById( this HtmlNode node, string idName )
		{
			return node.ChildNodes.Where( n => n.GetAttributeValue( "id", string.Empty ) == idName ).SingleOrDefault();
		}
	}
}