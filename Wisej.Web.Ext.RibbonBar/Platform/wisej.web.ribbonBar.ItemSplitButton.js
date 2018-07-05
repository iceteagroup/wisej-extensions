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
 * wisej.web.ribbonBar.ItemSplitButton
 *
 * Represents a split button in a wisej.web.ribbonBar.Group widget.
 * The regular wisej.web.ribbonBar.ItemButton shows the dropdown menu 
 * when clicking anywhere on the button. The ItemSplitButton instead
 * shows the dropdown menu only when clicking on the "arrow".
 */
qx.Class.define("wisej.web.ribbonBar.ItemSplitButton", {

	extend: wisej.web.ribbonBar.Item,

	construct: function () {

		this.base(arguments);

		this.addState("vertical");
	},

	properties: {

		// overridden.
		controlType: { refine: true, init: "splitbutton" },

		/**
		 * ButtonMenu property.
		 *
		 * Assigns the menu to the button and changes the button to a menu-button component.
		 */
		buttonMenu: { init: null, nullable: true, apply: "_applyButtonMenu", transform: "_transformMenu" },

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

			var item = new qx.ui.toolbar.SplitButton().set({
				label: this.getLabel(),
				icon: this.getIcon(),
				keepFocus: true,
				focusable: false,
			});
			item.getChildControl("button").set({
				rich: true,
			});
			item.getChildControl("arrow").set({
				center: true,
				show:"icon"
			});

			item._forwardStates.vertical = true;
			item._forwardStates.horizontal = true;

			if (!wisej.web.DesignMode) {
				item.addListener("execute", this._onItemExecute, this);

				// redirect pointerover and pointerout to add/remove the hovered state.
				item.addListener("pointerover", this._onItemPointerOver, this, true);
				item.addListener("pointerout", this._onItemPointerOut, this, true);
			}

			return item;
		},

		/**
		 * Applies the orientation property.
		 */
		_applyOrientation: function (value, old) {

			var button = this.control.getChildControl("button");

			if (value == "vertical") {
				button.set({
					center: true,
					iconPosition: "top"
				});
				this.addState("vertical");
				this.removeState("horizontal");

				this.control._getLayout().dispose();
				this.control._setLayout(new qx.ui.layout.VBox);
				this.setLayoutProperties({ flex: 1, newColumn: true, columnBreak: true });
			}
			else {
				button.set({
					center: false,
					iconPosition: "left"
				});
				this.addState("horizontal");
				this.removeState("vertical");

				this.control._getLayout().dispose();
				this.control._setLayout(new qx.ui.layout.HBox);
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
		 * Applies the buttonMenu property.
		 */
		_applyButtonMenu: function (value, old) {

			this.control.setMenu(value);

			if (value) {
				this.__wireMenuItems(value);
				value.setPosition("bottom-left");
			}
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

		/**
		 * Event handler for the pointer over event.
		 */
		_onItemPointerOver: function (e) {

			this.addState("hovered");
		},

		/**
		 * Event handler for the pointer out event.
		 */
		_onItemPointerOut: function (e) {

			if (!qx.ui.core.Widget.contains(this, e.getRelatedTarget()))
				this.removeState("hovered");
		},

	}
});

