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
	public class LabelCollection : ObservableCollection<string>
	{
		/// <summary>
		/// Initializes a new empty label collection.
		/// </summary>
		public LabelCollection()
		{
		}

		/// <summary>
		/// Initializes a new empty label collection bound to a chart.
		/// </summary>
		public LabelCollection(ChartJS4? chart)
		{
			Chart = chart;
		}

		/// <summary>
		/// Initializes a new label collection with initial labels.
		/// </summary>
		public LabelCollection(IEnumerable<string>? items)
		{
			if (items == null)
				return;

			foreach (var item in items)
				Add(item);
		}

		/// <summary>
		/// Gets or sets the owning chart.
		/// </summary>
		public ChartJS4? Chart { get; set; }

		/// <summary>
		/// Implicitly converts a string array to a label collection.
		/// </summary>
		public static implicit operator LabelCollection?(string[]? items)
		{
			return items == null
				? null
				: new LabelCollection(items);
		}

		/// <summary>
		/// Implicitly converts a string list to a label collection.
		/// </summary>
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
