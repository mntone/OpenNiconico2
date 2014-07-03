using System;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Storage;
using Windows.Storage.Streams;
#else
using System.IO;
#endif

namespace Mntone.Nico2.Test
{
	internal static class TestHelper
	{
		public static async Task<string> LoadAsync( string path )
		{
			string text;
#if WINDOWS_APP
			var file = await StorageFile.GetFileFromApplicationUriAsync( new Uri( @"ms-appx:///TestData/" + path ) );
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
#else
			var file = File.OpenRead( @"TestData/" + path );
			using( var reader = new StreamReader( file ) )
			{
				text = await reader.ReadToEndAsync();
			}
#endif
			return text;
		}

		public static string Load( string path )
		{
			return LoadAsync( path ).GetAwaiter().GetResult();
		}
	}

#if WINDOWS_APP
	internal static class Assert2
	{
		public static T ThrowsException<T>( Action action )
			where T: Exception
		{
			return Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert.ThrowsException<T>( action );
		}

		public static T ThrowsException<T>( Func<object> action )
			where T: Exception
		{
			return Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert.ThrowsException<T>( action );
		}
	}
#else
	internal static class Assert2
	{
		public static T ThrowsException<T>( Action action )
			where T: Exception
		{
			try
			{
				action();
			}
			catch( T ex )
			{
				return ex;
			}
			Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail( "Failed…" );
			return null;
		}

		public static T ThrowsException<T>( Func<object> action )
			where T: Exception
		{
			try
			{
				action();
			}
			catch( T ex )
			{
				return ex;
			}
			Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail( "Failed…" );
			return null;
		}
	}
#endif
}