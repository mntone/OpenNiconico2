
namespace Mntone.Nico2.Live.MyPage
{
	/// <summary>
	/// タイムシフトのデータを格納するクラス
	/// </summary>
	public sealed class TimeshiftProgramInfo
	{
		internal TimeshiftProgramInfo( string id, string title )
		{
			this.ID = id;
			this.Title = title;
		}

		/// <summary>
		/// 番組 ID
		/// </summary>
		public string ID { get; internal set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }
	}
}