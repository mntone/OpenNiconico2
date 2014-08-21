#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
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
			//CacheTime = imageNode.GetNamedChildNodeText( "cache_time" ).ToDateTimeOffsetFromIso8601();

			var titleAndRank = imageXml.GetNamedChildNodeText( "title" );
			var unitIndex = titleAndRank.IndexOf( "位 ", 1 );
			Rank = titleAndRank.Substring( 0, unitIndex ).ToUShort();
			Title = titleAndRank.Substring( unitIndex + 2 );

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
		/// 順位
		/// </summary>
		public ushort Rank { get; private set; }

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