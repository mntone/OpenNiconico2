using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2
{
	internal static class XmlDocumentExtensions
	{
		public static IXmlNode GetNamedAttribute( this IXmlNode node, string name )
		{
			return node.Attributes.GetNamedItem( name );
		}

		public static IEnumerable<IXmlNode> GetNamedChildNodes( this IXmlNode node, string name )
		{
			return node.ChildNodes.Where( childNode => childNode.NodeName == name );
		}

		public static IXmlNode GetNamedChildNode( this IXmlNode node, string name )
		{
			return node.ChildNodes.Where( childNode => childNode.NodeName == name ).Single();
		}
	}
}