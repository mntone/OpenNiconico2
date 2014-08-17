using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Storage.Streams;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Communities.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_APP
		public static Task<IBuffer> GetIconAsync( NiconicoContext context, uint communityID )
		{
			return context.GetClient().GetBufferAsync( string.Format( NiconicoUrls.CommunityIconUrl, communityID / 10000, communityID ) )
#else
		public static Task<byte[]> GetIconAsync( NiconicoContext context, uint communityID )
		{
			return context.GetClient().GetByteArrayAsync( string.Format( NiconicoUrls.CommunityIconUrl, communityID / 10000, communityID ) )
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
					return context.GetClient().GetBufferAsync( NiconicoUrls.CommunityBlankIconUrl ).Result;
#else
					return context.GetClient().GetByteArrayAsync( NiconicoUrls.CommunityBlankIconUrl ).Result;
#endif
				} );
		}
	}
}