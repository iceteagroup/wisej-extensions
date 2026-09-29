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

using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;
using Wisej.Web.Layout;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents the drop down that displays the items that overflow a <see cref="ToolStrip" />.
	/// </summary>
	/// <remarks>
	/// The overflow drop down is opened by the <see cref="ToolStrip.OverflowButton"/> and contains the items whose
	/// <see cref="ToolStripItem.Placement"/> is <see cref="ToolStripItemPlacement.Overflow"/>.
	/// </remarks>
	public class ToolStripOverflow : ToolStripDropDown
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripOverflow" /> class owned by the specified <see cref="ToolStripItem" />.
		/// </summary>
		/// <param name="parentItem">The <see cref="ToolStripItem" /> (usually the <see cref="ToolStripOverflowButton"/>) that owns this <see cref="ToolStripOverflow" /> instance.</param>
		public ToolStripOverflow(ToolStripItem parentItem)
		{
			this._parentItem = parentItem;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns all of the items on the <see cref="ToolStrip" /> that owns the overflow, whether they are currently being displayed or not.
		/// </summary>
		/// <returns>A <see cref="ToolStripItemCollection" /> containing all of the items.</returns>
		/// <remarks>
		/// Only the items with <see cref="ToolStripItem.Placement"/> set to <see cref="ToolStripItemPlacement.Overflow"/> are displayed in the overflow drop down.
		/// </remarks>
		/// <example>
		/// Listing the items currently in the overflow:
		/// <code><![CDATA[
		/// foreach (ToolStripItem item in this.toolStrip1.OverflowButton.DropDown.Items)
		/// {
		///     if (item.IsOnOverflow)
		///         System.Diagnostics.Debug.WriteLine(item.Text);
		/// }
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripItemsDescr")]
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override ToolStripItemCollection Items
		{
			get
			{
				return this._items;
			}
		}

		private ToolStripItemCollection _items;

		/// <summary>
		/// Returns the <see cref="Wisej.Web.Layout.LayoutEngine"/> used to arrange the overflow items.
		/// </summary>
		/// <returns>The <see cref="Wisej.Web.Layout.LayoutEngine"/> that lays out the items.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return this._layoutEngine;
			}
		}

		private LayoutEngine _layoutEngine;
		private ToolStripItem _parentItem;

		#endregion

		#region Methods

		/// <summary>
		/// Resets the collection of displayed and overflow items after a layout is done.
		///</summary>
		protected override void SetDisplayedItems()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the size of a rectangular area into which the overflow drop down can be fitted.
		/// </summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <example>
		/// Measuring the overflow drop down:
		/// <code><![CDATA[
		/// var overflow = (ToolStripOverflow)this.toolStrip1.OverflowButton.DropDown;
		/// var size = overflow.GetPreferredSize(new Size(300, 0));
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override Size GetPreferredSize(Size constrainingSize)
		{
			// TODO: Implement
			return new System.Drawing.Size();
		}

		/// <summary>
		/// Raises the <see cref="Control.Layout" /> event.
		///</summary>
		/// <param name="e">A <see cref="LayoutEventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			// TODO: Implement
		}

		/// <summary>
		/// Creates a new accessibility object for the control.
		///</summary>
		/// <returns>A new <see cref="AccessibleObject" /> for the control.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new AccessibleObject();
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
