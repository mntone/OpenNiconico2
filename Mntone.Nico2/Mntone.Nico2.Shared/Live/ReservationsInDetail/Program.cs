using Mntone.Nico2.Images.Illusts;
using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
		internal Program( IXmlNode reservedItemXml )
		{
			ID = "lv" + reservedItemXml.GetNamedChildNode( "vid" ).InnerText;
			Title = reservedItemXml.GetNamedChildNode( "title" ).InnerText;
			Status = reservedItemXml.GetNamedChildNode( "status" ).InnerText;
			IsUnwatched = reservedItemXml.GetNamedChildNode( "unwatch" ).InnerText.ToBooleanFrom1();

			var expire = reservedItemXml.GetNamedChildNode( "expire" ).InnerText;
			ExpiredAt = expire != "0" ? expire.ToDateTimeOffsetFromUnixTime() : DateTimeOffset.MaxValue;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

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