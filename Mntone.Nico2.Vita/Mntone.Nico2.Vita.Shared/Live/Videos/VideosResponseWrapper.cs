using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.Videos
{
	[DataContract]
	internal sealed class VideosResponseWrapper
	{
		private VideosResponseWrapper()
		{ }

		[DataMember( Name = "nicolive_video_response" )]
		public VideosResponse Response { get; private set; }
	}
}