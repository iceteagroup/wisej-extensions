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

namespace Wisej.Web.Ext.GoogleMaps
{
    /// <summary>
    /// Indicates additional data about the specified location.
    /// </summary>
    public enum GeocoderLocationType
    {
        /// <summary>
        /// Indicates that the returned result reflects a precise geocode.
        /// </summary>
        Rooftop,

        /// <summary>
        /// indicates that the returned result reflects an approximation (usually on a road)
        /// interpolated between two precise points (such as intersections).
        /// Interpolated results are generally returned when rooftop geocodes are unavailable for a street address.
        /// </summary>
        RangeInterpolated,

        /// <summary>
        /// Indicates that the returned result is the geometric center of a result such as a polyline
        /// (for example, a street) or polygon (region).
        /// </summary>
        GeometricCenter,

        /// <summary>
        /// Indicates that the returned result is approximate.
        /// </summary>
        Approximate
    }
}