using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Leave
{
	internal sealed class LeaveClient
	{
		public static Task<string> LeaveDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetString2Async( NiconicoUrls.LiveLeaveUrl + "?v=" + requestID );
		}

		public static bool ParseLeaveData( string leaveData )
		{
			return leaveData.ToBooleanFromString();
		}

		public static Task<bool> LeaveAsync( NiconicoContext context, string requestID )
		{
			return LeaveDataAsync( context, requestID )
				.ContinueWith( prevTask => ParseLeaveData( prevTask.Result ) );
		}
	}
}