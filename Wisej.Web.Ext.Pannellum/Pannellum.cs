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
	/// Built using HTML5, CSS3, JavaScript, and WebGL. See <see href="https://pannellum.org/"/>.
	/// </summary>
	/// <remarks>
	/// The viewer is configured through the dynamic <c>Options</c> property inherited from <see cref="T:Wisej.Web.Widget"/>
	/// using the configuration keys documented at <see href="https://pannellum.org/documentation/reference/"/>.
	/// Any change to the options destroys and recreates the viewer on the client.
	/// In design mode <c>autoLoad</c> is forced to false and the load button is removed.
	/// <para>
	/// The client fires the following widget events, received on the server through the <c>WidgetEvent</c> event
	/// (<c>e.Type</c>): "load", "scenechange" (<c>e.Data</c> is the scene id), "error" (<c>e.Data</c> is the error message),
	/// "errorcleared" and "hotspot" (<c>e.Data</c> is the hot spot configuration that was clicked).
	/// </para>
	/// </remarks>
	/// <example>
	/// Loading an equirectangular panorama with a clickable hot spot:
	/// <code><![CDATA[
	/// this.pannellum1.Options.type = "equirectangular";
	/// this.pannellum1.Options.panorama = "Images/lobby.jpg";
	/// this.pannellum1.Options.autoLoad = true;
	/// this.pannellum1.Options.hotSpots = new[] {
	///     new { pitch = -2.1, yaw = 132.9, type = "info", text = "Reception" }
	/// };
	/// this.pannellum1.Update();
	///
	/// this.pannellum1.WidgetEvent += (s, e) =>
	/// {
	///     if (e.Type == "hotspot")
	///         AlertBox.Show("Clicked: " + e.Data.text);
	/// };
	/// ]]></code>
	/// </example>
	[ToolboxBitmapAttribute(typeof(Pannellum))]
	[Description("Pannellum is a lightweight, free, and open source panorama viewer for the web.")]
	public class Pannellum : Widget
	{

		#region Properties

		/// <summary>
		/// Returns the initialization script that creates the Pannellum viewer on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded startup.js resource; the setter is ignored.
		/// Configure the viewer using the <c>Options</c> property instead.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns the list of packages (pannellum.js and pannellum.css, version 2.3.2) loaded
		/// by the client before the widget is initialized.
		/// </summary>
		/// <remarks>
		/// The packages are served from the resources embedded in this assembly and are added
		/// the first time the property is read, only if the list is empty.
		/// </remarks>
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
