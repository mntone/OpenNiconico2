
namespace Mntone.Nico2
{
	/// <summary>
	/// ログオンしたアカウントの権限
	/// </summary>
	public enum NiconicoAccountAuthority
	{
		/// <summary>
		/// ログオンしていない
		/// </summary>
		NotLoggedOn = 0,

		/// <summary>
		/// 一般会員でログオンしている
		/// </summary>
		Normal = 1,

		/// <summary>
		/// プレミアム会員でログオンしている
		/// </summary>
		Premium = 3,
	}
}