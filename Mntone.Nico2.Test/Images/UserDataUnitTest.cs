using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mntone.Nico2.Images.Illusts;
using Mntone.Nico2.Images.Users.Data;
using System;

namespace Mntone.Nico2.Test.Images
{
	[TestClass]
	public sealed class UserDataUnitTest
	{
		[TestMethod]
		public void UserData_0通常データ()
		{
			var ret = DataClient.ParseDataData( TestHelper.Load( @"Images/Users/Data/default.xml" ) );
			Assert.AreEqual( 3u, ret.ImageCount );

			Assert.AreEqual( 3, ret.Images.Count );

			Assert.AreEqual( "im3937109", ret.Images[0].ID );
			Assert.AreEqual( 27849771u, ret.Images[0].UserId );
			Assert.AreEqual( "ｶｾﾞ", ret.Images[0].Title );
			Assert.AreEqual( string.Empty, ret.Images[0].Description );
			Assert.AreEqual( 1399u, ret.Images[0].ViewCount );
			Assert.AreEqual( 8u, ret.Images[0].CommentCount );
			Assert.AreEqual( 79u, ret.Images[0].ClipCount );
			Assert.AreEqual( "かわいい・・・ 幼い肢体の太股… あらきれい いいわぁ・・・ 淡い塗りがいいですね やわらかい(確信) ぺろりてぇ・・・ ふぅ・・・", ret.Images[0].LastCommentBody );
			Assert.AreEqual( Genre.Popular, ret.Images[0].Genre );
			Assert.AreEqual( Category.Illust, ret.Images[0].Category );
			Assert.AreEqual( 1u, ret.Images[0].ImageType );
			Assert.AreEqual( 0u, ret.Images[0].IllustType );
			Assert.AreEqual( 1u, ret.Images[0].InspectionStatus );
			Assert.IsFalse( ret.Images[0].IsAnonymous );
			Assert.AreEqual( 0u, ret.Images[0].PublicStatus );
			Assert.IsFalse( ret.Images[0].IsDeleted );
			Assert.AreEqual( 0u, ret.Images[0].DeleteType );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 14, 27, 28, TimeSpan.FromHours( 9 ) ), ret.Images[0].PostedAt );

			Assert.AreEqual( "im3937107", ret.Images[1].ID );
			Assert.AreEqual( 27849771u, ret.Images[1].UserId );
			Assert.AreEqual( "ﾏﾘｰ", ret.Images[1].Title );
			Assert.AreEqual( "５月４日カゲプロオンリーでの、まにおさんの個人イラスト本にゲスト寄稿したものの一部です。（一部と見せかけてほとんど見せている）", ret.Images[1].Description );
			Assert.AreEqual( 107u, ret.Images[1].ViewCount );
			Assert.AreEqual( 0u, ret.Images[1].CommentCount );
			Assert.AreEqual( 1u, ret.Images[1].ClipCount );
			Assert.AreEqual( string.Empty, ret.Images[1].LastCommentBody );
			Assert.AreEqual( Genre.FanArt, ret.Images[1].Genre );
			Assert.AreEqual( Category.Illust, ret.Images[1].Category );
			Assert.AreEqual( 1u, ret.Images[1].ImageType );
			Assert.AreEqual( 0u, ret.Images[1].IllustType );
			Assert.AreEqual( 1u, ret.Images[1].InspectionStatus );
			Assert.IsFalse( ret.Images[1].IsAnonymous );
			Assert.AreEqual( 0u, ret.Images[1].PublicStatus );
			Assert.IsFalse( ret.Images[1].IsDeleted );
			Assert.AreEqual( 0u, ret.Images[1].DeleteType );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 14, 25, 10, TimeSpan.FromHours( 9 ) ), ret.Images[1].PostedAt );

			Assert.AreEqual( "im3937098", ret.Images[2].ID );
			Assert.AreEqual( 27849771u, ret.Images[2].UserId );
			Assert.AreEqual( "咲", ret.Images[2].Title );
			Assert.AreEqual( "おなにーするぞ", ret.Images[2].Description );
			Assert.AreEqual( 253u, ret.Images[2].ViewCount );
			Assert.AreEqual( 0u, ret.Images[2].CommentCount );
			Assert.AreEqual( 2u, ret.Images[2].ClipCount );
			Assert.AreEqual( string.Empty, ret.Images[2].LastCommentBody );
			Assert.AreEqual( Genre.Adult, ret.Images[2].Genre );
			Assert.AreEqual( Category.Adult, ret.Images[2].Category );
			Assert.AreEqual( 1u, ret.Images[2].ImageType );
			Assert.AreEqual( 1u, ret.Images[2].IllustType );
			Assert.AreEqual( 1u, ret.Images[2].InspectionStatus );
			Assert.IsFalse( ret.Images[2].IsAnonymous );
			Assert.AreEqual( 0u, ret.Images[2].PublicStatus );
			Assert.IsFalse( ret.Images[2].IsDeleted );
			Assert.AreEqual( 0u, ret.Images[2].DeleteType );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 14, 19, 24, TimeSpan.FromHours( 9 ) ), ret.Images[2].PostedAt );

			Assert.AreEqual( 8, ret.Comments.Count );

			Assert.AreEqual( 18534505u, ret.Comments[0].ID );
			Assert.AreEqual( "im3937109", ret.Comments[0].ImageID );
			Assert.AreEqual( 0u, ret.Comments[0].ResID );
			Assert.AreEqual( "ふぅ・・・", ret.Comments[0].Value );
			Assert.AreEqual( string.Empty, ret.Comments[0].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 15, 25, 53, TimeSpan.FromHours( 9 ) ), ret.Comments[0].PostedAt );
			Assert.AreEqual( -1, ret.Comments[0].Frame );
			Assert.AreEqual( "3rP8+l9ny9Y/iKhs8r9vSn/+jQA", ret.Comments[0].UserHash );
			Assert.IsTrue( ret.Comments[0].IsAnonymous );

			Assert.AreEqual( 18534610u, ret.Comments[1].ID );
			Assert.AreEqual( "im3937109", ret.Comments[1].ImageID );
			Assert.AreEqual( 0u, ret.Comments[1].ResID );
			Assert.AreEqual( "ぺろりてぇ・・・", ret.Comments[1].Value );
			Assert.AreEqual( string.Empty, ret.Comments[1].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 15, 33, 18, TimeSpan.FromHours( 9 ) ), ret.Comments[1].PostedAt );
			Assert.AreEqual( -1, ret.Comments[1].Frame );
			Assert.AreEqual( "RT4Uif3Ry5bDGAcRhin5orV/HHs", ret.Comments[1].UserHash );
			Assert.IsTrue( ret.Comments[1].IsAnonymous );

			Assert.AreEqual( 18535030u, ret.Comments[2].ID );
			Assert.AreEqual( "im3937109", ret.Comments[2].ImageID );
			Assert.AreEqual( 0u, ret.Comments[2].ResID );
			Assert.AreEqual( "やわらかい(確信)", ret.Comments[2].Value );
			Assert.AreEqual( string.Empty, ret.Comments[2].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 16, 27, 1, TimeSpan.FromHours( 9 ) ), ret.Comments[2].PostedAt );
			Assert.AreEqual( -1, ret.Comments[2].Frame );
			Assert.AreEqual( "9qiGEltBimLXeoC3e34PbQAloDE", ret.Comments[2].UserHash );
			Assert.IsTrue( ret.Comments[2].IsAnonymous );

			Assert.AreEqual( 18535065u, ret.Comments[3].ID );
			Assert.AreEqual( "im3937109", ret.Comments[3].ImageID );
			Assert.AreEqual( 0u, ret.Comments[3].ResID );
			Assert.AreEqual( "淡い塗りがいいですねー", ret.Comments[3].Value );
			Assert.AreEqual( string.Empty, ret.Comments[3].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 16, 32, 25, TimeSpan.FromHours( 9 ) ), ret.Comments[3].PostedAt );
			Assert.AreEqual( -1, ret.Comments[3].Frame );
			Assert.AreEqual( "yIPiLKswUDzzOrf9mPCNQpKvYNw", ret.Comments[3].UserHash );
			Assert.IsTrue( ret.Comments[3].IsAnonymous );

			Assert.AreEqual( 18535187u, ret.Comments[4].ID );
			Assert.AreEqual( "im3937109", ret.Comments[4].ImageID );
			Assert.AreEqual( 0u, ret.Comments[4].ResID );
			Assert.AreEqual( "いいわぁ・・・", ret.Comments[4].Value );
			Assert.AreEqual( string.Empty, ret.Comments[4].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 16, 50, 57, TimeSpan.FromHours( 9 ) ), ret.Comments[4].PostedAt );
			Assert.AreEqual( -1, ret.Comments[4].Frame );
			Assert.AreEqual( "fzOgikO+edl+vnjOJHmKbO3B3MI", ret.Comments[4].UserHash );
			Assert.IsTrue( ret.Comments[4].IsAnonymous );

			Assert.AreEqual( 18535527u, ret.Comments[5].ID );
			Assert.AreEqual( "im3937109", ret.Comments[5].ImageID );
			Assert.AreEqual( 0u, ret.Comments[5].ResID );
			Assert.AreEqual( "あらきれい", ret.Comments[5].Value );
			Assert.AreEqual( string.Empty, ret.Comments[5].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 17, 31, 56, TimeSpan.FromHours( 9 ) ), ret.Comments[5].PostedAt );
			Assert.AreEqual( -1, ret.Comments[5].Frame );
			Assert.AreEqual( "26751669", ret.Comments[5].UserHash );
			Assert.IsFalse( ret.Comments[5].IsAnonymous );

			Assert.AreEqual( 18535595u, ret.Comments[6].ID );
			Assert.AreEqual( "im3937109", ret.Comments[6].ImageID );
			Assert.AreEqual( 0u, ret.Comments[6].ResID );
			Assert.AreEqual( "幼い肢体の太股…", ret.Comments[6].Value );
			Assert.AreEqual( string.Empty, ret.Comments[6].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 17, 42, 19, TimeSpan.FromHours( 9 ) ), ret.Comments[6].PostedAt );
			Assert.AreEqual( -1, ret.Comments[6].Frame );
			Assert.AreEqual( "iRATiyaRxT904yOlMBjjXqLsBCQ", ret.Comments[6].UserHash );
			Assert.IsTrue( ret.Comments[6].IsAnonymous );

			Assert.AreEqual( 18536848u, ret.Comments[7].ID );
			Assert.AreEqual( "im3937109", ret.Comments[7].ImageID );
			Assert.AreEqual( 0u, ret.Comments[7].ResID );
			Assert.AreEqual( "かわいい・・・", ret.Comments[7].Value );
			Assert.AreEqual( string.Empty, ret.Comments[7].Command );
			Assert.AreEqual( new DateTimeOffset( 2014, 4, 19, 19, 40, 45, TimeSpan.FromHours( 9 ) ), ret.Comments[7].PostedAt );
			Assert.AreEqual( -1, ret.Comments[7].Frame );
			Assert.AreEqual( "PeeZ+d6n55NtgkVX2Yc9fdmns04", ret.Comments[7].UserHash );
			Assert.IsTrue( ret.Comments[7].IsAnonymous );
		}

		[TestMethod]
		public void UserData_1エラーデータ()
		{
			var ret = DataClient.ParseDataData( TestHelper.Load( @"Images/Users/Data/zero.xml" ) );
			Assert.AreEqual( 0u, ret.ImageCount );
			Assert.AreEqual( 0, ret.Images.Count );
			Assert.AreEqual( 0, ret.Comments.Count );
		}
	}
}