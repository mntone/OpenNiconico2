using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 部屋情報を格納するクラス
	/// </summary>
	public sealed class Room
	{
		internal Room( IXmlNode streamXml, IXmlNode userXml )
		{
			Name = userXml.GetNamedChildNode( "room_label" ).InnerText;
			SeatID = userXml.GetNamedChildNode( "room_seetno" ).InnerText.ToUShort();

			var seatTokenXml = streamXml.ChildNodes.Where( node => node.NodeName == "seat_token" ).SingleOrDefault();
			if( seatTokenXml != null )
			{
				SeatToken = seatTokenXml.InnerText;
			}
			else
			{
				SeatToken = string.Empty;
			}
		}

		/// <summary>
		/// 部屋名
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 席番号
		/// </summary>
		public ushort SeatID { get; private set; }

		/// <summary>
		/// 座席のトークン
		/// </summary>
		/// <remarks>
		/// 非ログインで使われる
		/// </remarks>
		public string SeatToken { get; private set; }
	}
}