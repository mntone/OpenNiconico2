using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Images.Illusts
{
	/// <summary>
	/// ジャンルとカテゴリー
	/// </summary>
	public enum GenreOrCategory
	{
		/// <summary>
		/// 創作
		/// </summary>
		Creation = 100,

		/// <summary>
		/// オリジナル
		/// </summary>
		Original = 110,

		/// <summary>
		/// 似顔絵
		/// </summary>
		Portrait = 120,

		/// <summary>
		/// ファンアート
		/// </summary>
		FanArt = 200,

		/// <summary>
		/// アニメ
		/// </summary>
		Anime = 210,

		/// <summary>
		/// ゲーム
		/// </summary>
		Game = 220,

		/// <summary>
		/// キャラクター
		/// </summary>
		Character = 230,

		/// <summary>
		/// 殿堂入り
		/// </summary>
		Popular = 300,

		/// <summary>
		/// 東方
		/// </summary>
		Toho = 310,

		/// <summary>
		/// ボーカロイド
		/// </summary>
		Vocaloid = 320,

		/// <summary>
		/// 艦これ
		/// </summary>
		KanColle = 330,

		/// <summary>
		/// 春画 (R-15)
		/// </summary>
		Adult = -100,

		/// <summary>
		/// R-15
		/// </summary>
		Rate15 = -110,
	}
}