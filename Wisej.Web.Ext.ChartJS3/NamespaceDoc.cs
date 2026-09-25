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


namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// <para>
	/// ChartJS3 component. Displays beautiful <see href="http://www.chartjs.org/"/> charts.
	/// </para>
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control renders line, bar, horizontal bar, radar, polar area, pie, doughnut,
	/// bubble and scatter charts using Chart.js 3.5.0 and the chartjs-plugin-datalabels plugin.
	/// The chart type is set with <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>, the data is added to
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/> as <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects,
	/// and the appearance is configured through <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/>, where the title, legend, tooltip
	/// and data labels settings are grouped under <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Bar;
	/// this.chartJS31.Labels = new[] { "North", "South", "East", "West" };
	/// this.chartJS31.Options.Plugins.Title.Display = true;
	/// this.chartJS31.Options.Plugins.Title.Text = "Sales by Region";
	/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
	///
	/// var sales = (BarDataSet)this.chartJS31.DataSets.Add("2024");
	/// sales.Data = new object[] { 320, 210, 180, 260 };
	/// sales.BackgroundColor = new[] { Color.SteelBlue, Color.Orange, Color.SeaGreen, Color.IndianRed };
	///
	/// this.chartJS31.ChartClick += (s, e) => AlertBox.Show(Convert.ToString(e.Values[0]));
	/// ]]></code>
	/// </example>
	internal class NamespaceDoc
	{
	}
}
