using HtmlAgilityPack;
using System.Linq;

namespace Mntone.Nico2.Users.Info
{
	/// <summary>
	/// ユーザー情報を格納するクラス
	/// </summary>
	public sealed class InfoResponse
	{
		internal InfoResponse( HtmlNode bodyHtml, string language )
		{
			var profileHtml = bodyHtml.GetElementByClassName( "userDetail" ).GetElementByClassName( "profile" );
			{
				var h2Html = profileHtml.Element( "h2" );
				this.Name = h2Html.FirstChild.InnerText;
			}

			{
				var accountIDHtml = profileHtml.GetElementByClassName( "account" ).GetElementByClassName( "accountNumber" ).Element( "span" );
				var keywords = accountIDHtml.InnerText.Split( new char[] { ' ', '(', ')' } );
				if( keywords.Count() >= 4 )
				{
					switch( language )
					{
					case "ja-jp":
						this.Id = keywords[0].ToUInt();
						this.JoinedVersion = keywords[1];
						this.IsPremium = keywords[3] == "プレミアム会員";
						break;
					case "en-us":
						this.Id = keywords[0].ToUInt();
						this.JoinedVersion = keywords[1];
						this.IsPremium = keywords[3] == "Premium";
						break;
					case "zh-tw":
						this.Id = keywords[0].ToUInt();
						this.JoinedVersion = keywords[1];
						this.IsPremium = keywords[3] == "白金會員";
						break;
					}
				}
			}

			{
				var statsHtml = profileHtml.GetElementByClassName( "stats" );
				this.FavoriteCount = statsHtml.GetElementByClassName( "fav" ).FirstChild.InnerText.ToUShort();

				var stampText = statsHtml.GetElementByClassName( "exp" ).FirstChild.InnerText;
				this.StampCount = stampText.Substring( 0, stampText.Length - 3 ).ToUShort();

				var nicoruText = statsHtml.GetElementByClassName( "nicoru" ).FirstChild.ChildNodes[1].InnerText;
				if( nicoruText != "-" )
				{
					this.NicoruCount = nicoruText.ToUShort();
				}

				var pointsText = statsHtml.GetElementByClassName( "nicopoint" ).FirstChild.InnerText;
				this.Points = pointsText.Substring( 0, pointsText.Length - 2 ).ToUInt();

				var creatorScoreText = statsHtml.GetElementByClassName( "cpp" ).FirstChild.InnerText;
				switch( language )
				{
				case "ja-jp":
				case "zh-tw":
					this.CreatorScore = creatorScoreText.Substring( 0, creatorScoreText.Length - 1 ).ToUInt();
					break;
				case "en-us":
					this.CreatorScore = creatorScoreText.Substring( 0, creatorScoreText.Length - 7 ).ToUInt();
					break;
				default:
					break;
				}
			}
		}

		/// <summary>
		/// 名前
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint Id { get; private set; }

		/// <summary>
		/// アカウントが加入したときのバージョン
		/// </summary>
		public string JoinedVersion { get; private set; }

		/// <summary>
		/// プレミアムか
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// お気に入り登録された数
		/// </summary>
		public ushort FavoriteCount { get; private set; }

		/// <summary>
		/// スタンプ数
		/// </summary>
		public ushort StampCount { get; private set; }

		/// <summary>
		/// ニコる数
		/// </summary>
		public ushort NicoruCount { get; private set; }

		/// <summary>
		/// ニコニコポイント数
		/// </summary>
		public uint Points { get; private set; }

		/// <summary>
		/// クリエイター推奨スコア
		/// </summary>
		public uint CreatorScore { get; private set; }
	}
}
