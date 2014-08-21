using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Leave
{
	internal sealed class LeaveClient
	{
		public static Task<string> LeaveDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.LiveLeaveUrl + "?v=" + requestId );
		}

		public static bool ParseLeaveData( string leaveData )
		{
			return leaveData.ToBooleanFromString();
		}

		public static Task<bool> LeaveAsync( NiconicoContext context, string requestId )
		{
			return LeaveDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseLeaveData( prevTask.Result ) );
		}
	}
}