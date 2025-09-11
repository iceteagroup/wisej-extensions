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
 * wisej.web.ribbonBar.ItemSeparator
 *
 * Represents a vertical separator in a wisej.web.ribbonBar.RibbonGroup widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemSeparator", {

	extend: wisej.web.ribbonBar.Item,

	properties: {

		// overridden.
		controlType: { refine: true, init: "separator" },
	},

	members: {

		// overridden
		_createItem: function () {

			this.setLayoutProperties({ flex: 1, newColumn: true, columnBreak: true });

			var item = new qx.ui.core.Widget().set({
				allowGrowX: false
			});
			return item
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
