using System;

namespace Mntone.Nico2.Vita
{
	/// <summary>
	/// 範囲
	/// </summary>
	public sealed class Range
	{
		private ushort _from = 0;
		private ushort _length = 0;

		private Range( ushort from, ushort length )
		{
			this._from = from;
			this._length = length;
		}

		/// <summary>
		/// from から to まで
		/// </summary>
		/// <param name="from">ここから</param>
		/// <param name="to">ここまで</param>
		/// <returns>範囲</returns>
		public static Range FromTo( ushort from, ushort to )
		{
			if( from > to )
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Range( from, ( ushort )( to - from + 1 ) );
		}

		/// <summary>
		/// from から until まで
		/// </summary>
		/// <param name="from">ここから</param>
		/// <param name="until">これ以前</param>
		/// <returns>範囲</returns>
		public static Range FromUntil( ushort from, ushort until )
		{
			if( from >= until )
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Range( from, ( ushort )( until - from ) );
		}

		/// <summary>
		/// from から length 間
		/// </summary>
		/// <param name="from">ここから</param>
		/// <param name="length">期間</param>
		/// <returns>範囲</returns>
		public static Range FromFor( ushort from, ushort length )
		{
			return new Range( from, length );
		}

		/// <summary>
		/// 長さの確認
		/// </summary>
		/// <param name="availableMaximumLength">有効な最大の長さ</param>
		public void CheckMaximumLength( ushort availableMaximumLength )
		{
			if( this.Length > availableMaximumLength )
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// 長さの確認
		/// </summary>
		/// <param name="availableMaximumLength">有効な最大の長さ</param>
		/// <param name="parameterName">引数名</param>
		public void CheckMaximumLength( ushort availableMaximumLength, string parameterName )
		{
			if( this.Length > availableMaximumLength )
			{
				throw new ArgumentOutOfRangeException( parameterName );
			}
		}

		/// <summary>
		/// 長さの確認
		/// </summary>
		/// <param name="availableLength">有効な長さの範囲</param>
		public void CheckLengthRange( Range availableLength )
		{
			if( this.Length < availableLength.From || this.Length > availableLength.Until )
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// インデックスの確認
		/// </summary>
		/// <param name="availableIndex">有効なインデックスの範囲</param>
		public void CheckIndexRange( Range availableIndex )
		{
			if( this.From < availableIndex.From || this.Until > availableIndex.Until )
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// ここから
		/// </summary>
		public ushort From { get { return this._from; } }

		/// <summary>
		/// ここまで
		/// </summary>
		public ushort To { get { return ( ushort )( this._from + this._length - 1 ); } }

		/// <summary>
		/// これ以前
		/// </summary>
		public ushort Until { get { return ( ushort )( this._from + this._length ); } }

		/// <summary>
		/// 開始インデックス
		/// </summary>
		public ushort BeginIndex { get { return this.From; } }

		/// <summary>
		/// 開始インデックス
		/// </summary>
		public ushort StartIndex { get { return this.From; } }

		/// <summary>
		/// 終了インデックス
		/// </summary>
		public ushort EndIndex { get { return this.Until; } }

		/// <summary>
		/// 長さ
		/// </summary>
		public ushort Length { get { return this._length; } }

		/// <summary>
		/// 大きさ
		/// </summary>
		public ushort Size { get { return this.Length; } }

		/// <summary>
		/// 等しいかどうか
		/// </summary>
		/// <param name="other">他の Range</param>
		/// <returns>等しければ true、そうでなければ false</returns>
		public bool Equals( Range other )
		{
			return this._from == other._from && this._length == other._length;
		}
	}
}