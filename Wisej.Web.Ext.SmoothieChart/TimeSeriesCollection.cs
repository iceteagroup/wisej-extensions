///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using Wisej.Core;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Wisej.Web.Ext.SmoothieChart
{
	/// <summary>
	/// Represents the collection of <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> lines
	/// displayed on the <see cref="T:Wisej.Web.Ext.SmoothieChart.SmoothieChart"/> control.
	/// </summary>
	[ApiCategory("SmoothieChart")]
	public class TimeSeriesCollection : IList<TimeSeries>, IList
	{
		// the owner of this list.
		SmoothieChart owner;

		// inner collection.
		SynchronizedList<TimeSeries> list;

		internal TimeSeriesCollection(SmoothieChart owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			this.owner = owner;
			this.IsNew = true;
			this.list = new SynchronizedList<TimeSeries>(syncLock: this);
		}

		/// <summary>
		/// Returns or sets the dirty state for the list.
		/// 
		/// When true, the list of items must be sent back to the client.
		/// </summary>
		internal bool IsDirty { get; set; }

		/// <summary>
		/// Returns or sets the new state for the list.
		/// 
		/// When true, the entire list is cleared and reloaded on the client.
		/// </summary>
		internal bool IsNew { get; set; }

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public TimeSeries this[int index]
		{
			get { return this.list[index]; }
			set
			{
				this.list[index] = value;
				this.IsDirty = true;

				Update();
			}
		}

		/// <summary>
		/// Returns the number of <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> objects in the collection.
		/// </summary>
		public int Count
		{
			get { return this.list.Count; }
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to the collection.
		/// </summary>
		/// <param name="series">The <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to add to the collection.</param>
		public void Add(TimeSeries series)
		{
			if (series == null)
				throw new ArgumentNullException("series");

			series.Owner = this.owner;
			this.list.Add(series);
			this.IsDirty = true;

			Update();
		}

		/// <summary>
		/// Removes all data sets.
		/// </summary>
		public void Clear()
		{
			this.list.Clear();
			this.IsDirty = true;

			Update();
		}

		/// <summary>
		/// Checks if the specified <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> exists in the collection.
		/// </summary>
		/// <param name="series">The <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to look for.</param>
		/// <returns></returns>
		public bool Contains(TimeSeries series)
		{
			return this.list.Contains(series);
		}

		/// <summary>
		/// Copies all data sets to the specified array.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="arrayIndex">The index at which to begin the copy.</param>
		public void CopyTo(TimeSeries[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> in the collection.
		/// </summary>
		/// <param name="series">The <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to look for.</param>
		/// <returns></returns>
		public int IndexOf(TimeSeries series)
		{
			return this.list.IndexOf(series);
		}

		/// <summary>
		/// Returns the index of the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> in the collection.
		/// </summary>
		/// <param name="index">The index where the new <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> should be insert at.</param>
		/// <param name="series">The <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to insert in the collection.</param>
		public void Insert(int index, TimeSeries series)
		{
			if (series == null)
				throw new ArgumentNullException("series");

			this.IsDirty = true;
			series.Owner = this.owner;
			this.list.Insert(index, series);
			Update();
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> from the collection and updates the chart.
		/// </summary>
		/// <param name="series">The <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to remove.</param>
		/// <returns></returns>
		public bool Remove(TimeSeries series)
		{
			if (series == null)
				throw new ArgumentNullException("series");

			var removed = false;
			series.Owner = null;
			this.IsDirty = true;
			removed = this.list.Remove(series);
			Update();
			return removed;
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> at the specified index from the collection and updates the chart.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> to remove.</param>
		public void RemoveAt(int index)
		{
			this.list[index].Owner = null;
			this.list.RemoveAt(index);
			this.IsDirty = true;
			Update();
		}

		/// <summary>
		/// Returns an enumerator that iterates all the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> objects in the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TimeSeries> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Updates the related chart when the properties change.
		private void Update()
		{
			if (this.owner != null)
				this.owner.Update();
		}

		#region IList

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> at the specified position.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		TimeSeries IList<TimeSeries>.this[int index]
		{
			get { return this.list[index]; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this.owner;
				this.list[index] = value;
				Update();
			}
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (TimeSeries)value; }
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection<TimeSeries>.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection.IsSynchronized
		{
			get { return true; }
		}

		object ICollection.SyncRoot
		{
			get { return this; }
		}

		int IList.Add(object value)
		{
			Add((TimeSeries)value);
			return this.Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((TimeSeries)value);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((IList)this.list).CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((TimeSeries)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (TimeSeries)value);
		}

		void IList.Remove(object value)
		{
			Remove((TimeSeries)value);
		}

		#endregion

		#region TypeConverter

		internal class Converter : System.ComponentModel.TypeConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					TimeSeriesCollection lines = (TimeSeriesCollection)value;

					if (lines == null)
						return "0 Time Series";
					else
						return lines.Count + " Time Series";
				}

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the list to the json definition for the client.
		/// </summary>
		/// <returns></returns>
		internal object Render()
		{
			lock (this)
			{
				object config = null;
				IWisejControl chart = this.owner;

				if (!chart.DesignMode)
				{
					// render the full list?
					bool isNew = (this.IsNew || chart.IsNew);
					if (!isNew && !this.IsDirty)
						return null;
				}

				// reset the state of the list.
				this.IsNew = false;
				this.IsDirty = false;

				config = this.list.ToArray();
				return config;
			}
		}

		#endregion
	}
}
