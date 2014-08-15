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
			return context.GetClient()
				.GetBufferAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 10000, userID ) )
				.ContinueWith( prevTask =>
				{
					try
					{
						return prevTask.Result;
					}
					catch( Exception ex )
					{
						if( ex.HResult != -2146233088 )
						{
							throw;
						}
					}
					return context.GetClient().GetBufferAsync( NiconicoUrls.UserBlankIconUrl ).Result;
				} );
		}
#else
		public static Task<byte[]> GetIconAsync( NiconicoContext context, uint userID )
		{
			return context.GetClient()
				.GetByteArrayAsync( string.Format( NiconicoUrls.UserIconUrl, userID / 1000, userID ) )
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
					return context.GetClient().GetByteArrayAsync( NiconicoUrls.UserBlankIconUrl ).Result;
				} );
		}
#endif
	}
}