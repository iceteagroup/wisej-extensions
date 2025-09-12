///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * Extends the wisej.web.menu.ContextMenu class to change the appearance of the context menu.
 */
qx.Class.define("wisej.web.ext.NavigationBarMenu", {

	extend: wisej.web.menu.ContextMenu,

	properties: {
		appearance: { init: "navbar-menu", refine: true }
	}
});

/**
 * Extends the wisej.web.menu.MenuItem class to change the appearance of the menu items.
 */
qx.Class.define("wisej.web.ext.NavigationBarMenuItem", {

	extend: wisej.web.menu.MenuItem,

	properties: {
		appearance: { init: "navbar-menu/item", refine: true }
	},

	members: {

		/**
		 * Applies the menuItems property.
		 */
		_applyMenuItems: function (value, old) {

			this.base(arguments, value, old);

			var menu = this.getMenu();
			if (menu)
				menu.setAppearance("navbar-menu");
		}
	}
})