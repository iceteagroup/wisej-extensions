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
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Configuration;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.GoogleMaps
{
	/// <summary>
	/// Represent an instance of the Google Maps widget.
	/// </summary>
	[ToolboxBitmap(typeof(GoogleMap))]
	public class GoogleMap : Widget
	{
		/// <summary>
		/// Constructs a new <see cref="T: Wisej.Web.Ext.GoogleMaps.GoogleMap"/> control.
		/// </summary>
		public GoogleMap()
		{
			this.Options.zoom = 4;
			this.Options.center = new LatLng(0, 0);
		}

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
			((MapMouseEventHandler) base.Events[nameof(MapClick)])?.Invoke(this, e);
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
			((MapMouseEventHandler) base.Events[nameof(MapDoubleClick)])?.Invoke(this, e);
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
			((MapPropertyChangedEventHandler) base.Events[nameof(MapPropertyChanged)])?.Invoke(this, e);
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
			((MarkerDragEventHandler) base.Events[nameof(MarkerDragStart)])?.Invoke(this, e);
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
			((MarkerDragEventHandler) base.Events[nameof(MarkerDragEnd)])?.Invoke(this, e);
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
		/// Returns or sets the specified MapOptions: https://developers.google.com/maps/documentation/javascript/3.exp/reference#MapOptions.
		/// </summary>
		[DesignerActionList]
		[MergableProperty(false)]
		[Editor("Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public new virtual dynamic Options
		{
			get
			{
				if (this._options == null)
					this._options = new DynamicObject();

				return this._options;
			}
			set
			{
				this._options = value;
				Update();
			}
		}

		private dynamic _options;

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
							Source = GoogleMapsURL + "?key=" + this.ApiKey
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
			get
			{
				if (_GoogleMapsURL == null)
				{
					// read the google maps api url from web.config.
					_GoogleMapsURL = WebConfigurationManager.AppSettings["GoogleMaps.URL"];
					if (String.IsNullOrEmpty(_GoogleMapsURL))
						_GoogleMapsURL = "//maps.googleapis.com/maps/api/js";
				}

				return _GoogleMapsURL;
			}
			set
			{
				_GoogleMapsURL = value;
			}
		}

		private static string _GoogleMapsURL;

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			string script = GetResourceString("Wisej.Web.Ext.GoogleMaps.JavaScript.startup.js");
			script = script.Replace("$options", WisejSerializer.Serialize(this.Options));
			script = script.Replace("$error", String.IsNullOrEmpty(this.ApiKey) ? "Missing Google Maps API Key" : "");

			return script;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a new marker to the map.
		/// </summary>
		/// <param name="markerId">The unique ID that identifies the marker.</param>
		/// <param name="lat">The latitude of the marker.</param>
		/// <param name="lng">The longitude of the marker.</param>
		/// <param name="options">An optional dynamic object that specifies the marker options: https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions. </param>
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
		/// <param name="options">An optional dynamic object that specifies the marker options: https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions. </param>
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
		/// <param name="options">An optional dynamic object that specifies the marker options: https://developers.google.com/maps/documentation/javascript/3.exp/reference#MarkerOptions. </param>
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
		/// <param name="options">Options for the creation of the InfoWindow. See https://developers.google.com/maps/documentation/javascript/infowindows. </param>
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
