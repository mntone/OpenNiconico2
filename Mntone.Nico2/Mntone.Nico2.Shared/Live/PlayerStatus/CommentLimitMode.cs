
namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 製品番組の場合のコメント モード
	/// </summary>
	public enum CommentLimitMode
	{
		/// <summary>
		/// 許可
		/// </summary>
		Allow = 0,

		/// <summary>
		/// 制限あり
		/// </summary>
		Restrict,

		/// <summary>
		/// 禁止
		/// </summary>
		Deny,
	}
}