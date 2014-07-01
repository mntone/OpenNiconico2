using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ネット デュエットの情報を格納するクラス
	/// </summary>
	public sealed class NetDuetto
	{
		internal NetDuetto( IXmlNode streamXml )
		{
			IsEnabled = streamXml.GetNamedChildNode( "allow_netduetto" ).InnerText.ToBooleanFrom1();
			Token = streamXml.GetNamedChildNode( "nd_token" ).InnerText;
		}

		/// <summary>
		/// 有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// トークン
		/// </summary>
		public string Token { get; private set; }
	}
}