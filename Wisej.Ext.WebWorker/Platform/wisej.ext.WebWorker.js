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
 * wisej.ext.WebWorker
 *
 */
qx.Class.define("wisej.ext.WebWorker", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	properties: {

		/**
		 * SourceUrl property.
		 *
		 * The URL for the code to run in the WebWorker.
		 */
		sourceUrl: { init: "", check: "String", apply: "_applySourceUrl" },
	},

	members: {

		// the current Worker instance manager by this component.
		__worker: null,

		/**
		 * Sends the data to the current Worker.
		 *
		 * @param data {Object} data object to pass to the Worker.
		 */
		sendMessage: function (data) {

			this.__createWorker();

			if (this.__worker != null)
				this.__worker.postMessage(data);
		},

		/**
		 * Terminates the current Worker.
		 */
		terminate: function () {

			if (this.__worker) {
				//this.__worker.terminate();
				this.__worker.dispose();
				this.__worker = undefined;
			}
		},

		/**
		 * Starts the Worker using the code.
		 */
		_applySourceUrl: function (value, old) {

			this.terminate();
			this.__createWorker();
		},

		__createWorker: function () {

			if (this.__worker != null)
				return this.__worker;

			var codeUrl = this.getSourceUrl();
			if (codeUrl) {

				this.__worker = new qx.bom.WebWorker(codeUrl);

				/*var me = this;
				this.__worker = new Worker(codeUrl);
				this.__worker.onmessage = function (e) {
					me.fireDataEvent("postMessage", e.data);
				};*/

				this.__worker.addListener("message", function (e) {
					this.fireDataEvent("postMessage", e.getData());
				}, this);
			}

			return this.__worker;
		},
	},

});
