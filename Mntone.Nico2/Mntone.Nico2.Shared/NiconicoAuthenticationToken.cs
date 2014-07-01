
namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコのログオン トークンを管理します
	/// </summary>
	public sealed class NiconicoAuthenticationToken
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		public NiconicoAuthenticationToken()
		{ }
		
		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="mailOrTelephone">メールアドレス または 電話番号</param>
		/// <param name="password">パスワード</param>
		public NiconicoAuthenticationToken( string mailOrTelephone, string password )
		{
			this.MailOrTelephone = mailOrTelephone;
			this.Password = password;
		}


		#region property
		
		/// <summary>
		/// メールアドレス または 電話番号
		/// </summary>
		public string MailOrTelephone { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string Password { get; set; }

		#endregion
	}
}