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

namespace Wisej.Web.Ext.ChartJS
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.ChartJS.ChartJS.ChartClick" /> event in a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/>.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.ChartJS.ChartClickEventArgs" /> that contains the event data. </param>
	public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.ChartJS.ChartJS.ChartClick" /> event of the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS" /> control.
	/// </summary>
	public class ChartClickEventArgs: EventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.ChartClickEventArgs"/>.
		/// </summary>
		/// <param name="chart"></param>
		/// <param name="e"></param>
		internal ChartClickEventArgs(Wisej.Web.Ext.ChartJS.ChartJS chart, WidgetEventArgs e)
		{
			dynamic[] points = e.Data;
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
		/// Returns  the indexes of the data points in the click radius.
		/// </summary>
		public object[] Values
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns  the indexes of the data points in the click radius.
		/// </summary>
		public int[] DataPoints
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the data sets in the click radius.
		/// </summary>
		public DataSet[] DataSets
		{
			get;
			private set;
		}
	}
}
