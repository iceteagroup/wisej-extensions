///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
	/// A waypoint alters a route by routing it through the specified location.
	/// </summary>
	/// <remarks>
	/// Waypoints are passed to the AddRoute overloads of <see cref="GoogleMap"/> that accept a <see cref="Waypoint"/> array.
	/// Specify either <see cref="Location"/> or <see cref="Address"/>: when <see cref="Location"/> is set, <see cref="Address"/> is ignored.
	/// See <see href="https://developers.google.com/maps/documentation/javascript/reference/directions#DirectionsWaypoint"/>.
	/// </remarks>
	/// <example>
	/// Routing through a location and an address:
	/// <code><![CDATA[
	/// var waypoints = new[]
	/// {
	///     new Waypoint(new LatLng { Lat = 43.7696, Lng = 11.2558 }, true),
	///     new Waypoint("Bologna, Italy", false)
	/// };
	/// this.googleMap1.AddRoute("Rome, Italy", "Milan, Italy", TravelMode.Driving, UnitSystem.Metric, waypoints);
	/// ]]></code>
	/// </example>
	[ApiCategory("GoogleMaps")]
	public class Waypoint
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.Waypoint"/> class.
		/// </summary>
		public Waypoint()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.Waypoint"/> class
		/// using a geographical location.
		/// </summary>
		/// <param name="location">The <see cref="LatLng"/> location of the waypoint.</param>
		/// <param name="stopover">True if the waypoint is a stop on the route, which splits the route into two legs.</param>
		/// <exception cref="ArgumentNullException"><paramref name="location"/> is null.</exception>
		public Waypoint(LatLng location, bool stopover)
		{
			this.Location = location;
			this.Stopover = stopover;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.Waypoint"/> class
		/// using an address.
		/// </summary>
		/// <param name="address">The address of the waypoint, geocoded by the Google Directions service.</param>
		/// <param name="stopover">True if the waypoint is a stop on the route, which splits the route into two legs.</param>
		/// <exception cref="NullReferenceException"><paramref name="address"/> is null or empty.</exception>
		public Waypoint(string address, bool stopover)
		{
			this.Address = address;
			this.Stopover = stopover;
		}

		/// <summary>
		/// Returns or sets the location of the waypoint, as a <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException">The value is null.</exception>
		/// <remarks>
		/// When set, it takes precedence over <see cref="Address"/>.
		/// </remarks>
		public LatLng Location {
			get
			{
				return this._location;
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException("Location");

				if (value != this._location)
					this._location = value;
			} 
		}
		private LatLng _location;

		/// <summary>
		/// Returns or sets the address of the waypoint.
		/// </summary>
		/// <exception cref="NullReferenceException">The value is null or empty and <see cref="Location"/> is not set.</exception>
		/// <remarks>
		/// Use this property when not providing a <see cref="Location"/>, otherwise it's ignored.
		/// The address is geocoded by the Google Directions service when the route is calculated.
		/// </remarks>
		public string Address
		{
			get
			{
				return this._address;
			}

			set
			{
				if (string.IsNullOrEmpty(value) && this.Location == null)
					throw new NullReferenceException("The address cannot be null if a Location is not provided");

				if (value != this._address)
				{
					this._address = value;
				}
			}
		}
		private string _address;

		/// <summary>
		/// Returns or sets whether the waypoint is a stop on the route, which has the effect of splitting the route into two legs.
		/// </summary>
		public bool Stopover {
			get
			{
				return _stopover;
			}

			set
			{
				if (value != _stopover)
					_stopover = value;
			} 
		}
		private bool _stopover = false;

		/// <summary>
		/// Returns a string representation of the <see cref="T:Wisej.Web.Ext.GoogleMaps.Waypoint"/>.
		/// </summary>
		/// <returns>A string in the format <c>{location:'...',stopover:True}</c> containing the <see cref="Address"/>
		/// when <see cref="Location"/> is not set, or the <see cref="Location"/> otherwise.</returns>
		/// <example>
		/// Logging a waypoint:
		/// <code><![CDATA[
		/// var waypoint = new Waypoint("Bologna, Italy", true);
		/// System.Diagnostics.Debug.WriteLine(waypoint.ToString()); // {location:'Bologna, Italy',stopover:True}
		/// ]]></code>
		/// </example>
		public override string ToString()
		{
			string str = string.Empty;

			if (this.Location == null && !string.IsNullOrEmpty(this.Address))
			{
				str += "{location:" + $"'{this.Address}'";
			}
			else
			{
				str += "{location:" + $"'{this.Location}'";
			}

			str += $",stopover:{this.Stopover}";
			str += "}";

			return str;
		}
	}
}
