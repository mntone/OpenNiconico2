﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.Videos
{
	/// <summary>
	/// 番組を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class VideosResponse
	{
		internal VideosResponse()
		{ }

		[DataMember( Name = "@status" )]
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
		/// 番組の一覧
		/// </summary>
		public IReadOnlyList<ProgramInfo> Programs { get { return this._Programs ?? ( this._Programs = new List<ProgramInfo>() ); } }
		private List<ProgramInfo> _Programs = null;

		[DataMember( Name = "video_info" )]
		private List<ProgramInfo> ProgramsImpl
		{
			get { return this._Programs ?? ( this._Programs = new List<ProgramInfo>() ); }
			set { this._Programs = value; }
		}

#if DEBUG
		/// <summary>
		/// 含まれている合計番組数
		/// </summary>
		[DataMember( Name = "count" )]
		public ushort ParticalCount { get; private set; }
#endif
	}
}