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
	/// Represents a control that displays a list of <see cref="ToolStripItem"/> objects in a popup, typically when the user
	/// clicks a <see cref="ToolStripDropDownButton" />, a <see cref="ToolStripMenuItem"/> or the <see cref="ToolStripOverflowButton"/>.
	/// </summary>
	/// <remarks>
	/// A <see cref="ToolStripDropDown"/> can also be displayed on its own using one of the <see cref="Show()"/> overloads
	/// and closed using <see cref="Close()"/>. The <see cref="Opening"/>, <see cref="Opened"/>, <see cref="Closing"/> and
	/// <see cref="Closed"/> events are raised when the drop down is shown and hidden.
	/// </remarks>
	public class ToolStripDropDown : ToolStrip
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDown" /> class.
		/// </summary>
		public ToolStripDropDown()
		{
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="Control.BackgroundImage" /> property changes.
		///</summary>
		[Browsable(false)]
		public event EventHandler BackgroundImageChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="Control.BackgroundImage" /> property changes.
		///</summary>
		[Browsable(false)]
		public event EventHandler BackgroundImageLayoutChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStrip.BindingContext" /> property changes.
		///</summary>
		[Browsable(false)]
		public event EventHandler BindingContextChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler ContextMenuChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler ContextMenuStripChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler DockChanged;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> is closed.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		public event ToolStripDropDownClosedEventHandler Closed;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> control is about to close.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosingDecr")]
		public event ToolStripDropDownClosingEventHandler Closing;

		/// <summary>
		/// Occurs when the focus enters the <see cref="ToolStripDropDown" />.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler Enter;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripDropDown.Font" /> property changes.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler FontChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStrip.ForeColor" /> property changes.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler ForeColorChanged;

		/// <summary>
		/// Occurs when the user requests help for a control.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event HelpEventHandler HelpRequested;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown.ImeModeChanged" /> property has changed.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler ImeModeChanged;

		/// <summary>
		/// Occurs when a key is pressed and held down while the <see cref="ToolStripDropDown" /> has focus.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event KeyEventHandler KeyDown;

		/// <summary>
		/// Occurs when a key is pressed while the <see cref="ToolStripDropDown" /> has focus.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event KeyPressEventHandler KeyPress;

		/// <summary>
		/// Occurs when a key is released while the control has focus.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		public event KeyEventHandler KeyUp;

		/// <summary>
		/// Occurs when the input focus leaves the control.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler Leave;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> control is opening.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		public event CancelEventHandler Opening;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> is opened.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpenedDescr")]
		public event EventHandler Opened;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripDropDown.Region" /> property changes.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler RegionChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event ScrollEventHandler Scroll;

		/// <summary>
		/// Occurs when the <see cref="ToolStripLayoutStyle" /> style changes.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler StyleChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TabStopChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TextChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[SRDescription("ControlOnTabIndexChangedDescr")]
		public event EventHandler TabIndexChanged;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Validated;

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event CancelEventHandler Validating;

		#endregion

		/// <summary>
		/// Raises the <see cref="Control.BackgroundImageChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
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
		[Browsable(false)]
		protected virtual void OnBackgroundImageLayoutChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.BackgroundImageLayoutChanged != null))
			{
				BackgroundImageLayoutChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.BindingContextChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		protected virtual void OnBindingContextChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.BindingContextChanged != null))
			{
				BindingContextChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ContextMenuChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnContextMenuChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ContextMenuChanged != null))
			{
				ContextMenuChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ContextMenuStripChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnContextMenuStripChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ContextMenuStripChanged != null))
			{
				ContextMenuStripChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.DockChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnDockChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.DockChanged != null))
			{
				DockChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.Closed" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripDropDownClosedEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
		{
			if ((this.Closed != null))
			{
				Closed(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.Closing" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripDropDownClosingEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosingDecr")]
		protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
		{
			if ((this.Closing != null))
			{
				Closing(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Enter" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnEnter(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.Enter != null))
			{
				Enter(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnFontChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.FontChanged != null))
			{
				FontChanged(this, e);
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnForeColorChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ForeColorChanged != null))
			{
				ForeColorChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.HelpRequested" /> event.
		///</summary>
		/// <param name="hevent">A <see cref="HelpEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnHelpRequested(Wisej.Web.HelpEventArgs hlpevent)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.HelpRequested != null))
			{
				HelpRequested(this, hlpevent);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.ImeModeChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnImeModeChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.ImeModeChanged != null))
			{
				ImeModeChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.KeyDown" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyPressEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnKeyDown(Wisej.Web.KeyPressEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.KeyDown != null))
			{
				//KeyDown(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.KeyPress" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyPressEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnKeyPress(Wisej.Web.KeyPressEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.KeyPress != null))
			{
				//KeyPress(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.KeyUp" /> event.
		///</summary>
		/// <param name="e">A <see cref="KeyPressEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		protected virtual void OnKeyUp(Wisej.Web.KeyPressEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.KeyUp != null))
			{
				//KeyUp(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Leave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnLeave(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.Leave != null))
			{
				Leave(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.Opening" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		protected virtual void OnOpening(System.ComponentModel.CancelEventArgs e)
		{
			if ((this.Opening != null))
			{
				Opening(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.Opened" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpenedDescr")]
		protected virtual void OnOpened(System.EventArgs e)
		{
			if ((this.Opened != null))
			{
				Opened(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.RegionChanged" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnRegionChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.RegionChanged != null))
			{
				RegionChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ScrollableControl.Scroll" /> event.
		///</summary>
		/// <param name="se">A <see cref="ScrollEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnScroll(Wisej.Web.ScrollEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.Scroll != null))
			{
				Scroll(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.StyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnStyleChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.StyleChanged != null))
			{
				StyleChanged(this, e);
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
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnTextChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TextChanged != null))
			{
				TextChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.TabIndexChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatPropertyChanged")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[SRDescription("ControlOnTabIndexChangedDescr")]
		protected virtual void OnTabIndexChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TabIndexChanged != null))
			{
				TabIndexChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Validated" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnValidated(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.Validated != null))
			{
				Validated(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Validating" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.Validating != null))
			{
				Validating(this, e);
			}
		}

		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>true to enable item reordering; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// Returns or sets a value indicating whether the <see cref="Opacity" /> of the <see cref="ToolStripDropDown"/> can be adjusted.
		/// </summary>
		/// <returns>true if the <see cref="Opacity" /> of the <see cref="ToolStripDropDown"/> can be adjusted; otherwise, false.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("ControlAllowTransparencyDescr")]
		public bool AllowTransparency
		{
			get
			{
				return this._allowTransparency;
			}
			set
			{
				if ((this._allowTransparency != value))
				{
					this._allowTransparency = value;
				}
			}
		}

		private bool _allowTransparency;

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A bitwise combination of the <see cref="AnchorStyles" /> values.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripDropDown" /> automatically adjusts its size to fit its items.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDown" /> control automatically resizes; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripDropDown" /> control should automatically close when it has lost activation.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDown" /> control automatically closes; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// When set to false, the drop down stays open when the user clicks outside of it or clicks one of its items,
		/// and it must be closed by calling <see cref="Close()"/>.
		/// </remarks>
		/// <example>
		/// Keeping a drop down open until the user presses a "Done" item:
		/// <code><![CDATA[
		/// this.dropDown1.AutoClose = false;
		/// this.buttonDone.Click += (s, e) => this.dropDown1.Close(ToolStripDropDownCloseReason.ItemClicked);
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripDropDownAutoCloseDescr")]
		public bool AutoClose
		{
			get
			{
				return this._autoClose;
			}
			set
			{
				if ((this._autoClose != value))
				{
					this._autoClose = value;
				}
			}
		}

		private bool _autoClose;

		/// <summary>
		/// Returns or sets a value indicating whether the items in a <see cref="ToolStripDropDown" /> can be sent to an overflow menu.
		/// </summary>
		/// <returns>true to send <see cref="ToolStripDropDown" /> items to an overflow menu; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// Returns a value indicating whether ToolTips are shown for the <see cref="ToolStripDropDown" /> by default.
		/// </summary>
		/// <returns>true if ToolTips are shown by default; otherwise, false.</returns>
		public override bool DefaultShowItemToolTips
		{
			get
			{
				return this._defaultShowItemToolTips;
			}
		}

		private bool _defaultShowItemToolTips;

		/// <summary>
		/// Returns the default docking location of the <see cref="ToolStripDropDown" />.
		/// </summary>
		/// <returns>One of the <see cref="DockStyle" /> values. A drop down is not docked, the default is <see cref="DockStyle.None"/>.</returns>
		public override DockStyle DefaultDock
		{
			get
			{
				return this._defaultDock;
			}
		}

		private DockStyle _defaultDock;

		/// <summary>
		/// Returns or sets the direction in which the child drop downs of this <see cref="ToolStripDropDown" /> are displayed relative to it.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripDropDownDirection" /> values.</returns>
		/// <example>
		/// Opening the sub menus of a drop down on the left side:
		/// <code><![CDATA[
		/// this.dropDown1.DefaultDropDownDirection = ToolStripDropDownDirection.Left;
		/// ]]></code>
		/// </example>
		[SRDescription("ToolStripDefaultDropDownDirectionDescr")]
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		public override ToolStripDropDownDirection DefaultDropDownDirection
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>One of the <see cref="DockStyle" /> values. The default is <see cref="DockStyle.None"/>.</returns>
		[DefaultValue(DockStyle.None)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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
		/// Returns or sets a value indicating whether a shadow effect appears when the <see cref="ToolStripDropDown" /> is displayed.
		/// </summary>
		/// <returns>true to enable the shadow effect; otherwise, false.</returns>
		public bool DropShadowEnabled
		{
			get
			{
				return this._dropShadowEnabled;
			}
			set
			{
				if ((this._dropShadowEnabled != value))
				{
					this._dropShadowEnabled = value;
				}
			}
		}

		private bool _dropShadowEnabled;

		/// <summary>
		/// Returns or sets the font of the text displayed on the <see cref="ToolStripDropDown" />.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripGripDisplayStyle" /> values.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return this._gripDisplayStyle;
			}
		}

		private ToolStripGripDisplayStyle _gripDisplayStyle;

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A <see cref="Padding" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripGripStyle" /> values. The default is <see cref="ToolStripGripStyle.Hidden"/>.</returns>
		[DefaultValue(ToolStripGripStyle.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// Returns a value indicating whether this <see cref="ToolStripDropDown" /> was automatically generated.
		/// </summary>
		/// <returns>true if this <see cref="ToolStripDropDown" /> is generated automatically; otherwise, false.</returns>
		/// <remarks>
		/// A drop down is generated automatically when the <see cref="ToolStripDropDownItem.DropDown"/> of an item is created
		/// on demand (for example when adding items to <see cref="ToolStripDropDownItem.DropDownItems"/>) instead of being assigned explicitly.
		/// </remarks>
		[Browsable(false)]
		public bool IsAutoGenerated
		{
			get
			{
				return this._isAutoGenerated;
			}
		}

		private bool _isAutoGenerated;

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Point" />.</returns>
		/// <remarks>
		/// Use one of the <see cref="Show()"/> overloads to position the <see cref="ToolStripDropDown"/>.
		/// </remarks>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return this._location;
			}
			set
			{
				if ((this._location != value))
				{
					this._location = value;
				}
			}
		}

		private Point _location;

		/// <summary>
		/// Returns or sets the opacity of the <see cref="ToolStripDropDown"/>.
		/// </summary>
		/// <returns>The level of opacity, from 0.0 (fully transparent) to 1.0 (fully opaque). The default is 1.0.</returns>
		/// <remarks>
		/// The value is applied only when <see cref="AllowTransparency"/> is true.
		/// </remarks>
		/// <example>
		/// Displaying a semi-transparent drop down:
		/// <code><![CDATA[
		/// this.dropDown1.AllowTransparency = true;
		/// this.dropDown1.Opacity = 0.85;
		/// ]]></code>
		/// </example>
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormOpacityDescr")]
		[DefaultValue(1D)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public double Opacity
		{
			get
			{
				return this._opacity;
			}
			set
			{
				if ((this._opacity != value))
				{
					this._opacity = value;
				}
			}
		}

		private double _opacity;

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>A <see cref="ToolStripOverflowButton" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripOverflowButton OverflowButton
		{
			get
			{
				return this._overflowButton;
			}
		}

		private ToolStripOverflowButton _overflowButton;

		/// <summary>
		/// Returns or sets the <see cref="ToolStripItem" /> that is the owner of this <see cref="ToolStripDropDown" />.
		/// </summary>
		/// <returns>The <see cref="ToolStripItem" /> that is the owner of this <see cref="ToolStripDropDown" />. The default is null.</returns>
		/// <remarks>
		/// The owner item is the item (usually a <see cref="ToolStripDropDownItem"/>) that opens this drop down.
		/// </remarks>
		[Browsable(false)]
		[DefaultValue(null)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this._ownerItem;
			}
			set
			{
				if ((this._ownerItem != value))
				{
					this._ownerItem = value;
				}
			}
		}

		private ToolStripItem _ownerItem;

		/// <summary>
		/// Returns or sets the window region associated with the <see cref="ToolStripDropDown" />.
		/// </summary>
		/// <returns>The window <see cref="System.Drawing.Region" /> associated with the control.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public Region Region
		{
			get
			{
				return this._region;
			}
			set
			{
				if ((this._region != value))
				{
					this._region = value;
				}
			}
		}

		private Region _region;

		/// <summary>
		/// Returns or sets a value indicating whether the items of the <see cref="ToolStripDropDown"/> are aligned to support locales using right-to-left fonts.
		/// </summary>
		/// <returns>One of the <see cref="RightToLeft"/> values.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlRightToLeftDescr")]
		public override RightToLeft RightToLeft
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
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>true to enable stretching; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Stretch
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
		/// Returns or sets the direction in which to draw the text on the items of the <see cref="ToolStripDropDown"/>.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripTextDirection" /> values. The default is <see cref="ToolStripTextDirection.Horizontal" />.</returns>
		[Browsable(false)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public override ToolStripTextDirection TextDirection
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
		/// Returns a value indicating whether the <see cref="ToolStripDropDown"/> is displayed on top of all the other windows.
		/// </summary>
		/// <returns>true in all cases.</returns>
		public virtual bool TopMost
		{
			get
			{
				return this._topMost;
			}
		}

		private bool _topMost;

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="ToolStripDropDown" /> is a top-level control.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDown" /> is a top-level control; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TopLevel
		{
			get
			{
				return this._topLevel;
			}
			set
			{
				if ((this._topLevel != value))
				{
					this._topLevel = value;
				}
			}
		}

		private bool _topLevel;

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <returns>An <see cref="System.Int32" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripDropDown" /> is visible or hidden.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripDropDown" /> is visible; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// Use the <see cref="Show()"/> overloads to display the drop down at a specific location and <see cref="Close()"/> to hide it.
		/// </remarks>
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ControlVisibleDescr")]
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Visible
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

		#endregion

		#region Methods

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStripDropDown" /> and optionally releases the managed resources. 
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Applies various layout options to the <see cref="ToolStripDropDown" />.
		///</summary>
		/// <returns>The <see cref="LayoutSettings" /> for this <see cref="ToolStripDropDown" />.</returns>
		/// <param name="style">One of the <see cref="ToolStripLayoutStyle" /> values. The possibilities are <see cref="ToolStripLayoutStyle.Flow" />, <see cref="ToolStripLayoutStyle.HorizontalStackWithOverflow" />, <see cref="ToolStripLayoutStyle.StackWithOverflow" />, <see cref="ToolStripLayoutStyle.Table" />, and <see cref="ToolStripLayoutStyle.VerticalStackWithOverflow" />.</param>
		protected override LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle style)
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void CreateHandle()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.HandleCreated" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStrip.ItemClicked" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			base.OnItemClicked(e);
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
		/// Raises the <see cref="ToolStripItem.VisibleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.ParentChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseUp" /> event.
		///</summary>
		/// <param name="mea">A <see cref="MouseEventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			// TODO: Implement
		}

		/// <summary>
		/// Processes a dialog box key.
		///</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="Keys" /> values that represents the key to process.</param>
		protected override bool ProcessDialogKey(Keys keyData)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Processes a dialog box character.
		///</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override bool ProcessDialogChar(char charCode)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Scales a control's location, size, padding and margin.
		///</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A value that specifies the bounds of the control to use when defining its size and position.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Performs the work of setting the specified bounds of this control.
		///</summary>
		/// <param name="x">The new <see cref="Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="BoundsSpecified" /> values. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Adjusts the size of the owner <see cref="ToolStrip" /> to accommodate the <see cref="ToolStripDropDown" /> if the owner <see cref="ToolStrip" /> is currently displayed, or clears and resets active <see cref="ToolStripDropDown" /> child controls of the <see cref="ToolStrip" /> if the <see cref="ToolStrip" /> is not currently displayed.
		///</summary>
		/// <param name="visible">true if the owner <see cref="ToolStrip" /> is currently displayed; otherwise, false. </param>
		protected override void SetVisibleCore(bool visible)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Closes the <see cref="ToolStripDropDown" /> control.
		/// </summary>
		/// <remarks>
		/// The <see cref="Closing"/> and <see cref="Closed"/> events are raised with the <see cref="ToolStripDropDownCloseReason.CloseCalled"/> reason.
		/// </remarks>
		/// <example>
		/// Closing a drop down from code:
		/// <code><![CDATA[
		/// private void buttonCancel_Click(object sender, EventArgs e)
		/// {
		///     this.dropDown1.Close();
		/// }
		/// ]]></code>
		/// </example>
		public void Close()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Closes the <see cref="ToolStripDropDown" /> control for the specified reason.
		/// </summary>
		/// <param name="reason">One of the <see cref="ToolStripDropDownCloseReason" /> values.</param>
		/// <remarks>
		/// The <paramref name="reason"/> is passed to the <see cref="Closing"/> and <see cref="Closed"/> event handlers.
		/// </remarks>
		/// <example>
		/// Closing the drop down as if one of its items had been clicked:
		/// <code><![CDATA[
		/// this.dropDown1.Close(ToolStripDropDownCloseReason.ItemClicked);
		/// ]]></code>
		/// </example>
		public void Close(ToolStripDropDownCloseReason reason)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Displays the <see cref="ToolStripDropDown" /> control in its default position.
		/// </summary>
		/// <example>
		/// Showing a drop down:
		/// <code><![CDATA[
		/// this.dropDown1.Show();
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Show()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> relative to the specified control location.
		/// </summary>
		/// <param name="control">The control (typically, a <see cref="ToolStripDropDownButton" />) that is the reference point for the <see cref="ToolStripDropDown" /> position.</param>
		/// <param name="position">The horizontal and vertical location, in pixels, relative to the upper-left corner of <paramref name="control"/>.</param>
		/// <exception cref="System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <example>
		/// Showing a drop down below a button:
		/// <code><![CDATA[
		/// private void button1_Click(object sender, EventArgs e)
		/// {
		///     var dropDown = new ToolStripDropDown();
		///     dropDown.Items.Add(new ToolStripButton("Copy"));
		///     dropDown.Items.Add(new ToolStripButton("Paste"));
		///     dropDown.Show(this.button1, new Point(0, this.button1.Height));
		/// }
		/// ]]></code>
		/// </example>
		public void Show(Control control, Point position)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> relative to the specified control at the specified location and with the specified direction relative to the parent control.
		/// </summary>
		/// <param name="control">The control (typically, a <see cref="ToolStripDropDownButton" />) that is the reference point for the <see cref="ToolStripDropDown" /> position.</param>
		/// <param name="position">The horizontal and vertical location, in pixels, relative to the upper-left corner of <paramref name="control"/>.</param>
		/// <param name="direction">One of the <see cref="ToolStripDropDownDirection" /> values.</param>
		/// <exception cref="System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <example>
		/// Showing a drop down above a button:
		/// <code><![CDATA[
		/// this.dropDown1.Show(this.button1, new Point(0, 0), ToolStripDropDownDirection.AboveRight);
		/// ]]></code>
		/// </example>
		public void Show(Control control, Point position, ToolStripDropDownDirection direction)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> relative to the specified control at the specified coordinates.
		/// </summary>
		/// <param name="control">The control (typically, a <see cref="ToolStripDropDownButton" />) that is the reference point for the <see cref="ToolStripDropDown" /> position.</param>
		/// <param name="x">The horizontal coordinate, in pixels, relative to the left edge of <paramref name="control"/>.</param>
		/// <param name="y">The vertical coordinate, in pixels, relative to the top edge of <paramref name="control"/>.</param>
		/// <exception cref="System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <example>
		/// Showing a drop down at the mouse position inside a panel:
		/// <code><![CDATA[
		/// private void panel1_MouseUp(object sender, MouseEventArgs e)
		/// {
		///     if (e.Button == MouseButtons.Right)
		///         this.dropDown1.Show(this.panel1, e.X, e.Y);
		/// }
		/// ]]></code>
		/// </example>
		public void Show(Control control, int x, int y)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> at the specified screen location.
		/// </summary>
		/// <param name="screenLocation">The horizontal and vertical location of the upper-left corner of the <see cref="ToolStripDropDown"/>, in screen coordinates (pixels).</param>
		/// <example>
		/// Showing a drop down at a fixed position on the screen:
		/// <code><![CDATA[
		/// this.dropDown1.Show(new Point(200, 150));
		/// ]]></code>
		/// </example>
		public void Show(Point screenLocation)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> at the specified screen location and with the specified direction.
		/// </summary>
		/// <param name="position">The horizontal and vertical location, in screen coordinates (pixels), used as the reference point.</param>
		/// <param name="direction">One of the <see cref="ToolStripDropDownDirection" /> values.</param>
		/// <example>
		/// Showing a drop down that opens upward from a point near the bottom of the page:
		/// <code><![CDATA[
		/// this.dropDown1.Show(new Point(20, 600), ToolStripDropDownDirection.AboveRight);
		/// ]]></code>
		/// </example>
		public void Show(Point position, ToolStripDropDownDirection direction)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Positions the <see cref="ToolStripDropDown" /> at the specified screen coordinates.
		/// </summary>
		/// <param name="x">The horizontal screen coordinate, in pixels.</param>
		/// <param name="y">The vertical screen coordinate, in pixels.</param>
		/// <example>
		/// Showing a drop down at a fixed position on the screen:
		/// <code><![CDATA[
		/// this.dropDown1.Show(200, 150);
		/// ]]></code>
		/// </example>
		public void Show(int x, int y)
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
