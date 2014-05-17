
namespace Mntone.Nico2.Vita
{
	internal static class RangeExtensions
	{
		public static string ToFromToString( this Range range )
		{
			return string.Format( "from={0}&to={1}", range.From, range.To );
		}

		public static string ToFromLimitString( this Range range )
		{
			return string.Format( "from={0}&limit={1}", range.From, range.Length );
		}
	}
}