using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// コミュニティー情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class CommunityInfo
	{
		internal CommunityInfo()
		{ }

		/// <summary>
		/// 生 ID
		/// </summary>
		[DataMember( Name = "id" )]
		public uint RawId { get; private set; }

#if DEBUG
		/// <summary>
		/// チャンネル ID
		/// </summary>
		[DataMember( Name = "channel_id" )]
		public string ChannelId { get; private set; }
#endif

		/// <summary>
		/// ID
		/// </summary>
		[DataMember( Name = "global_id" )]
		public string Id { get; private set; }

		/// <summary>
		/// 名前
		/// </summary>
		[DataMember( Name = "name" )]
		public string Name { get; private set; }

		/// <summary>
		/// ユーザー数
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "user_count" )]
		public uint UserCount { get; private set; }

		/// <summary>
		/// レベル
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "level" )]
		public ushort Level { get; private set; }

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