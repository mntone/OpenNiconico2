
namespace Mntone.Nico2
{
	/// <summary>
	/// ログイン状態
	/// </summary>
	public enum NiconicoSignInStatus
	{
		/// <summary>
		/// サービス停止中
		/// </summary>
		/// <remarks>
		/// niconico がメインテナンス中の可能性があります
		/// </remarks>
		ServiceUnavailable = -2,

		/// <summary>
		/// ログインに失敗した
		/// </summary>
		Failed = -1,

		/// <summary>
		/// ログインに成功した
		/// </summary>
		Success = 1,
	}
}