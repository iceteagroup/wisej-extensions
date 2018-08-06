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
	public class MapPropertyChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MapPropertyChangedEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
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
		/// The name of the property that has changed.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The new value of the property.
		/// </summary>
		public object Value { get; private set; }
	}
}
