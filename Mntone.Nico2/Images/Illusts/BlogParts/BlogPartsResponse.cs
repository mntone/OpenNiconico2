using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	/// <summary>
	/// clip の情報を格納するクラス
	/// </summary>
	public sealed class BlogPartsResponse
	{
		internal BlogPartsResponse( IXmlNode responseXml )
		{
#if DEBUG
			BaseUrl = responseXml.GetNamedChildNode( "base_url" ).InnerText.ToUri();
#endif
			PageUrl = responseXml.GetNamedChildNode( "icon_url" ).InnerText.ToUri();
#if DEBUG
			ImageBaseUrl = responseXml.GetNamedChildNode( "image_url" ).InnerText.ToUri();
#endif

			var imageListXml = responseXml.GetNamedChildNode( "image_list" );
			if( imageListXml.FirstChild.FirstChild != null )
			{
				Images = imageListXml.ChildNodes.Select( imageXml => new Image( imageXml ) ).ToList();
			}
			else
			{
				Images = new List<Image>();
			}
		}

#if DEBUG
		/// <summary>
		/// ベース URL
		/// </summary>
		public Uri BaseUrl { get; private set; }
#endif

		/// <summary>
		/// 視聴ページ
		/// </summary>
		public Uri PageUrl { get; private set; }

#if DEBUG
		/// <summary>
		/// 画像のベース URL
		/// </summary>
		public Uri ImageBaseUrl { get; private set; }
#endif
		/// <summary>
		/// 画像の一覧
		/// </summary>
		public IReadOnlyList<Image> Images { get; private set; }
	}
}