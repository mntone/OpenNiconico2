using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.UserInfo
{
	internal sealed class UserInfoClient
	{
		public static Task<string> GetUserInfoDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedString2Async( NiconicoUrls.UserPageUrl );
		}

		public static UserInfoResponse ParseUserInfoData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );
			return new UserInfoResponse( htmlHtml.Element( "body" ), language );
		}

		public static Task<UserInfoResponse> GetUserInfoAsync( NiconicoContext context )
		{
			return GetUserInfoDataAsync( context )
				.ContinueWith( prevTask => ParseUserInfoData( prevTask.Result ) );
		}
	}
}