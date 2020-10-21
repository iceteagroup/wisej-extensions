
///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.TaskBar
 * 
 * Implements a TaskBar container that can be used outside of a wisej.web.Desktop widget
 * to manage minimized floating windows without having to use a Desktop or a custom manager.
 */
qx.Class.define("wisej.web.ext.TaskBar", {

	extend: wisej.web.desktop.TaskBar,

	include: [
		wisej.mixin.MWisejControl
	],

	construct: function () {

		this.base(arguments);

		this.owner = this;
		this.setAppearance("desktop/taskbar");

		this.__windows = {};

		var root = qx.core.Init.getApplication().getRoot();
		root.addListener("windowAdded", this._onWindowAdded, this);
		root.addListener("windowRemoved", this._onWindowRemoved, this);
	},

	properties: {

		/**
		 * positionPosition property.
		 *
		 * Returns or sets the docking position of the taskbar.
		 */
		position: { init: "bottom", check: ["left", "top", "right", "bottom"], apply:"_applyPosition" },

	},

	members: {

		__windows: null,

		/**
		 * Create the taskbar item when a new child window is created.
		 */
		_onWindowAdded: function (e) {

			var child = e.getData();
			if (child instanceof wisej.web.Form) {
				if (!this.__windows[child.$$hash]) {
					this.addWindow(child);
					this.__windows[child.$$hash] = child;
				}
			}
		},

		/**
		 * Remove the taskbar item when a new child window is created.
		 */
		_onWindowRemoved: function (e) {

			var child = e.getData();
			if (child instanceof wisej.web.Form) {
				if (this.__windows[child.$$hash]) {
					this.removeWindow(child);
					delete this.__windows[child.$$hash];
				}
			}
		},

		/**
		 * Applies the Position property.
		 */
		_applyPosition: function (value, old) {

			// change the orientation of the task bar and its children.
			this.setOrientation(this.__getOrientation());

			// update the taskbar position in the singleton preview widget.
			wisej.web.desktop.TaskbarPreview.getInstance().setTaskbarPosition(value);

			var items = this.getChildren();
			if (items != null && items.length > 0) {
				var orientation = this.__getOrientation();
				for (var i = 0; i < items.length; i++) {
					items[i].removeState("horizontal");
					items[i].removeState("vertical");
					items[i].addState(orientation);
				}
			}
		},

		// Returns the orientation according to the Position value.
		__getOrientation: function () {

			switch (this.getPosition()) {
				case "left":
				case "right":
					return "vertical";

				default:
					return "horizontal";
					break;
			}
		},
	}
});