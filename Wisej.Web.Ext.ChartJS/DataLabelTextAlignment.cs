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
	/// Specifies the alignment of the text in a data label.
	/// See: <see href="https://v1_0_0--chartjs-plugin-datalabels.netlify.app/guide/formatting.html#text-alignment"/>.
	/// </summary>
	[ApiCategory("ChartJS")]
	public enum DataLabelTextAlignment
	{
		/// <summary>
		/// (default): the text is left-aligned.
		/// </summary>
		Start,

		/// <summary>
		/// The text is centered.
		/// </summary>
		Center,

		/// <summary>
		/// The text is right-aligned.
		/// </summary>
		End,

		/// <summary>
		/// Alias of 'start'.
		/// </summary>
		Left,

		/// <summary>
		/// Alias of 'end'.
		/// </summary>
		Right
	}
}
