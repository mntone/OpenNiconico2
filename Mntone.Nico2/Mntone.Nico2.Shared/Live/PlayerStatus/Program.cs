using System;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
#if WINDOWS_APP
		internal Program( IXmlNode streamXml, IXmlNode playerXml, IXmlNode nsenXml, ProgramTwitter programTwitter )
#else
		internal Program( XElement streamXml, XElement playerXml, XElement nsenXml, ProgramTwitter programTwitter )
#endif
		{
			ID = streamXml.GetNamedChildNodeText( "id" );
			Title = streamXml.GetNamedChildNodeText( "title" );
			Description = streamXml.GetNamedChildNodeText( "description" );

			WatchCount = streamXml.GetNamedChildNodeText( "watch_count" ).ToUInt();
			CommentCount = streamXml.GetNamedChildNodeText( "comment_count" ).ToUInt();

			CommunityType = streamXml.GetNamedChildNodeText( "provider_type" ).ToCommunityType();
			CommunityID = streamXml.GetNamedChildNodeText( "default_community" );
			BroadcasterID = streamXml.GetNamedChildNodeText( "owner_id" ).ToUInt();
			BroadcasterName = streamXml.GetNamedChildNodeText( "owner_name" );

			International = streamXml.GetNamedChildNodeText( "international" ).ToUShort();

			BaseAt = streamXml.GetNamedChildNodeText( "base_time" ).ToDateTimeOffsetFromUnixTime();
			OpenedAt = streamXml.GetNamedChildNodeText( "open_time" ).ToDateTimeOffsetFromUnixTime();
			StartedAt = streamXml.GetNamedChildNodeText( "start_time" ).ToDateTimeOffsetFromUnixTime();
			EndedAt = streamXml.GetNamedChildNodeText( "end_time" ).ToDateTimeOffsetFromUnixTime();

			var timeshiftTimeXml = streamXml.GetNamedChildNodeText( "timeshift_time" );
			if( !string.IsNullOrEmpty( timeshiftTimeXml ) )
			{
				TimeshiftAt = timeshiftTimeXml.ToDateTimeOffsetFromUnixTime();
			}

#if DEBUG
			BourbonUrl = streamXml.GetNamedChildNodeText( "bourbon_url" ).ToUri();
#endif
			CrowdedUrl = streamXml.GetNamedChildNodeText( "full_video" ).ToUri();
#if DEBUG
			AfterUrl = streamXml.GetNamedChildNodeText( "after_video" ).ToUri();
			BeforeUrl = streamXml.GetNamedChildNodeText( "before_video" ).ToUri();
#endif
			KickOutUrl = streamXml.GetNamedChildNodeText( "kickout_video" ).ToUri();
			KickOutImageUrl = playerXml.GetNamedChildNode( "dialog_image" ).GetNamedChildNodeText( "oidashi" ).ToUri();

			var pictureUrl = streamXml.GetNamedChildNodeText( "picture_url" ).ToUri();
			if( pictureUrl != null )
			{
				CommunityImageUrl = pictureUrl;
				CommunitySmallImageUrl = streamXml.GetNamedChildNodeText( "thumb_url" ).ToUri();
			}

			TicketUrl = streamXml.GetNamedChildNodeText( "product_ticket_url" ).ToUri();
			BannerUrl = streamXml.GetNamedChildNodeText( "product_banner_url" ).ToUri();
			ShutterUrl = streamXml.GetNamedChildNodeText( "shutter_url" ).ToUri();
			IsRerun = streamXml.GetNamedChildNodeText( "is_rerun_stream" ).ToBooleanFrom1();
			IsArchive = streamXml.GetNamedChildNodeText( "archive" ).ToBooleanFrom1();

			var isDJStream = streamXml.GetNamedChildNodeText( "is_dj_stream" );
			if( isDJStream.ToBooleanFrom1() )
			{
				ExtendedType = ProgramExtendedType.NewComer;
				NsenType = string.Empty;
				NsenCommand = string.Empty;
			}
			else
			{
				var isCruiseStream = streamXml.GetNamedChildNodeText( "is_cruise_stream" ).ToBooleanFrom1();
				if( isCruiseStream )
				{
					ExtendedType = ProgramExtendedType.Cruise;
					NsenType = string.Empty;
					NsenCommand = string.Empty;
				}
				else if( nsenXml != null && nsenXml.GetNamedChildNodeText( "is_ns_stream" ).ToBooleanFrom1() )
				{
					ExtendedType = ProgramExtendedType.Nsen;
					NsenType = nsenXml.GetNamedChildNodeText( "nstype" );
					NsenCommand = nsenXml.GetNamedChildNodeText( "nspanel" );
				}
				else
				{
					ExtendedType = ProgramExtendedType.None;
					NsenType = string.Empty;
					NsenCommand = string.Empty;
				}
			}

			IsHighQuality = streamXml.GetNamedChildNodeText( "hqstream" ).ToBooleanFrom1();
			IsInfinity = streamXml.GetNamedChildNodeText( "infinity_mode" ).ToBooleanFrom1();
			IsReserved = streamXml.GetNamedChildNodeText( "is_reserved" ).ToBooleanFrom1();
			IsArchivePlayServer = streamXml.GetNamedChildNodeText( "is_archiveplayserver" ).ToBooleanFrom1();
			IsTimeshiftEnabled = streamXml.GetNamedChildNodeText( "is_nonarchive_timeshift_enabled" ).ToBooleanFrom1();

			var isProductStreamText = streamXml.GetNamedChildNodeText( "is_product_stream" );
			if( isProductStreamText != null )
			{
				IsProductEnabled = isProductStreamText.ToBooleanFrom1();
				IsTrialEnabled = streamXml.GetNamedChildNodeText( "is_trial" ).ToBooleanFrom1();
				IsBannerForced = streamXml.GetNamedChildNodeText( "product_fixed_banner" ).ToBooleanFrom1();
			}

			IsNoticeBalloonEnabled = playerXml.GetNamedChildNodeText( "is_notice_viewer_balloon_enabled" ).ToBooleanFrom1();
			IsErrorReportEnabled = playerXml.GetNamedChildNodeText( "error_report" ).ToBooleanFrom1();

			Twitter = programTwitter;
		}

		#region Description

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明文
		/// </summary>
		public string Description { get; private set; }


		/// <summary>
		/// 来場者数
		/// </summary>
		public uint WatchCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }


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

		/// <summary>
		/// コミュニティーの ID
		/// </summary>
		public string CommunityID { get; private set; }

		/// <summary>
		/// 配信者 ID
		/// </summary>
		public uint BroadcasterID { get; private set; }

		/// <summary>
		/// 配信者名
		/// </summary>
		public string BroadcasterName { get; private set; }


		/// <summary>
		/// 国際化定義
		/// </summary>
		public ushort International { get; private set; }

		#endregion

		#region Time

		/// <summary>
		/// 基準日時
		/// </summary>
		public DateTimeOffset BaseAt { get; private set; }

		/// <summary>
		/// 開場日時
		/// </summary>
		public DateTimeOffset OpenedAt { get; private set; }

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get; private set; }

		/// <summary>
		/// 終了日時
		/// </summary>
		public DateTimeOffset EndedAt { get; private set; }

		/// <summary>
		/// タイムシフト日時
		/// </summary>
		public DateTimeOffset TimeshiftAt { get; private set; }

		#endregion

		#region Url

#if DEBUG
		/// <summary>
		/// (?)
		/// </summary>
		public Uri BourbonUrl { get; private set; }
#endif

		/// <summary>
		/// 満員のときに飛ぶ URL
		/// </summary>
		public Uri CrowdedUrl { get; private set; }

#if DEBUG
		/// <summary>
		/// (?)
		/// </summary>
		/// <remarks>
		/// もはや使われていない模様
		/// </remarks>
		public Uri AfterUrl { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		/// <remarks>
		/// もはや使われていない模様
		/// </remarks>
		public Uri BeforeUrl { get; private set; }
#endif

		/// <summary>
		/// 追い出されたときに飛ぶ URL
		/// </summary>
		public Uri KickOutUrl { get; private set; }

		/// <summary>
		/// 追い出し用画像 URL
		/// </summary>
		public Uri KickOutImageUrl { get; private set; }

		/// <summary>
		/// コミュニティー画像 URL
		/// </summary>
		public Uri CommunityImageUrl { get; private set; }

		/// <summary>
		/// コミュニティーの小さい画像 URL
		/// </summary>
		public Uri CommunitySmallImageUrl { get; private set; }

		/// <summary>
		/// チケット URL
		/// </summary>
		public Uri TicketUrl { get; private set; }

		/// <summary>
		/// バナー URL
		/// </summary>
		public Uri BannerUrl { get; private set; }

		/// <summary>
		/// シャッター URL
		/// </summary>
		public Uri ShutterUrl { get; private set; }

		#endregion

		/// <summary>
		/// 再放送か
		/// </summary>
		public bool IsRerun { get; private set; }

		/// <summary>
		/// タイムシフトか
		/// </summary>
		public bool IsArchive { get; private set; }

		/// <summary>
		/// 生放送か
		/// </summary>
		/// <remarks>
		/// 再放送も生放送として扱う
		/// </remarks>
		public bool IsLive { get { return !IsArchive; } }

		/// <summary>
		/// 新着動画か
		/// </summary>
		public bool IsNewComer { get { return ExtendedType == ProgramExtendedType.NewComer; } }

		/// <summary>
		/// クルーズか
		/// </summary>
		public bool IsCruise { get { return ExtendedType == ProgramExtendedType.Cruise; } }

		/// <summary>
		/// Nsen か
		/// </summary>
		public bool IsNsen { get { return ExtendedType == ProgramExtendedType.Nsen; } }

		/// <summary>
		/// 番組の拡張タイプ
		/// </summary>
		public ProgramExtendedType ExtendedType { get; private set; }

		/// <summary>
		/// 高画質配信か
		/// </summary>
		public bool IsHighQuality { get; private set; }

		/// <summary>
		/// 時間制限なしの放送か
		/// </summary>
		public bool IsInfinity { get; private set; }

		/// <summary>
		/// 予約番組か
		/// </summary>
		public bool IsReserved { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		public bool IsArchivePlayServer { get; private set; }

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		public bool IsTimeshiftEnabled { get; private set; }

		/// <summary>
		/// 製品番組か
		/// </summary>
		public bool IsProductEnabled { get; private set; }

		/// <summary>
		/// 仕様が有効か
		/// </summary>
		public bool IsTrialEnabled { get; private set; }

		/// <summary>
		/// バナー強制 (?)。
		/// stream/product_fixed_banner
		/// </summary>
		public bool IsBannerForced { get; private set; }

		/// <summary>
		/// 注意バルーンが有効か
		/// </summary>
		public bool IsNoticeBalloonEnabled { get; private set; }

		/// <summary>
		/// エラー報告が有効か
		/// </summary>
		public bool IsErrorReportEnabled { get; private set; }

		#region Nsen

		/// <summary>
		/// Nsen のカテゴリー
		/// </summary>
		public string NsenType { get; private set; }

		/// <summary>
		/// Nsen の現在のコマンド
		/// </summary>
		public string NsenCommand { get; private set; }

		#endregion

		/// <summary>
		/// Twitter 情報
		/// </summary>
		public ProgramTwitter Twitter { get; private set; }
	}
}