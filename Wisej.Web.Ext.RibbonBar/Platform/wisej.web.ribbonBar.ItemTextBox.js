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
 * wisej.web.ribbonBar.ItemTextBox
 *
 * Represents a textbox in a wisej.web.ribbonBar.Group widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemTextBox", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		var layout = new qx.ui.layout.Grid();
		layout.setRowFlex(0, 1);
		layout.setColumnFlex(1, 1);

		this.base(arguments, layout);

		// add local state properties.
		this.setStateProperties(this.getStateProperties().concat(["value"]));

		this.addListener("tap", this._onTap, this);
		this.addListener("keypress", this._onKeyPress, this);
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "textbox" },

		/**
		 * Value property.
		 */
		// defined using the setter/getter methods.,

		/**
		 * FieldWidth property.
		 *
		 * Sets the width of the inner qx.ui.form.TextField.
		 */
		fieldWidth: { check: "PositiveInteger", apply: "_applyFieldWith", themeable: true }

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

			var item = new qx.ui.form.TextField().set({
				allowGrowY: true,
				alignX: "right"
			});
			item.setLayoutProperties({ row: 0, column: 2 });

			if (!wisej.web.DesignMode)
				item.addListener("changeValue", this._onItemChangeValue, this);

			return item
		},

		// sets the focus to the inner control when clicked.
		_onTap: function (e) {
			this.control.focus();
		},

		// handles value change events.
		_onItemChangeValue: function (e) {
			if (!this.core.processingActions)
				this.fireDataEvent("changeValue", e.getData());
		},

		// handle key press events to navigate the fields
		// in the same wisej.web.ribbonBar.RibbonGroup using the Tab key.
		_onKeyPress: function (e) {

			switch (e.getKeyIdentifier())
			{
				case "Tab":
					if (e.isShiftPressed())
						this.getParent().focusPrev(this);
					else
						this.getParent().focusNext(this);

					e.stop();
					break;

				case "Up":
				case "Down":
				case "Left":
				case "Right":
				case "Home":
				case "End":
					break;

				default:
					this.setDirty(true);
					break;
			}
		},

		/**
		 * Applies the label  property.
		 */
		_applyLabel: function (value, old) {
			var label = this.getChildControl("label");
			label.setValue(value);
			value
				? label.show()
				: label.exclude();
		},

		/**
		 * Applies the icon property.
		 */
		_applyIcon: function (value, old) {
			var icon = this.getChildControl("icon");
			icon.setSource(value);
			value
				? icon.show()
				: icon.exclude();
		},

		/**
		 * Applies the fieldWidth property.
		 */
		_applyFieldWith: function (value, old) {

			this.control.setWidth(value);
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			switch (id) {

				case "icon":
					control = new qx.ui.basic.Image().set({
						source: this.getIcon(),
						anonymous: true,
						alignY: "middle",
						visibility: (this.getIcon() ? "visible" : "excluded")
					});
					this._add(control, { row: 0, column: 0 });
					break;

				case "label":
					control = new qx.ui.basic.Label().set({
						value: this.getLabel(),
						anonymous: true,
						alignY: "middle",
						visibility: (this.getLabel() ? "visible" : "excluded")
					});
					this._add(control, { row: 0, column: 1 });
					break;
			}

			return control || this.base(arguments, id);
		},

	}
});

