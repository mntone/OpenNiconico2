using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_APP
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
#endif

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
#if WINDOWS_APP
		public static Task<IBuffer> GetBufferAsync( this HttpClient client, string uri )
		{
			return client
				.GetStreamAsync( uri )
				.ContinueWith( prevTask =>
				{
					var stream = prevTask.Result;
					var byteArray = new byte[stream.Length];
					stream.Read( byteArray, 0, ( int )stream.Length );
					return byteArray.AsBuffer();
				} );
		}
#else
		public static Task<byte[]> GetByteArrayAsync( this HttpClient client, string uri )
		{
			return client
				.GetStreamAsync( uri )
				.ContinueWith( prevTask =>
				{
					var stream = prevTask.Result;
					var ret = new byte[stream.Length];
					stream.Read( ret, 0, ( int )stream.Length );
					return ret;
				} );
		}
#endif

		public static Task<string> GetString2Async( this HttpClient client, string uri )
		{
			return client.GetStringAsync( uri );
		}

		public static Task<string> GetConvertedString2Async( this HttpClient client, string uri )
		{
			return client.GetConvertedString2Async( uri, Encoding.UTF8 );
		}

		public static Task<string> GetConvertedString2Async( this HttpClient client, string uri, Encoding encoding )
		{
			return client
				.GetStreamAsync( uri )
				.ContinueWith( stream => new StreamReader( stream.Result, encoding ).ReadToEnd() );
		}

		public static Task<HttpResponseMessage> Post2Async( this HttpClient client, string uri, IEnumerable<KeyValuePair<string, string>> content )
		{
			return client.PostAsync( uri, new FormUrlEncodedContent( content ) );
		}

		public static Task<HttpResponseMessage> HeadAsync( this HttpClient client, Uri uri )
		{
			return client.SendAsync( new HttpRequestMessage( HttpMethod.Head, uri ) );
		}

		public static Task<HttpResponseMessage> Head2Async( this HttpClient client, string uri )
		{
			return client.HeadAsync( new Uri( uri ) );
		}
	}
}