#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ニコファーレの情報を格納するクラス
	/// </summary>
	public sealed class Farre
	{
#if WINDOWS_APP
		internal Farre( IXmlNode farreXml )
#else
		internal Farre( XElement farreXml )
#endif
		{
			IsEnabled = farreXml.GetNamedChildNodeText( "farremode" ).ToBooleanFrom1();
			IsAvatarmakerEnabled = farreXml.GetNamedChildNodeText( "avatarmaker_enabled" ).ToBooleanFrom1();
			IsInvokeAvatarmakerEnabled = farreXml.GetNamedChildNodeText( "is_invoke_avatarmaker" ).ToBooleanFrom1();
			IsMultiAngleEnabled = farreXml.GetNamedChildNodeText( "multi_angle" ).ToBooleanFrom1();
			MultiAngleCount = farreXml.GetNamedChildNodeText( "multi_angle_num" ).ToUShort();
		}

		/// <summary>
		/// 有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// Avatarmaker が有効か
		/// </summary>
		public bool IsAvatarmakerEnabled { get; private set; }

		/// <summary>
		/// InvokeAvatarmaker が有効か
		/// </summary>
		public bool IsInvokeAvatarmakerEnabled { get; private set; }

		/// <summary>
		/// マルチアングルが有効か
		/// </summary>
		public bool IsMultiAngleEnabled { get; private set; }

		/// <summary>
		/// マルチアングル数
		/// </summary>
		public ushort MultiAngleCount { get; private set; }
	}
}