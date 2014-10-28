
namespace Mntone.Nico2.Live.Tags
{
	/// <summary>
	/// タグのデータを格納するクラス
	/// </summary>
	public sealed class TagInfo
	{
		internal TagInfo( bool isCategoryTag, string value, ushort count )
		{
			this.IsCategoryTag = isCategoryTag;
			this.Value = value;
			this.Count = count;
		}

		/// <summary>
		/// カテゴリー タグかどうか
		/// </summary>
		public bool IsCategoryTag { get; internal set; }

		/// <summary>
		/// タグの内容
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// タグの利用回数
		/// </summary>
		public ushort Count { get; private set; }
	}
}