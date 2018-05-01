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
	/// Represents a button in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarItemButton : RibbonBarItem
	{
		#region Events

		/// <summary>
		/// Fired when the user clicks one of the drop down menu items.
		/// </summary>
		[SRCategory("CatAction")]
		[SRDescription("Fired when the user clicks one of the drop down menu items.")]
		public event RibbonBarMenuItemEventHandler ItemClicked
		{
			add { base.AddHandler(nameof(ItemClicked), value); }
			remove { base.RemoveHandler(nameof(ItemClicked), value); }
		}

		/// <summary>
		/// Fires the <see cref="ItemClicked" /> event.
		/// </summary>
		/// <param name="e">A <see cref="RibbonBarMenuItemEventArgs" /> that contains the event data. </param>
		protected internal virtual void OnItemClick(RibbonBarMenuItemEventArgs e)
		{
			((RibbonBarMenuItemEventHandler)base.Events[nameof(ItemClicked)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the value of the <see cref="Pushed"/> property changes.
		/// </summary>
		[SRCategory("CatAction")]
		[SRDescription("Fired when the value of the Pushed property changes.")]
		public event EventHandler PushedChanged
		{
			add { base.AddHandler(nameof(PushedChanged), value); }
			remove { base.RemoveHandler(nameof(PushedChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="PushedChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventHandler" /> that contains the event data. </param>
		protected internal virtual void OnPushedChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(PushedChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarItemButton"/> 
		/// is rendered using the "pushed" state.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets whether the RibbonBarItemButton is rendered using the pushed state.")]
		public bool Pushed
		{
			get { return this._pushed; }
			set
			{
				if (this._pushed != value)
				{
					this._pushed = value;
					Update();
					OnPushedChanged(EventArgs.Empty);
				}
			}
		}
		private bool _pushed = false;

		/// <summary>
		/// Returns or sets the layout orientation of the <see cref="RibbonBarItemButton"/>.
		/// </summary>
		[DefaultValue(Orientation.Vertical)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the orientation of the RibbonBarItemButton.")]
		public Orientation Orientation
		{
			get { return this._orientation; }
			set
			{
				if (this._orientation != value)
				{
					this._orientation = value;
					Update();
				}
			}
		}
		private Orientation _orientation = Orientation.Vertical;

		/// <summary>
		/// Returns or sets a value indicating whether a new column starts after
		/// this <see cref="RibbonBarItem"/>.
		/// </summary>
		public override bool ColumnBreak
		{
			get { return base.ColumnBreak || this.Orientation == Orientation.Vertical; }
			set { base.ColumnBreak = value; }
		}

		private bool ShouldSerializeColumnBreak()
		{
			return base.ColumnBreak && this.Orientation == Orientation.Horizontal;
		}

		private void ResetColumnBreak()
		{
			base.ColumnBreak = false;
		}

		/// <summary>
		/// Returns the collection of <see cref="MenuItem" /> objects associated with the button.
		/// </summary>
		/// <returns>A <see cref="Menu.MenuItemCollection" /> that represents the list of <see cref="MenuItem" /> objects stored in the menu.</returns>
		[MergableProperty(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns the collection of MenuItem objects associated with the button.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Menu.MenuItemCollection MenuItems
		{
			get
			{
				if (this._menu == null)
					this._menu = new RibbonBarItemButtonMenu(this);

				return this._menu.MenuItems;
			}
		}
		private Menu _menu;

		// Check if the button has menu items without creating the context menu.
		private bool HasMenuItems
		{
			get
			{
				return this._menu != null && this._menu.IsParent;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Disposes the control.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				if (this._menu != null)
				{
					this._menu.Dispose();
					this._menu = null;
				}
			}
		}
		#endregion

		#region Wisej Implementation

		// Handles clicks and taps from the client: "execute" event.
		private void ProcessExecuteWebEvent(WisejEventArgs e)
		{
			// determine if the button can be clicked.
			if (this.Enabled && this.Visible)
			{
				this.RibbonBar?.OnItemClick(new RibbonBarItemEventArgs(this));
			}
		}

		// Handles clicks on the menu items associated with a RibbonBarItemButton.
		private void ProcessItemClickWebEvent(WisejEventArgs e)
		{
			// determine if the button can be clicked.
			if (this.Enabled && this.Visible)
			{
				this.RibbonBar?.OnMenuButtonItemClick(new RibbonBarMenuItemEventArgs(this, e.Parameters.Item));
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "itemClick":
					ProcessItemClickWebEvent(e);
					break;

				case "execute":
					ProcessExecuteWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Adds references components to the list. Referenced components
		/// can be added individually or as a reference to a collection.
		/// </summary>
		/// <param name="items">Container for the referenced components or collections.</param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			if (this._menu != null)
				items.Add(this._menu);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.ItemButton";
			config.orientation = this.Orientation;
			config.pushed = this.Pushed;

			if (me.DesignMode)
			{
				config.showArrow = this.HasMenuItems;
			}
			else
			{
				if (this.HasMenuItems)
				{
					config.buttonMenu = ((IWisejComponent)this._menu).Id;
					config.wiredEvents.Add("itemClick(Item)");
				}
				else
				{
					config.buttonMenu = null;
					config.wiredEvents.Add("execute");
				}
			}
		}

		#endregion

	}
}