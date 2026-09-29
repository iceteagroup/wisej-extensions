///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Observable collection for chart labels that triggers chart updates on changes.
	/// </summary>
	/// <remarks>
	/// The labels map to the Chart.js <c>data.labels</c> array. Inserting, replacing, removing or clearing labels
	/// refreshes the owning <see cref="Chart"/>. The collection can be assigned from a <c>string[]</c> or a
	/// <c>List&lt;string&gt;</c> through the implicit conversion operators.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chart.Labels = new[] { "Q1", "Q2", "Q3" };
	/// chart.Labels.Add("Q4");
	/// chart.Labels[0] = "First Quarter";
	/// ]]></code>
	/// </example>
	public class LabelCollection : ObservableCollection<string>
	{
		/// <summary>
		/// Initializes a new empty label collection.
		/// </summary>
		/// <remarks>The collection is not bound to a chart until it is assigned to <see cref="ChartJS4.Labels"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = new LabelCollection();
		/// labels.Add("Mon");
		/// chart.Labels = labels;
		/// ]]></code>
		/// </example>
		public LabelCollection()
		{
		}

		/// <summary>
		/// Initializes a new empty label collection bound to a chart.
		/// </summary>
		/// <param name="chart">The owning <see cref="ChartJS4"/> control refreshed when the labels change, or <c>null</c>.</param>
		/// <example>
		/// <code><![CDATA[
		/// var labels = new LabelCollection(chart);
		/// chart.Labels = labels;
		/// ]]></code>
		/// </example>
		public LabelCollection(ChartJS4? chart)
		{
			Chart = chart;
		}

		/// <summary>
		/// Initializes a new label collection with initial labels.
		/// </summary>
		/// <param name="items">The initial labels, or <c>null</c> to create an empty collection.</param>
		/// <example>
		/// <code><![CDATA[
		/// var months = new List<string> { "Jan", "Feb", "Mar" };
		/// chart.Labels = new LabelCollection(months);
		/// ]]></code>
		/// </example>
		public LabelCollection(IEnumerable<string>? items)
		{
			if (items == null)
				return;

			foreach (var item in items)
				Add(item);
		}

		/// <summary>
		/// Returns or sets the owning chart.
		/// </summary>
		/// <value>The <see cref="ChartJS4"/> control refreshed when the labels change, or <c>null</c>.</value>
		/// <remarks>Set automatically when the collection is assigned to <see cref="ChartJS4.Labels"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Labels = new[] { "A", "B" };
		/// var owner = chart.Labels.Chart; // == chart
		/// ]]></code>
		/// </example>
		public ChartJS4? Chart { get; set; }

		/// <summary>
		/// Implicitly converts a string array to a label collection.
		/// </summary>
		/// <param name="items">The labels to copy into the new collection.</param>
		/// <returns>A new <see cref="LabelCollection"/> containing <paramref name="items"/>, or <c>null</c> if <paramref name="items"/> is <c>null</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// LabelCollection labels = new[] { "Red", "Green", "Blue" };
		/// chart.Labels = labels;
		/// ]]></code>
		/// </example>
		public static implicit operator LabelCollection?(string[]? items)
		{
			return items == null
				? null
				: new LabelCollection(items);
		}

		/// <summary>
		/// Implicitly converts a string list to a label collection.
		/// </summary>
		/// <param name="items">The labels to copy into the new collection.</param>
		/// <returns>A new <see cref="LabelCollection"/> containing <paramref name="items"/>, or <c>null</c> if <paramref name="items"/> is <c>null</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// var names = new List<string> { "North", "South" };
		/// chart.Labels = names;
		/// ]]></code>
		/// </example>
		public static implicit operator LabelCollection?(List<string>? items)
		{
			return items == null
				? null
				: new LabelCollection(items);
		}

		/// <inheritdoc/>
		protected override void InsertItem(int index, string item)
		{
			base.InsertItem(index, item);
			Chart?.Update();
		}

		/// <inheritdoc/>
		protected override void SetItem(int index, string item)
		{
			base.SetItem(index, item);
			Chart?.Update();
		}

		/// <inheritdoc/>
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
			Chart?.Update();
		}

		/// <inheritdoc/>
		protected override void ClearItems()
		{
			base.ClearItems();
			Chart?.Update();
		}
	}
}
