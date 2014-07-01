using System;

namespace Mntone.Nico2.Live
{
	internal static class CommunityTypeExtensions
	{
		public static CommunityType ToCommunityType( this string value )
		{
			switch( value )
			{
			case "official":
				return CommunityType.Official;
			case "community":
				return CommunityType.Community;
			case "channel":
				return CommunityType.Channel;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static string ToCommunityTypeString( this CommunityType value )
		{
			switch( value )
			{
			case CommunityType.Official:
				return "official";
			case CommunityType.Community:
				return "community";
			case CommunityType.Channel:
				return "channel";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}