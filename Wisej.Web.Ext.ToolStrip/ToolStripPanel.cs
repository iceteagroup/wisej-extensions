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
using Wisej.Web.Layout;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Creates a container within which other controls can share horizontal or vertical space.
	///</summary>
	public class ToolStripPanel : Wisej.Web.Panel
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripPanel" /> class. 
		///</summary>
		public ToolStripPanel()
		{
			// TODO: Implement
		}

		internal ToolStripPanel(ToolStripContainer owner)
			: this()
		{
			_owner = owner;
		}
		private readonly ToolStripContainer? _owner;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripPanel.AutoSize" /> property changes. 
		///</summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler AutoSizeChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripPanel.Renderer" /> property changes.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TabIndexChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TabStopChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler TextChanged;

		#endregion

		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ControlAllowDropDescr")]
		public override bool AllowDrop
		{
			get
			{
				return this._allowDrop;
			}
			set
			{
				if ((this._allowDrop != value))
				{
					this._allowDrop = value;
				}
			}
		}

		private bool _allowDrop;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		public override bool AutoScroll
		{
			get
			{
				return this._autoScroll;
			}
			set
			{
				if ((this._autoScroll != value))
				{
					this._autoScroll = value;
				}
			}
		}

		private bool _autoScroll;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size AutoScrollMargin
		{
			get
			{
				return this._autoScrollMargin;
			}
			set
			{
				if ((this._autoScrollMargin != value))
				{
					this._autoScrollMargin = value;
				}
			}
		}

		private Size _autoScrollMargin;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size AutoScrollMinSize
		{
			get
			{
				return this._autoScrollMinSize;
			}
			set
			{
				if ((this._autoScrollMinSize != value))
				{
					this._autoScrollMinSize = value;
				}
			}
		}

		private Size _autoScrollMinSize;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripPanel" /> automatically adjusts its size when the form is resized.
		///</summary>
		/// <returns>true if the <see cref="ToolStripPanel" /> automatically resizes; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlAutoSizeDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoSize
		{
			get
			{
				return this._autoSize;
			}
			set
			{
				if ((this._autoSize != value))
				{
					this._autoSize = value;
				}
			}
		}

		private bool _autoSize;

		public new Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		public new Padding DefaultMargin
		{
			get
			{
				return this._defaultMargin;
			}
		}

		private Padding _defaultMargin;

		/// <summary>
		/// Gets or sets the spacing, in pixels, between the <see cref="ToolStripPanelRow" />s and the <see cref="ToolStripPanel" />.
		///</summary>
		/// <returns>A <see cref="Padding" /> value representing the spacing, in pixels.</returns>
		public Padding RowMargin
		{
			get
			{
				return this._rowMargin;
			}
			set
			{
				if ((this._rowMargin != value))
				{
					this._rowMargin = value;
				}
			}
		}

		private Padding _rowMargin;

		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(DockStyle.None)]
		[SRDescription("ControlDockDescr")]
		public override DockStyle Dock
		{
			get
			{
				return this._dock;
			}
			set
			{
				if ((this._dock != value))
				{
					this._dock = value;
				}
			}
		}

		private DockStyle _dock;

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

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripPanel" /> can be moved or resized.
		///</summary>
		/// <returns>true if the <see cref="ToolStripPanel" /> can be moved or resized; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				if ((this._locked != value))
				{
					this._locked = value;
				}
			}
		}

		private bool _locked;

		/// <summary>
		/// Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="ToolStripPanel" />.
		///</summary>
		/// <returns>One of the <see cref="Orientation" /> values.</returns>
		public Orientation Orientation
		{
			get
			{
				return this._orientation;
			}
			set
			{
				if ((this._orientation != value))
				{
					this._orientation = value;
				}
			}
		}

		private Orientation _orientation;

		/// <summary>
		/// Gets or sets a <see cref="ToolStripRenderer" /> used to customize the appearance of a <see cref="ToolStripPanel" />.
		///</summary>
		/// <returns>A <see cref="ToolStripRenderer" /> that handles painting.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ToolStripRenderer Renderer
		{
			get
			{
				return this._renderer;
			}
			set
			{
				if ((this._renderer != value))
				{
					this._renderer = value;
				}
			}
		}

		private ToolStripRenderer _renderer;

		/// <summary>
		/// Gets or sets the painting styles to be applied to the <see cref="ToolStripPanel" />.
		///</summary>
		/// <returns>One of the <see cref="ToolStripRenderMode" /> values.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRenderModeDescr")]
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this._renderMode;
			}
			set
			{
				if ((this._renderMode != value))
				{
					this._renderMode = value;
				}
			}
		}

		private ToolStripRenderMode _renderMode;

		/// <summary>
		/// Gets the <see cref="ToolStripPanelRow" />s in this <see cref="ToolStripPanel" />.
		///</summary>
		/// <returns>A <see cref="ToolStripPanel.ToolStripPanelRowCollection" /> representing the <see cref="ToolStripPanelRow" />s in this <see cref="ToolStripPanel" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripPanelRowsDescr")]
		public ToolStripPanelRow[] Rows
		{
			get
			{
				return this._rows;
			}
		}

		private ToolStripPanelRow[] _rows;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Int32" /> representing the tab index.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TabIndex
		{
			get
			{
				return this._tabIndex;
			}
			set
			{
				if ((this._tabIndex != value))
				{
					this._tabIndex = value;
				}
			}
		}

		private int _tabIndex;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool TabStop
		{
			get
			{
				return this._tabStop;
			}
			set
			{
				if ((this._tabStop != value))
				{
					this._tabStop = value;
				}
			}
		}

		private bool _tabStop;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.String" /> representing the display text.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlTextDescr")]
		public override string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}

		private string _text;

		#endregion

		#region Methods

		/// <summary>
		/// Adds the specified <see cref="ToolStrip" /> to a <see cref="ToolStripPanel" /> at the specified location.
		///</summary>
		/// <param name="toolStripToDrag">The <see cref="ToolStrip" /> to add to the <see cref="ToolStripPanel" />.</param>
		/// <param name="location">A <see cref="System.Drawing.Point" /> value representing the x- and y-client coordinates, in pixels, of the new location for the <see cref="ToolStrip" />.</param>
		public void Join(ToolStrip toolStripToDrag, Point location)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves a collection of <see cref="ToolStripPanel" /> controls.
		///</summary>
		/// <returns>A collection of <see cref="ToolStripPanel" /> controls.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override ControlCollection CreateControlsInstance()
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStripPanel" /> and optionally releases the managed resources. 
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		//ITG:TODO: Review
		//[EditorBrowsable(EditorBrowsableState.Advanced)]
		//protected override void OnPaintBackground(PaintEventArgs e)
		//{
		//	base.OnPaintBackground(e);
		//	// TODO: Implement
		//}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ControlAdded" /> event.
		///</summary>
		/// <param name="e">A <see cref="ControlEventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ControlRemoved" /> event.
		///</summary>
		/// <param name="e">A <see cref="ControlEventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			// TODO: Implement
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
		/// Raises the <see cref="Control.OnParentChanged(System.EventArgs)" /> event.
		///</summary>
		/// <param name="e">The event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.DockChanged" /> event. 
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Begins the initialization of a <see cref="ToolStripPanel" />.
		///</summary>
		public void BeginInit()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Ends the initialization of a <see cref="ToolStripPanel" />.
		///</summary>
		public void EndInit()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.RightToLeftChanged" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Adds the specified <see cref="ToolStrip" /> to a <see cref="ToolStripPanel" />.
		///</summary>
		/// <param name="toolStripToDrag">The <see cref="ToolStrip" /> to add to the <see cref="ToolStripPanel" />.</param>
		public void Join(ToolStrip toolStripToDrag)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Adds the specified <see cref="ToolStrip" /> to a <see cref="ToolStripPanel" /> in the specified row.
		///</summary>
		/// <exception cref="System.ArgumentOutOfRangeException">The <paramref name="row" /> parameter is less than zero (0).</exception>
		/// <param name="toolStripToDrag">The <see cref="ToolStrip" /> to add to the <see cref="ToolStripPanel" />.</param>
		/// <param name="row">An <see cref="System.Int32" /> representing the <see cref="ToolStripPanelRow" /> to which the <see cref="ToolStrip" /> is added.</param>
		public void Join(ToolStrip toolStripToDrag, int row)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Adds the specified <see cref="ToolStrip" /> to a <see cref="ToolStripPanel" /> at the specified coordinates.
		///</summary>
		/// <param name="toolStripToDrag">The <see cref="ToolStrip" /> to add to the <see cref="ToolStripPanel" />.</param>
		/// <param name="x">The horizontal client coordinate, in pixels.</param>
		/// <param name="y">The vertical client coordinate, in pixels.</param>
		public void Join(ToolStrip toolStripToDrag, int x, int y)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the <see cref="ToolStripPanelRow" /> given a point within the <see cref="ToolStripPanel" /> client area.
		///</summary>
		/// <returns>The <see cref="ToolStripPanelRow" /> that contains the <paramref name="raftingContainerPoint" />, or null if no such <see cref="ToolStripPanelRow" /> exists.</returns>
		/// <param name="clientLocation">A <see cref="System.Drawing.Point" /> used as a reference to find the <see cref="ToolStripPanelRow" />.</param>
		public ToolStripPanelRow PointToRow(Point clientLocation)
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		/// <summary>
		/// Raises the <see cref="Control.AutoSizeChanged" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnAutoSizeChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.AutoSizeChanged != null))
			{
				AutoSizeChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripPanel.RendererChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		protected virtual void OnRendererChanged(System.EventArgs e)
		{
			if ((this.RendererChanged != null))
			{
				RendererChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.TabIndexChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnTabIndexChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TabIndexChanged != null))
			{
				TabIndexChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.TabStopChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnTabStopChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TabStopChanged != null))
			{
				TabStopChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.TextChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		protected virtual void OnTextChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TextChanged != null))
			{
				TextChanged(this, e);
			}
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