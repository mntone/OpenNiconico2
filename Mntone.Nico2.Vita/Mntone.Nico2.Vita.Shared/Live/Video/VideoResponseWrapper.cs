using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.Video
{
	[DataContract]
	internal sealed class VideoResponseWrapper
	{
		private VideoResponseWrapper()
		{ }

		[DataMember( Name = "nicolive_video_response" )]
		public VideoResponse Response { get; private set; }
	}
}