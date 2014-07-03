#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ情報
	/// </summary>
	public sealed class Tag
	{
#if WINDOWS_APP
		internal Tag( IXmlNode tagXml )
#else
		internal Tag( XElement tagXml )
#endif
		{
			Category = tagXml.GetNamedAttributeText( "category" ).ToBooleanFrom1();
			Lock = tagXml.GetNamedAttributeText( "lock" ).ToBooleanFrom1();
			Value = tagXml.GetText();
		}

		/// <summary>
		/// カテゴリー タグか
		/// </summary>
		public bool Category { get; private set; }

		/// <summary>
		/// ロックされているか
		/// </summary>
		public bool Lock { get; private set; }

		/// <summary>
		/// タグの内容
		/// </summary>
		public string Value { get; private set; }
	}
}