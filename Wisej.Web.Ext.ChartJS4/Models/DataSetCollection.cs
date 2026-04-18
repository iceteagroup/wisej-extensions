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
	public class DataSetCollection : ObservableCollection<ChartDataSet>
	{
		private ChartJS4? _chart;

		/// <summary>
		/// Initializes a new empty dataset collection.
		/// </summary>
		public DataSetCollection()
		{
		}

		/// <summary>
		/// Initializes a new empty dataset collection bound to a chart.
		/// </summary>
		public DataSetCollection(ChartJS4? chart)
		{
			Chart = chart;
		}

		/// <summary>
		/// Initializes a new dataset collection with initial items.
		/// </summary>
		public DataSetCollection(IEnumerable<ChartDataSet>? items)
		{
			if (items == null)
				return;

			foreach (var item in items)
				Add(item);
		}

		/// <summary>
		/// Gets or sets the owning chart.
		/// </summary>
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
		public static implicit operator DataSetCollection?(ChartDataSet[]? items)
		{
			return items == null
				? null
				: new DataSetCollection(items);
		}

		/// <summary>
		/// Implicitly converts a list to a dataset collection.
		/// </summary>
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
