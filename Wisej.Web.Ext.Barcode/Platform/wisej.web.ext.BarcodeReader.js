///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Levie Rufenacht
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
 * wisej.web.ext.Barcode
 *
 * This component uses the zxing js library (https://github.com/zxing-js/library)
 * to integrate real time barcode scanning into your application.
 */
qx.Class.define("wisej.web.ext.BarcodeReader", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	properties: {

		/**
		 * ScanMode property.
		 * 
		 * The scan mode of the camera (Continuous or Once)
		 */
		scanMode: { init: "automatic", check: ["automatic", "automaticOnce", "manual"], apply: "_applyScanMode" },

		/**
		 * Camera property.
		 * 
		 * Assigns the barcode scanner to the camera.
		 */
		camera: { init: null, nullable: true, transform: "_transformComponent", apply: "_applyCamera" }
	},

	members: {

		// automatic barcode reader.
		codeReader: null,

		// separate barcode reader for processing scanImage requests.
		cameraReader: null,

		/**
		 * Applies the new camera instance.
		 * @param {any} value The new camera instance.
		 */
		_applyCamera: function (value) {

			if (value) {

				var video = value.getMediaObject();
				var videoID = "camera_" + this.getId();
				video.id = videoID;
			}
		},

		/**
		 * Starts or stops recording of barcode events.
		 * @param {any} value The scan mode.
		 */
		_applyScanMode: function (value) {

			if (value !== "manual")
				this.startMonitoring();
			else
				this.stopMonitoring();
		},

		/**
		 * Attaches to the Camera property and starts recording scan events.
		 **/
		startMonitoring: function () {

			var me = this;
			var camera = this.getCamera();

			if (!camera)
				return;

			var video = camera.getMediaObject();

			// if the video source isn't ready yet, schedule a callback.
			if (!video.srcObject) {

				var play = function (e) {
					me.startMonitoring();
					video.onplay = null;
				}
				video.onplay = play;
				return;
			}

			if (!this.codeReader)
				this.codeReader = new ZXing.BrowserMultiFormatReader();

			this.codeReader.decodeFromVideoDevice(null, video.id, function (result, err) {

					if (result) {

						me.fireDataEvent("scanSuccess", result.text);

						if (me.getScanMode() !== "automatic")
							me.codeReader.stopContinuousDecode();
					}
				});
		},

		/**
		 * Stops recording barcode events.
		 **/
		stopMonitoring: function () {

			if (this.codeReader)
				this.codeReader.stopContinuousDecode();
		},

		/**
		 * Scans the last image from the attached camera.
		 * Fires success / error.
		 **/
		scanImage: function () {

			var camera = this.getCamera();
			var video = camera.getMediaObject();

			if (video.srcObject) {

				if (!this.cameraReader)
					this.cameraReader = new ZXing.BrowserMultiFormatReader();

				var me = this;
				this.cameraReader.decodeFromVideoDevice(null, video.id, function (result, error) {

					if (error) {
						me.fireDataEvent("scanError", error.message);
					} else {
						me.fireDataEvent("scanSuccess", result.text);
					}

					me.cameraReader.stopContinuousDecode();
				});
				
			} else {
				this.fireDataEvent("scanError", "Couldn't capture an image from the camera.");
			}
		},

		/**
		 * Resets the scanner when ScanMode = "automaticOnce".
		 **/
		resetScanner: function () {

			if (this.getScanMode() === "automaticOnce")
				this.startMonitoring();
		}
	},

	/**
	 * Performs cleanup by stopping barcode scanners.
	 **/
	destruct: function () {

		if (this.codeReader) {
			this.codeReader.stopContinuousDecode();
			this.codeReader = null;
		}
		
		if (this.cameraRecorder) {
			this.cameraRecorder.stopContinuousDecode();
			this.cameraRecorder = null;
		}
	}
});