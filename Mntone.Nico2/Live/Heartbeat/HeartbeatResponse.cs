using System;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.Heartbeat
{
	/// <summary>
	/// heartbeat の情報を格納するクラス
	/// </summary>
	public sealed class HeartbeatResponse
	{
		internal HeartbeatResponse( IXmlNode heartbeatXml )
		{
			Time = heartbeatXml.GetNamedAttribute( "time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			WatchCount = heartbeatXml.GetNamedChildNode( "watchCount" ).InnerText.ToUInt();
			CommentCount = heartbeatXml.GetNamedChildNode( "commentCount" ).InnerText.ToUInt();
			IsRestrict = heartbeatXml.GetNamedChildNode( "is_restrict" ).InnerText.ToBooleanFrom1();
			Ticket = heartbeatXml.GetNamedChildNode( "ticket" ).InnerText;
			WaitTime = heartbeatXml.GetNamedChildNode( "waitTime" ).InnerText.ToUShort();
		}

		/// <summary>
		/// 時間
		/// </summary>
		[XmlIgnore]
		public DateTimeOffset Time { get; private set; }

		/// <summary>
		/// 合計視聴者数
		/// </summary>
		public uint WatchCount { get; private set; }

		/// <summary>
		/// 合計コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		public bool IsRestrict { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; private set; }

		/// <summary>
		/// 待機時間
		/// </summary>
		public ushort WaitTime { get; private set; }
	}
}