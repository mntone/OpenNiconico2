using Mntone.Nico2.Images.Illusts;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Users.Data
{
	/// <summary>
	/// user/data の情報を格納するクラス
	/// </summary>
	public sealed class DataResponse
	{
#if WINDOWS_APP
		internal DataResponse( IXmlNode responseXml )
#else
		internal DataResponse( XElement responseXml )
#endif
		{
#if DEBUG
			ImageCount = responseXml.GetNamedChildNodeText( "image_count" ).ToUInt();
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

			var commentListXml = responseXml.GetNamedChildNode( "comment_list" );
			if( commentListXml.GetFirstChildNode().GetFirstChildNode() != null )
			{
				Comments = commentListXml.GetChildNodes().Select( commentXml => new Comment( commentXml ) ).ToList();
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