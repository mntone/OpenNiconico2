using HtmlAgilityPack;
using System;
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
	}
}
