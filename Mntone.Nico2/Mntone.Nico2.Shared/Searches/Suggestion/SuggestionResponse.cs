using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Searches.Suggestion
{
	/// <summary>
	/// ニコニコの検索候補を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class SuggestionResponse
	{
		internal SuggestionResponse()
		{ }

		/// <summary>
		/// 候補一覧
		/// </summary>
		public IReadOnlyList<string> Candidates
		{
			get { return this._Candidates; }
		}
		private List<string> _Candidates = null;

		[DataMember( Name = "candidates" )]
		private List<string> CandidatesImpl
		{
			get { return this._Candidates ?? ( this._Candidates = new List<string>() ); }
			set { this._Candidates = value; }
		}
	}
}