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
	/// Hosts a Wisej control, either a standard control such as a <see cref="DateTimePicker"/> or a custom control, as an item of a <see cref="ToolStrip" />.
	/// </summary>
	/// <remarks>
	/// Most appearance and behavior properties (<see cref="BackColor"/>, <see cref="ForeColor"/>, <see cref="Font"/>,
	/// <see cref="Enabled"/>, <see cref="Text"/>, <see cref="CausesValidation"/>, <see cref="RightToLeft"/>) are forwarded to the
	/// hosted <see cref="Control"/>. <see cref="ToolStripComboBox"/>, <see cref="ToolStripTextBox"/> and
	/// <see cref="ToolStripProgressBar"/> are specialized hosts for the most common controls.
	/// </remarks>
	public partial class ToolStripControlHost : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripControlHost" /> class that hosts the specified control.
		/// </summary>
		/// <param name="c">The <see cref="Control" /> hosted by this <see cref="ToolStripControlHost" /> class.</param>
		/// <exception cref="System.ArgumentNullException">The control referred to by the <paramref name="c" /> parameter is null.</exception>
		/// <example>
		/// Hosting a <see cref="DateTimePicker"/> in a tool strip:
		/// <code><![CDATA[
		/// var picker = new DateTimePicker
		/// {
		///     Width = 120
		/// };
		/// this.toolStrip1.Items.Add(new ToolStripControlHost(picker));
		/// ]]></code>
		/// </example>
		public ToolStripControlHost(Control c)
		{
			this._c = c;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripControlHost" /> class that hosts the specified control and that has the specified name.
		/// </summary>
		/// <param name="c">The <see cref="Control" /> hosted by this <see cref="ToolStripControlHost" /> class.</param>
		/// <param name="name">The name of the <see cref="ToolStripControlHost" />.</param>
		public ToolStripControlHost(Control c, string name)
		{
			this._c = c;
			this.Name = name;
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler DisplayStyleChanged;

		/// <summary>
		/// Occurs when the hosted control is entered.
		///</summary>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		public event EventHandler Enter;

		/// <summary>
		/// Occurs when the hosted control receives focus.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ToolStripItemOnGotFocusDescr")]
		[Browsable(false)]
		[SRCategory("CatFocus")]
		public event EventHandler GotFocus;

		/// <summary>
		/// Occurs when the input focus leaves the hosted control.
		///</summary>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		public event EventHandler Leave;

		/// <summary>
		/// Occurs when the hosted control loses focus.
		///</summary>
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LostFocus;

		/// <summary>
		/// Occurs when a key is pressed and held down while the hosted control has focus.
		///</summary>
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		public event KeyEventHandler KeyDown;

		/// <summary>
		/// Occurs when a key is pressed while the hosted control has focus.
		///</summary>
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		public event KeyPressEventHandler KeyPress;

		/// <summary>
		/// Occurs when a key is released while the hosted control has focus.
		///</summary>
		[SRDescription("ControlOnKeyUpDescr")]
		[SRCategory("CatKey")]
		public event KeyEventHandler KeyUp;

		/// <summary>
		/// Occurs while the hosted control is validating.
		///</summary>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		public event CancelEventHandler Validating;

		/// <summary>
		/// Occurs after the hosted control has been successfully validated.
		///</summary>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		public event EventHandler Validated;

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the background color of the hosted control.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Color" /> representing the background color of the hosted control.</returns>
		[SRDescription("ToolStripItemBackColorDescr")]
		[SRCategory("CatAppearance")]
		public override Color BackColor
		{
			get => Control.BackColor;
			set => Control.BackColor = value;
		}

		/// <summary>
		/// Returns or sets the background image displayed in the hosted control.
		/// </summary>
		/// <returns>An <see cref="System.Drawing.Image" /> that represents the image to display in the background of the hosted control.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public override Image BackgroundImage
		{
			get => Control.BackgroundImage;
			set => Control.BackgroundImage = value;
		}

		/// <summary>
		/// Returns or sets the background image layout of the hosted control.
		/// </summary>
		/// <returns>One of the <see cref="ImageLayout" /> values. The default is <see cref="Wisej.Web.ImageLayout.Tile" />.</returns>
		[Localizable(true)]
		[DefaultValue(ImageLayout.Tile)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public override ImageLayout BackgroundImageLayout
		{
			get => Control.BackgroundImageLayout;
			set => Control.BackgroundImageLayout = value;
		}

		/// <summary>
		/// Returns a value indicating whether the hosted control can be selected.
		/// </summary>
		/// <returns>true if the hosted control can be selected; otherwise, false.</returns>
		/// <remarks>
		/// Always returns true at design time, and false when there is no hosted control.
		/// </remarks>
		[Browsable(false)]
		public override bool CanSelect
		{
			get
			{
				if (_control is not null)
				{
					return (DesignMode || Control.CanSelect);
				}

				return false;
			}
		}

		/// <summary>
		/// Returns or sets a value indicating whether the hosted control causes and raises validation events on other controls when the hosted control receives focus.
		/// </summary>
		/// <returns>true if the hosted control causes and raises validation events on other controls when the hosted control receives focus; otherwise, false. The default is true.</returns>
		[SRCategory("CatFocus")]
		[DefaultValue(true)]
		[SRDescription("ControlCausesValidationDescr")]
		public bool CausesValidation
		{
			get => Control.CausesValidation;
			set => Control.CausesValidation = value;
		}

		/// <summary>
		/// Returns or sets the alignment of the hosted control within the bounds of the <see cref="ToolStripControlHost" />.
		/// </summary>
		/// <returns>One of the <see cref="System.Drawing.ContentAlignment" /> values. The default is <see cref="System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
		/// <remarks>
		/// Changing the value updates the bounds of the item.
		/// </remarks>
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Browsable(false)]
		public ContentAlignment ControlAlign
		{
			get
			{
				return this._controlAlign;
			}
			set
			{
				if (_controlAlign != value)
				{
					_controlAlign = value;
					OnBoundsChanged();
				}
			}
		}

		private ContentAlignment _controlAlign;

		/// <summary>
		/// Returns the <see cref="Control" /> that this <see cref="ToolStripControlHost" /> is hosting.
		/// </summary>
		/// <returns>The <see cref="Control" /> that this <see cref="ToolStripControlHost" /> is hosting.</returns>
		/// <remarks>
		/// The hosted control is disposed together with the <see cref="ToolStripControlHost"/>, after which this property returns null.
		/// </remarks>
		/// <example>
		/// Reading the value of a hosted <see cref="DateTimePicker"/>:
		/// <code><![CDATA[
		/// var picker = (DateTimePicker)this.toolStripControlHost1.Control;
		/// AlertBox.Show(picker.Value.ToShortDateString());
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		private Control _control;

		/// <summary>
		/// Returns the default size of the item.
		/// </summary>
		/// <returns>The default <see cref="System.Drawing.Size" /> of the item.</returns>
		/// <remarks>
		/// Returns the current size of the hosted control, since a control initializes its size to its own default size when
		/// it is created. When there is no hosted control, the default size of <see cref="ToolStripItem"/> is returned.
		/// </remarks>
		public override Size DefaultSize
		{
			get
			{
				if (Control is not null)
				{
					// When you create the control - it sets up its size as its default size.
					// Since the property is protected we don't know for sure, but this is a pretty good guess.
					return Control.Size;
				}

				return base.DefaultSize;
			}
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripItemDisplayStyle" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItemDisplayStyle DisplayStyle
		{
			get => base.DisplayStyle;
			set => base.DisplayStyle = value;
		}

		private ToolStripItemDisplayStyle _displayStyle;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if double clicking is enabled; otherwise, false. </returns>
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool DoubleClickEnabled
		{
			get => base.DoubleClickEnabled;
			set => base.DoubleClickEnabled = value;
		}

		/// <summary>
		/// Returns or sets the font to be used on the hosted control.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Font" /> for the hosted control.</returns>
		[SRDescription("ToolStripItemFontDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public override Font Font
		{
			get => Control.Font;
			set => Control.Font = value;
		}

		/// <summary>
		/// Returns or sets a value indicating whether the hosted control is enabled.
		/// </summary>
		/// <returns>true if the hosted control is enabled; otherwise, false. The default is true.</returns>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemEnabledDescr")]
		[DefaultValue(true)]
		public override bool Enabled
		{
			get => Control.Enabled;
			set => Control.Enabled = value;
		}

		/// <summary>
		/// Returns a value indicating whether the hosted control has input focus.
		/// </summary>
		/// <returns>true if the hosted control has input focus; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public virtual bool Focused
		{
			get
			{
				return Control.Focused;
			}
		}

		/// <summary>
		/// Returns or sets the foreground color of the hosted control.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Color" /> representing the foreground color of the hosted control.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemForeColorDescr")]
		public override Color ForeColor
		{
			get => Control.ForeColor;
			set => Control.ForeColor = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		public override Image Image
		{
			get => base.Image;
			set => base.Image = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripItemImageScaling" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItemImageScaling ImageScaling
		{
			get => base.ImageScaling;
			set => base.ImageScaling = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ImageTransparentColor
		{
			get => base.ImageTransparentColor;
			set => base.ImageTransparentColor = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.ContentAlignment" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public ContentAlignment ImageAlign
		{
			get => base.ImageAlign;
			set => base.ImageAlign = value;
		}

		private ContentAlignment _imageAlign;

		/// <summary>
		/// Returns or sets a value indicating whether the hosted control is drawn right-to-left, to support locales using right-to-left fonts.
		/// </summary>
		/// <returns>One of the <see cref="Wisej.Web.RightToLeft"/> values.</returns>
		/// <remarks>
		/// The value is read from and written to the hosted control. When there is no hosted control, the getter returns the value
		/// inherited from <see cref="ToolStripItem"/> and the setter has no effect.
		/// </remarks>
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftDescr")]
		public override RightToLeft RightToLeft
		{
			get
			{
				if (_control is not null)
				{
					return _control.RightToLeft;
				}

				return base.RightToLeft;
			}
			set
			{
				if (_control is not null)
				{
					_control.RightToLeft = value;
				}
			}
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if the image is mirrored; otherwise, false.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool RightToLeftAutoMirrorImage
		{
			get => base.RightToLeftAutoMirrorImage;
			set => base.RightToLeftAutoMirrorImage = value;
		}

		/// <summary>
		/// Returns a value indicating whether the item is selected.
		/// </summary>
		/// <returns>true if the hosted control has the focus; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Selected
		{
			get
			{
				return Control is not null && Control.Focused;
			}
		}

		/// <summary>
		/// Returns or sets the size of the <see cref="ToolStripItem" />.
		/// </summary>
		/// <returns>An ordered pair of type <see cref="System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemSizeDescr")]
		public override Size Size
		{
			get => base.Size;
			set => base.Size = value;
		}

		/// <summary>
		/// Returns or sets the site of the item.
		/// </summary>
		/// <returns>The <see cref="System.ComponentModel.ISite" /> associated with the item.</returns>
		/// <remarks>
		/// Setting a site also assigns to the hosted control an internal site that forwards the container, name, design mode and
		/// services to the site of the <see cref="ToolStripControlHost"/>; setting null removes the site from the hosted control.
		/// </remarks>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get => base.Site;
			set
			{
				base.Site = value;
				if (value is not null)
				{
					Control.Site = new StubSite(Control, this);
				}
				else
				{
					Control.Site = null;
				}
			}
		}

		private ISite _site;

		/// <summary>
		/// Returns or sets the text to be displayed on the hosted control.
		/// </summary>
		/// <returns>A <see cref="System.String" /> representing the text.</returns>
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextDescr")]
		public override string Text
		{
			get => Control.Text;
			set => Control.Text = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.ContentAlignment" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public ContentAlignment TextAlign
		{
			get => base.TextAlign;
			set => base.TextAlign = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripTextDirection" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public override ToolStripTextDirection TextDirection
		{
			get => base.TextDirection;
			set => base.TextDirection = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="TextImageRelation" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public TextImageRelation TextImageRelation
		{
			get => base.TextImageRelation;
			set => base.TextImageRelation = value;
		}

		private Control _c;

		#endregion

		#region Methods

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStripControlHost" /> and optionally releases the managed resources.
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing && Control != null)
			{
				OnUnsubscribeControlEvents(Control);

				// we only call control.Dispose if we are NOT being disposed in the finalizer.
				Control.Dispose();
				_control = null;
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DisplayStyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnDisplayStyleChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.DisplayStyleChanged != null))
			{
				DisplayStyleChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.Enter" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		protected virtual void OnEnter(System.EventArgs e)
		{
			if ((this.Enter != null))
			{
				Enter(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.GotFocus" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ToolStripItemOnGotFocusDescr")]
		[Browsable(false)]
		[SRCategory("CatFocus")]
		protected virtual void OnGotFocus(System.EventArgs e)
		{
			if ((this.GotFocus != null))
			{
				GotFocus(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.Leave" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		protected virtual void OnLeave(System.EventArgs e)
		{
			if ((this.Leave != null))
			{
				Leave(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.LostFocus" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLostFocus(System.EventArgs e)
		{
			if ((this.LostFocus != null))
			{
				LostFocus(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.KeyDown" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyEventArgs" /> that contains the event data.</param>
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		protected virtual void OnKeyDown(Wisej.Web.KeyEventArgs e)
		{
			if ((this.KeyDown != null))
			{
				KeyDown(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.KeyPress" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyPressEventArgs" /> that contains the event data.</param>
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		protected virtual void OnKeyPress(Wisej.Web.KeyPressEventArgs e)
		{
			if ((this.KeyPress != null))
			{
				KeyPress(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.KeyUp" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyEventArgs" /> that contains the event data.</param>
		[SRDescription("ControlOnKeyUpDescr")]
		[SRCategory("CatKey")]
		protected virtual void OnKeyUp(Wisej.Web.KeyEventArgs e)
		{
			if ((this.KeyUp != null))
			{
				KeyUp(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.Validating" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		protected virtual void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			if ((this.Validating != null))
			{
				Validating(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripControlHost.Validated" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		protected virtual void OnValidated(System.EventArgs e)
		{
			if ((this.Validated != null))
			{
				Validated(this, e);
			}
		}

		//ITG:TODO: Review

		///// <summary>
		///// Occurs when the <see cref="ToolStripItem.Bounds" /> property changes.
		/////</summary>
		//protected override void OnBoundsChanged()
		//{
		//	base.OnBoundsChanged();
		//	// TODO: Implement
		//}

		//ITG:TODO: Review

		//protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		//{
		//	base.OnParentChanged(oldParent, newParent);
		//	// TODO: Implement
		//}

		/// <summary>
		/// Synchronizes the resizing of the control host with the resizing of the hosted control.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnHostedControlResize(EventArgs e)
		{
			// TODO: Implement
		}

		protected override void SetVisibleCore(bool visible)
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		/// </summary>
		/// <example>
		/// Set the <see cref="BackColor"/> property to change the background color of the hosted control instead:
		/// <code><![CDATA[
		/// this.toolStripControlHost1.BackColor = Color.LightYellow;
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetBackColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// This method is not relevant to this class.
		/// </summary>
		/// <example>
		/// Set the <see cref="ForeColor"/> property to change the text color of the hosted control instead:
		/// <code><![CDATA[
		/// this.toolStripControlHost1.ForeColor = Color.DarkBlue;
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Gives the focus to the hosted control.
		/// </summary>
		/// <example>
		/// Moving the focus to a search box hosted in the tool strip when the page is shown:
		/// <code><![CDATA[
		/// private void Page1_Appear(object sender, EventArgs e)
		/// {
		///     this.toolStripTextBoxSearch.Focus();
		/// }
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Subscribes events from the hosted control.
		///</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		protected virtual void OnSubscribeControlEvents(Control control)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Unsubscribes events from the hosted control.
		///</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		protected virtual void OnUnsubscribeControlEvents(Control control)
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
