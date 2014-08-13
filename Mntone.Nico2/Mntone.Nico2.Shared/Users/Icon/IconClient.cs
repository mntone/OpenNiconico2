using HtmlAgilityPack;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Storage.Streams;
#endif

namespace Mntone.Nico2.Users.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_APP
		public static Task<IBuffer> GetIconDataAsync( NiconicoContext context, uint userID )
		{
			return context.GetClient().GetBufferAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 1000, userID ) );
		}

		public static Task<IBuffer> GetIconAsync( NiconicoContext context, uint userID )
		{
			return GetIconDataAsync( context, userID );
		}
#else
		public static Task<byte[]> GetIconDataAsync( NiconicoContext context, uint userID )
		{
			return context.GetClient().GetByteArrayAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 1000, userID ) );
		}

		public static Task<byte[]> GetIconAsync( NiconicoContext context, uint userID )
		{
			return GetIconDataAsync( context, userID );
		}
#endif
	}
}