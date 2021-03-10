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
 * wisej.web.ext.ScreenRecorder
 *
 * The Screen Recorder interface makes it possible to record the user's screen.
 */
qx.Class.define("wisej.web.ext.ScreenRecorder", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);
	},

	properties: {

		/**
		 * Constraints property.
		 * 
		 * See: https://developer.mozilla.org/en-US/docs/Web/API/MediaStreamConstraints
		 */
		constraints: { init: null, check: "Map", apply: "_applyConstraints" },

		/**
		 * SubmitURL property.
		 *
		 * The URL to use to send the files to the server.
		 */
		submitURL: { init: "", check: "String" }
	},

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

	members: {

		// a new media element.
		_media: null,

		// canvas context.
		canvas: null,

		// recorder.
		recordedBlob: null,
		mediaRecorder: null,

		/**
		 * Returns the current snapshot from the camera in base64.
		 */
		getImage: function () {

			var ctx = this.__getCanvasContext();
			if (ctx) {

				ctx.drawImage(this._media.getMediaObject(), 0, 0, this.canvas.width, this.canvas.height);
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
				bitsPerSecond: bitsPerSecond,
				mimeType: format || "video/webm"
			};

			try {

				var me = this;
				// creates a MediaRecorder instance from the current stream.
				this.mediaRecorder = new MediaRecorder(stream, options);
				this.mediaRecorder.ondataavailable = function (event) {

					me.recordedBlob = new Blob([event.data]);

					// process blobs and send them to wisej.
					wisej.web.ext.ScreenRecorder.uploadRecording(me.recordedBlob, me.getSubmitURL(), {

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
		 * Applies the Constraints property.
		 */
		_applyConstraints: function (value, old) {

			debugger;
			if (wisej.web.DesignMode)
				return;

			if (!this._media)
				this._media = new qx.bom.media.Video();

			var me = this;

			var navigatorMedia = null;

			if (navigator.getDisplayMedia) {
				navigatorMedia = navigator.getDisplayMedia(value);
			} else if (navigator.mediaDevices.getDisplayMedia) {
				navigatorMedia = navigator.mediaDevices.getDisplayMedia(value);
			} else {
				navigatorMedia = navigator.mediaDevices.getUserMedia(value);
			}

			navigatorMedia
				.then(function (stream) {

					// bind to the video element.
					me._bindStream(stream);
				});
		},

		// creates a hidden <canvas> element to capture the screen image.
		__getCanvasContext: function () {

			if (!this.canvas) {
				var bounds = this.getBounds();
				var el = document.createElement("canvas");
				el.style.display = "none";
				el.width = bounds.width;
				el.height = bounds.height;
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
				video.setAttribute("playsinline", "");
				video.play();
			}
		},

		/**
		 * Tells the user an error occurred while registering the stream.
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
		}
	},

	destruct: function () {

		if (this.canvas) {

			this.canvas.remove();
			this.canvas = null;
		}

		this.recordedBlob = null;
		this.mediaRecorder = null;
	}

});
