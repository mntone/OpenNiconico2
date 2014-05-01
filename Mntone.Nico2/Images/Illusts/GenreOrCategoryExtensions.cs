using System;

namespace Mntone.Nico2.Images.Illusts
{
	internal static class GenreOrCategoryExtensions
	{
		public static string ToGenreAndCategoryString( this GenreOrCategory value )
		{
			switch( value )
			{
			case GenreOrCategory.Creation:
				return "g_creation";
			case GenreOrCategory.Original:
				return "original";
			case GenreOrCategory.Portrait:
				return "portrait";
			case GenreOrCategory.FanArt:
				return "g_fanart";
			case GenreOrCategory.Anime:
				return "anime";
			case GenreOrCategory.Game:
				return "game";
			case GenreOrCategory.Character:
				return "character";
			case GenreOrCategory.Popular:
				return "g_popular";
			case GenreOrCategory.Toho:
				return "toho";
			case GenreOrCategory.Vocaloid:
				return "vocaloid";
			case GenreOrCategory.KanColle:
				return "kancolle";
			case GenreOrCategory.Adult:
				return "g_adult";
			case GenreOrCategory.Rate15:
				return "r15";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}