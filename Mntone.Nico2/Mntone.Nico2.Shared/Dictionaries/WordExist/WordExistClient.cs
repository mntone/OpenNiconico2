using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Dictionaries.WordExist
{
	internal sealed class WordExistClient
	{
		public static Task<string> WordExistDataAsync( NiconicoContext context, string targetWord )
		{
			return context.GetClient().GetString2Async( NiconicoUrls.DictionaryWordExistUrl + Uri.EscapeUriString( targetWord ) );
		}

		public static bool ParseWordExistData( string wordExistData )
		{
			return wordExistData.Substring( 1, 1 ).ToBooleanFrom1();
		}

		public static Task<bool> WordExistAsync( NiconicoContext context, string targetWord )
		{
			return WordExistDataAsync( context, targetWord )
				.ContinueWith( prevTask => ParseWordExistData( prevTask.Result ) );
		}
	}
}