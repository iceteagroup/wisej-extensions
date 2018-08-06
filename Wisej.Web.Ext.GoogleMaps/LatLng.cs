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


using System;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// A LatLng is a point in geographical coordinates: latitude and longitude.
	/// </summary>
	/// <remarks>
	/// 
	/// Latitude ranges between -90 and 90 degrees, inclusive. 
	/// Values above or below this range will be clamped to the range [-90, 90]. 
	/// This means that if the value specified is less than -90, it will be set to -90. And if the value is greater than 90, it will be set to 90.
	/// 
	/// Longitude ranges between -180 and 180 degrees, inclusive.Values above or below this range will be wrapped so that they fall within the range.
	/// For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. This reflects the fact that longitudes wrap around the globe.
	/// 
	/// Although the default map projection associates longitude with the x-coordinate of the map, and latitude 
	/// with the y-coordinate, the latitude coordinate is always written first, followed by the longitude.
	/// Notice that you cannot modify the coordinates of a LatLng. If you want to compute another point, you have to create a new one.
	/// 
	/// </remarks>
	public class LatLng
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> class.
		/// </summary>
		public LatLng()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> class.
		/// </summary>
		/// <param name="lat">The latitude.</param>
		/// <param name="lng">The longitude.</param>
		internal LatLng(double lat, double lng)
		{
			this.Lat = lat;
			this.Lng = lng;
		}

		/// <summary>
		/// The latitude in degrees.
		/// </summary>
		public double Lat { get; set; }

		/// <summary>
		/// The longitude in degrees.
		/// </summary>
		public double Lng { get; set; }

		/// <summary>
		/// Returns a string representation of a <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Concat(
				"{Lat=", this.Lat,
				", Lng=", this.Lng, "}"
			);
		}
	}
}
