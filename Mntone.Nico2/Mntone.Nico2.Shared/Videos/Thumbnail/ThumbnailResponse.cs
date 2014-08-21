using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// サムネール情報
	/// </summary>
	public sealed class ThumbnailResponse
	{
#if WINDOWS_APP
		internal ThumbnailResponse( IXmlNode thumbXml )
#else
		internal ThumbnailResponse( XElement thumbXml )
#endif
		{
			Id = thumbXml.GetNamedChildNodeText( "video_id" );
			Title = thumbXml.GetNamedChildNodeText( "title" );
			Description = thumbXml.GetNamedChildNodeText( "description" );
			ThumbnailUrl = thumbXml.GetNamedChildNodeText( "thumbnail_url" ).ToUri();
			PostedAt = thumbXml.GetNamedChildNodeText( "first_retrieve" ).ToDateTimeOffsetFromIso8601();
			Length = thumbXml.GetNamedChildNodeText( "length" ).ToTimeSpan();
			MovieType = thumbXml.GetNamedChildNodeText( "movie_type" ).ToMovieType();
			SizeHigh = thumbXml.GetNamedChildNodeText( "size_high" ).ToULong();
			SizeLow = thumbXml.GetNamedChildNodeText( "size_low" ).ToULong();
			ViewCount = thumbXml.GetNamedChildNodeText( "view_counter" ).ToUInt();
			CommentCount = thumbXml.GetNamedChildNodeText( "comment_num" ).ToUInt();
			MylistCount = thumbXml.GetNamedChildNodeText( "mylist_counter" ).ToUInt();
			LastCommentBody = thumbXml.GetNamedChildNodeText( "last_res_body" );
			PageUrl = thumbXml.GetNamedChildNodeText( "watch_url" ).ToUri();
			ThumbnailType = thumbXml.GetNamedChildNodeText( "thumb_type" ).ToThumbnailType();
			IsEmbeddable = thumbXml.GetNamedChildNodeText( "embeddable" ).ToBooleanFrom1();
			CannotPlayInLive = thumbXml.GetNamedChildNodeText( "no_live_play" ).ToBooleanFrom1();

			Tags = new Tags( thumbXml.GetNamedChildNode( "tags" ) );

			var userIdXml = thumbXml.GetNamedChildNode( "user_id" );
			if( userIdXml != null )
			{
				UserType = UserType.User;
				UserId = userIdXml.GetText().ToUInt();
				UserName = thumbXml.GetNamedChildNodeText( "user_nickname" );
				UserIconUrl = thumbXml.GetNamedChildNodeText( "user_icon_url" ).ToUri();
				return;
			}

			var chIdXml = thumbXml.GetNamedChildNode( "ch_id" );
			if( chIdXml != null )
			{
				UserType = UserType.Channel;
				UserId = chIdXml.GetText().ToUInt();
				UserName = thumbXml.GetNamedChildNodeText( "ch_name" );
				UserIconUrl = thumbXml.GetNamedChildNodeText( "ch_icon_url" ).ToUri();
				return;
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

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
		public bool IsEmbeddable { get; private set; }

		/// <summary>
		/// 生放送で再生不可能か
		/// </summary>
		public bool CannotPlayInLive { get; private set; }

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
		public uint UserId { get; private set; }

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