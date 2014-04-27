using System;
using System.Collections.Generic;
using Windows.Networking;

namespace Mntone.Nico2.Videos.Flv
{
	/// <summary>
	/// getflv の情報を格納するクラス
	/// </summary>
	public sealed class FlvResponse
	{
		internal FlvResponse( Dictionary<string, string> wwwFormData )
		{
			ThreadID = wwwFormData["thread_id"].ToUInt();
			Length = wwwFormData["l"].ToTimeSpanFromSecondsString();
			VideoUrl = wwwFormData["url"].ToUri();
			ReportUrl = wwwFormData["link"].ToUri();
			CommentServerUrl = wwwFormData["ms"].ToUri();
			SubCommentServerUrl = wwwFormData["ms_sub"].ToUri();

			PrivateReason = wwwFormData.ContainsKey( "deleted" ) ? ( PrivateReasonType )wwwFormData["deleted"].ToUShort() : PrivateReasonType.None;
			UserId = wwwFormData["user_id"].ToUInt();
			IsPremium = wwwFormData["is_premium"].ToBooleanFrom1();
			UserName = wwwFormData["nickname"];
			LoadedAt = DateTimeOffset.FromFileTime( 10000 * long.Parse( wwwFormData["time"] ) + 116444736000000000 );

			if( wwwFormData.ContainsKey( "needs_key" ) )
			{
				IsKeyRequired = wwwFormData["needs_key"].ToBooleanFrom1();
			}
			if( wwwFormData.ContainsKey( "optional_thread_id" ) )
			{
				OptionalThreadID = wwwFormData["optional_thread_id"].ToUInt();
			}
			if( wwwFormData.ContainsKey( "ng_ch" ) )
			{
				ChannelFilter = wwwFormData["ng_ch"];
			}
			if( wwwFormData.ContainsKey( "fmst" ) )
			{
				FlashMediaServerToken = wwwFormData["fmst"];
			}

			AppsHost = wwwFormData["hms"].ToHostName();
			AppsPort = wwwFormData["hmsp"].ToUShort();
			AppsThreadID = wwwFormData["hmst"].ToUShort();
			AppsTicket = wwwFormData["hmstk"];

#if DEBUG
			Done = wwwFormData["done"].ToBooleanFromString();
			NgRv = wwwFormData["ng_rv"].ToUShort();
#endif
		}

		/// <summary>
		/// スレッド ID
		/// </summary>
		public uint ThreadID { get; private set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get; private set; }

		/// <summary>
		/// 動画 URL
		/// </summary>
		public Uri VideoUrl { get; private set; }

		/// <summary>
		/// 連絡ページ URL
		/// </summary>
		public Uri ReportUrl { get; private set; }

		/// <summary>
		/// コメント サーバー URL
		/// </summary>
		public Uri CommentServerUrl { get; private set; }

		/// <summary>
		/// サブ コメント サーバー URL
		/// </summary>
		public Uri SubCommentServerUrl { get; private set; }

		/// <summary>
		/// 非公開理由
		/// </summary>
		public PrivateReasonType PrivateReason { get; private set; }

		/// <summary>
		/// 削除されたか
		/// </summary>
		/// <remarks>
		/// 非公開動画は削除されたうちに入らない
		/// </remarks>
		public bool IsDeleted { get { return PrivateReason != PrivateReasonType.None && PrivateReason != PrivateReasonType.Private; } }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// プレミアム会員か
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// キーが必要か
		/// </summary>
		public bool IsKeyRequired { get; private set; }

		/// <summary>
		/// 追加のスレッド ID
		/// </summary>
		public uint OptionalThreadID { get; private set; }

		/// <summary>
		/// チャンネル動画のフィルター
		/// </summary>
		public string ChannelFilter { get; private set; }

		/// <summary>
		/// Flash Media サーバーのトークン
		/// </summary>
		public string FlashMediaServerToken { get; private set; }

#if DEBUG
		/// <summary>
		/// ? done
		/// </summary>
		public bool Done { get; private set; }

		/// <summary>
		/// ? ng_rv
		/// </summary>
		public ushort NgRv { get; private set; }
#endif

		/// <summary>
		/// ニコニコアプリのホスト名
		/// </summary>
		public HostName AppsHost { get; private set; }

		/// <summary>
		/// ニコニコアプリのポート番号
		/// </summary>
		public ushort AppsPort { get; private set; }

		/// <summary>
		/// ニコニコアプリのスレッド ID
		/// </summary>
		public uint AppsThreadID { get; set; }

		/// <summary>
		/// ニコニコアプリのチケット
		/// </summary>
		public string AppsTicket { get; private set; }
	}
}