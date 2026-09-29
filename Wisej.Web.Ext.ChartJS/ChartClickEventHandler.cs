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
using System.Collections.Generic;
using System.ComponentModel;

namespace Wisej.Web.Ext.ChartJS
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.ChartJS.ChartJS.ChartClick" /> event in a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/>.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.ChartJS.ChartClickEventArgs" /> that contains the event data.</param>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartClick += new ChartClickEventHandler(this.chartJS1_ChartClick);
	///
	/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
	/// {
	///     var chart = (ChartJS)sender;
	///     this.labelStatus.Text = $"Clicked {e.DataPoints.Length} point(s) on {chart.Name}";
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="E:Wisej.Web.Ext.ChartJS.ChartJS.ChartClick" /> event of the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS" /> control.
    /// </summary>
    /// <remarks>
    /// The arrays <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.DataSets"/>, <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.DataPoints"/>
    /// and <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.Values"/> have the same length and are parallel: the element at index i of each array
    /// describes the same chart element returned by Chart.js for the click position.
    /// <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.SelectedDataSet"/> and <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.SelectedValue"/>
    /// describe the single element directly underneath the click.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
    /// {
    ///     this.listBox1.Items.Clear();
    ///     for (int i = 0; i < e.DataPoints.Length; i++)
    ///     {
    ///         var label = this.chartJS1.Labels[e.DataPoints[i]];
    ///         this.listBox1.Items.Add($"{e.DataSets[i].Label} - {label}: {e.Values[i]}");
    ///     }
    /// }
    /// ]]></code>
    /// </example>
	[ApiCategory("ChartJS")]
    public class ChartClickEventArgs: EventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.ChartClickEventArgs"/>.
		/// </summary>
		/// <param name="chart"></param>
		/// <param name="e"></param>
		internal ChartClickEventArgs(Wisej.Web.Ext.ChartJS.ChartJS chart, WidgetEventArgs e)
		{
			dynamic[] points = e.Data.data;
			dynamic selection = e.Data.selected;
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

				// add information about the value directly underneath the click.
				var selPointIndex = selection.pointIndex;
				var selDataSetIndex = selection.dataSetIndex;

				this.SelectedDataSet = chart.DataSets[selDataSetIndex];
				this.SelectedValue = chart.DataSets[selDataSetIndex].Data[selPointIndex];
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
		/// An array with the value of each data point, taken from the <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> array of the corresponding data set.
		/// </value>
		/// <remarks>
		/// The value at index i belongs to the data set at index i in <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.DataSets"/>
		/// and to the data point at index i in <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.DataPoints"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     var total = e.Values.Sum(v => Convert.ToDouble(v));
		///     this.labelTotal.Text = "Total: " + total;
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
		/// An array of zero-based indexes into the <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> array of the corresponding data set.
		/// The same index also identifies the label in <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Labels"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     int index = e.DataPoints[0];
		///     AlertBox.Show("You clicked " + this.chartJS1.Labels[index]);
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
		/// An array with the <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> of each clicked element. The same data set may appear more than once.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     var names = e.DataSets.Select(d => d.Label).Distinct();
		///     this.labelInfo.Text = String.Join(", ", names);
		/// }
		/// ]]></code>
		/// </example>
		public DataSet[] DataSets
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the data set directly underneath the click.
		/// </summary>
		/// <value>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> that contains the element directly underneath the click, or null when there is no clicked element.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     // toggle the visibility of the clicked data set.
		///     if (e.SelectedDataSet != null)
		///     {
		///         e.SelectedDataSet.Hidden = !e.SelectedDataSet.Hidden;
		///         this.chartJS1.Update();
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public DataSet SelectedDataSet
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the value directly underneath the click.
		/// </summary>
		/// <value>
		/// The value in the <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Data"/> array of <see cref="P:Wisej.Web.Ext.ChartJS.ChartClickEventArgs.SelectedDataSet"/>
		/// for the element directly underneath the click, or null when there is no clicked element.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// private void chartJS1_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     this.textBoxValue.Text = Convert.ToString(e.SelectedValue);
		/// }
		/// ]]></code>
		/// </example>
		public object SelectedValue
		{
			get;
			private set;
		}
	}
}
