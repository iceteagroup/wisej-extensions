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

using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Specifies the type of chart to display.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public enum ChartType
	{
		/// <summary>
		/// Line chart.
		/// </summary>
		Line,

		/// <summary>
		/// Bar chart.
		/// </summary>
		Bar,

		/// <summary>
		/// Horizontal bar chart.
		/// </summary>
		HorizontalBar,

		/// <summary>
		/// Radar chart.
		/// </summary>
		Radar,

		/// <summary>
		/// Doughnut chart.
		/// </summary>
		Doughnut,

		/// <summary>
		/// Polar area chart.
		/// </summary>
		PolarArea,

		/// <summary>
		/// Bubble chart.
		/// </summary>
		Bubble,

		/// <summary>
		/// Pie chart.
		/// </summary>
		Pie,

		/// <summary>
		/// Scatter chart.
		/// </summary>
		Scatter,

		/// <summary>
		/// Custom chart type defined by the user.
		/// </summary>
		Custom
	}
}
