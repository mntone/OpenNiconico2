using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ情報
	/// </summary>
	public sealed class Tag
	{
		internal Tag( IXmlNode tagXml )
		{
			var catAttr = tagXml.GetNamedAttribute( "category" );
			if( catAttr != null )
			{
				Category = catAttr.InnerText.ToBooleanFrom1();
			}

			var lockAttr = tagXml.GetNamedAttribute( "lock" );
			if( lockAttr != null )
			{
				Lock = lockAttr.InnerText.ToBooleanFrom1();
			}

			Value = tagXml.FirstChild.InnerText;
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