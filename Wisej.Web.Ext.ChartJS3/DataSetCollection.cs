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


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Collection of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects.
	/// Represents the sets of data to plot.
	/// </summary>
	/// <remarks>
	/// The collection is owned by a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control and is available through its <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/> property.
	/// Adding, inserting, replacing or removing data sets redraws the chart. Changing the properties of a data set already in the collection
	/// does not redraw the chart: call <see cref="M:Wisej.Web.Control.Update"/> on the chart, or
	/// <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.UpdateData(System.Int32)"/> when only the data values changed.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// DataSetCollection dataSets = this.chartJS31.DataSets;
	/// dataSets.Clear();
	///
	/// dataSets.Add("2023").Data = new object[] { 50, 60, 70 };
	/// dataSets.Add("2024").Data = new object[] { 55, 72, 81 };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	[TypeConverter(typeof(DataSetCollection.Converter))]
	[Editor("Wisej.Web.Ext.ChartJS3.Design.DataSetCollectionEditor", 
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataSetCollection : IList, IList<DataSet>
	{
		// reference to the ChartJS control that owns this data set collection.
		internal ChartJS3 chart;

		// the inner data collection.
		private List<DataSet> list = new List<DataSet>();

		/// <summary>
		/// Constructs a new instance of the <see cref="Wisej.Web.Ext.ChartJS3.DataSetCollection"/> class.
		/// </summary>
		/// <param name="chart">The <see cref="Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this collection.</param>
		/// <param name="previous">The previous data sets to reload into this new collection, or null.</param>
		internal DataSetCollection(ChartJS3 chart, DataSetCollection previous)
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
		/// Creates a new instance of <see cref="Wisej.Web.Ext.ChartJS3.DataSet"/> specialized according to the
		/// default <see cref="Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> of the <see cref="Wisej.Web.Ext.ChartJS3"/> control.
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
				case ChartType.HorizontalBar:
					return new BarDataSet() { Label = label };
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
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> at the specified position.
		/// </summary>
		/// <param name="index">The zero-based index of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to get or set.</param>
		/// <returns>The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> at the specified <paramref name="index"/>.</returns>
		/// <remarks>
		/// Setting a data set replaces the one at the specified position and redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or equal to or greater than <see cref="P:Wisej.Web.Ext.ChartJS3.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var first = (BarDataSet)this.chartJS31.DataSets[0];
		/// first.BackgroundColor = new[] { Color.SteelBlue };
		///
		/// // replace the second data set.
		/// this.chartJS31.DataSets[1] = new BarDataSet { Label = "Forecast", Data = new object[] { 5, 8, 13 } };
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> identified by the label name.
		/// </summary>
		/// <param name="label">The <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Label"/> of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to get or set.</param>
		/// <returns>The first <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> with the specified <paramref name="label"/>, or null if not found.</returns>
		/// <remarks>
		/// Setting a data set replaces the first data set with the specified label; when there is no data set with that label,
		/// the new data set is added to the collection. The label of the assigned data set is not changed. The chart is redrawn.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="label"/> is null when setting the value, or the value is null and no data set has the specified label.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var sales = this.chartJS31.DataSets["Sales"];
		/// if (sales != null)
		///     sales.Hidden = true;
		///
		/// // adds or replaces the "Target" data set.
		/// this.chartJS31.DataSets["Target"] = new LineDataSet { Label = "Target", Data = new object[] { 100, 100, 100 } };
		/// ]]></code>
		/// </example>
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
		/// Returns the number of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects in the collection.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// if (this.chartJS31.DataSets.Count == 0)
		///     this.chartJS31.DataSets.Add("Empty").Data = new object[0];
		/// ]]></code>
		/// </example>
		public int Count
		{
			get { return this.list.Count; }
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to add to the collection.</param>
		/// <remarks>
		/// Adding a data set redraws the chart. The data set is added as is: use a data set class that matches
		/// the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> of the chart, or <see cref="M:Wisej.Web.Ext.ChartJS3.DataSetCollection.Add(System.String)"/>
		/// to create the matching type automatically.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new LineDataSet
		/// {
		///     Label = "Temperature",
		///     Data = new object[] { 12, 15, 19, 17 },
		///     BorderColor = Color.OrangeRed,
		///     Fill = false
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public void Add(DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			this.list.Add(dataSet);
			Update();
		}

		/// <summary>
		/// Creates and adds a new <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to the collection.
		/// </summary>
		/// <param name="name">The name of the new <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/>, assigned to its <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Label"/>.</param>
		/// <returns>The new <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> added to the collection.</returns>
		/// <remarks>
		/// The type of the new data set matches the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> of the chart:
		/// <see cref="T:Wisej.Web.Ext.ChartJS3.LineDataSet"/> for line, bubble and scatter charts, <see cref="T:Wisej.Web.Ext.ChartJS3.BarDataSet"/> for bar and horizontal bar charts,
		/// and <see cref="T:Wisej.Web.Ext.ChartJS3.PieDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaDataSet"/> or
		/// <see cref="T:Wisej.Web.Ext.ChartJS3.RadarDataSet"/> for the other types. Cast the result to access the type specific properties.
		/// Adding the data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="name"/> is null or empty.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Pie;
		///
		/// var shares = (PieDataSet)this.chartJS31.DataSets.Add("Market Share");
		/// shares.Data = new object[] { 45, 30, 25 };
		/// shares.BackgroundColor = new[] { Color.Gold, Color.Teal, Color.Coral };
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Clearing the collection redraws the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.DataSets.Clear();
		/// this.chartJS31.DataSets.Add("New Data").Data = new object[] { 1, 2, 3 };
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			this.list.Clear();
			Update();
		}

		/// <summary>
		/// Checks if the specified <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> exists in the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to look for.</param>
		/// <returns>true if <paramref name="dataSet"/> is in the collection; otherwise, false.</returns>
		/// <example>
		/// <code><![CDATA[
		/// if (!this.chartJS31.DataSets.Contains(this.forecastDataSet))
		///     this.chartJS31.DataSets.Add(this.forecastDataSet);
		/// ]]></code>
		/// </example>
		public bool Contains(DataSet dataSet)
		{
			return this.list.Contains(dataSet);
		}

		/// <summary>
		/// Copies all data sets to the specified array.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="arrayIndex">The index at which to begin the copy.</param>
		/// <example>
		/// <code><![CDATA[
		/// var copy = new DataSet[this.chartJS31.DataSets.Count];
		/// this.chartJS31.DataSets.CopyTo(copy, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(DataSet[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> in the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to locate in the collection.</param>
		/// <returns>The zero-based index of <paramref name="dataSet"/> in the collection, or -1 if not found.</returns>
		/// <example>
		/// <code><![CDATA[
		/// int index = this.chartJS31.DataSets.IndexOf(this.chartJS31.DataSets["Sales"]);
		/// if (index > -1)
		///     this.chartJS31.DataSets.RemoveAt(index);
		/// ]]></code>
		/// </example>
		public int IndexOf(DataSet dataSet)
		{
			return this.list.IndexOf(dataSet);
		}

		/// <summary>
		/// Inserts the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="dataSet"/> is inserted.</param>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to insert.</param>
		/// <remarks>
		/// Inserting a data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="P:Wisej.Web.Ext.ChartJS3.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var baseline = new BarDataSet { Label = "Baseline", Data = new object[] { 10, 10, 10 } };
		/// this.chartJS31.DataSets.Insert(0, baseline);
		/// ]]></code>
		/// </example>
		public void Insert(int index, DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			this.list.Insert(index, dataSet);
			Update();
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> from the collection and updates the chart.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to remove.</param>
		/// <returns>true if <paramref name="dataSet"/> was removed; false if it was not found in the collection.</returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var target = this.chartJS31.DataSets["Target"];
		/// if (target != null)
		///     this.chartJS31.DataSets.Remove(target);
		/// ]]></code>
		/// </example>
		public bool Remove(DataSet dataSet)
		{
			if (dataSet == null)
				throw new ArgumentNullException("dataSet");

			Update();
			return this.list.Remove(dataSet);
		}

		/// <summary>
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> at the specified index from the collection and updates the chart.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or equal to or greater than <see cref="P:Wisej.Web.Ext.ChartJS3.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// // keep only the most recent data set.
		/// while (this.chartJS31.DataSets.Count > 1)
		///     this.chartJS31.DataSets.RemoveAt(0);
		/// ]]></code>
		/// </example>
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
			Update();
		}

		/// <summary>
		/// Returns an enumerator that iterates all the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects in the collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1"/> for the collection.</returns>
		/// <example>
		/// <code><![CDATA[
		/// foreach (var dataSet in this.chartJS31.DataSets)
		///     dataSet.BorderWidth = 2;
		///
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> at the specified position.
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
			get { return this[index]; }
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

		internal class Converter : TypeConverter
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