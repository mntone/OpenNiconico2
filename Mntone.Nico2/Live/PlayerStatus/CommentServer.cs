using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.Networking;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメント サーバーの情報を格納するクラス
	/// </summary>
	public sealed class CommentServer
	{
		internal CommentServer( IXmlNode commentServerXml, IXmlNode threadIDsXml )
		{
			Host = commentServerXml.GetNamedChildNode( "addr" ).InnerText.ToHostName();
			Port = commentServerXml.GetNamedChildNode( "port" ).InnerText.ToUShort();
			if( threadIDsXml.FirstChild != null )
			{
				ThreadIDs = threadIDsXml.ChildNodes.Select( threadIDXml => threadIDXml.InnerText.ToUInt() ).ToList();
			}
			else
			{
				ThreadIDs = new List<uint>()
				{
					commentServerXml.GetNamedChildNode( "thread" ).InnerText.ToUInt()
				};
			}
		}

		/// <summary>
		/// ホスト名
		/// </summary>
		public HostName Host { get; private set; }

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
		public IReadOnlyList<uint> ThreadIDs { get; private set; }
	}
}