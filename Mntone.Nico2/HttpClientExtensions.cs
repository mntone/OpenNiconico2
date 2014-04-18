using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Web.Http;

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
		public static IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> HeadAsync( this HttpClient client, Uri uri )
		{
			return client.SendRequestAsync( new HttpRequestMessage( HttpMethod.Head, uri ) );
		}
	}
}
