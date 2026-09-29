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
	/// <remarks>
	/// <para>
	/// The value of <see cref="ChartJS4.ChartType"/> is sent to Chart.js as the lower-case enum name
	/// (e.g. <c>"line"</c>, <c>"bar"</c>, <c>"doughnut"</c>). To use a different Chart.js type identifier,
	/// including a type registered by a plugin, set <see cref="Models.ChartOptions.Type"/>, which takes precedence.
	/// </para>
	/// <para>
	/// Chart.js type identifiers are case-sensitive and Chart.js 4 has no <c>horizontalBar</c> type: for polar area
	/// charts set <see cref="Models.ChartOptions.Type"/> to <c>"polarArea"</c>, and for horizontal bars use
	/// <see cref="Bar"/> with the Chart.js <c>indexAxis</c> option set to <c>"y"</c>.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartType = ChartType.Pie;
	/// chart.Labels = new[] { "Red", "Blue", "Yellow" };
	/// chart.DataSets.Add(new PieDataSet { Data = new object[] { 300, 50, 100 } });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public enum ChartType
	{
		/// <summary>
		/// Line chart (Chart.js type <c>"line"</c>). This is the default.
		/// </summary>
		Line,

		/// <summary>
		/// Bar chart (Chart.js type <c>"bar"</c>).
		/// </summary>
		Bar,

		/// <summary>
		/// Horizontal bar chart. Rendered as the type <c>"horizontalbar"</c>, which is not a built-in Chart.js 4 type;
		/// use <see cref="Bar"/> with the <c>indexAxis</c> option set to <c>"y"</c> instead.
		/// </summary>
		HorizontalBar,

		/// <summary>
		/// Radar chart (Chart.js type <c>"radar"</c>).
		/// </summary>
		Radar,

		/// <summary>
		/// Doughnut chart (Chart.js type <c>"doughnut"</c>).
		/// </summary>
		Doughnut,

		/// <summary>
		/// Polar area chart. The Chart.js type identifier is <c>"polarArea"</c>; since the enum name is sent
		/// in lower case, set <see cref="Models.ChartOptions.Type"/> to <c>"polarArea"</c> when using this chart type.
		/// </summary>
		PolarArea,

		/// <summary>
		/// Bubble chart (Chart.js type <c>"bubble"</c>).
		/// </summary>
		Bubble,

		/// <summary>
		/// Pie chart (Chart.js type <c>"pie"</c>).
		/// </summary>
		Pie,

		/// <summary>
		/// Scatter chart (Chart.js type <c>"scatter"</c>).
		/// </summary>
		Scatter,

		/// <summary>
		/// Custom chart type defined by the user. Set <see cref="Models.ChartOptions.Type"/> to the identifier of the
		/// custom Chart.js controller (for example one registered by a plugin).
		/// </summary>
		Custom
	}
}
