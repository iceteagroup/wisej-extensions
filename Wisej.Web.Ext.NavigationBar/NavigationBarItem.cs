///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing.Design;
using Wisej.Base;

namespace Wisej.Web.Ext.NavigationBar
{
	/// <summary>
	/// Represents a navigation item in the <see cref="NavigationBar"/> control.
	/// </summary>
	[ToolboxItem(false)]
	public partial class NavigationBarItem : Wisej.Web.FlexLayoutPanel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NavigationBarItem"/> class.
		/// </summary>
		[ToolboxItem(false)]
		public NavigationBarItem()
		{
			InitializeComponent();
		}

		#region Events

		/// <summary>
		/// Fired when the user clicks the shortcut icon.
		/// </summary>
		public event EventHandler ShortcutClick;

		/// <summary>
		/// Fired when the user clicks the info "bubble".
		/// </summary>
		public event EventHandler InfoClick;

		/// <summary>
		/// Fired when the <see cref="NavigationBarItem"/> is expanded to show the
		/// child items.
		/// </summary>
		public event EventHandler Expand;

		/// <summary>
		///  Fired when the <see cref="NavigationBarItem"/> is collapsed to hide
		///  child items.
		/// </summary>
		public event EventHandler Collapse;

		/// <summary>
		/// Fires the <see cref="ShortcutClick"/> event.
		/// </summary>
		/// <param name="e">Not used</param>
		protected virtual void OnShortcutClick(EventArgs e)
		{
			this.ShortcutClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="InfoClick"/> event.
		/// </summary>
		/// <param name="e">Not used</param>
		protected virtual void OnInfoClick(EventArgs e)
		{
			this.InfoClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="Expanded"/> event.
		/// </summary>
		/// <param name="e">Not used</param>
		protected virtual void OnExpand(EventArgs e)
		{
			this.Expand?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="Collapsed"/> event.
		/// </summary>
		/// <param name="e">Not used</param>
		protected virtual void OnCollapse(EventArgs e)
		{
			this.Collapse?.Invoke(this, e);
		}

		#region Redirect pointer events from the header panel

		public new event EventHandler Click
		{
			add { this.header.Click += value; }
			remove { this.header.Click -= value; }
		}

		public new event EventHandler Tap
		{
			add { this.header.Tap += value; }
			remove { this.header.Tap -= value; }
		}

		public new event EventHandler LongTap
		{
			add { this.header.LongTap += value; }
			remove { this.header.LongTap -= value; }
		}

		public new event SwipeEventHandler Swipe
		{
			add { this.header.Swipe += value; }
			remove { this.header.Swipe -= value; }
		}

		public new event MouseEventHandler MouseClick
		{
			add { this.header.MouseClick += value; }
			remove { this.header.MouseClick -= value; }
		}

		public new event MouseEventHandler MouseDown
		{
			add { this.header.MouseDown += value; }
			remove { this.header.MouseDown -= value; }
		}

		public new event MouseEventHandler MouseUp
		{
			add { this.header.MouseUp += value; }
			remove { this.header.MouseUp -= value; }
		}

		public new event EventHandler MouseEnter
		{
			add { this.header.MouseEnter += value; }
			remove { this.header.MouseEnter -= value; }
		}

		public new event EventHandler MouseLeave
		{
			add { this.header.MouseLeave += value; }
			remove { this.header.MouseLeave -= value; }
		}

		#endregion

		#endregion

		#region Properties

		/// <summary>
		/// Returns the owner <see cref="NavigationBar"/>.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NavigationBar NavigationBar
		{
			get
			{
				if (this._navbar == null)
				{
					for (var parent = base.Parent; parent != null; parent = parent.Parent)
					{
						if (parent is NavigationBar)
						{
							this._navbar = (NavigationBar)parent;
							break;

						}
					}
				}
				return this._navbar;
			}
		}
		private NavigationBar _navbar;

		/// <summary>
		/// Returns the parent <see cref="NavigationBarItem"/> or null.
		/// </summary>
		[Browsable(false)]
		public new NavigationBarItem Parent
		{
			get
			{
				for (var parent = base.Parent; parent != null; parent = parent.Parent)
				{
					if (parent is NavigationBarItem)
					{
						return (NavigationBarItem)parent;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Returns or sets the icon of the <see cref="NavigationBarItem"/>.
		/// </summary>
		[DefaultValue(null)]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string Icon
		{
			get => this.icon.ImageSource;
			set => this.icon.ImageSource = value;
		}

		/// <summary>
		/// Returns or sets the icon of the <see cref="NavigationBarItem"/>.
		/// </summary>
		[DefaultValue(typeof(Cursor), "Hand")]
		public override Cursor Cursor { get => base.Cursor; set => base.Cursor = value; }

		/// <summary>
		/// Returns or sets the title of the <see cref="NavigationBarItem"/>.
		/// </summary>
		[DefaultValue("")]
		public override string Text
		{
			get => this.title.Text;
			set => this.title.Text = value;
		}

		/// <summary>
		/// Returns the collection of items to display in the <see cref="NavigationBarItem"/>.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NavigationBarItemCollection Items
		{
			get => this._items = this._items ?? new NavigationBarItemCollection(this.items);
		}
		private NavigationBarItemCollection _items;

		/// <summary>
		/// Returns or sets the background color.
		/// </summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public new Color BackColor
		{
			get => this.header.BackColor;
			set => this.header.BackColor = value;
		}

		private bool ShouldSerializeBackColor()
		{
			return this.header.BackColor.Name != "@navbar-background";
		}

		private new void ResetBackColor()
		{
			this.header.BackColor = Color.Empty;
		}

		/// <summary>
		/// Returns or sets the text color.
		/// </summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ControlForeColorDescr")]
		public new Color ForeColor
		{
			get => this.header.ForeColor;
			set => this.header.ForeColor = value;
		}

		private bool ShouldSerializeForeColor()
		{
			return this.header.ForeColor.Name != "@navbar-text";
		}

		private new void ResetForeColor()
		{
			this.header.ForeColor = Color.Empty;
		}

		/// <summary>
		/// Determines whether to expand or collapse the item when clicking the item rather than
		/// having to click the open/close icon.
		/// </summary>
		[DefaultValue(true)]
		public bool ExpandOnClick
		{
			get { return this._expandOnClick; }
			set { this._expandOnClick = value; }
		}
		private bool _expandOnClick = true;

		/// <summary>
		/// Expands or collapses child items.
		/// </summary>
		[DefaultValue(false)]
		public bool Expanded
		{
			get => this.items.Visible;
			set
			{
				if (this.items.Controls.Count > 0)
					this.items.Visible = value;
				else
					this.items.Visible = false;

				// apply indentation.
				var level = this.Level + 1;
				var padding = new Padding(this.NavigationBar.Indentation * level, 0, 0, 0) ;
				foreach (NavigationBarItem i in this.items.Controls)
				{
					i.icon.Margin = padding;
				}
			}
		}

		/// <summary>
		/// Returns the indentation level of this <see cref="NavigationBarItem"/> item.
		/// </summary>
		[Browsable(false)]
		public int Level
		{
			get
			{
				var level = 0;
				for (var parent = this.Parent; parent != null; parent = parent.Parent)
				{
					level++;
				}
				return level;
			}
		}

		/// <summary>
		/// Returns whether the <see cref="NavigationBarItem"/> is the currently selected item.
		/// </summary>
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get => this.header.HasState("selected");
			internal set
			{
				if (value)
					this.header.AddState("selected");
				else
					this.header.RemoveState("selected");
			}
		}

		/// <summary>
		/// Returns or sets the text to show in the info "bubble" next to the item.
		/// </summary>
		[DefaultValue("")]
		public string InfoText
		{
			get => this.info.Text;
			set
			{
				this.info.Text = value;
				this.info.Visible = !this.CompactView && !String.IsNullOrEmpty(value);
			}
		}

		/// <summary>
		/// Returns or sets the background color of the info "bubble".
		/// </summary>
		public Color InfoTextBackColor
		{
			get => this.info.BackColor;
			set => this.info.BackColor = value;
		}

		private bool ShouldSerializeInfoTextBackColor()
		{
			return this.info.BackColor != this.header.BackColor;
		}

		private void ResetInfoTextBackColor()
		{
			this.info.BackColor = Color.Empty;
		}

		/// <summary>
		/// Returns or sets the text color of the info "bubble".
		/// </summary>
		public Color InfoTextForeColor
		{
			get => this.info.ForeColor;
			set => this.info.ForeColor = value;
		}

		private bool ShouldSerializeInfoTextForeColor()
		{
			return this.info.ForeColor.Name != "@navbar-text";
		}

		private void ResetInfoTextForeColor()
		{
			this.info.ForeColor = Color.Empty;
		}

		/// <summary>
		/// Shows or hides the shortcut button.
		/// </summary>
		[DefaultValue(false)]
		public bool ShowShortcut
		{
			get { return this._showShortcut; }
			set
			{
				this._showShortcut = value;
				this.shortcut.Visible = !this.CompactView && value;
			}
		}
		private bool _showShortcut;

		/// <summary>
		/// Returns or sets the shortcut icon.
		/// </summary>
		[DefaultValue("spinner-plus")]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string ShortcutIcon
		{
			get => this.shortcut.ImageSource;
			set => this.shortcut.ImageSource = value;
		}

		/// <summary>
		/// Returns or sets the height of the <see cref="NavigationBarItem"/> and its child items.
		/// </summary>
		internal int ItemHeight
		{
			get { return this.header.Height; }
			set
			{
				if (value < 0 || value > 32000)
					throw new ArgumentOutOfRangeException(nameof(ItemHeight));

				if (this.header.Height != value)
				{
					this.header.Height = value;

					if (this._items?.Count > 0)
					{
						foreach (var item in this.Items)
						{
							item.ItemHeight = value;
						}
					}
				}
			}
		}

		#endregion

		#region Implementation

		#region Unsupported properties and events

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FlexLayoutStyle LayoutStyle { get => base.LayoutStyle; private set => base.LayoutStyle = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MaximumSize { get => base.MaximumSize; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MinimumSize { get => base.MinimumSize; set => base.MinimumSize = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size Size { get => base.Size; set => base.Size = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Point Location { get => base.Location; set => base.Location = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock { get => base.Dock; set => base.Dock = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Margin { get => base.Margin; set => base.Margin = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding { get => base.Padding; set => base.Padding = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AutoSizeMode AutoSizeMode { get => base.AutoSizeMode; set => base.AutoSizeMode = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMargin { get => base.AutoScrollMargin; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize { get => base.AutoScrollMinSize; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll { get => base.AutoScroll; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor { get => base.Anchor; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ComponentToolCollection Tools => base.Tools;
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int TabIndex { get => base.TabIndex; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles ResizableEdges { get => base.ResizableEdges; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CausesValidation { get => base.CausesValidation; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Anonymous { get => base.Anonymous; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowCloseButton { get => base.ShowCloseButton; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowHeader { get => base.ShowHeader; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Collapsed { get => base.Collapsed; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override HeaderPosition CollapseSide { get => base.CollapseSide; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override Rectangle CollapsedBounds => base.CollapsedBounds;
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override PanelAutoShowMode AutoShow { get => base.AutoShow; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage { get => base.BackgroundImage; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string BackgroundImageSource { get => base.BackgroundImageSource; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image { get => base.Image; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex { get => base.ImageIndex; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey { get => base.ImageKey; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageSource { get => base.ImageSource; set { } }      /// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImageList ImageList { get => base.ImageList; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Movable { get => base.Movable; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string AppearanceKey { get => base.AppearanceKey; set => base.AppearanceKey = value; }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override HorizontalAlignment HeaderAlignment { get => base.HeaderAlignment; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override HeaderPosition HeaderPosition { get => base.HeaderPosition; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int HeaderSize { get => base.HeaderSize; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color HeaderBackColor { get => base.HeaderBackColor; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color HeaderForeColor { get => base.HeaderForeColor; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ScrollBars ScrollBars { get => base.ScrollBars; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override BorderStyle BorderStyle { get => base.BorderStyle; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new HorizontalAlignment HorizontalAlign { get => base.HorizontalAlign; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new VerticalAlignment VerticalAlign { get => base.VerticalAlign; set { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int Spacing { get => base.Spacing; set => base.Spacing = value; }

		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PanelCollapsed { add { } remove { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PanelExpanded { add { } remove { } }
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ToolClickEventHandler ToolClick { add { } remove { } }

		#endregion

		private bool CompactView
		{
			get => this.NavigationBar?.CompactView ?? false;
		}

		protected override void OnParentChanged(EventArgs e)
		{
			// sync the CompactView mode with the parent NavigationBar.
			if (this._navbar != null)
			{
				this._navbar.CompactViewChanged -= this.Navbar_CompactViewChanged;
				this._navbar = null;
			}

			var navbar = this.NavigationBar;
			if (navbar != null)
			{
				navbar.CompactViewChanged -= this.Navbar_CompactViewChanged;
				navbar.CompactViewChanged += this.Navbar_CompactViewChanged;
			}

			var parent = this.Parent;
			if (parent != null)
			{
				parent.Resize -= Parent_Resize;
				parent.Resize += Parent_Resize;
			}

			base.OnParentChanged(e);
		}

		private void Parent_Resize(object sender, EventArgs e)
		{
			var parent = (Control)sender;
			this.header.Width =
				this.items.Width = parent.Width;
		}

		private void Navbar_CompactViewChanged(object sender, EventArgs e)
		{
			var compactView = this.CompactView;

			this.title.Visible = !compactView;
			this.open.Visible = !compactView && this.Items.Count > 0;
			this.shortcut.Visible = !compactView && this.ShowShortcut;
			this.info.Visible = !compactView && !String.IsNullOrEmpty(this.InfoText);

			if (String.IsNullOrEmpty (this.ToolTipText))
				this.header.ToolTipText = compactView ? this.title.Text : null;
		}

		private void shortcut_Click(object sender, EventArgs e)
		{
			OnShortcutClick(e);
		}

		private void info_Click(object sender, EventArgs e)
		{
			OnInfoClick(e);
		}

		private void open_Click(object sender, EventArgs e)
		{
			this.Expanded = !this.Expanded;
		}

		private void items_VisibleChanged(object sender, EventArgs e)
		{
			if (this.items.Visible)
			{
				OnExpand(e);
				this.open.AddState("open");
			}
			else
			{
				OnCollapse(e);
				this.open.RemoveState("open");
			}
		}

		private void header_Click(object sender, EventArgs e)
		{
			if (this.ExpandOnClick)
				this.Expanded = !this.Expanded;

			this.NavigationBar?.FireItemClick(this);
		}

		private void items_ControlAdded(object sender, ControlEventArgs e)
		{
			this.open.Visible =
				!this.CompactView;

			if (this.NavigationBar != null)
				((NavigationBarItem)e.Control).ItemHeight = this.NavigationBar.ItemHeight;
		}

		private void items_ControlRemoved(object sender, ControlEventArgs e)
		{
			this.open.Visible =
				!this.CompactView
				&& this.items.Controls.Count > 0;
		}

		public override void Update()
		{
			base.Update();

			if (this.DesignMode)
				this.NavigationBar?.Update();
		}

		#endregion
	}
}
