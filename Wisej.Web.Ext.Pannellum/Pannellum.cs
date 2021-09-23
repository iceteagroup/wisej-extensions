///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wisej.Core;

namespace Wisej.Web.Ext.Pannellum
{
	/// <summary>
	/// Pannellum is a lightweight, free, and open source panorama viewer for the web.
	/// Built using HTML5, CSS3, JavaScript, and WebGL. See https://pannellum.org/.
	/// </summary>
	[ToolboxBitmapAttribute(typeof(Pannellum))]
	[Description("Pannellum is a lightweight, free, and open source panorama viewer for the web.")]
	public class Pannellum : Widget
	{

		#region Properties

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
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "pannellum.js",
						Source = GetResourceURL("Wisej.Web.Ext.Pannellum.JavaScript.pannellum-2.3.2.js")
					});
					base.Packages.Add(new Package()
					{
						Name = "pannellum.css",
						Source = GetResourceURL("Wisej.Web.Ext.Pannellum.JavaScript.pannellum-2.3.2.css")
					});
				}

				return base.Packages;
			}
		}

		#endregion

		#region Wisej Implementation

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			IWisejControl me = this;
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.Pannellum.JavaScript.startup.js");
			return script;
		}

		#endregion
	}
}
