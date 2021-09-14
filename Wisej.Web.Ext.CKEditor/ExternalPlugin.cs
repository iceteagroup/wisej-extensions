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

namespace Wisej.Web.Ext.CKEditor
{
	/// <summary>
	/// Represents a local plugin that can be used with a CDN installation of the CKEditor or
	/// a local installation, although for local installations, it is not necessary to register
	/// local plugins. See https://cdn.ckeditor.com/ under "Enabling Local Plugins.
	/// </summary>
	[ApiCategory("CKEditor")]
	public class ExternalPlugin
	{
		/// <summary>
		/// Initializes a new instance of <see cref="T:Wisej.Web.Ext.CKEditor.ExternalPlugin"/>.
		/// </summary>
		public ExternalPlugin()
		{
			this.Url = "";
			this.Name = "";
			this.FileName = "plugin.js";
		}

		/// <summary>
		/// The name of the plugin. This is the name used in "extraPlugins".
		/// </summary>
		[DefaultValue("")]
		[Description("The name of the plugin. This is the name used in \"extraPlugins\".")]
		public string Name { get; set; }

		/// <summary>
		/// The local URL of the plugin.
		/// </summary>
		[DefaultValue("")]
		[Description("The local URL of the plugin installation.")]
		public string Url { get; set; }

		/// <summary>
		/// The name of the javascript file of the plugin, using it's "plugin.js".
		/// </summary>
		[DefaultValue("plugin.js")]
		[Description("The name of the javascript file of the plugin, using it's \"plugin.js\".")]
		public string FileName { get; set; }
	}
}
