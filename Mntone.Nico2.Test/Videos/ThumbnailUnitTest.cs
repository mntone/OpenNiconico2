using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Videos.Thumbnail;
using System;

namespace Mntone.Nico2.Test.Videos
{
	[TestClass]
	public sealed class ThumbnailUnitTest
	{
		[TestMethod]
		public void Thumbnail_0不正なID()
		{
			Assert.ThrowsException<ArgumentException>( () =>
			{
				ThumbnailClient.GetThumbnailDataAsync( new NiconicoContext( new NiconicoAuthenticationToken() ), "ssm9" ).GetResults();
			} );
		}

		[TestMethod]
		public void Thumbnail_1通常データ()
		{
			var ret = ThumbnailClient.ParseThumbnailData( TestHelper.Load( @"Videos/Thumbnail/default.xml" ) );

			Assert.AreEqual( "sm9", ret.VideoID );
			Assert.AreEqual( "新・豪血寺一族 -煩悩解放 - レッツゴー！陰陽師", ret.Title );
			Assert.AreEqual( "レッツゴー！陰陽師（フルコーラスバージョン）", ret.Description );
			Assert.AreEqual( "http://tn-skr2.smilevideo.jp/smile?i=9", ret.ThumbnailUrl.ToString() );
			Assert.AreEqual( "2007-03-05T15:33:00", ret.PostedAt.ToUniversalTime().ToString( "s" ) );
			Assert.AreEqual( 5.0 * 60.0 + 19.0, ret.Length.TotalSeconds );
			Assert.AreEqual( MovieType.Flv, ret.MovieType );
			Assert.AreEqual( 21138631u, ret.SizeHigh );
			Assert.AreEqual( 17436492u, ret.SizeLow );
			Assert.AreEqual( 13831185u, ret.ViewCount );
			Assert.AreEqual( 4176155u, ret.CommentCount );
			Assert.AreEqual( 148424u, ret.MylistCount );
			Assert.AreEqual( "悪霊☯退散 よし行くか! ちょっとQK これくっそ寒い うううううううううう", ret.LastCommentBody );
			Assert.AreEqual( "http://www.nicovideo.jp/watch/sm9", ret.WatchUrl.ToString() );
			Assert.AreEqual( ThumbnailType.Video, ret.ThumbnailType );
			Assert.IsTrue( ret.Embeddable );
			Assert.IsFalse( ret.NoLivePlay );

			Assert.AreEqual( "jp", ret.Tags.Domain );
			Assert.AreEqual( 10, ret.Tags.Value.Count );

			Assert.IsFalse( ret.Tags.Value[0].Category );
			Assert.IsTrue( ret.Tags.Value[0].Lock );
			Assert.AreEqual( "陰陽師", ret.Tags.Value[0].Value );

			Assert.IsFalse( ret.Tags.Value[1].Category );
			Assert.IsTrue( ret.Tags.Value[1].Lock );
			Assert.AreEqual( "レッツゴー！陰陽師", ret.Tags.Value[1].Value );

			Assert.IsFalse( ret.Tags.Value[2].Category );
			Assert.IsTrue( ret.Tags.Value[2].Lock );
			Assert.AreEqual( "公式", ret.Tags.Value[2].Value );

			Assert.IsFalse( ret.Tags.Value[3].Category );
			Assert.IsTrue( ret.Tags.Value[3].Lock );
			Assert.AreEqual( "音楽", ret.Tags.Value[3].Value );

			Assert.IsFalse( ret.Tags.Value[4].Category );
			Assert.IsTrue( ret.Tags.Value[4].Lock );
			Assert.AreEqual( "ゲーム", ret.Tags.Value[4].Value );

			Assert.IsFalse( ret.Tags.Value[5].Category );
			Assert.IsFalse( ret.Tags.Value[5].Lock );
			Assert.AreEqual( "最古の動画", ret.Tags.Value[5].Value );

			Assert.IsFalse( ret.Tags.Value[6].Category );
			Assert.IsFalse( ret.Tags.Value[6].Lock );
			Assert.AreEqual( "sm9", ret.Tags.Value[6].Value );

			Assert.IsFalse( ret.Tags.Value[7].Category );
			Assert.IsFalse( ret.Tags.Value[7].Lock );
			Assert.AreEqual( "→sm13", ret.Tags.Value[7].Value );

			Assert.IsFalse( ret.Tags.Value[8].Category );
			Assert.IsFalse( ret.Tags.Value[8].Lock );
			Assert.AreEqual( "運営長の中の人", ret.Tags.Value[8].Value );

			Assert.IsFalse( ret.Tags.Value[9].Category );
			Assert.IsFalse( ret.Tags.Value[9].Lock );
			Assert.AreEqual( "元気の出る動画", ret.Tags.Value[9].Value );

			Assert.AreEqual( UserType.User, ret.UserType );
			Assert.AreEqual( 4u, ret.UserId );
			Assert.AreEqual( "運営長の中の人", ret.UserName );
			Assert.AreEqual( "http://usericon.nimg.jp/usericon/s/0/4.jpg?1390830505", ret.UserIconUrl.ToString() );
		}

		[TestMethod]
		public void Thumbnail_2チャンネル動画データ()
		{
			var ret = ThumbnailClient.ParseThumbnailData( TestHelper.Load( @"Videos/Thumbnail/channel.xml" ) );

			Assert.AreEqual( "so22734676", ret.VideoID );
			Assert.AreEqual( "魔法戦争　第1話「真夏の魔法少女」", ret.Title );
			Assert.AreEqual( "ある夏の日のこと。七瀬武は部室棟で、見たことがない制服を着た少女・相羽六が倒れるのを目撃する。意識を失いかけている六を介抱するため、保健室まで運ぶ武だったが、意識が戻った六に不審者と勘違いされてしまう。お互いの立場の違いか、今ひとつかみ合わない会話の中、何とか誤解は解けそうになるも、そこに怪しい集団が現われ六を連れ去ろうとするのだった。原作ノベル・コミック版が今すぐ読める動画一覧はこちら", ret.Description );
			Assert.AreEqual( "http://tn-skr1.smilevideo.jp/smile?i=22734676", ret.ThumbnailUrl.ToString() );
			Assert.AreEqual( "2014-01-26T15:00:00", ret.PostedAt.ToUniversalTime().ToString( "s" ) );
			Assert.AreEqual( 24.0 * 60.0 + 16.0, ret.Length.TotalSeconds );
			Assert.AreEqual( MovieType.Mp4, ret.MovieType );
			Assert.AreEqual( 151848175u, ret.SizeHigh );
			Assert.AreEqual( 51647683u, ret.SizeLow );
			Assert.AreEqual( 112797u, ret.ViewCount );
			Assert.AreEqual( 15890u, ret.CommentCount );
			Assert.AreEqual( 1357u, ret.MylistCount );
			Assert.AreEqual( "有料 opとEDは良いけど内容 この鈴村のキャラよく ...", ret.LastCommentBody );
			Assert.AreEqual( "http://www.nicovideo.jp/watch/1390532934", ret.WatchUrl.ToString() );
			Assert.AreEqual( ThumbnailType.Video, ret.ThumbnailType );
			Assert.IsTrue( ret.Embeddable );
			Assert.IsFalse( ret.NoLivePlay );

			Assert.AreEqual( "jp", ret.Tags.Domain );
			Assert.AreEqual( 7, ret.Tags.Value.Count );

			Assert.IsTrue( ret.Tags.Value[0].Category );
			Assert.IsTrue( ret.Tags.Value[0].Lock );
			Assert.AreEqual( "アニメ", ret.Tags.Value[0].Value );

			Assert.IsFalse( ret.Tags.Value[1].Category );
			Assert.IsTrue( ret.Tags.Value[1].Lock );
			Assert.AreEqual( "魔法戦争", ret.Tags.Value[1].Value );

			Assert.IsFalse( ret.Tags.Value[2].Category );
			Assert.IsTrue( ret.Tags.Value[2].Lock );
			Assert.AreEqual( "宮野真守", ret.Tags.Value[2].Value );

			Assert.IsFalse( ret.Tags.Value[3].Category );
			Assert.IsTrue( ret.Tags.Value[3].Lock );
			Assert.AreEqual( "東山奈央", ret.Tags.Value[3].Value );

			Assert.IsFalse( ret.Tags.Value[4].Category );
			Assert.IsTrue( ret.Tags.Value[4].Lock );
			Assert.AreEqual( "瀬戸麻沙美", ret.Tags.Value[4].Value );

			Assert.IsFalse( ret.Tags.Value[5].Category );
			Assert.IsTrue( ret.Tags.Value[5].Lock );
			Assert.AreEqual( "鈴村健一", ret.Tags.Value[5].Value );

			Assert.IsFalse( ret.Tags.Value[6].Category );
			Assert.IsFalse( ret.Tags.Value[6].Lock );
			Assert.AreEqual( "SHINAI", ret.Tags.Value[6].Value );

			Assert.AreEqual( UserType.Channel, ret.UserType );
			Assert.AreEqual( 2585303u, ret.UserId );
			Assert.AreEqual( "魔法戦争", ret.UserName );
			Assert.AreEqual( "http://icon.nimg.jp/channel/s/ch2585303.jpg?1390532933", ret.UserIconUrl.ToString() );
		}

		[TestMethod]
		public void Thumbnail_3エラーデータ_コミュニティー動画()
		{
			try
			{
				ThumbnailClient.ParseThumbnailData( TestHelper.Load( @"Videos/Thumbnail/community.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: community (COMMUNITY)", ex.Message );
			}
		}

		[TestMethod]
		public void Thumbnail_4エラーデータ_コミュニティー動画()
		{
			try
			{
				ThumbnailClient.ParseThumbnailData( TestHelper.Load( @"Videos/Thumbnail/delete.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: deleted (DELETED)", ex.Message );
			}
		}

		[TestMethod]
		public void Thumbnail_5エラーデータ_コミュニティー動画()
		{
			try
			{
				ThumbnailClient.ParseThumbnailData( TestHelper.Load( @"Videos/Thumbnail/not_found.xml" ) );
			}
			catch( Exception ex )
			{
				Assert.AreEqual( "Parse Error: not found or invalid (NOT_FOUND)", ex.Message );
			}
		}
	}
}
