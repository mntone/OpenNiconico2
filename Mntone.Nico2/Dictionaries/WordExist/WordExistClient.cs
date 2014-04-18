using System;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2.Dictionaries.WordExist
{
	internal sealed class WordExistClient
	{
		public static IAsyncOperationWithProgress<string, HttpProgress> WordExistDataAsync( NiconicoContext context, string targetWord )
		{
			return context.GetClient().GetStringAsync( new Uri( NiconicoUrls.DictionaryWordExistUrl + Uri.EscapeUriString( targetWord ) ) );
		}

		public static bool ParseWordExistData( string wordExistData )
		{
			if( wordExistData.Length == 7 && wordExistData.StartsWith( "z([" ) && wordExistData.EndsWith( "]);" ) )
			{
				return wordExistData.Substring( 3, 1 ).ToBooleanFrom1();
			}
			throw new Exception( "Parse Error" );
		}

		public static IAsyncOperation<bool> WordExistAsync( NiconicoContext context, string targetWord )
		{
			return WordExistDataAsync( context, targetWord )
				.AsTask()
				.ContinueWith( prevTask => ParseWordExistData( prevTask.Result ) )
				.AsAsyncOperation();
		}
	}
}