using Mntone.Nico2.Live;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	/// <summary>
	/// 検索リクエストを格納するクラス
	/// </summary>
	public sealed class SearchProgramsRequest
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		public SearchProgramsRequest()
		{ }

		/// <summary>
		/// 範囲
		/// </summary>
		public Range Range
		{
			get { return this._Range; }
			set
			{
				if( this._Range.Equals( value ) )
				{
					value.CheckMaximumLength( 149, "value" );
					this._Range = value;
				}
			}
		}
		private Range _Range;

		/// <summary>
		/// 検索の種類
		/// </summary>
		public SearchType SearchType { get; set; }

		/// <summary>
		/// ソート方向
		/// </summary>
		public SortDirection Direction { get; set; }

		/// <summary>
		/// 期間
		/// </summary>
		public DurationType Duration { get; set; }

		/// <summary>
		/// コミュニティーの種類
		/// </summary>
		/// <remarks>null はすべて</remarks>
		public Nullable<CommunityType> Provider { get; set; }

		/// <summary>
		/// ソート方法
		/// </summary>
		public SortType SortType { get; set; }

		/// <summary>
		/// 検索ワード
		/// </summary>
		public string Word { get; set; }

		internal string ToRequestString()
		{
			var sb = new StringBuilder( NiconicoUrls.LiveVideoSearchUrl );

			sb.Append( '&' );
			sb.Append( this._Range.ToFromLimitString() );

			if( this.SearchType == SearchType.Tag )
			{
				sb.Append( "&kind=tags" );
			}
			if( this.Direction == SortDirection.Descending )
			{
				sb.Append( "&order=" );
				sb.Append( this.Direction.ToChar() );
			}
			if( this.Duration != DurationType.OnAir )
			{
				sb.Append( "&mode=" );
				sb.Append( this.Duration.ToDurationTypeString() );
			}
			if( this.Provider.HasValue )
			{
				sb.Append( "&pt=" );
				sb.Append( this.Provider.Value.ToCommunityTypeString() );
			}
			if( this.SortType == SortType.Comment )
			{
				sb.Append( "&sort=comment" );
			}

			sb.Append( "&word=" );
			sb.Append( this.Word );

			return sb.ToString();
		}
	}
}