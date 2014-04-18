using System;

namespace Mntone.Nico2.Videos.Flv
{
	/// <summary>
	/// getflv の情報を格納するクラス
	/// </summary>
	public sealed class FlvResponse
	{
		internal FlvResponse()
		{ }

		/// <summary>
		/// スレッド ID
		/// </summary>
		public uint ThreadId { get; internal set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get; internal set; }

		/// <summary>
		/// 動画 URL
		/// </summary>
		public Uri VideoUrl { get; internal set; }

		/// <summary>
		/// 連絡ページ URL
		/// </summary>
		public Uri ReportUrl { get; internal set; }

		/// <summary>
		/// コメント サーバー URL
		/// </summary>
		public Uri MessageServerUrl { get; internal set; }

		/// <summary>
		/// サブ コメント サーバー URL
		/// </summary>
		public Uri SubMessageServerUrl { get; internal set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; internal set; }

		/// <summary>
		/// プレミアム会員かどうか
		/// </summary>
		public bool IsPremium { get; internal set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; internal set; }

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; internal set; }

#if DEBUG
		/// <summary>
		/// ? done
		/// </summary>
		public bool Done { get; internal set; }

		/// <summary>
		/// ? ng_rv
		/// </summary>
		public ushort NgRv { get; internal set; }

		/// <summary>
		/// ニコニコアプリの URL
		/// </summary>
		public Uri AppsUrl { get; internal set; }

		/// <summary>
		/// ニコニコアプリの ? (hmst)
		/// </summary>
		public ushort AppsT { get; set; }

		/// <summary>
		/// ニコニコアプリのチケット
		/// </summary>
		public string AppsTicket { get; internal set; }
#endif
	}
}