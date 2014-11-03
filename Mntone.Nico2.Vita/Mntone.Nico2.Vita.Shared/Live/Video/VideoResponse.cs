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
		/// 番組情報
		/// </summary>
		[DataMember( Name = "video_info", IsRequired = true )]
		public ProgramInfo Program { get; private set; }
	}
}