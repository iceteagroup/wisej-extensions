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
 * wisej.web.ext.TourPanel
 * 
 */
qx.Class.define("wisej.web.ext.TourPanel", {

	extend: qx.ui.popup.Popup,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [
		wisej.mixin.MWisejControl,
		wisej.mixin.MBorderStyle,
		wisej.mixin.MAccelerators,
		qx.ui.core.MRightToLeftLayout
	],

	construct: function () {

		this.base(arguments, new qx.ui.layout.Canvas());

		this.setPlacementModeX("best-fit");
		this.setPlacementModeY("best-fit");

		// show the arrow outside of the bounds.
		this.getContentElement().setStyle("overflow", "visible");

		this.addListener("move", this.__onMove, this);
		this.addListener("disappear", this.__onDisappear, this);
		this.addListener("changeBackgroundColor", this.__onChangeBackColor, this);

		this._createChildControl("arrow");
	},

	properties: {

		// overridden.
		appearance: { init: "tourpanel", refine: true },

		/**
		 * Container property.
		 *
		 * The container that this TourPanel is touring.
		 */
		container: { init: null, check: "Widget", nullable: true, transform: "_transformComponent" },

		/**
		 * HighlightTarget property.
		 *
		 * Determines whether the tour will highlight the target control.
		 */
		highlightTarget: { init: true, check: "Boolean" },

		/**
		 * HighlighterColor
		 *
		 * Sets the color index for the highlighter element.
		 */
		highlightColor: { init: "red", nullable: true, check: "Color", apply: "_applyHighlightColor", themeable: true },
	},

	members: {

		_forwardStates: {
			placementLeft: true,
			placementRight: true,
			placementAbove: true,
			placementBelow: true,
		},

		/** the current target widget. used to resize the highlighter.*/
		__target: null,

		/** the current target can receive pointer events.*/
		__targetEvents: false,

		/** the highlighter element. */
		__highlighterEl: null,

		/**
		 * Moves the tour panel next to the
		 * target widget according to the specified
		 * placement and offset.
		 *
		 * @param target {String|Widget} The target widget instance or id and path. 
		 * It can be "3" or "3/tools" or "3/tools[index]" or "3[index]".
		 * The "/" means name of child control and is equivalent to calling getChildControl(name).
		 * The "[]" means child control at the index position is equivalent to calling getChildren()[index].
		 *
		 * @param position {String} One the values alignment values.
		 * @param offset {Map} Offset from the target and position in pixels.
		 * @param allowEvents {Boolean} When true the target can receive pointer events.
		 */
		place: function (target, position, offset, allowEvents) {

			// resolve the target widget from the id and path.
			if (target == null) {
				target = this.getContainer();
			}
			else {
				target = this.__resolveTarget(target);

				if (target instanceof wisej.web.datagrid.ColumnHeader)
					target = target.getHeaderWidget();
			}

			// fire the "notfound" event to let the server handle the case where
			// the target is not found.
			if (!target) {
				this.fireEvent("notfound");
				return;
			}

			// make sure the target is rendered.
			qx.ui.core.queue.Manager.flush();

			// the tour panel should always be a child of the root.
			var root = qx.core.Init.getApplication().getRoot();
			if (this.getLayoutParent() != root)
				root.add(this);

			this.__target = target;
			this.__targetEvents = allowEvents;

			if (target != this.getContainer()) {

				// if the offset was not set, use the setting from the theme.
				if (offset != null)
					this.setOffset(offset);

				this.setPosition(qx.lang.String.hyphenate(position));
				this.placeToWidget(target, true);

				if (this.getHighlightTarget() || !allowEvents)
					this.__highlight(target, allowEvents);
			}
			else {

				// place the tour in relation to the
				// top edge of the parent.
				if (target) {
					this._place({
						top: 0,
						left: 0,
						bottom: 0,
						right: target.getWidth()
					});
				}

				this.removeState("placementLeft");
				this.removeState("placementRight");
				this.removeState("placementAbove");
				this.removeState("placementBelow");
			}

			this.show();
		},

		/**
		 * Internal method to read specific this properties and
		 * apply the results to the this afterwards.
		 *
		 * @param coords {Map} Location of the object to align the this to. This map
		 *   should have the keys <code>left</code>, <code>top</code>, <code>right</code>
		 *   and <code>bottom</code>.
		 */
		_place: function (coords)
		{
			// don't move the panel when the target has been hidden or
			// it will jump to 0,0.
			if (!coords.right || !coords.bottom)
				return;

			this.base(arguments, coords);

			// schedule the update of the arrow.
			qx.ui.core.queue.Widget.add(this);
		},

		/**
		 * This method is called during the flush of the
		 * {@link qx.ui.core.queue.Widget widget queue}.
		 *
		 * @param jobs {Map} A map of jobs.
		 */
		syncWidget: function (jobs) {

			this.__placeArrow();
		},

		// adjusts the arrow location to point to the middle of the target.
		__placeArrow: function () {

			var target = this.__target;
			if (!target)
				return;

			var bounds = this.getBounds();
			var location = target.getContentLocation();
			var arrow = this.getChildControl("arrow");

			if (this.hasState("placementBelow") || this.hasState("placementAbove")) {

				var arrowSize = arrow.getBounds().width;
				var panelSize = bounds.width;
				var diff = this.__getAbsoluteMiddle(location, false) - this.__getAbsoluteMiddle(bounds, false);

				panelSize = panelSize / 2;
				diff = Math.max(-panelSize + arrowSize, diff);
				diff = Math.min(panelSize - arrowSize, diff);

				arrow.resetMarginTop();
				arrow.setMarginLeft(diff | 0);
			}
			else if (this.hasState("placementRight") || this.hasState("placementLeft")) {

				var arrowSize = arrow.getBounds().height;
				var panelSize = bounds.height;
				var diff = this.__getAbsoluteMiddle(location, true) - this.__getAbsoluteMiddle(bounds, true);

				panelSize = panelSize / 2;
				diff = Math.max(-panelSize + arrowSize, diff);
				diff = Math.min(panelSize - arrowSize, diff);

				arrow.resetMarginLeft();
				arrow.setMarginTop(diff | 0);
			}
		},

		// returns the middle location in screen coordinates.
		__getAbsoluteMiddle: function (bounds, vertical) {

			if (bounds.width == undefined || bounds.height == undefined) {
				bounds.width = (bounds.right - bounds.left);
				bounds.height = (bounds.bottom - bounds.top);
			}

			return vertical
				? bounds.top + bounds.height / 2
				: bounds.left + bounds.width / 2;
		},

		// resolves the target id and path to an instance
		// of a widget, or null if not found.
		__resolveTarget: function (target) {

			var widget = target;

			if (typeof widget == "string") {

				// if the target is a string it can be:
				// 1
				// 1/name
				// 1[index]
				// 1/name[index]

				var path = target.split(/[//\[\]]/);
				for (var i = 0; i < path.length; i++) {

					if (!path[i])
						continue;
					
					if (i == 0) {
						// the first is the id.
						target = this.core.getComponent(this.core.IDPREFIX + path[i]);
					}
					else if (target) {

						if (!isNaN(path[i])) {
							// if it's an index integer, it's the index of a child or an array.
							var index = parseInt(path[i]);
							if (target.getChildren)
								target = target.getChildren()[index];
							else if (target._getChildren)
								target = target._getChildren()[index];
							else
								target = target[index];
						}
						else {
							// otherwise it's either a property or a child control
							var name = path[i];

							switch (name) {
								case "tools":
									target = this.__getTools(target);
									break;

								default:
									target = target.getChildControl(name, true);
									break;
							}
						}
					}

					if (!target)
						break;
				}
			}

			if (target instanceof qx.ui.core.Widget)
				return target;

			if (target instanceof wisej.web.datagrid.ColumnHeader)
				return target.getHeaderWidget();
		},

		// returns the collection of tool buttons hosted in the target widget.
		__getTools: function (widget) {

			var tools = widget.getTools();
			if (!tools || tools.length == 0)
				return [];

			// return the array of created widgets that corresponds to the tools.
			var list = [];
			var toolbuttons = [];
			if (widget.__leftToolsContainer)
				qx.lang.Array.append(toolbuttons, widget.__leftToolsContainer.getChildren());
			if (widget.__rightToolsContainer)
				qx.lang.Array.append(toolbuttons, widget.__rightToolsContainer.getChildren());

			for (var i = 0; i < tools.length; i++) {
				// find the widget in the left or right tools collection.
				var id = tools[i].id;
				for (var j = 0; j < toolbuttons.length; j++) {
					if (toolbuttons[j].getId() == id)
						list.push(toolbuttons[j]);
				}
			}
			return list;
		},

		// places an element with a shadow border over everything
		// below the tour panel with a "hole" sized for the target widget.
		__highlight: function (target, allowEvents) {

			if (!target)
				return;

			var el = this.__highlighterEl;
			var parent = this.getLayoutParent();

			if (!el) {
				el = this.__highlighterEl = new qx.html.Element("div", {
					boxSizing: "border-box",
					position: "absolute",
					display: "none",
					width: "100%",
					height: "100%",
					borderStyle: "solid"
				});
				el.hide();
				parent.getContentElement().add(el);
			}

			// calculate the highlighter border bounds.
			qx.html.Element.flush();
			var parentBounds = parent.getBounds();
			var targetLocation = target.getContentLocation();

			var borderWidth =
				Math.max(0, targetLocation.top) + "px " +
				Math.max(0, (parentBounds.width - targetLocation.right)) + "px " +
				Math.max(0, (parentBounds.height - targetLocation.bottom)) + "px " +
				Math.max(0, targetLocation.left) + "px";

			el.setStyles({
				display: "block",
				borderWidth: borderWidth,
				zIndex: 1e6 - 1 // below popups, messagebox, menus, above everything else.
			});

			var borderColor = this.getHighlightTarget() ? this.getHighlightColor() : "transparent";
			if (borderColor)
				el.setStyle("borderColor", borderColor);

			el.setStyle("pointerEvents", allowEvents ? "none" : "all");
			el.setAttribute("disabled", allowEvents ? "disabled" : null);

			if (!el.isVisible()) {
				qx.event.Idle.getInstance().removeListener("interval", this.__highlightLiveUpdater, this);
				qx.event.Idle.getInstance().addListener("interval", this.__highlightLiveUpdater, this);
			}

			el.show();
		},

		// adapts the color of the arrow.
		__onChangeBackColor: function (e) {

			var arrow = this.getChildControl("arrow");
			arrow.getContentElement().setStyle("border-right-color", e.getData());

		},

		// remove the highlighter when disappearing.
		__onDisappear: function (e) {

			var el = this.__highlighterEl;
			if (el)
				el.exclude();

			qx.event.Idle.getInstance().removeListener("interval", this.__highlightLiveUpdater, this);
		},

		// updates the location of the arrow when the tour panel changes its .location.
		__onMove: function (e) {

			qx.ui.core.queue.Widget.add(this);
		},

		// Keeps the highlighter in sync with the target size and position.
		__highlightLiveUpdater: function () {

			this.__highlight(this.__target, this.__targetEvents);
		},

		/**
		 * Applies the highlightColor property.
		 */
		_applyHighlightColor: function (value, old) {

			if (value == null) {
				this.resetHighlightColor();
				return;
			}

			var el = this.__highlighterEl;
			if (el) {
				el.setStyle("borderColor", value);
			}
		},

		/**
		 * Applies the alignment property.
		 */
		_applyAlignment: function (value, old) {

			this.setPosition(qx.lang.String.hyphenate(value));
		},

		// overridden
		_createChildControlImpl: function (id, hash) {
			var control;

			switch (id) {

				case "arrow":
					control = new qx.ui.basic.Image();
					this._add(control);
					break;

			}

			return control || this.base(arguments, id);
		},
	},

	destruct: function () {
		this.__target = null;
		this._disposeObjects("__highlighterEl");
	}
});