//# sourceURL=wisej.web.ext.ChartJS4.startup.js

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
this.init = function (config) {

	var me = this;

	Chart.register(ChartDataLabels);

	config.options.responsive = true;
	config.options.maintainAspectRatio = false;
	config.type = config.type === "horizontalBar" ? "bar" : config.type;

	this.__processPlugins(config);

	this.__processWidgetFunctions(config);

	// process the datasets.
	if (config.data.datasets)
		this.__processDataSets(config.data.datasets);

	// processes the scales format.
	this.__processScales(config.options);

	// convert fonts and colors from Wisej maps to
	// the appropriate field in the options map.
	this.__setFontAndColors(config.options);

	// convert color arrays with 1 element to single values.
	this.__normalizeColorArrays(config.data.datasets);

	// convert point style and colour arrays in line chart datasets into single values
	this.__normalizeLineChartDataSetArrays(config.data.datasets);

	// destroy the previous Chart object.
	// NOTE: if the server only changed the data set
	// it should call "UpdateDataSet" instead of "Update".
	if (this.chart != null) {
		this.chart.destroy();
		this.chart = null;
	}

	var canvas = this.canvas;
	if (canvas == null) {

		canvas = this.canvas = window.document.createElement("canvas");
		this.container.innerHTML = "";
		this.container.appendChild(canvas);

		// autoresize it to fill the widget container.
		this.addListener("resize", function (e) {
			var size = e.getData();
			canvas.style.width = size.width;
			canvas.style.height = size.height;
		});
	}

	// fire "render" when the chart has done rendering, when in design mode.
	if (wisej.web.DesignMode) {
		config.options.animation = {
			duration: 0,
			onComplete: function () {
				me.fireEvent("render");
			}
		};
	}
	
	// create and save the chart object.
	var ctx = canvas.getContext("2d");
	this.chart = this.widget = new Chart(ctx, config);

	// Runtime Mode Only:
	// Attach the click event to fire our managed event.
	if (!wisej.web.DesignMode) {

		canvas.onclick = function (e) {

			// returns the list of elements under the click point.
			var elements = me.chart.getActiveElements();
			if (elements.length > 0) {
				// package the data and send it back to our managed event handler.
				var result = {};
				var data = [];
				for (var i = 0; i < elements.length; i++) {
					data.push({
						pointIndex: elements[i].index,
						dataSetIndex: elements[i].datasetIndex
					});
				}
				result.data = data;

				me.fireWidgetEvent("chartClick", result);
			}
		};
	}
}

/**
 * Replace the default implementation not to fire "render"
 * here, but after the ChartJS widget has been fully rendered.
 */
this._onInitialized = function () {

	// do nothing here.
}

/**
 * Clean up when the widget is destroyed.
 */
this._onDestroyed = function () {

	if (this.chart) {
		this.chart.destroy();
		this.chart = null;
	}
}

/**
 * Returns the chart as base64 png image.
 */
this.getImage = function () {

	if (this.canvas == null)
		return null;

	return this.canvas.toDataURL();
}

/**
 * Updates the data in the chart performing a smooth
 * animated transition from one data set to the new one.
 *
 * NOTE: Careful when adding functions to "this". It's the
 * main wisej.web.Widget component and you may override system functions!
 *
 * @param datasets {Map} The new data set.
 * @param labels (array) the labels for the dataset
 * @param duration {Integer} The duration of the update animation in milliseconds.
 */
this.updateData = function (datasets, labels, duration) {

	if (!datasets)
		return;

	if (this.chart) {

		// update only the data of each data set.
		for (var i = 0; i < datasets.length; i++)
			this.chart.data.datasets[i].data = datasets[i].data;

		if (labels)
			this.chart.config.data.labels = labels;

		this.chart.update(duration);
	}
}

// registers the user functions with this widget class.
// if their names collide with existing functions they will
// override them. collisions are logged in the console.
this.__processWidgetFunctions = function (config) {

	var name, source;
	var functions = config.widgetFunctions;

	if (functions && functions.length > 0) {
		for (var i = 0; i < functions.length; i++) {
			name = functions[i].name;
			if (!name) continue;
			source = functions[i].source;
			if (!source) continue;

			var func = new Function("//# sourceURL=" + this.getName() + "." + name +
				"\r\n\r\n" + source);

			if (this[name] !== undefined)
				this.core.logWarning("Function " + name + " is being overridden.");

			this[name] = this._makeFunctionWrapper(name, func);
		}

		delete config.widgetFunctions;

		this._parseFunctions(config, functions);
	}
}

// creates a wrapper for the user function.
this._makeFunctionWrapper = function (name, func) {

	var me = this;
	function wrapper() {

		if (!me.widget) {
			var args = Array.prototype.slice.call(arguments);
			me.addListenerOnce('initialized', function (e) {
				func.apply(me, args);
			});
			return;
		}

		return func.apply(me, arguments);
	};

	return wrapper;
},

// ---------------------------------------------------
// Conversion of option field with functions
// i.e.: options.dataSource = "()=>createDataSource"
//		 options.dataSource = "(o)=>createDataSource"
//		 options.dataSource = "()=>createDataSource()"
//		 options.dataSource = "()=>createDataSource(1,'a')"
// ---------------------------------------------------

this._regexp = /^\((.*)\)=>([^\s\(\)]+)(\((.*)\))?$/;
this._parseFunctions = function (options, functions) {

	if (!functions || functions.length == 0)
		return;

	var match = null;
	for (var key in options) {

		var value = options[key];

		if (typeof (value) === "string") {

			// convert to function.
			match = this._regexp.exec(value);
			if (!match || match.length < 3)
				continue;

			var name = match[2];
			if (!name)
				continue;

			var func = functions.find(function (i) { return i.name == name; });
			if (!func)
				continue;

			if (!match[3]) {
				// assign a function to the field i.e. "()=>func" or "(p1,p2)=>func"
				var args = match[1] ? match[1].split(",") : [];
				args.push(func.source);
				options[key] = Function.constructor.apply(null, args).bind(this);
			}
			else {
				// invoke the function and assign the returned value to the field i.e. "()=>func()" or "()=>func(1,2)"
				var args = match[4] ? match[4].split(",") : null;
				var invoke = new Function(func.source);
				options[key] = invoke.apply(this, args);
			}

		} else if (typeof (value) === "object") {

			this._parseFunctions(value, functions);
		}
	}
}

/**
 * Applies a transformation to the datasets.
 * @param {any} datasets
 */
this.__processDataSets = function (datasets) {

	datasets.forEach(dataset => {
		if (dataset.stepped == "false")
			dataset.stepped = false;
	});
},

/**
 * Moves the scales to the correct location.
 * @param {any} options
 */
this.__processScales = function (options) {

	if (options == null)
		return;

	var xAxes = options.scales.xAxes;
	var yAxes = options.scales.yAxes;

	var scales = {};
	scales["x"] = xAxes[0];
	for (var i = 1; i < xAxes.length; i++) {
		scales["x" + i.toString()] = xAxes[i];
	}

	scales["y"] = yAxes[0];
	for (var i = 1; i < yAxes.length; i++) {
		scales["y" + i.toString()] = yAxes[i];
	}

	options.scales = scales;
}

/**
 * Fixes plugin names and adds system methods.
 * @param {any} options
 */
this.__processPlugins = function (config) {

	var plugins = config.options.plugins;
	if (plugins.dataLabels) {
		plugins.dataLabels.useEmbeddedFont = true;
		plugins.datalabels = plugins.dataLabels;
		delete plugins.dataLabels;
	}

	var datalabels = plugins.datalabels;
	if (!datalabels.formatter) {
		datalabels.formatter = this.__formatDataLabels;
	}
}

/**
 * Uses the formatted labels instead of the data labels.
 */
this.__formatDataLabels = function (value, context) {

	var dataset = context.dataset;
	var dataindex = context.dataIndex;
	if (dataset.formatted && dataset.formatted.length) {
		return dataset.formatted[dataindex];
	}

	return Math.round(value);
}

/**
 * Resolves themed fonts and colors recursively.
 */
this.__setFontAndColors = function (options) {

	if (options == null)
		return;

	var fontMgr = qx.theme.manager.Font.getInstance();
	var colorMgr = qx.theme.manager.Color.getInstance();

	for (var name in options) {

		// it's a font, resolve it.
		if (name == "font") {
			var font = fontMgr.resolve(options.font);
			if (font) {
				options.font = {
					size : font.getSize(),
					family : font.getFamily().join(","),
					style : font.isBold() ? "bold" : "normal"
				};
			}
			continue;
		}

		// it's a color, resolve it.
		if (name == "color" || name == "fontColor") {
			options[name] = colorMgr.resolve(options[name]);
			continue;
		}

		// it's an array, go through all elements.
		var array = options[name];
		if (array instanceof Array && array.length > 0) {
			for (var i = 0; i < array.length; i++)
				this.__setFontAndColors(array[i]);

			continue;
		}

		// re-enter for child objects.
		var object = options[name];
		if (object instanceof Object) {

			this.__setFontAndColors(object);
		}
	}
}

/**
 * Converts color arrays with only 1 element to a single variable.
 */
this.__normalizeColorArrays = function (datasets) {

	if (datasets == null || datasets.length == 0)
		return;

	for (var i = 0; i < datasets.length; i++) {

		var ds = datasets[i];

		for (var name in ds) {

			if (qx.lang.String.endsWith(name, "Color")) {
				var colors = ds[name];

				if (colors && colors instanceof Array && colors.length == 1)
					ds[name] = colors[0];
			}
		}
	}
}


/**
 * Various fix ups for line datasets
 */
this.__normalizeLineChartDataSetArrays = function (datasets) {

	if (datasets == null || datasets.length == 0)
		return;

	for (var i = 0; i < datasets.length; i++) {

		if (datasets[i].type == 'line') {

			var ds = datasets[i];

			if (ds.pointStyle.length == 1)
				ds.pointStyle = ds.pointStyle[0];

			if (ds.pointRadius.length == 1)
				ds.pointRadius = ds.pointRadius[0];

			if (ds.pointHoverRadius.length == 1)
				ds.pointHoverRadius = ds.pointHoverRadius[0];

			if (ds.steppedLine == 'false')
				ds.steppedLine = false;
		}
	}
}