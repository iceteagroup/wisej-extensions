///////////////////////////////////////////////////////////////////////////////
//
// (C) 2019 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.DataGridViewSummaryRow
{
	/// <summary>
	/// Represents a summary row in a <see cref="DataGridView"/> control.
	/// </summary>
	public class DataGridViewSummaryRow : DataGridViewRow
	{
		internal DataGridViewSummaryRow(DataGridViewRow[] group, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol, SummaryRowPosition summaryPosition)
		{
			if (group == null)
				throw new ArgumentNullException(nameof(group));

			this.GroupRows = group;
			this.GroupToColumn = groupToCol;
			this.GroupFromColumn = groupFromCol;
			this.SummaryPosition = summaryPosition;
		}

		/// <summary>
		/// Returns the array of rows that have been aggregated in this <see cref="DataGridViewSummaryRow"/>.
		/// </summary>
		public DataGridViewRow[] GroupRows
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the first <see cref="DataGridViewColumn"/> used to determine the group breaks.
		/// </summary>
		public DataGridViewColumn GroupFromColumn
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the last <see cref="DataGridViewColumn"/> used to determine the group breaks.
		/// </summary>
		public DataGridViewColumn GroupToColumn
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the <see cref="SummaryRowPosition"/> of this <see cref="DataGridViewSummaryRow"/>.
		/// </summary>
		public SummaryRowPosition SummaryPosition
		{
			get;
			private set;
		}

		internal bool Match(DataGridViewRow[] group, SummaryRowPosition summaryPosition, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol)
		{
			return
				this.SummaryPosition == summaryPosition
				&& (groupToCol == null || this.GroupToColumn == groupToCol)
				&& (groupFromCol == null || this.GroupFromColumn == groupFromCol)
				&& MatchGroupRows(group);
		}

		private bool MatchGroupRows(DataGridViewRow[] group)
		{
			if (group == null || group.Length == 0)
				return true;

			for (int i = 0; i < group.Length; i++)
			{
				if (i >= this.GroupRows.Length)
					return false;

				if (group[i] != this.GroupRows[i])
					return false;
			}

			return true;
		}
	}
}