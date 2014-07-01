using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 番組の Twitter 情報を格納するクラス
	/// </summary>
	public sealed class ProgramTwitter
	{
		internal ProgramTwitter( IXmlNode streamXml, IXmlNode twitterXml )
		{
			IsEnabled = twitterXml.GetNamedChildNode( "live_enabled" ).InnerText.ToBooleanFrom1();
			Hashtag = streamXml.GetNamedChildNode( "twitter_tag" ).InnerText;
			VipModeCount = twitterXml.GetNamedChildNode( "vip_mode_count" ).InnerText.ToUInt();
		}

		/// <summary>
		/// Twitter が有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// ハッシュタグ
		/// </summary>
		public string Hashtag { get; private set; }

		/// <summary>
		/// Vip アカウントとして表示するフォロワーの最低件数
		/// </summary>
		public uint VipModeCount { get; private set; }
	}
}