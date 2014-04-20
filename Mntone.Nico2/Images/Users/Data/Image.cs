using Mntone.Nico2.Images.Illusts;
using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images.Users.Data
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Image
	{
		internal Image( IXmlNode imageNode )
		{
			ID = "im" + imageNode.GetNamedChildNode( "id" ).InnerText;
			UserId = imageNode.GetNamedChildNode( "user_id" ).InnerText.ToUInt();
			Title = imageNode.GetNamedChildNode( "title" ).InnerText;
			Description = imageNode.GetNamedChildNode( "description" ).InnerText;
			ViewCount = imageNode.GetNamedChildNode( "view_count" ).InnerText.ToUInt();
			CommentCount = imageNode.GetNamedChildNode( "comment_count" ).InnerText.ToUInt();
			ClipCount = imageNode.GetNamedChildNode( "clip_count" ).InnerText.ToUInt();
			LastCommentBody = imageNode.GetNamedChildNode( "summary" ).InnerText;
			Genre = ( Genre )imageNode.GetNamedChildNode( "genre" ).InnerText.ToInt();
			Category = ( Category )imageNode.GetNamedChildNode( "category" ).InnerText.ToInt();
			ImageType = imageNode.GetNamedChildNode( "image_type" ).InnerText.ToUShort();
			IllustType = imageNode.GetNamedChildNode( "illust_type" ).InnerText.ToUShort();
			InspectionStatus = imageNode.GetNamedChildNode( "inspection_status" ).InnerText.ToUShort();
			IsAnonymous = imageNode.GetNamedChildNode( "anonymous_flag" ).InnerText.ToBooleanFrom1();
			PublicStatus = imageNode.GetNamedChildNode( "public_status" ).InnerText.ToUShort();
			IsDeleted = imageNode.GetNamedChildNode( "delete_flag" ).InnerText.ToBooleanFrom1();
			DeleteType = imageNode.GetNamedChildNode( "delete_type" ).InnerText.ToUShort();
			//CacheTime = ( imageNode.GetNamedChildNode( "cache_time" ).InnerText + "+09:00" ).ToDateTimeOffsetFromIso8601();
			PostedAt = ( imageNode.GetNamedChildNode( "created" ).InnerText + "+09:00" ).ToDateTimeOffsetFromIso8601();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

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

		/// <summary>
		/// カテゴリー
		/// </summary>
		public Category Category { get; private set; }

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

		/// <summary>
		/// キャッシュ時間 (?)
		/// </summary>
		//public DateTimeOffset CacheTime { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }
	}
}