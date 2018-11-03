///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.extender.WebAria
 *
 * This extender class manages the WebARIA attributes for any widget.
 */
qx.Class.define("wisej.web.extender.WebAria", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);
	},

	properties: {

		/**
		 * Controls property.
		 *
		 * Collection of widgets and relative ARIA properties.
		 */
		controls: { init: null, nullable: true, check: "Array", apply: "_applyControls" },
	},

	members: {

		_applyControls: function (value, old) {

			// remove previous "aria-" attributes.
			if (old != null && old.length > 0) {
				for (var i = 0; i < old.length; i++) {

					var id = old[i].id;
					var comp = Wisej.Core.getComponent(id);
					if (comp) {
						var el = comp.getAccessibilityElement();
						if (el) {
							var attributes = old[i].attributes;
							for (var a in attributes) {
								el.removeAttribute(a);
							}
						}
					}
				}
			}

			// assign the specified aria-attributes.
			if (value != null && value.length > 0) {
				for (var i = 0; i < value.length; i++) {

					var id = value[i].id;
					var comp = Wisej.Core.getComponent(id);
					if (comp) {
						var el = comp.getAccessibilityElement();
						if (el) {
							var attributes = value[i].attributes;
							el.setAttributes(attributes);
						}
					}
				}
			}
		},
	},
});