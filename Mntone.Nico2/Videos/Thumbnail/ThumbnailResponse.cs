using System;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// サムネール情報
	/// </summary>
	public sealed class ThumbnailResponse
	{
		internal ThumbnailResponse( IXmlNode thumbXml )
		{
			ID = thumbXml.GetNamedChildNode( "video_id" ).InnerText;
			Title = thumbXml.GetNamedChildNode( "title" ).InnerText;
			Description = thumbXml.GetNamedChildNode( "description" ).InnerText;
			ThumbnailUrl = thumbXml.GetNamedChildNode( "thumbnail_url" ).InnerText.ToUri();
			PostedAt = thumbXml.GetNamedChildNode( "first_retrieve" ).InnerText.ToDateTimeOffsetFromIso8601();
			Length = thumbXml.GetNamedChildNode( "length" ).InnerText.ToTimeSpan();
			MovieType = thumbXml.GetNamedChildNode( "movie_type" ).InnerText.ToMovieType();
			SizeHigh = thumbXml.GetNamedChildNode( "size_high" ).InnerText.ToULong();
			SizeLow = thumbXml.GetNamedChildNode( "size_low" ).InnerText.ToULong();
			ViewCount = thumbXml.GetNamedChildNode( "view_counter" ).InnerText.ToUInt();
			CommentCount = thumbXml.GetNamedChildNode( "comment_num" ).InnerText.ToUInt();
			MylistCount = thumbXml.GetNamedChildNode( "mylist_counter" ).InnerText.ToUInt();
			LastCommentBody = thumbXml.GetNamedChildNode( "last_res_body" ).InnerText;
			PageUrl = thumbXml.GetNamedChildNode( "watch_url" ).InnerText.ToUri();
			ThumbnailType = thumbXml.GetNamedChildNode( "thumb_type" ).InnerText.ToThumbnailType();
			Embeddable = thumbXml.GetNamedChildNode( "embeddable" ).InnerText.ToBooleanFrom1();
			NoLivePlay = thumbXml.GetNamedChildNode( "no_live_play" ).InnerText.ToBooleanFrom1();

			Tags = new Tags( thumbXml.GetNamedChildNode( "tags" ) );

			var userIDXml = thumbXml.ChildNodes.Where( node => node.NodeName == "user_id" ).SingleOrDefault();
			if( userIDXml != null )
			{
				UserType = UserType.User;
				UserId = userIDXml.InnerText.ToULong();
				UserName = thumbXml.GetNamedChildNode( "user_nickname" ).InnerText;
				UserIconUrl = thumbXml.GetNamedChildNode( "user_icon_url" ).InnerText.ToUri();
				return;
			}

			var chIDXml = thumbXml.ChildNodes.Where( node => node.NodeName == "ch_id" ).SingleOrDefault();
			if( chIDXml != null )
			{
				UserType = UserType.Channel;
				UserId = chIDXml.InnerText.ToULong();
				UserName = thumbXml.GetNamedChildNode( "ch_name" ).InnerText;
				UserIconUrl = thumbXml.GetNamedChildNode( "ch_icon_url" ).InnerText.ToUri();
				return;
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// サムネールの URL
		/// </summary>
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get; private set; }

		/// <summary>
		/// 動画の種類
		/// </summary>
		public MovieType MovieType { get; private set; }

		/// <summary>
		/// 高画質動画のサイズ
		/// </summary>
		public ulong SizeHigh { get; private set; }

		/// <summary>
		/// 低画質動画のサイズ
		/// </summary>
		public ulong SizeLow { get; private set; }

		/// <summary>
		/// 閲覧数
		/// </summary>
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// マイリスト数
		/// </summary>
		public uint MylistCount { get; private set; }

		/// <summary>
		/// 最新コメントの一部
		/// </summary>
		public string LastCommentBody { get; private set; }

		/// <summary>
		/// 視聴ページ URL
		/// </summary>
		public Uri PageUrl { get; private set; }

		/// <summary>
		/// サムネール情報の種類
		/// </summary>
		public ThumbnailType ThumbnailType { get; private set; }

		/// <summary>
		/// 埋め込みが可能か
		/// </summary>
		public bool Embeddable { get; private set; }

		/// <summary>
		/// 生放送で再生不可能か
		/// </summary>
		public bool NoLivePlay { get; private set; }

		/// <summary>
		/// タグ情報
		/// </summary>
		public Tags Tags { get; private set; }

		/// <summary>
		/// ユーザーの種類
		/// </summary>
		public UserType UserType { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public ulong UserId { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// ユーザー アイコン URL
		/// </summary>
		public Uri UserIconUrl { get; private set; }
	}
}