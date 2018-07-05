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
using System.Collections;
using System.ComponentModel;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a group of <see cref="RibbonBarItem"/> in a <see cref="RibbonBarPage"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarGroup : Wisej.Web.Component
	{
		#region Events

		/// <summary>
		/// Fired when the user clicks the <see cref="RibbonBarGroup"/> button next to the label.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the user clicks the RibbonBarGroup button next to the label.")]
		public event EventHandler Click
		{
			add { base.AddHandler(nameof(Click), value); }
			remove { base.RemoveHandler(nameof(Click), value); }
		}

		/// <summary>
		/// Fires the <see cref="Click" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		protected internal virtual void OnClick(EventArgs e)
		{
			((EventHandler)base.Events[nameof(Click)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the 
		/// <see cref="RibbonBarPage"/> that owns this
		/// <see cref="RibbonBarGroup"/>.
		/// </summary>
		[Browsable(false)]
		public RibbonBarPage Parent
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the <see cref="RibbonBar"/> that contains this <see cref="RibbonBarGroup"/>;
		/// </summary>
		[Browsable(false)]
		public RibbonBar RibbonBar
		{
			get { return this.Parent?.Parent; }
		}

		/// <summary>
		/// Returns or sets whether the items in the <see cref="RibbonBarGroup"/> can respond to user interaction.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the items in the RibbonBarGroup can respond to user interaction.")]
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
		/// Returns or sets whether the <see cref="RibbonBarGroup"/> is visible or hidden.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the RibbonBarGroup is visible or hidden.")]
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
		/// Returns the collection of <see cref="RibbonBarItem"/> children.
		/// </summary>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Returns the collection of RibbonBarItem children.")]
		public RibbonBarItemCollection Items
		{
			get
			{
				if (this._items == null)
					this._items = new RibbonBarItemCollection(this);

				return this._items;
			}
		}
		private RibbonBarItemCollection _items;

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarGroup"/> shows the expand
		/// button next to the group's title.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets whether the RibbonBarGroup shows the expand button next to the group's title.")]
		public bool ShowButton
		{
			get { return this._showButton; }
			set
			{
				if (this._showButton != value)
				{
					this._showButton = value;
					Update();
				}
			}
		}
		private bool _showButton = false;

		/// <summary>
		/// Returns or sets the title of the <see cref="RibbonBarGroup"/>.
		///</summary>
		/// <returns>The text displayed at the bottom of the group.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the title of the RibbonBarGroup.")]
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

		#endregion

		#region Methods

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="RibbonBarGroup" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._items != null)
				{
					this._items.Clear(true);
					this._items = null;
				}
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

		// Handles clicks on the group button.
		private void ProcessButtonClickWebEvent(WisejEventArgs e)
		{
			// determine if the button can be clicked.
			if (this.Enabled && this.Visible)
			{
				this.RibbonBar?.OnGroupClick(new RibbonBarGroupEventArgs(this));
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "buttonClick":
					ProcessButtonClickWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Returns a collection of referenced components or collection of components.
		/// </summary>
		///<param name="items"></param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			items.Add(this.Items);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.RibbonGroup";
			config.showButton = this.ShowButton;
			config.visible = this.Visible;
			config.enabled = this.Enabled;
			config.label = TextUtils.EscapeText(this.Text, false, false, false);

			if (me.DesignMode)
			{
				if (me.IsNew || me.IsDirty)
					config.controls = this.Items.Render();
			}
			else
			{
				if (me.IsNew || this.Items.IsDirty)
					config.controls = this.Items.Render();

				config.wiredEvents = new WiredEvents();
				config.wiredEvents.Add("buttonClick");
			}
		}

		#endregion

	}
}