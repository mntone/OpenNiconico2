using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mntone.Nico2.Live.MyPage
{
	/// <summary>
	/// 番組のデータを格納するクラス
	/// </summary>
	public sealed class ProgramInfo
	{
		const string FACE_OUT_IMAGE_URL = "img/10/cmn/icon/icon_face.gif?090827";
		const string ENCOUNTER_IMAGE_URL = "img/10/cmn/icon/icon_totsu.gif?090827";
		const string CRUISE_IMAGE_URL = "img/10/cmn/icon/icon_cruise.gif?110621";
		readonly Regex communityImageSrcRegex = new Regex( @"^http://icon\.nimg\.jp/(?:community/s/\d{1,12}/|channel/s/)(" + NiconicoRegex.CommunityIdRegexBase + "|" + NiconicoRegex.ChannelIdRegexBase + @")\.jpg(?:\?\d+)?$" );

		internal ProgramInfo( HtmlNode liveItemHtml, string language, bool isReserved = false )
		{
			this.CommunityType = liveItemHtml.GetAttributeValue( "class", string.Empty ).Split( new char[] { ' ' } ).Any( c => c == "liveItem_ch" )
				? CommunityType.Channel
				: CommunityType.Community;

			{
				var communityImageSrc = liveItemHtml.GetElementByTagName( "a" ).GetElementByTagName( "img" ).GetAttributeValue( "src", string.Empty );
				var match = communityImageSrcRegex.Match( communityImageSrc );
				if( match.Groups.Count >= 2 )
				{
					this.CommunityID = match.Groups[1].Value;
				}
			}

			var liveItemTextHtml = liveItemHtml.GetElementByClassName( "liveItemTxt" );
			var startTimeText = liveItemTextHtml.GetElementByClassName( "start_time" ).GetElementByTagName( "strong" ).InnerText.Trim();
			if( isReserved )
			{
				if( language == "en-us" )
				{
					// Nov 05 (WED) Opens 21:27 Starts 21:30
					var openedAt = DateTimeOffset.ParseExact( startTimeText.Substring( 0, 24 ), "MMM dd (ddd') Opens 'HH:mm", new CultureInfo( "en-us" ), DateTimeStyles.AssumeUniversal );
					openedAt = openedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.OpenedAt = openedAt;

					var startedAt = DateTimeOffset.ParseExact( startTimeText.Substring( 0, 13 ) + startTimeText.Substring( 26 ), "MMM dd (ddd') Starts 'HH:mm", new CultureInfo( "en-us" ), DateTimeStyles.AssumeUniversal );
					startedAt = startedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.StartedAt = startedAt;
				}
				else if( language == "zh-tw" )
				{
					// 11/05(三) 進場 21:27 開場 21:30
					var baseTimeText = startTimeText.Substring( 0, 6 ) + "週" + startTimeText.Substring( 6, 3 );

					var openedAt = DateTimeOffset.ParseExact( baseTimeText + startTimeText.Substring( 9, 8 ), "MM/dd(ddd') 進場 'HH:mm", new CultureInfo( "zh-tw" ), DateTimeStyles.AssumeUniversal );
					openedAt = openedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.OpenedAt = openedAt;

					var startedAt = DateTimeOffset.ParseExact( baseTimeText + startTimeText.Substring( 18 ), "MM/dd(ddd') 開場 'HH:mm", new CultureInfo( "zh-tw" ), DateTimeStyles.AssumeUniversal );
					startedAt = startedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.StartedAt = startedAt;
				}
				else
				{
					// 11/05(水) 開場 21:27 開演 21:30
					var openedAt = DateTimeOffset.ParseExact( startTimeText.Substring( 0, 17 ), "MM/dd(ddd') 開場 'HH:mm", new CultureInfo( "ja-jp" ), DateTimeStyles.AssumeUniversal );
					openedAt = openedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.OpenedAt = openedAt;

					var startedAt = DateTimeOffset.ParseExact( startTimeText.Substring( 0, 9 ) + startTimeText.Substring( 18 ), "MM/dd(ddd') 開演 'HH:mm", new CultureInfo( "ja-jp" ), DateTimeStyles.AssumeUniversal );
					startedAt = startedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.StartedAt = startedAt;
				}
			}
			else
			{
				if( language == "en-us" )
				{
					// Starts: 11/05(WED) 21:27
					var startedAt = DateTimeOffset.ParseExact( startTimeText, "'Starts: 'MM/dd(ddd) HH:mm", new CultureInfo( "en-us" ), DateTimeStyles.AssumeUniversal );
					startedAt = startedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.OpenedAt = this.StartedAt = startedAt;
				}
				else
				{
					// zh-tw: 11/05(三) 21:27 開始
					// ja-jp: 11/05(水) 21:27 開始
					if( language == "zh-tw" )
					{
						startTimeText = startTimeText.Substring( 0, 6 ) + "週" + startTimeText.Substring( 6 );
					}
					var startedAt = DateTimeOffset.ParseExact( startTimeText, "MM/dd(ddd) HH:mm 開始", new CultureInfo( "ja-jp" ), DateTimeStyles.AssumeUniversal );
					startedAt = startedAt.Subtract( TimeSpan.FromHours( 9 ) );
					this.OpenedAt = this.StartedAt = startedAt;
				}
			}

			var h3Html = liveItemTextHtml.GetElementByTagName( "h3" );
			if( this.IsCommunity )
			{
				var imgHtml = h3Html.GetElementsByTagName( "img" ).Select( img => img.GetAttributeValue( "src", string.Empty ) );
				this.Category = imgHtml.First().ToDetailCategory();
				foreach( var imgSrc in imgHtml.Skip( 1 ) )
				{
					switch( imgSrc )
					{
					case FACE_OUT_IMAGE_URL:
						this.IsFaceOut = true;
						break;
					case ENCOUNTER_IMAGE_URL:
						this.IsEnconter = true;
						break;
					case CRUISE_IMAGE_URL:
						this.IsCruise = true;
						break;
					}
				}
			}
			var aHtml = h3Html.GetElementByTagName( "a" );
			this.Title = aHtml.GetAttributeValue( "title", string.Empty );

			var href = aHtml.GetAttributeValue( "href", string.Empty );
			var count = this.IsCommunity ? 31 : 30;
			var indexOf = href.IndexOf( '?' );
			if( indexOf >= 0 )
			{
				this.ID = href.Substring( count, indexOf - count );
			}
			else
			{
				this.ID = href.Substring( count );
			}

			this.CommunityName = liveItemTextHtml.GetElementsByTagName( "p" ).Last().GetAttributeValue( "title", string.Empty );
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
		public CommunityType CommunityType { get; internal set; }

		/// <summary>
		/// カテゴリー
		/// </summary>
		public DetailCategoryType Category { get; private set; }

		/// <summary>
		/// 顔出し放送か
		/// </summary>
		public bool IsFaceOut { get; private set; }

		/// <summary>
		/// 凸待ち放送か
		/// </summary>
		public bool IsEnconter { get; private set; }

		/// <summary>
		/// クルーズ待ち放送か
		/// </summary>
		public bool IsCruise { get; private set; }

		/// <summary>
		/// 開場時間
		/// </summary>
		public DateTimeOffset OpenedAt { get; private set; }

		/// <summary>
		/// 開始時間
		/// </summary>
		public DateTimeOffset StartedAt { get; private set; }

		/// <summary>
		/// 番組名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 番組 ID
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		/// コミュニティー (チャンネル) 名
		/// </summary>
		public string CommunityName { get; private set; }

		/// <summary>
		/// コミュニティー (チャンネル) ID
		/// </summary>
		public string CommunityID { get; private set; }
	}
}