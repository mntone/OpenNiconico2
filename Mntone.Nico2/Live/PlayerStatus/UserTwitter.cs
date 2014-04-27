using System;
using Windows.Data.Xml.Dom;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザーの Twitter 情報を格納するクラス
	/// </summary>
	public sealed class UserTwitter
	{
		internal UserTwitter( IXmlNode twitterInfoXml )
		{
			IsEnabled = twitterInfoXml.GetNamedChildNode( "status" ).InnerText == "enabled";
			ScreenName = twitterInfoXml.GetNamedChildNode( "screen_name" ).InnerText;
			FollowersCount = twitterInfoXml.GetNamedChildNode( "followers_count" ).InnerText.ToUInt();
			IsVip = twitterInfoXml.GetNamedChildNode( "is_vip" ).InnerText.ToBooleanFrom1();
			ProfileImageUrl = twitterInfoXml.GetNamedChildNode( "profile_image_url" ).InnerText.ToUri();
			IsAuthenticationRequired = twitterInfoXml.GetNamedChildNode( "after_auth" ).InnerText.ToBooleanFrom1();
			Token = twitterInfoXml.GetNamedChildNode( "tweet_token" ).InnerText;
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