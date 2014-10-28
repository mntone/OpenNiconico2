using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Mntone.Nico2.Live.Tags
{
	/// <summary>
	/// タグ情報を格納するクラス
	/// </summary>
	public sealed class TagsResponse
	{
		internal TagsResponse( HtmlNode ulHtml )
		{
			this.IsEditable = ulHtml.ChildNodes.Reverse().Any( child => child.GetElementByClassName( "edit" ) != null );
			this._Tags = ulHtml.ChildNodes
				.Where( child => child.GetElementByClassName( "nicopedia" ) != null )
				.Select( child =>
				{
					var isCategoryTag = child.GetElementByClassName( "category" ) != null;
					var value = child.GetElementByClassName( "nicopedia" ).InnerText;
					var countText = child.GetElementByClassName( "npit" ).InnerText;
					var count = countText.Substring( 1, countText.Length - 2 ).ToUShort();
					return new TagInfo( isCategoryTag, value, count );
				} )
				.ToList();
		}

		/// <summary>
		/// 編集可能か
		/// </summary>
		public bool IsEditable { get; private set; }

		/// <summary>
		/// タグ 一覧
		/// </summary>
		public IReadOnlyList<TagInfo> Tags { get { return this._Tags; } }
		private List<TagInfo> _Tags = null;
	}
}
