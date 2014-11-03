using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	/// <summary>
	/// 番組数情報を格納するクラス
	/// </summary>
	[DataContract]
    public sealed class TotalCountInfo
    {
		private TotalCountInfo()
		{ }

		/// <summary>
		/// 放送中番組数
		/// </summary>
		[DataMember( Name = "onair", IsRequired = true )]
		public ushort OnAirProgramCount { get; private set; }

		/// <summary>
		/// 放送予定番組数
		/// </summary>
		[DataMember( Name = "reserved", IsRequired = true )]
		public ushort ReservedProgramCount { get; private set; }

		/// <summary>
		/// 放送終了番組数
		/// </summary>
		[DataMember( Name = "closed", IsRequired = true )]
		public ushort ClosedProgramCount { get; private set; }
    }
}