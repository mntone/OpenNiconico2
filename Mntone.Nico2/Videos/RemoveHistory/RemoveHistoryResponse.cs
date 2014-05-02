using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Videos.RemoveHistory
{
	/// <summary>
	/// 履歴を削除した結果を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class RemoveHistoryResponse
	{
		internal RemoveHistoryResponse()
		{ }

		[DataMember( Name = "status" )]
		private string StatusImpl
		{
			get { return string.Empty; }
			set
			{
				if( value != "ok" )
				{
					throw new Exception( "Parse Error." );
				}
			}
		}

		/// <summary>
		/// 履歴の件数
		/// </summary>
		[DataMember( Name = "count" )]
		public ushort HistoryCount { get; private set; }

		/// <summary>
		/// 削除した動画の ID
		/// </summary>
		[DataMember( Name = "removed" )]
		public string RemovedID { get; private set; }
	}
}