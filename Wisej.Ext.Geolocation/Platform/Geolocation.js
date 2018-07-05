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

/**
 * wisej.ext.Geolocation
 *
 * The Geolocation interface represents an object able to programmatically obtain the position of the device.
 */
qx.Class.define("wisej.ext.Geolocation", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);
	},

	properties: {

		/**
		 * ActiveWatch property.
		 *
		 * Enables or disables active watch mode for this geolocation component.
		 */
		activeWatch: { init: false, check: "Boolean", apply: "_applyActiveWatch" },

		/**
		 * HighAccuracy property.
		 *
		 * Indicates the application would like to receive the best possible results.
		 */
		highAccuracy: { init: false, check: "Boolean" },

		/**
		 * Timeout property.
		 *
		 * Sets or gets the maximum length of time (in milliseconds) the device is allowed to take in order to return a position.
		 */
		timeout: { init: -1, check: "Integer" },

		/**
		 * MaxAge property.
		 *
		 * Indicates the maximum age in milliseconds of a possible cached position that is acceptable to return.
		 */
		maxAge: { init: -1, check: "Integer" },
	},

	members: {

		// the current active watch id.
		__activeWatchId: 0,

		/**
		 *  Requests the current  position.
		 *
		 * @param id {Integer} The id of the request. It should be returned with the response.
		 */
		getPosition: function (id) {

			var geo = navigator.geolocation;
			if (!geo) {
				// if geolocation is not supported but the server called this method anyway, we respond
				// immediately with error PERMISSION_DENIED.
				this.fireDataEvent("callback", { id: id, errorCode: 1 });
				return;
			}

			var me = this;
			var callbackId = id;
			var options = {
				enableHighAccuracy: this.getHighAccuracy(),
				timeout: this.getTimeout() == -1 ? Infinity : this.getTimeout(),
				maximumAge: Infinity
			};

			// Note: alway use Infinity for maximumAge when calling getCurrentPosition() to force the return
			// of a location, otherwise we may have to wait several minutes to get a response to the call.

			geo.getCurrentPosition(

				// success
				function (position) {

					// convert the timestamp to a localized date.

					me.fireDataEvent("callback", { id: callbackId, position: position, errorCode: 0 /* success */ });
				},

				// error
				function (error) {
					me.fireDataEvent("callback", { id: callbackId, errorCode: error.code, errorMessage: error.message });
				},

				options
			);
		},

		/**
		 * Starts or stops the active watch mode.
		 */
		_applyActiveWatch: function (value, old) {

			var geo = navigator.geolocation;
			if (!geo)
				return;

			if (this.__activeWatchId) {
				geo.clearWatch(this.__activeWatchId);
				this.__activeWatchId = 0;
			}

			if (!value)
				return;

			var me = this;
			var options = {
				enableHighAccuracy: this.getHighAccuracy(),
				timeout: this.getTimeout() == -1 ? Infinity : this.getTimeout(),
				maximumAge: this.getMaxAge() == -1 ? Infinity : this.getMaxAge()
			};

			this.__activeWatchId = geo.watchPosition(

				// success
				function (position) {
					me.fireDataEvent("positionChanged", { position: position, errorCode: 0 });
				},

				// error
				function (error) {
					me.fireDataEvent("positionChanged", { position: null, errorCode: error.code });
				},

				options
			);
		},
	},

});
