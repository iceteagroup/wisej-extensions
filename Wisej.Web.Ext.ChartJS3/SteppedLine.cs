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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Defines the style of a stepped line for a line chart.
	/// </summary>
	[ApiCategory("ChartJS3")]
	public enum SteppedLine
	{
		/// <summary>
		/// No step interpolation.
		/// </summary>
		False,

		/// <summary>
		/// Step-before Interpolation, same as Before.
		/// </summary>
		True,

		/// <summary>
		/// Step-before interpolation.
		/// </summary>
		Before,
		
		/// <summary>
		/// Step-after interpolation.
		/// </summary>
		After,

		/// <summary>
		/// Step-middle Interpolation.
		/// </summary>
		Middle
	}
}
