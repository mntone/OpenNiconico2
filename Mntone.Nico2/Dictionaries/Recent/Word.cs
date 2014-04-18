using System.Runtime.Serialization;

namespace Mntone.Nico2.Dictionaries.Recent
{
	/// <summary>
	/// 単語の情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class Word
	{
		internal Word()
		{ }

		/// <summary>
		/// 単語のカテゴリー
		/// </summary>
		public Category Category { get; private set; }

		[DataMember( Name = "category" )]
		private string CategoryImpl
		{
			get { return Category.ToCategoryString(); }
			set { Category = value.ToCategory(); }
		}

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