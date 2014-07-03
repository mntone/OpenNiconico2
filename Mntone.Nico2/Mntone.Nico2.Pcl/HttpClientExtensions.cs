using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
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