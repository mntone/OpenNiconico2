using System;

namespace Mntone.Nico2.Live.OnAirStreams
{
	internal static class CategoryExtensions
	{
		public static string ToCategoryString( this Category category )
		{
			switch( category )
			{
			case Category.General:
				return "common";
			case Category.Challenge:
				return "try";
			case Category.Game:
				return "live";
			case Category.Introduction:
				return "req";
			case Category.FaceOut:
				return "face";
			case Category.Encounter:
				return "totu";
			case Category.Adult:
				return "r18";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}