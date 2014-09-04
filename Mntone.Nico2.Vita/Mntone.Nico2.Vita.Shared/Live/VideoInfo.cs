using Mntone.Nico2.Live;
using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class VideoInfo
	{
		internal VideoInfo()
		{ }

		/// <summary>
		/// ID
		/// </summary>
		[DataMember( Name = "id", IsRequired = true )]
		public string Id { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title", IsRequired = true )]
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "description" )]
		public string Description
		{
			get { return this._Description ?? string.Empty; }
			private set { this._Description = value; }
		}
		private string _Description = string.Empty;

		/// <summary>
		/// ユーザー ID
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "user_id" )]
		public uint UserId { get; private set; }

		/// <summary>
		/// 開場日時
		/// </summary>
		public DateTimeOffset OpenedAt { get { return this._OpenedAt; } }
		private DateTimeOffset _OpenedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "open_time", IsRequired = true )]
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

		[DataMember( Name = "start_time", IsRequired = true )]
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

		[DataMember( Name = "end_time", IsRequired = true )]
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

		[DataMember( Name = "provider_type", IsRequired = true )]
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
		public string RelatedChannelId { get; private set; }

		/// <summary>
		/// サムネール URL
		/// </summary>
		/// <remarks>公式配信時のみ存在します</remarks>
		[DataMember( Name = "_picture_url" )]
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 小さいサムネール URL
		/// </summary>
		/// <remarks>公式配信時のみ存在します</remarks>
		[DataMember( Name = "_thumbnail_url" )]
		public Uri SmallThumbnailUrl { get; private set; }

		/// <summary>
		/// 番組の状態
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public StatusType Status { get { return this._Status; } }
		private StatusType _Status;

		[DataMember( Name = "_currentstatus" )]
		private string StatusImpl
		{
			get { return this._Status.ToStatusTypeString(); }
			set { this._Status = value.ToStatusType(); }
		}

		/// <summary>
		/// オンライン (?) のスコア基準
		/// </summary>
		public ushort HidescoreOnline { get; private set; }

		[DataMember( Name = "hidescore_online", IsRequired = true )]
		public string HidescoreOnlineImpl
		{
			get { return this.HidescoreOnline.ToString(); }
			set
			{
				if( !string.IsNullOrEmpty( value ) )
				{
					this.HidescoreOnline = value.ToUShort();
				}
			}
		}

		/// <summary>
		/// コメント非表示のスコア基準
		/// </summary>
		[DataMember( Name = "hidescore_comment", IsRequired = true )]
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
		private string IsCommunityOnly
		{
			get { return this._IsMemberOnly.ToString1Or0(); }
			set
			{
				if( value.ToBooleanFrom1() )
				{
					this._IsMemberOnly = true;
				}
			}
		}

		[DataMember( Name = "channel_only" )]
		private string IsChannelOnly
		{
			get { return this._IsMemberOnly.ToString1Or0(); }
			set
			{
				if( value.ToBooleanFrom1() )
				{
					this._IsMemberOnly = true;
				}
			}
		}

		/// <summary>
		/// 閲覧数
		/// </summary>
		[DataMember( Name = "view_counter", IsRequired = true )]
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		[DataMember( Name = "comment_count", IsRequired = true )]
		public uint CommentCount { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "_timeshift_limit" )]
		public int TimeshiftLimit { get; private set; }

		/// <summary>
		/// タイムシフト有効開始時間
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public DateTimeOffset TimeshiftArchiveReleasedAt { get { return this._TimeshiftArchiveReleasedAt; } }
		private DateTimeOffset _TimeshiftArchiveReleasedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "_ts_archive_released_time" )]
		private string TimeshiftArchiveReleasedAtImpl
		{
			get { return this._TimeshiftArchiveReleasedAt.ToString(); }
			set { this._TimeshiftArchiveReleasedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public bool IsTimeshiftUsed { get; private set; }

		[DataMember( Name = "_use_tsarchive" )]
		private string IsTimeshiftUsedImpl
		{
			get { return this.IsTimeshiftUsed.ToString1Or0(); }
			set { this.IsTimeshiftUsed = value.ToBooleanFrom1(); }
		}

		/// <summary>
		/// タイムシフト有効開始時間
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public DateTimeOffset TimeshiftArchiveStartedAt { get { return this._TimeshiftArchiveStartedAt; } }
		private DateTimeOffset _TimeshiftArchiveStartedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "_ts_archive_start_time" )]
		private string TimeshiftArchiveStartedAtImpl
		{
			get { return this._TimeshiftArchiveStartedAt.ToString(); }
			set { this._TimeshiftArchiveStartedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// タイムシフト有効終了時間
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public DateTimeOffset TimeshiftArchiveEndedAt { get { return this._TimeshiftArchiveEndedAt; } }
		private DateTimeOffset _TimeshiftArchiveEndedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "_ts_archive_end_time" )]
		private string TimeshiftArchiveEndedAtImpl
		{
			get { return this._TimeshiftArchiveEndedAt.ToString(); }
			set { this._TimeshiftArchiveEndedAt = value.ToDateTimeOffsetFromIso8601(); }
		}

		/// <summary>
		/// タイムシフトの視聴回数上限 (?)
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		[DataMember( Name = "_ts_view_limit_num" )]
		public ushort TimeshiftViewLimitCount { get; private set; }

		/// <summary>
		/// タイムシフト視聴期間に制限がないか
		/// </summary>
		/// <remarks>詳細モード時のみ存在します</remarks>
		public bool IsTimeshiftEndless { get; private set; }

		[DataMember( Name = "_ts_is_endless" )]
		private string IsTimeshiftEndlessImpl
		{
			get { return this.IsTimeshiftEndless.ToString1Or0(); }
			set { this.IsTimeshiftEndless = value.ToBooleanFrom1(); }
		}

		/// <summary>
		/// タイムシフト予約数
		/// </summary>
		[DataMember( Name = "_ts_reserved_count", IsRequired = true )]
		public uint TimeshiftReservedCount { get; private set; }

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		[DataMember( Name = "timeshift_enabled", IsRequired = true )]
		public ushort TimeshiftEnabled { get; private set; }

		/// <summary>
		/// 高画質放送か
		/// </summary>
		public bool IsHighQuality { get; private set; }

		[DataMember( Name = "is_hq", IsRequired = true )]
		private string IsHighQualityImpl
		{
			get { return this.IsHighQuality.ToString1Or0(); }
			set { this.IsHighQuality = value.ToBooleanFrom1(); }
		}
	}
}