
namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 映像のアスペクト比
	/// </summary>
	public enum VideoAspect
	{
		/// <summary>
		/// 自動決定
		/// </summary>
		/// <remarks>
		/// 横/縦 比が 1.4 を超えたら 16:9。
		/// そうでなければ 4:3
		/// </remarks>
		Auto,

		/// <summary>
		/// 4:3
		/// </summary>
		Normal,

		/// <summary>
		/// 16:9
		/// </summary>
		Wide,

		/// <summary>
		/// 映像のアスペクト比のまま
		/// </summary>
		/// <remarks>
		/// ピクセルアスペクト比は 1:1
		/// </remarks>
		Raw,
	}
}