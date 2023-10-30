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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Represent an instance of the Google Maps widget.
	/// </summary>
	[ToolboxBitmap(typeof(GoogleMap))]
	[ApiCategory("GoogleMaps")]
	public class GoogleMap : Widget
	{

		#region Constructors

		/// <summary>
		/// Constructs a new <see cref="T: Wisej.Web.Ext.GoogleMaps.GoogleMap"/> control.
		/// </summary>
		public GoogleMap()
		{
			this.Options.zoom = 4;
			this.Options.center = new LatLng(0, 0);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user clicks on the map or a marker.
		/// </summary>
		public event MapMouseEventHandler MapClick
		{
			add { base.AddHandler(nameof(MapClick), value); }
			remove { base.RemoveHandler(nameof(MapClick), value); }
		}

		/// <summary>
		/// Fires the Click event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMapClick(MapMouseEventArgs e)
		{
			((MapMouseEventHandler)base.Events[nameof(MapClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user double clicks on the map or a marker.
		/// </summary>
		public event MapMouseEventHandler MapDoubleClick
		{
			add { base.AddHandler(nameof(MapDoubleClick), value); }
			remove { base.RemoveHandler(nameof(MapDoubleClick), value); }
		}

		/// <summary>
		/// Fires the MapDoubleClick event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMapDoubleClick(MapMouseEventArgs e)
		{
			((MapMouseEventHandler)base.Events[nameof(MapDoubleClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user changes the map by zooming, tilting, or selecting a different map type.
		/// </summary>
		public event MapPropertyChangedEventHandler MapPropertyChanged
		{
			add { base.AddHandler(nameof(MapPropertyChanged), value); }
			remove { base.RemoveHandler(nameof(MapPropertyChanged), value); }
		}

		/// <summary>
		/// Fires the MapPropertyChanged event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMapPropertyChanged(MapPropertyChangedEventArgs e)
		{
			((MapPropertyChangedEventHandler)base.Events[nameof(MapPropertyChanged)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user begins dragging a marker.
		/// </summary>
		public event MarkerDragEventHandler MarkerDragStart
		{
			add { base.AddHandler(nameof(MarkerDragStart), value); }
			remove { base.RemoveHandler(nameof(MarkerDragStart), value); }
		}

		/// <summary>
		/// Fires the MarkerDragStart event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMarkerDragStart(MarkerDragEventArgs e)
		{
			((MarkerDragEventHandler)base.Events[nameof(MarkerDragStart)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user drags a marker.
		/// </summary>
		public event MarkerDragEventHandler MarkerDragEnd
		{
			add { base.AddHandler(nameof(MarkerDragEnd), value); }
			remove { base.RemoveHandler(nameof(MarkerDragEnd), value); }
		}

		/// <summary>
		/// Fires the MarkerDragEnd event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMarkerDragEnd(MarkerDragEventArgs e)
		{
			((MarkerDragEventHandler)base.Events[nameof(MarkerDragEnd)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user drags the map.
		/// </summary>
		public event EventHandler MapDragEnd
		{
			add { base.AddHandler(nameof(MapDragEnd), value); }
			remove { base.RemoveHandler(nameof(MapDragEnd), value); }
		}

		/// <summary>
		/// Fires the MapDragEnd event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMapDragEnd(EventArgs e)
		{
			((EventHandler)base.Events[nameof(MapDragEnd)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the user starts dragging the map.
		/// </summary>
		public event EventHandler MapDragStart
		{
			add { base.AddHandler(nameof(MapDragStart), value); }
			remove { base.RemoveHandler(nameof(MapDragStart), value); }
		}

		/// <summary>
		/// Fires the MapDragStart event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMapDragStart(EventArgs e)
		{
			((EventHandler)base.Events[nameof(MapDragStart)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the Google Maps API key.
		/// </summary>
		[DesignerActionList]
		public string ApiKey
		{
			get { return this._apiKey; }
			set
			{
				this._apiKey = value;
				Update();
			}
		}

		private string _apiKey;

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
		}

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.

					// don't return the Google Maps package unless we have an API key.
					if (!String.IsNullOrEmpty(this.ApiKey))
					{
						base.Packages.Add(new Package()
						{
							Name = "GoogleMaps",
							Source = $"{GoogleMapsURL}?key={this.ApiKey}" 
								+ (String.IsNullOrEmpty(Version) ? "" : $"&v={Version}")
								+ (String.IsNullOrEmpty(Libraries) ? "" : $"&libraries={Libraries}")
						});
					}
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Returns or sets the default location of the Google Maps library. The default is
		/// //maps.googleapis.com/maps/api/js
		/// </summary>
		/// <remarks>
		/// You can assign this value directly or set it under the application keys
		/// using the key name "GoogleMaps.URL". It cannot be changed or assigned after the
		/// component has been loaded the first time.
		/// </remarks>
		public static string GoogleMapsURL
		{
			get { return _googleMapsURL; }
			set { _googleMapsURL = value; }
		}
		private static string _googleMapsURL = "//maps.googleapis.com/maps/api/js";

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			string script = GetResourceString("Wisej.Web.Ext.GoogleMaps.JavaScript.startup.js");
			script = script.Replace("$options", WisejSerializer.Serialize(this.Options));
			script = script.Replace("$error", String.IsNullOrEmpty(this.ApiKey) ? "Missing Google Maps API Key" : "");

			return script;
		}

		/// <summary>
		/// The Google Maps version to load.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://developers.google.com/maps/documentation/javascript/versions"/>.
		/// </remarks>
		public static string Version
		{
			get { return _version; }
			set { _version = value; }
		}
		private static string _version;

		/// <summary>
		/// Gets or sets the Google Maps libraries to load.
		/// See: <see href="https://developers.google.com/maps/documentation/javascript/libraries"/>.
		/// </summary>
		public static string Libraries
		{
			get { return _libraries; }
			set { _libraries = value; }
		}
		private static string _libraries;

		/// <summary>
		/// Suppress the rendering of route markers. If SuppressMarkers is set to true, and a route is created using 
		///  <see cref="AddRoute(string, string, TravelMode)"/>, the default markers will not be rendered 
		///  at the beginning and end of the route.
		///  
		/// Markers created using <see cref="AddMarker(string, LatLng, dynamic, bool)"/> will not be affected.
		/// </summary>
		[DefaultValue(false)]
		[Description("Suppress the rendering of route markers.")]
		public bool SuppressMarkers
		{
			get
			{
				return this._suppressMarkers;
			}

			set
			{
				if (this._suppressMarkers != value)
				{
					this._suppressMarkers = value;
					Call("suppressMarkers", this._suppressMarkers);
					Update();
				}
			}
		}
		private bool _suppressMarkers;

		#endregion

		#region Methods

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="lat">The latitude of the marker.</param>
		/// <param name="lng">The longitude of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		public void AddMarker(string markerId, double lat, double lng, dynamic options = null, bool center = false)
		{
			AddMarker(markerId, new LatLng(lat, lng), options, center);
		}

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="location">An instance of <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> than identifies the location of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		public void AddMarker(string markerId, LatLng location, dynamic options = null, bool center = false)
		{
			Call("addMarker", markerId, location, options, center);
		}

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="address">The address - to be geocoded - of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		public void AddMarker(string markerId, string address, dynamic options = null, bool center = false)
		{
			Call("addMarker", markerId, TextUtils.EscapeText(address), options, center);
		}

		/// <summary>
		/// Removes the marker.
		/// </summary>
		/// <param name="markerId">The unique ID of the marker to remove.</param>
		public void RemoveMarker(string markerId)
		{
			Call("removeMarker", markerId);
		}

		/// <summary>
		/// Removes all the markers from the map.
		/// </summary>
		public void ClearMarkers()
		{
			Call("clearMarkers");
		}

		/// <summary>
		/// Uses GoogleMaps DirectionService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The latitude and longitude of the origin.</param>
		/// <param name="destination">The latitude and longitude of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		public void AddRoute(LatLng origin, LatLng destination, TravelMode travelMode)
		{
			Call("addRoute", origin, destination, travelMode);
		}

		/// <summary>
		/// Uses GoogleMaps DirectionService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The name of the origin.</param>
		/// <param name="destination">The name of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		public void AddRoute(string origin, string destination, TravelMode travelMode)
		{
			Call("addRoute", origin, destination, travelMode);
		}

		/// <summary>
		/// Uses GoogleMaps DirectionService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The name of the origin.</param>
		/// <param name="destination">The name of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		/// <param name="unitSystem">(optional)  Specifies what unit system to use when displaying results. Note: This unit system setting 
		/// only affects the text displayed to the user. The directions result (returned by google maps API) also contains distance values, not shown to the user, 
		/// which are always expressed in meters.</param>
		/// <param name="waypoints">Specifies an array of <see cref="Waypoint"/>s. Waypoints alter a route by routing it through the specified location(s).</param>
		/// <param name="optimizeWaypoints">(optional) specifies that the route using the supplied waypoints may be 
		/// optimized by rearranging the waypoints in a more efficient order. If true, the Directions service will 
		/// return the reordered waypoints in a waypoint_order field.</param>
		/// <param name="provideRouteAlternatives">(optional) when set to true specifies that the Directions service may provide more 
		/// than one route alternative in the response. Note that providing route alternatives may increase the response time 
		/// from the server. This is only available for requests without intermediate waypoints.</param>
		/// <param name="avoidFerries">(optional) when set to true indicates that the calculated route(s) should avoid ferries, if possible.</param>
		/// <param name="avoidHighways">(optional) when set to true indicates that the calculated route(s) should avoid major highways, if possible.</param>
		/// <param name="avoidTolls">(optional) when set to true indicates that the calculated route(s) should avoid toll roads, if possible.</param>
		/// <param name="region">(optional) Return results biased to a particular region. This parameter takes a region code, specified as a two-character (non-numeric) Unicode region subtag.</param>
		public void AddRoute(string origin, string destination, TravelMode travelMode, UnitSystem unitSystem=UnitSystem.Default, Waypoint[] waypoints = null, bool optimizeWaypoints=false, bool provideRouteAlternatives=false, bool avoidFerries=false, bool avoidHighways=false, bool avoidTolls=false, string region="")
		{
			if (unitSystem == UnitSystem.Default)
			{
				// Don't send the unitSytem in the API request (send null instead), so that we get the default behavior.
				Call("addRoute", origin, destination, travelMode, null, waypoints, optimizeWaypoints, provideRouteAlternatives, avoidFerries, avoidHighways, avoidTolls, region);
			}
			else
			{
				// For unitSystem, The googlemaps API accepts 0 for metric and 1 for imperial
				Call("addRoute", origin, destination, travelMode, (int)unitSystem, waypoints, optimizeWaypoints, provideRouteAlternatives, avoidFerries, avoidHighways, avoidTolls, region);
			}
		}

		/// <summary>
		/// Uses GoogleMaps DirectionService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The latitude and longitude of the origin.</param>
		/// <param name="destination">The latitude and longitude of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		/// <param name="unitSystem">(optional)  Specifies what unit system to use when displaying results. Note: This unit system setting 
		/// only affects the text displayed to the user. The directions result (returned by google maps API) also contains distance values, not shown to the user, 
		/// which are always expressed in meters.</param>
		/// <param name="waypoints">Specifies an array of <see cref="Waypoint"/>s. Waypoints alter a route by routing it through the specified location(s).</param>
		/// <param name="optimizeWaypoints">(optional) specifies that the route using the supplied waypoints may be 
		/// optimized by rearranging the waypoints in a more efficient order. If true, the Directions service will 
		/// return the reordered waypoints in a waypoint_order field.</param>
		/// <param name="provideRouteAlternatives">(optional) when set to true specifies that the Directions service may provide more 
		/// than one route alternative in the response. Note that providing route alternatives may increase the response time 
		/// from the server. This is only available for requests without intermediate waypoints.</param>
		/// <param name="avoidFerries">(optional) when set to true indicates that the calculated route(s) should avoid ferries, if possible.</param>
		/// <param name="avoidHighways">(optional) when set to true indicates that the calculated route(s) should avoid major highways, if possible.</param>
		/// <param name="avoidTolls">(optional) when set to true indicates that the calculated route(s) should avoid toll roads, if possible.</param>
		/// <param name="region">(optional) Return results biased to a particular region. This parameter takes a region code, specified as a two-character (non-numeric) Unicode region subtag.</param>
		public void AddRoute(LatLng origin, LatLng destination, TravelMode travelMode, UnitSystem unitSystem=UnitSystem.Default, Waypoint[] waypoints=null, bool optimizeWaypoints = false, bool provideRouteAlternatives = false, bool avoidFerries = false, bool avoidHighways = false, bool avoidTolls = false, string region="")
		{
			if (unitSystem == UnitSystem.Default)
			{
				// Don't send the unitSytem in the API request (send null instead), so that we get the default behavior.
				Call("addRoute", origin, destination, travelMode, null, waypoints, optimizeWaypoints, provideRouteAlternatives, avoidFerries, avoidHighways, avoidTolls, region);
			}
			else
			{
				// For unitSystem, The googlemaps API accepts 0 for metric and 1 for imperial
				Call("addRoute", origin, destination, travelMode, (int)unitSystem, waypoints, optimizeWaypoints, provideRouteAlternatives, avoidFerries, avoidHighways, avoidTolls, region);
			}
		}

		/// <summary>
		/// Clears any routes, if they exist.
		/// </summary>
		public void ClearRoutes()
		{
			Call("clearRoutes");
		}

		/// <summary>
		/// Centers the map at the specified location.
		/// </summary>
		/// <param name="lat">The latitude of the center of the map.</param>
		/// <param name="lng">The longitude of the center of the map.</param>
		public void CenterMap(double lat, double lng)
		{
			CenterMap(new LatLng(lat, lng));
		}

		/// <summary>
		/// Centers the map at the specified location.
		/// </summary>
		/// <param name="location">An instance of <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> than identifies the center of the map.</param>
		public void CenterMap(LatLng location)
		{
			Call("centerMap", location);
		}

		/// <summary>
		/// Centers the map at the specified address.
		/// </summary>
		/// <param name="address">The address - to be geocoded - of the new center of the map.</param>
		public void CenterMap(string address)
		{
			Call("centerMap", address);
		}

		/// <summary>
		/// Centers the map around a set of coordinates.
		/// </summary>
		/// <param name="coordinates"></param>
		public void FitBounds(params LatLng[] coordinates)
		{
			Call("fitBounds", coordinates);
		}

		/// <summary>
		/// Shows an instance of the google.maps.InfoWindow class in relation to the marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		/// <param name="html">HTML content to display in the info window.</param>
		public void ShowInfoWindow(string markerId, string html)
		{
			Call("showInfoWindow", markerId, new {content = TextUtils.EscapeText(html, true)});
		}

		/// <summary>
		/// Shows an instance of the google.maps.InfoWindow class in relation to the marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		/// <param name="options">Options for the creation of the InfoWindow. See <see href="https://developers.google.com/maps/documentation/javascript/infowindows"/>. </param>
		public void ShowInfoWindow(string markerId, object options)
		{
			Call("showInfoWindow", markerId, options);
		}

		/// <summary>
		/// Closes the google.maps.InfoWindow related to the specified marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		public void CloseInfoWindow(string markerId)
		{
			Call("closeInfoWindow", markerId);
		}

		/// <summary>
		/// Retrieves geocode information.
		/// </summary>
		/// <param name="callback">The callback method.</param>
		/// <param name="address">The address.</param>
		public void GetGeocode(Action<GeocoderResult[]> callback, string address)
		{
			GetGeocodeCore(callback, null, address);
		}

		/// <summary>
		/// Retrieves geocode information.
		/// </summary>
		/// <param name="callback">The callback method.</param>
		/// <param name="location">The location (latitude/longitude)/.</param>
		public void GetGeocode(Action<GeocoderResult[]> callback, LatLng location)
		{
			GetGeocodeCore(callback, location, null);
		}


		/// <summary>
		/// Retrieves geocode information.
		/// </summary>
		/// <param name="callback">The callback method.</param>
		/// <param name="lat">The latitude.</param>
		/// <param name="lng">The longitude.</param>
		public void GetGeocode(Action<GeocoderResult[]> callback, double lat, double lng)
		{
			GetGeocodeCore(callback, new LatLng(lat, lng), null);
		}

		/// <summary>
		/// Asynchronously retrieves geocode information.
		/// </summary>
		/// <param name="address">The address.</param>
		public Task<GeocoderResult[]> GetGeocodeAsync(string address)
		{
			var tcs = new TaskCompletionSource<GeocoderResult[]>();

			GetGeocodeCore((geocoderResults) =>
			{
				tcs.SetResult(geocoderResults);
			}, null, address);

			return tcs.Task;
		}

		/// <summary>
		/// Asynchronously retrieves geocode information.
		/// </summary>
		/// <param name="location">The location (latitude/longitude)/.</param>
		public Task<GeocoderResult[]> GetGeocodeAsync(LatLng location)
		{
			var tcs = new TaskCompletionSource<GeocoderResult[]>();

			GetGeocodeCore((geocoderResults) =>
			{
				tcs.SetResult(geocoderResults);
			}, location, null);

			return tcs.Task;
		}


		/// <summary>
		/// Asynchronously retrieves geocode information.
		/// </summary>
		/// <param name="lat">The latitude.</param>
		/// <param name="lng">The longitude.</param>
		public Task<GeocoderResult[]> GetGeocodeAsync(double lat, double lng)
		{
			var tcs = new TaskCompletionSource<GeocoderResult[]>();

			GetGeocodeCore((geocoderResults) =>
			{
				tcs.SetResult(geocoderResults);
			}, new LatLng(lat, lng), null);

			return tcs.Task;
		}

		// Implementation
		private void GetGeocodeCore(Action<GeocoderResult[]> callback, LatLng location,
			string address)
		{
			// save the callback in the dictionary and issue a getGeocode request
			// using the hash of the callback object to identify the async response.
			if (this._callbacks == null)
				this._callbacks = new Dictionary<int, Action<GeocoderResult[]>>();

			int id = callback.GetHashCode();
			this._callbacks[id] = callback;

			if (!string.IsNullOrWhiteSpace(address))
				Call("getGeocode", id, address);
			else
				Call("getGeocode", id, location);
		}

		private Dictionary<int, Action<GeocoderResult[]>> _callbacks = null;

		/// <summary>
		/// Process the getCurrentPosition response from the client.
		/// </summary>
		/// <param name="e"></param>
		private void ProcessCallbackWidgetEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;

			// find the corresponding request.
			if (this._callbacks != null)
			{
				int id = data.id ?? 0;
				dynamic[] geocodes = data.geocode;

				List<GeocoderResult> geocoderResults = new List<GeocoderResult>();

				if (!string.IsNullOrWhiteSpace(data.statusCode) && data.statusCode != "OK")
				{
					GeocoderResult geocoderResult = new GeocoderResult(data.statusCode);
					geocoderResults.Add(geocoderResult);
				}
				else
				{
					foreach (var geocode in geocodes)
					{
						GeocoderResult geocoderResult = new GeocoderResult(geocode);
						geocoderResults.Add(geocoderResult);
					}
				}

				var geocoderResultArray = geocoderResults.ToArray();

				Action<GeocoderResult[]> callback = null;
				if (this._callbacks.TryGetValue(id, out callback))
				{
					this._callbacks.Remove(id);
					callback(geocoderResultArray);
				}
			}
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "callback":
					ProcessCallbackWidgetEvent(e);
					break;

				case "click":
				case "rightclick":
					OnMapClick(new MapMouseEventArgs(e));
					break;

				case "dblclick":
					OnMapDoubleClick(new MapMouseEventArgs(e));
					break;

				case "propertychanged":
					OnMapPropertyChanged(new MapPropertyChangedEventArgs(e));
					break;

				case "markerdragstart":
					OnMarkerDragStart(new MarkerDragEventArgs(e));
					break;

				case "markerdragend":
					OnMarkerDragEnd(new MarkerDragEventArgs(e));
					break;

				case "mapdragstart":
					OnMapDragStart(EventArgs.Empty);
					break;

				case "mapdragend":
					OnMapDragEnd(EventArgs.Empty);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		#endregion

	}
}
