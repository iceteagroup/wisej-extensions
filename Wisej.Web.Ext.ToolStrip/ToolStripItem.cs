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
	/// Represents the abstract base class that manages events and layout for all the elements that a <see cref="ToolStrip" /> or <see cref="ToolStripDropDown" /> can contain.
	///</summary>
	public class ToolStripItem : Wisej.Web.Component
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class.
		///</summary>
		public ToolStripItem()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class with the specified name, image, and event handler.
		///</summary>
		/// <param name="text">A <see cref="System.String" /> representing the name of the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="ToolStripItem.Click" /> event when the user clicks the <see cref="ToolStripItem" />.</param>
		public ToolStripItem(string text, Image image, EventHandler onClick)
		{
			//this._text = text;
			//this._image = image;
			//this._onClick = onClick;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class with the specified display text, image, event handler, and name. 
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The Image to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="onClick">The event handler for the <see cref="ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="ToolStripItem" />.</param>
		public ToolStripItem(string text, Image image, EventHandler onClick, string name)
		{
			//this._text = text;
			//this._image = image;
			//this._onClick = onClick;
			//this._name = name;
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripItem.Available" /> property changes.
		///</summary>
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnAvailableChangedDescr")]
		public event EventHandler AvailableChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripItem.BackColor" /> property changes.
		///</summary>
		[SRDescription("ToolStripItemOnBackColorChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler BackColorChanged;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem" /> is clicked.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		public event EventHandler Click;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem.DisplayStyle" /> has changed.
		///</summary>
		public event EventHandler DisplayStyleChanged;

		/// <summary>
		/// Occurs when the item is double-clicked with the mouse.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick;

		/// <summary>
		/// Occurs when the user drags an item and the user releases the mouse button, indicating that the item should be dropped into this item.
		///</summary>
		[SRDescription("ToolStripItemOnDragDropDescr")]
		[SRCategory("CatDragDrop")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragDrop;

		/// <summary>
		/// Occurs when the user drags an item into the client area of this item.
		///</summary>
		[SRDescription("ToolStripItemOnDragEnterDescr")]
		[SRCategory("CatDragDrop")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragEnter;

		/// <summary>
		/// Occurs when the user drags an item over the client area of this item.
		///</summary>
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragOverDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragOver;

		/// <summary>
		/// Occurs when the user drags an item and the mouse pointer is no longer over the client area of this item.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ToolStripItemOnDragLeaveDescr")]
		[SRCategory("CatDragDrop")]
		[Browsable(false)]
		public event EventHandler DragLeave;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem.Enabled" /> property value has changed.
		///</summary>
		[SRDescription("ToolStripItemEnabledChangedDescr")]
		public event EventHandler EnabledChanged;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem.ForeColor" /> property value changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnForeColorChangedDescr")]
		public event EventHandler ForeColorChanged;

		/// <summary>
		/// Occurs when the location of a <see cref="ToolStripItem" /> is updated.
		///</summary>
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemOnLocationChangedDescr")]
		public event EventHandler LocationChanged;

		/// <summary>
		/// Occurs when the mouse pointer is over the item and a mouse button is pressed.
		///</summary>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseDownDescr")]
		public event MouseEventHandler MouseDown;

		/// <summary>
		/// Occurs when the mouse pointer enters the item.
		///</summary>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseEnterDescr")]
		public event EventHandler MouseEnter;

		/// <summary>
		/// Occurs when the mouse pointer leaves the item.
		///</summary>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseLeaveDescr")]
		public event EventHandler MouseLeave;

		/// <summary>
		/// Occurs when the mouse pointer hovers over the item.
		///</summary>
		[SRDescription("ToolStripItemOnMouseHoverDescr")]
		[SRCategory("CatMouse")]
		public event EventHandler MouseHover;

		/// <summary>
		/// Occurs when the mouse pointer is moved over the item.
		///</summary>
		[SRDescription("ToolStripItemOnMouseMoveDescr")]
		[SRCategory("CatMouse")]
		public event MouseEventHandler MouseMove;

		/// <summary>
		/// Occurs when the mouse pointer is over the item and a mouse button is released.
		///</summary>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseUpDescr")]
		public event MouseEventHandler MouseUp;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem.Owner" /> property changes. 
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOwnerChangedDescr")]
		public event EventHandler OwnerChanged;

		/// <summary>
		/// Occurs when the item is redrawn.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemOnPaintDescr")]
		public event PaintEventHandler Paint;

		/// <summary>
		/// Occurs during a drag-and-drop operation and allows the drag source to determine whether the drag-and-drop operation should be canceled.
		///</summary>
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnQueryContinueDragDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event QueryContinueDragEventHandler QueryContinueDrag;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem.RightToLeft" /> property value changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripItem.Text" /> property changes.
		///</summary>
		[SRDescription("ToolStripItemOnTextChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler TextChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripItem.Visible" /> property changes.
		///</summary>
		[SRDescription("ToolStripItemOnVisibleChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler VisibleChanged;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="ToolStrip" />.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="ToolStripItemAlignment" /> values. </exception>
		/// <returns>One of the <see cref="ToolStripItemAlignment" /> values. The default is <see cref="Wisej.Web.Ext.ToolStripItemAlignment.Left" />.</returns>
		[DefaultValue(ToolStripItemAlignment.Left)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemAlignmentDescr")]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this._alignment;
			}
			set
			{
				if ((this._alignment != value))
				{
					this._alignment = value;
				}
			}
		}
		private ToolStripItemAlignment _alignment;

		/// <summary>
		/// Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.
		///</summary>
		/// <exception cref="System.ArgumentException"><see cref="ToolStripItem.AllowDrop" /> and <see cref="ToolStrip.AllowItemReorder" /> are both set to true. </exception>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemAllowDropDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public virtual bool AllowDrop
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
		/// Gets or sets a value indicating whether the item is automatically sized.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is automatically sized; otherwise, false. The default value is true.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAutoSizeDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoSize
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

		/// <summary>
		/// Gets or sets a value indicating whether to use the <see cref="ToolStripItem.Text" /> property or the <see cref="ToolStripItem.ToolTipText" /> property for the <see cref="ToolStripItem" /> ToolTip. 
		///</summary>
		/// <returns>true to use the <see cref="ToolStripItem.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
		[DefaultValue(false)]
		[SRDescription("ToolStripItemAutoToolTipDescr")]
		[SRCategory("CatBehavior")]
		public bool AutoToolTip
		{
			get
			{
				return this._autoToolTip;
			}
			set
			{
				if ((this._autoToolTip != value))
				{
					this._autoToolTip = value;
				}
			}
		}
		private bool _autoToolTip;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripItem" /> should be placed on a <see cref="ToolStrip" />.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is placed on a <see cref="ToolStrip" />; otherwise, false.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemAvailableDescr")]
		[Browsable(false)]
		public bool Available
		{
			get
			{
				return this._available;
			}
			set
			{
				if ((this._available != value))
				{
					this._available = value;
				}
			}
		}

		private bool _available;

		/// <summary>
		/// Gets or sets the background image displayed in the item.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" /> that represents the image to display in the background of the item.</returns>
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[Localizable(true)]
		public virtual Image BackgroundImage
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
		/// Gets or sets the background image layout used for the <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>One of the <see cref="ImageLayout" /> values. The default value is <see cref="Wisej.Web.Ext.ImageLayout.Tile" />.</returns>
		[DefaultValue(ImageLayout.Tile)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public virtual ImageLayout BackgroundImageLayout
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
		/// Gets or sets the background color for the item.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="Control.DefaultBackColor" /> property.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemBackColorDescr")]
		public virtual Color BackColor
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
		/// Gets the size and location of the item.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the <see cref="ToolStripItem" />.</returns>
		[Browsable(false)]
		public virtual Rectangle Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		private Rectangle _bounds;

		/// <summary>
		/// Gets the area where content, such as text and icons, can be placed within a <see cref="ToolStripItem" /> without overwriting background borders.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> containing four integers that represent the location and size of <see cref="ToolStripItem" /> contents, excluding its border.</returns>
		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				return this._contentRectangle;
			}
		}

		private Rectangle _contentRectangle;

		/// <summary>
		/// Gets a value indicating whether the item can be selected.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> can be selected; otherwise, false.</returns>
		[Browsable(false)]
		public virtual bool CanSelect
		{
			get
			{
				return this._canSelect;
			}
		}

		private bool _canSelect;

		/// <summary>
		/// Gets or sets the edges of the container to which a <see cref="ToolStripItem" /> is bound and determines how a <see cref="ToolStripItem" />  is resized with its parent.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="AnchorStyles" /> values.</exception>
		/// <returns>One of the <see cref="AnchorStyles" /> values.</returns>
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AnchorStyles Anchor
		{
			get
			{
				return this._anchor;
			}
			set
			{
				if ((this._anchor != value))
				{
					this._anchor = value;
				}
			}
		}

		private AnchorStyles _anchor;

		/// <summary>
		/// Gets or sets which <see cref="ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="ToolStripItem" /> is resized with its parent.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="DockStyle" /> values.</exception>
		/// <returns>One of the <see cref="DockStyle" /> values. The default is <see cref="Wisej.Web.Ext.DockStyle.None" />.</returns>
		[DefaultValue(DockStyle.None)]
		[Browsable(false)]
		public DockStyle Dock
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

		/// <summary>
		/// Gets a value indicating whether to display the <see cref="ToolTip" /> that is defined as the default.
		///</summary>
		/// <returns>false in all cases.</returns>
		public virtual bool DefaultAutoToolTip
		{
			get
			{
				return this._defaultAutoToolTip;
			}
		}

		private bool _defaultAutoToolTip;

		/// <summary>
		/// Gets the internal spacing characteristics of the item.
		///</summary>
		/// <returns>One of the <see cref="Padding" /> values.</returns>
		public virtual Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Gets the default size of the item.
		///</summary>
		/// <returns>The default <see cref="System.Drawing.Size" /> of the <see cref="ToolStripItem" />.</returns>
		public virtual Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// Gets a value indicating what is displayed on the <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemDisplayStyle" /> values. The default is <see cref="Wisej.Web.Ext.ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		public virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return this._defaultDisplayStyle;
			}
		}

		private ToolStripItemDisplayStyle _defaultDisplayStyle;

		/// <summary>
		/// Gets or sets whether text and images are displayed on a <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemDisplayStyle" /> values. The default is <see cref="Wisej.Web.Ext.ToolStripItemDisplayStyle.ImageAndText" /> .</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemDisplayStyleDescr")]
		public virtual ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return this._displayStyle;
			}
			set
			{
				if ((this._displayStyle != value))
				{
					this._displayStyle = value;
				}
			}
		}

		private ToolStripItemDisplayStyle _displayStyle;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripItem" /> can be activated by double-clicking the mouse. 
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> can be activated by double-clicking the mouse; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemDoubleClickedEnabledDescr")]
		public bool DoubleClickEnabled
		{
			get
			{
				return this._doubleClickEnabled;
			}
			set
			{
				if ((this._doubleClickEnabled != value))
				{
					this._doubleClickEnabled = value;
				}
			}
		}

		private bool _doubleClickEnabled;

		/// <summary>
		/// Gets or sets a value indicating whether the parent control of the <see cref="ToolStripItem" /> is enabled. 
		///</summary>
		/// <returns>true if the parent control of the <see cref="ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolStripItemEnabledDescr")]
		[SRCategory("CatBehavior")]
		public virtual bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((this._enabled != value))
				{
					this._enabled = value;
				}
			}
		}

		private bool _enabled;

		/// <summary>
		/// Gets or sets the foreground color of the item.
		///</summary>
		/// <returns>The foreground <see cref="System.Drawing.Color" /> of the item. The default is the value of the <see cref="Control.DefaultForeColor" /> property.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemForeColorDescr")]
		public virtual Color ForeColor
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
		/// Gets or sets the font of the text displayed by the item.
		///</summary>
		/// <returns>The <see cref="System.Drawing.Font" /> to apply to the text displayed by the <see cref="ToolStripItem" />. The default is the value of the <see cref="Control.DefaultFont" /> property.</returns>
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemFontDescr")]
		public virtual Font Font
		{
			get
			{
				return this._font;
			}
			set
			{
				if ((this._font != value))
				{
					this._font = value;
				}
			}
		}

		private Font _font;

		/// <summary>
		/// Gets or sets the height, in pixels, of a <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>An <see cref="System.Int32" /> representing the height, in pixels.</returns>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if ((this._height != value))
				{
					this._height = value;
				}
			}
		}

		private int _height;

		/// <summary>
		/// Gets or sets the alignment of the image on a <see cref="ToolStripItem" />.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="System.Drawing.ContentAlignment" /> values. </exception>
		/// <returns>One of the <see cref="System.Drawing.ContentAlignment" /> values. The default is <see cref="System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageAlignDescr")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this._imageAlign;
			}
			set
			{
				if ((this._imageAlign != value))
				{
					this._imageAlign = value;
				}
			}
		}

		private ContentAlignment _imageAlign;

		/// <summary>
		/// Gets or sets the image that is displayed on a <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>The <see cref="System.Drawing.Image" /> to be displayed.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		public virtual Image Image
		{
			get
			{
				return this._image;
			}
			set
			{
				if ((this._image != value))
				{
					this._image = value;
				}
			}
		}

		private Image _image;

		/// <summary>
		/// Gets or sets the color to treat as transparent in a <see cref="ToolStripItem" /> image.
		///</summary>
		/// <returns>One of the <see cref="System.Drawing.Color" /> values.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageTransparentColorDescr")]
		public Color ImageTransparentColor
		{
			get
			{
				return this._imageTransparentColor;
			}
			set
			{
				if ((this._imageTransparentColor != value))
				{
					this._imageTransparentColor = value;
				}
			}
		}

		private Color _imageTransparentColor;

		/// <summary>
		/// Gets or sets the index value of the image that is displayed on the item.
		///</summary>
		/// <exception cref="System.ArgumentException">The value specified is less than -1. </exception>
		/// <returns>The zero-based index of the image in the <see cref="ToolStrip.ImageList" /> that is displayed for the item. The default is -1, signifying that the image list is empty.</returns>
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ToolStripItemImageIndexDescr")]
		[Browsable(false)]
		public int ImageIndex
		{
			get
			{
				return this._imageIndex;
			}
			set
			{
				if ((this._imageIndex != value))
				{
					this._imageIndex = value;
				}
			}
		}

		private int _imageIndex;

		/// <summary>
		/// Gets or sets the key accessor for the image in the <see cref="ToolStrip.ImageList" /> that is displayed on a <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>A string representing the key of the image.</returns>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemImageKeyDescr")]
		[Browsable(false)]
		public string ImageKey
		{
			get
			{
				return this._imageKey;
			}
			set
			{
				if ((this._imageKey != value))
				{
					this._imageKey = value;
				}
			}
		}

		private string _imageKey;

		/// <summary>
		/// Gets or sets a value indicating whether an image on a <see cref="ToolStripItem" /> is automatically resized to fit in a container.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemImageScaling" /> values. The default is <see cref="Wisej.Web.Ext.ToolStripItemImageScaling.SizeToFit" />.</returns>
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolStripItemImageScaling.SizeToFit)]
		[Localizable(true)]
		[SRDescription("ToolStripItemImageScalingDescr")]
		public ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return this._imageScaling;
			}
			set
			{
				if ((this._imageScaling != value))
				{
					this._imageScaling = value;
				}
			}
		}

		private ToolStripItemImageScaling _imageScaling;

		/// <summary>
		/// Gets a value indicating whether the object has been disposed of.
		///</summary>
		/// <returns>true if the control has been disposed of; otherwise, false.</returns>
		[Browsable(false)]
		public bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		private bool _isDisposed;

		/// <summary>
		/// Gets a value indicating whether the container of the current <see cref="Control" /> is a <see cref="ToolStripDropDown" />. 
		///</summary>
		/// <returns>true if the container of the current <see cref="Control" /> is a <see cref="ToolStripDropDown" />; otherwise, false.</returns>
		[Browsable(false)]
		public bool IsOnDropDown
		{
			get
			{
				return this._isOnDropDown;
			}
		}

		private bool _isOnDropDown;

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripItem.Placement" /> property is set to <see cref="Wisej.Web.Ext.ToolStripItemPlacement.Overflow" />.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem.Placement" /> property is set to <see cref="Wisej.Web.Ext.ToolStripItemPlacement.Overflow" />; otherwise, false.</returns>
		[Browsable(false)]
		public bool IsOnOverflow
		{
			get
			{
				return this._isOnOverflow;
			}
		}

		private bool _isOnOverflow;

		/// <summary>
		/// Gets or sets how child menus are merged with parent menus. 
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="MergeAction" /> values.</exception>
		/// <returns>One of the <see cref="MergeAction" /> values. The default is <see cref="Wisej.Web.Ext.MergeAction.MatchOnly" />.</returns>
		[SRDescription("ToolStripMergeActionDescr")]
		[DefaultValue(MergeAction.Append)]
		[SRCategory("CatLayout")]
		public MergeAction MergeAction
		{
			get
			{
				return this._mergeAction;
			}
			set
			{
				if ((this._mergeAction != value))
				{
					this._mergeAction = value;
				}
			}
		}
		private MergeAction _mergeAction;

		/// <summary>
		/// Gets or sets the position of a merged item within the current <see cref="ToolStrip" />.
		///</summary>
		/// <returns>An integer representing the index of the merged item, if a match is found, or -1 if a match is not found.</returns>
		[SRDescription("ToolStripMergeIndexDescr")]
		[DefaultValue(-1)]
		[SRCategory("CatLayout")]
		public int MergeIndex
		{
			get
			{
				return this._mergeIndex;
			}
			set
			{
				if ((this._mergeIndex != value))
				{
					this._mergeIndex = value;
				}
			}
		}

		private int _mergeIndex;

		/// <summary>
		/// Gets or sets the name of the item.
		///</summary>
		/// <returns>A string representing the name. The default value is null.</returns>
		[Browsable(false)]
		[DefaultValue(null)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		private string _name;

		/// <summary>
		/// Gets or sets the owner of this item.
		///</summary>
		/// <returns>The <see cref="ToolStrip" /> that owns or is to own the <see cref="ToolStripItem" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStrip Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				if ((this._owner != value))
				{
					this._owner = value;
				}
			}
		}

		private ToolStrip _owner;

		/// <summary>
		/// Gets the parent <see cref="ToolStripItem" /> of this <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>The parent <see cref="ToolStripItem" /> of this <see cref="ToolStripItem" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this._ownerItem;
			}
		}

		private ToolStripItem _ownerItem;

		/// <summary>
		/// Gets or sets whether the item is attached to the <see cref="ToolStrip" /> or <see cref="ToolStripOverflowButton" /> or can float between the two.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="ToolStripItemOverflow" /> values. </exception>
		/// <returns>One of the <see cref="ToolStripItemOverflow" /> values. The default is <see cref="Wisej.Web.Ext.ToolStripItemOverflow.AsNeeded" />.</returns>
		[SRDescription("ToolStripItemOverflowDescr")]
		[DefaultValue(ToolStripItemOverflow.AsNeeded)]
		[SRCategory("CatLayout")]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this._overflow;
			}
			set
			{
				if ((this._overflow != value))
				{
					this._overflow = value;
				}
			}
		}

		private ToolStripItemOverflow _overflow;

		/// <summary>
		/// Gets or sets the internal spacing, in pixels, between the item's contents and its edges.
		///</summary>
		/// <returns>A <see cref="Padding" /> representing the item's internal spacing, in pixels.</returns>
		[SRDescription("ToolStripItemPaddingDescr")]
		[SRCategory("CatLayout")]
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
		/// Gets the current layout of the item.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemPlacement" /> values.</returns>
		[Browsable(false)]
		public ToolStripItemPlacement Placement
		{
			get
			{
				return this._placement;
			}
		}

		private ToolStripItemPlacement _placement;

		/// <summary>
		/// Gets a value indicating whether the state of the item is pressed. 
		///</summary>
		/// <returns>true if the state of the item is pressed; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Pressed
		{
			get
			{
				return this._pressed;
			}
		}

		private bool _pressed;

		/// <summary>
		/// Gets or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.
		///</summary>
		/// <returns>true if items are to be placed from right to left and text is to be written from right to left; otherwise, false.</returns>
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				return this._rightToLeft;
			}
			set
			{
				if ((this._rightToLeft != value))
				{
					this._rightToLeft = value;
				}
			}
		}

		private RightToLeft _rightToLeft;

		/// <summary>
		/// Mirrors automatically the <see cref="ToolStripItem" /> image when the <see cref="ToolStripItem.RightToLeft" /> property is set to <see cref="Wisej.Web.Ext.RightToLeft.Yes" />.
		///</summary>
		/// <returns>true to automatically mirror the image; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftAutoMirrorImageDescr")]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this._rightToLeftAutoMirrorImage;
			}
			set
			{
				if ((this._rightToLeftAutoMirrorImage != value))
				{
					this._rightToLeftAutoMirrorImage = value;
				}
			}
		}

		private bool _rightToLeftAutoMirrorImage;

		/// <summary>
		/// Gets a value indicating whether the item is selected.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is selected; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return this._selected;
			}
		}

		private bool _selected;

		/// <summary>
		/// Gets or sets the size of the item.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemSizeDescr")]
		public virtual Size Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if ((this._size != value))
				{
					this._size = value;
				}
			}
		}

		private Size _size;

		/// <summary>
		/// Gets or sets the object that contains data about the item.
		///</summary>
		/// <returns>An <see cref="System.object" /> that contains data about the control. The default is null.</returns>
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Localizable(false)]
		[SRDescription("ToolStripItemTagDescr")]
		public object Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				if ((this._tag != value))
				{
					this._tag = value;
				}
			}
		}

		private object _tag;

		/// <summary>
		/// Gets or sets the text that is to be displayed on the item.
		///</summary>
		/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		[SRDescription("ToolStripItemTextDescr")]
		public virtual string Text
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

		/// <summary>
		/// Gets or sets the alignment of the text on a <see cref="ToolStripLabel" />.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="System.Drawing.ContentAlignment" /> values. </exception>
		/// <returns>One of the <see cref="System.Drawing.ContentAlignment" /> values. The default is <see cref="System.Drawing.ContentAlignment.MiddleRight" />.</returns>
		[SRDescription("ToolStripItemTextAlignDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this._textAlign;
			}
			set
			{
				if ((this._textAlign != value))
				{
					this._textAlign = value;
				}
			}
		}

		private ContentAlignment _textAlign;

		/// <summary>
		/// Gets the orientation of text used on a <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>One of the <see cref="ToolStripTextDirection" /> values.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripTextDirectionDescr")]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				return this._textDirection;
			}
			set
			{
				if ((this._textDirection != value))
				{
					this._textDirection = value;
				}
			}
		}

		private ToolStripTextDirection _textDirection;

		/// <summary>
		/// Gets or sets the position of <see cref="ToolStripItem" /> text and image relative to each other.
		///</summary>
		/// <returns>One of the <see cref="TextImageRelation" /> values. The default is <see cref="Wisej.Web.Ext.TextImageRelation.ImageBeforeText" />.</returns>
		[Localizable(true)]
		[DefaultValue(TextImageRelation.ImageBeforeText)]
		[SRDescription("ToolStripItemTextImageRelationDescr")]
		[SRCategory("CatAppearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this._textImageRelation;
			}
			set
			{
				if ((this._textImageRelation != value))
				{
					this._textImageRelation = value;
				}
			}
		}

		private TextImageRelation _textImageRelation;

		#endregion

		#region Methods

		/// <summary>
		/// Raises the AvailableChanged event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnAvailableChangedDescr")]
		protected virtual void OnAvailableChanged(System.EventArgs e)
		{
			if ((this.AvailableChanged != null))
			{
				AvailableChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.BackColorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnBackColorChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		protected virtual void OnBackColorChanged(System.EventArgs e)
		{
			if ((this.BackColorChanged != null))
			{
				BackColorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.Click" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		protected virtual void OnClick(System.EventArgs e)
		{
			if ((this.Click != null))
			{
				Click(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DisplayStyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		protected virtual void OnDisplayStyleChanged(System.EventArgs e)
		{
			if ((this.DisplayStyleChanged != null))
			{
				DisplayStyleChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DoubleClick" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		protected virtual void OnDoubleClick(System.EventArgs e)
		{
			if ((this.DoubleClick != null))
			{
				DoubleClick(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DragDrop" /> event.
		///</summary>
		/// <param name="dragEvent">A <see cref="DragEventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnDragDropDescr")]
		[SRCategory("CatDragDrop")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		protected virtual void OnDragDrop(Wisej.Web.DragEventArgs e)
		{
			if ((this.DragDrop != null))
			{
				DragDrop(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DragEnter" /> event.
		///</summary>
		/// <param name="dragEvent">A <see cref="DragEventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnDragEnterDescr")]
		[SRCategory("CatDragDrop")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		protected virtual void OnDragEnter(Wisej.Web.DragEventArgs e)
		{
			if ((this.DragEnter != null))
			{
				DragEnter(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DragOver" /> event.
		///</summary>
		/// <param name="dragEvent">A <see cref="DragEventArgs" /> that contains the event data. </param>
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragOverDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		protected virtual void OnDragOver(Wisej.Web.DragEventArgs e)
		{
			if ((this.DragOver != null))
			{
				DragOver(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DragLeave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ToolStripItemOnDragLeaveDescr")]
		[SRCategory("CatDragDrop")]
		[Browsable(false)]
		protected virtual void OnDragLeave(System.EventArgs e)
		{
			if ((this.DragLeave != null))
			{
				DragLeave(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.EnabledChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemEnabledChangedDescr")]
		protected virtual void OnEnabledChanged(System.EventArgs e)
		{
			if ((this.EnabledChanged != null))
			{
				EnabledChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.ForeColorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnForeColorChangedDescr")]
		protected virtual void OnForeColorChanged(System.EventArgs e)
		{
			if ((this.ForeColorChanged != null))
			{
				ForeColorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.LocationChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemOnLocationChangedDescr")]
		protected virtual void OnLocationChanged(System.EventArgs e)
		{
			if ((this.LocationChanged != null))
			{
				LocationChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseDown" /> event.
		///</summary>
		/// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseDownDescr")]
		protected virtual void OnMouseDown(Wisej.Web.MouseEventArgs e)
		{
			if ((this.MouseDown != null))
			{
				MouseDown(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseEnter" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseEnterDescr")]
		protected virtual void OnMouseEnter(System.EventArgs e)
		{
			if ((this.MouseEnter != null))
			{
				MouseEnter(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseLeave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseLeaveDescr")]
		protected virtual void OnMouseLeave(System.EventArgs e)
		{
			if ((this.MouseLeave != null))
			{
				MouseLeave(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseHover" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnMouseHoverDescr")]
		[SRCategory("CatMouse")]
		protected virtual void OnMouseHover(System.EventArgs e)
		{
			if ((this.MouseHover != null))
			{
				MouseHover(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseMove" /> event.
		///</summary>
		/// <param name="mea">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnMouseMoveDescr")]
		[SRCategory("CatMouse")]
		protected virtual void OnMouseMove(Wisej.Web.MouseEventArgs e)
		{
			if ((this.MouseMove != null))
			{
				MouseMove(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseUp" /> event.
		///</summary>
		/// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseUpDescr")]
		protected virtual void OnMouseUp(Wisej.Web.MouseEventArgs e)
		{
			if ((this.MouseUp != null))
			{
				MouseUp(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.OwnerChanged" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOwnerChangedDescr")]
		protected virtual void OnOwnerChanged(System.EventArgs e)
		{
			if ((this.OwnerChanged != null))
			{
				OwnerChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.QueryContinueDrag" /> event.
		///</summary>
		/// <param name="queryContinueDragEvent">A <see cref="QueryContinueDragEventArgs" /> that contains the event data. </param>
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnQueryContinueDragDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		protected virtual void OnQueryContinueDrag(Wisej.Web.QueryContinueDragEventArgs e)
		{
			if ((this.QueryContinueDrag != null))
			{
				QueryContinueDrag(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.RightToLeftChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnRightToLeftChangedDescr")]
		protected virtual void OnRightToLeftChanged(System.EventArgs e)
		{
			if ((this.RightToLeftChanged != null))
			{
				RightToLeftChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.TextChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnTextChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		protected virtual void OnTextChanged(System.EventArgs e)
		{
			if ((this.TextChanged != null))
			{
				TextChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.VisibleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripItemOnVisibleChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		protected virtual void OnVisibleChanged(System.EventArgs e)
		{
			if ((this.VisibleChanged != null))
			{
				VisibleChanged(this, e);
			}
		}

		/// <summary>
		/// Sets the <see cref="ToolStripItem" /> to the specified visible state. 
		///</summary>
		/// <param name="visible">true to make the <see cref="ToolStripItem" /> visible; otherwise, false.</param>
		protected virtual void SetVisibleCore(bool visible)
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDisplayStyle()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetImage()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetTextDirection()
		{
			// TODO: Implement
		}

		public override String ToString()
		{
			// TODO: Implement
			return "";
		}

		/// <summary>
		/// Begins a drag-and-drop operation.
		///</summary>
		/// <returns>One of the <see cref="DragDropEffects" /> values.</returns>
		/// <param name="data">The object to be dragged. </param>
		/// <param name="allowedEffects">The drag operations that can occur. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			// TODO: Implement
			return ((Wisej.Web.DragDropEffects)(0));
		}

		/// <summary>
		/// Retrieves the <see cref="ToolStrip" /> that is the container of the current <see cref="ToolStripItem" />.
		///</summary>
		/// <returns>A <see cref="ToolStrip" /> that is the container of the current <see cref="ToolStripItem" />.</returns>
		public ToolStrip GetCurrentParent()
		{
			// TODO: Implement
			return new ToolStrip();
		}

		/// <summary>
		/// Invalidates the entire surface of the <see cref="ToolStripItem" /> and causes it to be redrawn.
		///</summary>
		public void Invalidate()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Invalidates the specified region of the <see cref="ToolStripItem" /> by adding it to the update region of the <see cref="ToolStripItem" />, which is the area that will be repainted at the next paint operation, and causes a paint message to be sent to the <see cref="ToolStripItem" />.
		///</summary>
		/// <param name="r">A <see cref="System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		public void Invalidate(Rectangle r)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Activates the <see cref="ToolStripItem" /> when it is clicked with the mouse.
		///</summary>
		public void PerformClick()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Selects the item.
		///</summary>
		public void Select()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetMargin()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetPadding()
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