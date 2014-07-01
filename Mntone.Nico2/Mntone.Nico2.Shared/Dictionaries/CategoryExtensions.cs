using System;

namespace Mntone.Nico2.Dictionaries
{
	internal static class CategoryExtensions
	{
		public static char ToCategoryChar( this Category category )
		{
			switch( category )
			{
			case Category.Word:
				return 'a';
			case Category.Video:
				return 'v';
			case Category.Live:
				return 'l';
			case Category.Community:
				return 'c';
			case Category.User:
				return 'u';
			case Category.Goods:
				return 'i';
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static Category ToCategory( this char categoryString )
		{
			switch( categoryString )
			{
			case 'a':
				return Category.Word;
			case 'v':
				return Category.Video;
			case 'l':
				return Category.Live;
			case 'c':
				return Category.Community;
			case 'u':
				return Category.User;
			case 'i':
				return Category.Goods;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}