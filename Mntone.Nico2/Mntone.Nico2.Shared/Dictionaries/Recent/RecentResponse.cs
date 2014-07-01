using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Dictionaries.Recent
{
	/// <summary>
	/// ニコニコ大百科の最近追加された単語を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class RecentResponse
	{
		internal RecentResponse()
		{ }

		/// <summary>
		/// 単語の一覧
		/// </summary>
		public IReadOnlyList<Word> Words
		{
			get { return this._Words; }
		}
		private List<Word> _Words = null;

		[DataMember( Name = "pages" )]
		private List<Word> WordsImpl
		{
			get { return this._Words ?? ( this._Words = new List<Word>() ); }
			set { this._Words = value; }
		}
	}
}