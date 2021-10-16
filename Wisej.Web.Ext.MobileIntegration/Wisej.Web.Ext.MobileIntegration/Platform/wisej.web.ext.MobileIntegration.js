///////////////////////////////////////////////////////////////////////////////
//
// (C) 2019 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.MobileIntegration
 *
 *
 * Manages the communication between the Wisej.Web.Ext.Device class and the
 * native mobile application.
 */
qx.Class.define("wisej.web.ext.MobileIntegration", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	statics: {

		// the singleton instance.
		__singleton: null,

		/**
		 * Returns the singleton instance.
		 */
		getInstance: function () {
			return wisej.web.ext.MobileIntegration.__singleton;
		}

	},

	construct: function () {

		if (wisej.web.ext.MobileIntegration.__singleton)
			wisej.web.ext.MobileIntegration.__singleton.dispose();

		window.Wisej.Mobile = this;
		wisej.web.ext.MobileIntegration.__singleton = this;

		this.__ios = null;
		this.__android = null;

		// determine the iOS handler.
		try {
			this.__ios = window.webkit.messageHandlers.Device;
		}
		catch (err) { }

		// TODO: determine the Android handler.
		try {
			this.__android = Device;
		}
		catch (err) { }

		this.base(arguments);
	},

	members: {

		/** iOS handler. */
		__ios: null,

		/** Android handler. */
		__android: null,

		/**
		 * Posts a message to the device.
		 */
		postMessage: function (type, args) {

			if (this.__ios)
				this.__ios.postMessage({ type: type, data: args });
			else if (this.__android)
				this.__android.postMessage(JSON.stringify({ type: type, data: args }));
		}
	}

});