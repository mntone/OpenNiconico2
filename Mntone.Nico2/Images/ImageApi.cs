using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;

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


		#region field

		private NiconicoContext _context;

		#endregion
	}
}