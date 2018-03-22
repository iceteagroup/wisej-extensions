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
 * wisej.web.ribbonBar.AppButton
 *
 * Represents the application button displayed before the first RibbonPage.
 */
qx.Class.define("wisej.web.ribbonBar.AppButton", {

	extend: qx.ui.form.Button,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

	},

	properties: {

		// overridden.
		appearance: { refine: true, init: "ribbonbar/appbutton" },

		/**
		 * Visible property.
		 */
		// defined using the setter/getter methods.,
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
	}

});

