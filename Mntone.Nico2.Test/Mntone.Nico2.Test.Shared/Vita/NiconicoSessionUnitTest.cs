using Mntone.Nico2.Vita;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Vita
{
	[TestClass]
	public sealed class RangeUnitTest
	{
		[TestMethod]
		public void Range_0FromTo()
		{
			var ret = Range.FromTo( 1, 4 );
			Assert.AreEqual( 1, ret.From );
			Assert.AreEqual( 4, ret.To );
			Assert.AreEqual( 5, ret.Until );
			Assert.AreEqual( 1, ret.StartIndex );
			Assert.AreEqual( 5, ret.EndIndex );
			Assert.AreEqual( 4, ret.Length );
			Assert.AreEqual( 4, ret.Size );
		}

		[TestMethod]
		public void Range_1FromUntil()
		{
			var ret = Range.FromUntil( 1, 5 );
			Assert.AreEqual( 1, ret.From );
			Assert.AreEqual( 4, ret.To );
			Assert.AreEqual( 5, ret.Until );
			Assert.AreEqual( 1, ret.StartIndex );
			Assert.AreEqual( 5, ret.EndIndex );
			Assert.AreEqual( 4, ret.Length );
			Assert.AreEqual( 4, ret.Size );
		}

		[TestMethod]
		public void Range_2FromFor()
		{
			var ret = Range.FromFor( 1, 4 );
			Assert.AreEqual( 1, ret.From );
			Assert.AreEqual( 4, ret.To );
			Assert.AreEqual( 5, ret.Until );
			Assert.AreEqual( 1, ret.StartIndex );
			Assert.AreEqual( 5, ret.EndIndex );
			Assert.AreEqual( 4, ret.Length );
			Assert.AreEqual( 4, ret.Size );
		}

		[TestMethod]
		public void Range_3OutOfRange()
		{
			Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
			{
				var ret = Range.FromTo( 3, 2 );
			} );
		}

		[TestMethod]
		public void Range_4CheckIndexRange()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort i = 0; i <= 2; ++i )
			{
				for( ushort j = 4; j <= 6; ++j )
				{
					ret.CheckIndexRange( Range.FromTo( i, j ) );
				}
			}
		}

		[TestMethod]
		public void Range_5CheckIndexOutOfRange()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort i = 3; i <= 5; ++i )
			{
				Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
				{
					ret.CheckIndexRange( Range.FromTo( i, 4 ) );
				} );
			}
			for( ushort j = 0; j <= 3; ++j )
			{
				Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
				{
					ret.CheckIndexRange( Range.FromTo( 2, j ) );
				} );
			}
		}

		[TestMethod]
		public void Range_6CheckLengthRange()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort i = 0; i <= 3; ++i )
			{
				ret.CheckLengthRange( Range.FromTo( i, 3 ) );
			}
			for( ushort j = 3; j <= 5; ++j )
			{
				ret.CheckLengthRange( Range.FromTo( 3, j ) );
			}
		}

		[TestMethod]
		public void Range_7CheckLengthOutOfRange()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort i = 0; i <= 2; ++i )
			{
				Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
				{
					ret.CheckIndexRange( Range.FromTo( 0, i ) );
				} );
			}
			for( ushort j = 5; j <= 7; ++j )
			{
				Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
				{
					ret.CheckIndexRange( Range.FromTo( j, ushort.MaxValue ) );
				} );
			}
		}

		[TestMethod]
		public void Range_8CheckMaximumLength()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort j = 3; j <= 5; ++j )
			{
				ret.CheckMaximumLength( j );
			}
		}

		[TestMethod]
		public void Range_9CheckMaximumLengthOutOfRange()
		{
			var ret = Range.FromTo( 2, 4 );
			for( ushort i = 0; i <= 2; ++i )
			{
				Assert2.ThrowsException<ArgumentOutOfRangeException>( () =>
				{
					ret.CheckMaximumLength( i );
				} );
			}
		}
	}
}