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

using System.Drawing;
using Wisej.Core;
using Wisej.Web.Layout;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a cell of a <see cref="ToolStripPanelRow"/> that hosts a single control, usually a <see cref="ToolStrip"/>,
	/// and tracks the space assigned to it by the row layout.
	/// </summary>
	public class ToolStripPanelCell : Control
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripPanelCell" /> class hosting the specified control.
		/// </summary>
		/// <param name="control">The <see cref="Control"/> hosted by the cell.</param>
		public ToolStripPanelCell(Control control)
		{
			this._control = control;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripPanelCell" /> class hosting the specified control in the specified row.
		/// </summary>
		/// <param name="parent">The <see cref="ToolStripPanelRow"/> that contains the cell.</param>
		/// <param name="control">The <see cref="Control"/> hosted by the cell.</param>
		public ToolStripPanelCell(ToolStripPanelRow parent, Control control)
		{
			this._parent = parent;
			this._control = control;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the bounds, in pixels, last calculated for the cell by the row layout.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> with the cached size and location of the cell.</returns>
		public Rectangle CachedBounds
		{
			get
			{
				return this._cachedBounds;
			}
			set
			{
				if ((this._cachedBounds != value))
				{
					this._cachedBounds = value;
				}
			}
		}

		private Rectangle _cachedBounds;

		/// <summary>
		/// Returns the control hosted by the cell.
		/// </summary>
		/// <returns>The <see cref="Control"/> hosted by the cell.</returns>
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		private ToolStripPanelRow _parent;
		private Control _control;

		/// <summary>
		/// Returns whether the hosted control is in design mode.
		/// </summary>
		public bool ControlInDesignMode
		{
			get
			{
				return this._controlInDesignMode;
			}
		}

		private bool _controlInDesignMode;

		//Todo: Implement in Wisej?

		//public IArrangedElement InnerElement
		//{
		//	get
		//	{
		//		return this._innerElement;
		//	}
		//}

		//private IArrangedElement _innerElement;

		/// <summary>
		/// Returns the control that is being dragged into the cell.
		/// </summary>
		public ISupportToolStripPanel DraggedControl
		{
			get
			{
				return this._draggedControl;
			}
		}

		private ISupportToolStripPanel _draggedControl;

		/// <summary>
		/// Returns or sets the <see cref="ToolStripPanelRow"/> that contains the cell.
		/// </summary>
		public ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				return this._toolStripPanelRow;
			}
			set
			{
				if ((this._toolStripPanelRow != value))
				{
					this._toolStripPanelRow = value;
				}
			}
		}

		private ToolStripPanelRow _toolStripPanelRow;

		/// <summary>
		/// Returns or sets whether the cell is visible.
		/// </summary>
		public override bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if ((this._visible != value))
				{
					this._visible = value;
				}
			}
		}

		private bool _visible;

		/// <summary>
		/// Returns the maximum size, in pixels, of the cell.
		/// </summary>
		public Size MaximumSize
		{
			get
			{
				return this._maximumSize;
			}
		}

		private Size _maximumSize;

		/// <summary>
		/// Returns the layout engine of the cell.
		/// </summary>
		/// <returns>The <see cref="Wisej.Web.Layout.LayoutEngine" /> for the cell's contents.</returns>
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return this._layoutEngine;
			}
		}

		private LayoutEngine _layoutEngine;

		#endregion

		#region Methods

		/// <summary>
		/// Retrieves the size of a rectangular area into which the cell and its hosted control can be fitted.
		/// </summary>
		/// <param name="constrainingSize">The custom-sized area for the cell.</param>
		/// <returns>A <see cref="System.Drawing.Size"/> representing the preferred width and height, in pixels.</returns>
		/// <example>
		/// Calculating the space needed by a tool strip in a horizontal row:
		/// <code><![CDATA[
		/// var cell = new ToolStripPanelCell(this.toolStrip1);
		/// Size preferred = cell.GetPreferredSize(new Size(400, 25));
		/// ]]></code>
		/// </example>
		public override Size GetPreferredSize(Size constrainingSize)
		{
			// TODO: Implement
			return new System.Drawing.Size();
		}

		/// <summary>
		/// Reduces the size of the cell, along the orientation of its row, by up to the specified number of pixels.
		/// </summary>
		/// <param name="shrinkBy">The number of pixels to remove from the cell.</param>
		/// <returns>The number of pixels the cell was actually reduced by.</returns>
		/// <example>
		/// Making room for 40 more pixels in a row:
		/// <code><![CDATA[
		/// var cell = new ToolStripPanelCell(this.toolStripPanelRow1, this.toolStrip1);
		/// int freed = cell.Shrink(40);
		/// ]]></code>
		/// </example>
		public int Shrink(int shrinkBy)
		{
			// TODO: Implement
			return 0;
		}

		//TODO: (Alaa) Implement ?

		//protected override IArrangedElement GetContainer()
		//{
		//	// TODO: Implement
		//	return new System.Windows.Forms.Layout.IArrangedElement();
		//}

		//protected override ArrangedElementCollection GetChildren()
		//{
		//	// TODO: Implement
		//	return new System.Windows.Forms.Layout.ArrangedElementCollection();
		//}

		//protected override void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
		//{
		//	// TODO: Implement
		//}

		/// <summary>
		/// Increases the size of the cell, along the orientation of its row, by up to the specified number of pixels.
		/// </summary>
		/// <param name="growBy">The number of pixels to add to the cell.</param>
		/// <returns>The number of pixels the cell was actually increased by.</returns>
		/// <example>
		/// Giving the remaining space of a row to a cell:
		/// <code><![CDATA[
		/// var cell = new ToolStripPanelCell(this.toolStripPanelRow1, this.toolStrip1);
		/// int remainingWidth = 120;
		/// remainingWidth -= cell.Grow(remainingWidth);
		/// ]]></code>
		/// </example>
		public int Grow(int growBy)
		{
			// TODO: Implement
			return 0;
		}

		protected override void Dispose(bool disposing)
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
