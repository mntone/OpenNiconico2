using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// Marquee 情報を格納するクラス
	/// </summary>
	public sealed class Marquee
	{
#if WINDOWS_APP
		internal Marquee( IXmlNode marqueeXml )
#else
		internal Marquee( XElement marqueeXml )
#endif
		{
			Category = marqueeXml.GetNamedChildNodeText( "category" );
			GameKey = marqueeXml.GetNamedChildNodeText( "game_key" );
			GameTime = marqueeXml.GetNamedChildNodeText( "game_time" ).ToDateTimeOffsetFromUnixTime();
			IsNotInterruptionForced = marqueeXml.GetNamedChildNodeText( "force_nicowari_off" ).ToBooleanFrom1();
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