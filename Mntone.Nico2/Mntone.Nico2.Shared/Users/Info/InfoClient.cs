using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Info
{
	internal sealed class InfoClient
	{
		public static Task<string> GetUserInfoDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedString2Async( NiconicoUrls.UserPageUrl );
		}

		public static InfoResponse ParseUserInfoData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );
			return new InfoResponse( htmlHtml.Element( "body" ), language );
		}

		public static Task<InfoResponse> GetUserInfoAsync( NiconicoContext context )
		{
			return GetUserInfoDataAsync( context )
				.ContinueWith( prevTask => ParseUserInfoData( prevTask.Result ) );
		}
	}
}