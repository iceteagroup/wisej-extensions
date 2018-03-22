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
 * wisej.web.ribbonBar.RibbonPage
 *
 * Represents a page in a wisej.web.RibbonBar control.
 */
qx.Class.define("wisej.web.ribbonBar.RibbonPage", {

	extend: qx.ui.tabview.Page,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [
		  wisej.mixin.MWisejControl,
		  wisej.mixin.MShortcutTarget
	],

	construct: function () {

		this.base(arguments);
		this.setLayout(new qx.ui.layout.HBox());

		// the page control needs the controls in the
		// order they are declared to display the groups in 
		// the correct sequence.
		this.setReverseControls(false);

	},

	properties: {

		// appearance
		appearance: { init: "$parent/page", refine: true },

		/**
		 * Hidden property.
		 *
		 * Hides the tab from the ribbon.
		 */
		hidden: { init: true, check: "Boolean", apply: "_applyHidden" },

		/**
		 * TabBackgroundColor property.
		 *
		 * Changes the background color of the tab button.
		 */
		tabBackgroundColor: { init: null, check: "Color", apply: "_applyTabBackgroundColor" },

		/**
		 * TabTextColor property.
		 *
		 * Changes the text color of the tab button.
		 */
		tabTextColor: { init: null, check: "Color", apply: "_applyTabTextColor" },

	},

	members: {

		/**
		 * applies the Hidden property.
		 */
		_applyHidden: function (value, old) {

			if (value) {
				this.getButton().exclude()
			}
			else {
				this.getButton().show();
			}
		},

		/**
		 * Applies the tabBackgroundColor property.
		 */
		_applyTabBackgroundColor: function (value, old) {

			this.getButton().setBackgroundColor(value);
		},

		/**
		 * Applies the tabTextColor property.
		 */
		_applyTabTextColor: function (value, old) {

			this.getButton().setTextColor(value);
		},

		/**
		 * Selects the tabPage when the corresponding mnemonic is pressed.
		 */
		executeMnemonic: function () {

			if (!this.isEnabled() || this.isHidden())
				return false;

			var tabControl = this.getParent();
			if (tabControl) {
				tabControl.setSelection([this]);
				return true;
			}

			return false;
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			switch (id) {

				case "button":
					control = this.base(arguments, id, hash);
					control.setRich(true); // needed for mnemonics.
					break;
			}

			return control || this.base(arguments, id);
		},

	}

});
