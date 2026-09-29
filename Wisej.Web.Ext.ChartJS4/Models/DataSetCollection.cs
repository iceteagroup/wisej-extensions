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
	/// Observable collection for chart datasets that keeps chart back-references in sync.
	/// </summary>
	/// <remarks>
	/// The data sets map to the Chart.js <c>data.datasets</c> array. Items added to the collection are bound to the
	/// owning <see cref="Chart"/> (and unbound when removed or replaced), and every change to the collection refreshes
	/// the chart. The collection can be assigned from a <c>ChartDataSet[]</c> or a <c>List&lt;ChartDataSet&gt;</c>
	/// through the implicit conversion operators.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chart.DataSets.Add(new BarDataSet { Label = "2024", Data = new object[] { 3, 5, 2 } });
	/// chart.DataSets.Add(new BarDataSet { Label = "2025", Data = new object[] { 4, 6, 3 } });
	/// chart.DataSets.RemoveAt(0);
	/// ]]></code>
	/// </example>
	public class DataSetCollection : ObservableCollection<ChartDataSet>
	{
		private ChartJS4? _chart;

		/// <summary>
		/// Initializes a new empty dataset collection.
		/// </summary>
		/// <remarks>The collection is not bound to a chart until it is assigned to <see cref="ChartJS4.DataSets"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSets = new DataSetCollection();
		/// dataSets.Add(new LineDataSet { Label = "Sales" });
		/// chart.DataSets = dataSets;
		/// ]]></code>
		/// </example>
		public DataSetCollection()
		{
		}

		/// <summary>
		/// Initializes a new empty dataset collection bound to a chart.
		/// </summary>
		/// <param name="chart">The owning <see cref="ChartJS4"/> control, or <c>null</c>.</param>
		/// <example>
		/// <code><![CDATA[
		/// var dataSets = new DataSetCollection(chart);
		/// chart.DataSets = dataSets;
		/// ]]></code>
		/// </example>
		public DataSetCollection(ChartJS4? chart)
		{
			Chart = chart;
		}

		/// <summary>
		/// Initializes a new dataset collection with initial items.
		/// </summary>
		/// <param name="items">The initial data sets, or <c>null</c> to create an empty collection.</param>
		/// <example>
		/// <code><![CDATA[
		/// var items = new List<ChartDataSet> { new LineDataSet { Label = "A" }, new LineDataSet { Label = "B" } };
		/// chart.DataSets = new DataSetCollection(items);
		/// ]]></code>
		/// </example>
		public DataSetCollection(IEnumerable<ChartDataSet>? items)
		{
			if (items == null)
				return;

			foreach (var item in items)
				Add(item);
		}

		/// <summary>
		/// Returns or sets the owning chart.
		/// </summary>
		/// <value>The <see cref="ChartJS4"/> control that owns the data sets, or <c>null</c>.</value>
		/// <remarks>
		/// Set automatically when the collection is assigned to <see cref="ChartJS4.DataSets"/>. Changing the value
		/// rebinds the <see cref="ChartModelBase.Chart"/> reference of every data set in the collection.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSets = new DataSetCollection(new[] { new PieDataSet() });
		/// dataSets.Chart = chart; // dataSets[0].Chart == chart
		/// ]]></code>
		/// </example>
		public ChartJS4? Chart
		{
			get => _chart;
			set
			{
				if (ReferenceEquals(_chart, value))
					return;

				foreach (var item in this)
					Unwire(item);

				_chart = value;

				foreach (var item in this)
					Wire(item);
			}
		}

		/// <summary>
		/// Implicitly converts an array to a dataset collection.
		/// </summary>
		/// <param name="items">The data sets to copy into the new collection.</param>
		/// <returns>A new <see cref="DataSetCollection"/> containing <paramref name="items"/>, or <c>null</c> if <paramref name="items"/> is <c>null</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// chart.DataSets = new ChartDataSet[]
		/// {
		///     new LineDataSet { Label = "Min", Data = new object[] { 1, 2, 3 } },
		///     new LineDataSet { Label = "Max", Data = new object[] { 4, 5, 6 } }
		/// };
		/// ]]></code>
		/// </example>
		public static implicit operator DataSetCollection?(ChartDataSet[]? items)
		{
			return items == null
				? null
				: new DataSetCollection(items);
		}

		/// <summary>
		/// Implicitly converts a list to a dataset collection.
		/// </summary>
		/// <param name="items">The data sets to copy into the new collection.</param>
		/// <returns>A new <see cref="DataSetCollection"/> containing <paramref name="items"/>, or <c>null</c> if <paramref name="items"/> is <c>null</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// var list = new List<ChartDataSet> { new BarDataSet { Label = "Units" } };
		/// chart.DataSets = list;
		/// ]]></code>
		/// </example>
		public static implicit operator DataSetCollection?(List<ChartDataSet>? items)
		{
			return items == null
				? null
				: new DataSetCollection(items);
		}

		/// <inheritdoc/>
		protected override void InsertItem(int index, ChartDataSet item)
		{
			base.InsertItem(index, item);
			Wire(item);
			_chart?.Update();
		}

		/// <inheritdoc/>
		protected override void SetItem(int index, ChartDataSet item)
		{
			var oldItem = this[index];
			Unwire(oldItem);
			base.SetItem(index, item);
			Wire(item);
			_chart?.Update();
		}

		/// <inheritdoc/>
		protected override void RemoveItem(int index)
		{
			var oldItem = this[index];
			Unwire(oldItem);
			base.RemoveItem(index);
			_chart?.Update();
		}

		/// <inheritdoc/>
		protected override void ClearItems()
		{
			foreach (var item in this)
				Unwire(item);

			base.ClearItems();
			_chart?.Update();
		}

		private void Wire(ChartDataSet? item)
		{
			if (item != null)
				item.Chart = _chart;
		}

		private void Unwire(ChartDataSet? item)
		{
			if (item != null && item.Chart == _chart)
				item.Chart = null;
		}
	}
}
