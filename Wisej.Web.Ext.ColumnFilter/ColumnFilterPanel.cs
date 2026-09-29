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
using System.Diagnostics;
using Wisej.Core;

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Base class for the filter panels to use in conjunction with the
	/// <see cref="ColumnFilter"/> extender.
	/// </summary>
	/// <remarks>
	/// The panel is a <see cref="UserPopup"/> that the <see cref="ColumnFilter"/> creates for each column the first time
	/// the user clicks the column's filter button. Pressing Enter applies the filters of all the columns,
	/// pressing Escape closes the panel. Derived classes implement <see cref="OnApplyFilter"/> to hide the rows
	/// that don't match the criteria selected by the user. Note that the base class doesn't make the rows visible again
	/// before applying the filters and doesn't close the panel: the built-in panels do it in their override of <c>ApplyFilters</c>.
	/// </remarks>
	/// <example>
	/// A custom filter panel that shows only the rows with a non-empty value:
	/// <code><![CDATA[
	/// public class NotEmptyFilterPanel : ColumnFilterPanel
	/// {
	///     protected override bool OnApplyFilter()
	///     {
	///         var column = this.DataGridViewColumn;
	///         var dataGrid = column.DataGridView;
	///         foreach (var row in dataGrid.Rows)
	///         {
	///             if (!row.IsNewRow && string.IsNullOrEmpty(Convert.ToString(row[column.Index].Value)))
	///                 row.Visible = false;
	///         }
	///         return true;
	///     }
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(false)]
	[ApiCategory("ColumnFilter")]
	public partial class ColumnFilterPanel : Wisej.Web.UserPopup
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnFilterPanel"/> class.
		/// </summary>
		public ColumnFilterPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the <see cref="DataGridViewColumn"/> bound
		/// to this <see cref="ColumnFilterPanel"/>.
		/// </summary>
		/// <remarks>
		/// Set by the <see cref="ColumnFilter"/> when it creates the panel; it's null after the panel is disposed.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewColumn DataGridViewColumn
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the control used as the filter button.
		/// </summary>
		/// <remarks>
		/// Returns the control in the header cell of <see cref="DataGridViewColumn"/>, or null if it's not a <see cref="PictureBox"/>
		/// (i.e. when <see cref="Wisej.Web.Ext.ColumnFilter.ColumnFilter.CreateFilterButton"/> has been overridden to return a different control).
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PictureBox FilterButton
		{
			get { return this.DataGridViewColumn?.HeaderCell.Control as PictureBox; }
		}

		/// <summary>
		/// Returns the instance of the <see cref="ColumnFilter"/> that is managing this
		/// <see cref="ColumnFilterPanel"/>.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ColumnFilter ColumnFilter
		{
			get;
			internal set;
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Performs initialization tasks when the panel is created.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.ColumnFilter.RegisterDataGrid(this.DataGridViewColumn.DataGridView);
		}

		private void Rows_Sorted(object sender, EventArgs e)
		{

			// re-apply the filter if the datasource did its own sorting.

			var grid = (DataGridView)sender;
			var dataSource = grid.DataSource != null
				? grid.BindingContext[grid.DataSource, grid.DataMember]
				: null;

			if (dataSource != null && dataSource is CurrencyManager manager &&
				manager.List is IBindingList bindingList && bindingList.SupportsSorting)
			{
				ApplyFilters();
			}
		}

		/// <summary>
		/// Returns all the <see cref="ColumnFilterPanel"/> panels
		/// associated to the columns of the <see cref="DataGridView"/> that
		/// owns the <see cref="DataGridViewColumn"/> linked to the
		/// calling panel.
		/// </summary>
		/// <returns></returns>
		protected ColumnFilterPanel[] GetFilterPanels()
		{
			var list = new List<ColumnFilterPanel>();
			var dataGrid = this.DataGridViewColumn.DataGridView;
			foreach (DataGridViewColumn column in dataGrid.Columns)
			{
				var panel = column.UserData.FilterPanel;
				if (panel != null)
					list.Add(panel);
			}

			return list.ToArray();
		}

		/// <summary>
		/// Applies the filters registered with each
		/// column in the order of creation in the <see cref="DataGridView.Columns"/> collection.
		/// </summary>
		protected virtual void ApplyFilters()
		{
			ColumnFilter columnFilter = null;
			var panels = GetFilterPanels();
			foreach (var panel in panels)
			{
				columnFilter = panel.ColumnFilter;
				var button = panel.FilterButton;

				if (button != null)
				{
					if (panel.OnApplyFilter())
					{
						if (columnFilter.FilteredImage != null)
							button.Image = columnFilter.FilteredImage;
						else if (columnFilter.FilteredImageSource?.Length > 0)
							button.ImageSource = columnFilter.FilteredImageSource;

						button.UserData.Filtered = true;
					}
					else
					{
						if (columnFilter.Image != null)
							button.Image = columnFilter.Image;
						else if (columnFilter.ImageSource.Length > 0)
							button.ImageSource = columnFilter.ImageSource;

						button.UserData.Filtered = false;
					}
				}
			}
		}

		/// <summary>
		/// Applies the filter.
		/// </summary>
		internal void ApplyFiltersInternal()
		{
			ApplyFilters();
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> is shown
		/// but before it is visible on the client.
		/// </summary>
		protected virtual void OnBeforeShow()
		{
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> after
		/// is visible on the client.
		/// </summary>
		protected virtual void OnAfterShow()
		{
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/>
		/// should apply the filter criteria selected by the user.
		/// </summary>
		/// <returns>True to indicate that the filter has been applied. False if the filter has been cleared.</returns>
		protected virtual bool OnApplyFilter()
		{
			return true;
		}

		private void ColumnFilterPanel_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
				OnBeforeShow();

			if (this.ColumnFilter.ShowOnHover)
			{
				var button = this.FilterButton;
				if (button != null)
				{
					button.UserData.PanelOpen = this.Visible;

					if (!this.Visible && button.UserData.Filtered != true && button.UserData.Active != true)
						button.Visible = false;
				}
			}
		}

		private void ColumnFilterPanel_Accelerator(object sender, AcceleratorEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Return:
					ok_Click(this.ok, EventArgs.Empty);
					break;

				case Keys.Escape:
					cancel_Click(this.cancel, EventArgs.Empty);
					break;
			}
		}

		private void ok_Click(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "appear":
					// initialize the panel after it has been displayed.
					OnAfterShow();
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			// get notified when the panel is visible on the client.
			config.wiredEvents.Add("appear");
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			if (disposing)
			{
				var column = this.DataGridViewColumn;
				if (column != null)
				{
					column.HeaderCell.Control?.Dispose();
					column.HeaderCell.Control = null;
					column.UserData.FilterPanel = null;
				}

				this.ColumnFilter = null;
				this.DataGridViewColumn = null;
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Clears the filter criteria of this panel.
		/// </summary>
		/// <param name="applyFilters">True (default) to re-apply the filters of all the columns after clearing the criteria.</param>
		/// <remarks>
		/// The base implementation doesn't do anything; derived panels override it to reset their controls.
		/// </remarks>
		/// <example>
		/// Clearing the filters of all the columns of a <see cref="DataGridView"/>:
		/// <code><![CDATA[
		/// foreach (DataGridViewColumn column in this.dataGridView1.Columns)
		/// {
		///     var panel = column.UserData.FilterPanel as ColumnFilterPanel;
		///     panel?.Clear(false);
		/// }
		/// this.columnFilter1.ApplyFilters(this.dataGridView1);
		/// ]]></code>
		/// </example>
		public virtual void Clear(bool applyFilters = true)
		{
		}

		#endregion
	}
}
