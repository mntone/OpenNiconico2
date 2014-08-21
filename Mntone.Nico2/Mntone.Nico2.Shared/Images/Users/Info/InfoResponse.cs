using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Users.Info
{
	/// <summary>
	/// user/info の情報を格納するクラス
	/// </summary>
	public sealed class InfoResponse
	{

#if WINDOWS_APP
		internal InfoResponse( IXmlNode userInfoXml )
#else
		internal InfoResponse( XElement userInfoXml )
#endif
		{
			UserId = userInfoXml.GetNamedChildNodeText( "id" ).ToUInt();
			UserName = userInfoXml.GetNamedChildNodeText( "nickname" );
		}

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }
	}
}