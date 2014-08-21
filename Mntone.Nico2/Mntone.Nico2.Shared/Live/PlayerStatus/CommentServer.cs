using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
using Windows.Networking;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメント サーバーの情報を格納するクラス
	/// </summary>
	public sealed class CommentServer
	{
#if WINDOWS_APP
		internal CommentServer( IXmlNode commentServerXml, IXmlNode threadIdsXml )
#else
		internal CommentServer( XElement commentServerXml, XElement threadIdsXml )
#endif
		{
#if WINDOWS_APP
			Host = commentServerXml.GetNamedChildNodeText( "addr" ).ToHostName();
#else
			Host = commentServerXml.GetNamedChildNodeText( "addr" );
#endif
			Port = commentServerXml.GetNamedChildNodeText( "port" ).ToUShort();
			if( threadIdsXml.GetFirstChildNode() != null )
			{
				ThreadIds = threadIdsXml.GetChildNodes().Select( threadIdXml => threadIdXml.GetText().ToUInt() ).ToList();
			}
			else
			{
				ThreadIds = new List<uint>()
				{
					commentServerXml.GetNamedChildNodeText( "thread" ).ToUInt()
				};
			}
		}

		/// <summary>
		/// ホスト名
		/// </summary>
#if WINDOWS_APP
		public HostName Host { get; private set; }
#else
		public string Host { get; private set; }
#endif

		/// <summary>
		/// ポート番号
		/// </summary>
		public ushort Port { get; private set; }

		/// <summary>
		/// スレッド ID
		/// </summary>
		/// <remarks>
		/// 配信者がこの API を叩くと、複数のスレッド ID を得ることができる。
		/// それにより、メインと立ち見の部屋を移動することができる。
		/// </remarks>
		public IReadOnlyList<uint> ThreadIds { get; private set; }
	}
}