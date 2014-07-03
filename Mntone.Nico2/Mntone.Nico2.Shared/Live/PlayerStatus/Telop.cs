using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// テロップの情報を格納するクラス
	/// </summary>
	public sealed class Telop
	{
#if WINDOWS_APP
		internal Telop( IXmlNode telopNode )
#else
		internal Telop( XElement telopNode )
#endif
		{
			IsEnabled = telopNode.GetNamedChildNodeText( "enable" ).ToBooleanFrom1();

			var mailXml = telopNode.GetNamedChildNodeText( "mail" );
			if( !string.IsNullOrEmpty( mailXml ) )
			{
				Mail = mailXml;
				Value = telopNode.GetNamedChildNodeText( "caption" );
			}
			else
			{
				Mail = string.Empty;
				Value = string.Empty;
			}
		}

		/// <summary>
		/// テロップが有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// テロップのコマンド
		/// </summary>
		public string Mail { get; private set; }

		/// <summary>
		/// テロップの内容
		/// </summary>
		public string Value { get; private set; }
	}
}
