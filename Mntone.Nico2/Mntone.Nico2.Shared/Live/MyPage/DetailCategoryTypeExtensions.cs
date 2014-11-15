using System;

namespace Mntone.Nico2.Live.MyPage
{
	internal static class DetailCategoryTypeExtensions
	{
		const string GENERAL_IMAGE_URL = "img/10/cmn/icon/icon_common.gif?090827";
		const string POLITICS_IMAGE_URL = "img/10/cmn/icon/icon_politics.gif?090827";
		const string ANIMAL_IMAGE_URL = "mg/10/cmn/icon/icon_animal.gif?090827";
		const string COOKING_IMAGE_URL = "img/10/cmn/icon/icon_cooking.gif?090827";
		const string PERFORMANCE_IMAGE_URL = "img/10/cmn/icon/icon_play.gif?090827";
		const string SING_IMAGE_URL = "img/10/cmn/icon/icon_sing.gif?090827";
		const string DANCE_IMAGE_URL = "img/10/cmn/icon/icon_dance.gif?090827";
		const string DRAW_IMAGE_URL = "img/10/cmn/icon/icon_draw.gif?090827";
		const string LECTURE_IMAGE_URL = "img/10/cmn/icon/icon_lecture.gif?090827";
		const string GAME_IMAGE_URL = "img/10/cmn/icon/icon_live.gif?090827";
		const string INTRODUCTION_IMAGE_URL = "img/10/cmn/icon/icon_request.gif?090827";
		const string ADULT_IMAGE_URL = "img/10/cmn/icon/icon__r18_.gif?090827";

		public static DetailCategoryType ToDetailCategory( this string categorySrc )
		{
			switch( categorySrc )
			{
			case GENERAL_IMAGE_URL:
				return DetailCategoryType.General;
			case POLITICS_IMAGE_URL:
				return DetailCategoryType.Politics;
			case ANIMAL_IMAGE_URL:
				return DetailCategoryType.Animal;
			case COOKING_IMAGE_URL:
				return DetailCategoryType.Cooking;
			case PERFORMANCE_IMAGE_URL:
				return DetailCategoryType.Performance;
			case SING_IMAGE_URL:
				return DetailCategoryType.Sing;
			case DANCE_IMAGE_URL:
				return DetailCategoryType.Dance;
			case DRAW_IMAGE_URL:
				return DetailCategoryType.Draw;
			case LECTURE_IMAGE_URL:
				return DetailCategoryType.Lecture;
			case GAME_IMAGE_URL:
				return DetailCategoryType.Game;
			case INTRODUCTION_IMAGE_URL:
				return DetailCategoryType.Introduction;
			case ADULT_IMAGE_URL:
				return DetailCategoryType.Adult;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}