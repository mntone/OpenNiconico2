using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
#if WINDOWS_APP
		internal Comment( IXmlNode streamXml, CommentServer commentServer )
#else
		internal Comment( XElement streamXml, CommentServer commentServer )
#endif
		{
			IsLocked = streamXml.GetNamedChildNodeText( "comment_lock" ).ToBooleanFrom1();

			var scale = streamXml.GetNamedChildNodeText( "font_scale" );
			Scale = !string.IsNullOrEmpty( scale ) ? scale.ToSingle() : 1.0f;

			Perm = streamXml.GetNamedChildNodeText( "perm" );

			var splitTop = streamXml.GetNamedChildNodeText( "split_top" ).ToBooleanFrom1();
			if( splitTop )
			{
				Position = CommentPosition.Bottom;
			}
			else
			{
				var splitBottom = streamXml.GetNamedChildNodeText( "split_top" ).ToBooleanFrom1();
				if( splitBottom )
				{
					Position = CommentPosition.Top;
				}
				else
				{
					var isTop = streamXml.GetNamedChildNodeText( "header_comment" ).ToBooleanFrom1();
					var isBottom = streamXml.GetNamedChildNodeText( "footer_comment" ).ToBooleanFrom1();
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

			FilteringLevel = ( CommentFilteringLevel )streamXml.GetNamedChildNodeText( "ng_scoring" ).ToUShort();
			SexMode = ( CommentSexMode )streamXml.GetNamedChildNodeText( "danjo_comment_mode" ).ToInt();

			var quesheetXml = streamXml.GetNamedChildNode( "quesheet" );
			if( quesheetXml != null )
			{
				Commands = quesheetXml.GetChildNodes().Select( queXml => new Command( queXml ) ).ToList();
			}

			IsRestrict = streamXml.GetNamedChildNodeText( "is_restrict" ).ToBooleanFrom1();

			var productCommentXml = streamXml.GetNamedChildNodeText( "product_comment" );
			if( !string.IsNullOrEmpty( productCommentXml ) )
			{
				LimitMode = ( CommentLimitMode )productCommentXml.ToInt();
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
		/// コマンド
		/// </summary>
		public IReadOnlyList<Command> Commands { get; private set; }

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