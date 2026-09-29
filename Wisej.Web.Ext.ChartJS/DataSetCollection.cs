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
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Collection of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects.
	/// Represents the sets of data to plot.
	/// </summary>
	/// <remarks>
	/// The collection is owned by a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control and is available through its
	/// <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.DataSets"/> property; it cannot be created directly.
	/// Any change to the collection (adding, inserting, replacing, removing or clearing data sets) redraws the owner chart.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bar;
	/// this.chartJS1.Labels = new[] { "Mon", "Tue", "Wed" };
	///
	/// var dataSets = this.chartJS1.DataSets;
	/// dataSets.Add("Orders").Data = new object[] { 5, 8, 3 };
	/// dataSets.Add("Returns").Data = new object[] { 1, 0, 2 };
	///
	/// foreach (var dataSet in dataSets)
	///     System.Diagnostics.Debug.WriteLine(dataSet.Label);
	/// ]]></code>
	/// </example>
	[TypeConverter(typeof(DataSetCollection.Converter))]
	[ApiCategory("ChartJS")]
	[Editor("Wisej.Web.Ext.ChartJS3.Design.DataSetCollectionEditor", 
			"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
		/// <param name="index">The zero-based index of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to get or set.</param>
		/// <returns>The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> at the specified index.</returns>
		/// <remarks>
		/// Setting a data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="P:Wisej.Web.Ext.ChartJS.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var first = this.chartJS1.DataSets[0];
		/// first.Label = "Actual";
		///
		/// // replace the second data set.
		/// this.chartJS1.DataSets[1] = new LineDataSet { Label = "Forecast", Data = new object[] { 3, 6, 9 } };
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
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> identified by the label name.
		/// </summary>
		/// <param name="label">The <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Label"/> of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to get or set.</param>
		/// <returns>The first <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> with the specified label, or null if not found.</returns>
		/// <remarks>
		/// The label comparison is case sensitive. When setting, the first data set with the specified label is replaced;
		/// if there is no data set with that label, the new data set is added to the collection. Setting a data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="label"/> is null when setting the value, or the value is null and no data set with the label exists.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var sales = this.chartJS1.DataSets["Sales"];
		/// if (sales != null)
		/// {
		///     sales.Data = new object[] { 40, 55, 32 };
		///     this.chartJS1.UpdateData();
		/// }
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
		/// Returns the number of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects in the collection.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// if (this.chartJS1.DataSets.Count == 0)
		///     this.chartJS1.DataSets.Add("Default").Data = new object[] { 1, 2, 3 };
		/// ]]></code>
		/// </example>
		public int Count
		{
			get { return this.list.Count; }
		}

		/// <summary>
		/// Adds a new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to add to the collection.</param>
		/// <remarks>
		/// Adding a data set redraws the chart. The data set type should match the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/>
		/// of the owner chart; use <see cref="M:Wisej.Web.Ext.ChartJS.DataSetCollection.Add(System.String)"/> to create a matching data set automatically.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new BarDataSet
		/// {
		///     Label = "Visitors",
		///     Data = new object[] { 300, 450, 380 },
		///     BackgroundColor = new[] { Color.SteelBlue, Color.SteelBlue, Color.Orange }
		/// };
		/// this.chartJS1.DataSets.Add(dataSet);
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
		/// Creates and adds a new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to the collection.
		/// </summary>
		/// <param name="name">The name of the new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>, assigned to its <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Label"/>.</param>
		/// <returns>The new <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.</returns>
		/// <remarks>
		/// The type of the new data set matches the <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/> of the owner chart:
		/// i.e. <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/> for <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> or
		/// <see cref="T:Wisej.Web.Ext.ChartJS.PieDataSet"/> for <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/>.
		/// <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bubble"/> and <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Scatter"/> charts
		/// create a <see cref="T:Wisej.Web.Ext.ChartJS.LineDataSet"/>. Adding the data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="name"/> is null or empty.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.ChartType = ChartType.Pie;
		///
		/// var pie = (PieDataSet)this.chartJS1.DataSets.Add("Market Share");
		/// pie.Data = new object[] { 45, 30, 25 };
		/// pie.BackgroundColor = new[] { Color.Red, Color.Green, Color.Blue };
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
		/// private void buttonReset_Click(object sender, EventArgs e)
		/// {
		///     this.chartJS1.DataSets.Clear();
		///     this.chartJS1.Labels = null;
		/// }
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			this.list.Clear();
			Update();
		}

		/// <summary>
		/// Checks if the specified <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> exists in the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to look for.</param>
		/// <returns>true if <paramref name="dataSet"/> is in the collection; otherwise, false.</returns>
		/// <example>
		/// <code><![CDATA[
		/// if (!this.chartJS1.DataSets.Contains(this.targetDataSet))
		///     this.chartJS1.DataSets.Add(this.targetDataSet);
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
		/// <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is too small to hold all the data sets starting at <paramref name="arrayIndex"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var copy = new DataSet[this.chartJS1.DataSets.Count];
		/// this.chartJS1.DataSets.CopyTo(copy, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(DataSet[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Returns the index of the specified <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> in the collection.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to locate.</param>
		/// <returns>The zero-based index of <paramref name="dataSet"/>, or -1 if it is not in the collection.</returns>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     int index = this.chartJS1.DataSets.IndexOf(e.SelectedDataSet);
		///     this.labelInfo.Text = "Data set #" + index;
		/// }
		/// ]]></code>
		/// </example>
		public int IndexOf(DataSet dataSet)
		{
			return this.list.IndexOf(dataSet);
		}

		/// <summary>
		/// Inserts the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="dataSet"/> should be inserted.</param>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to insert.</param>
		/// <remarks>
		/// Inserting a data set redraws the chart.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="P:Wisej.Web.Ext.ChartJS.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// // show the target line as the first data set.
		/// var target = new LineDataSet { Label = "Target", Data = new object[] { 50, 50, 50 } };
		/// this.chartJS1.DataSets.Insert(0, target);
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
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> from the collection and updates the chart.
		/// </summary>
		/// <param name="dataSet">The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to remove.</param>
		/// <returns>true if <paramref name="dataSet"/> was removed; false if it was not in the collection.</returns>
		/// <remarks>
		/// The chart is redrawn even when <paramref name="dataSet"/> is not in the collection.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="dataSet"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var forecast = this.chartJS1.DataSets["Forecast"];
		/// if (forecast != null)
		///     this.chartJS1.DataSets.Remove(forecast);
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
		/// Removes the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> at the specified index from the collection and updates the chart.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="P:Wisej.Web.Ext.ChartJS.DataSetCollection.Count"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// // remove the last data set.
		/// var dataSets = this.chartJS1.DataSets;
		/// if (dataSets.Count > 0)
		///     dataSets.RemoveAt(dataSets.Count - 1);
		/// ]]></code>
		/// </example>
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
			Update();
		}

		/// <summary>
		/// Returns an enumerator that iterates all the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects in the collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1"/> for the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects in the collection.</returns>
		/// <example>
		/// <code><![CDATA[
		/// // hide all the data sets except the first one.
		/// bool first = true;
		/// foreach (DataSet dataSet in this.chartJS1.DataSets)
		/// {
		///     dataSet.Hidden = !first;
		///     first = false;
		/// }
		/// this.chartJS1.Update();
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
