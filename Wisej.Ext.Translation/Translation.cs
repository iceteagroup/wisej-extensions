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
using System.Drawing;

namespace Wisej.Ext.Translation
{
	/// <summary>
	/// The Translation component interfaces with several third party translation providers to provide
	/// language translation services to the application.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Translation))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The Translation component interfaces with several third party translation providers to provide language translation services to the application.")]
	[ApiCategory("Translation")]
	public class Translation : System.ComponentModel.Component
	{
		private const string DEFAULT_PROVIDER_TYPE = "Wisej.Ext.Translation.TranslationProviderYandex";

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Translation" /> class.
		/// </summary>
		public Translation()
		{
			// start with the default translation provider.
			this._provider = new TranslationProviderYandex();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Translation" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		public Translation(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		#endregion

		#region Properties

		/// <summary>
		/// The client-id for the provider.
		/// </summary>
		[Description("The client-id for the provider.")]
		public string ClientID { get; set; }

		/// <summary>
		/// The secret client-key or api-key for the provider.
		/// </summary>
		[Description("The secret client-key or api-key for the provider.")]
		public string ClientSecret { get; set; }

		/// <summary>
		/// Returns or sets the type of the translation provider.
		/// </summary>
		[Description("Gets or sets the type of the translation provider.")]
		public string ProviderType
		{
			get { return this._providerTypeName; }
			set
			{
				if (this._providerTypeName != value)
				{
					this._providerType = Type.GetType(value, true);
					this._provider = (TranslationProviderBase)Activator.CreateInstance(this._providerType);
				}
			}
		}
		private Type _providerType = null;
		private string _providerTypeName = DEFAULT_PROVIDER_TYPE;

		/// <summary>
		/// Returns or sets the translation provider implementation.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TranslationProviderBase Provider
		{
			get { return this._provider; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this._provider = value;
			}
		}
		private TranslationProviderBase _provider;

		#endregion

		#region Methods

		/// <summary>
		/// Translates the text to the requested language.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to autodetect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <returns></returns>
		public TranslationResult Translate(string text, string from, string to)
		{
			if (String.IsNullOrEmpty(text))
				throw new ArgumentNullException("text");

			if (String.IsNullOrEmpty(to))
				throw new ArgumentNullException("to");

			this._provider.ClientID = this.ClientID;
			this._provider.ClientSecret = this.ClientSecret;
			return this._provider.Translate(text, from, to);
		}

		/// <summary>
		/// Invokes the translation service provider asynchronously and returns the result of the request in an instance
		/// of the <see cref="T:Wisej.Ext.Translation.TranslationResult"/> class.
		/// </summary>
		/// <param name="text">The text to translate.</param>
		/// <param name="from">The source language ("en", "de", ...) or null/empty to ask the provider to auto detect the source language.</param>
		/// <param name="to">The target language ("en", "de", ...)</param>
		/// <param name="resultCallback">Callback method that will receive the TranslationResult when ready.</param>
		public void TranslateAsync(string text, string from, string to, Action<TranslationResult> resultCallback)
		{
			if (String.IsNullOrEmpty(text))
				throw new ArgumentNullException("text");

			if (String.IsNullOrEmpty(to))
				throw new ArgumentNullException("to");

			if (resultCallback == null)
				throw new ArgumentNullException("resultCallback");

			this._provider.ClientID = this.ClientID;
			this._provider.ClientSecret = this.ClientSecret;
			this._provider.TranslateAsync(text, from, to, resultCallback);
		}

		#endregion

	}
}
