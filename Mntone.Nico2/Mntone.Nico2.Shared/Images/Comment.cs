using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
		internal Comment( IXmlNode commentNode )
		{
			ID = commentNode.GetNamedChildNode( "comment_id" ).InnerText.ToULong();
			ImageID = "im" + commentNode.GetNamedChildNode( "image_id" ).InnerText;
			ResID = commentNode.GetNamedChildNode( "res_id" ).InnerText.ToULong();
			Value = commentNode.GetNamedChildNode( "content" ).InnerText;
			Command = commentNode.GetNamedChildNode( "command" ).InnerText;
			PostedAt = ( commentNode.GetNamedChildNode( "created" ).InnerText + "+09:00" ).ToDateTimeOffsetFromIso8601();
			Frame = commentNode.GetNamedChildNode( "frame" ).InnerText.ToInt();
			UserHash = commentNode.GetNamedChildNode( "user_hash" ).InnerText;
			IsAnonymous = commentNode.GetNamedChildNode( "anonymous_flag" ).InnerText.ToBooleanFrom1();
		}

		/// <summary>
		/// ID
		/// </summary>
		public ulong ID { get; private set; }

		/// <summary>
		/// 画像 ID
		/// </summary>
		public string ImageID { get; private set; }

		/// <summary>
		/// レス先 ID
		/// </summary>
		public ulong ResID { get; private set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// コマンド
		/// </summary>
		public string Command { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }

		/// <summary>
		/// フレーム (?)
		/// </summary>
		public int Frame { get; private set; }

		/// <summary>
		/// ユーザー ハッシュ
		/// </summary>
		public string UserHash { get; private set; }

		/// <summary>
		/// 匿名か
		/// </summary>
		public bool IsAnonymous { get; private set; }
	}
}