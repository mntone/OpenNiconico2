using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	/// <summary>
	/// 放送中の番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OnAirProgram
	{
		internal OnAirProgram()
		{ }

		/// <summary>
		/// ビデオ
		/// </summary>
		[DataMember( Name = "video" )]
		public OnAirProgramVideo Video { get; private set; }

		/// <summary>
		/// コミュニティー
		/// </summary>
		[DataMember( Name = "community" )]
		public OnAirProgramCommunity Community { get; private set; }
	}
}