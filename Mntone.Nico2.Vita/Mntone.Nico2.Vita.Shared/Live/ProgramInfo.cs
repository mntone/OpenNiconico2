using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class ProgramInfo
	{
		internal ProgramInfo()
		{ }

		/// <summary>
		/// ビデオ
		/// </summary>
		[DataMember( Name = "video", IsRequired = true )]
		public VideoInfo Video { get; private set; }

		/// <summary>
		/// コミュニティー
		/// </summary>
		[DataMember( Name = "community" )]
		public CommunityInfo Community { get; private set; }

		/// <summary>
		/// タグ情報
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "livetags" )]
		public TagsInfo Tags { get; private set; }
	}
}