using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	[DataContract]
	public sealed class OnAirProgramCommunity
	{
		internal OnAirProgramCommunity()
		{ }

		/// <summary>
		/// 生 ID
		/// </summary>
		[DataMember( Name = "id" )]
		public uint RawID { get; private set; }

#if DEBUG
		/// <summary>
		/// チャンネル ID
		/// </summary>
		[DataMember( Name = "channel_id" )]
		public string ChannelID { get; private set; }
#endif

		/// <summary>
		/// ID
		/// </summary>
		[DataMember( Name = "global_id" )]
		public string ID { get; private set; }

		/// <summary>
		/// 名前
		/// </summary>
		[DataMember( Name = "name" )]
		public string Name { get; private set; }

		/// <summary>
		/// サムネール URL
		/// </summary>
		[DataMember( Name = "thumbnail" )]
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 小さいサムネール URL
		/// </summary>
		[DataMember( Name = "thumbnail_small" )]
		public Uri SmallThumbnailUrl { get; private set; }
	}
}