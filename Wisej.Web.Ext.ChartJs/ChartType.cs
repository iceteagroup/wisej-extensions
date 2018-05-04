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


namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Defines the type of chart rendered by the <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
	/// </summary>
	public enum ChartType
	{
		/// <summary>
		/// A line chart plots data points on a line.
		/// </summary>
		Line,

		/// <summary>
		/// A bar chart shows data as bars.
		/// </summary>
		Bar,

		/// <summary>
		/// The horizontal bar chart shows data as horizontal bars.
		/// </summary>
		HorizontalBar,

		/// <summary>
		/// The radar chart shows multiple data points and the variation between them.
		/// </summary>
		Radar,

		/// <summary>
		/// Polar area charts are similar to pie charts, but each segment has the same angle - the radius of the segment differs depending on the value.
		/// </summary>
		PolarArea,

		/// <summary>
		/// Pie and doughnut charts are probably the most commonly used chart there are. They are divided into segments, the arc of each segment shows the proportional value of each piece of data.
		/// </summary>
		Pie,

		/// <summary>
		/// Pie and doughnut charts are probably the most commonly used chart there are. They are divided into segments, the arc of each segment shows the proportional value of each piece of data.
		/// </summary>
		Doughnut,

		/// <summary>
		/// A bubble chart is used to display three dimensions of data at the same time. 
		/// The location of the bubble is determined by the first two dimensions and the corresponding 
		/// horizontal and vertical axes. The third dimension is represented by the size of the individual bubbles.
		/// </summary>
		Bubble,

		/// <summary>
		/// Scatter charts are based on basic line charts with the x axis changed to a linear axis. 
		/// To use a scatter chart, data must be passed as objects containing X and Y properties. 
		/// </summary>
		Scatter
	}
}
