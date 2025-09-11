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
	/// Manages the overflow behavior of a <see cref="ToolStrip" />.
	///</summary>
	public class ToolStripOverflow : ToolStripDropDown
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripOverflow" /> class derived from a base <see cref="ToolStripItem" />.
		///</summary>
		/// <param name="parentItem">The <see cref="ToolStripItem" /> from which to derive this <see cref="ToolStripOverflow" /> instance. </param>
		public ToolStripOverflow(ToolStripItem parentItem)
		{
			this._parentItem = parentItem;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets all of the items on the <see cref="ToolStrip" />, whether they are currently being displayed or not.
		///</summary>
		/// <returns>A <see cref="ToolStripItemCollection" /> containing all of the items.</returns>
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
		/// Retrieves the size of a rectangular area into which a control can be fitted.
		///</summary>
		/// <returns>An ordered pair of type <see cref="System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
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
