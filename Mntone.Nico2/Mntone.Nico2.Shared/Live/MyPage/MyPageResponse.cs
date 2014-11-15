using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Mntone.Nico2.Live.MyPage
{
	/// <summary>
	/// マイページ情報を格納するクラス
	/// </summary>
	public sealed class MyPageResponse
	{
		internal MyPageResponse( HtmlNode liveListHtml, string language )
		{
			this._TimeshiftPrograms = liveListHtml
				.GetElementById( "TS_list" )
				.GetElementById( "liveItemsWrap" )
				.GetElementById( "liveItemsWrap" )
				.GetElementsByClassName( "liveItems" )
				.SelectMany( i => i.GetElementsByClassName( "column" ) )
				.Select( i => i.GetElementByClassName( "name" ).GetElementByTagName( "a" ) )
				.Select( i => new TimeshiftProgramInfo( i.GetAttributeValue( "href", string.Empty ).Substring( 30 ), i.GetAttributeValue( "title", string.Empty ) ) )
				.ToList();

			var favoriteList = liveListHtml.GetElementById( "Favorite_list" );
			{
				var onAirWrap = favoriteList.GetElementById( "subscribeItemsWrap" );
				var onAirItems = onAirWrap != null ? onAirWrap.GetElementsByClassName( "liveItems" ) : null;
				if( onAirItems != null )
				{
					var allOnAirWrap = favoriteList.GetElementById( "all_subscribeItemsWrap" );
					var allOnAirItems = allOnAirWrap != null ? allOnAirWrap.GetElementsByClassName( "liveItems" ) : null;

					IEnumerable<HtmlNode> ie = onAirItems;
					if( allOnAirItems != null )
					{
						ie = ie.Union( allOnAirItems );
					}
					this._OnAirPrograms = ie.SelectMany( i => i.GetElementsByTagName( "div" ) ).Select( i => new ProgramInfo( i, language ) ).ToList();
				}
				else
				{
					this._OnAirPrograms = new List<ProgramInfo>();
				}
			}
			{
				var reservedWrap = favoriteList.GetElementById( "subscribeReservedItemsWrap" );
				var reservedItems = reservedWrap != null ? reservedWrap.GetElementsByClassName( "liveItems" ) : null;
				if( reservedItems != null )
				{
					var allReservedWrap = favoriteList.GetElementById( "all_subscribeReservedItemsWrap" );
					var allReservedItems = allReservedWrap != null ? allReservedWrap.GetElementsByClassName( "liveItems" ) : null;

					IEnumerable<HtmlNode> ie = reservedItems;
					if( allReservedItems != null )
					{
						ie = ie.Union( allReservedItems );
					}
					this._ReservedPrograms = ie.SelectMany( i => i.GetElementsByTagName( "div" ) ).Select( i => new ProgramInfo( i, language, true ) ).ToList();
				}
				else
				{
					this._ReservedPrograms = new List<ProgramInfo>();
				}
			}
		}

		/// <summary>
		/// タイムシフト番組一覧
		/// </summary>
		public IReadOnlyList<TimeshiftProgramInfo> TimeshiftPrograms { get { return this._TimeshiftPrograms; } }
		private List<TimeshiftProgramInfo> _TimeshiftPrograms = null;

		/// <summary>
		/// 放送中番組一覧
		/// </summary>
		public IReadOnlyList<ProgramInfo> OnAirPrograms { get { return this._OnAirPrograms; } }
		private List<ProgramInfo> _OnAirPrograms = null;

		/// <summary>
		/// 予約済み番組一覧
		/// </summary>
		public IReadOnlyList<ProgramInfo> ReservedPrograms { get { return this._ReservedPrograms; } }
		private List<ProgramInfo> _ReservedPrograms = null;
	}
}
