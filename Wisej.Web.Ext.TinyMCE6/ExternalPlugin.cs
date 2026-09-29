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

namespace Wisej.Web.Ext.TinyMCE6
{
	/// <summary>
	/// Represents a local plugin that can be used with a CDN installation of TinyMCE or
	/// a local installation, although for local installations it is not necessary to register
	/// local plugins.
	/// </summary>
	/// <remarks>
	/// The script is loaded from <see cref="Url"/> + "/" + <see cref="FileName"/>. See <see cref="TinyMCE.ExternalPlugins"/> for an example.
	/// </remarks>
	public class ExternalPlugin
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ExternalPlugin"/>.
		/// </summary>
		/// <remarks>
		/// <see cref="Name"/> and <see cref="Url"/> are initialized to an empty string; <see cref="FileName"/> is null and must be set.
		/// </remarks>
		public ExternalPlugin()
		{
			this.Url = "";
			this.Name = "";
		}

		/// <summary>
		/// Returns or sets the name of the plugin. This is the name used in the <c>plugins</c> option.
		/// </summary>
		[DefaultValue("")]
		[Description("The name of the plugin. This is the name used in \"extraPlugins\".")]
		public string Name { get; set; }

		/// <summary>
		/// Returns or sets the URL of the folder that contains the plugin, i.e. "Plugins/wordcounter".
		/// </summary>
		/// <remarks>
		/// Relative URLs are resolved against the application's URL. A trailing "/" is added when missing.
		/// </remarks>
		[DefaultValue("")]
		[Description("The local URL of the plugin installation.")]
		public string Url { get; set; }

		/// <summary>
		/// Returns or sets the name of the JavaScript file of the plugin, usually "plugin.js".
		/// </summary>
		/// <remarks>
		/// The plugin is not loaded when this property is null or empty. The constructor doesn't initialize it,
		/// even though the designer's default value is "plugin.js".
		/// </remarks>
		[DefaultValue("plugin.js")]
		[Description("The name of the javascript file of the plugin, using its \"plugin.js\".")]
		public string FileName { get; set; }
	}
}
