using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Mntone.Nico2.Test
{
	internal static class TestHelper
	{
		public static async Task<string> LoadAsync( string path )
		{
			var file = await StorageFile.GetFileFromApplicationUriAsync( new Uri( @"ms-appx:///TestData/" + path ) );
			string text;
			using( var stream = await file.OpenReadAsync() )
			{
				var size = stream.Size;
				using( var inputStream = stream.GetInputStreamAt( 0 ) )
				{
					var dataReader = new DataReader( inputStream );
					var numBytesLoaded = await dataReader.LoadAsync( ( uint )size );
					text = dataReader.ReadString( numBytesLoaded );
				}
			}
			return text;
		}

		public static string Load( string path )
		{
			return LoadAsync( path ).GetAwaiter().GetResult();
		}
	}
}