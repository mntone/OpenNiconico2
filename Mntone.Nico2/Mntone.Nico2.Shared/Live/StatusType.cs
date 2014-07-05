
namespace Mntone.Nico2.Live
{
	/// <summary>
	/// 番組の状態の種類
	/// </summary>
	public enum StatusType
	{
		/// <summary>
		/// 不明
		/// </summary>
		Invalid,

		/// <summary>
		/// 放送中
		/// </summary>
		OnAir,

		/// <summary>
		/// 放送予定
		/// </summary>
		ComingSoon,

		/// <summary>
		/// 放送終了
		/// </summary>
		Closed,
	}
}