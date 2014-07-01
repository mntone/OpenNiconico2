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
		/// カテゴリー
		/// </summary>
		public Category Category { get; private set; }

		[DataMember( Name = "category" )]
		private char CategoryImpl
		{
			get { return Category.ToCategoryChar(); }
			set { Category = value.ToCategory(); }
		}

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; private set; }

		/// <summary>
		/// 表示名
		/// </summary>
		[DataMember( Name = "view_title" )]
		public string ViewTitle { get; private set; }

		/// <summary>
		/// 概要
		/// </summary>
		[DataMember( Name = "summary" )]
		public string Summary { get; private set; }
	}
}