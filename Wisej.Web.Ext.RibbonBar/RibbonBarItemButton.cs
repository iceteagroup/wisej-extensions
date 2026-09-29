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
	/// <remarks>
	/// <para>
	/// The button can be displayed large, with the image above the text (<see cref="Orientation.Vertical"/>, the default), or
	/// small, with the image to the left of the text (<see cref="Orientation.Horizontal"/>).
	/// </para><para>
	/// When the button has <see cref="MenuItems"/>, clicking it opens the drop down menu and doesn't fire the click events;
	/// clicking a menu item fires <see cref="ItemClicked"/> and <see cref="RibbonBar.MenuButtonItemClick"/>.
	/// Use <see cref="RibbonBarItemSplitButton"/> to have both a clickable button and a drop down menu.
	/// Without menu items, clicking the button fires <see cref="RibbonBarItem.Click"/> and <see cref="RibbonBar.ItemClick"/>.
	/// </para>
	/// </remarks>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
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
		/// <returns>true if the button is displayed as pushed; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The pushed state is only visual: the button doesn't toggle it automatically when clicked.
		/// Changing the value fires the <see cref="PushedChanged"/> event.
		/// </remarks>
		/// <example>
		/// Using the button as a toggle button:
		/// <code><![CDATA[
		/// private void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     this.buttonBold.Pushed = !this.buttonBold.Pushed;
		///     ApplyBold(this.buttonBold.Pushed);
		/// }
		/// ]]></code>
		/// </example>
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
		/// <returns>One of the <see cref="Orientation"/> values. The default is <see cref="Orientation.Vertical"/>.</returns>
		/// <remarks>
		/// <see cref="Orientation.Vertical"/> displays a large button with the image above the text that fills the height of the group
		/// and occupies a column of its own. <see cref="Orientation.Horizontal"/> displays a small button with the image to the left of
		/// the text; small buttons are stacked vertically in the same column until an item has <see cref="ColumnBreak"/> set to true.
		/// Buttons added to a <see cref="RibbonBarItemButtonGroup"/> are always changed to <see cref="Orientation.Horizontal"/>.
		/// </remarks>
		/// <example>
		/// Creating a large button and two small buttons stacked in the next column:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Add(new RibbonBarItemButton { Text = "Paste", ImageSource = "icon-paste" });
		/// this.ribbonBarGroup1.Items.Add(new RibbonBarItemButton { Text = "Cut", ImageSource = "icon-cut", Orientation = Orientation.Horizontal });
		/// this.ribbonBarGroup1.Items.Add(new RibbonBarItemButton { Text = "Copy", ImageSource = "icon-copy", Orientation = Orientation.Horizontal });
		/// ]]></code>
		/// </example>
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
		/// <returns>true if the next item in the <see cref="RibbonBarGroup"/> is placed in a new column; otherwise, false.</returns>
		/// <remarks>
		/// Always returns true when <see cref="Orientation"/> is <see cref="Orientation.Vertical"/>, since large buttons
		/// occupy a column of their own. The value set is used only when <see cref="Orientation"/> is <see cref="Orientation.Horizontal"/>.
		/// </remarks>
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
		/// <remarks>
		/// When the collection contains at least one item, the button shows a drop down arrow and clicking it opens the menu
		/// below the button instead of firing the click events. Clicking a menu item, at any level, fires the <see cref="ItemClicked"/> event
		/// on this button and the <see cref="RibbonBar.MenuButtonItemClick"/> event on the <see cref="RibbonBar"/>.
		/// </remarks>
		/// <example>
		/// Adding a drop down menu to a button and handling the clicks on the menu items:
		/// <code><![CDATA[
		/// this.buttonPaste.MenuItems.Add(new MenuItem("Paste") { Name = "paste" });
		/// this.buttonPaste.MenuItems.Add(new MenuItem("Paste Special...") { Name = "pasteSpecial" });
		/// this.buttonPaste.MenuItems.Add(new MenuItem("Paste as Text") { Name = "pasteText" });
		///
		/// this.buttonPaste.ItemClicked += (s, e) =>
		/// {
		///     switch (e.MenuItem.Name)
		///     {
		///         case "paste": Paste(); break;
		///         case "pasteSpecial": new PasteSpecialDialog().ShowDialog(); break;
		///         case "pasteText": PasteAsText(); break;
		///     }
		/// };
		/// ]]></code>
		/// </example>
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
				MenuItem item = e.Parameters.Item;
				this.RibbonBar?.OnMenuButtonItemClick(new RibbonBarMenuItemEventArgs(this, item));
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