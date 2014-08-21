
namespace Mntone.Nico2
{
	/// <summary>
	/// ログインしたアカウントの権限
	/// </summary>
	public enum NiconicoAccountAuthority
	{
		/// <summary>
		/// ログインしていない
		/// </summary>
		NotSignedIn = 0,

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