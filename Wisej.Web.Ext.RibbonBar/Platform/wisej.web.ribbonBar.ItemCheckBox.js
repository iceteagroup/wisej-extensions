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
 * wisej.web.ribbonBar.ItemCheckBox
 *
 * Represents a checkbox in a wisej.web.ribbonBar.Group widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemCheckBox", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "checkbox" },

		/**
		 * Value property.
		 */
		// defined using the setter/getter methods.,
	},

	members: {

		/**
		 * Value property.
		 */
		getValue: function (value) {
			return this.control.getValue();
		},
		setValue: function (value) {
			this.control.setValue(value);
		},

		// overridden
		_createItem: function () {

			var item = new qx.ui.form.CheckBox().set({
				center: true,
				label: this.getLabel(),
				keepFocus: true,
				focusable: false
			});

			if (!wisej.web.DesignMode)
				item.addListener("changeValue", this._onItemChangeValue, this);

			return item
		},

		// handles value change events.
		_onItemChangeValue: function (e) {

			if (!this.core.processingActions)
				this.fireDataEvent("changeValue", e.getData());

		},

	}
});

