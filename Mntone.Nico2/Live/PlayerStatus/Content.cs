using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コンテンツを格納するクラス
	/// </summary>
	public sealed class Content
	{
		internal Content( IXmlNode contentsXml )
		{
			ID = contentsXml.GetNamedAttribute( "id" ).InnerText;
			IsAudioDisabled = contentsXml.GetNamedAttribute( "disableAudio" ).InnerText.ToBooleanFrom1();
			IsVideoDisabled = contentsXml.GetNamedAttribute( "disableVideo" ).InnerText.ToBooleanFrom1();
			StartedAt = contentsXml.GetNamedAttribute( "start_time" ).InnerText.ToDateTimeOffsetFromUnixTime();

			var durationAttribute = contentsXml.GetNamedAttribute( "duration" );
			if( durationAttribute != null )
			{
				Duration = durationAttribute.InnerText.ToTimeSpanFromSecondsString();
			}

			var titleAttribute = contentsXml.GetNamedAttribute( "title" );
			if( titleAttribute != null )
			{
				Title = titleAttribute.InnerText;
			}

			Value = contentsXml.InnerText;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get; private set; }

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