using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
#if WINDOWS_APP
		internal Comment( IXmlNode commentXml )
#else
		internal Comment( XElement commentXml )
#endif
		{
			ID = commentXml.GetNamedChildNodeText( "comment_id" ).ToULong();
			ImageID = "im" + commentXml.GetNamedChildNodeText( "image_id" );
			ResID = commentXml.GetNamedChildNodeText( "res_id" ).ToULong();
			Value = commentXml.GetNamedChildNodeText( "content" );
			Command = commentXml.GetNamedChildNodeText( "command" );
			PostedAt = ( commentXml.GetNamedChildNodeText( "created" ) + "+09:00" ).ToDateTimeOffsetFromIso8601();
			Frame = commentXml.GetNamedChildNodeText( "frame" ).ToInt();
			UserHash = commentXml.GetNamedChildNodeText( "user_hash" );
			IsAnonymous = commentXml.GetNamedChildNodeText( "anonymous_flag" ).ToBooleanFrom1();
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