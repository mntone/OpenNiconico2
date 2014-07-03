using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ データ
	/// </summary>
	public sealed class Tags
	{
#if WINDOWS_APP
		internal Tags( IXmlNode tagsXml )
#else
		internal Tags( XElement tagsXml )
#endif
		{
			Domain = tagsXml.GetNamedAttributeText( "domain" );
			Value = tagsXml.GetChildNodes().Select( tagXml => new Tag( tagXml ) ).ToList();
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