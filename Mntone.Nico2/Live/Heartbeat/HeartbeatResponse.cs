using System;
using System.Xml.Serialization;

namespace Mntone.Nico2.Live.Heartbeat
{
	/// <summary>
	/// heartbeat の情報を格納するクラス
	/// </summary>
	public sealed class HeartbeatResponse
	{
		internal HeartbeatResponse()
		{ }

		/// <summary>
		/// 時間
		/// </summary>
		[XmlIgnore]
		public DateTimeOffset Time { get; internal set; }

		/// <summary>
		/// 合計視聴者数
		/// </summary>
		public uint WatchCount { get; internal set; }

		/// <summary>
		/// 合計コメント数
		/// </summary>
		public uint CommentCount { get; internal set; }

		public bool IsRestrict { get; internal set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; internal set; }

		/// <summary>
		/// 待機時間
		/// </summary>
		public ushort WaitTime { get; internal set; }
	}
}