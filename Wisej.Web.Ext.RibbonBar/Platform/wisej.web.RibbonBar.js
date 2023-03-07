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
		 * CompactView property.
		 *
		 * Enabled the compact-view mode making only the tab buttons visible.
		 */
		compactView: { init: false, check: "Boolean", apply: "_applyCompactView" },

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

				// when in compact view, temporarily expand the ribbon bar.
				if (this.isCompactView())
					this.getContentElement().setStyle("overflow", "visible");
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
		 * Applies the compactView property.
		 */
		_applyCompactView: function (value, old) {

			// if the ribbon bar is not visible we have to wait until it's visible
			// to enable the compact view mode.
			if (!this.isSeeable()) {
				if (value) {
					this.addListenerOnce("appear", function () {
						this._applyCompactView(this.getCompactView());
					}, this);
				}
				return;
			}

			var bar = this.tabview.getChildControl("bar");
			var barSize = bar.getSizeHint();

			if (value) {
				this.setMaxHeight(barSize.height);
				this.tabview.setAllowEmptySelection(true);
				this.setSelectedIndex(-1);
				this.setZIndex(11);
				this.getContentElement().setStyle("overflow", "hidden");

				// handle pointer/mouse events globally when in compact view mode.
				qx.event.Registration.addListener(
					window.document.documentElement,
					"pointerdown",
					this._onCompactViewPointerDown, this, true);
			}
			else if (old) {
				this.resetMaxHeight();
				this.tabview.setAllowEmptySelection(false);
				this.setSelectedIndex(0);
				this.resetZIndex();
				this.getContentElement().setStyle("overflow", "hidden");

				qx.event.Registration.removeListener(
					window.document.documentElement,
					"pointerdown",
					this._onCompactViewPointerDown, this, true);
			}
		},

		/**
		 * Handles the pointerdown event on any element when in compact view mode.
		 */
		_onCompactViewPointerDown: function (e) {

			var target = e.getTarget();
			target = qx.ui.core.Widget.getWidgetByElement(target, true);

			// if the user clicked anywhere outside of the ribbon, collapse the view.
			if (qx.ui.core.Widget.contains(this, target))
				return;

			// check if the click was on menu item.
			var container = target.isWisejMenu ? target.findContainer() : null;
			var opener = container ? container.getOpener() : null;
			if (opener && qx.ui.core.Widget.contains(this, opener))
				return;

			// check if the click was on a popup.
			var popup = target;
			while (popup && !(popup instanceof qx.ui.popup.Popup)) {
				popup = popup.getLayoutParent();
			}
			if (popup)
				return;

			this.setSelectedIndex(-1);
			this.getContentElement().setStyle("overflow", "hidden");
		},

		/**
		 * Applies the designItem property.
		 *
		 * Adds a border to the widget.
		 */
		_applyDesignItem: function(value, old)
		{
			if (old) {
				old.getContentElement().removeStyle("border");
				old.syncAppearance();
			}

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

			this.resetHeight();
			this.invalidateLayoutCache();
			var size = this.getSizeHint();

			return {
				width: size.width,
				height: size.height,
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
	},

	destruct: function () {
		qx.event.Registration.removeListener(
			window.document.documentElement,
			"pointerdown",
			this._onCompactViewPointerDown, this, true);
	}
});
