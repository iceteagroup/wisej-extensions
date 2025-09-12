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
 * wisej.web.ribbonBar.ItemControl
 *
 * Represents a user-defined control in a wisej.web.ribbonBar.Group widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemControl", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "control" },

		/**
		 * Orientation property.
		 *
		 * Changes the layout of the control wrapper to either vertical (large) or horizontal (small).
		 */
		orientation: { init: "vertical", check: ["vertical", "horizontal"], apply: "_applyOrientation" },

		/**
		 * Control property.
		 *
		 * Reference to the widget to place inside the ribbonbar item.
		 */
		control: { init: null, nullable: true, apply: "_applyControl", transform: "_transformComponent" },
	},

	members: {

		// overridden
		_createItem: function () {

			var item = new qx.ui.core.Widget().set({
				anonymous: true,
				allowGrowX: true,
				allowGrowY: true,
			});
			item._setLayout(new qx.ui.layout.Grow);

			return item
		},

		/**
		 * Applies the control property.
		 */
		_applyControl: function (value, old) {

			if (old) {
				this.control._remove(old);
				this.removeState("control");

				old.removeListener("resize", this._onControlResize, this);
			}

			if (value) {
				// the wrapped widget should always fill this item.
				var widget = value;
				widget.resetUserBounds();
				this.control._add(widget);
				this.addState("control");

				widget.addListener("resize", this._onControlResize, this);
			}
		},

		// handles the "resize" even on the wrapped control
		// to fire the event to the wrapper component on the server
		// and update the wrapped control's size.
		_onControlResize: function (e) {

			this.fireDataEvent("controlResize", e.getData());
		},

		/**
		 * Applies the orientation property.
		 */
		_applyOrientation: function (value, old) {

			if (value == "vertical") {
				this.addState("vertical");
				this.removeState("horizontal");

				this.setLayoutProperties({ flex: 1, newColumn: true, columnBreak: true });
			}
			else {
				this.addState("horizontal");
				this.removeState("vertical");

				this.setLayoutProperties({ flex: 0, newColumn: false });
			}
		},

		// overridden.
		_applyLabel: function (value, old) {
			// ignore.
		},

		// overridden.
		_applyIcon: function (value, old) {
			// ignore.
		},

		// overridden
		_onPointerOver: function (e) {
			// ignore the hovered state.
		},

		// overridden
		_onPointerOut: function (e) {
			// ignore the hovered state.
		},
	}
});

