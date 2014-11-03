using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	/// <summary>
	/// タグ情報を格納するクラス
	/// </summary>
	[DataContract]
    public sealed class TagInfo
    {
		private TagInfo()
		{ }

		/// <summary>
		/// タグの個数
		/// </summary>
		[DataMember( Name = "count", IsRequired = true )]
		public ushort Count { get; private set; }

		/// <summary>
		/// タグの内容
		/// </summary>
		[DataMember( Name = "name", IsRequired = true )]
		public string Value { get; private set; }
    }
}