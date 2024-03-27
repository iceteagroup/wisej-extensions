///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.Camera
 *
 * The Camera interface makes it possible to take pictures with the device's camera and upload them to the server.
 */
qx.Class.define("wisej.web.ext.Camera", {

	extend: wisej.web.Video,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [
		wisej.mixin.MBorderStyle
	],

	statics: {

		/** 
		 * Uploads the files to the server using the submitUrl which may wire the
		 * request to a specific component or to the application instance (id="app").
		 *
		 * @param blob {Blob} the blob to package and send.
		 * @param submitUrl {String} the url to submit the data to.
		 * @param callbacks {Map} a map with the following handlers:
		 *
		 *		uploading(fileList[])
		 *		uploaded(fileList[])
		 *		completed(error)
		 *
		 */
		uploadRecording: function (blob, submitUrl, callbacks) {

			if (!submitUrl)
				return;

			// normalize the callbacks.
			callbacks = callbacks || {};

			var formData = new FormData();
			formData.append("file", blob, "video");

			// send the data to our handler.
			var xhr = new XMLHttpRequest();
			xhr.open("POST", submitUrl, true);
			xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
			xhr.setRequestHeader("X-Wisej-RequestType", "Postback");

			// uploading...
			if (callbacks.uploading)
				callbacks.uploading(fileList, xhr);

			// progress callback.
			xhr.upload.onprogress = callbacks.progress;

			xhr.onreadystatechange = function () {

				if (xhr.readyState == 4) {

					if (xhr.status === 200) {

						// uploaded...
						if (callbacks.uploaded)
							callbacks.uploaded(fileList);

						// completed...
						if (callbacks.completed)
							callbacks.completed();

						// let Wisej process the response from the server.
						Wisej.Core.processResponse.call(Wisej.Core, xhr.responseText);

					} else {

						// completed with error...
						if (callbacks.completed)
							callbacks.completed({ error: "upload", message: xhr.statusText });

					}
				}
			};

			xhr.send(formData);
		}
	},

	construct: function () {

		this.base(arguments);

		this.__readDeviceList();
	},

	properties: {

		appearance: { init: "widget", refine: true },

		/**
		 * Mirror property.
		 *
		 * Specifies whether the media stream should be mirrored.
		 */
		mirror: { init: false, check: "Boolean", apply: "_applyMirror" },

		/**
		 * Constraints property.
		 * 
		 * See: https://developer.mozilla.org/en-US/docs/Web/API/MediaStreamConstraints
		 */
		constraints: { init: null, check: "Map", apply: "_applyConstraints" },

		/**
		 * Filter property.
		 * 
		 * Sets the CSS filter to the video element: https://developer.mozilla.org/en-US/docs/Web/CSS/filter.
		 */
		videoFilter: { check: "String", apply: "_applyFilter" },

		/*
		 * object-fit Property
		 *
		 * The CSS object-fit property is used to specify how an video should be resized to fit its container.
		 * https://www.w3schools.com/css/css3_object-fit.asp
		 */
		objectFit: { check: "String", apply: "_applyObjectFit" },

		/**
		 * SubmitURL property.
		 *
		 * The URL to use to send the files to the server.
		 */
		submitURL: { init: "", check: "String" },

		/**
		 * Applies a zoom level to the camera.
		 */
		zoom: { init: 0, apply: "_applyZoom" }
	},

	members: {

		// canvas context.
		canvas: null,

		// recorder.
		recordedBlob: null,
		mediaRecorder: null,

		// cached device list.
		deviceList: null,

		/**
		 * Returns the current snapshot from the camera in base64.
		 */
		getImage: function () {

			var mediaObject = this._media.getMediaObject();
			var height = mediaObject.videoHeight;
			var width = mediaObject.videoWidth;
			
			var ctx = this.__getCanvasContext(height, width);
			if (ctx) {

				ctx.filter = this.getVideoFilter();
				ctx.drawImage(mediaObject, 0, 0, width, height);

				return this.canvas.toDataURL();
			}

			return null;
		},

		/**
		 * Starts recording the MediaStream with the specified configuration.
		 * @param {String?} format The video encoding mime type format, see https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types.
		 * @param {Integer?} bitsPerSecond Audio and video bits per second. see https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder/MediaRecorder.
		 * @param {Integer?} updateInterval Update interval in seconds. The default is zero causing the video to be uploaded on stopRecording().
		 */
		startRecording: function (format, bitsPerSecond, updateInterval) {

			var stream = this._media.getMediaObject().srcObject;
			if (stream == null) {
				this.handleError(new Error("No Stream."));
				return;
			}

			var options = {
				bitsPerSecond: bitsPerSecond
			};
			if (format)
				options.format = format;

			try {

				var me = this;
				// creates a MediaRecorder instance from the current stream.
				this.mediaRecorder = new MediaRecorder(stream, options);
				this.mediaRecorder.ondataavailable = function (event) {

					me.recordedBlob = new Blob([event.data]);

					// process blobs and send them to wisej.
					wisej.web.ext.Camera.uploadRecording(me.recordedBlob, me.getSubmitURL(), {

						progress: function (evt) {

							me.fireDataEvent("progress", { loaded: evt.loaded, total: evt.total });

						}
					});
				};

				if (updateInterval > 0) {
					var task = setInterval(function () {

						if (me.mediaRecorder && me.mediaRecorder.state === "recording")
							me.mediaRecorder.requestData();
						else
							clearInterval(task);

					}, updateInterval * 1000);
				}

				this.mediaRecorder.start();

			} catch (e) {

				this.handleError(e);
			}
		},

		/**
		 * Gets the video media object.
		 **/
		getMediaObject: function () {

			return this._media.getMediaObject();
		},

		/**
		 * Stops the recording of the MediaStream and sends the recording to Wisej.
		 */
		stopRecording: function () {

			if (!this.mediaRecorder || this.mediaRecorder.state !== "recording") {
				this.handleError(new Error("No active media recording."));
				return;
			}

			this.mediaRecorder.stop();
			this.mediaRecorder = null;
		},

		/**
		 * Applies the mirror property.
		 * @param {any} value
		 * @param {any} old
		 */
		_applyMirror: function (value, old) {

			var scaleFactor = value ? "-1" : "1";
			var media = this._media.getMediaObject();

			media.style.transform = `scaleX(${scaleFactor})`;
		},

		_applyZoom: function (value, old) {

			var media = this._media.getMediaObject();
			var stream = media.srcObject;
			if (stream != null) {
				var track = stream.getVideoTracks()[0];
				var capabilities = track.getCapabilities();
				var zoom = capabilities.zoom;
				if (zoom) {
					// ensure value in range.
					value = Math.max(zoom.min, Math.min(zoom.max, value));
					track.applyConstraints({ advanced: [{ zoom: value }] });
				}
			}
		},

		/**
		 * Applies the Constraints property.
		 */
		_applyConstraints: function (value, old) {

			if (wisej.web.DesignMode)
				return;

			this.deactivateCamera();

			var constraints = value;

			// convert deviceName to the corresponding deviceId.
			if (constraints && constraints.video && constraints.video.deviceName) {

				if (!this.deviceList) {
					this.__readDeviceList(function () {
						this._applyConstraints(value);
					});
					return;
				}

				var name = constraints.video.deviceName;
				var device = this.deviceList.find(function (device) { return device.label == name; });
				if (device)
					constraints.video.deviceId = device.deviceId;
			}

			var me = this;
			navigator.mediaDevices.getUserMedia(constraints)
				.then(function (stream) {

					// bind to the video element.
					me._bindStream(stream);
				});
		},

		/**
		 * Detaches the camera feed and clears the tracks.
		 **/
		deactivateCamera: function () {

			var video = this.getMediaObject();
			if (video) {
				video.pause();

				var stream = video.srcObject;
				if (stream) {
					stream.getTracks().forEach(function (track) {
						track.stop();
					});

					stream = null;
					video.src = "";
					video.srcObject = null;
				}
			}
		},

		/**
		 * Returns the list of available devices.
		 * 
		 * @param {Boolean?} refresh Reloads the device list from the browser.
		 */
		getDevices: function (refresh) {

			refresh = refresh == true;

			if (this.deviceList && !refresh) {
				return this.deviceList.map(function (item) { return item.label; });
			}
			else {
				var me = this;
				return new Promise(function (resolve) {
					me.__readDeviceList(function () {
						resolve(me.deviceList.map(function (item) { return item.label; }));
					});
				});
			}
		},

		/**
		 * Applies the Filter property.
		 */
		_applyFilter: function (value, old) {

			var video = this._media.getMediaObject();
			if (video) {
				video.style.filter = value;
				video.style.width = "100%";
				video.style.height = "100%";
			}
		},

		/**
		 * Applies the object-fit property.
		 */
		_applyObjectFit: function (value, old) {

			var video = this._media.getMediaObject();
			if (video) {
				video.style.objectFit = value;
			}
		},

		// creates a hidden <canvas> element to capture the camera image.
		__getCanvasContext: function (height, width) {

			if (!this.canvas) {
				var el = document.createElement("canvas");
				el.style.display = "none";
				el.width = width;
				el.height = height;
				document.body.appendChild(el);

				this.canvas = el;
			}

			return this.canvas.getContext("2d");
		},

		/**
		 * Binds the stream to the underlying video element.
		 */
		_bindStream: function (stream) {

			var video = this._media.getMediaObject();
			if (video) {
				video.srcObject = stream;

				this._applyZoom(this.getZoom());

				video.setAttribute("playsinline", "");
				video.play();
			}
		},

		/**
		 * Tells the user an error occurred while registering the camera or audio.
		 * @param {any} error
		 */
		handleError: function (error) {

			switch (error.name) {

				case "TypeError":
					// the user wants to turn off the video, discard error.
					break;

				default:
					this.fireDataEvent("error", error.message);
					break;
			}
		},

		__readDeviceList: function (callback) {

			if (typeof navigator == "undefined" ||
				typeof navigator.mediaDevices == "undefined") {

				if (callback)
					callback();

				return;
			}

			var me = this;
			navigator.mediaDevices.enumerateDevices().then(function (list) {

				me.deviceList = list.filter(function (device) {
					return device.kind == "videoinput" && device.label;
				});

				if (callback)
					callback.call(me);
			});

		}
	},

	destruct: function () {

		this.deactivateCamera();
		this.recordedBlob = null;

		if (this.canvas) {
			this.canvas.remove();
			this.canvas = null;
		}

		if (this.mediaRecorder) {
			this.mediaRecorder.stop();
			this.mediaRecorder = null;
		}
	}
});
