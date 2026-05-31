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

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Simple column filter panel showing the unique values found in
	/// the bound column in a checked list box. The user can select
	/// multiple values, clear the selection or select all.
	/// </summary>
	[ToolboxItem(false)]
	[ApiCategory("ColumnFilter")]
	public partial class SimpleColumnFilterPanel : ColumnFilterPanel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleColumnFilterPanel"/> class.
		/// </summary>
		public SimpleColumnFilterPanel()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Performs initialization tasks when the panel is created.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.items.Items.Clear();
			this.Disposed += this.SimpleColumnFilterPanel_Disposed;
			this.DataGridViewColumn.DataGridView.Sorted += this.Rows_Sorted;
			this.DataGridViewColumn.DataGridView.Rows.CollectionChanged += this.Rows_CollectionChanged;

			this.items.VirtualScroll = true;
		}

		private void SimpleColumnFilterPanel_Disposed(object sender, EventArgs e)
		{
			if (this.DataGridViewColumn != null && this.DataGridViewColumn.DataGridView != null)
			{
				this.DataGridViewColumn.DataGridView.Sorted -= this.Rows_Sorted;
				this.DataGridViewColumn.DataGridView.Rows.CollectionChanged -= this.Rows_CollectionChanged;
			}
		}

		private void Rows_Sorted(object sender, EventArgs e)
		{
			// refresh the list when the row collection changes.
			this.reloadItems = true;
		}

		private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			// refresh the list when the row collection changes.
			this.reloadItems = true;
		}
		private bool reloadItems = false;

		/// <summary>
		/// If true sorts the items alphabetically.
		/// </summary>
		/// <since>3.2.6</since>
		[DefaultValue(false)]
		public bool SortItems
		{
			get;
			set;
		} = false;

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> is shown
		/// but before it is visible on the client.
		/// </summary>
		protected override void OnBeforeShow()
		{
			// show the loader if we are about to populate the list of values.
			if (this.reloadItems && this.DataGridViewColumn.DataGridView.RowCount > 0)
				this.items.ShowLoader = true;
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> after
		/// is visible on the client.
		/// </summary>
		protected override void OnAfterShow()
		{
			try
			{
				if (this.reloadItems)
					PopulateList();
			}
			finally
			{
				this.items.ShowLoader = false;
			}
		}

		private void PopulateList()
		{
			var selection = this.items.Text;
			this.items.Items.Clear();

			var column = this.DataGridViewColumn;
			var colIndex = column.Index;
			var dataGrid = column.DataGridView;

			var filterItems = this.items.Items;

			if (filterItems.Count == 0)
			{
				var text = string.Empty;
				for (var rowIndex = 0; rowIndex < dataGrid.RowCount; rowIndex++)
				{
					if (rowIndex == dataGrid.NewRowIndex)
						break;

					text = Convert.ToString(dataGrid.GetFormattedValue(colIndex, rowIndex));
					if (text != string.Empty)
					{
						if (!filterItems.Contains(text))
						{
							var visible = dataGrid.GetRowState(rowIndex).HasFlag(DataGridViewElementStates.Visible);
							filterItems.Add(text, visible);
						}
					}
				}
			}
			else
			{
				var text = string.Empty;
				for (var rowIndex = 0; rowIndex < dataGrid.RowCount; rowIndex++)
				{
					text = Convert.ToString(dataGrid.GetFormattedValue(colIndex, rowIndex));
					if (text != string.Empty)
					{
						var index = filterItems.IndexOf(text);
						var visible = dataGrid.GetRowState(rowIndex).HasFlag(DataGridViewElementStates.Visible);
						if (index > -1)
						{
							this.items.SetItemChecked(index, visible);
						}
						else
						{
							filterItems.Add(text, visible);
						}
					}
				}
			}

			this.items.Text = selection;
			this.items.Sorted = this.SortItems;
		}

		/// <summary>
		/// Applies the filters registered with each
		/// column in the order of creation in the <see cref="DataGridView.Columns"/> collection.
		/// </summary>
		protected override void ApplyFilters()
		{
			try
			{
				// make all rows visible before applying the filters.
				var dataGrid = this.DataGridViewColumn.DataGridView;
				for (var rowIndex = 0; rowIndex < dataGrid.RowCount; rowIndex++)
				{
					dataGrid.SetRowState(rowIndex, DataGridViewElementStates.Visible, true);
				}

				// reset current cell 
				dataGrid.CurrentCell = null;

				// remove all summary rows.
				dataGrid.RemoveSummaryRows();

				// apply all the filters.
				base.ApplyFilters();

				if (this.ColumnFilter != null)
				{
					this.ColumnFilter.OnRowsFiltered(
						dataGrid.Rows.GetRowCount(DataGridViewElementStates.Visible));
				}
			}
			finally
			{
				Close();
			}
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/>
		/// should apply the filter criteria selected by the user.
		/// </summary>
		/// <returns>True to indicate that the filter has been applied. False if the filter has been cleared.</returns>
		protected override bool OnApplyFilter()
		{
			var column = this.DataGridViewColumn;
			var filterItems = this.items.Items;
			var checkedItems = this.items.CheckedItems;

			// determine when to show all.
			var showAll =
				checkedItems.Count == 0
				|| checkedItems.Count == filterItems.Count;

			if (showAll)
				return false;

			// filter the rows in the datagrid using a simple string comparison.
			var colIndex = column.Index;
			var dataGrid = column.DataGridView;

			for (var rowIndex = 0; rowIndex < dataGrid.RowCount; rowIndex++)
			{
				if (rowIndex == dataGrid.NewRowIndex)
					break;

				var cellText = Convert.ToString(dataGrid.GetFormattedValue(colIndex, rowIndex));

				if (!checkedItems.Contains(cellText))
					dataGrid.SetRowState(rowIndex, DataGridViewElementStates.Visible, false);
			}

			return true;
		}

		private void selectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0, count = this.items.Items.Count; i < count; i++)
			{
				this.items.SetItemChecked(i, true);
			}
		}

		private void clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clear(false);
		}

		/// <summary>
		/// Clear the filter panel
		/// </summary>
		/// <param name="applyFilters"></param>
		public override void Clear(bool applyFilters = true)
		{
			foreach (int i in this.items.CheckedIndices)
			{
				this.items.SetItemChecked(i, false);
			}
			if (applyFilters)
			{
				ApplyFilters();
			}
		}
	}
}
