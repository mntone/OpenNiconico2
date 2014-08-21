using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コンテンツを格納するクラス
	/// </summary>
	public sealed class Content
	{
#if WINDOWS_APP
		internal Content( IXmlNode contentsXml )
#else
		internal Content( XElement contentsXml )
#endif
		{
			Id = contentsXml.GetNamedAttributeText( "id" );
			IsAudioDisabled = contentsXml.GetNamedAttributeText( "disableAudio" ).ToBooleanFrom1();
			IsVideoDisabled = contentsXml.GetNamedAttributeText( "disableVideo" ).ToBooleanFrom1();
			StartedAt = contentsXml.GetNamedAttributeText( "start_time" ).ToDateTimeOffsetFromUnixTime();
			Duration = contentsXml.GetNamedAttributeText( "duration" ).ToTimeSpanFromSecondsString();
			Title = contentsXml.GetNamedAttributeText( "title" );
			Value = contentsXml.GetText();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 音声が無効か
		/// </summary>
		public bool IsAudioDisabled { get; private set; }

		/// <summary>
		/// 映像が無効か
		/// </summary>
		public bool IsVideoDisabled { get; private set; }

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get; private set; }

		/// <summary>
		/// 再生時間
		/// </summary>
		public TimeSpan Duration { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// コンテンツ
		/// </summary>
		public string Value { get; private set; }
	}
}