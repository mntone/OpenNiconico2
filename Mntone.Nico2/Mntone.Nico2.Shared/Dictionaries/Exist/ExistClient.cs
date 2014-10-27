using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Dictionaries.Exist
{
	internal sealed class ExistClient
	{
		public static Task<string> ExistDataAsync( NiconicoContext context, Category targetCategory, string targetWord )
		{
			return context.GetClient().GetStringAsync(
				NiconicoUrls.DictionaryExistUrl + targetCategory.ToCategoryChar() + '/' + Uri.EscapeUriString( targetWord ) );
		}

		public static bool ParseExistData( string existData )
		{
			return existData.ToBooleanFrom1();
		}

		public static Task<bool> ExistAsync( NiconicoContext context, Category targetCategory, string targetWord )
		{
			return ExistDataAsync( context, targetCategory, targetWord )
				.ContinueWith( prevTask => ParseExistData( prevTask.Result ) );
		}
	}
}