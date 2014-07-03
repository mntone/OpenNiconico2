
namespace Mntone.Nico2.Live.PlayerStatus
{
	internal static class VideoAspectExtensions
	{
		public static VideoAspect ToVideoAspect( this string value )
		{
			switch( value )
			{
			case "4:3":
				return VideoAspect.Normal;
			case "16:9":
				return VideoAspect.Wide;
			case "raw":
				return VideoAspect.Raw;
			default:
				return VideoAspect.Auto;
			}
		}
	}
}