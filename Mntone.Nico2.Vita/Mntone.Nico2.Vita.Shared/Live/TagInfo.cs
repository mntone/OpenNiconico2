
namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// タグのデータを格納するクラス
	/// </summary>
	public sealed class TagInfo
	{
		internal TagInfo( string value )
		{
			this.Value = value;
		}

		/// <summary>
		/// カテゴリー タグかどうか
		/// </summary>
		public bool IsCategoryTag { get; internal set; }

		/// <summary>
		/// ロックされているかどうか
		/// </summary>
		public bool IsLocked { get; internal set; }

		/// <summary>
		/// タグの内容
		/// </summary>
		public string Value { get; private set; }
	}
}