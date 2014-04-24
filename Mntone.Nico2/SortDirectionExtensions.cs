using System;

namespace Mntone.Nico2
{
	internal static class SortDirectionExtensions
	{
		public static char ToChar( this SortDirection direction )
		{
			switch( direction )
			{
			case SortDirection.Ascending:
				return 'a';
			case SortDirection.Descending:
				return 'd';
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static string ToShortString( this SortDirection direction )
		{
			switch( direction )
			{
			case SortDirection.Ascending:
				return "asc";
			case SortDirection.Descending:
				return "desc";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}