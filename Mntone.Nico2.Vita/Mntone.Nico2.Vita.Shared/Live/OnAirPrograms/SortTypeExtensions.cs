using System;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	internal static class SortTypeExtensions
	{
		public static string ToSortTypeString( this SortType type )
		{
			switch( type )
			{
			case SortType.StartTime:
				return "start_time";
			case SortType.ViewCount:
				return "view_counter";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}