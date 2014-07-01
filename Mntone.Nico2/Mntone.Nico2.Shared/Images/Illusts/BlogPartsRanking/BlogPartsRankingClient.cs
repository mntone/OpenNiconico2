using System;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
	internal sealed class BlogPartsRankingClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> GetRankingDataAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return context.GetClient().GetStringAsync( new Uri(
				NiconicoUrls.ImageBlogPartsUrl
				+ "ranking&key=" + targetDuration.ToDurationTypeString()
				+ "%2c" + targetGenreOrCategory.ToGenreAndCategoryString() ) );
		}

		public static BlogPartsRankingResponse ParseRankingData( string rankingData )
		{
			var xml = new XmlDocument();
			xml.LoadXml( rankingData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 4 } );

			var responseXml = xml.ChildNodes[1];
			if( responseXml.NodeName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new BlogPartsRankingResponse( responseXml );
		}

		public static IAsyncOperation<BlogPartsRankingResponse> GetRankingAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return GetRankingDataAsync( context, targetDuration, targetGenreOrCategory )
				.AsTask()
				.ContinueWith( prevTask => ParseRankingData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}