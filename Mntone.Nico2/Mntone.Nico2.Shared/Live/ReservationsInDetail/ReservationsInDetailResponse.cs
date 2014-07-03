using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// watchingreservation?mode=detaillist の情報を格納するクラス
	/// </summary>
	public sealed class ReservationsInDetailResponse
	{
#if WINDOWS_APP
		internal ReservationsInDetailResponse( IXmlNode reservedItemsXml )
#else
		internal ReservationsInDetailResponse( XElement reservedItemsXml )
#endif
		{
			if( reservedItemsXml != null )
			{
				ReservedProgram = reservedItemsXml.GetChildNodes().Select( reservedItemXml => new Program( reservedItemXml ) ).ToList();
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