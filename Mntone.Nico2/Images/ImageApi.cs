
namespace Mntone.Nico2.Images
{
	/// <summary>
	/// ニコニコ静画 API 群
	/// </summary>
	public sealed class ImageApi
	{
		internal ImageApi( NiconicoContext context )
		{
			this._context = context;
		}


		#region property (and related field)

		/// <summary>
		/// ニコニコ動画のユーザー API 群
		/// </summary>
		public Users.UserApi User
		{
			get { return this._User ?? ( this._User = new Users.UserApi( _context ) ); }
		}
		private Users.UserApi _User = null;

		#endregion


		#region field

		private NiconicoContext _context;

		#endregion
	}
}