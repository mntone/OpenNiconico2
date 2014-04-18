
namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコのログイン フラグ
	/// </summary>
	public enum NiconicoAccountAuthority
	{
		/// <summary>
		/// ログインしていない
		/// </summary>
		NotLoggedIn = 0,

		/// <summary>
		/// 一般会員でログインしている
		/// </summary>
		Normal = 1,

		/// <summary>
		/// プレミアム会員でログインしている
		/// </summary>
		Premium = 3,
	}
}