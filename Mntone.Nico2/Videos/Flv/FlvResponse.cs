using System;
using System.Collections.Generic;

namespace Mntone.Nico2.Videos.Flv
{
	/// <summary>
	/// getflv の情報を格納するクラス
	/// </summary>
	public sealed class FlvResponse
	{
		internal FlvResponse( Dictionary<string, string> wwwFormData )
		{
			ThreadId = wwwFormData["thread_id"].ToUInt();
			Length = TimeSpan.FromSeconds( ushort.Parse( wwwFormData["l"] ) );
			VideoUrl = wwwFormData["url"].ToUri();
			ReportUrl = wwwFormData["link"].ToUri();
			CommentServerUrl = wwwFormData["ms"].ToUri();
			SubCommentServerUrl = wwwFormData["ms_sub"].ToUri();
			UserId = wwwFormData["user_id"].ToUInt();
			IsPremium = wwwFormData["is_premium"].ToBooleanFrom1();
			UserName = wwwFormData["nickname"];
			LoadedAt = DateTimeOffset.FromFileTime( 10000 * long.Parse( wwwFormData["time"] ) + 116444736000000000 );

#if DEBUG
			Done = wwwFormData["done"].ToBooleanFromString();
			NgRv = wwwFormData["ng_rv"].ToUShort();
			AppsUrl = ( "http://" + wwwFormData["hms"] + ':' + wwwFormData["hmsp"] + '/' ).ToUri();
			AppsT = wwwFormData["hmst"].ToUShort();
			AppsTicket = wwwFormData["hmstk"];
#endif
		}

		/// <summary>
		/// スレッド ID
		/// </summary>
		public uint ThreadId { get; private set; }

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
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// プレミアム会員かどうか
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

#if DEBUG
		/// <summary>
		/// ? done
		/// </summary>
		public bool Done { get; private set; }

		/// <summary>
		/// ? ng_rv
		/// </summary>
		public ushort NgRv { get; private set; }

		/// <summary>
		/// ニコニコアプリの URL
		/// </summary>
		public Uri AppsUrl { get; private set; }

		/// <summary>
		/// ニコニコアプリの ? (hmst)
		/// </summary>
		public ushort AppsT { get; set; }

		/// <summary>
		/// ニコニコアプリのチケット
		/// </summary>
		public string AppsTicket { get; private set; }
#endif
	}
}