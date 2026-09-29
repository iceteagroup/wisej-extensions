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
	/// Represents the method that will handle the mouse events of a <see cref="T:Wisej.Web.Ext.GoogleMaps.GoogleMap" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.GoogleMaps.MouseEventArgs" /> that contains the event data. </param>
	public delegate void MapMouseEventHandler(object sender, MapMouseEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapClick" /> and the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapDoubleClick" /> event.
	/// </summary>
	[ApiCategory("GoogleMaps")]
	public class MapMouseEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MapMouseEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
		/// <exception cref="ArgumentNullException"><paramref name="e"/> is null.</exception>
		public MapMouseEventArgs(WidgetEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			this.Marker = e.Data.marker;
			this.Location = new LatLng(e.Data.lat, e.Data.lng);
			this.Button = e.Type == "rightclick" ? MouseButtons.Right : MouseButtons.Left;
		}

		/// <summary>
		/// Returns the ID of the clicked marker. Null if the click landed on the map outside of a marker.
		/// </summary>
		/// <example>
		/// Distinguishing clicks on markers from clicks on the map:
		/// <code><![CDATA[
		/// private void googleMap1_MapClick(object sender, MapMouseEventArgs e)
		/// {
		///     if (e.Marker != null)
		///         this.googleMap1.ShowInfoWindow(e.Marker, "Marker " + e.Marker);
		///     else
		///         this.googleMap1.CenterMap(e.Location);
		/// }
		/// ]]></code>
		/// </example>
		public string Marker { get; private set; }

		/// <summary>
		/// Returns the coordinates of the click.
		/// </summary>
		public LatLng Location { get; private set; }

		/// <summary>
		/// Returns which mouse button was pressed.
		/// </summary>
		/// <remarks>
		/// <see cref="MouseButtons.Right"/> for right clicks, <see cref="MouseButtons.Left"/> in all other cases.
		/// </remarks>
		/// <returns>One of the <see cref="T:Wisej.Web.MouseButtons" /> values.</returns>
		public MouseButtons Button { get; private set; }

	}
}
