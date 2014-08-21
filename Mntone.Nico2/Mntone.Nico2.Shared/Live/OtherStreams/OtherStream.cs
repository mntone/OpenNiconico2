using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Live.OtherStreams
{
	/// <summary>
	/// 放送中の番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OtherStream
	{
		internal OtherStream()
		{ }

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get { return this._Id; } }
		private string _Id = string.Empty;

		[DataMember( Name = "id" )]
		private string IdImpl
		{
			get { return this._Id != null && this._Id.Length > 2 ? this._Id.Substring( 2 ) : null; }
			set { this._Id = "lv" + value; }
		}

		/// <summary>
		/// 番組の状態
		/// </summary>
		public StatusType Status { get { return this._Status; } }
		private StatusType _Status;

		[DataMember( Name = "currentstatus" )]
		private string StatusImpl
		{
			get { return this._Status.ToStatusTypeString(); }
			set { this._Status = value.ToStatusType(); }
		}

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		[DataMember( Name = "description" )]
		public string Description { get; private set; }

		/// <summary>
		/// リストに表示しないか
		/// </summary>
		[DataMember( Name = "is_exclude_non_display" )]
		public bool IsHidden { get; private set; }

		/// <summary>
		/// private か (?)
		/// </summary>
		[DataMember( Name = "is_exclude_private" )]
		public bool IsPrivate { get; private set; }

		/// <summary>
		/// 製品番組か
		/// </summary>
		[DataMember( Name = "is_product" )]
		public bool IsProduct { get; private set; }

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		public ushort TimeshiftEnabled { get { return this._TimeshiftEnabled; } }
		private ushort _TimeshiftEnabled = 0;

		[DataMember( Name = "timeshift_enabled" )]
		private object TimeshiftEnabledImpl
		{
			get { return this._TimeshiftEnabled; }
			set
			{
				if( value is bool )
				{
					this._TimeshiftEnabled = ( ushort )( ( bool )value ? 1 : 0 );
				}
				else if( value is int )
				{
					this._TimeshiftEnabled = ( ushort )( ( int )value );
				}
			}
		}

		/// <summary>
		/// タイムシフト期間が終了しているか
		/// </summary>
		[DataMember( Name = "is_timeshift_already_closed" )]
		public bool IsTimeshiftClosed { get; private set; }

		/// <summary>
		/// タイムシフトが準備中か
		/// </summary>
		[DataMember( Name = "is_timeshift_preparing" )]
		public bool IsTimeshiftPreparing { get; private set; }

		/// <summary>
		/// サムネール URL
		/// </summary>
		[DataMember( Name = "picture_url" )]
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// チケット購入ページ URL
		/// </summary>
		[DataMember( Name = "ticket_url" )]
		public Uri TicketPageUrl { get; private set; }

		/// <summary>
		/// Twitter が無効か
		/// </summary>
		[DataMember( Name = "twitter_disabled" )]
		public bool IsTwitterDisabled { get; private set; }

		/// <summary>
		/// Twitter のハッシュタグ
		/// </summary>
		[DataMember( Name = "twitter_tag" )]
		public string TwitterHashtag { get; set; }

		/// <summary>
		/// 来場者数
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
		[DataMember( Name = "timeshift_reserved_count" )]
		public uint TimeshiftReservedCount { get; private set; }

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get { return this._StartedAt; } }
		private DateTimeOffset _StartedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "start_date_timestamp_sec" )]
		private long StartedAtImpl
		{
			get { return this._StartedAt.ToLongFromDateTimeOffset(); }
			set { this._StartedAt = value.ToDateTimeOffsetFromUnixTime(); }
		}

		/// <summary>
		/// 終了日時
		/// </summary>
		public DateTimeOffset EndedAt { get { return this._EndedAt; } }
		private DateTimeOffset _EndedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "end_date_timestamp_sec" )]
		private long EndedAtImpl
		{
			get { return this._EndedAt.ToLongFromDateTimeOffset(); }
			set { this._EndedAt = value.ToDateTimeOffsetFromUnixTime(); }
		}

		/// <summary>
		/// 公式配信か
		/// </summary>
		public bool IsOfficial { get { return this.CommunityType == CommunityType.Official; } }

		/// <summary>
		/// チャンネル配信か
		/// </summary>
		public bool IsChannel { get { return this.CommunityType == CommunityType.Channel; } }

		/// <summary>
		/// コミュニティー配信か
		/// </summary>
		public bool IsCommunity { get { return this.CommunityType == CommunityType.Community; } }

		/// <summary>
		/// コミュニティーの種類
		/// </summary>
		public CommunityType CommunityType { get { return this._CommunityType; } }
		private CommunityType _CommunityType;

		[DataMember( Name = "provider_type" )]
		private string CommunityTypeImpl
		{
			get { return string.Empty; }
			set { this._CommunityType = value.ToCommunityType(); }
		}

		/// <summary>
		/// Twitter が無効か
		/// </summary>
		[DataMember( Name = "view_channel_icon" )]
		public bool IsChannelIconEnabled { get; private set; }

#if DEBUG
		/// <summary>
		/// 合計時間表示テンプレート
		/// </summary>
		[DataMember( Name = "closed_total_template" )]
		public string ClosedTotalTemplate { get; set; }
#endif
	}
}