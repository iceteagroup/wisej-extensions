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
 * wisej.web.RibbonBar
 *
 * Represents a RibbonBar control.
 */
qx.Class.define("wisej.web.RibbonBar", {

	extend: wisej.web.Control,

	construct: function () {

		this.base(arguments, new qx.ui.layout.VBox());

		this.tabview = this._createChildControl("tabview");
		this.tabview.addListener("changeSelection", this._onChangeSelection, this);
	},

	properties: {

		// overridden.
		appearance: { refine: true, init: "ribbonbar" },

		/**
		 * Pages property.
		 *
		 * Collection of wisej.web.ribbonBar.Page.
		 *
		 * This property is defined using the getter/setter methods.
		 */

		/**
		 * SelectedIndex property.
		 *
		 * This property is defined using the getter/setter methods.
		 */

		/**
		 * Tools property.
		 *
		 * Collection of tool definitions to display to the right of the tab strip.
		 */
		tools: { check: "Array", apply: "_applyTools" },

		/**
		 * AppButton property.
		 *
		 * Assigns the widget to use as the application button.
		 */
		appButton: { init: null, apply: "_applyAppButton", transform: "_transformComponent" },

		/**
		 * DesignItem property.
		 *
		 * This property is used only at design time to highlight the
		 * item selected in the designer because child items are not controls
		 * and are not managed by the standard control designer.
		 */
		designItem: { init: null, apply: "_applyDesignItem", transform: "_transformComponent" },
	},

	members: {

		/** the inner wisej.web.ribbonBar.TabView */
		tabview: null,

		/**
		 * Applies the pages property.
		 *
		 * Sets the child pages to the inner tabview control.
		 */
		getPages: function () {
			return this.tabview.getControls();
		},
		setPages: function (value) {
			this.tabview.setControls(value);
		},

		/**
		 * SelectedIndex property.
		 *
		 * Returns or changes the current page in the inner tabview control.
		 */
		getSelectedIndex: function () {
			return this.tabview.getSelectedIndex();
		},
		setSelectedIndex: function (value) {
			this.tabview.setSelectedIndex(value);
		},

		/**
		 * Fires the "changePage" when the user changes the current page.
		 */
		_onChangeSelection: function (e) {

			// find the new active page.
			var page = e.getData()[0];
			if (page) {
				this.fireDataEvent("changePage", page);
			}
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			switch (id) {
				case "tabview":
					control = new wisej.web.ribbonBar.TabView();
					this.add(control);
					break;
			}

			return control || this.base(arguments, id);
		},

		// overridden to delay the "render" event to give a chance
		// to the designer to pick the correct rendered control.
		_onDesignRender: function () {

			var me = this;
			setTimeout(function () {
				me.fireEvent("render");
			}, 100);
		},

		/**
		 * Applies the AppButton property
		 *
		 * Adds the button to the slide bar before the scroll arrow.
		 */
		_applyAppButton: function (value, old) {

			var bar = this.tabview.getChildControl("bar");
			if (old) {
				bar._remove(old);
			}

			if (value) {
				bar._addAt(value, 0);
			}
		},

		/** 
		 * Applies the tools property.
		 */
		_applyTools: function (value, old) {

			if (value == null)
				return;

			if (value.length == 0 && (old == null || old.length == 0))
				return;

			var bar = this.tabview.getChildControl("bar");
			wisej.web.ToolContainer.install(this, bar, value, "left", { index: 4 }, null, "ribbonbar");
			wisej.web.ToolContainer.install(this, bar, value, "right", { index: 5 }, null, "ribbonbar");
		},

		/**
		 * Applies the designItem property.
		 *
		 * Adds a border to the widget.
		 */
		_applyDesignItem: function(value, old)
		{
			if (value) {
				value.getContentElement().setStyle("border", "1px dotted gray");
			}
		},

		/**
		 * getDesignMetrics
		 *
		 * Method used by the designer to retrieve metrics information about a widget in design mode.
		 */
		getDesignMetrics: function () {

			return {
				tabRects: this.__getTabRects(),
				itemMetrics: this.__getItemMetrics()
			};
		},

		// returns the rectangles of the tab buttons.
		__getTabRects: function () {
			return this.tabview.getTabRects();
		},

		// returns the rectangles of all the children of the ribbon bar, including
		// the groups, separators, and items.
		__getItemMetrics: function () {

			// collect all children at all levels.
			var all = [];
			this.tabview.addChildrenToQueue(all);

			// determine the top offset.
			var paneRect = this.tabview.getChildControl("pane").getBounds();
			if (!paneRect)
				return null;

			// return only the relevant components.
			var items = [];
			for (var i = 0, l = all.length; i < l; i++) {
				var widget = all[i];
				if (widget.isWisejComponent && widget.isSeeable()) {

					var widgetRect = widget.getBounds();
					if (widgetRect) {

						widgetRect.top += paneRect.top;

						items.push({
							id: widget.getId(),
							rect: widgetRect
						});

						// add the rect for the wrapped control.
						if (widget instanceof wisej.web.ribbonBar.ItemControl) {
							var control = widget.getControl();
							if (control && control.isWisejComponent) {
								items.push({
									id: control.getId(),
									rect: control.getBounds()
								});
							}
						}
					}
					else {
						items.push({
							id: widget.getId(),
							rect: null
						});
					}
				}
			}
			return items;
		},
	}

});
