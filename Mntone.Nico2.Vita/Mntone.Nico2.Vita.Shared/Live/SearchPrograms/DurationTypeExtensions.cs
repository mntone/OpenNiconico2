using System;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	internal static class DurationTypeExtensions
	{
		public static string ToDurationTypeString( this DurationType type )
		{
			switch( type )
			{
			case DurationType.OnAir:
				return "onair";
			case DurationType.Reserved:
				return "reserved";
			case DurationType.Closed:
				return "closed";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}