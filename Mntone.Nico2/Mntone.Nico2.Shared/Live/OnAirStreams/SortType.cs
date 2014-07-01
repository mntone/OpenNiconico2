
namespace Mntone.Nico2.Live.OnAirStreams
{
	/// <summary>
	/// 整列方法
	/// </summary>
	public enum SortType
	{
		/// <summary>
		/// 放送日時順
		/// </summary>
		StartTime,

		/// <summary>
		/// 来場者数順
		/// </summary>
		ViewCount,

		/// <summary>
		/// コメント数順
		/// </summary>
		CommentCount,

		/// <summary>
		/// コミュニティーレベル順
		/// </summary>
		CommunityLevel,

		/// <summary>
		/// コミュニティー作成日時順
		/// </summary>
		CommunityCreateTime,
	}
}