using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Images.Users.Info
{
	/// <summary>
	/// user/info の情報を格納するクラス
	/// </summary>
	public sealed class InfoResponse
	{
		internal InfoResponse( IXmlNode userInfoXml )
		{
			UserID = userInfoXml.GetNamedChildNode( "id" ).InnerText.ToUInt();
			UserName = userInfoXml.GetNamedChildNode( "nickname" ).InnerText;
		}

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserID { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }
	}
}