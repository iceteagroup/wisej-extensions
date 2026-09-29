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
using System.Collections.Generic;

namespace Wisej.Web.Ext.ChartJS3
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartClick" /> event in a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/>.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs" /> that contains the event data.</param>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartClick += new ChartClickEventHandler(this.chartJS31_ChartClick);
	///
	/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
	/// {
	///     var chart = (ChartJS3)sender;
	///     this.labelInfo.Text = $"Clicked {e.DataPoints.Length} point(s) on {chart.Name}.";
	/// }
	/// ]]></code>
	/// </example>
	public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartClick" /> event of the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3" /> control.
	/// </summary>
	/// <remarks>
	/// The arrays <see cref="P:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs.DataSets"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs.DataPoints"/>
	/// and <see cref="P:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs.Values"/> have the same length: the element at the same index in each array
	/// describes one chart element under the click point.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
	/// {
	///     for (int i = 0; i < e.DataSets.Length; i++)
	///     {
	///         string label = this.chartJS31.Labels[e.DataPoints[i]];
	///         this.listBox1.Items.Add($"{e.DataSets[i].Label} / {label} = {e.Values[i]}");
	///     }
	/// }
	/// ]]></code>
	/// </example>
	public class ChartClickEventArgs: EventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs"/>.
		/// </summary>
		/// <param name="chart"></param>
		/// <param name="e"></param>
		internal ChartClickEventArgs(ChartJS3 chart, WidgetEventArgs e)
		{
			dynamic[] points = e.Data.data;
			List<int> dataPoints = new List<int>();
			List<object> values = new List<object>();
			List<DataSet> dataSets = new List<DataSet>();

			if (points != null)
			{
				// collect the points, values and datasets in the click range.
				foreach (dynamic point in points)
				{
					int pointIndex = point.pointIndex;
					int dataSetIndex = point.dataSetIndex;

					dataPoints.Add(pointIndex);
					dataSets.Add(chart.DataSets[dataSetIndex]);
					values.Add(chart.DataSets[dataSetIndex].Data[pointIndex]);
				}
			}

			this.DataSets = dataSets.ToArray();
			this.DataPoints = dataPoints.ToArray();
			this.Values = new object[this.DataPoints.Length];
			for (int i = 0; i < this.Values.Length; i++)
				this.Values[i] = this.DataSets[i].Data[this.DataPoints[i]];
		}

		/// <summary>
		/// Returns the values of the data points in the click radius.
		/// </summary>
		/// <value>
		/// An array of objects. Each element is the value taken from the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> array of the data set at the same
		/// index in <see cref="P:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs.DataSets"/>, at the position at the same index in <see cref="P:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs.DataPoints"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     var total = e.Values.Sum(v => Convert.ToDouble(v));
		///     AlertBox.Show("Total: " + total);
		/// }
		/// ]]></code>
		/// </example>
		public object[] Values
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the indexes of the data points in the click radius.
		/// </summary>
		/// <value>
		/// An array of zero-based indexes into the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> array of the corresponding data set, which is also
		/// the index of the matching label in <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Labels"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     int index = e.DataPoints[0];
		///     this.labelMonth.Text = this.chartJS31.Labels[index];
		/// }
		/// ]]></code>
		/// </example>
		public int[] DataPoints
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the data sets in the click radius.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects from <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/>, one for each chart element under the click point.
		/// The same data set appears more than once when more than one of its data points is under the click point.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     // toggle the clicked data set.
		///     var dataSet = e.DataSets[0];
		///     dataSet.Hidden = !dataSet.Hidden;
		///     this.chartJS31.Update();
		/// }
		/// ]]></code>
		/// </example>
		public DataSet[] DataSets
		{
			get;
			private set;
		}
	}
}
