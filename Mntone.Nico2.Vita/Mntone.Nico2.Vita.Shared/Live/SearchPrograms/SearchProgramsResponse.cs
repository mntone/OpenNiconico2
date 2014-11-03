using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	/// <summary>
	/// 検索した番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class SearchProgramsResponse
	{
		private SearchProgramsResponse()
		{ }

		[DataMember( Name = "@status", IsRequired = true )]
		private string StatusImpl
		{
			get { return string.Empty; }
			set
			{
				if( value != "ok" )
				{
					if( value == "maintenance" )
					{
						throw CustomExceptionFactory.Create( NiconicoHResult.E_MAINTENANCE );
					}
					throw new ParseException();
				}
			}
		}

		/// <summary>
		/// 検索が末尾まで行ったか
		/// </summary>
		public bool IsTerminate { get; private set; }

		[DataMember( Name = "is_terminate", IsRequired = true )]
		private string IsTerminateImpl
		{
			get { return this.IsTerminate.ToString1Or0(); }
			set { this.IsTerminate = value.ToBooleanFrom1(); }
		}

#if DEBUG
		/// <summary>
		/// 含有個数
		/// </summary>
		[DataMember( Name = "count", IsRequired = true )]
		public ushort Count { get; private set; }
#endif

		/// <summary>
		/// 含有個数
		/// </summary>
		[DataMember( Name = "total_count", IsRequired = true )]
		public TotalCountInfo TotalCount { get; private set; }

		[DataContract]
		class TagInfoWrapper
		{
			internal TagInfoWrapper( List<ProgramInfo> tag )
			{
				this._Tags = tag;
			}

			[DataMember( Name = "tag" )]
			private List<ProgramInfo> TagsImpl
			{
				get { return this._Tags ?? ( this._Tags = new List<ProgramInfo>() ); }
				set { this._Tags = value; }
			}
			internal List<ProgramInfo> _Tags = null;
		}

		/// <summary>
		/// サジェストされたタグ
		/// </summary>
		public IReadOnlyList<ProgramInfo> Tags { get { return this._Tags ?? ( this._Tags = new List<ProgramInfo>() ); } }
		private List<ProgramInfo> _Tags = null;

		[DataMember( Name = "tags", IsRequired = true )]
		private TagInfoWrapper TagsImpl
		{
			get { return new TagInfoWrapper( this._Tags ); }
			set { this._Tags = value._Tags; }
		}

		/// <summary>
		/// 番組情報
		/// </summary>
		[DataMember( Name = "video_info", IsRequired = true )]
		public ProgramInfo Program { get; private set; }
	}
}