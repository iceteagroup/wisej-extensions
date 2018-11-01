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
 * wisej.web.ext.SideButton
 * 
 */
qx.Class.define("wisej.web.ext.SideButton", {

	extend: wisej.web.Button,

	construct: function (text) {

		this.base(arguments, text);

		this._forwardStates.right = true;
		this._forwardStates.left = true;
		this._forwardStates.collapsed = true;
	},

	properties: {

		// overridden.
		appearance: { init: "side-button", refine: true },

		/**
		 *  Collapsed property.
		 */
		collapsed: { init: true, check: "Boolean", apply: "_applyCollapsed" },

		/**
		 *  Alignment property.
		 *  
		 *  Indicates the direction the button "retracts" to. When aligned to the right, the button
		 *  will retracts to the right end. When aligned to the left, it will retract to the left side.
		 */
		alignment: { init: "left", check: ["left", "right"], apply: "_applyAlignment" }

	},

	members: {

		/**
		 * Applies the Collapsed property.
		 */
		_applyCollapsed: function (value, old) {

			if (value) {
				this.addState("collapsed");
				this.setUserData("expandedWidth", this.getWidth());

				if (!this.hasState("hovered"))
					this.resetWidth();
			}
			else {
				this.removeState("collapsed");
			}
		},

		/**
		 * Applies the Alignment property.
		 */
		_applyAlignment: function (value, old) {

			if (old)
				this.removeState(old);
			if (value)
				this.addState(value);
		},

		_onPointerOver: function (e) {

			this.base(arguments, e);

			if (this.isCollapsed()) {
				this.setWidth(this.getUserData("expandedWidth"));
			}
		},

		_onPointerOut: function (e) {

			this.base(arguments, e);

			if (this.isCollapsed()) {
				this.resetWidth();
			}
		},

		// overridden
		renderLayout: function (left, top, width, height) {

			var changes = this.base(arguments, left, top, width, height);

			if (this.getAlignment() === "right" && this.isCollapsed()) {

				var parentBounds = this.getLayoutParent().getBounds();
				var rightMargin = parentBounds.width - left - this.getUserData("expandedWidth");

				var el = this.getContentElement();
				el.setStyles({
					left: null,
					right: "0px",
					marginRight: rightMargin + "px"
				});

			}
		}

	}

});
