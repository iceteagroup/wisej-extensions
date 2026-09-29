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
using System.ComponentModel;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// A LatLngBounds instance represents a rectangle in geographical coordinates, including one that crosses the 180 degrees longitudinal meridian.
	/// </summary>
	/// <remarks>
	/// An instance is returned in <see cref="MapPropertyChangedEventArgs.Value"/> when the "bounds" property of the map changes.
	/// The class doesn't validate, clamp or wrap the values; the ranges described for each property are applied by the Google Maps library.
	/// </remarks>
	/// <example>
	/// Reading the visible area of the map:
	/// <code><![CDATA[
	/// private void googleMap1_MapPropertyChanged(object sender, MapPropertyChangedEventArgs e)
	/// {
	///     if (e.Name == "bounds" && e.Value is LatLngBounds bounds)
	///         this.labelBounds.Text = bounds.ToString();
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("GoogleMaps")]
	public class LatLngBounds
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLngBounds"/> class
		/// with all the coordinates set to 0.
		/// </summary>
		public LatLngBounds()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLngBounds"/> class.
		/// </summary>
		/// <param name="east">East longitude in degrees. Values outside the range [-180, 180] will be wrapped to the range [-180, 180). For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. This reflects the fact that longitudes wrap around the globe.</param>
		/// <param name="north">North latitude in degrees. Values will be clamped to the range [-90, 90]. This means that if the value specified is less than -90, it will be set to -90. And if the value is greater than 90, it will be set to 90.</param>
		/// <param name="south">South latitude in degrees. Values will be clamped to the range [-90, 90]. This means that if the value specified is less than -90, it will be set to -90. And if the value is greater than 90, it will be set to 90.</param>
		/// <param name="west">West longitude in degrees. Values outside the range [-180, 180] will be wrapped to the range [-180, 180). For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. This reflects the fact that longitudes wrap around the globe.</param>
		internal LatLngBounds(double east, double north, double south, double west)
		{
			this.East = east;
			this.North = north;
			this.South = south;
			this.West = west;
		}

		/// <summary>
		/// Returns or sets the east longitude in degrees. Values outside the range [-180, 180] will be wrapped to the range [-180, 180). For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. This reflects the fact that longitudes wrap around the globe.
		/// </summary>
		public double East { get; set; }

		/// <summary>
		/// Returns or sets the north latitude in degrees. Values will be clamped to the range [-90, 90]. This means that if the value specified is less than -90, it will be set to -90. And if the value is greater than 90, it will be set to 90.
		/// </summary>
		public double North { get; set; }

		/// <summary>
		/// Returns or sets the south latitude in degrees. Values will be clamped to the range [-90, 90]. This means that if the value specified is less than -90, it will be set to -90. And if the value is greater than 90, it will be set to 90.
		/// </summary>
		public double South { get; set; }

		/// <summary>
		/// Returns or sets the west longitude in degrees. Values outside the range [-180, 180] will be wrapped to the range [-180, 180). For example, a value of -190 will be converted to 170. A value of 190 will be converted to -170. This reflects the fact that longitudes wrap around the globe.
		/// </summary>
		public double West { get; set; }

		/// <summary>
		/// Returns a string representation of a <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLngBounds"/> object.
		/// </summary>
		/// <returns>A string in the format <c>{East=..., North=..., South=..., West=...}</c>, formatted using the current culture.</returns>
		/// <example>
		/// Logging the bounds of the map:
		/// <code><![CDATA[
		/// var bounds = new LatLngBounds { East = 12.6, North = 42.0, South = 41.8, West = 12.3 };
		/// System.Diagnostics.Debug.WriteLine(bounds.ToString());
		/// ]]></code>
		/// </example>
		public override string ToString()
		{
			return String.Concat(
				"{East=", this.East,
				", North=", this.North,
				", South=", this.South,
				", West=", this.West, "}"
			);
		}
	}
}
