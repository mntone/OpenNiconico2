using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Description
{
	internal sealed class DescriptionClient
	{
		public static Task<string> GetDescriptionDataAsync( NiconicoContext context, string requestID )
		{
			if( !NiconicoRegex.IsLiveID( requestID ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetConvertedString2Async( NiconicoUrls.LiveGatePageUrl + requestID );
		}

		public static DescriptionResponse ParseDescriptionData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );
			return new DescriptionResponse( htmlHtml.Element( "body" ).GetElementById( "all_cover" ).GetElementById( "all" ), language );
		}

		public static Task<DescriptionResponse> GetDescriptionAsync( NiconicoContext context, string requestID )
		{
			return GetDescriptionDataAsync( context, requestID )
				.ContinueWith( prevTask => ParseDescriptionData( prevTask.Result ) );
		}
	}
}