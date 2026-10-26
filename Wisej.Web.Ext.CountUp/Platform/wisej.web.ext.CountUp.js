///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.CountUp
 */
qx.Class.define("wisej.web.ext.CountUp", {

	extend: wisej.web.Control,

	properties: {

		/**
		 * Value property.
		 */
		value: { init: 0, check: "Number", apply: "_applyValue" },

		/**
		 * Duration property.
		 *
		 * Sets the duration of the animation in milliseconds.
		 */
		duration: { init: 2500, check: "Integer", apply: "_applyDuration" },

		/**
		 * UseEasing property.
		 */
		useEasing: { init: true, check: "Boolean", apply: "_applyOption" },

		/**
		 * UseGrouping property.
		 */
		useGrouping: { init: true, check: "Boolean", apply: "_applyOption" },

		/**
		 * Decimal property.
		 */
		decimal: { init: ".", check: "String", apply: "_applyOption" },

		/**
		 * Separator property.
		 */
		separator: { init: ",", check: "String", apply: "_applyOption" },

		/**
		 * Numerals property.
		 */
		numerals: { init: null, check: "Array", nullable: true, apply: "_applyOption" },

		/**
		 * Align property.
		 */
		align: { init: "left", check: ["left", "center", "right"], apply: "_applyAlign" }
	},

	construct: function () {

		this.base(arguments);

		this.addListenerOnce("appear", this.__onAppear, this);
	},

	members: {

		/**
		 * Creates the CountUp widget the first time the DOM element is created.
		 */
		__onAppear: function (e) {
			this.__createCountUpElement();
		},

		/**
		 * Creates the CountUp widgets inside our widget container.
		 */
		__createCountUpElement: function () {

			var dom = this.getContentElement().getDomElement();


			var options = {
				useEasing: this.getUseEasing(),
				useGrouping: this.getUseGrouping(),
				separator: this.getSeparator(),
				decimal: this.getDecimal(),
				numerals: this.getNumerals(),
				align: this.getAlign()
			};

			dom.align = options.align;

			var value = this.getValue();
			var duration = this.getDuration() / 1000;
			this.__countUp = new CountUp(dom, 0, value, 0, duration, options);

			// when in design mode, update the widget immediately, we can't animate the designer.
			if (wisej.web.DesignMode)
				this.__countUp.duration = 1;

			this.__countUp.start(this.__onCountTerminatedCallback.bind(this));
		},

		// Callback invoked when the count has reached the end, fires
		// the "countTerminated" event to the server.
		__onCountTerminatedCallback: function () {

			this.fireEvent("countTerminated");
		},

		/**
		 * Applies the value property.
		 */
		_applyValue: function (value, old) {

			if (this.__countUp != null)
				this.__countUp.update(value);
		},

		/**
		 * Applies the duration property.
		 */
		_applyDuration: function (value, old, name) {

			if (this.__countUp != null)
				this.__countUp.duration = value;
		},

		/**
		 * Applies the align property.
		 */
		_applyAlign: function (value, old) {

			// CountUp.js has no align option: align the content element directly.
			// Before the element exists, __createCountUpElement applies it.
			var dom = this.getContentElement().getDomElement();
			if (dom)
				dom.align = value;
		},

		/**
		 * Applies one of the options.
		 */
		_applyOption: function (value, old, name) {

			var countUp = this.__countUp;
			if (countUp == null)
				return;

			switch (name) {

				case "numerals":
					// formatNumber() expects an array.
					countUp.options.numerals = value || [];
					break;

				case "separator":
				case "useGrouping":
					// an empty separator turns grouping off, as the CountUp constructor does.
					countUp.options.separator = this.getSeparator();
					countUp.options.useGrouping = this.getUseGrouping() && this.getSeparator() !== "";
					break;

				default:
					countUp.options[name] = value;
					break;
			}

			// redraw the displayed number with the new options. A running
			// animation also picks them up on its next frame.
			countUp.printValue(countUp.frameVal);
		},

	},

	destruct: function () {

		// stop the animation, otherwise it keeps updating the removed element
		// and calls back into this widget after it's disposed.
		var countUp = this.__countUp;
		if (countUp != null) {
			cancelAnimationFrame(countUp.rAF);
			countUp.callback = null;
			this.__countUp = null;
		}
	}

});