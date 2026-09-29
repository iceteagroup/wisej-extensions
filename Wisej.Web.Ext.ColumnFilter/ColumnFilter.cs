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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Wisej.Core;

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Extender component that adds a filter button to the header of <see cref="DataGridViewColumn"/> columns.
	/// Clicking the button opens a <see cref="ColumnFilterPanel"/> that filters the rows of the <see cref="DataGridView"/>.
	/// </summary>
	/// <remarks>
	/// Set <see cref="FilterPanelType"/> to the <see cref="ColumnFilterPanel"/> subclass to use
	/// (i.e. <see cref="SimpleColumnFilterPanel"/> or <see cref="WhereColumnFilterPanel"/>) and enable
	/// the button on each column using the ShowFilter extender property or <see cref="SetShowFilter"/>.
	/// Filters are applied by hiding the rows that don't match; they are re-applied automatically when the
	/// <see cref="DataGridView"/> is sorted or its data binding completes.
	/// </remarks>
	/// <example>
	/// Adding a filter button to all the columns of a <see cref="DataGridView"/>:
	/// <code><![CDATA[
	/// var columnFilter = new ColumnFilter();
	/// columnFilter.FilterPanelType = typeof(SimpleColumnFilterPanel);
	/// columnFilter.RowsFiltered += (s, e) => this.labelCount.Text = $"{e.FilteredRowCount} rows";
	///
	/// foreach (DataGridViewColumn column in this.dataGridView1.Columns)
	/// {
	///     columnFilter.SetShowFilter(column, true);
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ColumnFilter))]
	[ProvideProperty("ShowFilter", typeof(DataGridViewColumn))]
	[Description("Adds a filter button to DataGridViewColumn to display a custom filter panel.")]
	[ApiCategory("ColumnFilter")]
	public class ColumnFilter : Wisej.Web.Component, IWisejExtenderProvider, ISupportInitialize
	{
		#region Constructors

		// keeps the list of columns using this filter panel.
		private List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

		// keeps the list of datagridviews using this filter panel.
		private List<DataGridView> dataGrids = new List<DataGridView>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnFilter"/> class.
		/// </summary>
		/// <remarks>
		/// The <see cref="ImageSource"/> of the filter button is initialized to "icon-search".
		/// </remarks>
		public ColumnFilter()
		{
			this.ImageSource = "icon-search";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnFilter" /> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="IContainer" /> that represents the container of the component.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		public ColumnFilter(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the type of the <see cref="ColumnFilterPanel"/> to associate
		/// with this <see cref="ColumnFilter"/> extender.
		/// </summary>
		/// <exception cref="ArgumentException">The value is not a subclass of <see cref="ColumnFilterPanel"/>.</exception>
		/// <remarks>
		/// The type must be a subclass of <see cref="ColumnFilterPanel"/> with a public parameterless constructor.
		/// A new instance is created for each column the first time the user clicks its filter button and it's reused afterwards;
		/// changing this property doesn't replace panels that have already been created.
		/// This property must be set before the user clicks a filter button: the default value null causes an exception.
		/// </remarks>
		/// <example>
		/// Using the built-in panel that lets the user build conditions with operators:
		/// <code><![CDATA[
		/// this.columnFilter1.FilterPanelType = typeof(WhereColumnFilterPanel);
		/// ]]></code>
		/// Using a custom panel derived from <see cref="ColumnFilterPanel"/>:
		/// <code><![CDATA[
		/// public class MyFilterPanel : ColumnFilterPanel
		/// {
		///     protected override bool OnApplyFilter()
		///     {
		///         // hide the rows that don't match and return true when a filter is active.
		///         return false;
		///     }
		/// }
		///
		/// this.columnFilter1.FilterPanelType = typeof(MyFilterPanel);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[TypeConverter(typeof(ColumnFilterPanelTypeConverter))]
		public Type FilterPanelType
		{
			get { return this._filterPanelType; }
			set
			{
				if (this._filterPanelType != value)
				{
					if (value != null && !value.IsSubclassOf(typeof(ColumnFilterPanel)))
						throw new ArgumentException(value.FullName + " is not a subclass of ColumnFilterPanel", nameof(value));

					this._filterPanelType = value;
				}
			}
		}
		private Type _filterPanelType;

		/// <summary>
		/// Creates the property manager for the Image properties on first use.
		/// </summary>
		internal virtual ImagePropertySettings ImageSettings
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
		/// Returns or sets the image that is displayed in the filter button.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Image" /> to display.</returns>
		/// <remarks>
		/// The image is copied to each filter button when the button is created and it's also restored when
		/// a column's filter is cleared. Changing it at runtime doesn't update the buttons already created until the filters of the column are re-applied.
		/// When both <see cref="Image"/> and <see cref="ImageSource"/> are set, <see cref="Image"/> takes precedence.
		/// </remarks>
		[Bindable(true)]
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the image that is displayed in the filter button.")]
		public Image Image
		{
			get { return this._imageSettings == null ? null : this._imageSettings.Image; }
			set { this.ImageSettings.Image = value; }
		}

		/// <summary>
		/// Returns or sets the theme name or URL for the image to display in the filter button.
		/// </summary>
		/// <returns>The theme name or URL for the image to display in the filter button.</returns>
		/// <remarks>
		/// The default value is "icon-search". The value is copied to each filter button when the button is created
		/// and it's restored when a column's filter is cleared.
		/// </remarks>
		/// <example>
		/// Using a different theme icon for the filter button and for the active filter state:
		/// <code><![CDATA[
		/// this.columnFilter1.ImageSource = "icon-filter";
		/// this.columnFilter1.FilteredImageSource = "Images/filter-active.svg";
		/// ]]></code>
		/// </example>
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display in the filter button.")]
		[TypeConverter("Wisej.Web.ImageSourceConverter, Wisej.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageSource
		{
			get { return this._imageSettings == null ? null : this._imageSettings.ImageSource; }
			set { this.ImageSettings.ImageSource = value; }
		}

		private bool ShouldSerializeImage()
		{
			return this._imageSettings == null ? false : this._imageSettings.ShouldSerializeImage();
		}
		private void ResetImage()
		{
			if (this._imageSettings != null) this._imageSettings.ResetImage();
		}
		private bool ShouldSerializeImageSource()
		{
			return this._imageSettings == null ? false : this._imageSettings.ShouldSerializeImageSource();
		}
		private void ResetImageSource()
		{
			if (this._imageSettings != null) this._imageSettings.ResetImageSource();
		}

		/// <summary>
		/// Creates the property manager for the Image properties on first use.
		/// </summary>
		internal virtual ImagePropertySettings FilteredImageSettings
		{
			get
			{
				if (this._filteredImageSettings == null)
					this._filteredImageSettings = new ImagePropertySettings(this);

				return this._filteredImageSettings;
			}
		}
		internal ImagePropertySettings _filteredImageSettings;

		/// <summary>
		/// Returns or sets the image that is displayed in the filter button when
		/// there is an active filter. Can be null.
		/// </summary>
		/// <returns>The <see cref="System.Drawing.Image" /> to display.</returns>
		/// <remarks>
		/// The image is assigned to the filter button of each column that has an active filter when the filters are applied.
		/// When both <see cref="FilteredImage"/> and <see cref="FilteredImageSource"/> are null, the button keeps its current image.
		/// </remarks>
		[Bindable(true)]
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the image that is displayed in the filter button when there is an active filter.")]
		public Image FilteredImage
		{
			get { return this._filteredImageSettings == null ? null : this._filteredImageSettings.Image; }
			set { this.FilteredImageSettings.Image = value; }
		}

		/// <summary>
		/// Returns or sets the theme name or URL for the image to display in the filter button when
		/// there is an active filter. Can be null.
		/// </summary>
		/// <returns>The theme name or URL for the image to display in the filter button.</returns>
		/// <remarks>
		/// Used only when <see cref="FilteredImage"/> is null. The value is assigned to the filter button of each column
		/// that has an active filter when the filters are applied.
		/// </remarks>
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display in the filter button.")]
		[TypeConverter("Wisej.Web.ImageSourceConverter, Wisej.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string FilteredImageSource
		{
			get { return this._filteredImageSettings == null ? null : this._filteredImageSettings.ImageSource; }
			set { this.FilteredImageSettings.ImageSource = value; }
		}

		private bool ShouldSerializeFilteredImage()
		{
			return this._filteredImageSettings == null ? false : this._filteredImageSettings.ShouldSerializeImage();
		}
		private void ResetFilteredImage()
		{
			if (this._filteredImageSettings != null) this._filteredImageSettings.ResetImage();
		}
		private bool ShouldSerializeFilteredImageSource()
		{
			return this._filteredImageSettings == null ? false : this._filteredImageSettings.ShouldSerializeImageSource();
		}
		private void ResetFilteredImageSource()
		{
			if (this._filteredImageSettings != null) this._filteredImageSettings.ResetImageSource();
		}

		/// <summary>
		/// Returns or sets whether the filter panel sorts the items before displaying them.
		/// </summary>
		/// <remarks>
		/// Only supported by <see cref="SimpleColumnFilterPanel"/>: the value is copied to <see cref="SimpleColumnFilterPanel.SortItems"/>
		/// when the panel for a column is created (the first time its filter button is clicked). Changing it later doesn't affect
		/// panels that have already been created.
		/// </remarks>
		/// <since>3.2.6</since>
		[DefaultValue(false)]
		[Wisej.Base.SRCategory("CatBehavior")]
		[Description("Sort items before displaying them. If supported by filter type.")]
		public bool SortItems
		{
			get;
			set;
		} = false;

		/// <summary>
		/// Returns or sets whether the filter button is shown only when the mouse is over the column header.
		/// </summary>
		/// <remarks>
		/// When true, the filter button remains visible while the column has an active filter or its filter panel is open.
		/// </remarks>
		[DefaultValue(false)]
		[Wisej.Base.SRCategory("CatBehavior")]
		[Description("Show the filter button only when the mouse is over the column header.")]
		public bool ShowOnHover
		{
			get => this._showOnHover;
			set
			{
				if (value != this._showOnHover)
				{
					this._showOnHover = value;

					foreach (var col in this.columns)
					{
						var icon = col.HeaderCell.Control as PictureBox;
						if (icon != null)
							icon.Visible = !value;
					}
				}
			}
		}
		private bool _showOnHover;

		/// <summary>
		/// Returns or sets the size of the filter button in the column header, in pixels.
		/// </summary>
		/// <remarks>
		/// The default value is 24 x 24. Changing the value resizes the filter buttons of all the registered columns.
		/// The image is centered in the button and it's not scaled.
		/// </remarks>
		[DefaultValue(typeof(Size), "24, 24")]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Size of the filter button image.")]
		public Size ImageSize
		{
			get => this._imageSize;
			set
			{
				if (value != this._imageSize)
				{
					this._imageSize = value;

					foreach (var col in this.columns)
					{
						var icon = col.HeaderCell.Control as Control;
						if (icon != null)
							icon.Size = value;
					}
				}
			}
		}
		private Size _imageSize = new Size(24, 24);

		#endregion

		#region Methods

		/// <summary>
		/// Returns whether the specified <see cref="DataGridViewColumn"/>
		/// shows the filter button in its header.
		/// </summary>
		/// <param name="column">The <see cref="DataGridViewColumn"/> to query.</param>
		/// <returns>True if the <see cref="DataGridViewColumn"/> shows the filter button.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="column"/> is null.</exception>
		/// <example>
		/// Toggling the filter button of a column:
		/// <code><![CDATA[
		/// var column = this.dataGridView1.Columns["Country"];
		/// this.columnFilter1.SetShowFilter(column, !this.columnFilter1.GetShowFilter(column));
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Returns whether the specified DataGridViewColumn shows the filter button in its header.")]
		public bool GetShowFilter(DataGridViewColumn column)
		{
			if (column == null)
				throw new ArgumentNullException(nameof(column));

			return this.columns.Contains(column);
		}

		/// <summary>
		/// Shows or hides the filter button on a <see cref="DataGridViewColumn"/> header panel.
		/// </summary>
		/// <param name="column">The <see cref="DataGridViewColumn"/> for which to show or hide the filter button.</param>
		/// <param name="show">True to show the filter button or false to remove it.</param>
		/// <exception cref="ArgumentNullException"><paramref name="column"/> is null.</exception>
		/// <remarks>
		/// Showing the filter button replaces the control in the column's header cell (<c>column.HeaderCell.Control</c>) with the control
		/// returned by <see cref="CreateFilterButton"/>. Removing it disposes that control and sets it to null.
		/// If the column was associated with a different <see cref="ColumnFilter"/>, it's detached from it
		/// and its existing filter panel is disposed.
		/// </remarks>
		/// <example>
		/// Showing the filter button on all the columns except the first:
		/// <code><![CDATA[
		/// foreach (DataGridViewColumn column in this.dataGridView1.Columns)
		/// {
		///     this.columnFilter1.SetShowFilter(column, column.Index > 0);
		/// }
		/// ]]></code>
		/// </example>
		[Description("Shows or hides the filter button on a DataGridViewColumn header.")]
		public void SetShowFilter(DataGridViewColumn column, bool show)
		{
			if (column == null)
				throw new ArgumentNullException(nameof(column));

			// detach from the previous ColumnFilter, if any.
			var userData = column.UserData;
			if (userData.ColumnFilter != this)
			{
				userData.FilterPanel?.Dispose();
				userData.ColumnFilter?.SetShowFilter(column, false);
			}

			if (show)
			{
				RegisterColumn(column);
			}
			else
			{
				UnregisterColumn(column);
			}

			if (column.Site?.DesignMode ?? false)
				column.DataGridView?.Invalidate();
		}

		private void RegisterColumn(DataGridViewColumn column)
		{
			if (!this.columns.Contains(column))
			{
				this.columns.Add(column);

				column.Disposed -= this.Column_Disposed;
				column.Disposed += this.Column_Disposed;
				column.UserData.ColumnFilter = this;
				column.HeaderCell.Control = CreateFilterButton(column);

				if (column.DataGridView != null)
				{
					RegisterDataGrid(column.DataGridView);
					column.HeaderCell.Control.Visible = !this.ShowOnHover;
				}
			}
		}

		private void UnregisterColumn(DataGridViewColumn column)
		{
			this.columns.Remove(column);
			column.Disposed -= this.Column_Disposed;
			column.UserData.ColumnFilter = null;
			column.HeaderCell.Control?.Dispose();
			column.HeaderCell.Control = null;
		}

		private void Column_Disposed(object sender, EventArgs e)
		{
			var column = (DataGridViewColumn)sender;
			column.UserData.ColumnFilter?.SetShowFilter(column, false);
		}

		/// <summary>
		/// Creates the filter button to add to the target column's header.
		/// </summary>
		/// <param name="column">The <see cref="DataGridViewColumn"/> that will display the filter button.</param>
		/// <returns>The <see cref="Control"/> to place in the column header. The default implementation returns
		/// a <see cref="PictureBox"/> docked to the right, sized to <see cref="ImageSize"/>, showing <see cref="Image"/> or <see cref="ImageSource"/>.</returns>
		/// <remarks>
		/// Called by <see cref="SetShowFilter"/> when a column is registered. The click and mouse enter/leave handlers that
		/// open the filter panel and implement <see cref="ShowOnHover"/> are attached by the default implementation;
		/// an override should call the base implementation and customize the returned control.
		/// Other members of the library (i.e. <see cref="ColumnFilterPanel.FilterButton"/>) expect the control to be a <see cref="PictureBox"/>.
		/// </remarks>
		/// <example>
		/// Customizing the filter button in a derived extender:
		/// <code><![CDATA[
		/// public class MyColumnFilter : ColumnFilter
		/// {
		///     public override Control CreateFilterButton(DataGridViewColumn column)
		///     {
		///         var button = base.CreateFilterButton(column);
		///         button.ToolTipText = "Filter " + column.HeaderText;
		///         return button;
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public virtual Control CreateFilterButton(DataGridViewColumn column)
		{
			var search = new PictureBox()
			{
				Dock = DockStyle.Right,
				Size = this.ImageSize,
				Cursor = Cursors.Hand,
				SizeMode = PictureBoxSizeMode.CenterImage
			};

			search.UserData.FilterColumn = column;
			search.ImageSource = this.ImageSource;
			search.Image = this.Image != null ? new Bitmap(this.Image) : null;

			search.Click += this.Search_Click;
			search.MouseEnter += this.Search_MouseEnter;
			search.MouseLeave += this.Search_MouseLeave;

			return search;
		}

		private void Search_MouseLeave(object sender, EventArgs e)
		{
			if (this.ShowOnHover)
			{
				var button = (Control)sender;
				button.UserData.Active = false;

				if (button.UserData.Filtered != true && button.UserData.PanelOpen != true)
					button.Visible = false;
			}
		}

		private void Search_MouseEnter(object sender, EventArgs e)
		{
			if (this.ShowOnHover)
			{
				var button = (Control)sender;
				button.Visible = true;
				button.UserData.Active = true;
			}
		}

		private void Search_Click(object sender, EventArgs e)
		{
			var control = (Control)sender;
			var column = (DataGridViewColumn)control.UserData.FilterColumn;
			if (column != null)
				ShowFilterPanel(column);
		}

		private void ShowFilterPanel(DataGridViewColumn column)
		{
			var filterPanel = (ColumnFilterPanel)column.UserData.FilterPanel;

			if (filterPanel == null)
			{
				filterPanel = (ColumnFilterPanel)Activator.CreateInstance(this.FilterPanelType);
				filterPanel.ColumnFilter = this;
				filterPanel.DataGridViewColumn = column;
				column.UserData.FilterPanel = filterPanel;

				if (filterPanel.GetType() == typeof(SimpleColumnFilterPanel))
					((SimpleColumnFilterPanel)filterPanel).SortItems = this.SortItems;
			}

			if (filterPanel.Visible)
				filterPanel.Close();
			else
				filterPanel.ShowPopup(column);
		}

		/// <summary>
		/// Re-applies the filters to the specified <see cref="DataGridView"/>.
		/// </summary>
		/// <param name="dataGridView">The <see cref="DataGridView"/> to filter.</param>
		/// <remarks>
		/// The filters of all the columns of <paramref name="dataGridView"/> are applied using the first
		/// <see cref="ColumnFilterPanel"/> found. Filter panels are created when the user first clicks a filter
		/// button, so this method doesn't do anything until at least one panel exists.
		/// Filters are re-applied automatically when the <see cref="DataGridView"/> is sorted or its data binding completes.
		/// The built-in panels fire the <see cref="RowsFiltered"/> event after applying the filters.
		/// </remarks>
		/// <example>
		/// Re-applying the filters after changing the data in code:
		/// <code><![CDATA[
		/// private void buttonRefresh_Click(object sender, EventArgs e)
		/// {
		///     this.dataGridView1.Rows.Add("Rome", "Italy");
		///     this.columnFilter1.ApplyFilters(this.dataGridView1);
		/// }
		/// ]]></code>
		/// </example>
		public void ApplyFilters(DataGridView dataGridView)
			=> ApplyFiltersInternal(dataGridView);

		internal static void ApplyFiltersInternal(DataGridView dataGridView)
		{
			foreach (DataGridViewColumn col in dataGridView.Columns)
			{
				var filterPanel = col.UserData.FilterPanel as ColumnFilterPanel;
				if (filterPanel != null)
				{
					filterPanel.ApplyFiltersInternal();
					break;
				}
			}
		}

		internal void RegisterDataGrid(DataGridView dataGridView)
		{
			if (!this.dataGrids.Contains(dataGridView))
			{
				this.dataGrids.Add(dataGridView);

				dataGridView.Sorted -= DataGridView_Sorted;
				dataGridView.Disposed -= DataGridView_Disposed;
				dataGridView.CellMouseEnter -= DataGridView_CellMouseEnter;
				dataGridView.CellMouseLeave -= DataGridView_CellMouseLeave;
				dataGridView.DataBindingComplete -= DataGridView_DataBindingComplete;

				dataGridView.Sorted += DataGridView_Sorted;
				dataGridView.Disposed += DataGridView_Disposed;
				dataGridView.CellMouseEnter += DataGridView_CellMouseEnter;
				dataGridView.CellMouseLeave += DataGridView_CellMouseLeave;
				dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
			}
		}

		private static void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			var dataGrid = (DataGridView)sender;
			if (!dataGrid.IsValidColumn(e.ColumnIndex))
				return;

			var column = dataGrid.Columns[e.ColumnIndex];
			var filter = (ColumnFilter)column.UserData.ColumnFilter;

			if (filter != null && filter.ShowOnHover)
			{
				var filterPanel = column.UserData.FilterPanel as ColumnFilterPanel;
				if (filterPanel != null && filterPanel.Visible)
					return;

				var button = column.HeaderCell.Control as PictureBox;
				if (button != null && button.UserData.Filtered != true && button.UserData.PanelOpen != true)
					button.Visible = false;
			}
		}

		private static void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			var dataGrid = (DataGridView)sender;
			if (!dataGrid.IsValidColumn(e.ColumnIndex))
				return;

			var column = dataGrid.Columns[e.ColumnIndex];
			var filter = (ColumnFilter)column.UserData.ColumnFilter;

			if (filter != null && filter.ShowOnHover)
			{
				var button = column.HeaderCell.Control as PictureBox;
				if (button != null)
					button.Visible = true;
			}
		}

		private static void DataGridView_Sorted(object sender, EventArgs e)
		{
			var dataGrid = (DataGridView)sender;
			ApplyFiltersInternal(dataGrid);
		}

		private static void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			var dataGrid = (DataGridView)sender;
			ApplyFiltersInternal(dataGrid);
		}

		private void DataGridView_Disposed(object sender, EventArgs e)
		{
			this.dataGrids.Remove((DataGridView)sender);
		}

		#endregion

		#region Events

		/// <summary>
		/// Event is fired when all filters are applied.
		/// </summary>
		[Browsable(true)]
		[Description("Fired when all filters are applied. Includes number of filtered rows.")]
		public event EventHandler<RowsFilteredEventArg> RowsFiltered;

		/// <summary>
		/// Provides data for the <see cref="RowsFiltered"/> event.
		/// </summary>
		public class RowsFilteredEventArg : EventArgs
		{
			/// <summary>
			/// Number of filtered rows.
			/// </summary>
			public int FilteredRowCount;
		}

		#endregion

		#region IExtenderProvider

		/// <summary>
		/// Returns true if the <see cref="ColumnFilter" /> extender can offer an extender property to the specified target component.
		/// </summary>
		/// <returns>true if the <see cref="ColumnFilter" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="extendee">The target object to add an extender property to. </param>
		bool IExtenderProvider.CanExtend(object extendee)
		{
			return extendee is DataGridViewColumn;
		}

		#endregion

		#region ISupportInitialize

		private int _initCount;

		void ISupportInitialize.BeginInit()
		{
			this._initCount++;
		}

		void ISupportInitialize.EndInit()
		{
			this._initCount--;

			if (this.IsInitialized)
			{
				// update the icons in all bound columns.
				// the icon of the ColumnFilter component 
				// is set in the InitializeComponent method
				// and it may change *after* the creation of the
				// filter button in the column header, ending
				// up using the wrong icon.
				foreach (var col in this.columns)
				{
					var icon = col.HeaderCell.Control as PictureBox;
					if (icon != null)
					{
						icon.Image = this.Image;
						icon.ImageSource = this.ImageSource;
					}
				}
			}
		}

		private bool IsInitialized
		{
			get { return this._initCount == 0; }
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the additional properties for the specified component.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		dynamic IWisejExtenderProvider.RenderDesignMode(IWisejComponent component)
		{
			// don't do anything, implementing IWisejExtenderProvider is enough
			// to have this extender called at design time and add the icon control
			// to the column headers.
			return null;
		}

		/// <summary>
		/// Fires the <see cref="RowsFiltered"/> event.
		/// </summary>
		/// <param name="filteredRowCount">The number of rows that are visible after applying the filters.</param>
		/// <remarks>
		/// Called by <see cref="SimpleColumnFilterPanel"/> and <see cref="WhereColumnFilterPanel"/> after applying the filters.
		/// Custom <see cref="ColumnFilterPanel"/> implementations should call it to notify the application.
		/// </remarks>
		/// <example>
		/// Firing the event from a custom filter panel after the rows have been filtered:
		/// <code><![CDATA[
		/// var dataGrid = this.DataGridViewColumn.DataGridView;
		/// this.ColumnFilter.OnRowsFiltered(dataGrid.Rows.GetRowCount(DataGridViewElementStates.Visible));
		/// ]]></code>
		/// </example>
		public virtual void OnRowsFiltered(int filteredRowCount)
		{
			if (this.RowsFiltered != null)
			{
				var args = new RowsFilteredEventArg { FilteredRowCount = filteredRowCount };
				this.RowsFiltered(this, args);
			}
		}

		#endregion
	}
}
