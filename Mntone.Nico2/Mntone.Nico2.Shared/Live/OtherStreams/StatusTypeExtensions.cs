using System;

namespace Mntone.Nico2.Live.OtherStreams
{
	internal static class StatusTypeExtensions
	{
		public static StatusType ToStatusType( this string value )
		{
			switch(value)
			{
			case "comingsoon":
				return StatusType.ComingSoon;
			case "closed":
				return StatusType.Closed;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static string ToStatusTypeString( this StatusType value )
		{
			switch( value )
			{
			case StatusType.ComingSoon:
				return "comingsoon";
			case StatusType.Closed:
				return "closed";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}