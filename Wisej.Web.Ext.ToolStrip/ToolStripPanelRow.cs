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
	/// Represents a row of a <see cref="ToolStripPanel" /> that can contain controls.
	///</summary>
	public class ToolStripPanelRow : Component
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripPanelRow" /> class, specifying the containing <see cref="ToolStripPanel" />. 
		///</summary>
		/// <param name="parent">The containing <see cref="ToolStripPanel" />.</param>
		public ToolStripPanelRow(ToolStripPanel parent)
		{
			this._parent = parent;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the size and location of the <see cref="ToolStripPanelRow" />, including its nonclient elements, in pixels, relative to the parent control.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> representing the size and location.</returns>
		public virtual Rectangle Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		private Rectangle _bounds;

		/// <summary>
		/// Returns the controls in the <see cref="ToolStripPanelRow" />.
		/// </summary>
		/// <returns>An array of controls.</returns>
		/// <remarks>
		/// The array is a snapshot of the controls in the row; to add or move a <see cref="ToolStrip"/> into a row use
		/// <see cref="ToolStripPanel.Join(ToolStrip, int)"/>.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlControlsDescr")]
		public Control[] Controls
		{
			get
			{
				return this._controls;
			}
		}

		private Control[] _controls;

		/// <summary>
		/// Returns the space, in pixels, that is specified by default between controls.
		/// </summary>
		/// <returns>A <see cref="Padding" /> that represents the default space between controls.</returns>
		public virtual Padding DefaultMargin
		{
			get
			{
				return this._defaultMargin;
			}
		}

		private Padding _defaultMargin;

		/// <summary>
		/// Returns the internal spacing, in pixels, of the contents of a control.
		/// </summary>
		/// <returns>A <see cref="Padding" /> that represents the internal spacing of the contents of a control.</returns>
		public virtual Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Returns the display area of the row.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> representing the size and location.</returns>
		public Rectangle DisplayRectangle
		{
			get
			{
				return this._displayRectangle;
			}
		}

		private Rectangle _displayRectangle;

		/// <summary>
		/// Returns an instance of the row's layout engine.
		/// </summary>
		/// <returns>The <see cref="Layout.LayoutEngine" /> for the row's contents.</returns>
		public LayoutEngine LayoutEngine
		{
			get
			{
				return this._layoutEngine;
			}
		}

		private LayoutEngine _layoutEngine;

		/// <summary>
		/// Returns or sets the space between controls.
		/// </summary>
		/// <returns>A <see cref="Padding" /> representing the space between controls.</returns>
		public Padding Margin
		{
			get
			{
				return this._margin;
			}
			set
			{
				if ((this._margin != value))
				{
					this._margin = value;
				}
			}
		}

		private Padding _margin;

		/// <summary>
		/// Returns or sets padding within the row.
		/// </summary>
		/// <returns>A <see cref="Padding" /> representing the row's internal spacing characteristics.</returns>
		public virtual Padding Padding
		{
			get
			{
				return this._padding;
			}
			set
			{
				if ((this._padding != value))
				{
					this._padding = value;
				}
			}
		}

		private Padding _padding;

		/// <summary>
		/// Returns the <see cref="ToolStripPanel" /> that contains the <see cref="ToolStripPanelRow" />.
		/// </summary>
		/// <returns>The <see cref="ToolStripPanel" /> that contains the <see cref="ToolStripPanelRow" />.</returns>
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this._parent;
			}
		}

		private ToolStripPanel _parent;

		/// <summary>
		/// Returns the layout direction of the <see cref="ToolStripPanelRow" /> relative to its containing <see cref="ToolStripPanel" />.
		/// </summary>
		/// <returns>One of the <see cref="Orientation" /> values.</returns>
		public Orientation Orientation
		{
			get
			{
				return this._orientation;
			}
		}

		private Orientation _orientation;

		#endregion

		#region Methods

		/// <summary>
		/// Returns a value indicating whether a <see cref="ToolStrip" /> can be dragged and dropped into the <see cref="ToolStripPanelRow" />.
		/// </summary>
		/// <param name="toolStripToDrag">The <see cref="ToolStrip" /> to be dragged and dropped into the <see cref="ToolStripPanelRow" />.</param>
		/// <returns>true if there is enough space in the <see cref="ToolStripPanelRow" /> to receive the <see cref="ToolStrip" />; otherwise, false.</returns>
		/// <example>
		/// Moving a tool strip into the first row only when it fits:
		/// <code><![CDATA[
		/// ToolStripPanelRow firstRow = this.toolStripPanel1.Rows[0];
		/// if (firstRow.CanMove(this.toolStrip2))
		/// {
		///     this.toolStripPanel1.Join(this.toolStrip2, 0);
		/// }
		/// ]]></code>
		/// </example>
		public bool CanMove(ToolStrip toolStripToDrag)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStripPanelRow" /> and optionally releases the managed resources. 
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.Layout" /> event.
		///</summary>
		/// <param name="e">A <see cref="LayoutEventArgs" /> that contains the event data.</param>
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Occurs when the <see cref="ToolStripPanelRow.Bounds" /> property changes.
		///</summary>
		/// <param name="oldBounds">The original value of the <see cref="ToolStripPanelRow.Bounds" /> property.</param>
		/// <param name="newBounds">The new value of the <see cref="ToolStripPanelRow.Bounds" /> property.</param>
		protected void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
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