using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
		internal Comment( IXmlNode streamXml, CommentServer commentServer )
		{
			IsLocked = streamXml.GetNamedChildNode( "comment_lock" ).InnerText.ToBooleanFrom1();

			var scale = streamXml.GetNamedChildNode( "font_scale" ).InnerText;
			Scale = !string.IsNullOrEmpty( scale ) ? scale.ToSingle() : 1.0f;

			var permXml = streamXml.ChildNodes.Where( node => node.NodeName == "perm" ).SingleOrDefault();
			if( permXml != null )
			{
				Perm = permXml.InnerText;
			}

			var splitTop = streamXml.GetNamedChildNode( "split_top" ).InnerText.ToBooleanFrom1();
			if( splitTop )
			{
				Position = CommentPosition.Bottom;
			}
			else
			{
				var splitBottom = streamXml.GetNamedChildNode( "split_top" ).InnerText.ToBooleanFrom1();
				if( splitBottom )
				{
					Position = CommentPosition.Top;
				}
				else
				{
					var isTop = streamXml.GetNamedChildNode( "header_comment" ).InnerText.ToBooleanFrom1();
					var isBottom = streamXml.GetNamedChildNode( "footer_comment" ).InnerText.ToBooleanFrom1();
					if( isTop )
					{
						Position = isBottom ? CommentPosition.Both : CommentPosition.Top;
					}
					else
					{
						Position = isBottom ? CommentPosition.Bottom : CommentPosition.Default;
					}
				}
			}

			FilteringLevel = ( CommentFilteringLevel )streamXml.GetNamedChildNode( "ng_scoring" ).InnerText.ToUShort();
			SexMode = ( CommentSexMode )streamXml.GetNamedChildNode( "danjo_comment_mode" ).InnerText.ToInt();

			var isRestrictXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_restrict" ).SingleOrDefault();
			IsRestrict = isRestrictXml != null ? isRestrictXml.InnerText.ToBooleanFrom1() : false;

			var productCommentXml = streamXml.ChildNodes.Where( node => node.NodeName == "product_comment" ).SingleOrDefault();
			if( productCommentXml != null )
			{
				LimitMode = ( CommentLimitMode )productCommentXml.InnerText.ToInt();
			}

			Server = commentServer;
		}

		/// <summary>
		/// コメント ロックされているか
		/// </summary>
		public bool IsLocked { get; private set; }

		/// <summary>
		/// コメント スケール率
		/// </summary>
		public float Scale { get; private set; }

		/// <summary>
		/// perm
		/// </summary>
		public string Perm { get; private set; }

		/// <summary>
		/// コメント表示位置
		/// </summary>
		public CommentPosition Position { get; private set; }

		/// <summary>
		/// フィルタリング レベル
		/// </summary>
		public CommentFilteringLevel FilteringLevel { get; private set; }

		/// <summary>
		/// 性別による装飾方法
		/// </summary>
		public CommentSexMode SexMode { get; private set; }

		/// <summary>
		/// 厳密に評価するか
		/// </summary>
		/// <remarks>
		/// 厳密に評価する場合、試用版では LimitType に応じて機能が制限されます。
		/// LimitType = Allow, Restrict: コメントは制限されません。
		/// LimitType = Deny: コメントは制限されます (購入者のみ投稿できます)
		/// </remarks>
		public bool IsRestrict { get; private set; }

		/// <summary>
		/// 製品のコメント モード
		/// </summary>
		public CommentLimitMode LimitMode { get; private set; }

		/// <summary>
		/// コメント サーバー情報
		/// </summary>
		public CommentServer Server { get; private set; }
	}
}