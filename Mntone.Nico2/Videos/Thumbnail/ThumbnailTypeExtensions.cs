using System;

namespace Mntone.Nico2.Videos.Thumbnail
{
	internal static class ThumbnailTypeExtensions
	{
		public static ThumbnailType ToThumbnailType( this string value )
		{
			switch( value )
			{
			case "video":
				return ThumbnailType.Video;
			case "mymemory":
				return ThumbnailType.MyMemory;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}