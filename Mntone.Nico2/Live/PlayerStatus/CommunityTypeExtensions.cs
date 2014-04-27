using System;

namespace Mntone.Nico2.Live.PlayerStatus
{
	internal static class CommunityTypeExtensions
	{
		public static CommunityType ToProviderType( this string value )
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

		public static string ToProviderTypeString( this CommunityType value )
		{
			switch( value )
			{
			case CommunityType.Official:
				return "offcial";
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