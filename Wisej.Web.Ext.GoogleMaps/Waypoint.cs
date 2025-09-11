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
	/// A waypoints alters a route by routing it through the specified location.
	/// </summary>
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
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.Waypoint"/> class.
		/// </summary>
		public Waypoint(LatLng location, bool stopover)
		{
			this.Location = location;
			this.Stopover = stopover;
		}

		public Waypoint(string address, bool stopover)
		{
			this.Address = address;
			this.Stopover = stopover;
		}

		/// <summary>
		/// the location of the waypoint, as a <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/>.
		/// </summary>
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
		///	The address of the waypoint.
		///	NOTE: USE THIS PROPERTY IF YOU'RE NOT PROVIDING A LOCATION, OTHERWISE IT WILL BE IGNORED.
		/// </summary>
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
		/// A boolean which indicates that the waypoint is a stop on the route, which has the effect of splitting the route into two routes.
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
