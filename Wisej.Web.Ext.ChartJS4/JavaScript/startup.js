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

	if (config.options?.plugins && config.options?.plugins?.datalabels) {

		Chart.register(ChartDataLabels);
	}
	// Set responsive behavior
	if (!config.options) {
		config.options = {};
	}
	config.options.responsive = true;
	config.options.maintainAspectRatio = false;

	// Convert all function strings in the entire config before creating the chart
	this.__convertFunctionStrings(config, config.widgetFunctions);

	// Process plugins
	this.__processPlugins(config);

	// Process widget functions (scriptable options callbacks)
	this.__processWidgetFunctions(config);

	// Convert fonts and colors from Wisej maps
	this.__setFontAndColors(config.options);
	if (config.data && config.data.datasets) {
		this.__setFontAndColors(config.data.datasets);
	}

	// Process animation function strings
	if (config.options.animation) {
		this.__processAnimation(config.options.animation);
	}
	if (config.options.animations) {
		this.__processAnimation(config.options.animations);
	}

	// Normalize color arrays
	this.__normalizeColorArrays(config.data.datasets);

	// Replace "__NaN__" sentinels with actual NaN values for Chart.js skip detection
	this.__processNaNValues(config.data.datasets);

	// destroy the previous Chart object if type changed, otherwise update in place
	// NOTE: if the server only changed the data set
	// it should call "UpdateDataSet" instead of "Update".
	if (this.chart != null) {
		if (this.chart.config.type === config.type) {
			this.chart.options = config.options;

			var datasets = config.data.datasets;
			var currentDatasets = this.chart.data.datasets;

			for (var i = 0; i < datasets.length; i++) {
				if (currentDatasets[i]) {
					// Use Object.keys to remove deleted properties on update
					for (var key in currentDatasets[i]) {
						if (!datasets[i].hasOwnProperty(key) && key !== '_meta' && key !== 'data') {
							delete currentDatasets[i][key];
						}
					}
					var newData = datasets[i].data;
					Object.assign(currentDatasets[i], datasets[i]);
					currentDatasets[i].data = newData;
				} else {
					currentDatasets.push(datasets[i]);
				}
			}

			if (currentDatasets.length > datasets.length) {
				currentDatasets.splice(datasets.length, currentDatasets.length - datasets.length);
			}

			this.chart.data.labels = config.data.labels;
			this.chart.update();
			return;
		}

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

	this.__processElements(config.options.elements);

	this.chart = new Chart(ctx, config);

	this.chart.update();
	this.widget = this.chart;

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

		// Convert all function strings
		this.__convertFunctionStrings(datasets, this.__widgetFunctionsRaw);

		// normalize color arrays and properties
		this.__normalizeColorArrays(datasets);
		this.__processNaNValues(datasets);
		if (this.__widgetFunctionsRaw) {
			this._parseFunctions(datasets, this.__widgetFunctionsRaw);
		}
		this.__setFontAndColors(datasets);

		// Update existing datasets to allow smooth animations
		var currentDatasets = this.chart.data.datasets;
		for (var i = 0; i < datasets.length; i++) {
			if (currentDatasets[i]) {
				// Update properties while keeping the object reference
				for (var key in currentDatasets[i]) {
					if (!datasets[i].hasOwnProperty(key) && key !== '_meta' && key !== 'data') {
						delete currentDatasets[i][key];
					}
				}
				var newData = datasets[i].data;
				Object.assign(currentDatasets[i], datasets[i]);
				currentDatasets[i].data = newData;
			} else {
				currentDatasets.push(datasets[i]);
			}
		}

		// Remove any extra datasets that were deleted
		if (currentDatasets.length > datasets.length) {
			currentDatasets.splice(datasets.length, currentDatasets.length - datasets.length);
		}

		if (labels)
			this.chart.data.labels = labels;

		this.chart.update(duration);
	}
}

/**
 * Chart.js API Methods
 */

/**
 * Triggers an update of the chart.
 * @param {String} mode - Optional update mode
 */
this.updateChart = function (mode) {
	if (this.chart) {
		if (mode) {
			this.chart.update(mode);
		} else {
			this.chart.update();
		}
	}
}

/**
 * Destroys the chart instance.
 */
this.destroy = function () {
	if (this.chart) {
		this.chart.destroy();
		this.chart = null;
	}
}

/**
 * Resets the chart to its initial state.
 */
this.reset = function () {
	if (this.chart) {
		this.chart.reset();
	}
}

/**
 * Triggers a redraw of the chart.
 */
this.render = function () {
	if (this.chart) {
		this.chart.render();
	}
}

/**
 * Stops all animations.
 */
this.stop = function () {
	if (this.chart) {
		this.chart.stop();
	}
}

/**
 * Resizes the chart canvas.
 * @param {Number} width - Optional width
 * @param {Number} height - Optional height
 */
this.resize = function (width, height) {
	if (this.chart) {
		if (width && height) {
			this.chart.resize(width, height);
		} else {
			this.chart.resize();
		}
	}
}

/**
 * Clears the chart canvas.
 */
this.clear = function () {
	if (this.chart) {
		this.chart.clear();
	}
}

/**
 * Returns a base64 encoded image of the chart.
 * @param {String} type - Image type (e.g., 'image/png')
 * @param {Number} quality - Quality for lossy formats (0.0 to 1.0)
 * @returns {String} Base64 encoded image
 */
this.toBase64Image = function (type, quality) {
	if (this.chart) {
		return this.chart.toBase64Image(type, quality);
	}
	return null;
}

/**
 * Generates an HTML legend.
 * @returns {String} HTML legend string
 */
this.generateLegend = function () {
	if (this.chart) {
		return this.chart.generateLegend();
	}
	return '';
}

/**
 * Gets the number of visible datasets.
 * @returns {Number} Count of visible datasets
 */
this.getVisibleDatasetCount = function () {
	if (this.chart) {
		return this.chart.getVisibleDatasetCount();
	}
	return 0;
}

/**
 * Checks if a dataset is visible.
 * @param {Number} datasetIndex - Index of the dataset
 * @returns {Boolean} True if visible
 */
this.isDatasetVisible = function (datasetIndex) {
	if (this.chart) {
		return this.chart.isDatasetVisible(datasetIndex);
	}
	return false;
}

/**
 * Sets the visibility of a dataset.
 * @param {Number} datasetIndex - Index of the dataset
 * @param {Boolean} visible - True to show, false to hide
 */
this.setDatasetVisibility = function (datasetIndex, visible) {
	if (this.chart) {
		this.chart.setDatasetVisibility(datasetIndex, visible);
	}
}

/**
 * Toggles the visibility of data at the specified index.
 * @param {Number} index - Index of the data
 */
this.toggleDataVisibility = function (index) {
	if (this.chart) {
		this.chart.toggleDataVisibility(index);
	}
}

/**
 * Gets the visibility state of data at the specified index.
 * @param {Number} index - Index of the data
 * @returns {Boolean} True if visible
 */
this.getDataVisibility = function (index) {
	if (this.chart) {
		return this.chart.getDataVisibility(index);
	}
	return false;
}

/**
 * Hides a dataset or data element.
 * @param {Number} datasetIndex - Index of the dataset
 * @param {Number} dataIndex - Optional index of the data element
 */
this.hide = function (datasetIndex, dataIndex) {
	if (this.chart) {
		if (dataIndex !== undefined) {
			this.chart.hide(datasetIndex, dataIndex);
		} else {
			this.chart.hide(datasetIndex);
		}
	}
}

/**
 * Shows a dataset or data element.
 * @param {Number} datasetIndex - Index of the dataset
 * @param {Number} dataIndex - Optional index of the data element
 */
this.show = function (datasetIndex, dataIndex) {
	if (this.chart) {
		if (dataIndex !== undefined) {
			this.chart.show(datasetIndex, dataIndex);
		} else {
			this.chart.show(datasetIndex);
		}
	}
}

/**
 * Sets the active (hovered) elements.
 * @param {Array} activeElements - Array of active element specifications
 */
this.setActiveElements = function (activeElements) {
	if (this.chart) {
		if (activeElements && activeElements.length > 0) {
			this.chart.setActiveElements(activeElements);
		} else {
			this.chart.setActiveElements([]);
		}
	}
}

	/**
	 * Gets the currently active (hovered) elements.
	 * @returns {Array} Array of active elements
	 */
	this.getActiveElements = function () {
		if (this.chart) {
			return this.chart.getActiveElements();
		}
		return [];
	}

	/**
	 * Registers user functions with this widget instance.
	 * Functions are referenced in chart options using the "(ctx)=>functionName" pattern.
	 */
	this.__processWidgetFunctions = function (config) {

		var name, source;
		var functions = config.widgetFunctions;

		if (functions && functions.length > 0) {
			this.__widgetFunctionsRaw = functions;
			for (var i = 0; i < functions.length; i++) {
				name = functions[i].name || functions[i].Name;
				if (!name) continue;
				source = functions[i].source || functions[i].Source;
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

	/**
	 * Creates a wrapper around a user function.
	 */
	this._makeFunctionWrapper = function (name, func) {

		var me = this;
		function wrapper() {
			// Chart.js requires scriptable options to execute synchronously and return a value.
			// previously this was queued waiting for initialization, which caused the initial render to miss styles.
			// If Chart.js executes this callback with a bound context (like a scale), we preserve it for `function` scopes.
			var ctx = (this == null || this === window) ? me : this;
			return func.apply(ctx, arguments);
		};

		return wrapper;
	}

	this._regexp = /^\((.*)\)(?:=>|\\u003[eE])([^\s\(\)]+)(\((.*)\))?$/;

	/**
	 * Recursively converts "(ctx)=>functionName" strings in the options object to actual function references.
	 */
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

				var func = functions.find(function (i) { return i.name == name || i.Name == name; });
				if (!func)
					continue;

				if (!match[3]) {
					// assign a function to the field i.e. "()=>func" or "(p1,p2)=>func"
					var args = match[1] ? match[1].split(",") : [];
					args.push(func.source || func.Source);
					options[key] = Function.constructor.apply(null, args).bind(this);
				}
				else {
					// invoke the function and assign the returned value to the field i.e. "()=>func()" or "()=>func(1,2)"
					var args = match[4] ? match[4].split(",") : null;
					var invoke = new Function(func.source || func.Source);
					options[key] = invoke.apply(this, args);
				}

			} else if (typeof (value) === "object") {

				this._parseFunctions(value, functions);
			}
		}
	}

	/**
	 * Configures plugins for the chart.
	 */
	this.__processPlugins = function (config) {

		if (!config.options.plugins) {
			config.options.plugins = {};
		}

		var plugins = config.options.plugins;

		// Configure datalabels plugin if present
		if (plugins.datalabels && (plugins.datalabels.display === true || plugins.datalabels.display === undefined)) {

			plugins.datalabels.useEmbeddedFont = true;
			if (!plugins.datalabels.formatter) {
				plugins.datalabels.formatter = this.__formatDataLabels;
			}
		}

		var hookNames = {
			beforeInit: true,
			afterInit: true,
			beforeUpdate: true,
			afterUpdate: true,
			beforeLayout: true,
			afterLayout: true,
			beforeDatasetsUpdate: true,
			afterDatasetsUpdate: true,
			beforeDatasetUpdate: true,
			afterDatasetUpdate: true,
			beforeRender: true,
			afterRender: true,
			beforeDraw: true,
			afterDraw: true,
			beforeDatasetsDraw: true,
			afterDatasetsDraw: true,
			beforeDatasetDraw: true,
			afterDatasetDraw: true,
			beforeEvent: true,
			afterEvent: true,
			resize: true,
			beforeTooltipDraw: true,
			afterTooltipDraw: true,
			destroy: true
		};

		for (var pluginId in plugins) {
			if (!Object.prototype.hasOwnProperty.call(plugins, pluginId))
				continue;

			var pluginOptions = plugins[pluginId];
			if (!pluginOptions || typeof pluginOptions !== "object" || Array.isArray(pluginOptions))
				continue;

			var pluginDefinition = null;
			for (var key in pluginOptions) {
				if (!Object.prototype.hasOwnProperty.call(pluginOptions, key))
					continue;

				if (!hookNames[key] || typeof pluginOptions[key] !== "function")
					continue;

				if (!pluginDefinition) {
					pluginDefinition = {
						id: pluginOptions.id || pluginId
					};
				}

				pluginDefinition[key] = pluginOptions[key];
			}

			if (pluginDefinition) {
				if (!config.plugins)
					config.plugins = [];

				config.plugins.push(pluginDefinition);
			}
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

	this.__convertFunctionStrings = function (options, explicitFunctions, depth) {
		if (options == null || typeof options !== "object") return;

		if (depth === undefined) depth = 0;
		if (depth > 10) return;

		var functions = explicitFunctions || this.__widgetFunctionsRaw;

		for (var key in options) {
			var value = options[key];

			if (typeof value === "string") {
				// First check if it's the "(ctx)=>functionName" shortcut pattern for explicit WidgetFunctions
				var match = this._regexp ? this._regexp.exec(value) : null;
				var isExplicitWidgetFunction = false;

				if (match && match.length >= 3) {
					var name = match[2];
					if (functions && functions.some(function (i) { return i.name == name || i.Name == name; })) {
						isExplicitWidgetFunction = true;
					}
				}

				if (isExplicitWidgetFunction) {
					// Skip processing here, let _parseFunctions handle the named mapping
					continue;
				}

				// Otherwise, see if it is raw inline Javascript code
				if (this.__looksLikeFunction(value)) {
					// Register it as a true WidgetFunction
					options[key] = this.__registerInlineFunction(value);
				}
			} else if (Array.isArray(value) || (value && typeof value === "object")) {
				this.__convertFunctionStrings(value, functions, depth + 1);
			}
		}
	}

	this.__inlineFuncCounter = 0;
	this.__inlineFuncCache = {};

	/**
	 * Converts an inline function string into a formal WidgetFunction, 
	 * giving it a unique name and wrapping its execution.
	 */
	this.__registerInlineFunction = function (source) {
		// Trim to avoid whitespace hashing issues
		var trimmedSource = source.trim();

		// Cache check: If we've seen this exact function string before, reuse it
		if (this.__inlineFuncCache[trimmedSource]) {
			var existingName = this.__inlineFuncCache[trimmedSource];
			return this[existingName];
		}

		// Generate a unique name
		this.__inlineFuncCounter++;
		var name = "__inline_func_" + this.__inlineFuncCounter;

		try {
			var sourceToCompile = trimmedSource;

			// If it's a raw return or block without function signature, wrap it
			if (!trimmedSource.startsWith("function") && !trimmedSource.startsWith("(") && !trimmedSource.includes("=>")) {
				// This matches user inputs that are just the body of the function
				if (!trimmedSource.startsWith("{")) {
					sourceToCompile = "function(ctx) { return " + trimmedSource + "; }"
				} else {
					sourceToCompile = "function(ctx) " + trimmedSource;
				}
			}

			// We use a factory function to bind `this` lexically for arrow functions!
			// By compiling it via eval inside a function bound to `this`, arrow functions inherit the widget as `this`.
			// We pass 'widget' as a parameter so the inner function can access the widget reference explicitly if needed.
			var factory = new Function("sourceCode", "widget",
				"//# sourceURL=" + this.getName() + "._inline_" + name + "\r\n" +
				"return eval('(' + sourceCode + ')');"
			);
			var compiledFunc = factory.call(this, sourceToCompile, this);

			// Check if the widget is already overriding something
			if (this[name] !== undefined)
				this.core.logWarning("Function " + name + " is being overridden.");

			// Wrap it so it waits for initialization, just like explicit WidgetFunctions
			this[name] = this._makeFunctionWrapper(name, compiledFunc);

			// Save name to cache
			this.__inlineFuncCache[trimmedSource] = name;

			return this[name];
		} catch (e) {
			console.error("Failed to compile inline function string:\n" + source, e);
			return function () { };
		}
	}

	this.__looksLikeFunction = function (str) {
		if (typeof str !== 'string') return false;

		var trimmed = str.trim();
		return (
			trimmed.startsWith('function') ||
			trimmed.startsWith('(') ||
			trimmed.startsWith('ctx') ||
			trimmed.startsWith('context') ||
			/^[a-zA-Z_$][a-zA-Z0-9_$]*\s*=>/.test(trimmed) ||
			/^\([^)]*\)\s*=>/.test(trimmed)
		);
	}

	this.__stringToFunction = function (string) {
		return this.__registerInlineFunction(string);
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
						size: font.getSize(),
						family: font.getFamily().join(","),
						style: font.isBold() ? "bold" : "normal"
					};
				}
				continue;
			}

			// it's a color, resolve it.
			if (name == "color" || name == "fontColor") {
				options[name] = colorMgr.resolve(options[name]);
				continue;
			}

			if (typeof name === "string" && qx.lang.String.endsWith(name, "Color")) {
				// check if the color is a function.
				if (this.__looksLikeFunction(options[name])) {
					// convert the string to a function.
					options[name] = this.__stringToFunction(options[name]);
					continue;
				}
			}

			// it's an array, go through all elements.
			var array = options[name];
			if (array instanceof Array && array.length > 0) {
				for (var i = 0; i < array.length; i++) {
					if (typeof name === "string" && qx.lang.String.endsWith(name, "Color") && this.__looksLikeFunction(array[i])) {
						array[i] = this.__stringToFunction(array[i]);
					} else if (array[i] && typeof array[i] === "object") {
						this.__setFontAndColors(array[i]);
					}
				}
				continue;
			}

			// re-enter for child objects.
			var object = options[name];
			if (object && typeof object === "object") {

				this.__setFontAndColors(object);
			}
		}
	}

	this.__processElements = function (elements) {

		if (!elements)
			return;

		//elements is an object with the elements to process.
		for (var name in elements) {

			if (elements[name] == null)
				continue;

			var element = elements[name];
			if (element instanceof Object) {
				this.__processElements(elements[name]);
				continue;
			}

			if (this.__looksLikeFunction(elements[name])) {
				elements[name] = this.__stringToFunction(elements[name]);
			}

		}
	}

	this.__processAnimation = function (config) {
		if (config && typeof config === "object") {
			for (const key of Object.keys(config)) {
				const value = config[key];

				if (value && typeof value === "object") {
					this.__processAnimation(value); // recurse
				} else if (this.__looksLikeFunction(value)) {
					try {
						// safer than eval
						config[key] = new Function("return " + value)();
					} catch (e) {
						console.error("Invalid function in config:", value, e);
					}
				}
			}
		}
	};

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
	 * Replaces "__NaN__" sentinel strings in dataset data arrays with the JavaScript NaN value.
	 * This allows C# callers to use ChartValue.NaN which Chart.js treats as a skipped point
	 * (ctx.p0.skip / ctx.p1.skip === true), enabling per-segment styling via the segment option.
	 */
	this.__processNaNValues = function (datasets) {

		if (datasets == null || datasets.length == 0)
			return;

		for (var i = 0; i < datasets.length; i++) {
			var data = datasets[i].data;
			if (!data || !(data instanceof Array))
				continue;

			for (var j = 0; j < data.length; j++) {
				if (data[j] === "__NaN__")
					data[j] = NaN;
			}
		}
	}
