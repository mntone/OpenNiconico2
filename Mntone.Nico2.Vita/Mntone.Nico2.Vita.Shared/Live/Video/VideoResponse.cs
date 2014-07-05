using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.Video
{
	/// <summary>
	/// 番組を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class VideoResponse
	{
		internal VideoResponse()
		{ }

		[DataMember( Name = "@status" )]
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
		/// 番組情報
		/// </summary>
		[DataMember( Name = "video_info" )]
		public ProgramInfo Program { get; private set; }
	}
}