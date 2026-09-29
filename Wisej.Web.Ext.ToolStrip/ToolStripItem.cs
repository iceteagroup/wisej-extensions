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
	/// Represents the base class that manages events and layout for all the elements that a <see cref="ToolStrip" /> or <see cref="ToolStripDropDown" /> can contain.
	/// </summary>
	/// <remarks>
	/// Applications normally use one of the derived classes, such as <see cref="ToolStripButton" />, <see cref="ToolStripLabel" />,
	/// <see cref="ToolStripSeparator" />, <see cref="ToolStripDropDownButton" />, <see cref="ToolStripSplitButton" /> or <see cref="ToolStripMenuItem" />,
	/// and add them to the <see cref="ToolStrip.Items" /> collection of the owner <see cref="ToolStrip" />.
	/// </remarks>
	public class ToolStripItem : Wisej.Web.Component
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class.
		/// </summary>
		public ToolStripItem()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class with the specified text, image, and event handler.
		/// </summary>
		/// <param name="text">The text to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="onClick">The event handler attached to the <see cref="ToolStripItem.Click" /> event.</param>
		public ToolStripItem(string text, Image image, EventHandler onClick)
		{
			//this._text = text;
			//this._image = image;
			//this._onClick = onClick;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItem" /> class with the specified display text, image, event handler, and name.
		/// </summary>
		/// <param name="text">The text to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripItem" />.</param>
		/// <param name="onClick">The event handler attached to the <see cref="ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="ToolStripItem" />, see <see cref="ToolStripItem.Name" />.</param>
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
		/// Returns or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemAlignment" /> values. The default is <see cref="ToolStripItemAlignment.Left" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="ToolStripItemAlignment" /> values.</exception>
		/// <remarks>
		/// Items aligned to <see cref="ToolStripItemAlignment.Right" /> are laid out starting from the end of the <see cref="ToolStrip" />,
		/// in the order in which they appear in the <see cref="ToolStrip.Items" /> collection.
		/// </remarks>
		/// <example>
		/// Placing a "Help" button at the far end of a tool bar:
		/// <code><![CDATA[
		/// var help = new ToolStripButton("Help");
		/// help.Alignment = ToolStripItemAlignment.Right;
		/// this.toolStrip1.Items.Add(help);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.
		/// </summary>
		/// <returns>true if drag-and-drop operations are allowed on the item; otherwise, false. The default is false.</returns>
		/// <exception cref="System.ArgumentException"><see cref="ToolStripItem.AllowDrop" /> and <see cref="ToolStrip.AllowItemReorder" /> are both set to true.</exception>
		/// <remarks>
		/// When true, the item raises the <see cref="ToolStripItem.DragEnter" />, <see cref="ToolStripItem.DragOver" />,
		/// <see cref="ToolStripItem.DragLeave" /> and <see cref="ToolStripItem.DragDrop" /> events.
		/// It cannot be combined with <see cref="ToolStrip.AllowItemReorder" />.
		/// </remarks>
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
		/// Returns or sets a value indicating whether the item is automatically sized.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is automatically sized; otherwise, false. The default value is true.</returns>
		/// <remarks>
		/// When true, the item computes its size from its <see cref="ToolStripItem.Text" />, <see cref="ToolStripItem.Image" />,
		/// <see cref="ToolStripItem.Font" /> and <see cref="ToolStripItem.Padding" />, and the value assigned to <see cref="ToolStripItem.Size" /> is not used.
		/// </remarks>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem.Text" /> property is used as the tooltip of the <see cref="ToolStripItem" />
		/// when no custom tooltip text is set.
		/// </summary>
		/// <returns>true to use the <see cref="ToolStripItem.Text" /> property for the tooltip; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The default value is the value of <see cref="ToolStripItem.DefaultAutoToolTip" />, which is false for the base class and
		/// true for <see cref="ToolStripButton" />, <see cref="ToolStripDropDownButton" /> and <see cref="ToolStripSplitButton" />.
		/// </remarks>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem" /> should be placed on a <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is placed on a <see cref="ToolStrip" />; otherwise, false.</returns>
		/// <remarks>
		/// Setting <see cref="ToolStripItem.Available" /> to false removes the item from the layout without removing it from the
		/// <see cref="ToolStrip.Items" /> collection. Changing the value raises the <see cref="ToolStripItem.AvailableChanged" /> event.
		/// </remarks>
		/// <example>
		/// Showing an "Admin" button only to administrators:
		/// <code><![CDATA[
		/// this.toolStripButtonAdmin.Available = IsAdministrator(Application.Session.UserName);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the background image displayed in the item.
		/// </summary>
		/// <returns>An <see cref="System.Drawing.Image" /> that represents the image to display in the background of the item. The default is null.</returns>
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
		/// Returns or sets the background image layout used for the <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>One of the <see cref="ImageLayout" /> values. The default value is <see cref="ImageLayout.Tile" />.</returns>
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
		/// Returns or sets the background color for the item.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the background color of the item.</returns>
		/// <remarks>
		/// Changing the value raises the <see cref="ToolStripItem.BackColorChanged" /> event. Call <see cref="ToolStripItem.ResetBackColor" /> to restore the default color.
		/// </remarks>
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
		/// Returns the size and location of the item.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the <see cref="ToolStripItem" />, relative to its owner <see cref="ToolStrip" />.</returns>
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
		/// Returns the area where content, such as text and icons, can be placed within a <see cref="ToolStripItem" /> without overwriting background borders.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the location and size of the <see cref="ToolStripItem" /> contents, excluding its border.</returns>
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
		/// Returns a value indicating whether the item can be selected.
		/// </summary>
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
		/// Returns or sets the edges of the container to which a <see cref="ToolStripItem" /> is bound and determines how a <see cref="ToolStripItem" /> is resized with its parent.
		/// </summary>
		/// <returns>One of the <see cref="AnchorStyles" /> values. The default is <c>Top</c> and <c>Left</c>.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="AnchorStyles" /> values.</exception>
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
		/// Returns or sets which <see cref="ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="ToolStripItem" /> is resized with its parent.
		/// </summary>
		/// <returns>One of the <see cref="DockStyle" /> values. The default is <see cref="DockStyle.None" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="DockStyle" /> values.</exception>
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
		/// Returns the default value of the <see cref="ToolStripItem.AutoToolTip" /> property.
		/// </summary>
		/// <returns>false in all cases for the base <see cref="ToolStripItem" /> class.</returns>
		public virtual bool DefaultAutoToolTip
		{
			get
			{
				return this._defaultAutoToolTip;
			}
		}

		private bool _defaultAutoToolTip;

		/// <summary>
		/// Returns the default internal spacing of the item.
		/// </summary>
		/// <returns>The default <see cref="Padding" /> value of the <see cref="ToolStripItem.Padding" /> property.</returns>
		public virtual Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Returns the default size of the item.
		/// </summary>
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
		/// Returns the default value of the <see cref="ToolStripItem.DisplayStyle" /> property.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemDisplayStyle" /> values. The default is <see cref="ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		public virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return this._defaultDisplayStyle;
			}
		}

		private ToolStripItemDisplayStyle _defaultDisplayStyle;

		/// <summary>
		/// Returns or sets whether text and images are displayed on a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemDisplayStyle" /> values. The default is <see cref="ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		/// <remarks>
		/// The item keeps both its <see cref="ToolStripItem.Text" /> and <see cref="ToolStripItem.Image" /> values; <see cref="ToolStripItem.DisplayStyle" />
		/// only determines which of them is rendered. When both are displayed, their relative position is determined by <see cref="ToolStripItem.TextImageRelation" />.
		/// Changing the value raises the <see cref="ToolStripItem.DisplayStyleChanged" /> event.
		/// </remarks>
		/// <example>
		/// Showing only the icons of the tool bar buttons and using the text as the tooltip:
		/// <code><![CDATA[
		/// foreach (ToolStripItem item in this.toolStrip1.Items)
		/// {
		///     if (item is ToolStripButton button)
		///     {
		///         button.DisplayStyle = ToolStripItemDisplayStyle.Image;
		///         button.AutoToolTip = true;
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem" /> can be activated by double-clicking the mouse.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem" /> can be activated by double-clicking the mouse; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The <see cref="ToolStripItem.DoubleClick" /> event is raised only when this property is true.
		/// </remarks>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem" /> is enabled.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// A disabled item is rendered grayed out and doesn't raise the <see cref="ToolStripItem.Click" /> event.
		/// Changing the value raises the <see cref="ToolStripItem.EnabledChanged" /> event.
		/// </remarks>
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
		/// Returns or sets the foreground color of the item.
		/// </summary>
		/// <returns>The foreground <see cref="System.Drawing.Color" /> of the item.</returns>
		/// <remarks>
		/// Changing the value raises the <see cref="ToolStripItem.ForeColorChanged" /> event. Call <see cref="ToolStripItem.ResetForeColor" /> to restore the default color.
		/// </remarks>
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
		/// Returns or sets the font of the text displayed by the item.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Font" /> to apply to the text displayed by the <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// Call <see cref="ToolStripItem.ResetFont" /> to restore the default font.
		/// </remarks>
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
		/// Returns or sets the height, in pixels, of a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>An <see cref="System.Int32" /> representing the height, in pixels.</returns>
		/// <remarks>
		/// This is the same value as the <c>Height</c> of <see cref="ToolStripItem.Size" />. It is not used when <see cref="ToolStripItem.AutoSize" /> is true.
		/// </remarks>
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
		/// Returns or sets the alignment of the image on a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>One of the <see cref="System.Drawing.ContentAlignment" /> values. The default is <see cref="System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="System.Drawing.ContentAlignment" /> values.</exception>
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
		/// Returns or sets the image that is displayed on a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Image" /> to be displayed.</returns>
		/// <remarks>
		/// The image is displayed only when <see cref="ToolStripItem.DisplayStyle" /> includes the image. As an alternative, you can select an image from the
		/// <see cref="ToolStrip.ImageList" /> of the owner using <see cref="ToolStripItem.ImageIndex" /> or <see cref="ToolStripItem.ImageKey" />.
		/// Call <see cref="ToolStripItem.ResetImage" /> to remove the image.
		/// </remarks>
		/// <example>
		/// Assigning an image loaded from the application folder:
		/// <code><![CDATA[
		/// this.toolStripButtonSave.Image = Image.FromFile(Application.MapPath("Images/save.png"));
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the color to treat as transparent in a <see cref="ToolStripItem" /> image.
		/// </summary>
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
		/// Returns or sets the index of the image in the <see cref="ToolStrip.ImageList" /> of the owner that is displayed on the item.
		/// </summary>
		/// <returns>The zero-based index of the image in the <see cref="ToolStrip.ImageList" /> that is displayed for the item. The default is -1, signifying that no image is selected.</returns>
		/// <exception cref="System.ArgumentException">The value specified is less than -1.</exception>
		/// <remarks>
		/// <see cref="ToolStripItem.ImageIndex" /> and <see cref="ToolStripItem.ImageKey" /> are mutually exclusive: the last one set is used.
		/// The value is resolved against the <see cref="ToolStrip.ImageList" /> of the <see cref="ToolStripItem.Owner" />.
		/// </remarks>
		/// <example>
		/// Selecting the images from the tool bar's image list:
		/// <code><![CDATA[
		/// this.toolStrip1.ImageList = this.imageList1;
		/// this.toolStripButtonOpen.ImageIndex = 0;
		/// this.toolStripButtonSave.ImageKey = "save.png";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the key of the image in the <see cref="ToolStrip.ImageList" /> of the owner that is displayed on the <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>A string representing the key of the image.</returns>
		/// <remarks>
		/// <see cref="ToolStripItem.ImageKey" /> and <see cref="ToolStripItem.ImageIndex" /> are mutually exclusive: the last one set is used.
		/// </remarks>
		/// <example>
		/// Selecting an image by its key:
		/// <code><![CDATA[
		/// this.toolStrip1.ImageList = this.imageList1;
		/// this.toolStripButtonPrint.ImageKey = "print.png";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets a value indicating whether an image on a <see cref="ToolStripItem" /> is automatically resized to fit in a container.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemImageScaling" /> values. The default is <see cref="ToolStripItemImageScaling.SizeToFit" />.</returns>
		/// <remarks>
		/// When set to <see cref="ToolStripItemImageScaling.SizeToFit" /> the image is scaled to the <see cref="ToolStrip.ImageScalingSize" /> of the owner.
		/// </remarks>
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
		/// Returns a value indicating whether the object has been disposed of.
		/// </summary>
		/// <returns>true if the item has been disposed of; otherwise, false.</returns>
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
		/// Returns a value indicating whether the container of the current <see cref="ToolStripItem" /> is a <see cref="ToolStripDropDown" />.
		/// </summary>
		/// <returns>true if the container of the current <see cref="ToolStripItem" /> is a <see cref="ToolStripDropDown" />; otherwise, false.</returns>
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
		/// Returns a value indicating whether the <see cref="ToolStripItem.Placement" /> property is set to <see cref="ToolStripItemPlacement.Overflow" />.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem.Placement" /> property is set to <see cref="ToolStripItemPlacement.Overflow" />; otherwise, false.</returns>
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
		/// Returns or sets how child menus are merged with parent menus.
		/// </summary>
		/// <returns>One of the <see cref="MergeAction" /> values. The default is <see cref="MergeAction.Append" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="MergeAction" /> values.</exception>
		/// <remarks>
		/// This property is used when the <see cref="ToolStrip" /> that contains this item is merged into another <see cref="ToolStrip" /> using <see cref="ToolStripManager" />.
		/// Source items are matched to target items by their <see cref="ToolStripItem.Text" />; <see cref="MergeAction.Insert" /> uses
		/// <see cref="ToolStripItem.MergeIndex" /> to determine the position of the item in the target.
		/// </remarks>
		/// <example>
		/// Inserting a child form's menu item at a specific position of the main menu when the menus are merged:
		/// <code><![CDATA[
		/// this.toolStripMenuItemReports.MergeAction = MergeAction.Insert;
		/// this.toolStripMenuItemReports.MergeIndex = 2;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the position of a merged item within the target <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>An integer representing the zero-based position of the merged item in the target <see cref="ToolStrip" />, or -1 to use the default position. The default is -1.</returns>
		/// <remarks>
		/// This value is used together with <see cref="ToolStripItem.MergeAction" /> when the <see cref="ToolStrip" /> that contains this item is merged into another one.
		/// </remarks>
		/// <example>
		/// Inserting the item as the first item of the target when merging:
		/// <code><![CDATA[
		/// this.toolStripButtonExport.MergeAction = MergeAction.Insert;
		/// this.toolStripButtonExport.MergeIndex = 0;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the name of the item.
		/// </summary>
		/// <returns>A string representing the name. The default value is null.</returns>
		/// <remarks>
		/// The name is used as the key by <see cref="ToolStripItemCollection.ContainsKey" />, <see cref="ToolStripItemCollection.IndexOfKey" />,
		/// <see cref="ToolStripItemCollection.RemoveByKey" /> and <see cref="ToolStripItemCollection.Find" />.
		/// </remarks>
		/// <example>
		/// Locating an item by name:
		/// <code><![CDATA[
		/// var save = new ToolStripButton("Save");
		/// save.Name = "buttonSave";
		/// this.toolStrip1.Items.Add(save);
		///
		/// int index = this.toolStrip1.Items.IndexOfKey("buttonSave");
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the owner of this item.
		/// </summary>
		/// <returns>The <see cref="ToolStrip" /> that owns or is to own the <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The owner is set automatically when the item is added to the <see cref="ToolStrip.Items" /> collection of a <see cref="ToolStrip" />.
		/// Changing the value raises the <see cref="ToolStripItem.OwnerChanged" /> event. For items displayed on a drop-down, the owner is the
		/// <see cref="ToolStripDropDown" />; use <see cref="ToolStripItem.OwnerItem" /> to get the item that opened it.
		/// </remarks>
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
		/// Returns the parent <see cref="ToolStripItem" /> of this <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>The <see cref="ToolStripDropDownItem" /> that displays the drop-down containing this item, or null if the item is not on a drop-down.</returns>
		/// <example>
		/// Finding the menu that contains a clicked item:
		/// <code><![CDATA[
		/// private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		/// {
		///     var item = (ToolStripItem)sender;
		///     AlertBox.Show("Clicked in: " + item.OwnerItem?.Text);
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the item is attached to the <see cref="ToolStrip" /> or <see cref="ToolStripOverflowButton" /> or can float between the two.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemOverflow" /> values. The default is <see cref="ToolStripItemOverflow.AsNeeded" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="ToolStripItemOverflow" /> values.</exception>
		/// <remarks>
		/// With <see cref="ToolStripItemOverflow.AsNeeded" /> the item moves to the overflow drop-down only when there isn't enough room on the <see cref="ToolStrip" />.
		/// The current location of the item is returned by <see cref="ToolStripItem.Placement" />.
		/// </remarks>
		/// <example>
		/// Keeping the most important button always visible and moving a rarely used one to the overflow menu:
		/// <code><![CDATA[
		/// this.toolStripButtonSave.Overflow = ToolStripItemOverflow.Never;
		/// this.toolStripButtonAbout.Overflow = ToolStripItemOverflow.Always;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the internal spacing, in pixels, between the item's contents and its edges.
		/// </summary>
		/// <returns>A <see cref="Padding" /> representing the item's internal spacing, in pixels. The default is <see cref="ToolStripItem.DefaultPadding" />.</returns>
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
		/// Returns the current layout of the item.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripItemPlacement" /> values: <see cref="ToolStripItemPlacement.Main" /> when the item is displayed on the <see cref="ToolStrip" />,
		/// <see cref="ToolStripItemPlacement.Overflow" /> when it is displayed in the overflow drop-down, or <see cref="ToolStripItemPlacement.None" /> when it is not displayed.</returns>
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
		/// Returns a value indicating whether the state of the item is pressed.
		/// </summary>
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
		/// Returns or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.
		/// </summary>
		/// <returns>One of the <see cref="RightToLeft" /> values.</returns>
		/// <remarks>
		/// Changing the value raises the <see cref="ToolStripItem.RightToLeftChanged" /> event. Call <see cref="ToolStripItem.ResetRightToLeft" /> to restore the default value.
		/// </remarks>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem" /> image is mirrored automatically when the <see cref="ToolStripItem.RightToLeft" /> property is set to <see cref="RightToLeft.Yes" />.
		/// </summary>
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
		/// Returns a value indicating whether the item is selected.
		/// </summary>
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
		/// Returns or sets the size of the item.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Size" />, representing the width and height of the item, in pixels.</returns>
		/// <remarks>
		/// The value is used only when <see cref="ToolStripItem.AutoSize" /> is false.
		/// </remarks>
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
		/// Returns or sets the object that contains data about the item.
		/// </summary>
		/// <returns>An <see cref="System.Object" /> that contains data about the item. The default is null.</returns>
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
		/// Returns or sets the text that is to be displayed on the item.
		/// </summary>
		/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
		/// <remarks>
		/// The text is displayed only when <see cref="ToolStripItem.DisplayStyle" /> includes the text. Changing the value raises the <see cref="ToolStripItem.TextChanged" /> event.
		/// </remarks>
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
		/// Returns or sets the alignment of the text on a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>One of the <see cref="System.Drawing.ContentAlignment" /> values. The default is <see cref="System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="System.Drawing.ContentAlignment" /> values.</exception>
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
		/// Returns or sets the orientation of text used on a <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripTextDirection" /> values.</returns>
		/// <remarks>
		/// <see cref="ToolStripTextDirection.Inherit" /> uses the text direction of the owner <see cref="ToolStrip" />. Call
		/// <see cref="ToolStripItem.ResetTextDirection" /> to restore the default value.
		/// </remarks>
		/// <example>
		/// Rendering the text of a button vertically on a tool bar docked to the left:
		/// <code><![CDATA[
		/// this.toolStripButtonNotes.TextDirection = ToolStripTextDirection.Vertical270;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the position of <see cref="ToolStripItem" /> text and image relative to each other.
		/// </summary>
		/// <returns>One of the <see cref="TextImageRelation" /> values. The default is <see cref="TextImageRelation.ImageBeforeText" />.</returns>
		/// <remarks>
		/// The value is used only when <see cref="ToolStripItem.DisplayStyle" /> is <see cref="ToolStripItemDisplayStyle.ImageAndText" />.
		/// </remarks>
		/// <example>
		/// Displaying large buttons with the icon above the text:
		/// <code><![CDATA[
		/// this.toolStripButtonNew.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
		/// this.toolStripButtonNew.TextImageRelation = TextImageRelation.ImageAboveText;
		/// ]]></code>
		/// </example>
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
		/// Resets the <see cref="ToolStripItem.DisplayStyle" /> property to its default value, <see cref="ToolStripItem.DefaultDisplayStyle" />.
		/// </summary>
		/// <example>
		/// Restoring the default display style:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetDisplayStyle();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDisplayStyle()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.Font" /> property to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default font:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetFont();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.Image" /> property to its default value (null).
		/// </summary>
		/// <example>
		/// Removing the image from a button:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetImage();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetImage()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.RightToLeft" /> property to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default right-to-left setting:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetRightToLeft();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.TextDirection" /> property to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default text direction:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetTextDirection();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetTextDirection()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Returns a string that represents the current <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>A string that represents the current <see cref="ToolStripItem" />.</returns>
		/// <example>
		/// Logging the items of a tool bar:
		/// <code><![CDATA[
		/// foreach (ToolStripItem item in this.toolStrip1.Items)
		/// {
		///     System.Diagnostics.Debug.WriteLine(item.ToString());
		/// }
		/// ]]></code>
		/// </example>
		public override String ToString()
		{
			// TODO: Implement
			return "";
		}

		/// <summary>
		/// Begins a drag-and-drop operation.
		/// </summary>
		/// <param name="data">The object to be dragged.</param>
		/// <param name="allowedEffects">The drag operations that can occur.</param>
		/// <returns>One of the <see cref="DragDropEffects" /> values.</returns>
		/// <example>
		/// Starting a drag operation when the user presses the mouse on a button:
		/// <code><![CDATA[
		/// private void toolStripButtonDocument_MouseDown(object sender, MouseEventArgs e)
		/// {
		///     this.toolStripButtonDocument.DoDragDrop(this.toolStripButtonDocument.Text, DragDropEffects.Copy);
		/// }
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			// TODO: Implement
			return ((Wisej.Web.DragDropEffects)(0));
		}

		/// <summary>
		/// Retrieves the <see cref="ToolStrip" /> that is the container of the current <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>A <see cref="ToolStrip" /> that is the container of the current <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The returned container is the <see cref="ToolStrip" /> or <see cref="ToolStripDropDown" /> on which the item is currently displayed,
		/// which can be the overflow drop-down when <see cref="ToolStripItem.IsOnOverflow" /> is true.
		/// </remarks>
		/// <example>
		/// Retrieving the tool bar that contains the clicked item:
		/// <code><![CDATA[
		/// private void toolStripButton1_Click(object sender, EventArgs e)
		/// {
		///     ToolStrip parent = ((ToolStripItem)sender).GetCurrentParent();
		///     AlertBox.Show("Clicked on: " + parent.Name);
		/// }
		/// ]]></code>
		/// </example>
		public ToolStrip GetCurrentParent()
		{
			// TODO: Implement
			return new ToolStrip();
		}

		/// <summary>
		/// Invalidates the entire surface of the <see cref="ToolStripItem" /> and causes it to be redrawn.
		/// </summary>
		/// <example>
		/// Redrawing an item after changing data used by a custom <see cref="ToolStripItem.Paint" /> handler:
		/// <code><![CDATA[
		/// this.unreadCount = 5;
		/// this.toolStripButtonInbox.Invalidate();
		/// ]]></code>
		/// </example>
		public void Invalidate()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Invalidates the specified region of the <see cref="ToolStripItem" /> and causes it to be redrawn.
		/// </summary>
		/// <param name="r">A <see cref="System.Drawing.Rectangle" /> that represents the region to invalidate, in item coordinates.</param>
		/// <example>
		/// Redrawing only the content area of the item:
		/// <code><![CDATA[
		/// this.toolStripButtonInbox.Invalidate(this.toolStripButtonInbox.ContentRectangle);
		/// ]]></code>
		/// </example>
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
		/// Generates a <see cref="ToolStripItem.Click" /> event for the <see cref="ToolStripItem" />, as if it was clicked by the user.
		/// </summary>
		/// <remarks>
		/// The event is raised only if the item is <see cref="ToolStripItem.Enabled" /> and <see cref="ToolStripItem.Available" />.
		/// </remarks>
		/// <example>
		/// Executing the "Save" button when the user presses a button on the form:
		/// <code><![CDATA[
		/// private void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     this.toolStripButtonSave.PerformClick();
		/// }
		/// ]]></code>
		/// </example>
		public void PerformClick()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Selects the item.
		/// </summary>
		/// <remarks>
		/// The item is selected only if <see cref="ToolStripItem.CanSelect" /> is true. After the call, <see cref="ToolStripItem.Selected" /> returns true.
		/// </remarks>
		/// <example>
		/// Highlighting the first item of a tool bar:
		/// <code><![CDATA[
		/// this.toolStrip1.Items[0].Select();
		/// ]]></code>
		/// </example>
		public void Select()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.BackColor" /> property to its default value.
		/// </summary>
		/// <example>
		/// Removing a highlight color:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetBackColor();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.ForeColor" /> property to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default text color:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetForeColor();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the margin of the item to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default margin:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetMargin();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetMargin()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripItem.Padding" /> property to its default value, <see cref="ToolStripItem.DefaultPadding" />.
		/// </summary>
		/// <example>
		/// Restoring the default padding:
		/// <code><![CDATA[
		/// this.toolStripButton1.ResetPadding();
		/// ]]></code>
		/// </example>
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