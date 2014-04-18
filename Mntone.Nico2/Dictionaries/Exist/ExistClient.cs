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
				NiconicoUrls.DictionaryExistUrl + targetCategory.ToCategoryString() + '/' + Uri.EscapeUriString( targetWord ) ) );
		}

		public static bool ParseExistData( string existData )
		{
			if( existData.Length == 5 && existData.StartsWith( "z(" ) && existData.EndsWith( ");" ) )
			{
				return existData.Substring( 2, 1 ).ToBooleanFrom1();
			}
			throw new Exception( "Parse Error" );
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