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

using MicrosoftCharts = System.Windows.Forms.DataVisualization.Charting;

namespace Wisej.Web.Ext.WebCharts
{
	/// <summary>
	/// Overrides the Microsoft Chart class to prevent the creation of the handle: we only need the rendering capabilities.
	/// </summary>
	internal class ChartRenderer : MicrosoftCharts.Chart
	{
		protected override void CreateHandle()
		{
			// do nothing and the handle will not be created.
			// everything works just the same, but in memory, as any
			// good web control.
		}
	}
}
