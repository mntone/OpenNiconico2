using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.PostKey
{
	internal sealed class PostKeyClient
	{
		public static Task<string> GetPostKeyDataAsync( NiconicoContext context, uint threadId, uint blockNo )
		{
			return context.GetClient().GetStringAsync(
				NiconicoUrls.LivePostKeyUrl + "?thread=" + threadId + "&block_no=" + blockNo );
		}

		public static string ParsePostKeyData( string postKeyData )
		{
			if( postKeyData.StartsWith( "postkey=" ) )
			{
				return postKeyData.Substring( 8 );
			}
			throw CustomExceptionFactory.Create( NiconicoHResult.E_PARSE );
		}

		public static Task<string> GetPostKeyAsync( NiconicoContext context, uint threadId, uint blockNo )
		{
			return GetPostKeyDataAsync( context, threadId, blockNo )
				.ContinueWith( prevTask => ParsePostKeyData( prevTask.Result ) );
		}
	}
}