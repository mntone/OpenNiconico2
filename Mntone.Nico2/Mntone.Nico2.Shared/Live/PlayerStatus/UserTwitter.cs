using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザーの Twitter 情報を格納するクラス
	/// </summary>
	public sealed class UserTwitter
	{
#if WINDOWS_APP
		internal UserTwitter( IXmlNode twitterInfoXml )
#else
		internal UserTwitter( XElement twitterInfoXml )
#endif
		{
			IsEnabled = twitterInfoXml.GetNamedChildNodeText( "status" ) == "enabled";
			ScreenName = twitterInfoXml.GetNamedChildNodeText( "screen_name" );
			FollowersCount = twitterInfoXml.GetNamedChildNodeText( "followers_count" ).ToUInt();
			IsVip = twitterInfoXml.GetNamedChildNodeText( "is_vip" ).ToBooleanFrom1();
			ProfileImageUrl = twitterInfoXml.GetNamedChildNodeText( "profile_image_url" ).ToUri();
			IsAuthenticationRequired = twitterInfoXml.GetNamedChildNodeText( "after_auth" ).ToBooleanFrom1();
			Token = twitterInfoXml.GetNamedChildNodeText( "tweet_token" );
		}

		/// <summary>
		/// Twitter 情報が有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// スクリーン ネーム
		/// </summary>
		public string ScreenName { get; private set; }

		/// <summary>
		/// フォロワー数
		/// </summary>
		public uint FollowersCount { get; private set; }

		/// <summary>
		/// VIP アカウントか
		/// </summary>
		public bool IsVip { get; private set; }

		/// <summary>
		/// 画像 URL
		/// </summary>
		public Uri ProfileImageUrl { get; private set; }

		/// <summary>
		/// 認証が必要か
		/// </summary>
		public bool IsAuthenticationRequired { get; private set; }

		/// <summary>
		/// トークン
		/// </summary>
		public string Token { get; private set; }
	}
}