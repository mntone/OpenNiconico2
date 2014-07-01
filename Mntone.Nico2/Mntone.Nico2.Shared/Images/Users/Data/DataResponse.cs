using Mntone.Nico2.Images.Illusts;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images.Users.Data
{
	/// <summary>
	/// user/data の情報を格納するクラス
	/// </summary>
	public sealed class DataResponse
	{
		internal DataResponse( IXmlNode responseXml )
		{
#if DEBUG
			ImageCount = responseXml.GetNamedChildNode( "image_count" ).InnerText.ToUInt();
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

			var commentListXml = responseXml.GetNamedChildNode( "comment_list" );
			if( commentListXml.FirstChild.FirstChild != null )
			{
				Comments = commentListXml.ChildNodes.Select( commentXml => new Comment( commentXml ) ).ToList();
			}
			else
			{
				Comments = new List<Comment>();
			}
		}

#if DEBUG
		/// <summary>
		/// 画像の数
		/// </summary>
		public uint ImageCount { get; private set; }
#endif

		/// <summary>
		/// 画像の一覧
		/// </summary>
		public IReadOnlyList<Image> Images { get; private set; }

		/// <summary>
		/// コメントの一覧
		/// </summary>
		public IReadOnlyList<Comment> Comments { get; private set; }
	}
}