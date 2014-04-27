using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コマンドを格納するクラス
	/// </summary>
	/// <remarks>
	/// タイムシフトでは stream/quesheet による放送再現を行う
	/// </remarks>
	public sealed class Command
	{
		internal Command( IXmlNode queXml )
		{
			Position = TimeSpan.FromTicks( queXml.GetNamedAttribute( "vpos" ).InnerText.ToLong() * 10000 );
			Mail = queXml.GetNamedAttribute( "mail" ).InnerText;
			Name = queXml.GetNamedAttribute( "name" ).InnerText;
			Value = queXml.InnerText;
		}

		/// <summary>
		/// 実行時間
		/// </summary>
		public TimeSpan Position { get; private set; }

		/// <summary>
		/// 文字コマンド
		/// </summary>
		public string Mail { get; private set; }

		/// <summary>
		/// 投稿者名
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// コマンドの内容
		/// </summary>
		public string Value { get; private set; }
	}
}