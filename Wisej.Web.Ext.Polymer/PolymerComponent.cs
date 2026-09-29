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
using Wisej.Core;

namespace Wisej.Web.Ext.Polymer
{
	/// <summary>
	/// Represents a Polymer (<see href="https://www.polymer-project.org"/>) non-visual component.
	/// Used to import polymer libraries, such as iron-icons sets and others.
	/// </summary>
	/// <remarks>
	/// The libraries listed in <see cref="Imports"/> are imported in the browser using <c>&lt;link rel="import"&gt;</c>
	/// elements. Each library is loaded only once per browser page.
	/// </remarks>
	/// <example>
	/// Importing the iron-icons sets used by <see cref="PolymerWidget"/> controls:
	/// <code><![CDATA[
	/// var polymerComponent = new PolymerComponent(this.components)
	/// {
	///     Imports = new[]
	///     {
	///         "iron-icons/iron-icons.html",
	///         "iron-icons/social-icons.html"
	///     }
	/// };
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(PolymerWidget))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The PolymerComponent component represents a set of polymer libraries to import in the application's page using &lt;link rel='import'&gt; elements.")]
	[ApiCategory("Polymer")]
	public class PolymerComponent : Wisej.Base.Component, IComponent
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PolymerComponent" /> class.
		/// </summary>
		public PolymerComponent()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PolymerComponent" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="IContainer" /> that represents the container for the component.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		public PolymerComponent(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the base URL for the polymer files.
		/// </summary>
		/// <remarks>
		/// Relative entries in <see cref="Imports"/> and the elements created by <see cref="PolymerWidget"/> are loaded
		/// from this URL; it must end with a slash. The default is <c>"https://wisej.s3.amazonaws.com/libs/polymers/"</c>.
		/// This is a static setting shared by all sessions, but the new value is sent only to the browser of the current
		/// session: set it before creating any Polymer control, i.e. in <c>Program.Main</c>, for every session.
		/// A null value is converted to an empty string.
		/// </remarks>
		/// <example>
		/// Loading the Polymer libraries from the application's own folder:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     PolymerComponent.PolymerBaseUrl = "https://myapp.example.com/polymer/";
		///     new MainPage().Show();
		/// }
		/// ]]></code>
		/// </example>
		public static string PolymerBaseUrl
		{
			get { return _polymerBaseUrl; }
			set
			{
				value = value ?? string.Empty;
				if (_polymerBaseUrl != value)
				{
					_polymerBaseUrl = value;
					Application.Eval("wisej.web.ext.PolymerComponent.PolymerBaseUrl = \"" + value + "\"");
				}
			}
		}
		private static string _polymerBaseUrl = "https://wisej.s3.amazonaws.com/libs/polymers/";

		/// <summary>
		/// Returns or sets the list of polymer libraries to import.
		/// </summary>
		/// <remarks>
		/// Each entry is either an absolute URL (starting with <c>http:</c> or <c>https:</c>) or a path relative to
		/// <see cref="PolymerBaseUrl"/>. Libraries already loaded in the browser are not loaded again.
		/// </remarks>
		/// <example>
		/// Importing a relative and an absolute library:
		/// <code><![CDATA[
		/// this.polymerComponent1.Imports = new[]
		/// {
		///     "iron-icons/maps-icons.html",
		///     "https://cdn.example.com/elements/my-element.html"
		/// };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string[] Imports
		{
			get { return this._imports; }
			set
			{
				this._imports = value;
				Update();
			}
		}
		private string[] _imports;

		#endregion

		#region IComponent

		/// <summary>
		/// Returns or sets the <see cref="T:System.ComponentModel.ISite" /> associated with 
		/// the <see cref="T:System.ComponentModel.IComponent" />.
		/// </summary>
		/// <remarks>
		/// This property is used by the designer. Setting it also updates the design mode state of the component.
		/// </remarks>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get { return this._site; }
			set
			{
				this._site = value;
				((IWisejComponent)this).DesignMode = value == null ? false : value.DesignMode;
			}
		}
		private ISite _site;

		/// <summary>
		/// Returns a value that indicates whether the <see cref="T:System.ComponentModel.IComponent" /> is currently in design mode.
		/// </summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IComponent" /> is in design mode; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get { return this._site != null && this._site.DesignMode; }
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			IWisejComponent me = this;
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.PolymerComponent";
			config.imports = this.Imports;
		}

		#endregion

	}
}
