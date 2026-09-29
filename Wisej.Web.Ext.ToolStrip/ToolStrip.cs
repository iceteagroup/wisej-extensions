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
using System.ComponentModel.Design;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Provides a container for toolbar objects such as <see cref="ToolStripButton"/>, <see cref="ToolStripLabel"/>,
	/// <see cref="ToolStripSeparator"/> and <see cref="ToolStripDropDownButton"/> items.
	/// </summary>
	/// <remarks>
	/// This class is a port of the WinForms <c>System.Windows.Forms.ToolStrip</c> control. The items displayed
	/// by the tool strip are managed through the <see cref="Items"/> collection.
	/// </remarks>
	public class ToolStrip : ScrollableControl, IWisejControl, IWisejDesignTarget2
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStrip" /> class.
		/// </summary>
		public ToolStrip()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStrip" /> class with the specified array of <see cref="ToolStripItem"/> objects.
		/// </summary>
		/// <param name="items">An array of <see cref="ToolStripItem"/> objects to add to the <see cref="Items"/> collection.</param>
		/// <example>
		/// Creating a tool strip with a set of buttons:
		/// <code><![CDATA[
		/// var toolStrip = new ToolStrip(new ToolStripItem[]
		/// {
		///     new ToolStripButton("New"),
		///     new ToolStripButton("Open"),
		///     new ToolStripSeparator(),
		///     new ToolStripButton("Save")
		/// });
		/// this.Controls.Add(toolStrip);
		/// ]]></code>
		/// </example>
		public ToolStrip(ToolStripItem[] items)
		{
			//this._items = items;
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the <see cref="ToolStrip.AutoSize" /> property has changed.
		///</summary>
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add => base.AutoSizeChanged += value;
			remove => base.AutoSizeChanged -= value;
		}

		/// <summary>
		/// Occurs when the user begins to drag the <see cref="ToolStrip" /> control.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripOnBeginDrag")]
		public event EventHandler BeginDrag
		{
			add => Events.AddHandler(nameof(BeginDrag), value);
			remove => Events.RemoveHandler(nameof(BeginDrag), value);
		}

		/// <summary>
		/// Occurs when the <see cref="ToolStrip.CausesValidation" /> property changes.
		///</summary>
		[Browsable(false)]
		public new event EventHandler CausesValidationChanged
		{
			add => base.CausesValidationChanged += value;
			remove => base.CausesValidationChanged -= value;
		}

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add => base.ControlAdded += value;
			remove => base.ControlAdded -= value;
		}

		/// <summary>
		/// Occurs when the value of the <see cref="Cursor" /> property changes.
		///</summary>
		[Browsable(false)]
		public event EventHandler CursorChanged
		{
			add => base.CursorChanged += value;
			remove => base.CursorChanged -= value;
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event ControlEventHandler ControlRemoved;

		/// <summary>
		/// Occurs when the user stops dragging the <see cref="ToolStrip" /> control.
		/// </summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripOnEndDrag")]
		public event EventHandler EndDrag;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStrip.ForeColor" /> property changes.
		/// </summary>
		[Browsable(false)]
		public event EventHandler ForeColorChanged;

		/// <summary>
		/// Occurs when a new <see cref="ToolStripItem" /> is added to the <see cref="ToolStripItemCollection" />.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemAddedDescr")]
		public event ToolStripItemEventHandler ItemAdded;

		/// <summary>
		/// Occurs when the <see cref="ToolStripItem" /> is clicked.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		public event ToolStripItemClickedEventHandler ItemClicked
		{
			add => Events.AddHandler(nameof(ItemClicked), value);
			remove => Events.RemoveHandler(nameof(ItemClicked), value);
		}

		/// <summary>
		/// Occurs when a <see cref="ToolStripItem" /> is removed from the <see cref="ToolStripItemCollection" />.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemRemovedDescr")]
		public event ToolStripItemEventHandler ItemRemoved
		{
			add => Events.AddHandler(nameof(ItemRemoved), value);
			remove => Events.RemoveHandler(nameof(ItemRemoved), value);
		}

		/// <summary>
		/// Occurs when the layout of the <see cref="ToolStrip" /> is complete.
		///</summary>
		[SRDescription("ToolStripLayoutCompleteDescr")]
		[SRCategory("CatAppearance")]
		public event EventHandler LayoutCompleted;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStrip.LayoutStyle" /> property changes.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLayoutStyleChangedDescr")]
		public event EventHandler LayoutStyleChanged;

		///// <summary>
		///// Raises the <see cref="ToolStrip.BeginDrag" /> event. 
		/////</summary>
		///// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		//[SRCategory("CatBehavior")]
		//[SRDescription("ToolStripOnBeginDrag")]
		//protected virtual void OnBeginDrag(System.EventArgs e)
		//{
		//	if ((this.BeginDrag != null))
		//	{
		//		BeginDrag(this, e);
		//	}
		//}

		///// <summary>
		///// Raises the <see cref="Control.CausesValidationChanged" /> event.
		/////</summary>
		///// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		//[Browsable(false)]
		//protected virtual void OnCausesValidationChanged(System.EventArgs e)
		//{
		//	// TODO: Add new to the method, it hides a base event.
		//	if ((this.CausesValidationChanged != null))
		//	{
		//		CausesValidationChanged(this, e);
		//	}
		//}

		///// <summary>
		///// Raises the <see cref="Control.ControlAdded" /> event.
		/////</summary>
		///// <param name="e">A <see cref="ControlEventArgs" /> that contains the event data. </param>
		//[Browsable(false)]
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//protected internal virtual void OnControlAdded(ControlEventArgs e)
		//{
		//	// TODO: Add new to the method, it hides a base event.
		//	if ((this.ControlAdded != null))
		//	{
		//		ControlAdded(this, e);
		//	}
		//}

		///// <summary>
		///// Raises the <see cref="Control.CursorChanged" /> event.
		/////</summary>
		///// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		//[Browsable(false)]
		//new protected virtual void OnCursorChanged(System.EventArgs e)
		//{
		//	// TODO: Add new to the method, it hides a base event.
		//	if ((this.CursorChanged != null))
		//	{
		//		CursorChanged(this, e);
		//	}
		//}

		/// <summary>
		/// Raises the <see cref="Control.ControlRemoved" /> event.
		///</summary>
		/// <param name="e">A <see cref="ControlEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		new protected virtual void OnControlRemoved(ControlEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ControlRemoved != null))
			{
				ControlRemoved(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.EndDrag" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripOnEndDrag")]
		protected virtual void OnEndDrag(System.EventArgs e)
		{
			if ((this.EndDrag != null))
			{
				EndDrag(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ForeColorChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		new protected virtual void OnForeColorChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ForeColorChanged != null))
			{
				ForeColorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ItemAdded" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemAddedDescr")]
		protected virtual void OnItemAdded(ToolStripItemEventArgs e)
		{
			if ((this.ItemAdded != null))
			{
				ItemAdded(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ItemClicked" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemClickedEventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			((ToolStripItemClickedEventHandler)base.Events[nameof(ItemClicked)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ItemRemoved" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemRemovedDescr")]
		protected virtual void OnItemRemoved(ToolStripItemEventArgs e)
		{
			((ToolStripItemEventHandler)base.Events[nameof(ItemRemoved)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.LayoutCompleted" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRDescription("ToolStripLayoutCompleteDescr")]
		[SRCategory("CatAppearance")]
		protected virtual void OnLayoutCompleted(System.EventArgs e)
		{
			if ((this.LayoutCompleted != null))
			{
				LayoutCompleted(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.LayoutStyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLayoutStyleChangedDescr")]
		protected virtual void OnLayoutStyleChanged(System.EventArgs e)
		{
			if ((this.LayoutStyleChanged != null))
			{
				LayoutStyleChanged(this, e);
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets a value indicating whether the control is automatically resized to display its entire contents.
		/// </summary>
		/// <returns>true if the control adjusts its size to closely fit its contents; otherwise, false. The default is true.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(true)]
		[SRDescription("ControlAutoSizeDescr")]
		[Localizable(true)]
		[SRCategory("CatLayout")]
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

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>true to automatically scroll; otherwise, false.</returns>
		/// <exception cref="System.NotSupportedException">Automatic scrolling is not supported by <see cref="ToolStrip" /> controls.</exception>
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
		/// </summary>
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
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Size" /> value.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Point" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Point AutoScrollPosition
		{
			get
			{
				return this._autoScrollPosition;
			}
			set
			{
				if ((this._autoScrollPosition != value))
				{
					this._autoScrollPosition = value;
				}
			}
		}

		private Point _autoScrollPosition;

		/// <summary>
		/// Returns or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.
		/// </summary>
		/// <returns>true to control drag-and-drop and item reordering through events that you implement; otherwise, false. The default is false.</returns>
		/// <exception cref="System.ArgumentException"><see cref="AllowDrop" /> and <see cref="AllowItemReorder" /> are both set to true.</exception>
		/// <remarks>
		/// <see cref="AllowDrop"/> and <see cref="AllowItemReorder"/> are mutually exclusive: use <see cref="AllowDrop"/> to handle
		/// the drag-and-drop events yourself, or <see cref="AllowItemReorder"/> to let the <see cref="ToolStrip"/> reorder its items.
		/// </remarks>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
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
		/// Returns or sets a value indicating whether drag-and-drop and item reordering are handled privately by the <see cref="ToolStrip" /> class.
		/// </summary>
		/// <returns>true to cause the <see cref="ToolStrip" /> class to handle drag-and-drop and item reordering automatically; otherwise, false. The default is false.</returns>
		/// <exception cref="System.ArgumentException"><see cref="AllowDrop" /> and <see cref="AllowItemReorder" /> are both set to true.</exception>
		/// <remarks>
		/// This property cannot be set to true when <see cref="AllowDrop"/> is also true.
		/// </remarks>
		/// <example>
		/// Letting the user reorder the items of a tool strip:
		/// <code><![CDATA[
		/// this.toolStrip1.AllowDrop = false;
		/// this.toolStrip1.AllowItemReorder = true;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripAllowItemReorderDescr")]
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		public bool AllowItemReorder
		{
			get
			{
				return this._allowItemReorder;
			}
			set
			{
				if ((this._allowItemReorder != value))
				{
					this._allowItemReorder = value;
				}
			}
		}

		private bool _allowItemReorder;

		/// <summary>
		/// Returns or sets a value indicating whether the items of this <see cref="ToolStrip"/> can be combined with the items of another
		/// <see cref="ToolStrip"/>, <see cref="ToolStripDropDownMenu"/> or <see cref="ToolStripMenuItem"/> using <see cref="ToolStripManager.Merge(ToolStrip, ToolStrip)"/>.
		/// </summary>
		/// <returns>true if merging is allowed; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// The way each item is merged is controlled by the <see cref="ToolStripItem.MergeAction"/> and <see cref="ToolStripItem.MergeIndex"/>
		/// properties of the items in the source tool strip.
		/// </remarks>
		/// <example>
		/// Merging the tool strip of a child view into the main tool strip:
		/// <code><![CDATA[
		/// this.mainToolStrip.AllowMerge = true;
		/// this.childToolStrip.AllowMerge = true;
		///
		/// var manager = new ToolStripManager();
		/// manager.Merge(this.childToolStrip, this.mainToolStrip);
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripAllowMergeDescr")]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		public bool AllowMerge
		{
			get
			{
				return this._allowMerge;
			}
			set
			{
				if ((this._allowMerge != value))
				{
					this._allowMerge = value;
				}
			}
		}

		private bool _allowMerge;

		/// <summary>
		/// Returns or sets the edges of the container to which a <see cref="ToolStrip" /> is bound and determines how a <see cref="ToolStrip" /> is resized with its parent.
		/// </summary>
		/// <returns>A bitwise combination of the <see cref="AnchorStyles" /> values. The default is <c>Top, Left</c>.</returns>
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[SRDescription("ControlAnchorDescr")]
		public override AnchorStyles Anchor
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
		/// Returns or sets the background color for the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the background color of the <see cref="ToolStrip" />.</returns>
		[SRDescription("ToolStripBackColorDescr")]
		[SRCategory("CatAppearance")]
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
		/// Returns or sets the binding context for the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>A <see cref="BindingContext" /> for the <see cref="ToolStrip" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBindingContextDescr")]
		public override BindingContext BindingContext
		{
			get
			{
				return this._bindingContext;
			}
			set
			{
				if ((this._bindingContext != value))
				{
					this._bindingContext = value;
				}
			}
		}

		private BindingContext _bindingContext;

		/// <summary>
		/// Returns or sets a value indicating whether items in the <see cref="ToolStrip" /> can be sent to an overflow menu.
		/// </summary>
		/// <returns>true to send <see cref="ToolStrip" /> items to an overflow menu; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// When enabled, items that don't fit in the <see cref="ToolStrip"/> and have their <see cref="ToolStripItem.Overflow"/>
		/// property set to <see cref="ToolStripItemOverflow.AsNeeded"/> (or <see cref="ToolStripItemOverflow.Always"/>) are moved
		/// to the drop down of the <see cref="OverflowButton"/>.
		/// </remarks>
		/// <example>
		/// Keeping the most important button always visible while the others can overflow:
		/// <code><![CDATA[
		/// this.toolStrip1.CanOverflow = true;
		/// this.buttonSave.Overflow = ToolStripItemOverflow.Never;
		/// this.buttonExport.Overflow = ToolStripItemOverflow.AsNeeded;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripCanOverflowDescr")]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		public bool CanOverflow
		{
			get
			{
				return this._canOverflow;
			}
			set
			{
				if ((this._canOverflow != value))
				{
					this._canOverflow = value;
				}
			}
		}

		private bool _canOverflow;

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="ToolStrip" /> causes validation to be performed on any controls that require validation when it receives focus.
		/// </summary>
		/// <returns>true if the <see cref="ToolStrip"/> causes validation; otherwise, false. The default is false.</returns>
		[Browsable(false)]
		[DefaultValue(false)]
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
		/// </summary>
		/// <returns>A <see cref="Control.ControlCollection" /> representing the collection of controls contained within the <see cref="ToolStrip" />.</returns>
		/// <remarks>
		/// Use the <see cref="Items"/> collection to add items to the <see cref="ToolStrip"/>.
		/// </remarks>
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

		/// <summary>
		/// Returns or sets the cursor that is displayed when the mouse pointer is over the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>A <see cref="Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="ToolStrip" />.</returns>
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
		/// Returns or sets the font used to display text in the control.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Font"/> used to display the text of the <see cref="ToolStrip"/> items.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlFontDescr")]
		public override Font Font
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
		/// Returns the default size of the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>The default <see cref="System.Drawing.Size" /> of the <see cref="ToolStrip" />.</returns>
		public new Size DefaultSize
		{
			// TODO : Should this be public?
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// Gets the internal spacing, in pixels, of the contents of a <see cref="ToolStrip" />.
		///</summary>
		/// <returns>A <see cref="Padding" /> value of (0, 0, 1, 0).</returns>
		protected override Padding DefaultPadding
		{
			// TODO : Should this be public?
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Gets the spacing, in pixels, between the <see cref="ToolStrip" /> and the <see cref="ToolStripContainer" />.
		///</summary>
		/// <returns>One of the <see cref="Padding" /> values. The default is <see cref="Wisej.Web.Ext.Padding.Empty" />.</returns>
		protected override Padding DefaultMargin
		{
			// TODO : Should this be public?
			get
			{
				return this._defaultMargin;
			}
		}

		private Padding _defaultMargin;

		/// <summary>
		/// Returns the default docking location of the <see cref="ToolStrip" />, indicating which borders are docked to the container.
		/// </summary>
		/// <returns>One of the <see cref="DockStyle" /> values. The default is <see cref="DockStyle.Top" />.</returns>
		public virtual DockStyle DefaultDock
		{
			get
			{
				return this._defaultDock;
			}
		}

		private DockStyle _defaultDock;

		/// <summary>
		/// Returns the default spacing, in pixels, between the sizing grip and the edges of the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns><see cref="Padding" /> values representing the spacing, in pixels.</returns>
		/// <remarks>
		/// This is the value used by <see cref="GripMargin"/> when it's not set explicitly.
		/// </remarks>
		public virtual Padding DefaultGripMargin
		{
			get
			{
				return this._defaultGripMargin;
			}
		}

		private Padding _defaultGripMargin;

		/// <summary>
		/// Returns a value indicating whether ToolTips are shown for the <see cref="ToolStrip" /> by default.
		/// </summary>
		/// <returns>true in all cases.</returns>
		/// <remarks>
		/// Derived classes can override this property to change the initial value of <see cref="ShowItemToolTips"/>.
		/// </remarks>
		public virtual bool DefaultShowItemToolTips
		{
			get
			{
				return this._defaultShowItemToolTips;
			}
		}

		private bool _defaultShowItemToolTips;

		/// <summary>
		/// Returns or sets a value representing the default direction in which a <see cref="ToolStripDropDown" /> control is displayed relative to the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripDropDownDirection" /> values.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="ToolStripDropDownDirection" /> values.</exception>
		/// <remarks>
		/// The value is used by the drop downs of the items in this <see cref="ToolStrip"/> unless an item specifies its own
		/// <see cref="ToolStripDropDownItem.DropDownDirection"/>.
		/// </remarks>
		/// <example>
		/// Opening all the drop downs of a tool strip docked at the bottom of the page upward:
		/// <code><![CDATA[
		/// this.statusToolStrip.Dock = DockStyle.Bottom;
		/// this.statusToolStrip.DefaultDropDownDirection = ToolStripDropDownDirection.AboveRight;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripDefaultDropDownDirectionDescr")]
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		public virtual ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				return this._defaultDropDownDirection;
			}
			set
			{
				if ((this._defaultDropDownDirection != value))
				{
					this._defaultDropDownDirection = value;
				}
			}
		}

		private ToolStripDropDownDirection _defaultDropDownDirection;

		/// <summary>
		/// Returns or sets which <see cref="ToolStrip" /> borders are docked to its parent control and determines how a <see cref="ToolStrip" /> is resized with its parent.
		/// </summary>
		/// <returns>One of the <see cref="DockStyle" /> values. The default is <see cref="DockStyle.Top" />.</returns>
		[DefaultValue(DockStyle.Top)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
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

		/// <summary>
		/// Returns the current display rectangle.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> representing the <see cref="ToolStrip" /> area for item layout.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisplayRectangleDescr")]
		public override Rectangle DisplayRectangle
		{
			get
			{
				return this._displayRectangle;
			}
		}

		private Rectangle _displayRectangle;

		/// <summary>
		/// Returns or sets the foreground color of the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Color" /> representing the foreground color.</returns>
		[Browsable(false)]
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
		/// Returns or sets whether the <see cref="ToolStrip" /> move handle is visible or hidden.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripGripStyle" /> values. The default is <see cref="ToolStripGripStyle.Visible" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="ToolStripGripStyle" /> values.</exception>
		[SRDescription("ToolStripGripStyleDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolStripGripStyle.Visible)]
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return this._gripStyle;
			}
			set
			{
				if ((this._gripStyle != value))
				{
					this._gripStyle = value;
				}
			}
		}

		private ToolStripGripStyle _gripStyle;

		/// <summary>
		/// Returns the orientation of the <see cref="ToolStrip" /> move handle.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripGripDisplayStyle" /> values: <see cref="ToolStripGripDisplayStyle.Horizontal" /> or <see cref="ToolStripGripDisplayStyle.Vertical" />.</returns>
		/// <remarks>
		/// The grip orientation is the opposite of the <see cref="Orientation"/> of the <see cref="ToolStrip"/>: a horizontal
		/// tool strip has a vertical grip and vice versa.
		/// </remarks>
		[Browsable(false)]
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return this._gripDisplayStyle;
			}
		}

		private ToolStripGripDisplayStyle _gripDisplayStyle;

		/// <summary>
		/// Returns or sets the space around the <see cref="ToolStrip" /> move handle.
		/// </summary>
		/// <returns>A <see cref="Padding" />, which represents the spacing. The default is the value of <see cref="DefaultGripMargin"/>.</returns>
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripGripDisplayStyleDescr")]
		public Padding GripMargin
		{
			get
			{
				return this._gripMargin;
			}
			set
			{
				if ((this._gripMargin != value))
				{
					this._gripMargin = value;
				}
			}
		}

		private Padding _gripMargin;

		/// <summary>
		/// Returns the boundaries of the <see cref="ToolStrip" /> move handle.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> representing the move handle boundaries. If the move handle is not visible, the <see cref="GripRectangle" /> property returns <see cref="System.Drawing.Rectangle.Empty" />.</returns>
		[Browsable(false)]
		public Rectangle GripRectangle
		{
			get
			{
				return this._gripRectangle;
			}
		}

		private Rectangle _gripRectangle;

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>true if the <see cref="ToolStrip" /> has children; otherwise, false.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasChildren
		{
			get
			{
				return this._hasChildren;
			}
		}

		private bool _hasChildren;

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>An instance of the <see cref="HScrollProperties" /> class, which provides basic properties for a horizontal scroll bar.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HScrollProperties HorizontalScroll
		{
			get
			{
				return this._horizontalScroll;
			}
		}

		private HScrollProperties _horizontalScroll;

		/// <summary>
		/// Returns or sets the size, in pixels, of an image used on a <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Size" /> value representing the size of the image, in pixels. The default is 16 x 16 pixels.</returns>
		/// <remarks>
		/// The size is applied only to the images of the items that have their <see cref="ToolStripItem.ImageScaling"/> property
		/// set to <see cref="ToolStripItemImageScaling.SizeToFit"/>.
		/// </remarks>
		/// <example>
		/// Displaying larger icons on a tool strip:
		/// <code><![CDATA[
		/// this.toolStrip1.ImageScalingSize = new Size(32, 32);
		/// this.buttonPrint.ImageScaling = ToolStripItemImageScaling.SizeToFit;
		/// ]]></code>
		/// </example>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripImageScalingSizeDescr")]
		public Size ImageScalingSize
		{
			get
			{
				return this._imageScalingSize;
			}
			set
			{
				if ((this._imageScalingSize != value))
				{
					this._imageScalingSize = value;
				}
			}
		}

		private Size _imageScalingSize;

		/// <summary>
		/// Returns or sets the image list that contains the images displayed on the <see cref="ToolStrip" /> items.
		/// </summary>
		/// <returns>An object of type <see cref="ImageList" />. The default is null.</returns>
		/// <remarks>
		/// The items select their image from this list using <see cref="ToolStripItem.ImageIndex"/> or <see cref="ToolStripItem.ImageKey"/>.
		/// </remarks>
		/// <example>
		/// Assigning images to the tool strip items by key:
		/// <code><![CDATA[
		/// this.toolStrip1.ImageList = this.imageList1;
		/// this.buttonNew.ImageKey = "new.png";
		/// this.buttonOpen.ImageKey = "open.png";
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripImageListDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				return this._imageList;
			}
			set
			{
				if ((this._imageList != value))
				{
					this._imageList = value;
				}
			}
		}

		private ImageList _imageList;

		/// <summary>
		/// Returns a value indicating whether the user is currently moving the <see cref="ToolStrip" /> from one <see cref="ToolStripContainer" /> to another.
		/// </summary>
		/// <returns>true if the user is currently moving the <see cref="ToolStrip" /> from one <see cref="ToolStripContainer" /> to another; otherwise, false.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public virtual bool IsCurrentlyDragging
		{
			get
			{
				return this._isCurrentlyDragging;
			}
		}

		private bool _isCurrentlyDragging;

		/// <summary>
		/// Returns all the items that belong to a <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>An object of type <see cref="ToolStripItemCollection" />, representing all the elements contained by a <see cref="ToolStrip" />.</returns>
		/// <remarks>
		/// Adding or removing items raises the <see cref="ItemAdded"/> and <see cref="ItemRemoved"/> events.
		/// </remarks>
		/// <example>
		/// Adding items to a tool strip and handling their clicks in one place:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.Add(new ToolStripButton("Save") { Name = "save" });
		/// this.toolStrip1.Items.Add(new ToolStripSeparator());
		/// this.toolStrip1.Items.Add(new ToolStripButton("Print") { Name = "print" });
		///
		/// this.toolStrip1.ItemClicked += (s, e) =>
		/// {
		///     AlertBox.Show("Clicked: " + e.ClickedItem.Name);
		/// };
		/// ]]></code>
		/// </example>
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ToolStripItemsDescr")]
		public virtual ToolStripItemCollection Items
		{
			get
			{
				return this._items;
			}
		}

		private ToolStripItemCollection _items;

		/// <summary>
		/// Returns a value indicating whether a <see cref="ToolStrip" /> is a <see cref="ToolStripDropDown" /> control.
		/// </summary>
		/// <returns>true if the <see cref="ToolStrip" /> is a <see cref="ToolStripDropDown" /> control; otherwise, false.</returns>
		[Browsable(false)]
		public bool IsDropDown
		{
			get
			{
				return this._isDropDown;
			}
		}
		private bool _isDropDown;

		/// <summary>
		/// Returns or sets a value indicating how the <see cref="ToolStrip" /> lays out the items collection.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripLayoutStyle" /> values: <see cref="ToolStripLayoutStyle.Table" />, <see cref="ToolStripLayoutStyle.Flow" />, <see cref="ToolStripLayoutStyle.StackWithOverflow" />, <see cref="ToolStripLayoutStyle.HorizontalStackWithOverflow" /> or <see cref="ToolStripLayoutStyle.VerticalStackWithOverflow" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="LayoutStyle" /> is not one of the <see cref="ToolStripLayoutStyle" /> values.</exception>
		/// <remarks>
		/// Changing the value raises the <see cref="LayoutStyleChanged"/> event. Only the stack layouts move items to the
		/// overflow menu when <see cref="CanOverflow"/> is true.
		/// </remarks>
		/// <example>
		/// Laying out the items vertically:
		/// <code><![CDATA[
		/// this.toolStrip1.Dock = DockStyle.Left;
		/// this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
		/// ]]></code>
		/// </example>
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripLayoutStyle")]
		public ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return this._layoutStyle;
			}
			set
			{
				if ((this._layoutStyle != value))
				{
					this._layoutStyle = value;
				}
			}
		}
		private ToolStripLayoutStyle _layoutStyle;

		/// <summary>
		/// Returns the <see cref="ToolStripItem" /> that is the overflow button for a <see cref="ToolStrip" /> with overflow enabled.
		/// </summary>
		/// <returns>An object of type <see cref="ToolStripOverflowButton" /> with its <see cref="ToolStripItem.Alignment" /> set to <see cref="ToolStripItemAlignment.Right" /> and its <see cref="ToolStripItem.Overflow" /> value set to <see cref="ToolStripItemOverflow.Never" />.</returns>
		/// <remarks>
		/// The button is displayed only when <see cref="CanOverflow"/> is true and at least one item doesn't fit in the <see cref="ToolStrip"/>.
		/// </remarks>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ToolStripOverflowButton OverflowButton
		{
			get
			{
				return this._overflowButton;
			}
		}

		private ToolStripOverflowButton _overflowButton;

		/// <summary>
		/// Returns the orientation of the <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>One of the <see cref="Orientation" /> values. The default is <see cref="Orientation.Horizontal" />.</returns>
		/// <remarks>
		/// The orientation is determined by the <see cref="LayoutStyle"/> and, for <see cref="ToolStripLayoutStyle.StackWithOverflow"/>, by the <see cref="Dock"/> value.
		/// </remarks>
		[Browsable(false)]
		public Orientation Orientation
		{
			get
			{
				return this._orientation;
			}
		}

		private Orientation _orientation;

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="ToolStrip" /> stretches from end to end in the <see cref="ToolStripContainer" />.
		/// </summary>
		/// <returns>true if the <see cref="ToolStrip" /> stretches from end to end in its <see cref="ToolStripContainer" />; otherwise, false. The default is false.</returns>
		[SRDescription("ToolStripStretchDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		public virtual bool Stretch
		{
			get
			{
				return this._stretch;
			}
			set
			{
				if ((this._stretch != value))
				{
					this._stretch = value;
				}
			}
		}

		private bool _stretch;

		/// <summary>
		/// Returns or sets a value indicating whether ToolTips are to be displayed on <see cref="ToolStrip" /> items.
		/// </summary>
		/// <returns>true if ToolTips are to be displayed; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public bool ShowItemToolTips
		{
			get
			{
				return this._showItemToolTips;
			}
			set
			{
				if ((this._showItemToolTips != value))
				{
					this._showItemToolTips = value;
				}
			}
		}
		private bool _showItemToolTips;

		/// <summary>
		/// Returns or sets the direction in which to draw text on a <see cref="ToolStrip" />.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripTextDirection" /> values. The default is <see cref="ToolStripTextDirection.Horizontal" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="ToolStripTextDirection" /> values.</exception>
		/// <remarks>
		/// Items that have their <see cref="ToolStripItem.TextDirection"/> set to <see cref="ToolStripTextDirection.Inherit"/> use this value.
		/// </remarks>
		/// <example>
		/// Rotating the text of a vertical tool strip:
		/// <code><![CDATA[
		/// this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
		/// this.toolStrip1.TextDirection = ToolStripTextDirection.Vertical90;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripTextDirectionDescr")]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[SRCategory("CatAppearance")]
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
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>An instance of the <see cref="VScrollProperties" /> class, which provides basic properties for a vertical scroll bar.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public VScrollProperties VerticalScroll
		{
			get
			{
				return this._verticalScroll;
			}
		}

		private VScrollProperties _verticalScroll;

		#endregion

		#region Methods

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStrip" /> and optionally releases the managed resources.
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the next <see cref="ToolStripItem" /> from the specified reference point and moving in the specified direction.
		/// </summary>
		/// <param name="start">The <see cref="ToolStripItem" /> that is the reference point from which to begin the retrieval of the next item.</param>
		/// <param name="direction">One of the values of <see cref="ArrowDirection" /> that specifies the direction to move.</param>
		/// <returns>A <see cref="ToolStripItem" /> that is specified by the <paramref name="start" /> parameter and is next in the order as specified by the <paramref name="direction" /> parameter.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified value of the <paramref name="direction" /> parameter is not one of the values of <see cref="ArrowDirection" />.</exception>
		/// <remarks>
		/// Use <see cref="ArrowDirection.Left"/> and <see cref="ArrowDirection.Right"/> for horizontal tool strips and
		/// <see cref="ArrowDirection.Up"/> and <see cref="ArrowDirection.Down"/> for vertical tool strips.
		/// </remarks>
		/// <example>
		/// Selecting the item to the right of the current button:
		/// <code><![CDATA[
		/// var next = this.toolStrip1.GetNextItem(this.buttonOpen, ArrowDirection.Right);
		/// if (next != null)
		///     next.Select();
		/// ]]></code>
		/// </example>
		public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
		{
			// TODO: Implement
			return new ToolStripItem();
		}

		/// <summary>
		/// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
		///</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="Keys" /> values.</param>
		protected override bool IsInputKey(Keys keyData)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Determines whether a character is an input character that the item recognizes.
		///</summary>
		/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test.</param>
		protected override bool IsInputChar(char charCode)
		{
			// TODO: Implement
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			// TODO: Implement
		}

		//TODO:ITG: Remove when reviewd

		//protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		//{
		//    // TODO: Implement
		//    return false;
		//}

		/// <summary>
		/// Processes a dialog box key.
		///</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="Keys" /> values that represents the key to process. </param>
		protected override bool ProcessDialogKey(Keys keyData)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Raises the <see cref="Control.HandleCreated" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.HandleDestroyed" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.Layout" /> event.
		///</summary>
		/// <param name="e">A <see cref="LayoutEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.LostFocus" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.Leave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseDown" /> event.
		///</summary>
		/// <param name="mea">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseMove" /> event.
		///</summary>
		/// <param name="mea">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseLeave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			// TODO: Implement
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseUp" /> event.
		///</summary>
		/// <param name="mea">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.RightToLeftChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.Paint" /> event for the <see cref="ToolStrip" /> background.
		///</summary>
		/// <param name="e">A <see cref="PaintEventArgs" /> that contains information about the control to paint. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaintBackground(PaintEventArgs e)
		{
			OnPaintBackground(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.VisibleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			// TODO: Implement
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			// TODO: Implement
		}

		/// <summary>
		/// Controls the return location of the focus.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RestoreFocus()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Enables you to change the parent <see cref="ToolStrip" /> of a <see cref="ToolStripItem" />.
		///</summary>
		/// <param name="item">The <see cref="ToolStripItem" /> whose <see cref="Control.Parent" /> property is to be changed. </param>
		/// <param name="parent">The <see cref="ToolStrip" /> that is the parent of the <see cref="ToolStripItem" /> referred to by the <paramref name="item" /> parameter. </param>
		protected void SetItemParent(ToolStripItem item, ToolStrip parent)
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant for this class.
		/// </summary>
		/// <param name="point">A <see cref="System.Drawing.Point" />.</param>
		/// <returns>A <see cref="Control" />.</returns>
		/// <remarks>
		/// Use <see cref="GetItemAt(Point)"/> to find the <see cref="ToolStripItem"/> at a location.
		/// </remarks>
		/// <example>
		/// Use <see cref="GetItemAt(Point)"/> instead:
		/// <code><![CDATA[
		/// var item = this.toolStrip1.GetItemAt(new Point(40, 10));
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control GetChildAtPoint(Point point)
		{
			// TODO: Implement
			return new Wisej.Web.Control();
		}

		/// <summary>
		/// This method is not relevant for this class.
		/// </summary>
		/// <param name="pt">A <see cref="System.Drawing.Point" /> value.</param>
		/// <param name="skipValue">A <see cref="GetChildAtPointSkip" /> value.</param>
		/// <returns>A <see cref="Control" />.</returns>
		/// <remarks>
		/// Use <see cref="GetItemAt(Point)"/> to find the <see cref="ToolStripItem"/> at a location.
		/// </remarks>
		/// <example>
		/// Use <see cref="GetItemAt(Point)"/> instead:
		/// <code><![CDATA[
		/// var item = this.toolStrip1.GetItemAt(new Point(40, 10));
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			// TODO: Implement
			return new Wisej.Web.Control();
		}

		/// <summary>
		/// Returns the item located at the specified x- and y-coordinates of the <see cref="ToolStrip" /> client area.
		/// </summary>
		/// <param name="x">The horizontal coordinate, in pixels, from the left edge of the client area.</param>
		/// <param name="y">The vertical coordinate, in pixels, from the top edge of the client area.</param>
		/// <returns>The <see cref="ToolStripItem" /> located at the specified location, or null if the <see cref="ToolStripItem" /> is not found.</returns>
		/// <example>
		/// Finding the item under the mouse pointer:
		/// <code><![CDATA[
		/// private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
		/// {
		///     var item = this.toolStrip1.GetItemAt(e.X, e.Y);
		///     if (item != null)
		///         AlertBox.Show(item.Text);
		/// }
		/// ]]></code>
		/// </example>
		public ToolStripItem GetItemAt(int x, int y)
		{
			// TODO: Implement
			// return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns the item located at the specified point in the client area of the <see cref="ToolStrip" />.
		/// </summary>
		/// <param name="point">The <see cref="System.Drawing.Point" /> at which to search for the <see cref="ToolStripItem" />.</param>
		/// <returns>The <see cref="ToolStripItem" /> at the specified location, or null if the <see cref="ToolStripItem" /> is not found.</returns>
		/// <example>
		/// Finding the item under the mouse pointer:
		/// <code><![CDATA[
		/// private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
		/// {
		///     var item = this.toolStrip1.GetItemAt(e.Location);
		///     if (item != null)
		///         AlertBox.Show(item.Text);
		/// }
		/// ]]></code>
		/// </example>
		public ToolStripItem GetItemAt(Point point)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// This method is not relevant for this class.
		/// </summary>
		/// <param name="x">The horizontal margin, in pixels.</param>
		/// <param name="y">The vertical margin, in pixels.</param>
		/// <remarks>
		/// Automatic scrolling is not supported by <see cref="ToolStrip"/> controls.
		/// </remarks>
		/// <example>
		/// The call has no effect on a <see cref="ToolStrip"/>:
		/// <code><![CDATA[
		/// this.toolStrip1.SetAutoScrollMargin(0, 0);
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetAutoScrollMargin(int x, int y)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resets the collection of displayed and overflow items after a layout is done.
		///</summary>
		protected virtual void SetDisplayedItems()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Returns a string that represents the <see cref="ToolStrip"/>.
		/// </summary>
		/// <returns>A string that represents the current <see cref="ToolStrip"/>.</returns>
		/// <example>
		/// Logging the tool strip:
		/// <code><![CDATA[
		/// System.Diagnostics.Debug.WriteLine(this.toolStrip1.ToString());
		/// ]]></code>
		/// </example>
		public override String ToString()
		{
			// TODO: Implement
			return "";
		}

		/// <summary>
		/// Returns whether the designer should draw the design-time border around the <see cref="ToolStrip"/>.
		/// </summary>
		/// <returns>true if the designer should draw the border; otherwise, false.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and is not meant to be called from application code.
		/// </remarks>
		/// <example>
		/// Checking the design-time border setting:
		/// <code><![CDATA[
		/// bool drawBorder = this.toolStrip1.ShouldDrawBorder();
		/// ]]></code>
		/// </example>
		public bool ShouldDrawBorder()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes Windows mouse messages forwarded by the designer.
		/// </summary>
		/// <param name="m">The <see cref="System.Windows.Forms.Message"/> forwarded by the designer.</param>
		/// <returns>true to prevent the base class from processing the message; otherwise, false.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and is not meant to be called from application code.
		/// </remarks>
		/// <example>
		/// Forwarding a message from a custom designer:
		/// <code><![CDATA[
		/// protected override void WndProc(ref System.Windows.Forms.Message m)
		/// {
		///     if (!this.toolStrip.DesignerWndProc(ref m))
		///         base.WndProc(ref m);
		/// }
		/// ]]></code>
		/// </example>
		public bool DesignerWndProc(ref System.Windows.Forms.Message m)
		{
			throw new NotImplementedException();
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

		#region IWisejDesignTarget2

		bool IWisejDesignTarget2.ShouldDrawBorder()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes Windows mouse messages forwarded by the designer.
		/// </summary>
		/// <param name="m">The <see cref="System.Windows.Forms.Message"/> forwarded by the designer.</param>
		/// <returns>Returns true to prevent the base class from processing the message.</returns>
		bool IWisejDesignTarget.DesignerWndProc(ref System.Windows.Forms.Message m)
		{
			throw new NotImplementedException();
		}

		// Represents the child item that is selected in the designer.
		private IWisejComponent DesignItem
		{
			get { return this.UserData.DesignItem; }
			set
			{
				if (this.DesignItem != value)
				{
					this.UserData.DesignItem = value;
					Update();
				}
			}
		}

		private void OnDesignComponentSelectionChanged(object server, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets the design-time metrics used by the designer to adapt the
		/// control on the screen to the HTML metrics used by the renderer.
		/// </summary>
		/// <param name="metrics">Design metrics from the renderer.</param>
		void IWisejControl.SetDesignMetrics(dynamic metrics)
		{
			throw new NotImplementedException();
		}

		// Finds the child component, at any level, with the specified id.
		private IWisejComponent FindDesignChildComponent(string id)
		{
			throw new NotImplementedException();
		}

		// Finds the child component, at any level, containing the specified coordinate.
		private IWisejComponent FindDesignChildComponent(Point location)
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}

