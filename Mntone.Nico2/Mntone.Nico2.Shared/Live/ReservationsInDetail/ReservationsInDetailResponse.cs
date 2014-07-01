using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// watchingreservation?mode=detaillist の情報を格納するクラス
	/// </summary>
	public sealed class ReservationsInDetailResponse
	{
		internal ReservationsInDetailResponse( IXmlNode reservedItemsXml )
		{
			if( reservedItemsXml != null )
			{
				ReservedProgram = reservedItemsXml.ChildNodes.Select( reservedItemXml => new Program( reservedItemXml ) ).ToList();
			}
			else
			{
				ReservedProgram = new List<Program>();
			}
		}

		/// <summary>
		/// 画像の一覧
		/// </summary>
		public IReadOnlyList<Program> ReservedProgram { get; private set; }
	}
}