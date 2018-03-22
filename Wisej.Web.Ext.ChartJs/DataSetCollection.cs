///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Collection of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects.
	/// Represents the sets of data to plot.
	/// </summary>
	[TypeConverter(typeof(DataSetCollection.Converter))]
	[Editor(typeof(Design.DataSetCollectionEditor), typeof(UITypeEditor))]
	public class DataSetCollection : IList, IList<DataSet>
	{
		// reference to the ChartJS control that owns this data set collection.
		internal ChartJS chart;

		// the inner data collection.
		private List<DataSet> list = new List<DataSet>();

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSetCollection"/> class.
		/// </summary>
		/// <param name="chart">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this collection.</param>
		/// <param name="previous">The previous data sets to reload into this new collection, or null.</param>
		internal DataSetCollection(ChartJS chart, DataSetCollection previous)
		{
			if (chart == null)
				throw new ArgumentNullException("chart");

			this.chart = chart;

			// preserve the previous data sets.
			if (previous != null)
			{
				foreach (var d in previous)
				{
					DataSet dataSet = CreateDataSet(d.Label);
					dataSet.CopyFrom(d);
					Add(dataSet);
				}
			}
		}

		/// <summary>
		/// Creates a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> specialized according to the
		/// default <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> of the <see cref="T:Wisej.Web.Ext.ChartJS"/> control.
		/// </summary>
		/// <param name="label"></param>
		/// <returns></returns>
		internal DataSet CreateDataSet(string label)
		{
			ChartType type = ChartType.Line;
			if (this.chart != null)
				type = this.chart.ChartType;

			switch (type)
			{
				default:
				case ChartType.Line:
					return new LineDataSet() { Label = label };
				case ChartType.Bar:
					return new BarDataSet() { Label = label };
				case ChartType.HorizontalBar:
					return new HorizontalBarDataSet() { Label = label };
				case ChartType.Doughnut:
					return new DoughnutDataSet() { Label = label };
				case ChartType.Pie:
					return new PieDataSet() { Label = label };
				case ChartType.PolarArea:
					return new PolarAreaDataSet() { Label = label };
				case ChartType.Radar:
					return new RadarDataSet() { Label = label };
			}
		}

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> at the specified position.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public DataSet this[int index]
		{
			get { return this.list[index]; }
			set
			{
				this.list[index] = value;
				Update();
			}
		}

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> identified by the label name.
		/// </summary>
		/// <param name="label"></param>
		/// <returns></returns>
		public DataSet this[string label]
		{
			get { return this.list.Find(o => o.Label == label); }
			set
			{
				if (label == null)
					throw new ArgumentNullException("label");

				int index = this.list.FindIndex(o => o.Label == label);
				if (index > -1)
					this.list[index] = value;
				else
					Add(value);

				Update();
			}
		}

		/// <summary>
		/// Returns the number of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects in the collection.
		/// </summary>
		public int Count
		{
			get { return this.list.Count; }
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to add to the collection.</param>
		public void Add(DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			this.list.Add(dataSet);
			Update();
		}

		/// <summary>
		/// Creates and adds a new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the collection.
		/// </summary>
		/// <param name="name">The name of the new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.</param>
		/// <returns></returns>
		public DataSet Add(string name)
		{
			if (String.IsNullOrEmpty(name))
				throw new ArgumentNullException(name);

			var dataSet = CreateDataSet(name);
			Add(dataSet);
			return dataSet;
		}

		/// <summary>
		/// Removes all data sets.
		/// </summary>
		public void Clear()
		{
			this.list.Clear();
			Update();
		}

		/// <summary>
		/// Checks if the specified <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> exists in the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to look for.</param>
		/// <returns></returns>
		public bool Contains(DataSet dataSet)
		{
			return this.list.Contains(dataSet);
		}

		/// <summary>
		/// Copies all data sets to the specified array.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="arrayIndex">The index at which to begin the copy.</param>
		public void CopyTo(DataSet[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> in the collection.
		/// </summary>
		/// <param name="dataSet"></param>
		/// <returns></returns>
		public int IndexOf(DataSet dataSet)
		{
			return this.list.IndexOf(dataSet);
		}

		/// <summary>
		/// Returns the index of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> in the collection.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="dataSet"></param>
		public void Insert(int index, DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			this.list.Insert(index, dataSet);
			Update();
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> from the collection and updates the chart.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to remove.</param>
		/// <returns></returns>
		public bool Remove(DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			Update();
			return this.list.Remove(dataSet);
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> at the specified index from the collection and updates the chart.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to remove.</param>
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
			Update();
		}

		/// <summary>
		/// Returns an enumerator that iterates all the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects in the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<DataSet> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Updates the related chart when the properties change.
		private void Update()
		{
			if (this.chart != null)
				this.chart.Update();
		}

		#region IList

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> at the specified position.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		DataSet IList<DataSet>.this[int index]
		{
			get { return this.list[index]; }
			set
			{
				this.list[index] = value;
				Update();
			}
		}

		object IList.this[int index]
		{
			get { return this[index];  }
			set { this[index] = (DataSet)value; }
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection<DataSet>.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection.IsSynchronized
		{
			get { return false; }
		}

		object ICollection.SyncRoot
		{
			get { return this; }
		}

		int IList.Add(object value)
		{
			Add((DataSet)value);
			return this.Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((DataSet)value);
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
			return IndexOf((DataSet)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DataSet)value);
		}

		void IList.Remove(object value)
		{
			Remove((DataSet)value);
		}

		#endregion

		#region TypeConverter

		internal class Converter : System.ComponentModel.TypeConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					DataSetCollection dataSets = (DataSetCollection)value;

					if (dataSets == null || dataSets.Count == 0)
						return "0 Data Sets";
					else if (dataSets.Count == 1)
						return "1 Data Set";
					else
						return dataSets.Count + " Data Sets";
				}

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion
	}
}
