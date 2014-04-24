using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Live.Leave;
using System;

namespace Mntone.Nico2.Test.Live
{
	[TestClass]
	public sealed class LeaveUnitTest
	{
		[TestMethod]
		public void Leave_0true()
		{
			var ret = LeaveClient.ParseLeaveData( "true" );
			Assert.IsTrue( ret );
		}

		[TestMethod]
		public void Leave_1false()
		{
			var ret = LeaveClient.ParseLeaveData( "false" );
			Assert.IsFalse( ret );
		}

		[TestMethod]
		public void Leave_2other()
		{
			var ret = LeaveClient.ParseLeaveData( "xyz" );
			Assert.IsFalse( ret );
		}
	}
}