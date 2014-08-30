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

		internal static string PatchJson( string jsonData )
		{
			return jsonData.Replace( "\"community\":\"\"", "\"community\":null" );
		}

		internal static string PatchJson2( string jsonData )
		{
			if( jsonData.Contains( "\"count\":\"1\"" ) )
			{
				jsonData = jsonData.Replace( "\"video_info\":{\"", "\"video_info\":[{\"" ).Replace( "},\"count\":\"", "}],\"count\":\"" );
			}
			return PatchJson( jsonData );
		}
	}
}