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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Cryptography;
using System.Text;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.NavigationBar
{
	/// <summary>
	/// Represents a responsive vertical navigation bar that displays
	/// an application header with logo, child items, and a user panel
	/// with gravatar and other user information.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(NavigationBar))]
	[Description("Responsive vertical navigation bar.")]
	public partial class NavigationBar : Wisej.Web.FlexLayoutPanel, IWisejDesignTarget
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of <see cref="NavigationBar"/>.
		/// </summary>
		public NavigationBar()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user clicks the title header.
		/// </summary>
		[Description("Fired when the user clicks the title header.")]
		public event EventHandler TitleClick;

		/// <summary>
		/// Fired when the user clicks the user information panel.
		/// </summary>
		[Description("Fired when the user clicks the user information panel.")]
		public event EventHandler UserClick;

		/// <summary>
		/// Fired when the user clicks an item in the <see cref="NavigationBar"/>.
		/// </summary>
		[Description("Fired when the user clicks an item in the NavigationBar.")]
		public event NavigationBarItemClickEventHandler ItemClick;

		/// <summary>
		/// Fired when the value of the property <see cref="CompactView"/> changes.
		/// </summary>
		[Description("Fired when the value of the property CompactView changes.")]
		public event EventHandler CompactViewChanged;

		/// <summary>
		/// Fired when the value of the property <see cref="SelectedItem"/> changes.
		/// </summary>
		[Description("Fired when the value of the property SelectedItem changes.")]
		public event EventHandler SelectedItemChanged;

		/// <summary>
		/// Fires the <see cref="TitleClick"/> event.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected virtual void OnTitleClick(EventArgs e)
		{
			this.TitleClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="UserClick"/> event.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected virtual void OnUserClick(EventArgs e)
		{
			this.UserClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="CompactViewChanged"/> event.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected virtual void OnCompactViewChanged(EventArgs e)
		{
			this.CompactViewChanged?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="CompactViewChanged"/> event.
		/// </summary>
		/// <param name="e">A <see cref="NavigationBarItemClickEventArgs"/> containing the event data.</param>
		protected virtual void OnItemClick(NavigationBarItemClickEventArgs e)
		{
			this.ItemClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the <see cref="SelectedItemChanged"/> event.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
			this.SelectedItemChanged?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the selected item.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NavigationBarItem SelectedItem
		{
			get { return this._selectedItem; }
			set {

				if (value != null && value.NavigationBar != this)
					throw new ArgumentException("The NavigationBarItem doesn't belong to this NavigationBar.");

				if (this._selectedItem != value)
				{
					if (this._selectedItem != null)
						this._selectedItem.Selected = false;

					this._selectedItem = value;

					if (this._selectedItem != null)
						this._selectedItem.Selected = true;

					// expand all parents.
					if (this._selectedItem != null)
					{
						for (var parent = this._selectedItem.Parent; parent != null; parent = parent.Parent)
						{
							parent.Expanded = true;
						}
					}

					OnSelectedItemChanged(EventArgs.Empty);
				}
			}
		}
		private NavigationBarItem _selectedItem;

		/// <summary>
		/// Returns or sets the compact view mode.
		/// </summary>
		[ResponsiveProperty]
		[DefaultValue(false)]
		public bool CompactView
		{
			get => this._compactView;
			set
			{
				if (this._compactView != value)
				{
					this._compactView = value;

					this.title.Visible = !value;
					this.userInfo.Visible = !value;
					this.userName.Visible = !value;

					if (value)
					{
						this._savedWidth = this.Width;
						this._savedAvatarSize = this.avatar.Size;

						this.Width = this.CompactViewWidth;
						this.avatar.Size = this.avatar.MaximumSize = this.logo.Size;
					}
					else if (this._savedWidth > 0)
					{
						this.Width = this._savedWidth;
						this.avatar.Size = this.avatar.MaximumSize = this._savedAvatarSize;
					}

					OnCompactViewChanged(EventArgs.Empty);
				}
			}
		}
		private bool _compactView;
		private int _savedWidth = 0;
		private Size _savedAvatarSize = Size.Empty;

		private int CompactViewWidth
		{
			get
			{
				return this.logo.Right + this.logo.Left;
			}
		}

		/// <summary>
		/// Returns or sets the indentation in pixels for child items.
		/// </summary>
		[ResponsiveProperty]
		[DefaultValue(0)]
		public int Indentation
		{
			get { return this._indentation; }
			set
			{
				if (this._indentation != value)
				{
					this._indentation = value;

					if (this._items != null)
					{
						foreach (var i in this.Items)
						{
							i.UpdateIndentation();
						}
					}
				}
			}
		}
		private int _indentation = 0;

		/// <summary>
		/// Returns or sets the logo to display in the <see cref="NavigationBar"/>.
		/// </summary>
		[DefaultValue("")]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string Logo
		{
			get => this.logo.ImageSource;
			set => this.logo.ImageSource = value;
		}

		/// <summary>
		/// Returns or sets the title to display in the <see cref="NavigationBar"/>.
		/// </summary>
		[DefaultValue("")]
		public override string Text
		{
			get => this.title.Text;
			set => this.title.Text = value;
		}

		/// <summary>
		/// Shows or hides the user panel.
		/// </summary>
		public bool ShowUser
		{
			get => this.user.Visible;
			set => this.user.Visible = value;
		}

		/// <summary>
		/// Returns or sets the user avatar to display in the <see cref="NavigationBar"/>.
		/// </summary>
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string UserAvatar
		{
			get => this.avatar.ImageSource;
			set => this.avatar.ImageSource = value;
		}

		/// <summary>
		/// Returns or sets the user status color.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		public Color UserStatusColor
		{
			get => this.userStatusColor.BackColor;
			set => this.userStatusColor.BackColor = value;
		}

		/// <summary>
		/// Returns or sets the user status.
		/// </summary>
		[DefaultValue("")]
		public string UserStatus
		{
			get => this.userStatusName.Text;
			set => this.userStatusName.Text = value;
		}

		/// <summary>
		/// Returns or sets the user name.
		/// </summary>
		[DefaultValue("")]
		public string UserName
		{
			get => this.userName.Text;
			set => this.userName.Text = value;
		}

		/// <summary>
		/// Returns or sets the height of the child <see cref="NavigationBarItem"/> elements.
		/// </summary>
		[DefaultValue(45)]
		public int ItemHeight
		{
			get { return this._itemHeight; }
			set
			{
				if (value < 0 || value > 32000)
					throw new ArgumentOutOfRangeException(nameof(ItemHeight));

				if (this._itemHeight != value)
				{
					this._itemHeight = value;

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
		private int _itemHeight = 45;

		/// <summary>
		/// Returns the collection of items to display in the <see cref="NavigationBar"/>.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public NavigationBarItemCollection Items
		{
			get => this._items = this._items ?? new NavigationBarItemCollection(this.items);
		}
		private NavigationBarItemCollection _items;

		private void items_ControlAdded(object sender, ControlEventArgs e)
		{
			((NavigationBarItem)e.Control).ItemHeight = this.ItemHeight;
		}

		/// <summary>
		/// Allows the user to change the <see cref="SelectedItem"/> using the keyboard.
		/// </summary>
		[DefaultValue(false)]
		public bool EnableKeyboardNavigation
		{
			get { return this._enableKeyboardNavigation; }
			set
			{
				if (this._enableKeyboardNavigation != value)
				{
					this._enableKeyboardNavigation = value;

					if (value)
					{
						this.Focusable = true;
						this.KeyDown += this.NavigationBar_KeyDown;
						this.KeyPress += this.NavigationBar_KeyPress;
					}
					else
					{
						this.Focusable = true;
						this.KeyDown -= this.NavigationBar_KeyDown;
						this.KeyPress -= this.NavigationBar_KeyPress;
					}
				}
			}
		}
		private bool _enableKeyboardNavigation = false;

		#endregion

		#region Methods

		/// <summary>
		/// Returns the URL for the https://en.gravatar.com associated
		/// to specified <paramref name="email"/> address.
		/// </summary>
		/// <param name="email">Email address for which to retrieve the gravatar URL.</param>
		/// <returns></returns>
		public string GetGravatarUrl(string email)
		{
			if (email is null)
				throw new ArgumentNullException(nameof(email));

			email = email.ToLower().Trim();
			using (var md5 = new MD5CryptoServiceProvider())
			{
				byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(email));
				return "https://gravatar.com/avatar/" + BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
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
		public override ComponentToolCollection Tools => base.Tools;
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
		public new string ImageSource { get => base.ImageSource; set { } }
		/// <exclude/>
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

		private void NavigationBar_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					this.SelectedItem = GetPreviousItem(this.SelectedItem, true);
					break;

				case Keys.Down:
					this.SelectedItem = GetNextItem(this.SelectedItem, true);
					break;

				case Keys.Right:
					if (this.SelectedItem?.Items.Count > 0)
						this.SelectedItem = this.SelectedItem.Items[0];
					break;

				case Keys.Left:
					if (this.SelectedItem?.Parent != null)
						this.SelectedItem = this.SelectedItem?.Parent;
					break;

				case Keys.Home:
					if (this.Items.Count > 0)
						this.SelectedItem = this.Items[0];
					break;

				case Keys.End:
					if (this.Items.Count > 0)
						this.SelectedItem = this.Items[this.Items.Count - 1];
					break;

				case Keys.Space:
				case Keys.Enter:
					if (this.SelectedItem != null)
						this.SelectedItem.Expanded = !this.SelectedItem.Expanded;
					break;
			}
		}

		private void NavigationBar_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case '-':
					if (this.SelectedItem != null)
						this.SelectedItem.Expanded = false;
					break;

				case '+':
					if (this.SelectedItem != null)
						this.SelectedItem.Expanded = true;
					break;
			}
		}

		private NavigationBarItem GetPreviousItem(NavigationBarItem selectedItem, bool deep)
		{
			if (selectedItem != null)
			{
				var items = selectedItem.Parent?.Items ?? this.Items;
				if (items != null)
				{
					var index = items.IndexOf(selectedItem) - 1;
					if (index > -1)
					{
						selectedItem = items[index];
						while (deep && selectedItem.Expanded && selectedItem.Items?.Count > 0)
						{
							selectedItem = selectedItem.Items[selectedItem.Items.Count - 1];
						}
					}
					else
					{
						selectedItem = selectedItem.Parent;
					}
				}
			}

			if (selectedItem == null && this.Items.Count > 0)
				selectedItem = this.Items[this.Items.Count - 1];

			return selectedItem;
		}

		private NavigationBarItem GetNextItem(NavigationBarItem selectedItem, bool deep)
		{
			if (selectedItem != null)
			{
				if (deep && selectedItem.Expanded && selectedItem.Items?.Count > 0)
					return selectedItem.Items[0];

				var items = selectedItem.Parent?.Items ?? this.Items;
				if (items != null)
				{
					var index = items.IndexOf(selectedItem) + 1;
					if (index < items.Count)
						return items[index];

					return GetNextItem(selectedItem.Parent, false);
				}
			}

			if (selectedItem == null && this.Items.Count > 0)
				selectedItem = this.Items[0];

			return selectedItem;
		}

		internal void FireItemClick(NavigationBarItem item)
		{
			Debug.Assert(item != null);

			OnItemClick(new NavigationBarItemClickEventArgs(item));

			this.SelectedItem = item;
		}

		private void user_Click(object sender, EventArgs e)
		{
			OnUserClick(e);
		}

		private void header_Click(object sender, EventArgs e)
		{
			OnTitleClick(e);
		}

		#endregion

		#region IWisejDesignTarget

		bool IWisejDesignTarget.DesignerWndProc(ref System.Windows.Forms.Message m)
		{
			switch (m.Msg)
			{
				// WM_LBUTTONDOWN
				case 0x0201:
					var lParam = (int)m.LParam.ToInt64();
					return SelectClickedItem(lParam);
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

		// Selects the NavigationBarItem at the coordinate specified in lParam.
		private bool SelectClickedItem(int lParam)
		{
			Point mouseLoc = new Point(
				(short)(lParam & 65535),
				(short)(lParam >> 16 & 65535));

			// find clicks on a child item.
			var target = GetChildAtPoint(mouseLoc);
			if (target != null && this.Site != null)
			{
				while (target != null && target.Site == null)
				{
					mouseLoc.Offset(-target.Left, -target.Top);
					target = target.GetChildAtPoint(mouseLoc);
				}

				if (target != null && target is NavigationBarItem && target.Site != null && target.Site.DesignMode)
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
			}
			this.DesignItem = null;
			return false;
		}

		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.CompactView)
				width = this.CompactViewWidth;

			base.SetBoundsCore(x, y, width, height, specified);
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
					{
						this.DesignItem = null;
						selectionService.SetSelectedComponents(new[] { target }, SelectionTypes.Replace);
					}
				}
			}
		}

		#endregion
	}
}
