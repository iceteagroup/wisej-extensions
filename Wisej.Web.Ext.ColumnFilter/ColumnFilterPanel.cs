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
	[ToolboxItem(false)]
	public partial class ColumnFilterPanel : Wisej.Web.UserPopup
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="ColumnFilterPanel"/>.
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
		[Browsable(false)]
		public DataGridViewColumn DataGridViewColumn
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the control used as the filter button.
		/// </summary>
		[Browsable(false)]
		public PictureBox FilterButton
		{
			get { return this.DataGridViewColumn?.HeaderCell.Control as PictureBox; }
		}

		/// <summary>
		/// Returns the instance of the <see cref="ColumnFilter"/> that is managing this
		/// <see cref="ColumnFilterPanel"/>.
		/// </summary>
		[Browsable(false)]
		public ColumnFilter ColumnFilter
		{
			get;
			internal set;
		}

		#endregion

		#region Implementation

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
			var panels = GetFilterPanels();
			foreach (var panel in panels)
			{
				var columnFilter = panel.ColumnFilter;
				if (panel.OnApplyFilter())
				{
					if (columnFilter.FilteredImage != null)
						panel.FilterButton.Image = columnFilter.FilteredImage;
					else if (columnFilter.FilteredImageSource?.Length > 0)
						panel.FilterButton.ImageSource = columnFilter.FilteredImageSource;
				}
				else
				{
					if (columnFilter.Image != null)
						panel.FilterButton.Image = columnFilter.Image;
					else if (columnFilter.ImageSource.Length > 0)
						panel.FilterButton.ImageSource = columnFilter.ImageSource;
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
			Trace.TraceWarning("OnBeforeShow is not implemented.");
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/> after
		/// is visible on the client.
		/// </summary>
		protected virtual void OnAfterShow()
		{
			Trace.TraceWarning("OnAfterShow is not implemented.");
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/>
		/// should apply the filter criteria selected by the user.
		/// </summary>
		/// <returns>True to indicate that the filter has been applied. False if the filter has been cleared.</returns>
		protected virtual bool OnApplyFilter()
		{
			Trace.TraceWarning("OnApplyFilter is not implemented.");
			return true;
		}

		private void ColumnFilterPanel_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
				OnBeforeShow();
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
		/// Clear the filter
		/// </summary>
		/// <param name="applyFilters"></param>
		public virtual void Clear(bool applyFilters = true)
		{
			Trace.TraceWarning("Clear is not implemented.");
		}

		#endregion
	}
}
