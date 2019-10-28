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

namespace Wisej.Web.Ext.DataGridViewSummaryRow
{

	/// <summary>
	/// Determines the aggregation result to display in the <see cref="DataGridViewSummaryRow"/>.
	/// </summary>
	public enum SummaryType
	{
		/// <summary>
		/// The summary row is created without any aggregation result. Can be used
		/// to automatically group and nest rows.
		/// </summary>
		None,

		/// <summary>
		/// Adds the values in the group.
		/// </summary>
		Sum,

		/// <summary>
		/// Counts the non-empty cells in the group.
		/// </summary>
		Count,

		/// <summary>
		/// Calculates the average value of the non-empty cells in the group.
		/// </summary>
		Average,

		/// <summary>
		/// Calculates the minimum value in the group.
		/// </summary>
		Min,

		/// <summary>
		/// Calculates the maximum value in the group.
		/// </summary>
		Max,

		/// <summary>
		/// Calculates the standard deviation of the values in the group.
		/// </summary>
		Std
	}

	/// <summary>
	/// Determines the location where to insert the <see cref="DataGridViewSummaryRow"/>.
	/// </summary>
	public enum SummaryRowPosition
	{
		/// <summary>
		/// The summary row is inserted above the group.
		/// </summary>
		Above,

		/// <summary>
		///  The summary row is inserted below the group.
		/// </summary>
		Below,

		/// <summary>
		/// The summary row is created a the parent row of the group.
		/// </summary>
		Parent
	}
}
