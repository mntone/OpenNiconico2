using System;

namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
	internal static class DurationTypeExtensions
	{
		public static string ToDurationTypeString( this DurationType duration )
		{
			switch( duration )
			{
			case DurationType.Fresh:
				return "fresh";
			case DurationType.Hourly:
				return "hourly";
			case DurationType.Daily:
				return "daily";
			case DurationType.Weekly:
				return "weekly";
			case DurationType.Monthly:
				return "monthly";
			case DurationType.Total:
				return "total";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}