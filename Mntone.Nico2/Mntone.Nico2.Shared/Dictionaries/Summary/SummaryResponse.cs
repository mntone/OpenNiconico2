using System.Runtime.Serialization;

namespace Mntone.Nico2.Dictionaries.Summary
{
	/// <summary>
	/// ニコニコ大百科の summary を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class SummaryResponse
	{
		internal SummaryResponse()
		{ }

		/// <summary>
		/// 単語の題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; private set; }

		/// <summary>
		/// 単語の表示名
		/// </summary>
		[DataMember( Name = "view_title" )]
		public string ViewTitle { get; private set; }

		/// <summary>
		/// 単語の概要
		/// </summary>
		[DataMember( Name = "summary" )]
		public string Summary { get; private set; }
	}
}