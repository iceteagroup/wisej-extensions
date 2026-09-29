///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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


using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Wisej.Ext.Translation
{
	/// <summary>
	/// Base class for the translation providers.
	/// </summary>
	/// <remarks>
	/// Derive from this class to connect the <see cref="Translation"/> component to a different translation service,
	/// then assign an instance to <see cref="Translation.Provider"/> or set <see cref="Translation.ProviderType"/>
	/// to the assembly-qualified name of the class.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// public class MyTranslationProvider : TranslationProviderBase
	/// {
	/// 	public override string ClientID { get; set; }
	///
	/// 	public override string ClientSecret { get; set; }
	///
	/// 	public override TranslationResult Translate(string text, string from, string to)
	/// 	{
	/// 		try
	/// 		{
	/// 			string translated = MyTranslationService.Translate(this.ClientSecret, text, from, to);
	/// 			return new TranslationResult(text, translated, from, to, 0, null);
	/// 		}
	/// 		catch (Exception ex)
	/// 		{
	/// 			return new TranslationResult(text, null, from, to, ex.HResult, ex.Message);
	/// 		}
	/// 	}
	///
	/// 	public override void TranslateAsync(string text, string from, string to, Action<TranslationResult> resultCallback)
	/// 	{
	/// 		Application.StartTask(() => resultCallback(Translate(text, from, to)));
	/// 	}
	/// }
	///
	/// // Use the custom provider.
	/// this.translation1.Provider = new MyTranslationProvider();
	/// ]]></code>
	/// </example>
	[ApiCategory("Translation")]
	public abstract class TranslationProviderBase
	{
		/// <summary>
		/// The client-id for the provider.
		/// </summary>
		[Description("The client-id for the provider.")]
		public abstract string ClientID { get; set; }

		/// <summary>
		/// The secret client-key or api-key for the provider.
		/// </summary>
		[Description("The secret client-key or api-key for the provider.")]
		public abstract string ClientSecret { get; set; }

		/// <summary>
		/// Invokes the translation service provider and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language code ("en", "de", ...), or null or empty to let the provider auto-detect the source language.</param>
		/// <param name="to">The target language code ("en", "de", ...).</param>
		/// <returns>An instance of <see cref="TranslationResult"/>.</returns>
		/// <remarks>
		/// Implementations should not throw when the service fails. Instead, return a <see cref="TranslationResult"/>
		/// with a null <see cref="TranslationResult.TranslatedText"/> and a non-zero <see cref="TranslationResult.ErrorCode"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// TranslationProviderBase provider = new TranslationProviderYandex { ClientSecret = "<your-api-key>" };
		///
		/// TranslationResult result = provider.Translate("Thank you", "en", "es");
		/// if (result.ErrorCode == 0)
		/// 	label1.Text = result.TranslatedText;
		/// ]]></code>
		/// </example>
		public abstract TranslationResult Translate(string text, string from, string to);

		/// <summary>
		/// Invokes the translation service provider asynchronously and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language code ("en", "de", ...), or null or empty to let the provider auto-detect the source language.</param>
		/// <param name="to">The target language code ("en", "de", ...).</param>
		/// <param name="resultCallback">Callback method that will receive the <see cref="TranslationResult"/> when ready.</param>
		/// <remarks>
		/// Implementations should return immediately and invoke <paramref name="resultCallback"/> once the result is available,
		/// typically from a task started with <see cref="Wisej.Web.Application.StartTask(System.Action)"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// TranslationProviderBase provider = new TranslationProviderYandex { ClientSecret = "<your-api-key>" };
		///
		/// provider.TranslateAsync("Thank you", "en", "es", result =>
		/// {
		/// 	label1.Text = result.TranslatedText ?? result.ErrorMessage;
		/// 	Application.Update(this);
		/// });
		/// ]]></code>
		/// </example>
		public abstract void TranslateAsync(string text, string from, string to, Action<TranslationResult> resultCallback);
	}
}
