using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// Marquee 情報を格納するクラス
	/// </summary>
	public sealed class Marquee
	{
		internal Marquee( IXmlNode marqueeXml )
		{
			Category = marqueeXml.GetNamedChildNode( "category" ).InnerText;
			GameKey = marqueeXml.GetNamedChildNode( "game_key" ).InnerText;
			GameTime = marqueeXml.GetNamedChildNode( "game_time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			IsNotInterruptionForced = marqueeXml.GetNamedChildNode( "force_nicowari_off" ).InnerText.ToBooleanFrom1();
		}

		/// <summary>
		/// カテゴリー
		/// </summary>
		public string Category { get; private set; }

		/// <summary>
		/// ゲーム キー
		/// </summary>
		public string GameKey { get; private set; }

		/// <summary>
		/// ゲーム時間
		/// </summary>
		public DateTimeOffset GameTime { get; private set; }

		/// <summary>
		/// 割り込みを強制しないか
		/// </summary>
		public bool IsNotInterruptionForced { get; private set; }
	}
}