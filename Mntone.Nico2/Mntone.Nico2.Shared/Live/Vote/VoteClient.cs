using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Vote
{
	internal sealed class VoteClient
	{
		public static Task<string> VoteDataAsync( NiconicoContext context, string requestId, ushort choiceNumber )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}
			if( choiceNumber > 8 )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync(
				NiconicoUrls.LiveVoteUrl + "?v=" + requestId + "&id=" + choiceNumber );
		}

		public static bool ParseVoteData( string voteData )
		{
			if( voteData.StartsWith( "status=" ) )
			{
				return voteData.Substring( 7 ) == "true";
			}
			throw CustomExceptionFactory.Create( NiconicoHResult.E_PARSE );
		}

		public static Task<bool> VoteAsync( NiconicoContext context, string requestId, ushort choiceNumber )
		{
			return VoteDataAsync( context, requestId, choiceNumber )
				.ContinueWith( prevTask => ParseVoteData( prevTask.Result ) );
		}
	}
}