using System;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
		internal Program( IXmlNode streamXml, IXmlNode playerXml, IXmlNode nsenXml, ProgramTwitter programTwitter )
		{
			ID = streamXml.GetNamedChildNode( "id" ).InnerText;
			Title = streamXml.GetNamedChildNode( "title" ).InnerText;
			Description = streamXml.GetNamedChildNode( "description" ).InnerText;

			WatchCount = streamXml.GetNamedChildNode( "watch_count" ).InnerText.ToUInt();
			CommentCount = streamXml.GetNamedChildNode( "comment_count" ).InnerText.ToUInt();

			CommunityType = streamXml.GetNamedChildNode( "provider_type" ).InnerText.ToProviderType();
			CommunityID = streamXml.GetNamedChildNode( "default_community" ).InnerText;
			BroadcasterID = streamXml.GetNamedChildNode( "owner_id" ).InnerText.ToUInt();
			BroadcasterName = streamXml.GetNamedChildNode( "owner_name" ).InnerText;

			International = streamXml.GetNamedChildNode( "international" ).InnerText.ToUShort();

			BaseAt = streamXml.GetNamedChildNode( "base_time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			OpenedAt = streamXml.GetNamedChildNode( "open_time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			StartedAt = streamXml.GetNamedChildNode( "start_time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			EndedAt = streamXml.GetNamedChildNode( "end_time" ).InnerText.ToDateTimeOffsetFromUnixTime();

			var timeshiftTimeXml = streamXml.ChildNodes.Where( node => node.NodeName == "timeshift_time" ).SingleOrDefault();
			if( timeshiftTimeXml != null )
			{
				TimeshiftAt = timeshiftTimeXml.InnerText.ToDateTimeOffsetFromUnixTime();
			}

#if DEBUG
			BourbonUrl = streamXml.GetNamedChildNode( "bourbon_url" ).InnerText.ToUri();
#endif
			CrowdedUrl = streamXml.GetNamedChildNode( "full_video" ).InnerText.ToUri();
#if DEBUG
			AfterUrl = streamXml.GetNamedChildNode( "after_video" ).InnerText.ToUri();
			BeforeUrl = streamXml.GetNamedChildNode( "before_video" ).InnerText.ToUri();
#endif
			KickOutUrl = streamXml.GetNamedChildNode( "kickout_video" ).InnerText.ToUri();
			KickOutImageUrl = playerXml.GetNamedChildNode( "dialog_image" ).GetNamedChildNode( "oidashi" ).InnerText.ToUri();

			var pictureUrlXml = streamXml.ChildNodes.Where( node => node.NodeName == "picture_url" ).SingleOrDefault();
			if( pictureUrlXml != null )
			{
				CommunityImageUrl = pictureUrlXml.InnerText.ToUri();
				CommunitySmallImageUrl = streamXml.GetNamedChildNode( "thumb_url" ).InnerText.ToUri();
			}

			var productTicketUrlXml = streamXml.ChildNodes.Where( node => node.NodeName == "product_ticket_url" ).SingleOrDefault();
			if( productTicketUrlXml != null )
			{
				TicketUrl = productTicketUrlXml.InnerText.ToUri();
			}
			var productBannerUrlXml = streamXml.ChildNodes.Where( node => node.NodeName == "product_banner_url" ).SingleOrDefault();
			if( productBannerUrlXml != null )
			{
				BannerUrl = productBannerUrlXml.InnerText.ToUri();
			}
			var shutterUrlXml = streamXml.ChildNodes.Where( node => node.NodeName == "shutter_url" ).SingleOrDefault();
			if( shutterUrlXml != null )
			{
				ShutterUrl = shutterUrlXml.InnerText.ToUri();
			}

			IsRerun = streamXml.GetNamedChildNode( "is_rerun_stream" ).InnerText.ToBooleanFrom1();
			IsArchive = streamXml.GetNamedChildNode( "archive" ).InnerText.ToBooleanFrom1();

			var isDJStreamXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_dj_stream" ).SingleOrDefault();
			if( isDJStreamXml != null && isDJStreamXml.InnerText.ToBooleanFrom1() )
			{
				ExtendedType = ProgramExtendedType.NewComer;

			}
			else
			{
				var isCruiseStreamXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_cruise_stream" ).SingleOrDefault();
				if( isCruiseStreamXml != null && !string.IsNullOrEmpty( isCruiseStreamXml.InnerText ) )
				{
					ExtendedType = ProgramExtendedType.Cruise;
				}
				else if( nsenXml != null && nsenXml.GetNamedChildNode( "is_ns_stream" ).InnerText.ToBooleanFrom1() )
				{
					ExtendedType = ProgramExtendedType.Nsen;
					NsenType = nsenXml.GetNamedChildNode( "nstype" ).InnerText;
					NsenCommand = nsenXml.GetNamedChildNode( "nspanel" ).InnerText;
				}
				else
				{
					ExtendedType = ProgramExtendedType.None;
				}
			}

			var isHQStreamXml = streamXml.ChildNodes.Where( node => node.NodeName == "hqstream" ).SingleOrDefault();
			IsHighQuality = isHQStreamXml != null ? isHQStreamXml.InnerText.ToBooleanFrom1() : false;

			IsInfinity = streamXml.GetNamedChildNode( "infinity_mode" ).InnerText.ToBooleanFrom1();
			IsReserved = streamXml.GetNamedChildNode( "is_reserved" ).InnerText.ToBooleanFrom1();

			var isArchivePlayServerXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_archiveplayserver" ).SingleOrDefault();
			IsArchivePlayServer = isArchivePlayServerXml != null ? isArchivePlayServerXml.InnerText.ToBooleanFrom1() : false;

			IsTimeshiftEnabled = streamXml.GetNamedChildNode( "is_nonarchive_timeshift_enabled" ).InnerText.ToBooleanFrom1();

			var isProductStreamXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_product_stream" ).SingleOrDefault();
			if( isProductStreamXml != null )
			{
				IsProductEnabled = isProductStreamXml.InnerText.ToBooleanFrom1();
				IsTrialEnabled = streamXml.GetNamedChildNode( "is_trial" ).InnerText.ToBooleanFrom1();
				IsBannerForced = streamXml.GetNamedChildNode( "product_fixed_banner" ).InnerText.ToBooleanFrom1();
			}

			IsNoticeBalloonEnabled = playerXml.GetNamedChildNode( "is_notice_viewer_balloon_enabled" ).InnerText.ToBooleanFrom1();
			IsErrorReportEnabled = playerXml.GetNamedChildNode( "error_report" ).InnerText.ToBooleanFrom1();

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