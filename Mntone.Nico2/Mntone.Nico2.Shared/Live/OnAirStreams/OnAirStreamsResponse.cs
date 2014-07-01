using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Live.OnAirStreams
{
	/// <summary>
	/// 放送中の番組を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OnAirStreamsResponse
	{
		internal OnAirStreamsResponse()
		{ }

		/// <summary>
		/// 放送中の番組の一覧
		/// </summary>
		public IReadOnlyList<OnAirStream> OnAirStreams { get { return this._OnAirStreams; } }
		private List<OnAirStream> _OnAirStreams = null;

		[DataMember( Name = "onair_stream_list" )]
		private List<OnAirStream> OnAirStreamsImpl
		{
			get { return this._OnAirStreams ?? ( this._OnAirStreams = new List<OnAirStream>() ); }
			set { this._OnAirStreams = value; }
		}

		/// <summary>
		/// 予約している放送中の番組一覧 (index のみ)
		/// </summary>
		public IReadOnlyList<ReservedStream> ReservedStreams { get { return this._ReservedStreams; } }
		private List<ReservedStream> _ReservedStreams = null;

		[DataMember( Name = "reserved_stream_list" )]
		private List<ReservedStream> ReservedStreamsImpl
		{
			get { return this._ReservedStreams ?? ( this._ReservedStreams = new List<ReservedStream>() ); }
			set { this._ReservedStreams = value; }
		}
	}
}