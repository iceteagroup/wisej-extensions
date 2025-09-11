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
 * wisej.web.ext.PolymerWidget
 *
 * Represents a Polymer widget. This class is
 * able to render any Polymer component, read/write
 * the polymer's custom properties and route the
 * polymer's events back to the server.
 *
 * The polymer element is created adjacent (not inside)
 * the wisej widget element in order to preserve the hierarchy between
 * polymer elements. Wisej widgets basically act like decorators.
 *
 * 
 * See: https://www.polymer-project.org
 */
qx.Class.define("wisej.web.ext.PolymerWidget", {

	extend: wisej.web.Control,

	construct: function () {

		this.base(arguments);

		this.addListenerOnce("appear", this.__onAppear, this);
	},

	properties: {

		/**
		 * The custom tag name for the polymer element.
		 */
		elementType: { init: null, check: "String" },

		/**
		 * The inner HTML content to render inside the polymer widget.
		 */
		content: { init: null, check: "String", apply: "_applyContent" },

		/**
		 * The CSS class name for the inner polymer element.
		 */
		elementClassName: { init: null, check: "String", apply: "_applyElementClassName" },

		/**
		 * Collection of properties to set on the polymer widget and to
		 * to send back to the server when an event occurs.
		 */
		properties: { init: null, check: "Map", apply: "_applyProperties" },

		/**
		 * List of events to wire back to the server.
		 */
		events: { init: null, check: "Array", apply: "_applyEvents" },
	},

	members: {

		// the polymer element.
		polymer: null,

		// pre-bound event handler.
		__polymerEventHandler: null,

		// default class names for the polymer.
		__polymerDefaultClassName: null,

		/**
		 * Applies the ElementClassName property on the Polymer component.
		 */
		_applyElementClassName: function (value, old) {

			// if the dom is not created yet, do nothing now
			// the property will get assigned in __createPolymer after the dom has been created.
			if (!this.polymer)
				return;

			if (value)
				this.polymer.className = this.__polymerDefaultClassName + " " + value;
			else
				this.polymer.className = this.__polymerDefaultClassName;
		},

		/**
		 * Applies the content property.
		 *
		 */
		_applyContent: function (value, old) {

			// if the dom is not created yet, do nothing now
			if (!this.polymer)
				return;

			if (value != null)
				Polymer.dom(this.polymer).innerHTML = content;
		},

		/**
		 * Applies the events property.
		 *
		 * Subscribes to the listed events. When an event occurs, the "widgetEvent" is fired
		 * on the server side passing the properties specified in "properties" as the data of the event.
		 */
		_applyEvents: function (value, old) {

			// if the dom is not created yet, do nothing now.
			if (!this.polymer)
				return;

			if (!this.__polymerEventHandler)
				this.__polymerEventHandler = this._onPolymerEvent.bind(this);

			// remove the previous events.
			if (old != null) {
				for (var i = 0; i < old.length; i++)
					this.polymer.removeEventListener(old[i], this.__polymerEventHandler);
			}

			// add the new events.
			if (value != null) {
				for (var i = 0; i < value.length; i++)
					this.polymer.addEventListener(value[i], this.__polymerEventHandler);
			}
		},

		// event handler for all polymer events.
		// packs the requested properties and fires our "widgetEvent".
		_onPolymerEvent: function (e) {

			var data = {};

			var properties = this.getProperties();
			if (properties != null) {
				for (var name in properties) {
					try {

						// send back only the properties that have changed.
						var oldValue = properties[name];
						var newValue = this.polymer[name];
						if (oldValue != newValue)
							data[name] = newValue;
					}
					catch (e) {

						if (!wisej.web.DesignMode)
							this.core.logError(e);
					}
				}
			}

			this.fireDataEvent("polymerEvent", { type: e.type, data: data });
		},

		/**
		 * Applies the properties on the Polymer component.
		 */
		_applyProperties: function (value, old) {

			// if the dom is not created yet, do nothing.
			// the properties will get assigned in __createPolymer after the dom has been created.
			if (!this.polymer)
				return;

			if (value) {

				for (var name in value) {
					try {
						if (this.polymer[name] != value[name])
							this.polymer[name] = value[name];
					}
					catch (e) {

						if (!wisej.web.DesignMode)
							this.core.logError(e);
					}
				}
			}
		},

		/**
		 * Creates the content element. The style properties
		 * position and zIndex are modified from the Widget
		 * core.
		 *
		 * This function may be overridden to customize a class
		 * content.
		 *
		 * @return {qx.html.Element} The widget's content element
		 */
		_createContentElement: function () {

			// we want the container wisej element to show
			// the overflow to let the polymer effects render outside of its boundaries.
			return new qx.html.Element("div", {
				overflowX: "visible",
				overflowY: "visible"
			});
		},

		/**
		 * Renders the third party widget into this element
		 * after the dom has been created.
		 */
		__onAppear: function () {

			if (wisej.web.DesignMode)
				return;

			this.__loadWebComponents(function () {
				this.__renderWidget();
			});

		},

		/**
		 * Creates the Polymer component inside the wisej widget.
		 */
		__createPolymer: function () {

			var elementType = this.getElementType();
			if (elementType) {

				var el = this.getContentElement();
				var dom = el.getDomElement();
				if (dom) {

					if (!wisej.web.DesignMode)
						this.core.logInfo("Creating element: ", elementType);

					// create the polymer element.
					this.polymer = document.createElement(elementType);
					this.polymer.style.width = "100%";
					this.polymer.style.height = "100%";
					this.polymer.style.margin = "0px";
					this.polymer.style.boxSizing = "border-box";

					dom.appendChild(this.polymer);

					// save the default class name of the polymer.
					this.__polymerDefaultClassName = this.polymer.className;

					// add the content text.
					var content = this.getContent();
					if (content)
						Polymer.dom(this.polymer).innerHTML = content;

					// apply the properties.
					this._applyEvents(this.getEvents());
					this._applyProperties(this.getProperties());
					this._applyElementClassName(this.getElementClassName());
				}
			}

			if (wisej.web.DesignMode)
				this.fireEvent("render");
		},

		/**
		 * Overridden.
		 *
		 * Renders the third party widget into this element.
		 */
		__renderWidget: function () {

			try {
				// if the element type is already registered, create it now.
				if (this.__isRegistered(this.getElementType())) {

					this.__createPolymer();
					return;
				}

				// is the Polymer library loaded?
				if (!window.Polymer) {

					wisej.web.ext.PolymerComponent.loadPolymer(
						"polymer/polymer.html",
						// success
						function (url) { this.__renderWidget(); },
						// error
						function (e, url) { this.__onError("Error loading Polymer from: " + url); },
						// context
						this
					);

					return;
				}

				// load the request polymer element type.
				var elementType = this.getElementType();
				wisej.web.ext.PolymerComponent.loadPolymer(
					elementType + "/" + elementType + ".html",
					// success
					function (url) { this.__createPolymer(); },
					// error
					function (e, url) { this.__onError("Error loading Polymer from: " + url); },
					// context
					this
				);
			}
			catch (ex) {

				this.__onError(ex.message);
			}
		},

		/**
		 * Renders the error message within the widget container
		 * and fires the "render" event to let the designer display
		 * this widget and move on.
		 */
		__onError: function (message) {

			var el = this.getContentElement();
			if (el.getDomElement())
				el.getDomElement().innerText = message;

			if (wisej.web.DesignMode)
				this.fireEvent("render");
		},

		/**
		 * Checks whether a polymer element is already registered.
		 */
		__isRegistered: function (tagName) {

			if (window.Polyer === undefined)
				return false;

			if (Polyer.telemetry === undefined)
				return false;

			var regs = Polymer.telemetry.registrations;
			for (var i = 0; i < regs.length; i++) {
				var p = regs[i];
				if (p && p.is == tagName)
					return true;
			}

			return false;
		},

		/**
		 * Loads the WebComponents library.
		 */
		__loadWebComponents: function (callback) {

			var me = this;
			wisej.utils.Loader.load(
				[{
					id: "webcomponents.js",
					url: wisej.web.ext.PolymerComponent.getPolymerUrl("webcomponentsjs/webcomponents-lite.js")
				}],
				function () { callback.call(me) });
		},

		// overridden to prevent the "render" event.
		// it will be fired when the polymer has been rendered.
		_onDesignRender: function () {

			this.__loadWebComponents(function () {
				this.__renderWidget();
			});
		},
	}

});