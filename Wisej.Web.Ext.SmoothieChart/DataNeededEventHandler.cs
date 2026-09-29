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
using System.ComponentModel;

namespace Wisej.Web.Ext.SmoothieChart
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.SmoothieChart.SmoothieChart.DataNeeded" /> event in a <see cref="T:Wisej.Web.Ext.SmoothieChart.SmoothieChart"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.SmoothieChart.DataNeededEventArgs" /> that contains the event data. </param>
	public delegate void DataNeededEventHandler(object sender, DataNeededEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.SmoothieChart.SmoothieChart.DataNeeded" /> event of the 
	/// <see cref="T:Wisej.Web.Ext.SmoothieChart.SmoothieChart" /> control.
	/// </summary>
	[ApiCategory("SmoothieChart")]
	public class DataNeededEventArgs : EventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.SmoothieChart.DataNeededEventArgs"/>.
		/// </summary>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> line that is requesting a data point.</param>
		/// <param name="time">The time of the data point being requested.</param>
		public DataNeededEventArgs(int index, DateTime time)
		{
			this.TimeStamp = time;
			this.LineIndex = index;
		}

		/// <summary>
		/// Returns or sets the time of the data point.
		/// </summary>
		/// <remarks>
		/// It's initialized to the current server time (<see cref="DateTime.Now"/>). The client calculates the difference
		/// between its clock and the time of the first point it receives, and uses it to align all the following points.
		/// </remarks>
		public DateTime TimeStamp
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the value to plot on the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> line.
		/// </summary>
		/// <remarks>
		/// The same event data instance is used for all the lines of a single request: always set the value, otherwise
		/// the line receives the value assigned for the previous line (or 0 for the first line).
		/// </remarks>
		/// <example>
		/// Providing a value for each line:
		/// <code><![CDATA[
		/// private void smoothieChart1_DataNeeded(object sender, DataNeededEventArgs e)
		/// {
		///     switch (e.LineIndex)
		///     {
		///         case 0: e.Value = GetCpuUsage(); break;
		///         case 1: e.Value = GetMemoryUsage(); break;
		///         default: e.Value = 0; break;
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public int Value
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the index of the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> line that is requesting the data point.
		/// </summary>
		/// <remarks>
		/// This is the index of the line in the <see cref="SmoothieChart.TimeSeries"/> collection.
		/// </remarks>
		public int LineIndex
		{
			get;
			internal set;
		}

	}
}
