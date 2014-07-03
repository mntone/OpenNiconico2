using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2
{
	internal static class XmlDocumentExtensions
	{
		public static IXmlNode GetDocumentRootNode( this XmlDocument node )
		{
			return node.ChildNodes[1];
		}


		public static IXmlNode GetNamedAttribute( this IXmlNode node, string name )
		{
			return node.Attributes.GetNamedItem( name );
		}

		public static string GetNamedAttributeText( this IXmlNode node, string name )
		{
			return node.GetNamedAttribute( name ).GetText();
		}

		public static XmlNodeList GetChildNodes( this IXmlNode node )
		{
			return node.ChildNodes;
		}

		public static IEnumerable<IXmlNode> GetNamedChildNodes( this IXmlNode node, string name )
		{
			return node.ChildNodes.Where( childNode => childNode.NodeName == name );
		}

		public static IXmlNode GetFirstChildNode( this IXmlNode node )
		{
			return node.FirstChild;
		}

		public static IXmlNode GetChildNodeAt( this IXmlNode node, int index )
		{
			return node.ChildNodes[index];
		}

		public static IXmlNode GetNamedChildNode( this IXmlNode node, string name )
		{
			return node.GetNamedChildNodes( name ).SingleOrDefault();
		}

		public static string GetNamedChildNodeText( this IXmlNode node, string name )
		{
			return node.GetNamedChildNode( name ).GetText();
		}

		public static string GetName( this IXmlNode node )
		{
			return node.NodeName;
		}

		public static string GetText( this IXmlNode node )
		{
			if( node == null )
			{
				return string.Empty;
			}

			return node.InnerText;
		}
	}
}