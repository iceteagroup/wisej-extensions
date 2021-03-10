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
	/// </para>
	/// </remarks>
	[ToolboxItem(true)]
	[DefaultEvent("Load")]
	[ToolboxBitmap(typeof(RibbonBar))]
	[Description("The RibbonBar organizes the features of an application into a series of tabs.")]
	public class RibbonBar : Control, IWisejControl, IWisejDesignTarget
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="RibbonBar"/> control.
		/// </summary>
		public RibbonBar()
		{
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
		public new event EventHandler AutoSizeChanged
		{
			add { base.AutoSizeChanged += value; }
			remove { base.AutoSizeChanged -= value; }
		}

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
		/// Fired when a <see cref="T:Wisej.Web.ComponentTool" /> is clicked.
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
		/// Returns or sets whether clicking RibbonBar items causes validation to be performed on
		/// the active control.
		/// </summary>
		/// <returns>true if clicking RibbonBar items causes validation to be performed 
		/// on the active control; otherwise, false. The default is false.</returns>
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
		/// Represents the application button displayed before the first
		/// <see cref="RibbonBarPage"/>.
		/// </summary>
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
				case CollectionChangeAction.Remove:
					if (e.Element == this.SelectedPage)
					{
						if (this.Pages.Count > 0)
							this.SelectedPage = this.Pages.FirstOrDefault(p => p.Visible);
						else
							this.SelectedPage = null;

						Update("selectedIndex");
					}
					break;
			}
		}

		/// <summary>
		/// Returns or sets the collection of images available to the RibbonBar items.
		///</summary>
		/// <returns>An <see cref="T:Wisej.Web.ImageList" /> that contains images available to the <see cref="RibbonBarItem" /> controls. The default is null.</returns>
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
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
			// retrieve the current active theme.
			ClientTheme theme = ((IWisejControl)this).Theme;

			object height = theme.GetProperty<int>("ribbonbar", "height", "default");

			if (height is int)
				return (int)height;

			return 80;
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
				this.Size = new Size(
					Convert.ToInt32(size.width),
					Convert.ToInt32(size.height));
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
			config.selectedIndex = this.SelectedPageIndex;

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

				config.designItem = this.UserData.DesignItem;
			}
			else
			{
				config.appButton = ((IWisejComponent)this._appButton)?.Id;

				if (me.IsNew || this.Pages.IsDirty)
					config.pages = this.Pages.Render();

				config.wiredEvents.Add("changePage(Page)", "toolClick(Tool)", "resize(Size)");
			}

		}

		#endregion

		#region IWisejDesignTarget

		/// <summary>
		/// Processes Windows mouse messages forwarded by the designer.
		/// </summary>
		/// <param name="m">The <see cref="System.Windows.Forms.Message"/> forwarded by the designer.</param>
		/// <returns>Returns true to prevent the base class from processing the message.</returns>
		bool IWisejDesignTarget.DesignerWndProc(ref System.Windows.Forms.Message m)
		{
			switch (m.Msg)
			{
				// WM_LBUTTONDOWN
				case 0x0201:
					var lParam = (int)m.LParam.ToInt64();
					return SelectClickedTab(lParam) || SelectClickedItem(lParam);
			}

			return false;
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
		private bool SelectClickedTab(int lParam)
		{
			Rectangle[] tabRects = this.UserData.DesignTabRects;
			int tabCount = tabRects?.Length ?? 0;
			if (tabCount > 0)
			{
				Point mouseLoc = new Point(
					(short)(lParam & 65535),
					(short)(lParam >> 16 & 65535));

				for (int i = 0; i < tabCount; i++)
				{
					Rectangle tabRect = tabRects[i];
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
		private bool SelectClickedItem(int lParam)
		{
			Point mouseLoc = new Point(
				(short)(lParam & 65535),
				(short)(lParam >> 16 & 65535));

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
			IWisejComponent target = this.DesignItem;
			if (target != null && this.Site != null)
			{
				var selectionService = (ISelectionService)this.Site.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					if (selectionService.GetComponentSelected(this))
						selectionService.SetSelectedComponents(new[] { target });
				}
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