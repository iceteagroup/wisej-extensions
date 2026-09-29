///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Provides basic functionality for controls that display a <see cref="ToolStripDropDown" /> when a <see cref="ToolStripDropDownButton" />, <see cref="ToolStripMenuItem" />, or <see cref="ToolStripSplitButton" /> control is clicked.
	/// </summary>
	public class ToolStripDropDownItem : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownItem" /> class. 
		///</summary>
		public ToolStripDropDownItem()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownItem" /> class with the specified display text, image, and <see cref="ToolStripItem" /> collection that the drop-down control contains.
		/// </summary>
		/// <param name="text">The text to display on the item.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the item.</param>
		/// <param name="dropDownItems">The items to add to <see cref="ToolStripDropDownItem.DropDownItems" />.</param>
		public ToolStripDropDownItem(string text, Image image, ToolStripItem[] dropDownItems)
		{
			this.Text = text;
			this.Image = image;
			this.DropDownItems.AddRange(dropDownItems);
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> closes. 
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		public event EventHandler DropDownClosed;

		/// <summary>
		/// Occurs as the <see cref="ToolStripDropDown" /> is opening.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		public event EventHandler DropDownOpening;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> has opened.
		///</summary>
		[SRDescription("ToolStripDropDownOpenedDescr")]
		[SRCategory("CatAction")]
		public event EventHandler DropDownOpened;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> is clicked.
		///</summary>
		[SRCategory("CatAction")]
		public event ToolStripItemClickedEventHandler DropDownItemClicked;

		#endregion

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownClosed" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		protected virtual void OnDropDownClosed(System.EventArgs e)
		{
			if ((this.DropDownClosed != null))
			{
				DropDownClosed(this, e);
			}
		}

		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		protected virtual void OnDropDownOpening(System.EventArgs e)
		{
			if ((this.DropDownOpening != null))
			{
				DropDownOpening(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownOpened" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripDropDownOpenedDescr")]
		[SRCategory("CatAction")]
		protected virtual void OnDropDownOpened(System.EventArgs e)
		{
			if ((this.DropDownOpened != null))
			{
				DropDownOpened(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownItemClicked" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		protected virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
		{
			if ((this.DropDownItemClicked != null))
			{
				DropDownItemClicked(this, e);
			}
		}

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="ToolStripDropDown" /> that will be displayed when this <see cref="ToolStripDropDownItem" /> is clicked.
		/// </summary>
		/// <returns>A <see cref="ToolStripDropDown" /> that is associated with the <see cref="ToolStripDropDownItem" />.</returns>
		/// <remarks>
		/// When not set, a default drop-down is created the first time it's needed; <see cref="ToolStripDropDownItem.DropDownItems" /> returns its <see cref="ToolStrip.Items" />.
		/// Assigning an existing <see cref="ToolStripDropDown" /> lets several items share the same drop-down.
		/// </remarks>
		/// <example>
		/// Sharing the same drop-down between a menu item and a drop-down button:
		/// <code><![CDATA[
		/// var colors = new ToolStripDropDown();
		/// colors.Items.Add("Red");
		/// colors.Items.Add("Green");
		/// colors.Items.Add("Blue");
		///
		/// this.toolStripMenuItemColor.DropDown = colors;
		/// this.toolStripDropDownButtonColor.DropDown = colors;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripDropDownDescr")]
		[SRCategory("CatData")]
		public ToolStripDropDown DropDown
		{
			get
			{
				return this._dropDown;
			}
			set
			{
				if ((this._dropDown != value))
				{
					this._dropDown = value;
				}
			}
		}

		private ToolStripDropDown _dropDown;

		/// <summary>
		/// Returns or sets a value indicating the direction in which the <see cref="ToolStripDropDown" /> emerges from its parent container.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripDropDownDirection" /> values.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The property is set to a value that is not one of the <see cref="ToolStripDropDownDirection" /> values.</exception>
		/// <remarks>
		/// <see cref="ToolStripDropDownDirection.Default" /> lets the item choose the direction based on its position and on the <see cref="ToolStripItem.RightToLeft" /> setting.
		/// </remarks>
		/// <example>
		/// Opening the drop-down of a button placed on a status bar upwards:
		/// <code><![CDATA[
		/// this.toolStripDropDownButtonZoom.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[SRDescription("ToolStripDropDownItemDropDownDirectionDescr")]
		[SRCategory("CatBehavior")]
		public ToolStripDropDownDirection DropDownDirection
		{
			get
			{
				return this._dropDownDirection;
			}
			set
			{
				if ((this._dropDownDirection != value))
				{
					this._dropDownDirection = value;
				}
			}
		}

		private ToolStripDropDownDirection _dropDownDirection;

		/// <summary>
		/// Returns the collection of items in the <see cref="ToolStripDropDown" /> that is associated with this <see cref="ToolStripDropDownItem" />.
		/// </summary>
		/// <returns>A <see cref="ToolStripItemCollection" /> of items.</returns>
		/// <remarks>
		/// This is the <see cref="ToolStrip.Items" /> collection of <see cref="ToolStripDropDownItem.DropDown" />.
		/// </remarks>
		/// <example>
		/// Populating a sub-menu at runtime:
		/// <code><![CDATA[
		/// this.toolStripMenuItemRecent.DropDownItems.Clear();
		/// foreach (string file in GetRecentFiles())
		/// {
		///     this.toolStripMenuItemRecent.DropDownItems.Add(file, null, (s, e) => OpenDocument(file));
		/// }
		/// ]]></code>
		/// </example>
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ToolStripDropDownItemsDescr")]
		public ToolStripItemCollection DropDownItems
		{
			get
			{
				return this._dropDownItems;
			}
		}

		private ToolStripItemCollection _dropDownItems;

		/// <summary>
		/// Returns a value indicating whether the <see cref="ToolStripDropDownItem" /> has items in its <see cref="ToolStripDropDownItem.DropDownItems" /> collection.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDownItem" /> has drop-down items; otherwise, false.</returns>
		[Browsable(false)]
		public virtual bool HasDropDownItems
		{
			get
			{
				return this._hasDropDownItems;
			}
		}

		private bool _hasDropDownItems;

		/// <summary>
		/// Returns a value indicating whether a <see cref="ToolStripDropDown" /> has been created or assigned to <see cref="ToolStripDropDownItem.DropDown" />.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDownItem" /> has a <see cref="ToolStripDropDown" />; otherwise, false.</returns>
		/// <remarks>
		/// Unlike reading <see cref="ToolStripDropDownItem.DropDown" />, checking this property doesn't create the default drop-down.
		/// </remarks>
		[Browsable(false)]
		public bool HasDropDown
		{
			get
			{
				return this._hasDropDown;
			}
		}

		private bool _hasDropDown;

		/// <summary>
		/// Returns a value indicating whether the <see cref="ToolStripDropDownItem" /> is in the pressed state.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDownItem" /> is in the pressed state (its drop-down is open); otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Pressed
		{
			get
			{
				return this._pressed;
			}
		}

		private bool _pressed;

		#endregion

		#region Methods

		/// <summary>
		/// Creates a generic <see cref="ToolStripDropDown" /> for which events can be defined.
		///</summary>
		/// <returns>A <see cref="ToolStripDropDown" />.</returns>
		protected virtual ToolStripDropDown CreateDefaultDropDown()
		{
			// TODO: Implement
			return new ToolStripDropDown();
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		//ITG:TODO: Review
		//protected override void OnBoundsChanged()
		//{
		//	base.OnBoundsChanged();
		//	// TODO: Implement
		//}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.HideDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnDropDownHide(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.ShowDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnDropDownShow(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Makes a visible <see cref="ToolStripDropDown" /> hidden.
		/// </summary>
		/// <remarks>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownClosed" /> event.
		/// </remarks>
		/// <example>
		/// Closing the drop-down after the user picks a value:
		/// <code><![CDATA[
		/// private void toolStripDropDownButtonColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		/// {
		///     this.selectedColor = e.ClickedItem.Text;
		///     this.toolStripDropDownButtonColor.HideDropDown();
		/// }
		/// ]]></code>
		/// </example>
		public void HideDropDown()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Displays the <see cref="ToolStripDropDown" /> associated with this <see cref="ToolStripDropDownItem" />.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">The <see cref="ToolStripDropDownItem" /> is the same as the parent <see cref="ToolStrip" />.</exception>
		/// <remarks>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownOpening" /> and <see cref="ToolStripDropDownItem.DropDownOpened" /> events.
		/// The drop-down is displayed only if the item has drop-down items.
		/// </remarks>
		/// <example>
		/// Opening a menu from code:
		/// <code><![CDATA[
		/// private void buttonOptions_Click(object sender, EventArgs e)
		/// {
		///     this.toolStripDropDownButtonOptions.ShowDropDown();
		/// }
		/// ]]></code>
		/// </example>
		public void ShowDropDown()
		{
			// TODO: Implement
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion
	}

}
