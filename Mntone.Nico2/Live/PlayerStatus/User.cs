using System.Linq;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザー情報を格納するクラス
	/// </summary>
	public sealed class User
	{
		internal User( IXmlNode streamXml, IXmlNode userXml )
		{
			ID = userXml.GetNamedChildNode( "user_id" ).InnerText.ToUInt();
			Name = userXml.GetNamedChildNode( "nickname" ).InnerText;
			IsPremium = userXml.GetNamedChildNode( "is_premium" ).InnerText.ToBooleanFrom1();
			Age = userXml.GetNamedChildNode( "userAge" ).InnerText.ToUShort();
			Sex = userXml.GetNamedChildNode( "userSex" ).InnerText.ToBooleanFrom1() ? Sex.Male : Sex.Female;
			Domain = userXml.GetNamedChildNode( "userDomain" ).InnerText;
			Prefecture = ( Prefecture )userXml.GetNamedChildNode( "userPrefecture" ).InnerText.ToInt();
			Language = userXml.GetNamedChildNode( "userLanguage" ).InnerText;

			var hKeyXml = streamXml.ChildNodes.Where( node => node.NodeName == "hkey" ).SingleOrDefault();
			HKey = hKeyXml != null ? hKeyXml.InnerText : string.Empty;

			IsOwner = streamXml.GetNamedChildNode( "is_owner" ).InnerText.ToBooleanFrom1();
			
			var isJoinXml = streamXml.ChildNodes.Where( node => node.NodeName == "is_join" ).SingleOrDefault();
			IsJoin = isJoinXml != null ? isJoinXml.InnerText.ToBooleanFrom1() : false;

			IsReserved = streamXml.GetNamedChildNode( "is_timeshift_reserved" ).InnerText.ToBooleanFrom1();

			var isPriorityPrefectureXml = streamXml.ChildNodes.Where( node => node.NodeName =="is_priority_prefecture" ).SingleOrDefault();
			IsPrefecturePreferential = isPriorityPrefectureXml != null ? isPriorityPrefectureXml.InnerText.ToBooleanFrom1() : false;

			var productPurchasedXml = streamXml.ChildNodes.Where( node => node.NodeName == "product_purchased" ).SingleOrDefault();
			if( productPurchasedXml != null )
			{
				IsPurchased = productPurchasedXml.InnerText.ToBooleanFrom1();
				IsSerialUsing = streamXml.GetNamedAttribute( "is_serial_stream" ).InnerText.ToBooleanFrom1();
			}

			Twitter = new UserTwitter( userXml.GetNamedChildNode( "twitter_info" ) );
		}

		/// <summary>
		/// ID
		/// </summary>
		public uint ID { get; private set; }

		/// <summary>
		/// 名前
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// プレミアム会員か
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// 年齢
		/// </summary>
		public ushort Age { get; private set; }

		/// <summary>
		/// 男性か
		/// </summary>
		public bool IsMale { get { return Sex == Sex.Male; } }

		/// <summary>
		/// 女性か
		/// </summary>
		public bool IsFemale { get { return Sex == Sex.Female; } }

		/// <summary>
		/// 性別
		/// </summary>
		public Sex Sex { get; private set; }

		/// <summary>
		/// ドメイン
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// 都道府県
		/// </summary>
		public Prefecture Prefecture { get; private set; }

		/// <summary>
		/// 言語
		/// </summary>
		public string Language { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		public string HKey { get; private set; }


		/// <summary>
		/// コミュニティーのオーナーか
		/// </summary>
		public bool IsOwner { get; private set; }

		/// <summary>
		/// コミュニティーに参加しているか
		/// </summary>
		public bool IsJoin { get; private set; }

		/// <summary>
		/// タイムシフト予約したか
		/// </summary>
		public bool IsReserved { get; private set; }

		/// <summary>
		/// 都道府県が優先的か
		/// </summary>
		public bool IsPrefecturePreferential { get; private set; }

		/// <summary>
		/// 購入または利用したか
		/// </summary>
		public bool IsPurchased { get; private set; }

		/// <summary>
		/// シリアル コードを使ったか
		/// </summary>
		/// <remarks>
		/// メッセージを変更するのに用いられる
		/// </remarks>
		public bool IsSerialUsing { get; private set; }


		/// <summary>
		/// Twitter 情報
		/// </summary>
		public UserTwitter Twitter { get; private set; }
	}
}