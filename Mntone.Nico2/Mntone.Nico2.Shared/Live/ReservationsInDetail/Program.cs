using Mntone.Nico2.Images.Illusts;
using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
#if WINDOWS_APP
		internal Program( IXmlNode reservedItemXml )
#else
		internal Program( XElement reservedItemXml )
#endif
		{
			Id = "lv" + reservedItemXml.GetNamedChildNodeText( "vid" );
			Title = reservedItemXml.GetNamedChildNodeText( "title" );
			Status = reservedItemXml.GetNamedChildNodeText( "status" );
			IsUnwatched = reservedItemXml.GetNamedChildNodeText( "unwatch" ).ToBooleanFrom1();

			var expire = reservedItemXml.GetNamedChildNodeText( "expire" );
			ExpiredAt = expire != "0" ? expire.ToDateTimeOffsetFromUnixTime() : DateTimeOffset.MaxValue;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 状態
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// 未視聴か
		/// </summary>
		public bool IsUnwatched { get; private set; }

		/// <summary>
		/// 有効期限日時
		/// </summary>
		public DateTimeOffset ExpiredAt { get; private set; }
	}
}