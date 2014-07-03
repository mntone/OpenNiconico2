using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2
{
	internal static class XmlDocumentExtensions
	{
		public static XElement GetDocumentRootNode( this XDocument node )
		{
			return node.Elements().First();
		}


		public static XAttribute GetNamedAttribute( this XElement node, string name )
		{
			return node.Attribute( name );
		}

		public static string GetText( this XAttribute node )
		{
			if( node == null )
			{
				return string.Empty;
			}

			return node.Value.Trim();
		}

		public static string GetNamedAttributeText( this XElement node, string name )
		{
			return node.GetNamedAttribute( name ).GetText();
		}

		public static IEnumerable<XElement> GetChildNodes( this XElement node )
		{
			return node.Elements();
		}

		public static IEnumerable<XElement> GetNamedChildNodes( this XElement node, string name )
		{
			return node.Elements( name );
		}

		public static XElement GetFirstChildNode( this XElement node )
		{
			return node.Elements().ElementAtOrDefault( 0 );
		}

		public static XElement GetChildNodeAt( this XElement node, int index )
		{
			return node.Elements().ElementAtOrDefault( index );
		}

		public static XElement GetNamedChildNode( this XElement node, string name )
		{
			return node.Element( name );
		}

		public static string GetNamedChildNodeText( this XElement node, string name )
		{
			return node.GetNamedChildNode( name ).GetText();
		}

		public static string GetName( this XElement node )
		{
			return node.Name.LocalName;
		}

		public static string GetText( this XElement node )
		{
			if( node == null )
			{
				return string.Empty;
			}

			return node.Value.Trim();
		}
	}
}