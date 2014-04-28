using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// テロップの情報を格納するクラス
	/// </summary>
	public sealed class Telop
	{
		internal Telop( IXmlNode telopNode )
		{
			IsEnabled = telopNode.GetNamedChildNode( "enable" ).InnerText.ToBooleanFrom1();

			var mailXml = telopNode.ChildNodes.Where( node => node.NodeName == "mail" ).SingleOrDefault();
			if( mailXml != null )
			{
				Mail = mailXml.InnerText;
				Value = telopNode.GetNamedChildNode( "caption" ).InnerText;
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
