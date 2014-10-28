using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.TagRevision
{
	internal sealed class TagRevisionClient
	{
		public static Task<string> GetTagRevisionDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.LiveTagRevisionUrl + requestId );
		}

		public static ushort ParseTagRevisionData( string tagRevisionData )
		{
			return tagRevisionData.Substring( 7, tagRevisionData.Length - 8 ).ToUShort();
		}

		public static Task<ushort> GetTagRevisionAsync( NiconicoContext context, string requestId )
		{
			return GetTagRevisionDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseTagRevisionData( prevTask.Result ) );
		}
	}
}