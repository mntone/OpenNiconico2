using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.MyPage
{
	internal sealed class MyPageClient
	{
		public static Task<string> GetMyPageDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.LiveMyPageUrl );
		}

		public static MyPageResponse ParseMyPageData( string myPageData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( myPageData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );
			if( language == "ja-jp" && myPageData.Contains( "locale=\"en-us\"" ) )
			{
				language = "en-us";
			}
			return new MyPageResponse(
				htmlHtml.Element( "body" ).GetElementById( "all_cover" ).GetElementById( "all" ).GetElementByClassName( "container" ).GetElementById( "liveList" ),
				language );
		}

		public static Task<MyPageResponse> GetMyPageAsync( NiconicoContext context )
		{
			return GetMyPageDataAsync( context )
				.ContinueWith( prevTask => ParseMyPageData( prevTask.Result ) );
		}
	}
}