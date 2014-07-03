using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
	internal sealed class BlogPartsRankingClient
	{
		public static Task<string> GetRankingDataAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return context.GetClient().GetString2Async(
				NiconicoUrls.ImageBlogPartsUrl
				+ "ranking&key=" + targetDuration.ToDurationTypeString()
				+ "%2c" + targetGenreOrCategory.ToGenreAndCategoryString() );
		}

		public static BlogPartsRankingResponse ParseRankingData( string rankingData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( rankingData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );
#else
			var xml = XDocument.Parse( rankingData );
#endif

			var responseXml = xml.GetDocumentRootNode();
			if( responseXml.GetName() != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new BlogPartsRankingResponse( responseXml );
		}

		public static Task<BlogPartsRankingResponse> GetRankingAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return GetRankingDataAsync( context, targetDuration, targetGenreOrCategory )
				.ContinueWith( prevTask => ParseRankingData( prevTask.Result ) );
		}
	}
}