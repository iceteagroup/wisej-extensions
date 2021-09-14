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

using System.ComponentModel;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Enumerates the theme systems supported by the FullCalendar.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public enum ThemeSystem
	{
		/// <summary>
		/// Renders a minimal look &amp; feel. Does not require any CSS files beyond the FullCalendar base files.
		/// </summary>
		Standard,

		/// <summary>
		/// Supports Bootstrap 3 themes. The Bootstrap CSS file must be loaded separately in its own link tag.
		/// </summary>
		Bootstrap3,

		/// <summary>
		/// Supports jQuery UI themes. The jQuery UI CSS file must be loaded separately in its own link tag.
		/// </summary>
		JQueryUI
	}
}
