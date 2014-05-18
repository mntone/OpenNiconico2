using Mntone.Nico2.Live;
using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	/// <summary>
	/// 放送中の番組ビデオ情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OnAirProgramVideo
	{
		internal OnAirProgramVideo()
		{ }

		/// <summary>
		/// ID
		/// </summary>
		[DataMember( Name = "id" )]
		public string ID { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; private set; }

		/// <summary>
		/// 開場日時
		/// </summary>
		public DateTimeOffset OpenedAt { get { return this._OpenedAt; } }
		private DateTimeOffset _OpenedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "open_time" )]
		private string OpenedAtImpl
		{
			get { return this._OpenedAt.ToString(); }
			set { this._OpenedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get { return this._StartedAt; } }
		private DateTimeOffset _StartedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "start_time" )]
		private string StartedAtImpl
		{
			get { return this._StartedAt.ToString(); }
			set { this._StartedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// 終了予定日時
		/// </summary>
		public DateTimeOffset EndedAtInPlan { get { return this._EndedAtInPlan; } }
		private DateTimeOffset _EndedAtInPlan = DateTimeOffset.MinValue;

		[DataMember( Name = "schedule_end_time" )]
		private string EndedAtInPlanImpl
		{
			get { return this._EndedAtInPlan.ToString(); }
			set { this._EndedAtInPlan = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// 終了日時
		/// </summary>
		public DateTimeOffset EndedAt { get { return this._EndedAt; } }
		private DateTimeOffset _EndedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "end_time" )]
		private string EndedAtImpl
		{
			get { return this._EndedAt.ToString(); }
			set { this._EndedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// 公式配信か
		/// </summary>
		public bool IsOfficial { get { return CommunityType == CommunityType.Official; } }

		/// <summary>
		/// チャンネル配信か
		/// </summary>
		public bool IsChannel { get { return CommunityType == CommunityType.Channel; } }

		/// <summary>
		/// コミュニティー配信か
		/// </summary>
		public bool IsCommunity { get { return CommunityType == CommunityType.Community; } }

		/// <summary>
		/// コミュニティーの種類
		/// </summary>
		public CommunityType CommunityType { get; private set; }

		[DataMember( Name = "provider_type" )]
		private string CommunityTypeImpl
		{
			get { return this.CommunityType.ToCommunityTypeString(); }
			set { this.CommunityType = value.ToCommunityType(); }
		}

		/// <summary>
		/// 関連したチャンネル ID
		/// </summary>
		/// <remarks>
		/// 公式配信時、関連したチャンネル ID が格納されている
		/// </remarks>
		[DataMember( Name = "related_channel_id" )]
		public string RelatedChannelID { get; private set; }

		/// <summary>
		/// サムネール URL
		/// </summary>
		[DataMember( Name = "_picture_url" )]
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 小さいサムネール URL
		/// </summary>
		[DataMember( Name = "_thumbnail_url" )]
		public Uri SmallThumbnailUrl { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		[DataMember( Name = "hidescore_online" )]
		public ushort HidescoreOnline { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		[DataMember( Name = "hidescore_comment" )]
		public ushort HidescoreComment { get; private set; }

		/// <summary>
		/// メンバー限定放送か
		/// </summary>
		/// <remarks>
		/// コミュニティーならコミュニティー限定。
		/// チャンネルなら会員限定。
		/// </remarks>
		public bool IsMemberOnly { get { return this._IsMemberOnly; } }
		private bool _IsMemberOnly = false;

		[DataMember( Name = "community_only" )]
		private bool IsCommunityOnly
		{
			get { return this._IsMemberOnly; }
			set
			{
				if( value )
				{
					this._IsMemberOnly = true;
				}
			}
		}

		[DataMember( Name = "channel_only" )]
		private bool IsChannelOnly
		{
			get { return this._IsMemberOnly; }
			set
			{
				if( value )
				{
					this._IsMemberOnly = true;
				}
			}
		}

		/// <summary>
		/// 閲覧数
		/// </summary>
		[DataMember( Name = "view_counter" )]
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		[DataMember( Name = "comment_count" )]
		public uint CommentCount { get; private set; }

		/// <summary>
		/// タイムシフト予約数
		/// </summary>
		[DataMember( Name = "_ts_reserved_count" )]
		public uint TimeshiftReservedCount { get; private set; }

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		[DataMember( Name = "timeshift_enabled" )]
		public ushort TimeshiftEnabled { get; private set; }

		/// <summary>
		/// 高画質放送か
		/// </summary>
		[DataMember( Name = "is_hq" )]
		public bool IsHighQuality { get; private set; }
	}
}