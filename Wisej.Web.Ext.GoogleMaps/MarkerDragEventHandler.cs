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
	public class MarkerDragEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.MarkerDragEventArgs" /> class.
		/// </summary>
		/// <param name="e">The event data from the client.</param>
		public MarkerDragEventArgs(WidgetEventArgs e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			this.Marker = e.Data.marker;
			this.Location = new LatLng(e.Data.lat ?? 0, e.Data.lng ?? 0);
			this.Position = new Point(e.Data.x ?? 0, e.Data.y ?? 0);
		}

		/// <summary>
		/// The ID of the clicked marker that was dragged.
		/// </summary>
		public string Marker { get; private set; }

		/// <summary>
		/// The coordinates of the marker.
		/// </summary>
		public LatLng Location { get; private set; }

		/// <summary>
		/// The position of the marker in pixels.
		///</summary>
		public Point Position { get; private set; }

	}
}
