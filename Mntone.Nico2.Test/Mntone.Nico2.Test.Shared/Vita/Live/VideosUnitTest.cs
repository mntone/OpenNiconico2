using Mntone.Nico2.Live;
using Mntone.Nico2.Vita;
using Mntone.Nico2.Vita.Live.Videos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test.Vita.Live
{
	[TestClass]
	public sealed class VideosUnitTest
	{
		private void CheckMethod( string data )
		{
			var actual = VideosClient.ParseVideosData( data );
			var expected = JObject.Parse( data )["nicolive_video_response"];

			var expectedProgramsInfo = expected["video_info"].AsJEnumerable();
			for( var i = 0; i < actual.Programs.Count; ++i )
			{
				var actualProgramInfo = actual.Programs[i];
				var expectedProgramInfo = expectedProgramsInfo[i];

				LiveAssert.CheckSimpleVideo( expectedProgramInfo["video"], actualProgramInfo.Video );
				LiveAssert.CheckSimpleCommunity( expectedProgramInfo["community"], actualProgramInfo.Community );
				Assert.IsNull( actualProgramInfo.Tags );
			}

			Assert.AreEqual( expected["count"].Value<ushort>(), actual.ParticalCount );
		}

		[TestMethod]
		public void Videos_0不正なデータ()
		{
			Assert2.ThrowsException<ArgumentException>( () =>
			{
				VideosClient.GetVideosDataAsync( new NiconicoVitaContext(), new List<string>() { "lv9", "sm9" } ).GetAwaiter().GetResult();
			} );
		}

		[TestMethod]
		public void Videos_1データ()
		{
			var data = TestHelper.Load( @"Vita/Live/Videos/data.json" );
			CheckMethod( data );
		}
	}
}