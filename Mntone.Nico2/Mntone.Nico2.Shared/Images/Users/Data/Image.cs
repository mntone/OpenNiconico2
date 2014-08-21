using Mntone.Nico2.Images.Illusts;
using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Users.Data
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Image
	{
#if WINDOWS_APP
		internal Image( IXmlNode imageXml )
#else
		internal Image( XElement imageXml )
#endif
		{
			Id = "im" + imageXml.GetNamedChildNodeText( "id" );
			UserId = imageXml.GetNamedChildNodeText( "user_id" ).ToUInt();
			Title = imageXml.GetNamedChildNodeText( "title" );
			Description = imageXml.GetNamedChildNodeText( "description" );
			ViewCount = imageXml.GetNamedChildNodeText( "view_count" ).ToUInt();
			CommentCount = imageXml.GetNamedChildNodeText( "comment_count" ).ToUInt();
			ClipCount = imageXml.GetNamedChildNodeText( "clip_count" ).ToUInt();
			LastCommentBody = imageXml.GetNamedChildNodeText( "summary" );
			Genre = ( Genre )imageXml.GetNamedChildNodeText( "genre" ).ToInt();
#if DEBUG
			Site = ( Site )imageXml.GetNamedChildNodeText( "category" ).ToInt();
#endif
			ImageType = imageXml.GetNamedChildNodeText( "image_type" ).ToUShort();
			IllustType = imageXml.GetNamedChildNodeText( "illust_type" ).ToUShort();
			InspectionStatus = imageXml.GetNamedChildNodeText( "inspection_status" ).ToUShort();
			IsAnonymous = imageXml.GetNamedChildNodeText( "anonymous_flag" ).ToBooleanFrom1();
			PublicStatus = imageXml.GetNamedChildNodeText( "public_status" ).ToUShort();
			IsDeleted = imageXml.GetNamedChildNodeText( "delete_flag" ).ToBooleanFrom1();
			DeleteType = imageXml.GetNamedChildNodeText( "delete_type" ).ToUShort();
			//CacheTime = ( imageNode.GetNamedChildNodeText( "cache_time" ) + "+09:00" ).ToDateTimeOffsetFromIso8601();
			PostedAt = ( imageXml.GetNamedChildNodeText( "created" ) + "+09:00" ).ToDateTimeOffsetFromIso8601();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// 閲覧数
		/// </summary>
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// クリップ数
		/// </summary>
		public uint ClipCount { get; private set; }

		/// <summary>
		/// 最新コメントの一部
		/// </summary>
		public string LastCommentBody { get; private set; }

		/// <summary>
		/// ジャンル
		/// </summary>
		public Genre Genre { get; private set; }

#if DEBUG
		/// <summary>
		/// サイト
		/// </summary>
		public Site Site { get; private set; }
#endif

		/// <summary>
		/// 匿名か
		/// </summary>
		public bool IsAnonymous { get; private set; }

		/// <summary>
		/// 画像の種類 (?)
		/// </summary>
		public ushort ImageType { get; private set; }

		/// <summary>
		/// イラストの種類 (?)
		/// </summary>
		public ushort IllustType { get; private set; }

		/// <summary>
		/// ?
		/// </summary>
		public ushort InspectionStatus { get; private set; }

		/// <summary>
		/// 公開状態 (?)
		/// </summary>
		public ushort PublicStatus { get; private set; }

		/// <summary>
		/// 削除されたか
		/// </summary>
		public bool IsDeleted { get; private set; }

		/// <summary>
		/// 削除タイプ
		/// </summary>
		public ushort DeleteType { get; private set; }

		///// <summary>
		///// キャッシュ時間 (?)
		///// </summary>
		//public DateTimeOffset CacheTime { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }
	}
}