///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Specifies what unit system to use when displaying results.
	/// </summary>
	/// <remarks>
	/// By default, directions are calculated and displayed using the unit system of the origin's country or region.
	/// (Note: Origins expressed using latitude/longitude coordinates rather than addresses always default to metric units.) 
	/// For example, a route from "Chicago, IL" to "Toronto, ONT" will display results in miles, while the reverse route 
	/// will display results in kilometers. You can override this unit system by setting one explicitly.
	/// Note: This unit system setting only affects the text displayed to the user. The directions result also contains 
	/// distance values, not shown to the user, which are always expressed in meters.
	/// </remarks>
	[ApiCategory("GoogleMaps")]
	public enum UnitSystem
	{
		/// <summary>
		/// Specifies usage of the metric system. Distances are shown using kilometers.
		/// </summary>
		Metric,

		/// <summary>
		/// Specifies usage of the Imperial (English) system. Distances are shown using miles.
		/// </summary>
		Imperial,

		/// <summary>
		/// Directions are calculated and displayed using the unit system of the origin's country or region. 
		/// </summary>
		Default
	}
}