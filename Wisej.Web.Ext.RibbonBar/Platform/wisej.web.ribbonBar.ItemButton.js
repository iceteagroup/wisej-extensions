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
 * wisej.web.ribbonBar.ItemButton
 *
 * Represents a button in a wisej.web.ribbonBar.Group widget.
 */
qx.Class.define("wisej.web.ribbonBar.ItemButton", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);

		this.addState("vertical");
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "button" },

		/**
		 * ButtonMenu property.
		 *
		 * Assigns the menu to the button and changes the button to a menu-button component.
		 */
		buttonMenu: { init: null, nullable: true, apply: "_applyButtonMenu", transform: "_transformMenu" },

		/**
		 * ShowArrow property.
		 *
		 * Shows the down arrow icon.
		 */
		showArrow: { init: false, check: "Boolean", apply: "_applyShowArrow" },

		/**
		 * Orientation property.
		 *
		 * Changes the layout of the button to either vertical (large) or horizontal (small).
		 */
		orientation: { init: "vertical", check: ["vertical", "horizontal"], apply: "_applyOrientation" },

		/**
		 * Pushed property.
		 *
		 * Determines whether the button is rendered using the "pushed" state.
		 */
		pushed: { init: false, check: "Boolean", apply: "_applyPushed" }
	},

	members: {

		// overridden
		/**
		 * @lint ignoreReferenceField(_forwardStates)
		 */
		_forwardStates: {
			focused: true,
			hovered: true,
			horizontal: true,
			vertical: true
		},

		// overridden
		_createItem: function () {

			var item = new qx.ui.toolbar.MenuButton().set({
				center: true,
				label: this.getLabel(),
				icon: this.getIcon(),
				rich: true,
				keepFocus: true,
				focusable: false,
				iconPosition: "top"
			});

			item._forwardStates.vertical = true;
			item._forwardStates.horizontal = true;

			// always create the arrow to occupy the space and
			// keep the vertical buttons aligned.
			item.getChildControl("label").exclude();
			item.getChildControl("arrow").hide();

			if (!wisej.web.DesignMode)
				item.addListener("execute", this._onItemExecute, this);

			return item
		},

		/**
		 * Applies the orientation property.
		 */
		_applyOrientation: function (value, old) {

			if (value == "vertical") {
				this.control.set({
					center: true,
					iconPosition: "top"
				});
				this.addState("vertical");
				this.removeState("horizontal");

				this.setLayoutProperties({ flex: 1, newColumn: true, columnBreak: true });
			}
			else {
				this.control.set({
					center: false,
					iconPosition: "left"
				});
				this.addState("horizontal");
				this.removeState("vertical");
				this._applyShowArrow(this.getShowArrow());

				this.setLayoutProperties({ flex: 0, newColumn: false });
			}
		},

		/**
		 * Applies the pushed property.
		 */
		_applyPushed: function (value, old) {

			if (value)
				this.control.addState("pushed");
			else
				this.control.removeState("pushed");
		},

		/**
		 * Applies the showArrow property.
		 */
		_applyShowArrow: function (value, old) {

			var arrow = this.control.getChildControl("arrow");

			if (this.getOrientation() == "vertical") {
				value
					? arrow.show()
					: arrow.hide();
			}
			else {
				value
					? arrow.show()
					: arrow.exclude();
			}
		},

		/**
		 * Applies the buttonMenu property.
		 */
		_applyButtonMenu: function (value, old) {

			this.control.setMenu(value);

			if (value) {
				this.__wireMenuItems(value);
				value.setPosition("bottom-left");
			}

			// show the down arrow if we have a menu.
			this.setShowArrow(value != null);
		},

		// iterates all the child items and wires the execute event
		// in order to fire it on the button owner.
		__wireMenuItems: function (parent) {

			if (parent == null)
				return;

			var children = parent.getChildren();
			for (var i = 0; i < children.length; i++) {
				var child = children[i];
				if (child instanceof qx.ui.menu.AbstractButton) {

					child.addListener("execute", this._onMenuItemExecute, this);

					// recurse.
					this.__wireMenuItems(child.getMenu());

				}
			}
		},

		// handles clicks on menu items.
		_onMenuItemExecute: function (e) {

			this.fireDataEvent("itemClick", e.getTarget());

		},

		// handles clicks on the child button.
		_onItemExecute: function (e) {

			this.fireEvent("execute");

		},

	}

});

