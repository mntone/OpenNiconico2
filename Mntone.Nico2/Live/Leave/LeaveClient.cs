using System;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.Leave
{
	internal sealed class LeaveClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> LeaveDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.LiveLeaveUrl + "?v=" + requestID ) );
		}

		public static bool ParseLeaveData( string leaveData )
		{
			return leaveData == "true";
		}

		public static IAsyncOperation<bool> LeaveAsync( NiconicoContext context, string requestID )
		{
			return LeaveDataAsync( context, requestID )
				.AsTask()
				.ContinueWith( prevTask => ParseLeaveData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}