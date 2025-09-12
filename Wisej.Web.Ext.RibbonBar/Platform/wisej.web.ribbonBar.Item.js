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
 * wisej.web.ribbonBar.Item
 *
 * Base class for all RibbonBar items.
 */
qx.Class.define("wisej.web.ribbonBar.Item", {

	type: "abstract",
	extend: qx.ui.core.Widget,

	// All Wisej controls must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejControl],

	/**
	 * @param layout {qx.ui.layout.Abstract} A layout instance to use to
	 * place widgets on the screen.
	 */
	construct: function (layout) {

		layout = layout || new qx.ui.layout.Grow();

		this.base(arguments);
		this._setLayout(layout);
		this._createChildControl(this.getControlType());

		// hovered event handlers.
		this.addListener("pointerover", this._onPointerOver, this);
		this.addListener("pointerout", this._onPointerOut, this);
	},

	properties: {

		// overridden.
		appearance: { refine: true, init: "ribbonbar-item" },

		/**
		 * Visible property.
		 */
		// defined using the setter/getter methods.,

		/**
		 * Type of item to created in the ribbon-bar group.
		 * This is the name of the child control created by the implementation of wisej.web.ribbonBar.Item.
		 */
		controlType: { init: null, check: "String" },

		/**
		 * Parent property.
		 *
		 * This is the parent of the ribbonBar item.
		 */
		parent: { init: null, nullable: true, apply: "_applyParent", transform: "_transformComponent" },

		/**
		 * ColumnBreak property.
		 *
		 * Indicates whether the next child items is located in a new column.
		 */
		columnBreak: { init: false, check: "Boolean", apply: "_applyColumnBreak" },

		/**
		 * Label property.
		 * 
		 * The text that is displayed in the implementation of the wisej.web.ribbonBar.Item.
		 */
		label: { init: "", check: "String", apply: "_applyLabel" },

		/**
		 * Icon property.
		 *
		 * Gets or sets the icon to display with the wisej.web.ribbonBar.Item.
		 */
		icon: { init: null, nullable: true, apply: "_applyIcon" },
	},

	members: {

		/** the inner control. */
		control: null,

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
		 * Applies the icon property.
		 */
		_applyIcon: function (value, old) {

			if (this.control == null)
				return;

			this.control.setIcon(value);
		},

		/**
		 * Applies the label  property.
		 */
		_applyLabel: function (value, old) {

			// the default implementation sets the Label property on the
			// child widget. the specific ribbon item can override this method to do something else.
			if (this.control)
				this.control.setLabel(value);
		},

		/**
		 * Applies the parent property.
		 * 
		 */
		_applyParent: function (value, old) {

			if (old) {

				if (!value) {
					try {

						if (old.indexOf != null && old.indexOf(this) > -1)
							old.remove(this);
						else if (old._indexOf != null && old._indexOf(this) > -1)
							old._remove(this);

					} catch (ex) {

						// ignore this error.
					}
				}
			}

			// register with the new parent
			if (value) {

				if (value.add) {
					value.add(this);
				}
				else {
					value._add(this);
				}
			}
		},

		/**
		 * Applies the columnBreak property.
		 *
		 * Causes the parent RibbonGroup to update the layout.
		 */
		_applyColumnBreak: function (value, old) {

			this.setLayoutProperties({ columnBreak: value });
		},

		/**
		 * Creates the inner widget .
		 *
		 * @abstract
		 * @return {Widget} the widget to render in the wisej.web.ribbonBar.Item child.
		 */
		_createItem: function () {
			this.warn("Missing _createItem() implementation!");
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			if (id == this.getControlType()) {

				this.control = control = this._createItem();
				this._add(control);
			}

			return control || this.base(arguments, id);
		},

		/**
		 * Event handler for the pointer over event.
		 */
		_onPointerOver: function (e) {

			this.addState("hovered");
		},

		/**
		 * Event handler for the pointer out event.
		 */
		_onPointerOut: function (e) {

			if (!qx.ui.core.Widget.contains(this, e.getRelatedTarget()))
				this.removeState("hovered");
		},

	}

});

