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
	public class Editor : Widget, IWisejHandler
	{
		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
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

			// script = script.Replace("$options", options.ToString());

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
