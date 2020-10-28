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
 * wisej.web.ribbonBar.RibbonGroup
 *
 * Represents a container in a wisej.web.ribbonBar.Page control.
 */
qx.Class.define("wisej.web.ribbonBar.RibbonGroup", {

	extend: qx.ui.container.Composite,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [
		wisej.mixin.MWisejControl,
		qx.ui.core.MRemoteChildrenHandling,
		qx.ui.core.MRightToLeftLayout
	],

	construct: function (label) {

		this.base(arguments, new qx.ui.layout.Grid());

		this._createChildControl("pane");
		this._createChildControl("label");

		if (label)
			this.setLabel(label);

		var layout = this.getLayout();
		layout.setRowFlex(0, 1);
		layout.setColumnFlex(0, 1);

		// the group control needs the controls in the
		// order they are declared to display the items in 
		// the correct sequence.
		this.setReverseControls(false);

		// RightToLeft support.
		this.setRtlLayout(true);
		this.addListener("changeRtl", this._onRtlChange, this);
	},

	properties: {

		// overridden.
		appearance: { refine: true, init: "ribbonbar-group" },

		/**
		 * Visible property.
		 */
		// defined using the setter/getter methods.,

		/**
		 * Label property;
		 *
		 */
		label: { init: "", check: "String", apply: "_applyLabel" },

		/**
		 * ShowButton property.
		 *
		 * Determines whether the "advanced" button is visible or hidden.
		 */
		showButton: { init: false, check: "Boolean", apply: "_applyShowButton" },
	},

	members: {

		/**
		 * Visible property
		 */
		getVisible: function () {
			return this.getVisibility() === "visible";
		},
		setVisible: function (value) {
			this.setVisibility(value ? "visible" : "excluded");
		},

		/**
		 * Applies the Label property.
		 */
		_applyLabel: function (value, old) {

			var label = this.getChildControl("label");

			label.setValue(value);
			value ? label.show() : label.exclude();
		},

		/**
		 * Applies the ShowButton property.
		 */
		_applyShowButton: function (value, old) {
			value
				? this._showChildControl("button")
				: this._excludeChildControl("button");
		},

		// reference to the inner children container.
		__childrenContainer: null,

		/**
		 * The children container needed by the {@link qx.ui.core.MRemoteChildrenHandling}
		 * mixin.
		 *
		 * @return {qx.ui.container.Composite} pane sub widget
		 */
		getChildrenContainer: function () {

			if (this.__childrenContainer == null)
				this.__childrenContainer = this.getChildControl("pane");

			return this.__childrenContainer;
		},

		/**
		 * Focuses the next focusable ribbon item (ItemTextBox or ItemComboBox) in the RibbonGroup.
		 */
		focusNext: function (current) {

			var children = this.getChildren();
			var index = children.indexOf(current);
			
			for (var i = index + 1; i < children.length; i++) {
				if (this.__isFocusable(children[i])) {
					children[i].focus();
				}
			}
		},

		/**
		 * Focuses the previous focusable ribbon item (ItemTextBox or ItemComboBox) in the RibbonGroup.
		 */
		focusPrev: function (current) {

		},

		// checks if the ribbonbar.Item is focusable.
		__isFocusable: function (item) {
			return item.control != null && item.control.isFocusable();
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			switch (id) {

				case "label":
					control = new qx.ui.basic.Label().set({
						value: this.getLabel(),
						allowGrowX: true
					});
					this._add(control, { row: 1, column: 0 });
					break;

				case "button":
					control = new qx.ui.form.Button().set({
						keepFocus: true,
						show: "icon",
						icon: "ribbon-group"
					});
					control.addListener("execute", this._onButtonClick, this);
					this._add(control, { row: 1, column: 1 });
					break;

				case "pane":
					control = new qx.ui.container.Composite(new wisej.web.ribbonBar.RibbonGroupLayout());
					this._add(control, { row: 0, column: 0, colSpan: 2 });
					break;
			}

			return control || this.base(arguments, id);
		},

		// fires the "buttonClick" even when clicking on the group "advanced" button.
		_onButtonClick: function (e) {
			this.fireEvent("buttonClick");
		},

		// Listens to "changeRtl" to mirror the label and icon position.
		_onRtlChange: function (e) {

			if (e.getData() === e.getOldData())
				return;

			var rtl = e.getData();
			if (rtl != null) {
				this._mirrorChildren(rtl);
			}
		},
	}

});