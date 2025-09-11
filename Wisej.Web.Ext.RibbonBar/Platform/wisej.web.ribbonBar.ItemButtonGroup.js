///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ribbonBar.ItemButtonGroup
 *
 * Represents a collection of buttons organized in an horizontal layout.
 */
qx.Class.define("wisej.web.ribbonBar.ItemButtonGroup", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "button-group" },
	},

	members: {

		// collection of child buttons.
		__buttons: null,

		// overridden
		/**
		 * @lint ignoreReferenceField(_forwardStates)
		 */
		_forwardStates: {
			focused: true,
			hovered: true,
			horizontal: true,
			vertical: true
		},

		// overridden
		_createItem: function () {

			var item = new qx.ui.container.Composite(new qx.ui.layout.Flow).set({
			});

			return item
		},

		/**
		 * Applies the buttons property.
		 */
		getButtons: function () {

			return this.__buttons;

		},
		setButtons: function (value) {

			this.control.removeAll();

			var buttons = this._transformComponents(value);
			if (buttons != null && buttons.length > 0) {

				for (var i = 0; i < buttons.length; i++) {
					var child = buttons[i];
					if (child != null)
						this.control.add(child);
				}
			}

			this.__buttons = buttons;
		},

		// overridden.
		_applyLabel: function (value, old) {
			// ignore.
		},

		// overridden.
		_applyIcon: function (value, old) {
			// ignore.
		},

		// overridden.
		_onPointerOver: function (e) {
			// ignore.
		},

		// overridden.
		_onPointerOut: function (e) {
			// ignore.
		},

	}

});

