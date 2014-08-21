#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Image
	{
#if WINDOWS_APP
		internal Image( IXmlNode imageXml )
#else
		internal Image( XElement imageXml )
#endif
		{
			Id = "im" + imageXml.GetNamedChildNodeText( "id" );
			//CacheTime = imageXml.GetNamedChildNodeText( "cache_time" ).ToDateTimeOffsetFromIso8601();
			Title = imageXml.GetNamedChildNodeText( "title" );
			UserName = imageXml.GetNamedChildNodeText( "nickname" );
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

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