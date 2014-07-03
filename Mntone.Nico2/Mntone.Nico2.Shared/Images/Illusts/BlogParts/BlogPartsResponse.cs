using System;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	/// <summary>
	/// blogparts の情報を格納するクラス
	/// </summary>
	public sealed class BlogPartsResponse
	{
#if WINDOWS_APP
		internal BlogPartsResponse( IXmlNode responseXml )
#else
		internal BlogPartsResponse( XElement responseXml )
#endif
		{
#if DEBUG
			BaseUrl = responseXml.GetNamedChildNodeText( "base_url" ).ToUri();
#endif
			PageUrl = responseXml.GetNamedChildNodeText( "icon_url" ).ToUri();
#if DEBUG
			ImageBaseUrl = responseXml.GetNamedChildNodeText( "image_url" ).ToUri();
#endif

			var imageListXml = responseXml.GetNamedChildNode( "image_list" );
			if( imageListXml.GetFirstChildNode().GetFirstChildNode() != null )
			{
				Images = imageListXml.GetChildNodes().Select( imageXml => new Image( imageXml ) ).ToList();
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