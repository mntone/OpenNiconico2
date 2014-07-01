using System;

namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコ セッションを管理します
	/// </summary>
	public sealed class NiconicoSession
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		public NiconicoSession()
		{ }

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="key">セッション キー</param>
		/// <param name="expires">セッションの有効期限</param>
		public NiconicoSession( string key, DateTimeOffset expires )
		{
			this.Key = key;
			this.Expires = expires;
		}

		/// <summary>
		/// セッション キー
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// セッションの有効期限
		/// </summary>
		public DateTimeOffset Expires
		{
			get { return _Expires; }
			set
			{
				if( value < DateTimeOffset.Now )
				{
					throw new Exception( "Expires is out of range." );
				}

				this._Expires = value;
			}
		}
		private DateTimeOffset _Expires = DateTimeOffset.MinValue;

		/// <summary>
		/// ユーザー権限
		/// </summary>
		public NiconicoAccountAuthority AccountAuthority
		{
			get { return this._AccountAuthority; }
			internal set { this._AccountAuthority = value; }
		}
		private NiconicoAccountAuthority _AccountAuthority = NiconicoAccountAuthority.NotLoggedOn;

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; internal set; }
	}
}