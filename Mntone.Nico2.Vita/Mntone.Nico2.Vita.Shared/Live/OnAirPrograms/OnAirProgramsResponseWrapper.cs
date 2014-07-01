using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.OnAirPrograms
{
	[DataContract]
	internal sealed class OnAirProgramsResponseWrapper
	{
		private OnAirProgramsResponseWrapper()
		{ }

		[DataMember( Name = "nicolive_video_response" )]
		public OnAirProgramsResponse Response { get; private set; }
	}
}