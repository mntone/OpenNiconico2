using System;

namespace Mntone.Nico2
{
	/// <summary>
	/// パース失敗例外
	/// </summary>
#if WINDOWS_APP
	internal sealed class ParseException
#else
	public sealed class ParseException
#endif
		: Exception
	{
		const string MESSAGE = "Text has invalid context.";

		/// <summary>
		/// コンストラクタ－
		/// </summary>
		public ParseException()
			: base( MESSAGE )
		{
			this.HResult = NiconicoHResult.E_PARSE;
		}

		/// <summary>
		/// コンストラクタ－
		/// </summary>
		/// <param name="message">メッセージ</param>
		public ParseException( string message )
			: base( message )
		{
			this.HResult = NiconicoHResult.E_PARSE;
		}

		/// <summary>
		/// コンストラクタ－
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="innerException">内部例外</param>
		public ParseException( string message, Exception innerException )
			: base( message, innerException )
		{
			this.HResult = NiconicoHResult.E_PARSE;
		}
	}
}