using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Image
	{
		internal Image( IXmlNode imageNode )
		{
			ID = "im" + imageNode.GetNamedChildNode( "id" ).InnerText;
			//CacheTime = imageNode.GetNamedChildNode( "cache_time" ).InnerText.ToDateTimeOffsetFromIso8601();
			Title = imageNode.GetNamedChildNode( "title" ).InnerText;
			UserName = imageNode.GetNamedChildNode( "nickname" ).InnerText;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

		///// <summary>
		///// キャッシュ時間 (?)
		///// </summary>
		//public DateTimeOffset CacheTime { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }
	}
}