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
 * wisej.web.ext.ApexChartsWidget
 *
 * Wraps all the basic functionality of ApexCharts.
 */
qx.Class.define("wisej.web.ext.ApexChartsWidget", {

	extend: wisej.web.Widget,

	construct: function () {

		this.base(arguments);

		this.addListener("focus", this._onFocus);
	},

	properties: {

		/**
		 * Additional HTML to add to the inner container that may be needed by the widget.
		 */
		widgetHtml: { init: null, check: "String" },

		/**
		 * Collection of event handlers to attach to the widget's events.
		 */
		widgetEvents: { init: [], check: "Array" },

		/**
		 * Collection of additional javascript methods to add to the widget.
		 */
		widgetFunctions: { init: [], check: "Array", apply: "_applyWidgetFunctions" },

		/**
		 * Collection of template scripts to register with the browser.
		 */
		widgetTemplates: { init: [], check: "Array", apply: "_applyWidgetTemplates" }
	},

	members: {

		/**
		 * Initializes the widget.
		 *
		 * This function is called when the InitScript property of
		 * wisej.web.Widget changes.
		 *
		 * 'this' refers to the container which is a wisej.web.Widget instance.
		 *
		 * 'this.container' refers to the DOM element and can be used to initialize
		 * the third party javascript widget.
		 *
		 * @param options {Map} Options map (optional).
		 */
		init: function (options) {

			// show a default chart if the user hasn't specified options.
			if (Object.keys(options).length == 0) {
				options = this.getDefaultChartConfig();
			}

			// ensure the chart fits our container.
			if (!options.chart) {
				options.chart = {};
			}

			// disable animations at design-time.
			if (wisej.web.DesignMode) {
				if (!options.chart.animations) {
					options.chart.animations = {};
				}
				options.chart.animations.enabled = false;
			}

			options.chart.height = "100%";
			options.chart.width = "100%";

			var html = this.getWidgetHtml();
			if (html) {
				this.container.innerHTML = html;
				this.container = this.container.firstChild;
			}

			if (this.filterOptions)
				this.filterOptions(options);

			this._registerEventHandlers(options);
			this._parseFunctions(options, this.getWidgetFunctions());

			this.widget = new ApexCharts(this.container, options);
			this.widget.render();

			this.getContentElement().setStyle("overflow", "visible");

			if (wisej.web.DesignMode) {
				var el = document.querySelector("div[style*='fixed']");
				if (el)
					el.remove();
			}
		},

		getDefaultChartConfig: function () {
			return {
				chart: {
					type: "line"
				},
				series: []
			};
		},

		/**
		 * Updates the widget when the options object changes.
		 *
		 * @param options {Map} Options map (optional).
		 * @param old {Map} Previous options map (optional).
		 */
		update: function (options, old) {

			if (this.filterOptions)
				this.filterOptions(options, old);

			this._parseFunctions(options, this.getWidgetFunctions());

			this.widget.updateOptions(options, true);
		},

		/**
		 * Destroys and recreates the widget with the given configuration.
		 * @param {any} options
		 */
		recreateWidget: function (options) {

			if (this.widget) {
				this.widget.destroy();
				var widget = this.getContentElement().getDomElement();

				widget.innerHTML = `<div 
					id='${this.getId()}_container' 
					style='width:${this.getWidth()}px;height:${this.getHeight()}px' />`;

				this.container = widget.firstChild;

				this.init(options);

				if (this.initWidget)
					this.initWidget();
			}
		},

		// registers the user functions with this widget class.
		// if their names collide with existing functions they will
		// override them. collisions are logged in the console.
		_registerFunctions: function (functions) {

			var name, source;

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
			}
		},

		// creates a wrapper for the user function.
		_makeFunctionWrapper: function (name, func) {

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

		_regexp: /^\((.*)\)=>([^\s\(\)]+)(\((.*)\))?$/,
		_parseFunctions: function (options, functions) {

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
		},

		// ---------------------------------------------------
		// Dynamic event system implementation.
		// ---------------------------------------------------

		/**
		 * Registers the handler function to listen to the event name on the wrapped widget.
		 * 
		 * @param name {String} name of the event.
		 * @param handler {Function} event handler.
		 */
		_addListener: function (name, handler) {
			this.widget.addEventListener(name, handler);
		},

		/**
		 * Unregisters the handler functions from the event name on the wrapped widget.
		 * 
		 * @param name {String} name of the event.
		 * @param handler {Function} event handler.
		 */
		_removeListener: function (name, handler) {
			this.widget.removeEventListener(name, handler);
		},

		/**
		 * Collects the event date from the arguments received by the widget.
		 * 
		 * @param type {String} name of the event.
		 * @param e {Object} event data from the widget.
		 * @param expression {String?} optional string expression that returns the event data from e.
		 */
		_getEventData: function (type, e, expression) {

			if (expression)
				return this.base(arguments, type, e, expression);

			// otherwise try to build a usable data object for the event data.
			// this._getEventData can also be overwritten in a specific widget js file.

			// add primitives at the root level and in "target".
			var data = this.__copyEventData(e, 0);
			return data;
		},

		__copyEventData: function (source, level) {

			level++;
			if (level >= 3)
				return null;

			var data = {};
			for (var key in source) {

				var value = source[key];
				if (value == null)
					continue;

				switch (typeof (value)) {
					case "string":
					case "number":
					case "boolean":
						data[key] = value;
						break;
					case "object":

						if (value instanceof Date) {
							data[key] = value;
						}
						else if (value == this || value == this.widget) {
							continue;
						}
						else if (value instanceof HTMLElement) {
							continue;
						}
						else if (value instanceof Event) {
							continue;
						}
						else if (value instanceof Array) {
							for (index in value) {
								var itemData = this.__copyEventData(value[index], level);
								if (!data[key])
									data[key] = [];

								data[key].push(itemData);
							}
						}
						else {
							try {

								var childData = this.__copyEventData(value, level);
								if (childData)
									data[key] = childData;
							} catch (ex) {
								this.core.logError(ex);
							}
						}
						break;
				}
			}

			return (Object.keys(data).length == 0) ? null : data;
		},

		// ---------------------------------------------------
		// END: Dynamic event system implementation.
		// ---------------------------------------------------

		// attaches the specified event callback functions
		// to the options map.
		_registerEventHandlers: function (options) {

			var handlers = this.getWidgetEvents();
			if (handlers && handlers.length > 0) {
				for (var i = 0; i < handlers.length; i++) {

					var name = handlers[i].name;
					if (!name)
						continue;

					var source = handlers[i].source;
					if (!source)
						continue;

					var eventName = name.split('(')[0].trim();

					var handler = new Function("e",
						"//# sourceURL=" + this.getName() + "." + eventName +
						"\r\n\r\n" + source);

					options[eventName] = this._makeWidgetEventHandler(name, handler);
				}
			}
		},

		// creates a closure to execute the javascript handler for the widget's event
		// and fire the corresponding "widgetEvent" back to the server carrying the specified data.
		_makeWidgetEventHandler: function (name, handler) {

			var me = this;

			// e is the event data coming from the widget.
			function callback(e) {

				// we add the "container" field referencing the wisej widget.
				e.container = me;

				// we add the "type" field with the event name in case the widget didn't.
				e.type = name;

				// call the client side handler code, if specified.
				// the context (this) is the third party widget, NOT the wisej widget.
				handler.apply(this, arguments);
			};

			return callback;
		},

		/**
		 * Initializes a function specified in the widgetFunctions array
		 * without waiting for the widget to be initialized.
		 */
		initFunction: function (name) {

			if (typeof (name) === "function")
				return name;

			if (typeof (name) === "string") {

				var functions = this.getWidgetFunctions();
				for (var i = 0; i < functions.length; i++) {
					if (functions[i].name == name) {
						return this._makeFunctionWrapper(name, new Function(functions[i].source))
					}
				}
			}
		},

		initStyle: function (name) {

			if (typeof (name) === "string") {

				return $("#" + name);
			}
		},

		/**
		 * Applies the WidgetFunctions property.
		 *
		 * Creates the user function on this widget.
		 */
		_applyWidgetFunctions: function (value, old) {

			this._registerFunctions(value);
		},

		/**
		 * Applies the WidgetTemplates property.
		 *
		 * Registers the custom templates functions received from the app.
		 */
		_applyWidgetTemplates: function (value, old) {

			var templates = value;
			if (!templates || templates.length == 0)
				return;

			var tpl, t;
			for (var i = 0; i < templates.length; i++) {

				t = templates[i];
				if (!t.id || !t.template)
					continue;

				if (!document.getElementById(t.id)) {
					// build the template <script id="id" type="text/x-jsrender">template</script>.
					tpl = document.createElement("script");
					tpl.id = t.id;
					tpl.innerHTML = t.template;
					tpl.type = t.type || "text/x-jsrender";
					document.head.appendChild(tpl);
				}
			}

		},

		// fires the "render" event when in design mode to notify
		// the HTML renderer that the widget has been initialized.
		//
		// widgets may replace this function to fire the "render" event
		// according to the widget's implementation.
		//
		_onInitialized: function () {

			this.base(arguments);

			if (this.initWidget)
				this.initWidget();
		},

		// handles the "focus" event to try and set the
		// focus to the inner widget.
		_onFocus: function (e) {

			try {
				if (!this.widget) {
				} else {
					if (this.widget.container)
						this.widget.container.focus();
				}
			} catch (ex) { }
		},
	},

	destruct: function () {

		if (this.widget) {
			try {
				this.widget.destroy();
			} catch (ex) { }
			this.widget = null;
			this.container.innerHTML = "";
		}
	}
});

