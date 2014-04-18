using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ データ
	/// </summary>
	public sealed class Tags
	{
		internal Tags( IXmlNode tagsXml )
		{
			Domain = tagsXml.GetNamedAttribute( "domain" ).InnerText;
			Value = tagsXml.ChildNodes.Select( tagXml => new Tag( tagXml ) ).ToList();
		}

		/// <summary>
		/// タグのドメイン
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// タグの一覧
		/// </summary>
		public IReadOnlyList<Tag> Value { get; private set; }
	}
}