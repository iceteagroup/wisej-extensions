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
using System.Drawing.Design;
using Wisej.Core;

namespace Wisej.Web.Ext.Polymer
{
	/// <summary>
	/// Represents a polymer (https://elements.polymer-project.org/) non-visual component.
	/// Used to import polymer libraries, such as iron-icons sets and others.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(PolymerWidget))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The PolymerComponent component represents a set of polymer libraries to import in the application's page using &lt;link rel='import'&gt; elements.")]
	public class PolymerComponent : Wisej.Base.Component, IComponent
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Polymer.PolymerComponent" /> class.
		/// </summary>
		public PolymerComponent()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Polymer.PolymerComponent" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
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
		/// Returns or sets the base url for the polymer files.
		/// </summary>
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
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
