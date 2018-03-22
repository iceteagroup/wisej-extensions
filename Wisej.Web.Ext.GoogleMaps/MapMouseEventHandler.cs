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
	/// Represents the method that will handle the mouse events of a <see cref="T:Wisej.Web.Ext.GoogleMaps.GoogleMap" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.GoogleMaps.MouseEventArgs" /> that contains the event data. </param>
	public delegate void MapMouseEventHandler(object sender, MapMouseEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapClick" /> and the <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MapDoubleClick" /> event.
	/// </summary>
	public class MapMouseEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MouseEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
		public MapMouseEventArgs(WidgetEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			this.Marker = e.Data.marker;
			this.Location = new LatLng(e.Data.lat, e.Data.lng);
			this.Button = e.Type == "rightclick" ? MouseButtons.Right : MouseButtons.Left;
		}

		/// <summary>
		/// The ID of the clicked marker. Null if the click landed on the map outside of a marker.
		/// </summary>
		public string Marker { get; private set; }

		/// <summary>
		/// The coordinates of  the click.
		/// </summary>
		public LatLng Location { get; private set; }

		/// <summary>
		/// Returns which mouse button was pressed.
		///</summary>
		/// <returns>One of the <see cref="T:Wisej.Web.MouseButtons" /> values.</returns>
		public MouseButtons Button { get; private set; }

	}
}
