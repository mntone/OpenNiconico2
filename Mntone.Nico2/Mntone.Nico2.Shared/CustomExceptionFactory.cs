using System;

namespace Mntone.Nico2
{
	internal static class CustomExceptionFactory
	{
		class CustomException
			: Exception
		{
			public CustomException( int hResult )
				: base()
			{
				this.HResult = hResult;
			}

			public CustomException( string message, int hResult )
				: base( message )
			{
				this.HResult = hResult;
			}
		}

		public static Exception Create( int hResult )
		{
			return new CustomException( hResult );
		}

		public static Exception Create( string message, int hResult )
		{
			return new CustomException( message, hResult );
		}
	}
}