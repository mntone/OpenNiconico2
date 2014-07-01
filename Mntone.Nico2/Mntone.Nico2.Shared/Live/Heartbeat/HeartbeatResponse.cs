using System;
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
			LoadedAt = heartbeatXml.GetNamedAttribute( "time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			WatchCount = heartbeatXml.GetNamedChildNode( "watchCount" ).InnerText.ToUInt();
			CommentCount = heartbeatXml.GetNamedChildNode( "commentCount" ).InnerText.ToUInt();
			IsRestrict = heartbeatXml.GetNamedChildNode( "is_restrict" ).InnerText.ToBooleanFrom1();
			Ticket = heartbeatXml.GetNamedChildNode( "ticket" ).InnerText;
			WaitDuration = heartbeatXml.GetNamedChildNode( "waitTime" ).InnerText.ToTimeSpanFromSecondsString();
		}

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// 合計視聴者数
		/// </summary>
		public uint WatchCount { get; private set; }

		/// <summary>
		/// 合計コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// ?
		/// </summary>
		public bool IsRestrict { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; private set; }

		/// <summary>
		/// 待機時間
		/// </summary>
		public TimeSpan WaitDuration { get; private set; }
	}
}