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
	/// Represents an instance of the Google Maps JavaScript API map widget.
	/// </summary>
	/// <remarks>
	/// A valid <see cref="ApiKey"/> is required to load the Google Maps library.
	/// The map options (<see href="https://developers.google.com/maps/documentation/javascript/reference/map#MapOptions"/>)
	/// are set using the dynamic <see cref="Widget.Options"/> object; the default options are <c>zoom = 4</c>
	/// and <c>center = {lat: 0, lng: 0}</c>.
	/// Methods that change the map (markers, routes, info windows) run asynchronously on the client.
	/// </remarks>
	/// <example>
	/// Creating a map centered on a location with a marker:
	/// <code><![CDATA[
	/// var map = new GoogleMap
	/// {
	///     Dock = DockStyle.Fill,
	///     ApiKey = "YOUR_API_KEY"
	/// };
	/// map.Options.zoom = 12;
	/// map.Options.mapTypeId = "roadmap";
	/// map.Options.center = new LatLng { Lat = 40.7128, Lng = -74.0060 };
	/// this.Controls.Add(map);
	///
	/// map.AddMarker("office", 40.7128, -74.0060, new { title = "Our office" });
	/// ]]></code>
	/// </example>
	[ToolboxBitmap(typeof(GoogleMap))]
	[ApiCategory("GoogleMaps")]
	public class GoogleMap : Widget
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.GoogleMaps.GoogleMap"/> class.
		/// </summary>
		/// <remarks>
		/// Initializes <see cref="Widget.Options"/> with a zoom level of 4 centered at latitude 0 and longitude 0.
		/// </remarks>
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
		/// <remarks>
		/// The key is appended to <see cref="GoogleMapsURL"/> to load the Google Maps library. When the key is empty,
		/// the library is not loaded and the widget fails to initialize with the error "Missing Google Maps API Key".
		/// Set the key before the control is rendered for the first time: the library URL is built only once per instance.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/get-api-key"/>.
		/// </remarks>
		/// <example>
		/// Setting the API key when creating the page:
		/// <code><![CDATA[
		/// public MapPage()
		/// {
		///     InitializeComponent();
		///     this.googleMap1.ApiKey = "YOUR_API_KEY";
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns the initialization script that creates the google.maps.Map instance on the client.
		/// </summary>
		/// <remarks>
		/// The script is generated from the embedded startup.js resource and includes the serialized <see cref="Widget.Options"/>.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
		}

		/// <summary>
		/// Returns the list of packages (scripts) to load before the widget is initialized.
		/// </summary>
		/// <remarks>
		/// Contains the Google Maps library, loaded from <see cref="GoogleMapsURL"/> with the <see cref="ApiKey"/>,
		/// <see cref="Version"/> and <see cref="Libraries"/> parameters. The list is empty when <see cref="ApiKey"/> is not set.
		/// It's built the first time it's accessed and it's not rebuilt when these properties change.
		/// </remarks>
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
		/// Returns or sets the location of the Google Maps library. The default is
		/// //maps.googleapis.com/maps/api/js
		/// </summary>
		/// <remarks>
		/// This is a static property shared by all the sessions of the application. The query string parameters
		/// (key, v, libraries) are appended automatically and must not be included.
		/// Changing the value has no effect on maps that have already built their <see cref="Packages"/> list or
		/// after the library has been loaded in the browser.
		/// </remarks>
		/// <example>
		/// Loading the library from a different host at application startup:
		/// <code><![CDATA[
		/// GoogleMap.GoogleMapsURL = "https://maps.googleapis.com/maps/api/js";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the Google Maps version to load.
		/// </summary>
		/// <remarks>
		/// This is a static property shared by all the sessions of the application. When set, it's passed to
		/// the library URL as the "v" parameter, i.e. "weekly", "quarterly", "beta" or a specific version number.
		/// When null or empty, Google loads its default version.
		/// Set it before the first <see cref="GoogleMap"/> is rendered.
		/// See: <see href="https://developers.google.com/maps/documentation/javascript/versions"/>.
		/// </remarks>
		/// <example>
		/// Selecting the release channel at application startup:
		/// <code><![CDATA[
		/// GoogleMap.Version = "quarterly";
		/// ]]></code>
		/// </example>
		public static string Version
		{
			get { return _version; }
			set { _version = value; }
		}
		private static string _version;

		/// <summary>
		/// Returns or sets the additional Google Maps libraries to load.
		/// </summary>
		/// <remarks>
		/// This is a static property shared by all the sessions of the application. The value is a comma-separated
		/// list of library names passed to the library URL as the "libraries" parameter.
		/// Set it before the first <see cref="GoogleMap"/> is rendered.
		/// See: <see href="https://developers.google.com/maps/documentation/javascript/libraries"/>.
		/// </remarks>
		/// <example>
		/// Loading the places and geometry libraries at application startup:
		/// <code><![CDATA[
		/// GoogleMap.Libraries = "places,geometry";
		/// ]]></code>
		/// </example>
		public static string Libraries
		{
			get { return _libraries; }
			set { _libraries = value; }
		}
		private static string _libraries;

		/// <summary>
		/// Returns or sets whether the default markers at the beginning and end of the routes are suppressed.
		/// </summary>
		/// <remarks>
		/// When true, routes created using <see cref="AddRoute(string, string, TravelMode)"/> and the other AddRoute overloads
		/// are rendered without the default A and B markers.
		/// Markers created using <see cref="AddMarker(string, LatLng, dynamic, bool)"/> are not affected.
		/// Set this property before calling AddRoute: to change it for a route that is already displayed,
		/// call <see cref="ClearRoutes"/> and add the route again.
		/// </remarks>
		/// <example>
		/// Displaying a route using custom markers for the origin and destination:
		/// <code><![CDATA[
		/// this.googleMap1.SuppressMarkers = true;
		/// this.googleMap1.AddRoute("Boston, MA", "New York, NY", TravelMode.Driving);
		/// this.googleMap1.AddMarker("start", "Boston, MA", new { title = "Start" });
		/// this.googleMap1.AddMarker("end", "New York, NY", new { title = "End" });
		/// ]]></code>
		/// </example>
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
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/reference/marker#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		/// <remarks>
		/// A marker with the same <paramref name="markerId"/> is replaced. Clicking the marker fires <see cref="MapClick"/>
		/// (or <see cref="MapDoubleClick"/>) with <see cref="MapMouseEventArgs.Marker"/> set to <paramref name="markerId"/>.
		/// The <see cref="MarkerDragStart"/> and <see cref="MarkerDragEnd"/> events are fired only when the options
		/// include <c>draggable = true</c>.
		/// </remarks>
		/// <example>
		/// Adding a draggable marker and centering the map on it:
		/// <code><![CDATA[
		/// this.googleMap1.AddMarker("pin1", 48.8584, 2.2945, new { title = "Eiffel Tower", draggable = true }, true);
		/// ]]></code>
		/// </example>
		public void AddMarker(string markerId, double lat, double lng, dynamic options = null, bool center = false)
		{
			AddMarker(markerId, new LatLng(lat, lng), options, center);
		}

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="location">An instance of <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> that identifies the location of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/reference/marker#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		/// <remarks>
		/// A marker with the same <paramref name="markerId"/> is replaced. Clicking the marker fires <see cref="MapClick"/>
		/// (or <see cref="MapDoubleClick"/>) with <see cref="MapMouseEventArgs.Marker"/> set to <paramref name="markerId"/>.
		/// The <see cref="MarkerDragStart"/> and <see cref="MarkerDragEnd"/> events are fired only when the options
		/// include <c>draggable = true</c>.
		/// </remarks>
		/// <example>
		/// Adding a marker where the user clicks on the map:
		/// <code><![CDATA[
		/// private void googleMap1_MapClick(object sender, MapMouseEventArgs e)
		/// {
		///     if (e.Marker == null)
		///         this.googleMap1.AddMarker("pin" + (++this.pinCount), e.Location, new { label = this.pinCount.ToString() });
		/// }
		/// ]]></code>
		/// </example>
		public void AddMarker(string markerId, LatLng location, dynamic options = null, bool center = false)
		{
			Call("addMarker", markerId, location, options, center);
		}

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="address">The address - to be geocoded - of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: <see href="https://developers.google.com/maps/documentation/javascript/reference/marker#MarkerOptions"/>. </param>
		/// <param name="center">True to center the map after setting the marker.</param>
		/// <remarks>
		/// The address is geocoded on the client using google.maps.Geocoder and the marker is placed at the first result.
		/// If geocoding fails, the browser shows an alert with the failure status and no marker is added.
		/// A marker with the same <paramref name="markerId"/> is replaced.
		/// </remarks>
		/// <example>
		/// Adding a marker at an address:
		/// <code><![CDATA[
		/// this.googleMap1.AddMarker("hq", "1600 Amphitheatre Parkway, Mountain View, CA", new { title = "Headquarters" }, true);
		/// ]]></code>
		/// </example>
		public void AddMarker(string markerId, string address, dynamic options = null, bool center = false)
		{
			Call("addMarker", markerId, TextUtils.EscapeText(address), options, center);
		}

		/// <summary>
		/// Removes the marker.
		/// </summary>
		/// <param name="markerId">The unique ID of the marker to remove.</param>
		/// <remarks>
		/// Nothing happens if a marker with the specified ID doesn't exist.
		/// </remarks>
		/// <example>
		/// Removing a marker when the user right-clicks it:
		/// <code><![CDATA[
		/// private void googleMap1_MapClick(object sender, MapMouseEventArgs e)
		/// {
		///     if (e.Marker != null && e.Button == MouseButtons.Right)
		///         this.googleMap1.RemoveMarker(e.Marker);
		/// }
		/// ]]></code>
		/// </example>
		public void RemoveMarker(string markerId)
		{
			Call("removeMarker", markerId);
		}

		/// <summary>
		/// Removes all the markers from the map.
		/// </summary>
		/// <remarks>
		/// Only the markers added with AddMarker are removed; the markers of a route are removed by <see cref="ClearRoutes"/>.
		/// </remarks>
		/// <example>
		/// Replacing all the markers with a new set of locations:
		/// <code><![CDATA[
		/// this.googleMap1.ClearMarkers();
		/// foreach (var store in stores)
		/// {
		///     this.googleMap1.AddMarker(store.Id, store.Latitude, store.Longitude, new { title = store.Name });
		/// }
		/// ]]></code>
		/// </example>
		public void ClearMarkers()
		{
			Call("clearMarkers");
		}

		/// <summary>
		/// Uses the Google Maps DirectionsService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The latitude and longitude of the origin.</param>
		/// <param name="destination">The latitude and longitude of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		/// <remarks>
		/// The route is calculated on the client by google.maps.DirectionsService and displayed by a single
		/// google.maps.DirectionsRenderer: adding a new route replaces the one currently displayed.
		/// If the Directions service fails to find a route, nothing is displayed and no error is reported to the server.
		/// Use <see cref="SuppressMarkers"/> to hide the default markers at the beginning and end of the route.
		/// </remarks>
		/// <example>
		/// Displaying a walking route between two coordinates:
		/// <code><![CDATA[
		/// var origin = new LatLng { Lat = 51.5007, Lng = -0.1246 };
		/// var destination = new LatLng { Lat = 51.5081, Lng = -0.0759 };
		/// this.googleMap1.AddRoute(origin, destination, TravelMode.Walking);
		/// ]]></code>
		/// </example>
		public void AddRoute(LatLng origin, LatLng destination, TravelMode travelMode)
		{
			Call("addRoute", origin, destination, travelMode);
		}

		/// <summary>
		/// Uses the Google Maps DirectionsService to route and display a path between the origin and destination.
		/// See <see href="https://developers.google.com/maps/documentation/javascript/directions"/>.
		/// </summary>
		/// <param name="origin">The name of the origin.</param>
		/// <param name="destination">The name of the destination.</param>
		/// <param name="travelMode">The type of routing requested.</param>
		/// <remarks>
		/// The route is calculated on the client by google.maps.DirectionsService and displayed by a single
		/// google.maps.DirectionsRenderer: adding a new route replaces the one currently displayed.
		/// If the Directions service fails to find a route, nothing is displayed and no error is reported to the server.
		/// Use <see cref="SuppressMarkers"/> to hide the default markers at the beginning and end of the route.
		/// </remarks>
		/// <example>
		/// Displaying a driving route between two addresses:
		/// <code><![CDATA[
		/// this.googleMap1.AddRoute("San Francisco, CA", "Los Angeles, CA", TravelMode.Driving);
		/// ]]></code>
		/// </example>
		public void AddRoute(string origin, string destination, TravelMode travelMode)
		{
			Call("addRoute", origin, destination, travelMode);
		}

		/// <summary>
		/// Uses the Google Maps DirectionsService to route and display a path between the origin and destination.
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
		/// <remarks>
		/// The route is calculated on the client by google.maps.DirectionsService and displayed by a single
		/// google.maps.DirectionsRenderer: adding a new route replaces the one currently displayed.
		/// If the Directions service fails to find a route, nothing is displayed and no error is reported to the server.
		/// Use <see cref="SuppressMarkers"/> to hide the default markers at the beginning and end of the route.
		/// </remarks>
		/// <example>
		/// Displaying a route through two waypoints, avoiding tolls and using miles:
		/// <code><![CDATA[
		/// var waypoints = new[]
		/// {
		///     new Waypoint("Philadelphia, PA", true),
		///     new Waypoint("Baltimore, MD", false)
		/// };
		///
		/// this.googleMap1.AddRoute(
		///     "New York, NY",
		///     "Washington, DC",
		///     TravelMode.Driving,
		///     UnitSystem.Imperial,
		///     waypoints,
		///     avoidTolls: true);
		/// ]]></code>
		/// </example>
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
		/// Uses the Google Maps DirectionsService to route and display a path between the origin and destination.
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
		/// <remarks>
		/// The route is calculated on the client by google.maps.DirectionsService and displayed by a single
		/// google.maps.DirectionsRenderer: adding a new route replaces the one currently displayed.
		/// If the Directions service fails to find a route, nothing is displayed and no error is reported to the server.
		/// Use <see cref="SuppressMarkers"/> to hide the default markers at the beginning and end of the route.
		/// </remarks>
		/// <example>
		/// Displaying a bicycling route in kilometers that avoids highways:
		/// <code><![CDATA[
		/// var origin = new LatLng { Lat = 52.3676, Lng = 4.9041 };
		/// var destination = new LatLng { Lat = 52.0907, Lng = 5.1214 };
		/// this.googleMap1.AddRoute(origin, destination, TravelMode.Bicycling, UnitSystem.Metric, avoidHighways: true, region: "nl");
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Removes the route displayed by the AddRoute methods, including its default markers. Markers added with AddMarker are not affected.
		/// </remarks>
		/// <example>
		/// Removing the current route before showing a new one:
		/// <code><![CDATA[
		/// this.googleMap1.ClearRoutes();
		/// this.googleMap1.AddRoute("Milan, Italy", "Rome, Italy", TravelMode.Driving);
		/// ]]></code>
		/// </example>
		public void ClearRoutes()
		{
			Call("clearRoutes");
		}

		/// <summary>
		/// Centers the map at the specified location.
		/// </summary>
		/// <param name="lat">The latitude of the center of the map.</param>
		/// <param name="lng">The longitude of the center of the map.</param>
		/// <remarks>
		/// The zoom level is not changed. To also change the zoom level, set <c>Options.zoom</c>.
		/// </remarks>
		/// <example>
		/// Centering the map on a location:
		/// <code><![CDATA[
		/// this.googleMap1.CenterMap(35.6762, 139.6503);
		/// ]]></code>
		/// </example>
		public void CenterMap(double lat, double lng)
		{
			CenterMap(new LatLng(lat, lng));
		}

		/// <summary>
		/// Centers the map at the specified location.
		/// </summary>
		/// <param name="location">An instance of <see cref="T:Wisej.Web.Ext.GoogleMaps.LatLng"/> than identifies the center of the map.</param>
		/// <remarks>
		/// The zoom level is not changed. To also change the zoom level, set <c>Options.zoom</c>.
		/// </remarks>
		/// <example>
		/// Centering the map on a geocoded result:
		/// <code><![CDATA[
		/// private async void buttonSydney_Click(object sender, EventArgs e)
		/// {
		///     var results = await this.googleMap1.GetGeocodeAsync("Sydney Opera House");
		///     if (results.Length > 0 && !results[0].IsError)
		///         this.googleMap1.CenterMap(results[0].GeocodeGeometry.Location);
		/// }
		/// ]]></code>
		/// </example>
		public void CenterMap(LatLng location)
		{
			Call("centerMap", location);
		}

		/// <summary>
		/// Centers the map at the specified address.
		/// </summary>
		/// <param name="address">The address - to be geocoded - of the new center of the map.</param>
		/// <remarks>
		/// The address is geocoded on the client using google.maps.Geocoder and the map is centered at the first result.
		/// If geocoding fails, the browser shows an alert with the failure status. The zoom level is not changed.
		/// </remarks>
		/// <example>
		/// Centering the map on an address:
		/// <code><![CDATA[
		/// this.googleMap1.CenterMap("Piazza San Marco, Venice, Italy");
		/// ]]></code>
		/// </example>
		public void CenterMap(string address)
		{
			Call("centerMap", address);
		}

		/// <summary>
		/// Centers and zooms the map to fit a set of coordinates.
		/// </summary>
		/// <param name="coordinates">The coordinates that must be visible on the map.</param>
		/// <remarks>
		/// Sets the viewport (center and zoom level) so that all the <paramref name="coordinates"/> are visible.
		/// The map must already be initialized on the client when this method is called.
		/// </remarks>
		/// <example>
		/// Showing all the markers on the map:
		/// <code><![CDATA[
		/// var rome = new LatLng { Lat = 41.9028, Lng = 12.4964 };
		/// var paris = new LatLng { Lat = 48.8566, Lng = 2.3522 };
		/// this.googleMap1.AddMarker("rome", rome);
		/// this.googleMap1.AddMarker("paris", paris);
		/// this.googleMap1.FitBounds(rome, paris);
		/// ]]></code>
		/// </example>
		public void FitBounds(params LatLng[] coordinates)
		{
			Call("fitBounds", coordinates);
		}

		/// <summary>
		/// Shows an instance of the google.maps.InfoWindow class in relation to the marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		/// <param name="html">HTML content to display in the info window.</param>
		/// <remarks>
		/// The google.maps.InfoWindow is created the first time it's shown for a marker and it's reused afterwards:
		/// calling this method again for the same marker reopens the existing window without changing its content.
		/// To display different content, add the marker again with <c>AddMarker</c> before showing the info window.
		/// Nothing happens if the marker doesn't exist.
		/// </remarks>
		/// <example>
		/// Showing an info window when the user clicks a marker:
		/// <code><![CDATA[
		/// private void googleMap1_MapClick(object sender, MapMouseEventArgs e)
		/// {
		///     if (e.Marker == "office")
		///         this.googleMap1.ShowInfoWindow(e.Marker, "<b>Head Office</b><br/>Open 9:00 - 17:00");
		/// }
		/// ]]></code>
		/// </example>
		public void ShowInfoWindow(string markerId, string html)
		{
			Call("showInfoWindow", markerId, new {content = TextUtils.EscapeText(html, true)});
		}

		/// <summary>
		/// Shows an instance of the google.maps.InfoWindow class in relation to the marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		/// <param name="options">Options for the creation of the InfoWindow. See <see href="https://developers.google.com/maps/documentation/javascript/infowindows"/>. </param>
		/// <remarks>
		/// The google.maps.InfoWindow is created the first time it's shown for a marker and it's reused afterwards:
		/// calling this method again for the same marker reopens the existing window and ignores the new <paramref name="options"/>.
		/// Nothing happens if the marker doesn't exist.
		/// </remarks>
		/// <example>
		/// Showing an info window with a maximum width:
		/// <code><![CDATA[
		/// this.googleMap1.ShowInfoWindow("office", new
		/// {
		///     content = "<b>Head Office</b><br/>123 Main Street",
		///     maxWidth = 200
		/// });
		/// ]]></code>
		/// </example>
		public void ShowInfoWindow(string markerId, object options)
		{
			Call("showInfoWindow", markerId, options);
		}

		/// <summary>
		/// Closes the google.maps.InfoWindow related to the specified marker.
		/// </summary>
		/// <param name="markerId">The marker unique ID.</param>
		/// <remarks>
		/// Nothing happens if the marker doesn't exist or its info window has never been shown.
		/// </remarks>
		/// <example>
		/// Closing the info window of a marker:
		/// <code><![CDATA[
		/// this.googleMap1.CloseInfoWindow("office");
		/// ]]></code>
		/// </example>
		public void CloseInfoWindow(string markerId)
		{
			Call("closeInfoWindow", markerId);
		}

		/// <summary>
		/// Retrieves geocode information.
		/// </summary>
		/// <param name="callback">The callback method.</param>
		/// <param name="address">The address.</param>
		/// <remarks>
		/// The request is processed asynchronously on the client using google.maps.Geocoder: <paramref name="callback"/> is invoked
		/// when the response is received. If the request fails, the array contains a single <see cref="GeocoderResult"/>
		/// with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/> set to the status
		/// returned by Google, i.e. "ZERO_RESULTS".
		/// Pending requests are identified by the hash code of <paramref name="callback"/>: issuing a second request with the
		/// same delegate instance before the first one completes replaces the first callback.
		/// </remarks>
		/// <example>
		/// Showing the coordinates of an address:
		/// <code><![CDATA[
		/// this.googleMap1.GetGeocode(results =>
		/// {
		///     if (results.Length > 0 && !results[0].IsError)
		///         AlertBox.Show(results[0].GeocodeGeometry.Location.ToString());
		/// }, "10 Downing Street, London");
		/// ]]></code>
		/// </example>
		public void GetGeocode(Action<GeocoderResult[]> callback, string address)
		{
			GetGeocodeCore(callback, null, address);
		}

		/// <summary>
		/// Retrieves geocode information.
		/// </summary>
		/// <param name="callback">The callback method.</param>
		/// <param name="location">The location (latitude/longitude).</param>
		/// <remarks>
		/// The request is processed asynchronously on the client using google.maps.Geocoder: <paramref name="callback"/> is invoked
		/// when the response is received. If the request fails, the array contains a single <see cref="GeocoderResult"/>
		/// with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/> set to the status
		/// returned by Google, i.e. "ZERO_RESULTS".
		/// Pending requests are identified by the hash code of <paramref name="callback"/>: issuing a second request with the
		/// same delegate instance before the first one completes replaces the first callback.
		/// </remarks>
		/// <example>
		/// Showing the address of the location clicked by the user:
		/// <code><![CDATA[
		/// private void googleMap1_MapClick(object sender, MapMouseEventArgs e)
		/// {
		///     this.googleMap1.GetGeocode(results =>
		///     {
		///         if (results.Length > 0 && !results[0].IsError)
		///             AlertBox.Show(results[0].FormattedAddress);
		///     }, e.Location);
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// The request is processed asynchronously on the client using google.maps.Geocoder: <paramref name="callback"/> is invoked
		/// when the response is received. If the request fails, the array contains a single <see cref="GeocoderResult"/>
		/// with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/> set to the status
		/// returned by Google, i.e. "ZERO_RESULTS".
		/// Pending requests are identified by the hash code of <paramref name="callback"/>: issuing a second request with the
		/// same delegate instance before the first one completes replaces the first callback.
		/// </remarks>
		/// <example>
		/// Reading the country of a location:
		/// <code><![CDATA[
		/// this.googleMap1.GetGeocode(results =>
		/// {
		///     foreach (var component in results[0].AddressComponents ?? new GeocoderResult.AddressComponent[0])
		///     {
		///         if (Array.IndexOf(component.Types, "country") > -1)
		///             this.labelCountry.Text = component.LongName;
		///     }
		/// }, 45.4642, 9.1900);
		/// ]]></code>
		/// </example>
		public void GetGeocode(Action<GeocoderResult[]> callback, double lat, double lng)
		{
			GetGeocodeCore(callback, new LatLng(lat, lng), null);
		}

		/// <summary>
		/// Asynchronously retrieves geocode information.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <returns>A task that completes with the array of <see cref="GeocoderResult"/> objects returned by the geocoder.</returns>
		/// <remarks>
		/// The request is processed on the client using google.maps.Geocoder. If the request fails, the array contains a single
		/// <see cref="GeocoderResult"/> with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/>
		/// set to the status returned by Google, i.e. "ZERO_RESULTS".
		/// The task doesn't complete if the client doesn't respond, for example when the Google Maps library has not been loaded.
		/// </remarks>
		/// <example>
		/// Geocoding an address entered by the user:
		/// <code><![CDATA[
		/// private async void buttonSearch_Click(object sender, EventArgs e)
		/// {
		///     var results = await this.googleMap1.GetGeocodeAsync(this.textBoxAddress.Text);
		///     if (results.Length == 0 || results[0].IsError)
		///     {
		///         AlertBox.Show("Address not found.");
		///         return;
		///     }
		///
		///     var location = results[0].GeocodeGeometry.Location;
		///     this.googleMap1.AddMarker("search", location, null, true);
		/// }
		/// ]]></code>
		/// </example>
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
		/// <param name="location">The location (latitude/longitude).</param>
		/// <returns>A task that completes with the array of <see cref="GeocoderResult"/> objects returned by the geocoder.</returns>
		/// <remarks>
		/// The request is processed on the client using google.maps.Geocoder. If the request fails, the array contains a single
		/// <see cref="GeocoderResult"/> with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/>
		/// set to the status returned by Google, i.e. "ZERO_RESULTS".
		/// The task doesn't complete if the client doesn't respond, for example when the Google Maps library has not been loaded.
		/// </remarks>
		/// <example>
		/// Showing the address of a marker after it has been dragged:
		/// <code><![CDATA[
		/// private async void googleMap1_MarkerDragEnd(object sender, MarkerDragEventArgs e)
		/// {
		///     var results = await this.googleMap1.GetGeocodeAsync(e.Location);
		///     if (results.Length > 0 && !results[0].IsError)
		///         this.textBoxAddress.Text = results[0].FormattedAddress;
		/// }
		/// ]]></code>
		/// </example>
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
		/// <returns>A task that completes with the array of <see cref="GeocoderResult"/> objects returned by the geocoder.</returns>
		/// <remarks>
		/// The request is processed on the client using google.maps.Geocoder. If the request fails, the array contains a single
		/// <see cref="GeocoderResult"/> with <see cref="GeocoderResult.IsError"/> set to true and <see cref="GeocoderResult.ResultCode"/>
		/// set to the status returned by Google, i.e. "ZERO_RESULTS".
		/// The task doesn't complete if the client doesn't respond, for example when the Google Maps library has not been loaded.
		/// </remarks>
		/// <example>
		/// Reverse geocoding a set of coordinates:
		/// <code><![CDATA[
		/// private async void buttonLookup_Click(object sender, EventArgs e)
		/// {
		///     var results = await this.googleMap1.GetGeocodeAsync(40.6892, -74.0445);
		///     if (results.Length > 0 && !results[0].IsError)
		///         this.labelAddress.Text = results[0].FormattedAddress;
		/// }
		/// ]]></code>
		/// </example>
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
