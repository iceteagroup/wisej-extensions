///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.ext.Tesseract
 *
 * This component uses the Tesseract js library (https://github.com/naptha/tesseract.js/)
 * to integrate real time text scanning into your application.
 */
qx.Class.define("wisej.ext.Tesseract", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		wisej.utils.Loader.load([
			{
				id: "tesseract.js",
				url: "resource.wx/Wisej.Ext.Tesseract/JavaScript/tesseract.min.js"
			}
		]);
	},

	properties: {

		/**
		 * Camera property.
		 * 
		 * Assigns the barcode scanner to the camera.
		 */
		camera: { init: null, nullable: true, transform: "_transformComponent", apply: "_applyCamera" },

		enabled: { init: false, apply: "_applyEnabled" },

		interval: { init: 0, apply: "_applyInterval" },

		keywords: { init: null, nullable: true },

		language: { init: "", apply: "_applyLanguage" },

		whitelist: { init: "", apply: "_applyWhitelist" },

		workerCount: { init: 0, apply: "_applyWorkerCount" },

		showWords: { init: false }
	},

	members: {

		index: 0,

		workers: null,

		canvas: null,

		overlayCanvas: null,

		timer: null,

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
		 * Starts or stops recording of text events.
		 * @param {any} value The enabled state.
		 */
		_applyEnabled: function (value) {

			if (value) {
				this.startMonitoring();
			} else {
				this.stopMonitoring();
			}
		},

		_applyWhitelist: function (value) {

			if (!this.workers)
				return;

			this.workers.forEach(async function (worker) {
				await worker.setParameters({
					tessedit_char_whitelist: value,
				});
			});
		},

		_applyLanguage: function (value) {

			if (!this.workers)
				return;

			this.workers.forEach(async function (worker) {
				await worker.loadLanguage(value);
			});
		},

		_applyInterval: function (value) {

			if (!this.timer)
				return;

			this.timer.setInterval(value);
		},

		_applyWorkerCount: function (value) {

			if (typeof Tesseract == "undefined") {
				qx.event.Timer.once(this._applyWorkerCount, this, 100);
				return;
			}

			if (!this.workers)
				this.workers = [];

			(async () => {

				var count = this.getWorkerCount() - this.workers.length;
				if (count > 0) {
					for (var i = 0; i < count; i++) {
						var me = this;
						var worker = await Tesseract.createWorker({
							errorHandler: function (e) {
								me.fireDataEvent("workerError", e);
							}
						});
						await worker.loadLanguage(this.getLanguage());
						await worker.initialize(this.getLanguage());
						await worker.setParameters({
							tessedit_char_whitelist: this.getWhitelist(),
						});

						this.workers.push(worker);
					}
				} else {
					var removedWorkers = this.workers.slice(0, count * -1);
					this.workers = this.workers.slice(count * -1);

					removedWorkers.forEach(function (worker) {
						worker.terminate();
					});
				}
			})();
		},

		/**
		 * Attaches to the Camera property and starts recording text detection events.
		 **/
		startMonitoring: function () {

			if (typeof Tesseract == "undefined") {
				qx.event.Timer.once(this.startMonitoring, this, 100);
				return;
			}

			var camera = this.getCamera();
			if (!camera)
				return;

			var video = camera.getMediaObject();
			var stream = video.srcObject;

			// if the video source isn't ready yet, schedule a callback.
			if (!stream) {
				qx.event.Timer.once(this.startMonitoring, this, 100);
				return;
			}
			
			if (!this.timer)
				this.timer = new qx.event.Timer(this.getInterval());

			this.timer.addListener("interval", function () {
				this.processLastCameraImage();
			}, this);

			this.timer.start();
		},

		/**
		 * Stops recording text detection events.
		 **/
		stopMonitoring: function () {
			if (this.timer)
				this.timer.stop();
		},

		/**
		 * Scans the last image from the attached camera.
		 * Fires success / error.
		 **/
		processLastCameraImage: function () {

			if (typeof Tesseract == "undefined") {
				qx.event.Timer.once(this.processLastCameraImage, this, 100);
				return;
			}

			var camera = this.getCamera();
			var video = camera.getMediaObject();

			var stream = video.srcObject;
			if (stream) {
				if (!this.canvas) {
					this._insertOverlayCanvas(video);
					this.canvas = document.createElement("canvas");
				}

				var width = camera.getWidth();
				var height = camera.getHeight();

				this.canvas.width = width;
				this.canvas.height = height;

				this.canvas.getContext('2d').drawImage(video, 0, 0, width, height);

				(async () => {

					if (!this.workers || this.workers.length == 0)
						return;

					var me = this;
					var text = await this.getImageText(this.canvas.toDataURL());
					var keywords = this.getKeywords();

					this.overlayCanvas.getContext("2d").clearRect(0, 0, this.overlayCanvas.width, this.overlayCanvas.height);

					if (text && this.getShowWords())
						this._drawRectangles(result.data.words);

					if (keywords && keywords.length > 0) {
						keywords.forEach(function (keyword) {
							if (text.includes(keyword))
								me.fireDataEvent("textRecognized", me._transformResult(result));
						});
					}
					else {
						me.fireDataEvent("textRecognized", me._transformResult(result));
					}
				})();

			} else {
				this.fireDataEvent("scanError", "Couldn't capture an image from the camera.");
			}
		},

		getImageText: async function (image) {

			if (this.index >= this.workers.length)
				this.index = 0;

			var result = await this.workers[this.index++].recognize(image);
			return result.data.text;
		},

		scanImage: function (image) {
			var me = this;
			return (async function () {
				return me.getImageText(image);
			})();
		},

		_insertOverlayCanvas: function (video) {

			this.overlayCanvas = document.createElement('canvas');
			this.overlayCanvas.style.position = 'absolute';
			this.overlayCanvas.style.top = '0';
			this.overlayCanvas.style.left = '0';
			this.overlayCanvas.style.pointerEvents = 'none'; // Ensures mouse events are passed to the video element below
			this.overlayCanvas.width = video.clientWidth;
			this.overlayCanvas.height = video.clientHeight;
			video.parentElement.appendChild(this.overlayCanvas);
		},

		_drawRectangles: function (words) {

			var context = this.overlayCanvas.getContext("2d");

			words.forEach(function (word) {

				if (word.confidence >= 30 && word.text.trim().length > 1) {
					var bbox = word.bbox;
					context.beginPath();
					context.rect(bbox.x0, bbox.y0, bbox.x1 - bbox.x0, bbox.y1 - bbox.y0);
					context.strokeStyle = "red";
					context.lineWidth = 2;
					context.stroke();
				}
			});
		},

		_transformResult: function(result) {
			return {
				text: result.data.text,
				confidence: result.data.confidence,
				words: result.data.words.map(w => w.text),
			}
		}
	},

	/**
	 * Performs cleanup by stopping text scanners.
	 **/
	destruct: function () {

		this.stopMonitoring();

		if (this.canvas)
			this.canvas.remove();

		if (this.overlayCanvas)
			this.overlayCanvas.remove();

		if (this.workers) {
			this.workers.forEach(function (worker) {
				worker.terminate();
			});
		}
	}
});