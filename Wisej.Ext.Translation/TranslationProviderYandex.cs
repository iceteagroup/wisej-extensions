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
using System.Net;
using System.Web;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using Wisej.Base;
using System.Text;
using Wisej.Web;

namespace Wisej.Ext.Translation
{
	/// <summary>
	/// Yandex translation provider.
	/// </summary>
	/// <remarks>
	/// See https://tech.yandex.com/translate/.
	/// </remarks>
	public class TranslationProviderYandex : TranslationProviderBase
	{
		private const string URL = "https://translate.yandex.net/api/v1.5/tr/translate";

		/// <summary>
		/// Yandex only needs the api-key.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// The secret client-key or api-key for the provider.
		/// </summary>
		public override string ClientSecret
		{
			get;
			set;
		}

		/// <summary>
		/// Invokes the translation service provider and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to auto detect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <returns></returns>
		public override TranslationResult Translate(string text, string from, string to)
		{
			string requestUrl = String.Format("{0}?key={1}&text={2}&lang={3}",
				URL,
				this.ClientSecret,
				HttpUtility.UrlEncode(text),
				(String.IsNullOrEmpty(from) ? string.Empty : from + "-") + to);

			try
			{
				using (WebClient client = new WebClient())
				{
					client.Encoding = Encoding.UTF8;
					string xmlResult = client.DownloadString(requestUrl);
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(xmlResult);

					// detect the result.
					XmlNode result = xmlDoc["Translation"];
					if (result != null)
					{
						string translatedText = result["text"].InnerText;
						string language = result.Attributes["lang"].Value;

						// extract the source language if auto detected.
						if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(language))
						{
							string[] parts = language.Split('-');
							if (parts.Length > 0)
								from = parts[0];
						}
						return new TranslationResult(text, translatedText, from, to, 0, null);
					}

					// detect error.
					XmlNode error = xmlDoc["Error"];
					if (error != null)
					{
						int code = int.Parse(error.Attributes["code"].Value);
						string message = error.Attributes["message"].Value;
						return new TranslationResult(text, null, from, to, code, message);
					}
				}
			}
			catch (Exception ex)
			{
				return new TranslationResult(text, null, from, to, ex.HResult, ex.Message);
			}

			return new TranslationResult(text, null, from, to, -1, "Timeout");
		}

		/// <summary>
		/// Invokes the translation service provider asynchronously and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to auto detect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <param name="resultCallback">Callback method that will receive the TranslationResult when ready.</param>
		public override void TranslateAsync(string text, string from, string to, Action<TranslationResult> resultCallback)
		{
			var callback = resultCallback;
			Application.StartTask(() =>
			{
				TranslationResult result = this.Translate(text, from, to);
				callback(result);

			});
		}
	}
}
