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


namespace Wisej.Ext.Translation
{
	/// <summary>
	/// <para>
	/// Translation component. Adds language translation features to Wisej applications.
	/// </para>
	/// <para>
	/// Drop a <see cref="Translation"/> component on a page or form, set its <see cref="Translation.ClientSecret"/>
	/// to the API key of the translation service, and call <see cref="Translation.Translate(string, string, string)"/>
	/// or <see cref="Translation.TranslateAsync(string, string, string, System.Action{TranslationResult})"/>.
	/// The service is implemented by a <see cref="TranslationProviderBase"/> subclass;
	/// <see cref="TranslationProviderYandex"/> is used by default.
	/// </para>
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// using Wisej.Ext.Translation;
	///
	/// var translation = new Translation { ClientSecret = "<your-api-key>" };
	///
	/// TranslationResult result = translation.Translate("Hello World", "en", "de");
	/// if (result.ErrorCode == 0)
	/// 	AlertBox.Show(result.TranslatedText);
	/// ]]></code>
	/// </example>
	internal class NamespaceDoc
	{
	}
}
