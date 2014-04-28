using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ストリーム情報を格納するクラス
	/// </summary>
	public sealed class Stream
	{
		internal Stream( IXmlNode streamXml, IXmlNode rtmpXml, IXmlNode ticketsXml, IXmlNode playerXml )
		{
			var isFmsXml = rtmpXml.GetNamedAttribute( "is_fms" );
			IsFlashMediaServer = isFmsXml != null ? isFmsXml.InnerText.ToBooleanFrom1() : false;

			var rtmptPortXml = rtmpXml.GetNamedAttribute( "rtmpt_port" );
			RtmptPort = rtmptPortXml != null ? rtmptPortXml.InnerText.ToUShort() : ( ushort )0u;
			
			RtmpUrl = rtmpXml.GetNamedChildNode( "url" ).InnerText.ToUri();
			Ticket = rtmpXml.GetNamedChildNode( "ticket" ).InnerText;

			if( ticketsXml != null )
			{
				Tickets = ticketsXml.ChildNodes.ToDictionary(
					ticketXml => ticketXml.GetNamedAttribute( "name" ).InnerText,
					ticketXml => ticketXml.InnerText );
			}

			Contents = streamXml.GetNamedChildNode( "contents_list" ).ChildNodes.Select( contentsXml => new Content( contentsXml ) ).ToList();

			var splitTop = streamXml.GetNamedChildNode( "split_top" ).InnerText.ToBooleanFrom1();
			if( splitTop )
			{
				Position = VideoPosition.Top;
			}
			else
			{
				var splitBottom = streamXml.GetNamedChildNode( "split_bottom" ).InnerText.ToBooleanFrom1();
				if( splitBottom )
				{
					Position = VideoPosition.Bottom;
				}
				else
				{
					var background = streamXml.GetNamedChildNode( "background_comment" ).InnerText.ToBooleanFrom1();
					Position = background ? VideoPosition.Small : VideoPosition.Default;
				}
			}

			var aspectXml = streamXml.ChildNodes.Where( node => node.NodeName == "aspect" ).SingleOrDefault();
			Aspect = aspectXml != null ? aspectXml.InnerText.ToVideoAspect() : VideoAspect.Auto;

			var broadcastTokenXml = streamXml.ChildNodes.Where( node => node.NodeName == "broadcast_token" ).SingleOrDefault();
			BroadcastToken = broadcastTokenXml != null ? broadcastTokenXml.InnerText : string.Empty;

			IsQualityOfServiceAnalyticsEnabled = playerXml.GetNamedChildNode( "qos_analytics" ).InnerText.ToBooleanFrom1();
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