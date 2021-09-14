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

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represent a page in a <see cref="RibbonBar"/> control.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarPage : Wisej.Web.Component
	{
		#region Properties

		/// <summary>
		/// Returns the 
		/// <see cref="Wisej.Web.Ext.RibbonBar.RibbonBar"/> that owns this
		/// <see cref="RibbonBarPage"/>.
		/// </summary>
		[Browsable(false)]
		public RibbonBar Parent
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the <see cref="RibbonBar"/> that contains this <see cref="RibbonBarPage"/>;
		/// </summary>
		[Browsable(false)]
		public RibbonBar RibbonBar
		{
			get { return this.Parent; }
		}

		/// <summary>
		/// Returns the <see cref="RibbonBarGroup"/> collection in the <see cref="RibbonBarPage"/>.
		/// </summary>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Returns the collection of RibbonBarGroup children.")]
		public RibbonBarGroupCollection Groups
		{
			get
			{
				if (this._groups == null)
					this._groups = new RibbonBarGroupCollection(this);

				return this._groups;
			}
		}
		private RibbonBarGroupCollection _groups;

		/// <summary>
		/// Returns or sets the title of the <see cref="RibbonBarPage"/>.
		///</summary>
		/// <returns>The text displayed in the button that corresponds to this page.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the title of the RibbonBarPage.")]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._text != value)
				{
					this._text = value;
					Update();
				}
			}
		}
		private string _text = string.Empty;

		/// <summary>
		/// Returns or sets the background color of the <see cref="RibbonBarPage"/> tab button.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the background color of the RibbonBarPage tab button.")]
		public Color TabBackColor
		{
			get { return this._tabBackColor; }
			set
			{
				if (this._tabBackColor != value)
				{
					this._tabBackColor = value;
					Update();
				}
			}
		}
		private Color _tabBackColor = Color.Empty;

		/// <summary>
		/// Returns or sets the text color of the <see cref="RibbonBarPage"/> tab button.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the text color of the RibbonBarPage tab button.")]
		public Color TabForeColor
		{
			get { return this._tabForeColor; }
			set
			{
				if (this._tabForeColor != value)
				{
					this._tabForeColor = value;
					Update();
				}
			}
		}
		private Color _tabForeColor = Color.Empty;

		/// <summary>
		/// Returns or sets whether the first character 
		/// that is preceded by an ampersand (&amp;) is used as the mnemonic key
		/// to select this <see cref="RibbonBarPage"/>.
		///</summary>
		/// <returns>true if the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key.")]
		public bool UseMnemonic
		{
			get
			{
				return this._useMnemonic;
			}
			set
			{
				if (this._useMnemonic != value)
				{
					this._useMnemonic = value;
					Update();
				}
			}
		}
		private bool _useMnemonic = true;

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarPage"/> is visible or hidden.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the RibbonBarPage is visible or hidden.")]
		public virtual bool Visible
		{
			get { return this._visible; }
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					Update();
				}
			}
		}
		private bool _visible = true;

		/// <summary>
		/// Returns or sets whether the items in the <see cref="RibbonBarPage"/> can respond to user interaction.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the items in the RibbonBarPage can respond to user interaction.")]
		public virtual bool Enabled
		{
			get { return this._enabled; }
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					Update();
				}
			}
		}
		private bool _enabled = true;

		/// <summary>
		/// Returns or sets the name of the <see cref="RibbonBarPage"/>.
		/// </summary>
		/// <returns>The name of the <see cref="RibbonBarPage"/>. The default is an empty string ("").</returns>
		[Browsable(false)]
		public string Name
		{
			get
			{
				if (this.Site != null && !String.IsNullOrEmpty(this.Site.Name))
					this._name = this.Site.Name;

				return this._name;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._name != value)
				{
					this._name = value;

					if (this.Site != null)
						this.Site.Name = value;
				}
			}
		}
		private string _name = string.Empty;

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarPage"/> is the 
		/// currently <see cref="RibbonBar.SelectedPage"/>.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get { return this.RibbonBar?.SelectedPage == this; }
			set
			{
				if (this.RibbonBar != null)
					this.RibbonBar.SelectedPage = this;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="RibbonBarPage" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._groups != null)
				{
					this._groups.Clear(true);
					this._groups = null;
				}
				this.RibbonBar?.Pages.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		public override void Update()
		{
			if (this.DesignMode)
				this.RibbonBar?.Update();

			base.Update();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current object.</returns>
		public override string ToString()
		{
			return String.Concat(base.ToString(), ", Text: ", this.Text);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns a collection of referenced components or collection of components.
		/// </summary>
		///<param name="items"></param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			items.Add(this.Groups);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.RibbonPage";
			config.enabled = this.Enabled;
			config.hidden = !this.Visible;
			config.tabTextColor = this.TabForeColor;
			config.tabBackgroundColor = this.TabBackColor;
			config.label = TextUtils.EscapeText(this.Text, false, this.UseMnemonic, false);
			config.mnemonic = this.UseMnemonic ? TextUtils.GeMnemonic(this.Text) : null;

			if (me.DesignMode)
			{
				if (me.IsNew || me.IsDirty)
					config.controls = this.Groups.Render();
			}
			else
			{
				if (me.IsNew || this.Groups.IsDirty)
					config.controls = this.Groups.Render();
			}

		}

		#endregion
	}
}