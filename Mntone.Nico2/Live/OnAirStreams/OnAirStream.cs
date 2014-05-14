using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Live.OnAirStreams
{
	/// <summary>
	/// 放送中の番組情報を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class OnAirStream
	{
		internal OnAirStream()
		{ }

		/// <summary>
		/// リストに表示しないか
		/// </summary>
		[DataMember( Name = "hide_zapping" )]
		public bool IsHidden { get; private set; }

		/// <summary>
		/// ID
		/// </summary>
		public string ID { get { return this._ID; } }
		private string _ID = string.Empty;

		[DataMember( Name = "id" )]
		private string IDImpl
		{
			get { return this._ID != null && this._ID.Length > 2 ? this._ID.Substring( 2 ) : null; }
			set { this._ID = "lv" + value; }
		}

		/// <summary>
		/// Nsen 番組か
		/// </summary>
		[DataMember( Name = "is_nsen" )]
		public bool IsNsen { get; private set; }

		/// <summary>
		/// 製品番組か
		/// </summary>
		[DataMember( Name = "is_product" )]
		public bool IsProduct { get; private set; }

		/// <summary>
		/// ザッピング モードが有効か
		/// </summary>
		[DataMember( Name = "is_zapping_mode_enabled" )]
		public bool IsZappingModeEnabled { get; private set; }

		/// <summary>
		/// 小さいサムネール URL
		/// </summary>
		[DataMember( Name = "thumbnail_small_url" )]
		public Uri SmallThumbnailUrl { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; private set; }

		/// <summary>
		/// 来場者数
		/// </summary>
		[DataMember( Name = "view_counter" )]
		public uint ViewCount { get; private set; }
	}
}