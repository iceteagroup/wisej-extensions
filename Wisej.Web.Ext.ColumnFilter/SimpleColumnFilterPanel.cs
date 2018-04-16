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
using System.Linq;
using Wisej.Web;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Simple column filter panel showing the unique values found in
	/// the bound column in a checked list box. The user can select
	/// multiple values, clear the selection or select all.
	/// </summary>
	public partial class SimpleColumnFilterPanel : ColumnFilterPanel
	{
		public SimpleColumnFilterPanel()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.items.Items.Clear();
			this.DataGridViewColumn.DataGridView.Rows.CollectionChanged += Rows_CollectionChanged;
		}

		private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			// clear the list when the row collection changes.
			if (e.Action == CollectionChangeAction.Refresh)
				this.items.Items.Clear();
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> is shown
		/// but before it is visible on the client.
		/// </summary>
		protected override void OnBeforeShow()
		{
			// show the loader if we are about to populate the list
			// of values.
			if (this.items.Items.Count == 0 && this.DataGridViewColumn.DataGridView.RowCount > 0)
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
				PopulateList();
			}
			finally
			{
				this.items.ShowLoader = false;
			}
		}

		private void PopulateList()
		{
			var column = this.DataGridViewColumn;
			var colIndex = column.Index;
			var dataGrid = column.DataGridView;

			var visibleValues = dataGrid.Rows
				.Where(r => r.Visible)
				.Select(r => r[colIndex].FormattedValue ?? string.Empty)
				.Distinct();

			var filterItems = this.items.Items;

			// if the list is empty, fill it with all the visible rows.
			if (filterItems.Count == 0)
			{
				foreach (string v in visibleValues)
				{
					this.items.SetItemChecked(
						this.items.Items.Add(v), true);
				}
			}
			else
			{
				// otherwise add only the values that are visible now but
				// missing from the checked listBox.
				foreach (string v in visibleValues)
				{
					var index = filterItems.IndexOf(v);
					if (index == -1)
					{
						this.items.SetItemChecked(
							this.items.Items.Add(v), true);
					}
					else
					{
						this.items.SetItemChecked(index, true);
					}
				}
			}
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
				foreach (var row in dataGrid.Rows)
				{
					row.Visible = true;
				}

				// apply all the filters.
				base.ApplyFilters();
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
			var cellText = "";
			var index = column.Index;
			var dataGrid = column.DataGridView;
			foreach (var row in dataGrid.Rows)
			{
				cellText = row[index].FormattedValue?.ToString() ?? string.Empty;

				if (!checkedItems.Contains(cellText))
					row.Visible = false;
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
			foreach (int i in this.items.CheckedIndices)
			{
				this.items.SetItemChecked(i, false);
			}
		}
	}
}
