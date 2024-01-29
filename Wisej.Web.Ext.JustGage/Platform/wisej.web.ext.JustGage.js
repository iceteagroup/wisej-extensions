///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Gianluca Pivato
// Additions: Nic Adams
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
 * wisej.web.ext.JustGage
 */
qx.Class.define("wisej.web.ext.JustGage", {

	extend: wisej.web.Control,

	include: [
		wisej.mixin.MBorderStyle
	],

	properties: {

		/**
		 * Title property.
		 *
		 * The title above or below the gauge.
		 */
		title: { init: "", check: "String", apply: "_applyProperty" },

		/**
		 * Label property.
		 *
		 * The label below the gauge.
		 */
		label: { init: "", check: "String", apply: "_applyProperty" },

		/**
        * Symbol property.
        *
        * The symbol displayed with the value.
        */
		symbol: { init: "", check: "String", apply: "_applyProperty" },

		/**
		 * Value property.
		 */
		value: { init: 0, check: "Float", apply: "_applyValue" },

		/**
		 * Minimum property.
		 */
		minimum: { init: 0, check: "Integer", apply: "_applyProperty" },

		/**
		 * Maximum property.
		 */
		maximum: { init: 100, check: "Integer", apply: "_applyProperty" },

		/**
		 * ShowMinMaxLabels property.
		 *
		 * Shows or hides the Min/Max labels below the gage.
		 */
		showMinMax: { init: true, check: "Boolean", apply: "_applyProperty" },

		/**
        * ShowValue property.
        *
        * Shows or hides the value below the gage.
        */
		showValue: { init: true, check: "Boolean", apply: "_applyProperty" },

		/**
        * ShowPointer property.
        *
        * Shows or hides the pointer on the gauge.
        */
		showPointer: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Reverse gauge property.
        *
        * If true then the gauge goes from right to left
        */
		reverse: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Display as a donut rather than a gauge.
        *
        */
		showDonut: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Change colour using a gradient rather than sector colors
        *
        */
		showGradient: { init: true, check: "Boolean", apply: "_applyProperty" },

		/**
        * Display large numbers in a human friendly way for min/max and value
        * ie. 1232343 -> 1.2M
        */
		humanFriendly: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Number of decimal places to display
        */
		decimals: { init: 0, check: "Integer", apply: "_applyProperty" },

		/**
		* Gauge will fill starting from the center, rather than from the min value.
		*/
		differential: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Display large numbers with thousand separators (commas)
        */
		formatNumber: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Animate the value change.
        */
		counter: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
        * Animation property.
        *
        * How the gauge is animated when first displayed
        */
		startAnimationType: { init: ">", check: "String", apply: "_applyProperty" },

		/**
        * Animation property.
        *
        * How the gauge is animated when the value is changed
        */
		refreshAnimationType: { init: ">", check: "String", apply: "_applyProperty" },

		/**
        * Title position property.
        *
        * Is the title above or below the gauge
        */
		titlePosition: { init: "above", check: "String", apply: "_applyProperty" },

		/**
        * Define your own sectors rather than using those built in.
        *
        */
		customSectors: { init: null, check: "Array", apply: "_applyProperty" },

		/**
		 * LabelColor properties.
		 *
		 * Sets the color of the label.
		 */
		labelColor: { check: "Color", apply: "_applyProperty", nullable: true, themeable: true },

		/**
		 * ValueColor properties.
		 *
		 * Sets the color of the label.
		 */
		valueColor: { check: "Color", apply: "_applyProperty", nullable: true, themeable: true },
	},

	construct: function () {

		this.base(arguments);

		this.addListenerOnce("appear", this.__onAppear);
	},

	members: {

		/**
		 * Creates the justGage widget the first time the DOM element is created.
		 */
		__onAppear: function (e) {

			this.__createJustGageElement();

		},

		syncWidget: function (jobs) {

			if (!jobs || !jobs["update"])
				return;

			// update the metrics on the server
			this.__createJustGageElement();
		},

		/**
		 * Creates the JustGage widgets inside our widget container.
		 */
		__createJustGageElement: function () {

			var elem = this.getContentElement();
			var dom = elem.getDomElement();
			if (dom == null)
				return;

			var elementId = this.getId();
			elementId = elementId ? elementId : "justgage";
			dom.id = elementId;

			var Title = this.getTitle();
			var Label = this.getLabel();
			var Symbol = this.getSymbol();
			var Decimals = this.getDecimals();

			var config = {
				id: elementId,
				value: this.getValue(),
				min: this.getMinimum(),
				max: this.getMaximum(),
				hideMinMax: !this.isShowMinMax(),
				title: Title == null ? "" : Title,
				label: Label == null ? "" : Label,
				symbol: Symbol == null ? "" : Symbol,
				pointer: this.isShowPointer(),
				reverse: this.isReverse(),
				hideValue: !this.isShowValue(),
				donut: this.isShowDonut(),
				noGradient: !this.isShowGradient(),
				humanFriendly: this.isHumanFriendly(),
				humanFriendlyDecimal: Decimals,
				decimals: Decimals,
				differential: this.isDifferential(),
				counter: this.isCounter(),
				formatNumber: this.isFormatNumber(),
				startAnimationType: this.getStartAnimationType(),
				refreshAnimationType: this.getRefreshAnimationType(),
				titlePosition: this.getTitlePosition(),
				relativeGaugeSize: true
			};

			// assign the colors only if they have a value. the latest
			// release of the justgage library has a bug (or feature) in the
			// kvLookup function and doesn't recognized undefined or null as
			// undefined or null...
			if (this.getTextColor())
				config.titleFontColor = this.__resolveColor(this.getTextColor());
			if (this.getLabelColor())
				config.labelFontColor = this.__resolveColor(this.getLabelColor());
			if (this.getBackgroundColor())
				config.gaugeColor = this.__resolveColor(this.getBackgroundColor());
			if (this.getValueColor())
				config.valueFontColor = this.__resolveColor(this.getValueColor());

			if (this.getCustomSectors() != null)
				config.customSectors = this.getCustomSectors();

			if (wisej.web.DesignMode)
				config.startAnimationTime = 1;

			dom.innerHTML = "";
			this.__justGage = new JustGage(config);
		},

		/**
		 * Resolves the theme or named color to a usable html color value.
		 */
		__resolveColor: function (value) {

			return qx.theme.manager.Color.getInstance().resolve(value);
		},

		/**
		 * Recreate the JustGage control when one of the
		 * properties that cannot be refreshed is updated.
		 */
		_applyProperty: function (value, old) {
			this.__createJustGageElementDeferred();
		},

		/**
		 * Applies the background color.
		 */
		_applyBackgroundColor: function (value, old) {

			this.__createJustGageElementDeferred();
		},

		/**
		 * Applies the text/title color.
		 */
		_applyTextColor: function (value, old) {

			this.__createJustGageElementDeferred();
		},

		/**
		 * Applies the value property.
		 */
		_applyValue: function (value, old) {

			if (this.__justGage != null)
				this.__justGage.refresh(value);
		},

		// Schedules the recreation of the JustGage internal widget.
		__createJustGageElementDeferred: function () {

			qx.ui.core.queue.Widget.add(this, "update");

		},

		// overridden to delay the "render" event to give a chance
		// to the designer to pick the correct rendered control.
		_onDesignRender: function () {
			var me = this;
			setTimeout(function () {
				me.fireEvent("render");
			}, 50);
		},

	},

});