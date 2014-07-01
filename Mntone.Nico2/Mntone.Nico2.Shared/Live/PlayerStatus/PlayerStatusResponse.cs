using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// getplayerstatus の情報を格納するクラス
	/// </summary>
	/// <!--
	/// よくわからないもの
	/// - stream/is_kirei
	/// - stream/press
	/// -->
	public sealed class PlayerStatusResponse
	{
		internal PlayerStatusResponse( IXmlNode playerStatusXml )
		{
			var streamXml = playerStatusXml.GetNamedChildNode( "stream" );
			var userXml = playerStatusXml.GetNamedChildNode( "user" );
			var playerXml = playerStatusXml.GetNamedChildNode( "player" );

			LoadedAt = playerStatusXml.GetNamedAttribute( "time" ).InnerText.ToDateTimeOffsetFromUnixTime();
			Program = new Program(
				streamXml,
				playerXml,
				playerStatusXml.GetNamedAttribute( "nsen" ),
				new ProgramTwitter( streamXml, playerStatusXml.GetNamedChildNode( "twitter" ) ) );
			Room = new Room( streamXml, userXml );
			Stream = new Stream(
				streamXml,
				playerStatusXml.GetNamedChildNode( "rtmp" ),
				playerStatusXml.ChildNodes.Where( node => node.NodeName == "tickets" ).SingleOrDefault(),
				playerXml );
			Comment = new Comment(
				streamXml,
				new CommentServer(
					playerStatusXml.GetNamedChildNode( "ms" ),
					playerStatusXml.GetNamedChildNode( "tid_list" ) ) );
			Telop = new Telop( streamXml.GetNamedChildNode( "telop" ) );
			NetDuetto = new NetDuetto( streamXml );
			Marquee = new Marquee( playerStatusXml.GetNamedChildNode( "marquee" ) );
			User = new User( streamXml, userXml );
		}

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// 番組情報
		/// </summary>
		public Program Program { get; private set; }

		/// <summary>
		/// 部屋情報
		/// </summary>
		public Room Room { get; private set; }

		/// <summary>
		/// ストリーム情報
		/// </summary>
		public Stream Stream { get; private set; }

		/// <summary>
		/// コメント情報
		/// </summary>
		public Comment Comment { get; private set; }

		/// <summary>
		/// テロップ情報
		/// </summary>
		public Telop Telop { get; private set; }

		/// <summary>
		/// ネット デュエット情報
		/// </summary>
		public NetDuetto NetDuetto { get; private set; }

		/// <summary>
		/// Marquee 情報
		/// </summary>
		public Marquee Marquee { get; private set; }

		/// <summary>
		/// ユーザー情報
		/// </summary>
		/// <remarks>
		/// 視聴者の情報が格納されています
		/// </remarks>
		public User User { get; private set; }
	}
}