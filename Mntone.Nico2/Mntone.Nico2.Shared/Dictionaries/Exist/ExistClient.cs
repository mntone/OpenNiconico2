using System;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Dictionaries.Exist
{
	internal sealed class ExistClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> ExistDataAsync(
			NiconicoContext context, Category targetCategory, string targetWord )
		{
			return context.GetClient().GetStringAsync( new Uri(
				NiconicoUrls.DictionaryExistUrl + targetCategory.ToCategoryChar() + '/' + Uri.EscapeUriString( targetWord ) ) );
		}

		public static bool ParseExistData( string existData )
		{
			return existData.ToBooleanFrom1();
		}

		public static IAsyncOperation<bool> ExistAsync( NiconicoContext context, Category targetCategory, string targetWord )
		{
			return ExistDataAsync( context, targetCategory, targetWord )
				.AsTask()
				.ContinueWith( prevTask => ParseExistData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}