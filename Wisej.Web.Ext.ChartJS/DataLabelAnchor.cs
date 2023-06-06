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

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Anchor configuration for ChartJS datalabel. 
	/// See: <see href="https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#anchoring"/>.
	/// </summary>
	[ApiCategory("ChartJS")]
	public enum DataLabelAnchor
	{
		/// <summary>
		/// (default): element center.
		/// </summary>
		Center,

		/// <summary>
		/// Lowest element boundary
		/// </summary>
		Start,

		/// <summary>
		/// Highest element boundary.
		/// </summary>
		End
	}
}
