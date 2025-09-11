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
	/// Provides panels on each side of the form and a central panel that can hold one or more controls.
	///</summary>
	public partial class ToolStripContainer : ContainerControl
	{
		private readonly ToolStripPanel _topPanel;
		private readonly ToolStripPanel _bottomPanel;
		private readonly ToolStripPanel _leftPanel;
		private readonly ToolStripPanel _rightPanel;
		private readonly ToolStripContentPanel _contentPanel;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripContainer" /> class. 
		///</summary>
		public ToolStripContainer()
		{
            SetStyle(ControlStyles.ContainerControl, true);
			
			SuspendLayout();

			try
			{
				// undone - smart demand creation
				_topPanel = new ToolStripPanel(this);
				_bottomPanel = new ToolStripPanel(this);
				_leftPanel = new ToolStripPanel(this);
				_rightPanel = new ToolStripPanel(this);
				_contentPanel = new ToolStripContentPanel
				{
					Dock = DockStyle.Fill
				};
				_topPanel.Dock = DockStyle.Top;
				_bottomPanel.Dock = DockStyle.Bottom;
				_rightPanel.Dock = DockStyle.Right;
				_leftPanel.Dock = DockStyle.Left;

				if (Controls is ToolStripContainerTypedControlCollection controlCollection)
				{
					controlCollection.AddInternal(_contentPanel);
					controlCollection.AddInternal(_leftPanel);
					controlCollection.AddInternal(_rightPanel);
					controlCollection.AddInternal(_topPanel);
					controlCollection.AddInternal(_bottomPanel);
				}

				// else consider throw new exception
			}
			finally
			{
				ResumeLayout(true);
			}
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler BackColorChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler BackgroundImageChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler BackgroundImageLayoutChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripContainer.CausesValidation" /> property changes.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler CausesValidationChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripContainer.ContextMenuStrip" /> property changes.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler ContextMenuStripChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler CursorChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler ForeColorChanged;
		#endregion

		#region Properties

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>true to enable automatic scrolling; otherwise, false. </returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" /> value.</returns>
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
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color BackColor
		{
			get
			{
				return this._backColor;
			}
			set
			{
				if ((this._backColor != value))
				{
					this._backColor = value;
				}
			}
		}

		private Color _backColor;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image BackgroundImage
		{
			get
			{
				return this._backgroundImage;
			}
			set
			{
				if ((this._backgroundImage != value))
				{
					this._backgroundImage = value;
				}
			}
		}

		private Image _backgroundImage;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>An <see cref="ImageLayout" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return this._backgroundImageLayout;
			}
			set
			{
				if ((this._backgroundImageLayout != value))
				{
					this._backgroundImageLayout = value;
				}
			}
		}

		private ImageLayout _backgroundImageLayout;

		/// <summary>
		/// Gets the bottom panel of the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>A <see cref="ToolStripPanel" /> representing the bottom panel of the <see cref="ToolStripContainer" />.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel BottomToolStripPanel
		{
			get
			{
				return this._bottomToolStripPanel;
			}
		}

		private ToolStripPanel _bottomToolStripPanel;

		/// <summary>
		/// Gets or sets a value indicating whether the bottom panel of the <see cref="ToolStripContainer" /> is visible. 
		///</summary>
		/// <returns>true if the bottom panel of the <see cref="ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool BottomToolStripPanelVisible
		{
			get
			{
				return this._bottomToolStripPanelVisible;
			}
			set
			{
				if ((this._bottomToolStripPanelVisible != value))
				{
					this._bottomToolStripPanelVisible = value;
				}
			}
		}

		private bool _bottomToolStripPanelVisible;

		/// <summary>
		/// Gets the center panel of the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>A <see cref="ToolStripContentPanel" /> representing the center panel of the <see cref="ToolStripContainer" />.</returns>
		[SRDescription("ToolStripContainerContentPanelDescr")]
		[SRCategory("CatAppearance")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripContentPanel ContentPanel
		{
			get
			{
				return this._contentPanel;
			}
		}

		private ToolStripContentPanel _contentPanel;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>true if the control causes validation; otherwise, false. </returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CausesValidation
		{
			get
			{
				return this._causesValidation;
			}
			set
			{
				if ((this._causesValidation != value))
				{
					this._causesValidation = value;
				}
			}
		}

		private bool _causesValidation;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="ContextMenuStrip" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContextMenu ContextMenuStrip
		{
			get
			{
				return this._contextMenuStrip;
			}
			set
			{
				if ((this._contextMenuStrip != value))
				{
					this._contextMenuStrip = value;
				}
			}
		}

		private ContextMenu _contextMenuStrip;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="Cursor" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlCursorDescr")]
		public override Cursor Cursor
		{
			get
			{
				return this._cursor;
			}
			set
			{
				if ((this._cursor != value))
				{
					this._cursor = value;
				}
			}
		}

		private Cursor _cursor;

		/// <summary>
		/// Gets the default size of the <see cref="ToolStripContainer" />, in pixels.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" /> representing the horizontal and vertical dimensions of the <see cref="ToolStripContainer" />, in pixels.</returns>
		public new Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ForeColor
		{
			get
			{
				return this._foreColor;
			}
			set
			{
				if ((this._foreColor != value))
				{
					this._foreColor = value;
				}
			}
		}

		private Color _foreColor;

		/// <summary>
		/// Gets the left panel of the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>A <see cref="ToolStripPanel" /> representing the left panel of the <see cref="ToolStripContainer" />.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel LeftToolStripPanel
		{
			get
			{
				return this._leftToolStripPanel;
			}
		}

		private ToolStripPanel _leftToolStripPanel;

		/// <summary>
		/// Gets or sets a value indicating whether the left panel of the <see cref="ToolStripContainer" /> is visible.
		///</summary>
		/// <returns>true if the left panel of the <see cref="ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool LeftToolStripPanelVisible
		{
			get
			{
				return this._leftToolStripPanelVisible;
			}
			set
			{
				if ((this._leftToolStripPanelVisible != value))
				{
					this._leftToolStripPanelVisible = value;
				}
			}
		}

		private bool _leftToolStripPanelVisible;

		/// <summary>
		/// Gets the right panel of the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>A <see cref="ToolStripPanel" /> representing the right panel of the <see cref="ToolStripContainer" />.</returns>
		[SRDescription("ToolStripContainerRightToolStripPanelDescr")]
		[SRCategory("CatAppearance")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel RightToolStripPanel
		{
			get
			{
				return this._rightToolStripPanel;
			}
		}

		private ToolStripPanel _rightToolStripPanel;

		/// <summary>
		/// Gets or sets a value indicating whether the right panel of the <see cref="ToolStripContainer" /> is visible.
		///</summary>
		/// <returns>true if the right panel of the <see cref="ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		[SRDescription("ToolStripContainerRightToolStripPanelVisibleDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		public bool RightToolStripPanelVisible
		{
			get
			{
				return this._rightToolStripPanelVisible;
			}
			set
			{
				if ((this._rightToolStripPanelVisible != value))
				{
					this._rightToolStripPanelVisible = value;
				}
			}
		}

		private bool _rightToolStripPanelVisible;

		/// <summary>
		/// Gets the top panel of the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>A <see cref="ToolStripPanel" /> representing the top panel of the <see cref="ToolStripContainer" />.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerTopToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel TopToolStripPanel
		{
			get
			{
				return this._topToolStripPanel;
			}
		}

		private ToolStripPanel _topToolStripPanel;

		/// <summary>
		/// Gets or sets a value indicating whether the top panel of the <see cref="ToolStripContainer" /> is visible.
		///</summary>
		/// <returns>true if the top panel of the <see cref="ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[SRDescription("ToolStripContainerTopToolStripPanelVisibleDescr")]
		[SRCategory("CatAppearance")]
		public bool TopToolStripPanelVisible
		{
			get
			{
				return this._topToolStripPanelVisible;
			}
			set
			{
				if ((this._topToolStripPanelVisible != value))
				{
					this._topToolStripPanelVisible = value;
				}
			}
		}

		private bool _topToolStripPanelVisible;

		/// <summary>
		/// This property is not relevant for this class.
		///</summary>
		/// <returns>A <see cref="Control.ControlCollection" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlCollection Controls
		{
			get
			{
				return this._controls;
			}
		}

		private ControlCollection _controls;

		#endregion

		#region Methods

		/// <summary>
		/// Creates and returns a <see cref="ToolStripContainer" /> collection.
		///</summary>
		/// <returns>A read-only <see cref="ToolStripContainer" /> collection.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual ControlCollection CreateControlsInstance()
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		/// <summary>
		/// Raises the <see cref="Control.RightToLeftChanged" /> event.
		///</summary>
		/// <param name="e">The event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			throw new NotImplementedException();
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.SizeChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSizeChanged(EventArgs e)
		{
			throw new NotImplementedException();
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.BackColorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual void OnBackColorChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.BackColorChanged != null))
			{
				BackColorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.BackgroundImageChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual void OnBackgroundImageChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.BackgroundImageChanged != null))
			{
				BackgroundImageChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.BackgroundImageLayoutChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual void OnBackgroundImageLayoutChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.BackgroundImageLayoutChanged != null))
			{
				BackgroundImageLayoutChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.CausesValidationChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		protected virtual void OnCausesValidationChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.CausesValidationChanged != null))
			{
				CausesValidationChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ContextMenuStripChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		protected virtual void OnContextMenuStripChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ContextMenuStripChanged != null))
			{
				ContextMenuStripChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.CursorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual void OnCursorChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.CursorChanged != null))
			{
				CursorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ForeColorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual void OnForeColorChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ForeColorChanged != null))
			{
				ForeColorChanged(this, e);
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
