using System;

namespace Mntone.Nico2.Videos.Thumbnail
{
	internal static class MovieTypeExtensions
	{
		public static MovieType ToMovieType( this string value )
		{
			switch( value )
			{
			case "flv":
				return MovieType.Flv;
			case "mp4":
				return MovieType.Mp4;
			case "swf":
				return MovieType.Swf;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}