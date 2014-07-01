
namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 映像の表示位置
	/// </summary>
	public enum VideoPosition
	{
		/// <summary>
		/// 通常
		/// </summary>
		Default,

		/// <summary>
		/// 上に表示
		/// </summary>
		Top,

		/// <summary>
		/// 下に表示
		/// </summary>
		Bottom,

		/// <summary>
		/// 小さく表示
		/// </summary>
		/// <remarks>
		/// コメントが背面に流れるので、映像を小さく表示します
		/// </remarks>
		Small,
	}
}