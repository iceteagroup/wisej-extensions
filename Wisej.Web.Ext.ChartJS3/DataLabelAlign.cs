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
	/// Alignment configuration for ChartJS datalabel. 
	/// See: https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#alignment-and-offset.
	/// </summary>
	public enum DataLabelAlign
	{
		/// <summary>
		/// (default): the label is centered on the anchor point.
		/// </summary>
		Center,

		/// <summary>
		/// The label is positioned before the anchor point, following the same direction.
		/// </summary>
		Start,

		/// <summary>
		/// The label is positioned after the anchor point, following the same direction.
		/// </summary>
		End,

		/// <summary>
		/// The label is positioned to the right of the anchor point (0°).
		/// </summary>
		Right,

		/// <summary>
		/// The label is positioned to the bottom of the anchor point (90°)
		/// </summary>
		Bottom,

		/// <summary>
		/// The label is positioned to the left of the anchor point (180°)
		/// </summary>
		Left,

		/// <summary>
		/// The label is positioned to the top of the anchor point (270°)
		/// </summary>
		Top
	}
}
