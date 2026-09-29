///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// The RibbonBar organizes the features of an application into a series of tabs.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The RibbonBar is a command bar that organizes the features of an application into a series of tabs at the top of the application main page or window.
	/// </para><para>
	/// The RibbonBar user interface (UI) increases discoverability of features and functions, enables 
	/// quicker learning of the application, and makes users feel more in control of their experience with the application.
	/// </para><para>
	/// The RibbonBar replaces the traditional menu bar and toolbars.
	/// </para><para>
	/// The content is organized in a hierarchy: the <see cref="Pages"/> collection contains <see cref="RibbonBarPage"/> tabs,
	/// each page contains <see cref="RibbonBarGroup"/> panels (<see cref="RibbonBarPage.Groups"/>), and each group contains
	/// <see cref="RibbonBarItem"/> components (<see cref="RibbonBarGroup.Items"/>) such as <see cref="RibbonBarItemButton"/>,
	/// <see cref="RibbonBarItemCheckBox"/> or <see cref="RibbonBarItemComboBox"/>.
	/// </para><para>
	/// The events fired by the child components are also routed to the RibbonBar (<see cref="ItemClick"/>,
	/// <see cref="MenuButtonItemClick"/>, <see cref="GroupClick"/>, <see cref="ItemValueChanged"/>), allowing
	/// the application to handle all the ribbon commands in a single handler.
	/// </para>
	/// </remarks>
	/// <example>
	/// Building a simple ribbon in code and handling all the item clicks in one place:
	/// <code><![CDATA[
	/// var ribbonBar = new RibbonBar();
	///
	/// var home = new RibbonBarPage { Text = "&Home" };
	/// var clipboard = new RibbonBarGroup { Text = "Clipboard" };
	/// clipboard.Items.Add(new RibbonBarItemButton { Name = "paste", Text = "Paste", ImageSource = "icon-paste" });
	/// clipboard.Items.Add(new RibbonBarItemButton { Name = "cut", Text = "Cut", Orientation = Orientation.Horizontal });
	/// clipboard.Items.Add(new RibbonBarItemButton { Name = "copy", Text = "Copy", Orientation = Orientation.Horizontal });
	/// home.Groups.Add(clipboard);
	/// ribbonBar.Pages.Add(home);
	///
	/// ribbonBar.ItemClick += (s, e) =>
	/// {
	///     AlertBox.Show("Clicked: " + e.Item.Name);
	/// };
	///
	/// this.Controls.Add(ribbonBar);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[DefaultEvent("Load")]
	[ApiCategory("RibbonBar")]
	[ToolboxBitmap(typeof(RibbonBar))]
	[Description("The RibbonBar organizes the features of an application into a series of tabs.")]
	public class RibbonBar : Control, IWisejControl, IWisejDesignTarget
	{
		// autosize height
		private int _requestedHeight;

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonBar"/> control.
		/// </summary>
		/// <remarks>
		/// The new control is docked to the top (<see cref="DockStyle.Top"/>), has <see cref="AutoSize"/> set to true,
		/// is not focusable and doesn't cause validation (<see cref="CausesValidation"/> is false).
		/// </remarks>
		public RibbonBar()
		{
			base.AutoSize = true;
			base.Focusable = false;
			this.Dock = DockStyle.Top;
			base.CausesValidation = false;
		}

		#endregion

		#region Events

		#region Not Relevant

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add { base.ContextMenuChanged += value; }
			remove { base.ContextMenuChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add { base.BackgroundImageChanged += value; }
			remove { base.BackgroundImageChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add { base.BackgroundImageLayoutChanged += value; }
			remove { base.BackgroundImageLayoutChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add { base.ImeModeChanged += value; }
			remove { base.ImeModeChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add { base.TextChanged += value; }
			remove { base.TextChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add { base.TabIndexChanged += value; }
			remove { base.TabIndexChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add { base.TabStopChanged += value; }
			remove { base.TabStopChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add { base.Enter += value; }
			remove { base.Enter -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add { base.Leave += value; }
			remove { base.Leave -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add { base.Validated += value; }
			remove { base.Validated -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add { base.Validating += value; }
			remove { base.Validating -= value; }
		}

		/// <summary>
		/// This event is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add { base.ControlAdded += value; }
			remove { base.ControlAdded -= value; }
		}

		/// <summary>
		/// This event is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlRemoved
		{
			add { base.ControlRemoved += value; }
			remove { base.ControlRemoved -= value; }
		}

		/// <summary>
		/// This event is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add { base.PaddingChanged += value; }
			remove { base.PaddingChanged -= value; }
		}

		#endregion

		/// <summary>
		/// Fired when the <see cref="AppButton"/> is clicked.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the ApplicationButton is clicked.")]
		public event EventHandler AppButtonClick
		{
			add { base.AddHandler(nameof(AppButtonClick), value); }
			remove { base.RemoveHandler(nameof(AppButtonClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="AppButtonClick" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		protected internal virtual void OnAppButtonClick(EventArgs e)
		{
			((EventHandler)base.Events[nameof(AppButtonClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the value of the <see cref="SelectedPage"/> property changes.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the value of the SelectedPage property changes.")]
		public event EventHandler SelectedPageChanged
		{
			add { base.AddHandler(nameof(SelectedPageChanged), value); }
			remove { base.RemoveHandler(nameof(SelectedPageChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="SelectedPageChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		protected virtual void OnSelectedPageChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(SelectedPageChanged)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user clicks on a <see cref="RibbonBarItem"/>.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the user clicks on a RibbonBarItem.")]
		public event RibbonBarItemEventHandler ItemClick
		{
			add { base.AddHandler(nameof(ItemClick), value); }
			remove { base.RemoveHandler(nameof(ItemClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="ItemClick"/> event.
		/// </summary>
		/// <param name="e">A <see cref="RibbonBarItemEventArgs"/> that contains the event data.</param>
		protected internal virtual void OnItemClick(RibbonBarItemEventArgs e)
		{
			Debug.Assert(e.Item != null);

			if (this.CausesValidation && !ValidateActiveControl())
				return;

			// dispatch to the child item as well.
			e.Item?.OnClick(EventArgs.Empty);

			((RibbonBarItemEventHandler)base.Events[nameof(ItemClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user clicks one of the drop down menu items
		/// of a 
		/// <see cref="RibbonBarItemButton"/> or 
		/// <see cref="RibbonBarItemSplitButton"/>.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the user clicks one of the drop down menu items.")]
		public event RibbonBarMenuItemEventHandler MenuButtonItemClick
		{
			add { base.AddHandler(nameof(MenuButtonItemClick), value); }
			remove { base.RemoveHandler(nameof(MenuButtonItemClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="MenuButtonItemClick" /> event.
		/// </summary>
		/// <param name="e">A <see cref="MenuButtonItemClickedEventArgs" /> that contains the event data. </param>
		protected internal virtual void OnMenuButtonItemClick(RibbonBarMenuItemEventArgs e)
		{
			if (this.CausesValidation && !ValidateActiveControl())
				return;

			// dispatch to the child item as well.
			e.Item?.OnItemClick(e);

			((RibbonBarMenuItemEventHandler)base.Events[nameof(MenuButtonItemClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user clicks on a <see cref="RibbonBarGroup"/> button.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the user clicks on a RibbonBarGroup button.")]
		public event RibbonBarGroupEventHandler GroupClick
		{
			add { base.AddHandler(nameof(GroupClick), value); }
			remove { base.RemoveHandler(nameof(GroupClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="GroupClick"/> event.
		/// </summary>
		/// <param name="e">A <see cref="RibbonBarGroupEventArgs"/> that contains the event data.</param>
		protected internal virtual void OnGroupClick(RibbonBarGroupEventArgs e)
		{
			Debug.Assert(e.Group != null);

			// dispatch to the child item as well.
			e.Group?.OnClick(EventArgs.Empty);

			((RibbonBarGroupEventHandler)base.Events[nameof(GroupClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user changes the value of a <see cref="RibbonBarItem"/>.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the user changes the value of a RibbonBarItem.")]
		public event RibbonBarItemEventHandler ItemValueChanged
		{
			add { base.AddHandler(nameof(ItemValueChanged), value); }
			remove { base.RemoveHandler(nameof(ItemValueChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="ItemValueChanged"/> event.
		/// </summary>
		/// <param name="e">A <see cref="RibbonBarItemEventArgs"/> that contains the event data.</param>
		protected internal virtual void OnItemValueChanged(RibbonBarItemEventArgs e)
		{
			Debug.Assert(e.Item != null);

			((RibbonBarItemEventHandler)base.Events[nameof(ItemValueChanged)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when a <see cref="Wisej.Web.ComponentTool" /> is clicked.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolClickDescr")]
		public event ToolClickEventHandler ToolClick
		{
			add { base.AddHandler(nameof(ToolClick), value); }
			remove { base.RemoveHandler(nameof(ToolClick), value); }
		}

		/// <summary>
		/// Fires the ToolClick event.
		///</summary>
		/// <param name="e">A <see cref="T:Wisej.Web.ToolClickEventArgs" /> that contains the event data. </param>
		[SRCategory("CatBehavior")]
		[SRDescription("ToolClickDescr")]
		protected virtual void OnToolClick(ToolClickEventArgs e)
		{
			((ToolClickEventHandler)base.Events[nameof(ToolClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the property <see cref="CompactView"/> changes value.
		///</summary>
		[SRCategory("CatBehavior")]
		[Description("Fired when the property CompactView changes value.")]
		public event EventHandler CompactViewChanged
		{
			add { base.AddHandler(nameof(CompactViewChanged), value); }
			remove { base.RemoveHandler(nameof(CompactViewChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="CompactViewChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		protected virtual void OnCompactViewChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(CompactViewChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		#region Not Relevant

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get { return Padding.Empty; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string BackgroundImageSource
		{
			get { return base.BackgroundImageSource; }
			set { base.BackgroundImageSource = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>An <see cref="T:Wisej.Web.ImageLayout" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool TabStop
		{
			get { return base.TabStop; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Focusable
		{
			get { return base.Focusable; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int TabIndex
		{
			get { return base.TabIndex; }
			set { base.TabIndex = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ContextMenu ContextMenu
		{
			get { return null; }
			set { }
		}

		#endregion

		/// <summary>
		/// Returns or sets a value that indicates whether the control resizes based on its contents.
		/// </summary>
		/// <returns>true if the control automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// When true, the height of the <see cref="RibbonBar"/> is measured on the client (it depends on the theme and on the
		/// content of the pages) and sent back to the server, see <see cref="GetPreferredSize"/>. The measured height is
		/// limited by <see cref="Control.MinimumSize"/> and <see cref="Control.MaximumSize"/>.
		/// The height is not adjusted when the control is docked to the left, right or fill, or when it's anchored to both
		/// the top and the bottom.
		/// </remarks>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}

		/// <summary>
		/// Returns or sets whether clicking RibbonBar items causes validation to be performed on
		/// the active control.
		/// </summary>
		/// <returns>true if clicking RibbonBar items causes validation to be performed 
		/// on the active control; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// When true, the active control is validated before the <see cref="ItemClick"/> and <see cref="MenuButtonItemClick"/>
		/// events (and the corresponding events on the items) are fired. If the validation is cancelled, the events are not fired.
		/// The <see cref="GroupClick"/> and <see cref="ItemValueChanged"/> events are not affected. Note that the
		/// <see cref="RibbonBarItemCheckBox.Checked"/> and <see cref="RibbonBarItemRadioButton.Checked"/> values are
		/// updated before the validation takes place.
		/// </remarks>
		/// <example>
		/// Validating the field being edited before executing a ribbon command:
		/// <code><![CDATA[
		/// this.ribbonBar1.CausesValidation = true;
		///
		/// private void textBoxAmount_Validating(object sender, CancelEventArgs e)
		/// {
		///     if (!decimal.TryParse(this.textBoxAmount.Text, out _))
		///     {
		///         AlertBox.Show("Please enter a valid amount.");
		///         e.Cancel = true;
		///     }
		/// }
		///
		/// // not called when textBoxAmount fails validation.
		/// private void ribbonBar1_ItemClick(object sender, RibbonBarItemEventArgs e)
		/// {
		///     if (e.Item.Name == "save")
		///         SaveDocument();
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets whether clicking RibbonBar items causes validation to be performed on the active control.")]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}

		/// <returns>
		/// The default <see cref="T:System.Drawing.Size" /> of the control.
		/// </returns>
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, GetRibbonBarThemeHeight());
			}
		}

		///// <summary>
		///// Enables the overflow handling which automatically removes
		///// <see cref="RibbonBarGroup"/> panels
		///// that don't fit in the <see cref="RibbonBar"/>
		///// and adds them to a drop-down menu button.
		///// </summary>
		///// <returns>true if the <see cref="RibbonBar"/> should automatically handle overflowing; otherwise, false. The default is true.</returns>
		//[DefaultValue(true)]
		//[SRDescription("Enables the overflow handling which automatically removes RibbonBarGroup panels that don't fit the RibbonBar.")]
		//public bool AutoOverflow
		//{
		//	get { return this._autoOverflow; }
		//	set
		//	{
		//		if (this._autoOverflow != value)
		//		{
		//			this._autoOverflow = value;

		//			Update();
		//		}
		//	}
		//}
		//private bool _autoOverflow = true;

		/// <summary>
		/// Returns the application button displayed before the first
		/// <see cref="RibbonBarPage"/>.
		/// </summary>
		/// <remarks>
		/// The <see cref="RibbonBarAppButton"/> instance is always available and created on first use, but it's hidden
		/// by default: set <see cref="RibbonBarAppButton.Visible"/> to true to show it. Clicking the button fires the
		/// <see cref="AppButtonClick"/> event.
		/// </remarks>
		/// <example>
		/// Showing the application button and opening a menu page when it's clicked:
		/// <code><![CDATA[
		/// this.ribbonBar1.AppButton.Text = "File";
		/// this.ribbonBar1.AppButton.BackColor = Color.SteelBlue;
		/// this.ribbonBar1.AppButton.ForeColor = Color.White;
		/// this.ribbonBar1.AppButton.Visible = true;
		///
		/// this.ribbonBar1.AppButtonClick += (s, e) =>
		/// {
		///     new FileMenuDialog().ShowDialog();
		/// };
		/// ]]></code>
		/// </example>
		[SRCategory("CatBehavior")]
		[Description("Represents the application button displayed before the first RibbonBarPage.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RibbonBarAppButton AppButton
		{
			get { return 
					this._appButton = 
					this._appButton ?? new RibbonBarAppButton(this); }
		}
		private RibbonBarAppButton _appButton;

		/// <summary>
		/// Returns the instance of <see cref="Wisej.Web.ComponentToolCollection"/> associated with this control.
		/// </summary>
		/// <remarks>
		/// The tools are displayed as small icon buttons in the tab strip, next to the <see cref="RibbonBarPage"/> tabs.
		/// Clicking a tool fires the <see cref="ToolClick"/> event.
		/// </remarks>
		/// <example>
		/// Adding a help tool to the tab strip:
		/// <code><![CDATA[
		/// this.ribbonBar1.Tools.Add(new ComponentTool
		/// {
		///     Name = "help",
		///     ImageSource = "icon-help",
		///     ToolTipText = "Help"
		/// });
		///
		/// this.ribbonBar1.ToolClick += (s, e) =>
		/// {
		///     if (e.Tool.Name == "help")
		///         Application.Navigate("https://docs.wisej.com", "_blank");
		/// };
		/// ]]></code>
		/// </example>
		[Browsable(true)]
		[MergableProperty(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ComponentToolCollection Tools
		{
			get
			{
				return
					this._tools =
					this._tools ?? new ComponentToolCollection(this);
			}
		}
		private ComponentToolCollection _tools;

		/// <summary>
		/// Returns or sets the currently active <see cref="RibbonBarPage"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException">The value is null.</exception>
		/// <exception cref="ArgumentException">The <see cref="RibbonBarPage"/> doesn't belong to this <see cref="RibbonBar"/>.</exception>
		/// <remarks>
		/// When no page has been selected yet, the getter returns the first visible page in <see cref="Pages"/>, or null
		/// when there are no visible pages. Changing the value, either in code or when the user clicks a tab,
		/// fires the <see cref="SelectedPageChanged"/> event. You can also use <see cref="RibbonBarPage.Selected"/>.
		/// </remarks>
		/// <example>
		/// Switching to a contextual page when the user selects a picture:
		/// <code><![CDATA[
		/// private void pictureBox1_Click(object sender, EventArgs e)
		/// {
		///     var page = this.ribbonBar1.Pages["pictureTools"];
		///     page.Visible = true;
		///     this.ribbonBar1.SelectedPage = page;
		/// }
		///
		/// private void ribbonBar1_SelectedPageChanged(object sender, EventArgs e)
		/// {
		///     this.labelStatus.Text = "Page: " + this.ribbonBar1.SelectedPage?.Text;
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the currently active RibbonBarPage.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RibbonBarPage SelectedPage
		{
			get
			{

				if (this._selectedPage == null)
				{
					if (this.Pages.Count > 0)
						this._selectedPage = this.Pages.FirstOrDefault(p => p.Visible);
				}
				return this._selectedPage;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				if (this._selectedPage != value)
				{
					if (value.Parent != this)
						throw new ArgumentException("The RibbonBarPage doesn't belong to this RibbonBar.");

					this._selectedPage = value;

					OnSelectedPageChanged(EventArgs.Empty);
					Update("selectedIndex");
				}
			}
		}
		private RibbonBarPage _selectedPage;

		/// <summary>
		/// Returns the index of the currently selected <see cref="RibbonBarPage"/>.
		/// </summary>
		private int SelectedPageIndex
		{
			get
			{
				return
				  this._selectedPage == null
					 ? -1
					: this.Pages.IndexOf(this._selectedPage);
			}
		}

		/// <summary>
		/// Returns the collection of <see cref="RibbonBarPage"/> pages in the <see cref="RibbonBar"/>.
		/// </summary>
		/// <remarks>
		/// Each page is displayed as a tab in the ribbon. Adding a page to the collection sets its
		/// <see cref="RibbonBarPage.Parent"/> to this <see cref="RibbonBar"/>. When the selected page is removed,
		/// the first visible page becomes the <see cref="SelectedPage"/>. Pages can be retrieved by index or
		/// by name (case insensitive).
		/// </remarks>
		/// <example>
		/// Adding a page with a group and finding it again by name:
		/// <code><![CDATA[
		/// var insert = new RibbonBarPage { Name = "insert", Text = "&Insert" };
		/// var tables = new RibbonBarGroup { Text = "Tables" };
		/// tables.Items.Add(new RibbonBarItemButton { Name = "insertTable", Text = "Table", ImageSource = "icon-table" });
		/// insert.Groups.Add(tables);
		/// this.ribbonBar1.Pages.Add(insert);
		///
		/// // later...
		/// this.ribbonBar1.Pages["insert"].Enabled = false;
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[Description("Returns the collection of RibbonBarPage pages in the RibbonBar")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RibbonBarPageCollection Pages
		{
			get
			{
				if (this._pages == null)
				{
					this._pages = new RibbonBarPageCollection(this);
					this._pages.CollectionChanged += this.Pages_CollectionChanged;
				}

				return this._pages;
			}
		}
		private RibbonBarPageCollection _pages;

		private void Pages_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			switch (e.Action)
			{
				case CollectionChangeAction.Refresh:
					if (this.Pages.Count == 0)
					{
						this._selectedPage = null;
						Update("selectedIndex");
					}
					break;

				case CollectionChangeAction.Remove:
					if (e.Element == this.SelectedPage)
					{
						if (this.Pages.Count > 0)
							this.SelectedPage = this.Pages.FirstOrDefault(p => p.Visible);
						else
							this._selectedPage = null;

						Update("selectedIndex");
					}
					break;
			}
		}

		/// <summary>
		/// Returns or sets the collection of images available to the RibbonBar items.
		///</summary>
		/// <returns>An <see cref="T:Wisej.Web.ImageList" /> that contains images available to the <see cref="RibbonBarItem" /> controls. The default is null.</returns>
		/// <remarks>
		/// The images in the list are referenced by the <see cref="RibbonBarItem.ImageIndex"/> and <see cref="RibbonBarItem.ImageKey"/>
		/// properties of the items and by <see cref="RibbonBarAppButton.ImageIndex"/> and <see cref="RibbonBarAppButton.ImageKey"/>.
		/// </remarks>
		/// <example>
		/// Assigning images to the ribbon items using an <see cref="T:Wisej.Web.ImageList"/>:
		/// <code><![CDATA[
		/// this.ribbonBar1.ImageList = this.imageList1;
		/// this.buttonSave.ImageKey = "save.png";
		/// this.buttonPrint.ImageIndex = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the collection of images available to the RibbonBar items.")]
		public ImageList ImageList
		{
			get { return this._imageSettings == null ? null : this._imageSettings.ImageList; }
			set { this.ImageSettings.ImageList = value; }
		}

		/// <summary>
		/// Creates the property manager for the Image properties on first use.
		/// </summary>
		internal ImagePropertySettings ImageSettings
		{
			get
			{
				if (this._imageSettings == null)
					this._imageSettings = new ImagePropertySettings(this);

				return this._imageSettings;
			}
		}
		internal ImagePropertySettings _imageSettings;

		/// <summary>
		/// Returns or sets the compact view mode.
		/// </summary>
		/// <returns>true if only the tab strip is visible; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// In compact view only the tab buttons are displayed and no page is shown as selected. When the user clicks a tab,
		/// the page is displayed temporarily on top of the content below the <see cref="RibbonBar"/> and it collapses again
		/// when the user clicks anywhere outside of the ribbon (clicks on its drop down menus and popups are ignored).
		/// The server-side <see cref="SelectedPage"/> keeps the last page selected by the user.
		/// The height of the control is saved when entering the compact view and restored when leaving it.
		/// Changing the value fires the <see cref="CompactViewChanged"/> event.
		/// </remarks>
		/// <example>
		/// Toggling the compact view from a button:
		/// <code><![CDATA[
		/// private void buttonCollapse_Click(object sender, EventArgs e)
		/// {
		///     this.ribbonBar1.CompactView = !this.ribbonBar1.CompactView;
		/// }
		///
		/// private void ribbonBar1_CompactViewChanged(object sender, EventArgs e)
		/// {
		///     this.buttonCollapse.ImageSource = this.ribbonBar1.CompactView ? "icon-down" : "icon-up";
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the compact view mode.")]
		public bool CompactView
		{
			get => this._compactView;
			set
			{
				if (this._compactView != value)
				{
					if (value)
						this._savedHeight = this.Height;
					else
						this.Height = this._savedHeight;

					this._compactView = value;

					OnCompactViewChanged(EventArgs.Empty);
					Update();
				}
			}
		}
		private bool _compactView;
		private int _savedHeight = 0;

		private int CompactViewHeight
		{
			get;
			set;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Validates the current control.
		/// </summary>
		/// <returns>true if the active control is validated.</returns>
		protected internal bool ValidateActiveControl()
		{
			bool allowFocusChange = false;
			bool validated = ValidateActiveControl(out allowFocusChange, false);
			return (!this.ValidationCancelled && (validated || allowFocusChange));
		}

		// Returns the height of the RibbonBar as defined in the theme.
		private int GetRibbonBarThemeHeight()
		{
			var theme = ((IWisejControl)this).Theme;
			var height = theme.GetProperty<int>("ribbonbar", "height", "default");

			return
				height > 0
					? height
					: 33;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="RibbonBar" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._pages != null)
				{
					this._pages.Clear(true);
					this._pages = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Retrieves the size of a rectangular area into which a control can be fitted.
		///</summary>
		/// <param name="proposedSize">The custom size specified for the control.</param>
		/// <returns>The <see cref="System.Drawing.Size" /> representing the preferred size of the control.</returns>
		/// <remarks>
		/// The <paramref name="proposedSize"/> is ignored. The returned size is the current size of the control with the height
		/// replaced by the height measured on the client, limited by <see cref="Control.MinimumSize"/> and
		/// <see cref="Control.MaximumSize"/>. The current size is returned unchanged when the height is not known yet or
		/// when the control is docked to the left, right or fill, or anchored to both the top and the bottom.
		/// </remarks>
		/// <example>
		/// Reading the height that the <see cref="RibbonBar"/> needs:
		/// <code><![CDATA[
		/// var size = this.ribbonBar1.GetPreferredSize(Size.Empty);
		/// this.panelContent.Top = size.Height;
		/// ]]></code>
		/// </example>
		public override Size GetPreferredSize(Size proposedSize)
		{
			var size = this.Size;

			if (this._requestedHeight > 0 && !IsHeightDynamic())
			{
				size.Height = this._requestedHeight;

				var minSize = this.MinimumSize;
				var maxSize = this.MaximumSize;

				if (maxSize != Size.Empty)
				{
					size.Height = maxSize.Height > 0 ? Math.Min(maxSize.Height, size.Height) : size.Height;
				}

				if (minSize != Size.Empty)
				{
					size.Height = Math.Max(minSize.Height, size.Height);
				}
			}

			return size;
		}

		private bool IsHeightDynamic()
		{
			switch (this.Dock)
			{
				case DockStyle.Left:
				case DockStyle.Right:
				case DockStyle.Fill:
					return true;
			}

			return (this.Anchor.HasFlag(AnchorStyles.Top | AnchorStyles.Bottom));
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "ribbonbar"; }
		}

		/// <summary>
		/// Returns a collection of referenced components or collection of components.
		/// </summary>
		///<param name="items"></param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			items.Add(this.Pages);

			if (this._appButton != null)
				items.Add(this._appButton);
		}

		// Handles changePage event coming from the client.
		private void ProcessChangePageWebEvent(WisejEventArgs e)
		{
			RibbonBarPage page = e.Parameters.Page;
			if (page != null)
			{
				this._selectedPage = page;
				OnSelectedPageChanged(EventArgs.Empty);
			}
		}

		// Handles clicks on the inner tool icons.
		private void ProcessToolClickWebEvent(WisejEventArgs e)
		{
			ComponentTool tool = e.Parameters.Tool;
			if (tool != null)
				OnToolClick(new ToolClickEventArgs(tool));
		}

		// Handles resize events from the client.
		private void ProcessResizeWebEvent(WisejEventArgs e)
		{
			dynamic size = e.Parameters.Size;
			if (size != null)
			{
				this._requestedHeight = Convert.ToInt32(size.height);
				this.Height = this._requestedHeight;
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "changePage":
					ProcessChangePageWebEvent(e);
					break;

				case "toolClick":
					ProcessToolClickWebEvent(e);
					break;

				case "resize":
					ProcessResizeWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.RibbonBar";
			config.compactView = this.CompactView;

			// Tools.
			if (this._tools != null)
				config.tools = this._tools.Render();

			if (me.DesignMode)
			{
				if (this._appButton != null)
				{
					dynamic appButtonConfig = new DynamicObject();
					((IWisejComponent)this._appButton).Render(appButtonConfig);
					config.appButton = appButtonConfig;
				}

				if (me.IsNew || me.IsDirty)
					config.pages = this.Pages.Render();

				config.designItem = this.DesignItem;
			}
			else
			{
				config.appButton = ((IWisejComponent)this._appButton)?.Id;

				if (me.IsNew || this.Pages.IsDirty)
					config.pages = this.Pages.Render();

				config.wiredEvents.Add(
					"changePage(Page)",
					"toolClick(Tool)",
					"resize(Size)");
			}
			config.selectedIndex = this.SelectedPageIndex;
		}

		#endregion

		#region IWisejDesignTarget

		bool IWisejDesignTarget.ShouldDrawBorder()
		{
			return this.Pages.Count == 0;
		}

		// Handles the design-time click on the control.
		bool IWisejDesignTarget.OnMouseClick(Point location)
		{
			return SelectClickedTab(location) || SelectClickedItem(location);
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

		// Selects the RibbonBarPage at the coordinate specified in lParam.
		private bool SelectClickedTab(Point location)
		{
			Rectangle[] tabRects = this.UserData.DesignTabRects;
			int tabCount = tabRects?.Length ?? 0;
			if (tabCount > 0)
			{
				var mouseLoc = location;

				for (int i = 0; i < tabCount; i++)
				{
					var tabRect = tabRects[i];
					if (tabRect.Contains(mouseLoc))
					{
						this.SelectedPage = this.Pages[i];
						return true;
					}
				}
			}

			return false;
		}

		// Selects the RibbonBarGroup or RibbonBarItem at the coordinates specified in lParam.
		private bool SelectClickedItem(Point location)
		{
			Point mouseLoc = location;

			// find clicks on a child item.
			var target = FindDesignChildComponent(mouseLoc);
			if (target != null && this.Site != null)
			{
				var selectionService = (ISelectionService)this.Site.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SelectionChanged -= this.OnDesignComponentSelectionChanged;
					selectionService.SelectionChanged += this.OnDesignComponentSelectionChanged;
					selectionService.SetSelectedComponents(new[] { target });
					this.DesignItem = target;
					return true;
				}
			}
			this.DesignItem = null;
			return false;
		}

		private void OnDesignComponentSelectionChanged(object server, EventArgs e)
		{
			var designItem = this.DesignItem;
			if (designItem != null && this.Site != null)
			{
				var selectionService = (ISelectionService)this.Site.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					if (selectionService.GetComponentSelected(this))
					{
						selectionService.SetSelectedComponents(new[] { designItem });
						this.UserData.DesignItem = null;
					}
				}
			}
			else
			{
				Update();
			}
		}

		/// <summary>
		/// Sets the design-time metrics used by the designer to adapt the
		/// control on the screen to the HTML metrics used by the renderer.
		/// </summary>
		/// <param name="metrics">Design metrics from the renderer.</param>
		void IWisejControl.SetDesignMetrics(dynamic metrics)
		{
			if (metrics != null)
			{
				if (this.AutoSize && !IsHeightDynamic())
				{
					int height = Convert.ToInt32(metrics.height ?? 0);
					if (height > 0)
					{
						this.Height = height;
						this._requestedHeight = height;
					}
				}

				// retrieve the rectangles of the tab buttons.
				if (metrics.tabRects != null)
				{
					dynamic[] rects = metrics.tabRects;
					var tabRects = new Rectangle[rects.Length];
					for (int i = 0; i < rects.Length; i++)
					{
						dynamic rect = rects[i];
						if (rect != null)
							tabRects[i] = new Rectangle(rect.left, rect.top, rect.width, rect.height);
					}
					this.UserData.DesignTabRects = tabRects;
				}

				// retrieve the rectangles of all the visible items.
				if (metrics.itemMetrics != null)
				{
					dynamic[] itemMetrics = metrics.itemMetrics;
					for (int i = 0; i < itemMetrics.Length; i++)
					{
						dynamic item = itemMetrics[i];
						if (item != null)
						{
							var id = item.id;
							var rect = item.rect;
							if (rect != null)
							{
								// find the ribbon item.
								IWisejComponent component = FindDesignChildComponent(id);
								if (component != null)
								{
									var designRect = new Rectangle(rect.left, rect.top, rect.width, rect.height);
									component.DesignRect = designRect;
								}
							}
						}
					}
				}
			}
		}

		// Finds the child component, at any level, with the specified id.
		private IWisejComponent FindDesignChildComponent(string id)
		{
			foreach (var page in this.Pages)
			{
				foreach (IWisejComponent group in page.Groups)
				{
					if (group.Id == id)
						return group;

					foreach (IWisejComponent item in ((RibbonBarGroup)group).Items)
					{
						if (item.Id == id)
							return item;

						if (item is RibbonBarItemButtonGroup)
						{
							foreach (IWisejComponent button in ((RibbonBarItemButtonGroup)item).Buttons)
							{
								if (button.Id == id)
									return item;
							}
						}

						if (item is RibbonBarItemControl)
						{
							var itemControl = (RibbonBarItemControl)item;
							if (((IWisejComponent)itemControl.Control)?.Id == id)
								return itemControl.Control;
						}
					}
				}
			}

			return null;
		}

		// Finds the child component, at any level, containing the specified coordinate.
		private IWisejComponent FindDesignChildComponent(Point location)
		{
			IWisejComponent target = null;

			var page = this.SelectedPage;
			if (page != null)
			{
				foreach (IWisejComponent group in page.Groups)
				{
					Rectangle groupRect = group.DesignRect;

					if (groupRect.Contains(location))
					{
						target = group;

						// drill down to the items;
						foreach (IWisejComponent item in ((RibbonBarGroup)group).Items)
						{
							Rectangle itemRect = item.DesignRect;
							itemRect.Offset(groupRect.Left, 0);

							if (itemRect.Contains(location))
							{
								target = item;

								if (item is RibbonBarItemButtonGroup)
								{
									foreach (IWisejComponent button in ((RibbonBarItemButtonGroup)item).Buttons)
									{
										Rectangle buttonRect = button.DesignRect;
										buttonRect.Offset(itemRect.Left, 0);

										if (buttonRect.Contains(location))
										{
											target = button;
											break;
										}
									}
								}
								break;
							}
						}
						break;
					}
				}
			}
			return target;
		}

		#endregion
	}
}