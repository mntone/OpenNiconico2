using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mntone.Nico2.Live.Description
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	public sealed class DescriptionResponse
	{
		internal DescriptionResponse( HtmlNode coverHtml, string language )
		{
			var infoBoxHtml = coverHtml.GetElementByClassName( "container" ).GetElementById( "gate" ).GetElementByClassName( "infobox" );
			{
				var h2Html = infoBoxHtml.Element( "h2" );
				this.IsHighQuality = h2Html.GetAttributeValue( "class", string.Empty ) == "hq";
				this.Title = h2Html.GetElementByClassName( "program-title" ).InnerText;
			}

			var textBoxHtml = infoBoxHtml.GetElementByClassName( "textbox" );
			var bgHtml = textBoxHtml.GetElementByClassName( "bg" );
			var leBoxHtml = bgHtml != null ? bgHtml.GetElementByClassName( "lebox" ) : textBoxHtml.GetElementByClassName( "lebox" );
			var gboxHtml = leBoxHtml.GetElementById( "bn_gbox" );
			{
				var thumnailHtml = gboxHtml.GetElementByClassName( "bn" ).Element( "meta" );
				if( thumnailHtml.GetAttributeValue( "itemprop", string.Empty ) == "thumbnail" )
				{
					this.ThumbnailUrl = thumnailHtml.GetAttributeValue( "content", string.Empty ).ToUri();
				}
			}

			{
				var publishedHtml = gboxHtml.GetElementByClassName( "blbox" ).GetElementByClassName( "hmf" ).GetElementByClassName( "kaijo" ).Element( "meta" );
				if( publishedHtml.GetAttributeValue( "itemprop", string.Empty ) == "datePublished" )
				{
					this.OpenedAt = publishedHtml.GetAttributeValue( "content", string.Empty ).ToDateTimeOffsetFromIso8601();
				}
			}

			{
				var bgmHtml = leBoxHtml.GetElementByClassName( "bgm" ).GetElementByClassName( "text_area" );
				this.Description = bgmHtml.InnerHtml;
			}

			{
				var tableHtml = leBoxHtml.GetElementById( "livetags" ).GetElementByTagName( "table" );
				var tbodyHtml = tableHtml.GetElementByTagName( "tbody" );
				var trHtml = tbodyHtml != null ? tbodyHtml.GetElementByTagName( "tr" ) : tableHtml.GetElementByTagName( "tr" );
				this._Tags = trHtml
					.GetElementsByTagName( "td" ).Last()
					.GetElementsByTagName( "nobr" )
					.Select( child =>
					{
						var imgTag = child.GetElementByTagName( "img" );
						var isCategoryTag = imgTag != null ? imgTag.GetAttributeValue( "src", string.Empty ) == "img/watch/icon_ctgry/ja-jp.gif" : false;
						var value = child.GetElementByClassName( "nicopedia" ).InnerText.Trim( new char[] { ' ', '\t', '\n' } );
						var countText = child.GetElementByClassName( "npit" ).InnerText;
						var count = !string.IsNullOrEmpty( countText ) ? countText.Substring( 1, countText.Length - 2 ).ToUShort() : ( ushort )0u;
						return new Tags.TagInfo( isCategoryTag, value, count );
					} )
					.ToList();
			}
		}

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// 高画質配信か
		/// </summary>
		public bool IsHighQuality { get; private set; }

		/// <summary>
		/// サムネール
		/// </summary>
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 開場時間
		/// </summary>
		public DateTimeOffset OpenedAt { get; private set; }

		/// <summary>
		/// タグ 一覧
		/// </summary>
		public IReadOnlyList<Tags.TagInfo> Tags { get { return this._Tags; } }
		private List<Tags.TagInfo> _Tags = null;
	}
}
