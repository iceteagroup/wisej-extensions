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
	/// <para>
	/// ChartJS component. Displays beautiful <see href="http://www.chartjs.org/"/> charts.
	/// </para>
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control renders line, bar, horizontal bar, radar, polar area, pie, doughnut,
	/// bubble and scatter charts using Chart.js 2.7.2 and the chartjs-plugin-datalabels plugin.
	/// The chart type is set with <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.ChartType"/>, the data is added to
	/// <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.DataSets"/> as <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/> objects,
	/// and the appearance is configured through <see cref="P:Wisej.Web.Ext.ChartJS.ChartJS.Options"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS1.ChartType = ChartType.Bar;
	/// this.chartJS1.Labels = new[] { "North", "South", "East", "West" };
	/// this.chartJS1.Options.Title.Display = true;
	/// this.chartJS1.Options.Title.Text = "Sales by Region";
	///
	/// var sales = (BarDataSet)this.chartJS1.DataSets.Add("2024");
	/// sales.Data = new object[] { 320, 210, 180, 260 };
	/// sales.BackgroundColor = new[] { Color.SteelBlue, Color.Orange, Color.SeaGreen, Color.IndianRed };
	///
	/// this.chartJS1.ChartClick += (s, e) => AlertBox.Show(Convert.ToString(e.SelectedValue));
	/// ]]></code>
	/// </example>
	internal class NamespaceDoc
	{
	}
}
