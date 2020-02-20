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
using System.Collections.Generic;
using System.Linq;

namespace Wisej.Web.Ext.DataGridViewSummaryRow
{
	/// <summary>
	/// Adds summary methods to the <see cref="DataGridView"/> control.
	/// </summary>
	public static class DataGridViewSummaryRowExtensions
	{
		#region Methods

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in column <paramref name="groupCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="groupCol">Name of the column that determines the group break values.</param>
		/// <param name="summaryCol">name of the column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, string groupCol, string summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				SummaryRowPosition.Below, 
				groupCol, 
				groupCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in column <paramref name="groupCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="summaryPosition">Indicates the position of the <see cref="DataGridViewSummaryRow"/>.</param>
		/// <param name="groupCol">Name of the column that determines the group break values.</param>
		/// <param name="summaryCol">Name of the column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, SummaryRowPosition summaryPosition, string groupCol, string summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				summaryPosition, 
				groupCol, 
				groupCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in the columns from <paramref name="groupFromCol"/> to <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="groupFromCol">Name of the first column that determines the group break values.</param>
		/// <param name="groupToCol">Name of the last column that determines the group break values.</param>
		/// <param name="summaryCol">Name of the column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, string groupFromCol, string groupToCol, string summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				SummaryRowPosition.Below, 
				groupFromCol, 
				groupToCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in the columns from <paramref name="groupFromCol"/> to <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="summaryPosition">Indicates the position of the <see cref="DataGridViewSummaryRow"/>.</param>
		/// <param name="groupFromCol">Name of the first column that determines the group break values.</param>
		/// <param name="groupToCol">Name of the last column that determines the group break values.</param>
		/// <param name="summaryCol">Name of the column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, SummaryRowPosition summaryPosition, string groupFromCol, string groupToCol, string summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid,
				summaryType,
				summaryPosition,
				String.IsNullOrEmpty(groupFromCol) ? null : grid.Columns[groupFromCol],
				String.IsNullOrEmpty(groupToCol) ? null : grid.Columns[groupToCol],
				String.IsNullOrEmpty(summaryCol) ? null : grid.Columns[summaryCol],
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in columns <paramref name="groupCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="groupCol">Column that determines the group break values.</param>
		/// <param name="summaryCol">Column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, DataGridViewColumn groupCol, DataGridViewColumn summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				SummaryRowPosition.Above, 
				groupCol, 
				groupCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in columns <paramref name="groupCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="summaryPosition">Indicates the position of the <see cref="DataGridViewSummaryRow"/>.</param>
		/// <param name="groupCol">Column that determines the group break values.</param>
		/// <param name="summaryCol">Column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, SummaryRowPosition summaryPosition, DataGridViewColumn groupCol, DataGridViewColumn summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				summaryPosition, 
				groupCol, 
				groupCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in the columns from <paramref name="groupFromCol"/> to <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="groupFromCol">First column that determines the group break values.</param>
		/// <param name="groupToCol">Last column that determines the group break values.</param>
		/// <param name="summaryCol">Column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol, DataGridViewColumn summaryCol, DataGridViewCellStyle style = null)
		{
			return AddSummaryRows(
				grid, 
				summaryType, 
				SummaryRowPosition.Below, 
				groupFromCol, 
				groupToCol, 
				summaryCol,
				style);
		}

		/// <summary>
		/// Creates or updates a <see cref="DataGridViewSummaryRow"/> for each group limited by the
		/// values in the columns from <paramref name="groupFromCol"/> to <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Extension class.</param>
		/// <param name="summaryType">Determines the aggregation type.</param>
		/// <param name="summaryPosition">Indicates the position of the <see cref="DataGridViewSummaryRow"/>.</param>
		/// <param name="groupFromCol">First column that determines the group break values.</param>
		/// <param name="groupToCol">Last column that determines the group break values.</param>
		/// <param name="summaryCol">Column to aggregate.</param>
		/// <param name="style">Optional <see cref="DataGridViewCellStyle"/> for the summary rows.</param>
		/// <returns>Array of the <see cref="DataGridViewSummaryRow"/> rows displaying the aggregated values.</returns>
		public static DataGridViewSummaryRow[] AddSummaryRows(this DataGridView grid, SummaryType summaryType, SummaryRowPosition summaryPosition, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol, DataGridViewColumn summaryCol, DataGridViewCellStyle style = null)
		{
			if (summaryType != SummaryType.None && summaryCol == null)
				throw new ArgumentNullException(nameof(summaryCol));

			lock (grid.Rows)
			{
				var groups = FindSummaryGroups(grid, summaryPosition, groupFromCol, groupToCol);
				if (groups.Length == 0)
					groups = CollectSummaryGroups(grid, summaryPosition, groupFromCol, groupToCol);

				// calculate the specified aggregates.
				var summaryRows = new List<DataGridViewSummaryRow>();
				if (groups.Length > 0)
				{
					for (int i = 0; i < groups.Length; i++)
					{
						// retrieve or create the summary row.
						var summaryRow =
							RetrieveSummaryRow(grid, groups[i], summaryPosition, groupFromCol, groupToCol)
								?? CreateSummaryRow(grid, groups[i], summaryType, summaryPosition, groupFromCol, groupToCol, summaryCol, style);

						// calculate the aggregate value.
						CalculateSummary(summaryRow, summaryType, groups[i], summaryCol);

						summaryRows.Add(summaryRow);
					}

					return summaryRows.ToArray();
				}
			}

			return null;
		}

		/// <summary>
		/// Removes the all the summary rows.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		public static void RemoveSummaryRows(this DataGridView grid)
		{
			RemoveSummaryRows(grid, (DataGridViewColumn)null, (DataGridViewColumn)null);
		}

		/// <summary>
		/// Removes the summary rows that match the specified <paramref name="summaryPosition"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="summaryPosition">Position of the summary rows to remove.</param>
		public static void RemoveSummaryRows(this DataGridView grid, SummaryRowPosition summaryPosition)
		{
			RemoveSummaryRows(grid, summaryPosition, (DataGridViewColumn)null, (DataGridViewColumn)null);
		}

		/// <summary>
		/// Removes the summary rows grouped by
		/// <paramref name="groupFromCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="groupFromCol">Name of the first column that determnines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, string groupFromCol)
		{
			RemoveSummaryRows(grid, groupFromCol, groupFromCol);
		}

		/// <summary>
		/// Removes the summary rows grouped by
		/// <paramref name="groupFromCol"/> and <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="groupFromCol">Name of the first column that determnines the group break values.</param>
		/// <param name="groupToCol">Name of the last column that determines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, string groupFromCol, string groupToCol)
		{
			RemoveSummaryRows(grid, grid.Columns[groupFromCol], grid.Columns[groupToCol]);
		}

		/// <summary>
		/// Removes the summary rows grouped by
		/// <paramref name="groupFromCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="groupFromCol">First column that determnines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, DataGridViewColumn groupFromCol)
		{
			RemoveSummaryRows(grid, groupFromCol, groupFromCol);
		}

		/// <summary>
		/// Removes the summary rows grouped by
		/// <paramref name="groupFromCol"/> and <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="groupFromCol">First column that determnines the group break values.</param>
		/// <param name="groupToCol">Last column that determines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol)
		{
			lock (grid.Rows)
			{
				// clear existing aggregates.
				foreach (SummaryRowPosition position in Enum.GetValues(typeof(SummaryRowPosition)))
				{
					DataGridViewSummaryRow summaryRow = null;
					while ((summaryRow = RetrieveSummaryRow(grid, null, position, groupFromCol, groupToCol)) != null)
					{
						grid.Rows.Remove(summaryRow);
					}
				}
			}
		}

		/// <summary>
		/// Removes the summary rows that match the specified <paramref name="summaryPosition"/> and grouped by
		/// <paramref name="groupFromCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="summaryPosition">Position of the summary rows to remove.</param>
		/// <param name="groupFromCol">Name of the first column that determnines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, SummaryRowPosition summaryPosition, string groupFromCol)
		{
			RemoveSummaryRows(grid, summaryPosition, groupFromCol, groupFromCol);
		}

		/// <summary>
		/// Removes the summary rows that match the specified <paramref name="summaryPosition"/> and grouped by
		/// <paramref name="groupFromCol"/> and <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="summaryPosition">Position of the summary rows to remove.</param>
		/// <param name="groupFromCol">Name of the first column that determnines the group break values.</param>
		/// <param name="groupToCol">Name of the last column that determines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, SummaryRowPosition summaryPosition, string groupFromCol, string groupToCol)
		{
			RemoveSummaryRows(grid, summaryPosition, grid.Columns[groupFromCol], grid.Columns[groupToCol]);
		}

		/// <summary>
		/// Removes the summary rows that match the specified <paramref name="summaryPosition"/> and grouped by
		/// <paramref name="groupFromCol"/> and <paramref name="groupToCol"/>.
		/// </summary>
		/// <param name="grid">Target <see cref="DataGridView"/>.</param>
		/// <param name="summaryPosition">Position of the summary rows to remove.</param>
		/// <param name="groupFromCol">First column that determnines the group break values.</param>
		/// <param name="groupToCol">Last column that determines the group break values.</param>
		public static void RemoveSummaryRows(this DataGridView grid, SummaryRowPosition summaryPosition, DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol)
		{
			lock (grid.Rows)
			{
				// clear existing aggregates.
				DataGridViewSummaryRow summaryRow = null;
				while ((summaryRow = RetrieveSummaryRow(grid, null, summaryPosition, groupFromCol, groupToCol)) != null)
				{
					grid.Rows.Remove(summaryRow);
				}
			}
		}

		#endregion

		#region Aggregations

		private static object CalculateSummarySum(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			object value = null;
			decimal result = 0;

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				try
				{
					result += (Decimal)Convert.ChangeType(value, typeof(Decimal));
				}
				catch { }
			}

			return result;
		}

		private static object CalculateSummaryMin(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			bool anyValue = false;
			object value = null;
			decimal result = Decimal.MaxValue;

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				try
				{
					result = Math.Min(result, (Decimal)Convert.ChangeType(value, typeof(Decimal)));
					anyValue = true;
				}
				catch { }
			}

			if (!anyValue)
				return null;

			return result;
		}

		private static object CalculateSummaryMax(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			bool anyValue = false;
			object value = null;
			decimal result = Decimal.MinValue;

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				try
				{
					result = Math.Max(result, (Decimal)Convert.ChangeType(value, typeof(Decimal)));
					anyValue = true;
				}
				catch { }
			}

			if (!anyValue)
				return null;

			return result;
		}

		private static object CalculateSummaryCount(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			object value = null;
			int result = 0;

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				result++;
			}

			return result;
		}

		private static object CalculateSummaryAverage(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			bool anyValue = false;
			object value = null;
			decimal result = 0;
			int count = 0;

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				try
				{
					result += (Decimal)Convert.ChangeType(value, typeof(Decimal));
					count++;
					anyValue = true;
				}
				catch { }
			}

			if (!anyValue)
				return null;

			return result / count;
		}

		private static object CalculateSummaryStd(DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			int colIndex = summaryCol.Index;

			bool anyValue = false;
			object value = null;
			decimal result = 0;
			int count = 0;
			List<decimal> values = new List<decimal>();

			foreach (var r in group)
			{
				if (r is DataGridViewSummaryRow)
					continue;

				value = r[colIndex].Value;
				if (value == null || Convert.IsDBNull(value))
					continue;

				try
				{
					values.Add((Decimal)Convert.ChangeType(value, typeof(Decimal)));
					count++;
					anyValue = true;
					result += values[values.Count - 1];
				}
				catch { }
			}

			if (!anyValue)
				return null;
		
			decimal mean = result / count;
			result = 0;
			foreach (var v in values)
			{
				result += (v - mean) * (v - mean);
			}
			decimal variance = result / count;
			result = (decimal)Math.Sqrt((double)variance);

			return result;
		}

		#endregion

		#region Implementation

		private static void CalculateSummary(DataGridViewSummaryRow summaryRow, SummaryType summaryType, DataGridViewRow[] group, DataGridViewColumn summaryCol)
		{
			object value = null;
			if (group.Length > 0)
			{
				switch (summaryType)
				{
					case SummaryType.None:
						value = null;
						break;

					case SummaryType.Sum:
						value = CalculateSummarySum(group, summaryCol);
						break;

					case SummaryType.Min:
						value = CalculateSummaryMin(group, summaryCol);
						break;

					case SummaryType.Max:
						value = CalculateSummaryMax(group, summaryCol);
						break;

					case SummaryType.Count:
						value = CalculateSummaryCount(group, summaryCol);
						break;

					case SummaryType.Average:
						value = CalculateSummaryAverage(group, summaryCol);
						break;

					case SummaryType.Std:
						value = CalculateSummaryStd(group, summaryCol);
						break;
				}
			}

			if (summaryCol != null)
			{
				summaryRow[summaryCol].Value = value;
			}
		}

		private static DataGridViewRow[][] FindSummaryGroups(
			DataGridView grid, 
			SummaryRowPosition position, 
			DataGridViewColumn groupFrom, 
			DataGridViewColumn groupTo)
		{
			return grid.Rows
				.Where(r => (r as DataGridViewSummaryRow)?.Match(null, position, groupFrom, groupTo) ?? false)
				.Select(r => ((DataGridViewSummaryRow)r).GroupRows).ToArray();
		}

		private static DataGridViewSummaryRow RetrieveSummaryRow(
			DataGridView grid, 
			DataGridViewRow[] group, 
			SummaryRowPosition summaryPosition, 
			DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol)
		{
			return (DataGridViewSummaryRow)grid.Rows
				.FirstOrDefault(r => (r as DataGridViewSummaryRow)?.Match(group, summaryPosition, groupFromCol, groupToCol) ?? false);
		}

		private static DataGridViewSummaryRow CreateSummaryRow(
			DataGridView grid, 
			DataGridViewRow[] group, 
			SummaryType summaryType, 
			SummaryRowPosition summaryPosition, 
			DataGridViewColumn groupFromCol, DataGridViewColumn groupToCol, 
			DataGridViewColumn summaryCol,
			DataGridViewCellStyle style)
		{
			if (group.Length > 0)
			{
				// create and add the summary row.
				var summaryRow = new DataGridViewSummaryRow(
					group,
					groupFromCol,
					groupToCol,
					summaryPosition);
				
				summaryRow.ReadOnly = true;

				if (style != null)
					summaryRow.DefaultCellStyle = style;

				switch (summaryPosition)
				{
					case SummaryRowPosition.Above:
						grid.Rows.Insert(FindInsertIndex(grid, group, summaryPosition), summaryRow);
						break;

					case SummaryRowPosition.Parent:
						grid.Rows.Insert(FindInsertIndex(grid, group, summaryPosition), summaryRow);
						foreach (var r in group) { r.ParentRow = summaryRow; }
						break;

					case SummaryRowPosition.Below:
					default:
						grid.Rows.Insert(FindInsertIndex(grid, group, summaryPosition), summaryRow);
						break;
				}

				DisplayGroupValues(summaryRow);

				return summaryRow;
			}

			return null;
		}

		private static void DisplayGroupValues(DataGridViewSummaryRow row)
		{
			// copy the group values.
			if (row.GroupRows.Length > 0)
			{
				var groupRow = row.GroupRows[0];
				int colFromIndex = row.GroupFromColumn?.Index ?? 0;
				int colToIndex = row.GroupToColumn?.Index ?? row.CellCount - 1;

				for (var c = colFromIndex; c <= colToIndex; c++)
				{
					if (c >= row.CellCount)
						break;
					if (c >= groupRow.CellCount)
						break;

					row[c].Value = groupRow[c].Value;
				}
			}
		}

		private static int FindInsertIndex(DataGridView grid, DataGridViewRow[] group, SummaryRowPosition summaryPosition)
		{
			var index = 0;
			switch (summaryPosition)
			{
				case SummaryRowPosition.Above:
					{
						index = group[0].Index;
						while (index > 0
							&& grid.Rows[index - 1] is DataGridViewSummaryRow
							&& ((DataGridViewSummaryRow)grid.Rows[index - 1]).SummaryPosition == summaryPosition) index--;
					}
					break;

				case SummaryRowPosition.Below:
					{
						index = group[group.Length - 1].Index + 1;
						while (index < grid.RowCount - 1
							&& grid.Rows[index + 1] is DataGridViewSummaryRow
							&& ((DataGridViewSummaryRow)grid.Rows[index + 1]).SummaryPosition == summaryPosition) index++;
					}
					break;

				case SummaryRowPosition.Parent:
					{
						index = group[0].Index;
					}
					break;
			}

			return index;
		}

		private static DataGridViewRow[][] CollectSummaryGroups(DataGridView grid, SummaryRowPosition position, DataGridViewColumn groupFrom, DataGridViewColumn groupTo)
		{
			List<DataGridViewRow> group = new List<DataGridViewRow>();
			List<DataGridViewRow[]> groups = new List<DataGridViewRow[]>();

			var rows = grid.Rows;
			DataGridViewRow groupRow = null;
			foreach (DataGridViewRow r in rows)
			{
				// skip hidden rows that are not a child row.
				if (!r.Visible && !r.IsChild)
					continue;

				if (r is DataGridViewSummaryRow)
				{
					if (position == SummaryRowPosition.Parent)
					{
						if (group.Count > 0)
						{
							groups.Add(group.ToArray());
							group.Clear();
							groupRow = null;
						}
					}

					continue;
				}

				// first row in group?
				if (groupRow == null)
				{
					groupRow = r;
					group.Add(r);
				}
				else if (IsGroupBreak(groupRow, r, groupFrom, groupTo))
				{
					// create a new group.
					groups.Add(group.ToArray());
					group.Clear();
					groupRow = r;
					group.Add(r);
				}
				else
				{
					// add to current group.
					group.Add(r);
				}
			}

			if (group.Count > 0)
				groups.Add(group.ToArray());

			return groups.ToArray();
		}

		private static bool IsGroupBreak(DataGridViewRow groupRow, DataGridViewRow row, DataGridViewColumn groupFrom, DataGridViewColumn groupTo)
		{
			if (groupFrom != null || groupTo != null)
			{
				DataGridView grid = row.DataGridView;
				int colFromIndex = groupFrom?.Index ?? 0;
				int colToIndex = groupTo?.Index ?? groupRow.CellCount - 1;

				for (var c = colFromIndex; c <= colToIndex; c++)
				{
					if (c >= row.CellCount)
						return true;
					if (c >= groupRow.CellCount)
						return true;

					if (!Object.Equals(groupRow[c].Value, row[c].Value))
						return true;
				}
			}

			return false;
		}

		#endregion
	}
}
