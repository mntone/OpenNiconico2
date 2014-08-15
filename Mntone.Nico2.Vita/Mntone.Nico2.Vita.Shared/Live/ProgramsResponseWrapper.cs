using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live
{
	[DataContract]
	internal sealed class ProgramsResponseWrapper
	{
		private ProgramsResponseWrapper()
		{ }

		[DataMember( Name = "nicolive_video_response" )]
		public ProgramsResponse Response { get; private set; }
	}
}