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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web;
using Wisej.Core;

namespace Wisej.Web.Ext.OnlyOffice
{
	/// <summary>
	/// Hosts the ONLYOFFICE document editor (<see href="https://api.onlyoffice.com/editors/basic"/>).
	/// </summary>
	/// <remarks>
	/// The editor script (api.js) is loaded from the ONLYOFFICE Document Server set in <see cref="OnlyOfficeURL"/>.
	/// This control is a preliminary implementation: the client configuration is currently fixed in the embedded
	/// startup.js script (a .docx document loaded from "Documents/Features Table.docx") and the server
	/// options are not sent to the client.
	/// </remarks>
	/// <example>
	/// Adding the editor to a page and pointing it to a private Document Server:
	/// <code><![CDATA[
	/// var editor = new Wisej.Web.Ext.OnlyOffice.Editor();
	/// editor.Dock = DockStyle.Fill;
	/// editor.OnlyOfficeURL = "https://docs.example.com/web-apps/apps/api/documents/api.js";
	/// this.Controls.Add(editor);
	/// ]]></code>
	/// </example>
	public class Editor : Widget, IWisejHandler
	{
		/// <summary>
		/// Returns the initialization script that creates the ONLYOFFICE <c>DocsAPI.DocEditor</c> on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded startup.js resource; the setter is ignored.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns the list of packages loaded by the client before the widget is initialized.
		/// </summary>
		/// <remarks>
		/// The list contains the ONLYOFFICE api.js script located at <see cref="OnlyOfficeURL"/>.
		/// The package is added the first time the property is read, only if the list is empty; changing
		/// <see cref="OnlyOfficeURL"/> after that doesn't change the source of the package already in the list.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "OnlyOffice",
						Source = this.OnlyOfficeURL
					});
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Returns or sets the URL of the ONLYOFFICE Document Server api.js script.
		/// </summary>
		/// <remarks>
		/// The default value is "https://doc.onlyoffice.com/OfficeWeb/apps/api/documents/api.js".
		/// Setting it to null or an empty string restores the default. Changing the value calls <c>Update()</c>.
		/// The URL is used when the <see cref="Packages"/> list is first populated, so set it before
		/// the control is created on the client.
		/// </remarks>
		/// <example>
		/// Using a self-hosted Document Server:
		/// <code><![CDATA[
		/// this.editor1.OnlyOfficeURL = "https://docs.example.com/web-apps/apps/api/documents/api.js";
		/// ]]></code>
		/// </example>
		public string OnlyOfficeURL
		{
			get
			{
				return this._onlyOfficeURL ?? "https://doc.onlyoffice.com/OfficeWeb/apps/api/documents/api.js";
			}
			set
			{
				value = value == string.Empty ? null : value;

				if (this._onlyOfficeURL != value)
				{
					this._onlyOfficeURL = value;
					Update();
				}
			}
		}

		private string _onlyOfficeURL = null;

		private bool ShouldSerializeComponentURL()
		{
			return this._onlyOfficeURL != null;
		}

		private void ResetComponentURL()
		{
			this._onlyOfficeURL = null;
		}

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{

			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.OnlyOffice.JavaScript.startup.js");

			// script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));

			return script;
		}

		bool IWisejHandler.Compress
		{
			get { return false; }
		}

		void IWisejHandler.ProcessRequest(HttpContext context)
		{
		}
	}
}
