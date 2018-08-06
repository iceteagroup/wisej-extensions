///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.SmoothieChart
 */
qx.Class.define("wisej.web.ext.SmoothieChart", {

	extend: wisej.web.Control,

	construct: function () {

		this.base(arguments);

		this.addListenerOnce("appear", this.__createSmoothieChart);

	},

	properties: {

		/**
		 * UpdateDelay
		 */
		updateDelay: { init: 1000, check: "PositiveInteger", apply: "_applyUpdateDelay" },

		/**
		 * DataFrequency
		 */
		dataFrequency: { init: 500, check: "PositiveInteger", apply: "_applyDataFrequency" },

		/**
		 * ScrollSpeed
		 */
		scrollSpeed: { init: 20, check: "PositiveInteger", apply: "_applyScrollSpeed" },

		/**
		 * VerticalSections
		 */
		verticalSections: { init: 2, check: "PositiveInteger", apply: "_applyVerticalSections" },

		/**
		 * TimeLineSpacing
		 */
		timeLineSpacing: { init: 1000, check: "PositiveInteger", apply: "_applyTimeLineSpacing" },

		/**
		 * ShowMinMaxLabels
		 */
		showMinMaxLabels: { init: true, check: "Boolean", apply: "_applyShowMinMaxLabels" },

		/**
		 * ShowTimeStamps
		 */
		showTimeStamps: { init: true, check: "Boolean", apply: "_applyShowTimeStamps" },

		/**
		 * Interpolation
		 */
		interpolation: { init: "bezier", check: ["bezier", "linear", "step"], apply: "_applyInterpolation" },

		/**
		 * MinValue
		 */
		minValue: { init: null, nullable: true, check: "Integer", apply: "_applyMinValue" },

		/**
		 * MaxValue
		 */
		maxValue: { init: null, nullable: true, check: "Integer", apply: "_applyMaxValue" },

		/**
		 * GridLineColor
		 */
		gridLineColor: { init: null, nullable: true, check: "Color", apply: "_applyGridLineColor" },

		/**
		 * GridLineSize
		 */
		gridLineSize: { init: 1, check: "PositiveInteger", apply: "_applyGridLineSize" },

		/**
		 * TimeSeries
		 */
		timeSeries: { init: null, nullable: true, check: "Array", apply: "_applyTimeSeries" },

	    /**
		 * FontSize
		 */
		fontSize: { init: null, nullable: true, check: "Integer", apply: "_applyFontSize" },

	},

	members: {

		// the SmoothieChart instance.
		smoothie: null,

		/** Update timer id. */
		__updateTimer: 0,

		/** Synchronization flag to avoid overlapping updates. */
		__processingUpdate: false,

		/** Time gap between the client and the server. Need to sync the clocks. */
		__timeGap: null,

		/** Chart running flag */
		__run: true,

		/**
		 * Starts the chart animation.
		 */
		start: function () {

			this.__run = false;

			if (this.smoothie)
				this.smoothie.start();

		},

		/**
		 * Stops the chart animation.
		 */
		stop: function () {

			this.__run = true;

			if (this.smoothie)
				this.smoothie.stop();
		},

		/**
		 * Applies the updateDelay property.
		 *
		 * It is the time lag between the data received from the server and the screen update.
		 */
		_applyUpdateDelay: function (value, old) {

			if (this.smoothie)
				this.smoothie.delay = value;
		},

		/**
		 * Applies the dataFrequency property.
		 *
		 * It is the interval between data requests.
		 */
		_applyDataFrequency: function (value, old) {

			if (wisej.web.DesignMode)
				return;

			if (this.__updateTimer) {
				clearInterval(this.__updateTimer);
				this.__updateTimer = 0;
			}

			if (value > 0) {
				// retrieve data a bit faster than the update delay.
				this.__updateTimer = setInterval(this.__onUpdateTimer.bind(this), value);
			}
		},

		/**
		 * Applies the scrollSpeed property.
		 */
		_applyScrollSpeed: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.millisPerPixel = value;
		},

		/**
		 * Applies the VerticalSections property.
		 */
		_applyVerticalSections: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.grid.verticalSections = value;
		},

		/**
		 * Applies the TimeLineSpacing property.
		 */
		_applyTimeLineSpacing: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.grid.millisPerLine = value;
		},

		/**
		 * Applies the ShowMinMaxLabels property.
		 */
		_applyShowMinMaxLabels: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.labels.disabled = !value;
		},

		/**
		 * Applies the ShowTimeStamps property.
		 */
		_applyShowTimeStamps: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.timestampFormatter = value ? SmoothieChart.timeFormatter : null;
		},

		/**
		 * Applies the Interpolation property.
		 */
		_applyInterpolation: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.interpolation = value;
		},

		/**
		 * Applies the MinValue property.
		 */
		_applyMinValue: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.minValue = value;
		},

		/**
		 * Applies the MaxValue property.
		 */
		_applyMaxValue: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.maxValue = value;
		},

	    /**
		 * Applies the FontSize property.
		 */
		_applyFontSize: function (value, old) {

		    if (this.smoothie)
		        this.smoothie.options.labels.fontSize = value;
		},

		/**
		 * Applies the GridLineColor property.
		 */
		_applyGridLineColor: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.grid.strokeStyle = this.__resolveColor(value);
		},

		/**
		 * Applies the GridLineSize property.
		 */
		_applyGridLineSize: function (value, old) {

			if (this.smoothie)
				this.smoothie.options.grid.lineWidth = value;
		},

		/**
		 * Overrides _applyBackgroundColor property.
		 */
		_applyBackgroundColor: function (value, old) {

			this.base(arguments, value, old);

			if (this.smoothie)
				this.smoothie.options.grid.fillStyle = this.__resolveColor(value);
		},

		/**
		 * Overrides _applyTextColor property.
		 */
		_applyTextColor: function (value, old) {

			this.base(arguments, value, old);

			if (this.smoothie)
				this.smoothie.options.labels.fillStyle = this.__resolveColor(value);
		},

		/**
		 * Applies the TimeSeries property.
		 */
		_applyTimeSeries: function (value, old) {

			if (!this.smoothie)
				return;

			if (old != null) {
				for (var i = 0; i < old.length; i++) {
					this.smoothie.removeTimeSeries(old[i].timeSeries);
				}
			}

			if (value != null) {
				for (var i = 0; i < value.length; i++) {

					var timeSeries = new TimeSeries();
					var timeSeriesOptions = {
						lineWidth: value[i].lineWidth || 1,
						strokeStyle: this.__resolveColor(value[i].lineColor),
						fillStyle: this.__resolveColor(value[i].fillColor)
					};

					value[i].timeSeries = timeSeries;
					this.smoothie.addTimeSeries(timeSeries, timeSeriesOptions);
				}
			}
		},

		/**
		 * Resolves the theme or named color to a usable html color value.
		 */
		__resolveColor: function (value) {

			return qx.theme.manager.Color.getInstance().resolve(value);
		},

		// overridden
		renderLayout: function (x, y, width, height) {

			this.base(arguments, x, y, width, height);

			// update the canvas size when the dimensions
			// are actually changed. otherwise it gets cleared when setting existing width, height.
			var el = this.getContentElement();
			var currentWidth = el.getAttribute("width");
			var currentHeight = el.getAttribute("height");
			if (currentWidth != width || currentHeight != height) {

				// the canvas element needs the width and height attributes.
				// setting the css dimensions stretches the canvas.
				el.setAttributes({ width: width, height: height }, true /* direct */ );
			}
		},

		/**
		 * Create the instance of the SmoothieChart object
		 * bounds to our canvas element.
		 */
		__createSmoothieChart: function () {

			if (this.smoothie)
				return;

			var canvas = this.getContentElement().getDomElement();

			this.smoothie = new SmoothieChart();
			this.smoothie.canvas = canvas;
			this.smoothie.delay = this.getUpdateDelay();
			this.smoothie.options.millisPerPixel = this.getScrollSpeed();
			this.smoothie.options.grid.fillStyle = this.__resolveColor(this.getBackgroundColor());
			this.smoothie.options.grid.strokeStyle = this.__resolveColor(this.getGridLineColor());
			this.smoothie.options.labels.fillStyle = this.__resolveColor(this.getTextColor());
			this.smoothie.options.grid.verticalSections = this.getVerticalSections();
			this.smoothie.options.grid.millisPerLine = this.getTimeLineSpacing();
			this.smoothie.options.grid.sharpLines = true;
			this.smoothie.options.grid.borderVisible = false;
			this.smoothie.options.grid.lineWidth = this.getGridLineSize();
			this.smoothie.options.labels.disabled = !this.getShowMinMaxLabels();
			this.smoothie.options.interpolation = this.getInterpolation();
			this.smoothie.options.minValue = this.getMinValue();
			this.smoothie.options.maxValue = this.getMaxValue();			
			this.smoothie.options.timestampFormatter = this.getShowTimeStamps() ? SmoothieChart.timeFormatter : null;
			this.smoothie.options.fontSize = this.getFontSize();

			// initialize the time series lines after the smoothie has been created.
			this._applyTimeSeries(this.getTimeSeries());

			// start streaming, if not in design mode.
			if (!wisej.web.DesignMode) {
				if (this.__run)
					this.smoothie.start();
			}
		},

		// overridden to render the chart before firing the "render" event.
		_onDesignRender: function () {

			this.__createSmoothieChart();

			this.smoothie.render();

			var me = this;
			setTimeout(function () {
				me.fireEvent("render");
			}, 10);
		},

		/**
		 * Updates the widget pulling data from the control.
		 */
		__onUpdateTimer: function () {

			if (this.__processingUpdate)
				return;

			this.__processingUpdate = true;

			// call the server-side method getData(start, count) and
			// receive the return value in the callback.
			//
			// parameters are just an example.
			// you can pass strings, dates, numbers and even objects.

			var me = this;

			this.GetData(

				// return value.
				function (array) {

					if (array && array.length > 0) {

						var lines = me.getTimeSeries();
						if (lines == null)
							return;

						for (var i = 0, count = Math.min(lines.length, array.length); i < count; i++) {

							var data = array[i];
							var line = lines[i].timeSeries;

							// calculate the time gap, once.
							if (me.__timeGap == null)
								me.__timeGap = Date.now() - data.time.getTime();

							if (line)
								line.append(data.time.getTime() + me.__timeGap, data.value);
						}
					}

					// release the sync lock.
					me.__processingUpdate = false;


				}
			);
		},

		// overridden
		_createContentElement: function () {

			return new qx.html.Element("canvas");
		},
	},

});