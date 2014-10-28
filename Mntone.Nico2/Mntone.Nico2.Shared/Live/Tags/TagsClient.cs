using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Tags
{
	internal sealed class TagsClient
	{
		public static Task<string> GetTagsDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.LiveGatePageUrl + requestId );
		}

		public static TagsResponse ParseTagsData( string tagsData )
		{
			var ulHtml = new HtmlDocument();
			ulHtml.LoadHtml( tagsData );

			return new TagsResponse( ulHtml.DocumentNode );
		}

		public static Task<TagsResponse> GetTagsAsync( NiconicoContext context, string requestId )
		{
			return GetTagsDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseTagsData( prevTask.Result ) );
		}
	}
}