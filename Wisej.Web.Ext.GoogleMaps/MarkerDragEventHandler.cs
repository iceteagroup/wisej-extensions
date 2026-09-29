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
using System.Drawing;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Represents the method that will handle the marker drag events of a <see cref="T:Wisej.Web.Ext.GoogleMaps.GoogleMap" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.GoogleMaps.MarkerDragEventArgs" /> that contains the event data. </param>
	public delegate void MarkerDragEventHandler(object sender, MarkerDragEventArgs e);

	/// <summary>
	/// Provides data for the 
	/// <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MarkerDragStart" /> and the 
	/// <see cref="E:Wisej.Web.Ext.GoogleMaps.GoogleMap.MarkerDragEnd" /> event.
	/// </summary>
	[ApiCategory("GoogleMaps")]
	public class MarkerDragEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MarkerDragEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
		/// <exception cref="ArgumentNullException"><paramref name="e"/> is null.</exception>
		public MarkerDragEventArgs(WidgetEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			this.Marker = e.Data.marker;
			this.Location = new LatLng(e.Data.lat ?? 0, e.Data.lng ?? 0);
			this.Position = new Point(e.Data.x ?? 0, e.Data.y ?? 0);
		}

		/// <summary>
		/// Returns the ID of the marker that was dragged.
		/// </summary>
		public string Marker { get; private set; }

		/// <summary>
		/// Returns the coordinates of the marker.
		/// </summary>
		/// <remarks>
		/// For <see cref="GoogleMap.MarkerDragEnd"/> it's the new location of the marker, for <see cref="GoogleMap.MarkerDragStart"/>
		/// the location at the start of the drag operation.
		/// </remarks>
		/// <example>
		/// Saving the new position of a marker:
		/// <code><![CDATA[
		/// private void googleMap1_MarkerDragEnd(object sender, MarkerDragEventArgs e)
		/// {
		///     SaveMarkerPosition(e.Marker, e.Location.Lat, e.Location.Lng);
		/// }
		/// ]]></code>
		/// </example>
		public LatLng Location { get; private set; }

		/// <summary>
		/// Returns the position of the marker in pixels, as reported by the Google Maps drag event.
		/// </summary>
		public Point Position { get; private set; }

	}
}
