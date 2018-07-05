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


using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Wisej.Ext.Translation
{
	/// <summary>
	/// Base class for the translation providers.
	/// </summary>
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
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to autodetect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <returns>An instance of <see cref="T:Wisej.Ext.Translation.TranslationResult"/>.</returns>
		public abstract TranslationResult Translate(string text, string from, string to);

		/// <summary>
		/// Invokes the translation service provider asynchronously and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to autodetect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <param name="resultCallback">Callback method that will receive the TranslationResult when ready.</param>
		public abstract void TranslateAsync(string text, string from, string to, Action<TranslationResult> resultCallback);
	}
}
