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

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Indicates additional data about the specified location.
	/// </summary>
	[ApiCategory("GoogleMaps")]
	public enum TravelMode
    {
        /// <summary>
        /// Specifies a bicycling directions request.
        /// </summary>
        Bicycling,

        /// <summary>
        /// Specifies a driving directions request.
        /// </summary>
        Driving,

        /// <summary>
        /// Specifies a transit directions request.
        /// </summary>
        Transit,

        /// <summary>
        /// Specifies a walking directions request.
        /// </summary>
        Walking
    }
}