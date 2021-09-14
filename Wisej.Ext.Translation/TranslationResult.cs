///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;

namespace Wisej.Ext.Translation
{
	/// <summary>
	/// Represents the result of a translation request. 
	/// </summary>
	[ApiCategory("Translation")]
	public class TranslationResult
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Ext.Translation.TranslationResult"/>.
		/// </summary>
		/// <param name="originalText">The original text.</param>
		/// <param name="translatedText">The translated text - or null if an error occurred.</param>
		/// <param name="from">The language of the original text.</param>
		/// <param name="to">The language of the translated text.</param>
		/// <param name="errorCode">The error code returned by the translatation service, or -1 if the service returned an error message without an error code. Zero if the request was successful.</param>
		/// <param name="errorMessage">The error messsage returned by the translation service, or null if the request was successful.</param>
		public TranslationResult(string originalText, string translatedText, string from, string to, int errorCode, string errorMessage)
		{
			this.OriginalText = originalText;
			this.TranslatedText = translatedText;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}

		/// <summary>
		/// The error code returned by the translatation service, or -1 if the service returned an error message without an error code. Zero if the request was successful.
		/// </summary>
		public int ErrorCode { get; private set; }

		/// <summary>
		/// The error messsage returned by the translation service, or null if the request was successful.
		/// </summary>
		public string ErrorMessage { get; private set; }

		/// <summary>
		/// The original text.
		/// </summary>
		public string OriginalText { get; private set; }

		/// <summary>
		/// The translated text - or null if an error occurred.
		/// </summary>
		public string TranslatedText { get; private set; }

		/// <summary>
		/// Returns the language of the original text.
		/// </summary>
		public string OriginalLanguage { get; private set; }

		/// <summary>
		/// Returns the language of the translated text.
		/// </summary>
		public string TargetLanguage { get; private set; }
	}
}
