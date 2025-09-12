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
	public class ToolStripPanelCell : Control
	{

		#region Constructors

		public ToolStripPanelCell(Control control)
		{
			this._control = control;
			// TODO: Implement
		}

		public ToolStripPanelCell(ToolStripPanelRow parent, Control control)
		{
			this._parent = parent;
			this._control = control;
			// TODO: Implement
		}

		#endregion

		#region Properties

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

		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		private ToolStripPanelRow _parent;
		private Control _control;

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

		public ISupportToolStripPanel DraggedControl
		{
			get
			{
				return this._draggedControl;
			}
		}

		private ISupportToolStripPanel _draggedControl;

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

		public Size MaximumSize
		{
			get
			{
				return this._maximumSize;
			}
		}

		private Size _maximumSize;

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

		public override Size GetPreferredSize(Size constrainingSize)
		{
			// TODO: Implement
			return new System.Drawing.Size();
		}

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
