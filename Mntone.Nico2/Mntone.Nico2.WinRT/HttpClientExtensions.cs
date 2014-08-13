using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
		public static Task<IBuffer> GetBufferAsync( this HttpClient client, string uri )
		{
			return client.GetBufferAsync( new Uri( uri ) ).AsTask();
		}

		public static Task<byte[]> GetByteArrayAsync( this HttpClient client, string uri )
		{
			return client
				.GetInputStreamAsync( new Uri( uri ) )
				.AsTask()
				.ContinueWith( prevTask =>
				{
					var stream = prevTask.Result.AsStreamForRead();
					var ret = new byte[stream.Length];
					stream.Read( ret, 0, ( int )stream.Length );
					return ret;
				} );
		}

		public static Task<string> GetString2Async( this HttpClient client, string uri )
		{
			return client.GetStringAsync( new Uri( uri ) ).AsTask();
		}

		public static Task<string> GetConvertedString2Async( this HttpClient client, string uri )
		{
			return client.GetConvertedString2Async( uri, Encoding.UTF8 );
		}

		public static Task<string> GetConvertedString2Async( this HttpClient client, string uri, Encoding encoding )
		{
			return client
				.GetInputStreamAsync( new Uri( uri ) )
				.AsTask()
				.ContinueWith( buffer => new StreamReader( buffer.Result.AsStreamForRead(), encoding ).ReadToEnd() );
		}

		public static Task<HttpResponseMessage> Post2Async( this HttpClient client, string uri, IEnumerable<KeyValuePair<string, string>> content )
		{
			return client.PostAsync( new Uri( uri ), new HttpFormUrlEncodedContent( content ) ).AsTask();
		}

		public static IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> HeadAsync( this HttpClient client, Uri uri )
		{
			return client.SendRequestAsync( new HttpRequestMessage( HttpMethod.Head, uri ) );
		}

		public static Task<HttpResponseMessage> Head2Async( this HttpClient client, string uri )
		{
			return client.HeadAsync( new Uri( uri ) ).AsTask();
		}
	}
}