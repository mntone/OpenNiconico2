using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザー情報を格納するクラス
	/// </summary>
	public sealed class User
	{
#if WINDOWS_APP
		internal User( IXmlNode streamXml, IXmlNode userXml )
#else
		internal User( XElement streamXml, XElement userXml )
#endif
		{
			ID = userXml.GetNamedChildNodeText( "user_id" ).ToUInt();
			Name = userXml.GetNamedChildNodeText( "nickname" );
			IsPremium = userXml.GetNamedChildNodeText( "is_premium" ).ToBooleanFrom1();
			Age = userXml.GetNamedChildNodeText( "userAge" ).ToUShort();
			Sex = userXml.GetNamedChildNodeText( "userSex" ).ToBooleanFrom1() ? Sex.Male : Sex.Female;
			Domain = userXml.GetNamedChildNodeText( "userDomain" );
			Prefecture = ( Prefecture )userXml.GetNamedChildNodeText( "userPrefecture" ).ToInt();
			Language = userXml.GetNamedChildNodeText( "userLanguage" );
			HKey = streamXml.GetNamedChildNodeText( "hkey" );
			IsOwner = streamXml.GetNamedChildNodeText( "is_owner" ).ToBooleanFrom1();
			IsJoin = userXml.GetNamedChildNodeText( "is_join" ).ToBooleanFrom1();
			IsReserved = streamXml.GetNamedChildNodeText( "is_timeshift_reserved" ).ToBooleanFrom1();
			IsPrefecturePreferential = streamXml.GetNamedChildNodeText( "is_priority_prefecture" ).ToBooleanFrom1();

			var productPurchasedXml = streamXml.GetNamedChildNodeText( "product_purchased" );
			if( !string.IsNullOrEmpty( productPurchasedXml ) )
			{
				IsPurchased = productPurchasedXml.ToBooleanFrom1();
				IsSerialUsing = streamXml.GetNamedAttributeText( "is_serial_stream" ).ToBooleanFrom1();
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