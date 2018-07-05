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
 * wisej.web.ext.PolymerComponent
 *
 * Represents a Polymer component. This class is
 * able to import polymer libraries.
 * 
 * See: https://www.polymer-project.org
 */
qx.Class.define("wisej.web.ext.PolymerComponent", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	statics: {

		/**
		 * Base URL for the polymer library and components.
		 */
		PolymerBaseUrl: "https://wisej.s3.amazonaws.com/libs/polymers/",

		/**
		 * Cached loaded imports.
		 */
		loadedImports: {},

		/**
		 * Imports Polymer libraries.
		 *
		 * @param url {String} polymer library to load. 
		 * @param onload {Function} success callback(url).
		 * @param onerror {Function} error callback(event, url).
		 * @param context {Object} callback context.
		 */
		loadPolymer: function (url, onload, onerror, context) {

			if (!url)
				return;

			url = this.getPolymerUrl(url);

			if (!wisej.web.DesignMode)
				Wisej.Core.logInfo("Loading Polymer from: ", url);

			// already loaded?
			if (this.loadedImports[url]) {
				if (onload)
					return onload.call(context, url);

				return;
			}

			var l = document.createElement("link");
			l.rel = "import";
			l.href = url;

			var me = this;
			l.onload = function (e) {

				me.loadedImports[url] = true;

				if (!wisej.web.DesignMode)
					Wisej.Core.logInfo("Polymer loaded successfully.");

				if (onload)
					onload.call(context, url);
			};

			if (onerror) {
				l.onerror = function (e) {

					if (!wisej.web.DesignMode)
						Wisej.Core.logError("Error loading Polymer.");

					onerror.call(context, e, url);
				};
			}

			document.head.appendChild(l);
		},

		/**
		 * Returns the absolute url for the polymer library.
		 * When the specified url is not rooted, it is appended to PolymerBaseUrl.
		 *
		 * @param url {String} Absolute or relative url.
		 */
		getPolymerUrl: function (url) {

			if (url.indexOf("http:") == -1 && url.indexOf("https:") == -1)
				url = this.PolymerBaseUrl + url;

			return url;
		},
	},

	properties: {

		/**
		 * Imports property.
		 * 
		 * Array of libraries to import. Each entry can be absolute or relative
		 * to the base URL defined in wisej.web.ext.PolymerComponent.POLYMER_BASE_URL.
		 */
		imports: { init: null, check: "Array", apply: "_applyImports" },
	},

	members: {

		/**
		 * Loads the imports.
		 *
		 * Libraries are cached in a static map and are loaded only once.
		 */
		_applyImports: function (value, old) {

			if (!value || value.length == 0)
				return;

			setTimeout(function () {

				var loaded = wisej.web.ext.PolymerComponent.loadedImports;

				for (var i = 0; i < value.length; i++) {
					var url = value[i];
					wisej.web.ext.PolymerComponent.loadPolymer(url);
				}

			}, 1);
		},
	}

});