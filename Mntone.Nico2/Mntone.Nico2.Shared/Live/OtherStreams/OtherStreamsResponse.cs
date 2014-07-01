using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Live.OtherStreams
{
	/// <summary>
	/// 番組を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OtherStreamsResponse
	{
		internal OtherStreamsResponse()
		{ }

		/// <summary>
		/// 番組の一覧
		/// </summary>
		public IReadOnlyList<OtherStream> Streams { get { return this._Streams ?? ( this._Streams = new List<OtherStream>() ); } }
		private List<OtherStream> _Streams = null;

		[DataMember( Name = "reserved_stream_list" )]
		private List<OtherStream> StreamsImpl
		{
			get { return this._Streams ?? ( this._Streams = new List<OtherStream>() ); }
			set { this._Streams = value; }
		}

#if DEBUG
		/// <summary>
		/// 含まれている合計番組数
		/// </summary>
		[DataMember( Name = "total" )]
		public ushort TotalCount { get; set; }
#endif
	}
}