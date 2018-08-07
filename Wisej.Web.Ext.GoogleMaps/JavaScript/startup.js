//# sourceURL=wisej.web.ext.GoogleMaps.startup.js

/**
 * Initializes the widget.
 *
 * This function is called when the InitScript property of
 * wisej.web.Widget changes.
 *
 * "this" refers to the container which is a wisej.web.Widget instance.
 *
 * The widget has an inner container with id = "container" that can
 * be used referring to this.container.
 *
 */
this.init = function () {

	var me = this;

	// prepare the configuration map.
	// [$]options is a placeholder that is replaced with the options map.
	var options = $options;

	// error placeholder.
	var error = "$error";
	if (error)
		throw new Error(error);

	// create the Google map widget.
	if (this.map == null) {

		this.map = new google.maps.Map(this.container, options);

		// dynamically resize the map.
		this.addListener("resize", function () {
			google.maps.event.trigger(this.map, "resize");
		});

		// hook the events that we want to redirect to the server component.
		this.map.addListener("click", this._onMapClick.bind(this, "click", null));
		this.map.addListener("dblclick", this._onMapClick.bind(this, "dblclick", null));
		this.map.addListener("rightclick", this._onMapClick.bind(this, "rightclick", null));
		this.map.addListener("dragstart", this._onMapDragEvent.bind(this, "mapdragstart"));
		this.map.addListener("dragend", this._onMapDragEvent.bind(this, "mapdragend"));
		this.map.addListener("zoom_changed", this._onMapPropertyChanged.bind(this, "zoom"));
		this.map.addListener("tilt_changed", this._onMapPropertyChanged.bind(this, "tilt"));
		this.map.addListener("maptypeid_changed", this._onMapPropertyChanged.bind(this, "mapTypeId"));
		this.map.addListener("center_changed", this._onMapPropertyChanged.bind(this, "center"));
		this.map.addListener("bounds_changed", this._onMapPropertyChanged.bind(this, "bounds"));
		this.map.addListener("heading_changed", this._onMapPropertyChanged.bind(this, "heading"));
		this.map.addListener("projection_changed", this._onMapPropertyChanged.bind(this, "projection"));

		// collection of markers.
		this.__markers = {};
	}
	else {
		// or update the existing map object.
		this.map.setValues(options);
	}
}

/**
 * Replace the default implementation to fire "render"
 * after the Google Map has been rendered.
 */
this._onInitialized = function () {

	if (wisej.web.DesignMode) {

		if (this.map) {
			var me = this;
			this.map.addListener("tilesloaded", function () {
				setTimeout(function () { me.fireEvent("render"); }, 100);
			});
		}
		else {
			this.fireEvent("render");
		}
	}

	this.fireEvent("initialized");
}


/**
 * Redirects map click events from the google.maps.Map widget.
 * to the wisej GoogleMap component.
 */
this._onMapClick = function (type, marker, e) {

	if (Wisej.Platform.getDeviceType() != "Desktop") {
		if (window.event.eventPhase != 1)
			return;
	}

	var lat = e.latLng ? e.latLng.lat() : 0;
	var lng = e.latLng ? e.latLng.lng() : 0;
	this.fireWidgetEvent(type, { marker: marker, lat: lat, lng: lng });
}

/**
 * Forwards marker drag events to the sever.
 */
this._onMapMarkerDrag = function (type, marker, e) {

	var x = e.pixel.x | 0;
	var y = e.pixel.y | 0;
	var lat = e.latLng ? e.latLng.lat() : 0;
	var lng = e.latLng ? e.latLng.lng() : 0;
	this.fireWidgetEvent(type, { marker: marker, lat: lat, lng: lng, x: x, y: y });
}

/**
 * Redirects property change events from the google.maps.Map widget
 * to the wisej GoogleMap component.
 */
this._onMapPropertyChanged = function (name) {

	var value = null;
	try {
		var getter = this.map["get" + qx.lang.String.firstUp(name)];
		if (getter) {
			value = JSON.parse(JSON.stringify(getter.call(this.map)));
		}
	}
	catch (ex) { }

	this.fireWidgetEvent("propertychanged", { name: name, value: value });
}

/**
 * Fires map drag events.
 */
this._onMapDragEvent = function (type) {

	this.fireWidgetEvent(type);
}

/*
====================================================================
Google Maps Markers
====================================================================
*/

/**
 * Creates a new marker and adds it to the map.
 *
 * @param id {String} unique marker identifier.
 * @param location {google.maps.LatLng | google.maps.LatLngLiteral | String} marker location or address to geocode.
 * @param options {google.maps.MarkerOptions} marker specification.
 * @param center {Boolean} true to center the map at the marker's location.
 */
this.addMarker = function (id, location, options, center) {

	if (!id)
		return;

	if (!location)
		return;

	if (!this.map) {
		this.addListenerOnce("initialized", function (e) {
			this.addMarker(id, location, options, center);
		});
		return;
	}

	// geocode an address.
	if (typeof location == "string") {
		var me = this;
		var geocoder = new google.maps.Geocoder();
		geocoder.geocode({ 'address': location }, function (results, status) {

			if (status == 'OK') {

				me.addMarker(id, results[0].geometry.location, options, center);

			}
			else {

				alert('Geocode was not successful for the following reason: ' + status);
			}
		});

		return;
	}

	options = options || {};
	options.map = this.map;
	options.position = location;

	this.removeMarker(id);
	var marker = this.__markers[id] = new google.maps.Marker(options);
	marker.addListener("click", this._onMapClick.bind(this, "click", id));
	marker.addListener("dblclick", this._onMapClick.bind(this, "dblclick", id));
	marker.addListener("rightclick", this._onMapClick.bind(this, "rightclick", id));

	if (options.draggable) {
		marker.addListener("dragend", this._onMapMarkerDrag.bind(this, "markerdragend", id));
		marker.addListener("dragstart", this._onMapMarkerDrag.bind(this, "markerdragstart", id));
	}

	if (center === true)
		this.map.setCenter(location);
}

/**
 * Removes the marker identified by the id.
 *
 * @param id {String} unique marker identifier.
 */
this.removeMarker = function (id) {

	if (!id || !this.__markers)
		return;

	var marker = this.__markers[id];
	if (marker) {
		marker.setMap(null);
		delete this.__markers[id];
	}
}

/**
 * Removes all markers.
 */
this.clearMarkers = function () {

	if (!this.__markers)
		return;

	for (var id in this.__markers) {
		this.removeMarker(id);
	}
}

/*
====================================================================
Google Maps  Methods
====================================================================
*/

/**
 * Centers the map at he specified location or address.
 *
 * @param location {google.maps.LatLng | google.maps.LatLngLiteral | String} marker location or address to geocode.
 */
this.centerMap = function (location) {

	if (!location)
		return;

	if (!this.map) {
		this.addListenerOnce("initialized", function (e) {
			this.centerMap(location);
		});
		return;
	}

	// geocode an address.
	if (typeof location == "string") {
		var me = this;
		var geocoder = new google.maps.Geocoder();
		geocoder.geocode({ 'address': location }, function (results, status) {

			if (status == 'OK') {

				me.centerMap(results[0].geometry.location);

			}
			else {

				alert('Geocode failed for the following reason: ' + status);
			}
		});

		return;
	}

	this.map.setCenter(location);
}

/**
 * Show a google.maps.InfoWindow in relation to the specified marker.
 *
 * @param id {String} the marker unique id.
 * @param options {Map} the options for the initialization of the InfoWindow.
 */
this.showInfoWindow = function (id, options) {

	if (!id)
		return;

	if (!this.map) {
		this.addListenerOnce("initialized", function (e) {
			this.showInfoWindow(id, content);
		});
		return;
	}

	if (!this.__markers)
		return;

	var marker = this.__markers[id];
	if (marker) {

		options = options || {};
		marker.infoWindow = marker.infoWindow || new google.maps.InfoWindow(options);
		marker.infoWindow.open(this.map, marker);
	}
}

/**
 * Closes the google.maps.InfoWindow related to the specified marker.
 *
 * @param id {String} the marker unique id.
 */
this.closeInfoWindow = function (id) {

	if (!id || !this.map || !this.__markers)
		return;

	var marker = this.__markers[id];
	if (marker) {

		if (marker.infoWindow)
			marker.infoWindow.close();
	}
}

/*
====================================================================
Google Maps Reverse Geocode
====================================================================
*/

/**
 * Retrieves geocode information.
 *
 * @param callbackId {Integer} The id of the request. It should be returned with the response.
 * @param location {google.maps.LatLng | google.maps.LatLngLiteral | String} address to geocode.
 */
this.getGeocode = function (callbackId, location) {

	if (!callbackId)
		return;

	if (!location)
		return;

	var me = this;
	var geocoder = new google.maps.Geocoder();

	// geocode an address.
	if (typeof location == "string") {

		geocoder.geocode({ address: location }, function (results, status) {

			if (status == 'OK') {

				// convert lat/lng functions to geometry properties
				for (i = 0; i < results.length; i++) {
					results[i].geometry.lat = results[i].geometry.location.lat();
					results[i].geometry.lng = results[i].geometry.location.lng();
				}

				me.fireWidgetEvent("callback", { id: callbackId, geocode: results, statusCode: status });
			} else {
				me.fireWidgetEvent("callback", { id: callbackId, statusCode: status });
			}

		});

		return;
	}

	// geocode a location
	geocoder.geocode({ location: location }, function (results, status) {

		if (status == 'OK') {

			// convert lat/lng functions to geometry properties
			for (i = 0; i < results.length; i++) {
				results[i].geometry.lat = results[i].geometry.location.lat();
				results[i].geometry.lng = results[i].geometry.location.lng();
			}

			me.fireWidgetEvent("callback", { id: callbackId, geocode: results, statusCode: status });
		}
		else {
			me.fireWidgetEvent("callback", { id: callbackId, statusCode: status });
		}

	});
}