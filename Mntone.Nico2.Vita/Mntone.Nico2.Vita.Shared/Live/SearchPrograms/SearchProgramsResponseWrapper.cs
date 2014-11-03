using System.Runtime.Serialization;

namespace Mntone.Nico2.Vita.Live.SearchPrograms
{
	[DataContract]
    internal sealed class SearchProgramsResponseWrapper
	{
		private SearchProgramsResponseWrapper()
		{ }

		[DataMember( Name = "nicolive_video_response" )]
		public SearchProgramsResponse Response { get; private set; }
    }
}