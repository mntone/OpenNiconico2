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
		public static Task<IBuffer> GetIconAsync( NiconicoContext context, string requestId )
#else
		public static Task<byte[]> GetIconAsync( NiconicoContext context, string requestId )
#endif
		{
			if( !NiconicoRegex.IsCommunityId( requestId ) )
			{
				throw new ArgumentException();
			}

			var communityNumber = requestId.Substring( 2 ).ToUInt();
			return context.GetClient()
#if WINDOWS_APP
				.GetBufferAsync( string.Format( NiconicoUrls.CommunityIconUrl, communityNumber / 10000, communityNumber ) )
#else
				.GetByteArrayAsync( string.Format( NiconicoUrls.CommunityIconUrl, communityNumber / 10000, communityNumber ) )
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