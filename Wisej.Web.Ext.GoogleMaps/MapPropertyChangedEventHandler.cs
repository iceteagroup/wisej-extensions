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
using Wisej.Core;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapPropertyChanged"/> event of 
	/// a <see cref="T:Wisej.Web.Ext.GoogleMaps.GoogleMap" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.GoogleMaps.MapPropertyChangedEventArgs" /> that contains the event data. </param>
	public delegate void MapPropertyChangedEventHandler(object sender, MapPropertyChangedEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapPropertyChanged" /> event.
	/// </summary>
	[ApiCategory("GoogleMaps")]
	public class MapPropertyChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MapPropertyChangedEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
		/// <exception cref="ArgumentNullException"><paramref name="e"/> is null.</exception>
		public MapPropertyChangedEventArgs(WidgetEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			dynamic data = e.Data;
			this.Name = data.name;
			this.Value = data.value;

			// detect LatLng values.
			if (this.Value != null && this.Value is DynamicObject)
			{
				dynamic latlng = this.Value;
				if (latlng != null && latlng.lat != null && latlng.lng != null)
					this.Value = new LatLng(latlng.lat, latlng.lng);

				if (latlng != null && latlng.east != null && latlng.north != null && latlng.south != null && latlng.west != null)
					this.Value = new LatLngBounds(latlng.east, latlng.north, latlng.south, latlng.west);
			}
		}

		/// <summary>
		/// Returns the name of the property that has changed.
		/// </summary>
		/// <remarks>
		/// One of "zoom", "tilt", "mapTypeId", "center", "bounds", "heading" or "projection".
		/// </remarks>
		public string Name { get; private set; }

		/// <summary>
		/// Returns the new value of the property.
		/// </summary>
		/// <remarks>
		/// The type depends on <see cref="Name"/>: a <see cref="LatLng"/> for "center", a <see cref="LatLngBounds"/> for "bounds",
		/// a number for "zoom", "tilt" and "heading", and a string for "mapTypeId". It can be null when the client
		/// cannot serialize the value.
		/// </remarks>
		/// <example>
		/// Tracking the zoom level and the center of the map:
		/// <code><![CDATA[
		/// private void googleMap1_MapPropertyChanged(object sender, MapPropertyChangedEventArgs e)
		/// {
		///     switch (e.Name)
		///     {
		///         case "zoom":
		///             this.labelZoom.Text = "Zoom: " + Convert.ToInt32(e.Value);
		///             break;
		/// 
		///         case "center":
		///             var center = (LatLng)e.Value;
		///             this.labelCenter.Text = $"{center.Lat:F4}, {center.Lng:F4}";
		///             break;
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public object Value { get; private set; }
	}
}
