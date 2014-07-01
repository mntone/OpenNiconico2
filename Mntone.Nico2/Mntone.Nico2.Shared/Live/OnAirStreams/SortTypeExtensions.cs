using System;

namespace Mntone.Nico2.Live.OnAirStreams
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
			case SortType.CommentCount:
				return "comment_num";
			case SortType.CommunityLevel:
				return "community_level";
			case SortType.CommunityCreateTime:
				return "community_create_time";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}