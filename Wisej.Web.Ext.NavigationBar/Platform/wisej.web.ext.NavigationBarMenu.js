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
 * Extends the wisej.web.menu.ContextMenu class to change the appearance of the context menu,
 */
qx.Class.define("wisej.web.ext.NavigationBarMenu", {

	extend: wisej.web.menu.ContextMenu,

	construct: function () {

		this.base(arguments);

		this.setAppearance("navbar-menu");
	},

	members: {

		/**
		 * Shows the context menu at the position.
		 *
		 * @param opener {Widget} the widget used to position the context menu.
		 * @param offset {Array} shorthand "offsetTop", "offsetRight", "offsetBottom", "offsetLeft",
		 * @param position {String} on of the placement values defined in {@link qx.ui.core.MPlacement.position}.
		 */
		show: function (opener, offset, position) {
			this.setOffset(offset || 0);
			this.setOpener(opener);
			this.setPosition(qx.lang.String.hyphenate(position));
			this.placeToWidget(opener, true);
			this.setVisibility("visible");
		}
	}
})

/**
 * Extends the wisej.web.menu.MenuItem class to change the appearance of the menu items.
 */
qx.Class.define("wisej.web.ext.NavigationBarMenuItem", {

	extend: wisej.web.menu.MenuItem,

	construct: function () {

		this.base(arguments);

		this.setAppearance("navbar-menu/item");
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