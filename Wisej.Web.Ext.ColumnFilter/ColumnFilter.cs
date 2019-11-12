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
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using Wisej.Core;
using System.Collections.Generic;

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Adds a custom filter button to <see cref="DataGridViewColumn"/> to
	/// display a custom filter panel.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ColumnFilter))]
	[ProvideProperty("ShowFilter", typeof(DataGridViewColumn))]
	[Description("Adds a filter button to DataGridViewColumn to display a custom filter panel.")]
	public class ColumnFilter : Wisej.Web.Component, IWisejExtenderProvider, ISupportInitialize
	{
		#region Constructors

		// keeps the list of columns using this filter panel.
		private List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

		/// <summary>
		/// Initializes a new instance of <see cref="ColumnFilter"/>
		/// </summary>
		public ColumnFilter()
		{
			this.ImageSource = "icon-search";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnFilter" /> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="IContainer" />container. </param>
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
		/// Returns or sets the <see cref="ColumnFilterPanel"/> to associate
		/// with this <see cref="ColumnFilter"/> extender.
		/// </summary>
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
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
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
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display in the filter button.")]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
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
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
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
		[Localizable(true)]
		[Wisej.Base.SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display in the filter button.")]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
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

		#endregion

		#region Methods

		/// <summary>
		/// Returns whether the specified <see cref="DataGridViewColumn"/>
		/// shows the filter button in its header.
		/// </summary>
		/// <param name="column">The <see cref="DataGridViewColumn"/> to query.</param>
		/// <returns>True if the <see cref="DataGridViewColumn"/> shows the filter button.</returns>
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
				if (!GetShowFilter(column))
				{
					this.columns.Add(column);
					column.Disposed -= Column_Disposed;
					column.Disposed += Column_Disposed;
					column.UserData.ColumnFilter = this;
					column.HeaderCell.Control = CreateFilterButton(column);
				}
			}
			else
			{
				this.columns.Remove(column);
				column.Disposed -= Column_Disposed;
				column.UserData.ColumnFilter = null;
				column.HeaderCell.Control?.Dispose();
				column.HeaderCell.Control = null;
			}

			if (column.Site?.DesignMode ?? false)
				column.DataGridView?.Invalidate();
		}

		private void Column_Disposed(object sender, EventArgs e)
		{
			var column = (DataGridViewColumn)sender;
			column.UserData.ColumnFilter?.SetShowFilter(column, false);
		}

		// Creates the filter button to add to the target
		// column's header.
		private Control CreateFilterButton(DataGridViewColumn column)
		{
			var search = new PictureBox()
			{
				Dock = DockStyle.Right,
				Size = new System.Drawing.Size(24, 24),
				Cursor = Cursors.Hand
			};

			var me = this;
			search.Click += FilterButton_Click;
			search.UserData.FilterColumn = column;
			search.Image = me.Image;
			search.ImageSource = me.ImageSource;

			return search;
		}

		private void FilterButton_Click(object sender, EventArgs e)
		{
			if (this.FilterPanelType == null)
				throw new InvalidOperationException("FilterPanelType is null.");

			var column = (DataGridViewColumn)((Control)sender).UserData.FilterColumn;
			var filterPanel = (ColumnFilterPanel)column.UserData.FilterPanel;

			if (filterPanel == null)
			{
				filterPanel = (ColumnFilterPanel)Activator.CreateInstance(this.FilterPanelType);
				filterPanel.ColumnFilter = this;
				filterPanel.DataGridViewColumn = column;
				column.UserData.FilterPanel = filterPanel;
			}

			if (filterPanel.Visible)
				filterPanel.Close();
			else
				filterPanel.ShowPopup(column);
		}

		/// <summary>
		/// Applies all the filters on the specified <see cref="DataGridView"/>.
		/// </summary>
		/// <param name="dataGrid">The <see cref="DataGridView"/> with the filters to apply.</param>
		public void ApplyFilters(DataGridView dataGrid)
		{
			if (dataGrid == null)
				throw new ArgumentNullException(nameof(dataGrid));

			foreach (DataGridViewColumn column in dataGrid.Columns)
			{
				if (column.UserData.ColumnFilter == this)
				{
					var panel = column.UserData.FilterPanel as ColumnFilterPanel;
					if (panel != null)
					{
						panel.ApplyFiltersInternal();
						break;
					}
				}
			}
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

		#endregion
	}
}
