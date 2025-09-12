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
 * wisej.web.ribbonBar.ItemRadioButton
 *
 * Represents a checkbox in a wisej.web.ribbonBar.Group widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemRadioButton", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "radiobutton" },

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

			var item = new qx.ui.form.RadioButton().set({
				center: true,
				label: this.getLabel(),
				keepFocus: true,
				focusable: false
			});

			if (!wisej.web.DesignMode)
				item.addListener("changeValue", this._onItemChangeChecked, this);

			return item
		},

		/**
		 * Event listener for <code>changeValue</code> event.
		 *
		 * @param e {qx.event.type.Data} Data event
		 */
		_onItemChangeChecked: function (e) {

			if (!this.core.processingActions)
				this.fireDataEvent("changeValue", e.getData());

			// uncheck all radio buttons in the same group.
			var group = this.getParent();
			var checked = e.getData();
			if (checked && group != null)
			{
				var children = group.getChildren();
				for (var i = 0; i < children.length; i++) {
					if (children[i] != this && children[i] instanceof wisej.web.ribbonBar.ItemRadioButton) {
						children[i].setValue(false);
					}
				}
			}
		},

	}
});