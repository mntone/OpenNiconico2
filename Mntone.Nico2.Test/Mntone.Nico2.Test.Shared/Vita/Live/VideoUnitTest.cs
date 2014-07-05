using Mntone.Nico2.Live;
using Mntone.Nico2.Vita;
using Mntone.Nico2.Vita.Live.Video;
using Newtonsoft.Json.Linq;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Vita.Live
{
	[TestClass]
	public sealed class VideoUnitTest
	{
		private void CheckMethod( string data )
		{
			var actual = VideoClient.ParseVideoData( data );
			var expected = JObject.Parse( data )["nicolive_video_response"];

			var expectedProgramInfo = expected["video_info"];
			LiveAssert.CheckDetailVideo( expectedProgramInfo["video"], actual.Program.Video );
			LiveAssert.CheckDetailCommunity( expectedProgramInfo["community"], actual.Program.Community );
			LiveAssert.CheckTags( expectedProgramInfo["livetags"], actual.Program.Tags );
		}

		[TestMethod]
		public void Videos_0不正なデータ()
		{
			Assert2.ThrowsException<ArgumentException>( () =>
			{
				VideoClient.GetVideoDataAsync( new NiconicoVitaContext(), "sm9" ).GetAwaiter().GetResult();
			} );
		}

		[TestMethod]
		public void Video_1officialデータ()
		{
			var data = TestHelper.Load( @"Vita/Live/Video/official.json" );
			CheckMethod( data );
		}

		[TestMethod]
		public void Video_2userデータ()
		{
			var data = TestHelper.Load( @"Vita/Live/Video/user.json" );
			CheckMethod( data );
		}
	}
}