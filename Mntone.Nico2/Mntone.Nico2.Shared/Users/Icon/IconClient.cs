using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Storage.Streams;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_APP
		public static Task<IBuffer> GetIconAsync( NiconicoContext context, uint userID )
		{
			return context.GetClient().GetBufferAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 10000, userID ) )
#else
		public static Task<byte[]> GetIconAsync( NiconicoContext context, uint userID )
		{
			return context.GetClient().GetByteArrayAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 10000, userID ) )
#endif
				.ContinueWith( prevTask =>
				{
					try
					{
						return prevTask.Result;
					}
					catch( AggregateException ex )
					{
						if( ex.HResult != -2146233088 )
						{
							throw;
						}
					}
#if WINDOWS_APP
					return context.GetClient().GetBufferAsync( NiconicoUrls.UserBlankIconUrl ).Result;
#else
					return context.GetClient().GetByteArrayAsync( NiconicoUrls.UserBlankIconUrl ).Result;
#endif
				} );
		}
	}
}