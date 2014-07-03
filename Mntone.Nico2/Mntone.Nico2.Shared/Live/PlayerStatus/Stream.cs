using System;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ストリーム情報を格納するクラス
	/// </summary>
	public sealed class Stream
	{
#if WINDOWS_APP
		internal Stream( IXmlNode streamXml, IXmlNode rtmpXml, IXmlNode ticketsXml, IXmlNode playerXml )
#else
		internal Stream( XElement streamXml, XElement rtmpXml, XElement ticketsXml, XElement playerXml )
#endif
		{
			IsFlashMediaServer = rtmpXml.GetNamedAttributeText( "is_fms" ).ToBooleanFrom1();

			var rtmptPortXml = rtmpXml.GetNamedAttributeText( "rtmpt_port" );
			RtmptPort = !string.IsNullOrEmpty( rtmptPortXml ) ? rtmptPortXml.ToUShort() : ( ushort )0u;
			
			RtmpUrl = rtmpXml.GetNamedChildNodeText( "url" ).ToUri();
			Ticket = rtmpXml.GetNamedChildNodeText( "ticket" );

			if( ticketsXml != null )
			{
				Tickets = ticketsXml.GetChildNodes().ToDictionary(
					ticketXml => ticketXml.GetNamedAttributeText( "name" ),
					ticketXml => ticketXml.GetText() );
			}

			Contents = streamXml.GetNamedChildNode( "contents_list" ).GetChildNodes().Select( contentsXml => new Content( contentsXml ) ).ToList();

			var splitTop = streamXml.GetNamedChildNodeText( "split_top" ).ToBooleanFrom1();
			if( splitTop )
			{
				Position = VideoPosition.Top;
			}
			else
			{
				var splitBottom = streamXml.GetNamedChildNodeText( "split_bottom" ).ToBooleanFrom1();
				if( splitBottom )
				{
					Position = VideoPosition.Bottom;
				}
				else
				{
					var background = streamXml.GetNamedChildNodeText( "background_comment" ).ToBooleanFrom1();
					Position = background ? VideoPosition.Small : VideoPosition.Default;
				}
			}

			var aspectXml = streamXml.GetNamedChildNodeText( "aspect" );
			Aspect = !string.IsNullOrEmpty( aspectXml ) ? aspectXml.ToVideoAspect() : VideoAspect.Auto;

			BroadcastToken = streamXml.GetNamedChildNodeText( "broadcast_token" );
			IsQualityOfServiceAnalyticsEnabled = playerXml.GetNamedChildNodeText( "qos_analytics" ).ToBooleanFrom1();
		}

		/// <summary>
		/// Flash Media サーバーか
		/// </summary>
		public bool IsFlashMediaServer { get; private set; }

		/// <summary>
		/// rtmpt の場合のポート番号
		/// </summary>
		public ushort RtmptPort { get; private set; }

		/// <summary>
		/// rtmp の URL
		/// </summary>
		public Uri RtmpUrl { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public IReadOnlyDictionary<string, string> Tickets { get; private set; }

		/// <summary>
		/// コンテンツ
		/// </summary>
		public IReadOnlyList<Content> Contents { get; private set; }

		/// <summary>
		/// 映像の表示位置
		/// </summary>
		public VideoPosition Position { get; private set; }

		/// <summary>
		/// 映像のアスペクト比
		/// </summary>
		public VideoAspect Aspect { get; private set; }

		/// <summary>
		/// 配信のトークン
		/// </summary>
		/// <remarks>
		/// 配信で使われる
		/// </remarks>
		public string BroadcastToken { get; private set; }

		/// <summary>
		/// QoS 分析が有効か
		/// </summary>
		public bool IsQualityOfServiceAnalyticsEnabled { get; private set; }
	}
}