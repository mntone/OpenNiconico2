using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// 番組を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class ProgramsResponse
	{
		internal ProgramsResponse()
		{ }

		[DataMember( Name = "@status", IsRequired = true )]
		private string StatusImpl
		{
			get { return string.Empty; }
			set
			{
				if( value != "ok" )
				{
					throw new Exception( "Parse Error." );
				}
			}
		}

		/// <summary>
		/// 番組の一覧
		/// </summary>
		public IReadOnlyList<ProgramInfo> Programs { get { return this._Programs ?? ( this._Programs = new List<ProgramInfo>() ); } }
		private List<ProgramInfo> _Programs = null;

		[DataMember( Name = "video_info" )]
		internal List<ProgramInfo> ProgramsImpl
		{
			get { return this._Programs ?? ( this._Programs = new List<ProgramInfo>() ); }
			set { this._Programs = value; }
		}

#if DEBUG
		/// <summary>
		/// 含まれている合計番組数
		/// </summary>
		[DataMember( Name = "count" )]
		public ushort ParticalCount { get; set; }
#endif

		/// <summary>
		/// 合計番組数
		/// </summary>
		[DataMember( Name = "total_count", IsRequired = true )]
		public ushort TotalCount { get; set; }
	}
}