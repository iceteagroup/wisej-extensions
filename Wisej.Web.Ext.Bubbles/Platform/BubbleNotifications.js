///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.extender.bubbles.BubbleNotifications
 *
 * This extender class manages a collection of bubble notifications.
 */
qx.Class.define("wisej.web.extender.bubbles.BubbleNotifications", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		this.__bubbleWidgets = {};
	},

	properties: {

		/**
		 * Bubbles property.
		 *
		 * Collection of widgets and relative values to show in a bubble notification. The items
		 * in the collection use this format: { id: [widget-id], value: [notification value] }.
		 */
		bubbles: { init: null, nullable: true, check: "Array", apply: "_applyBubbles" },

		/**
		 * Animation property.
		 *
		 * Defines the animation frames to use on the bubble when the value changes.
		 */
		animation: { init: null, nullable: true, check: "Map", themeable: true },

		/**
		 * Bubble alignment.
		 */
		alignment: {
			init: "topRight",
			check: ["topRight", "middleRight", "bottomRight", "topLeft", "topCenter", "middleLeft", "middleCenter", "bottomLeft", "bottomCenter"],
			apply: "_updateBubbles"
		},

		/*
		 * Bubble marging (Top, Right, Bottom, Left).
		 */
		margin: { init: null, check: "Array", apply: "_updateBubbles", transform: "_transformMargin" }
	},

	members: {

		// collection of bubble widgets, the key is the id of the
		// associated control.
		__bubbleWidgets: null,

		__updaterId: null,

		_transformMargin: function (value) {

			if (value instanceof Array) {
				return {
					top: value[0],
					right: value[1],
					bottom: value[2],
					left: value[3],
				};
			}
			else if (!isNaN(value)) {
				return {
					top: value,
					right: value,
					bottom: value,
					left: value,
				};
			}
			else {
				return {
					top: 0,
					right: 0,
					bottom: 0,
					left: 0,
				};
			}
		},

		_applyBubbles: function (value, old) {

			var hasBubbles = false;
			var bubbles = this.__bubbleWidgets;

			if (old != null && old.length > 0) {
				for (var i = 0; i < old.length; i++) {

					var id = old[i].id;

					// don't remove the bubble if it is in the
					// new values list.
					if (value && value.length > 0) {
						for (var j = 0; j < value.length; j++) {
							if (value[j].id == id) {
								id = null;
								break;
							}
						}
					}

					if (!id)
						continue;

					var bubble = bubbles[id];
					if (bubble) {
						bubble.destroy();
						delete bubbles[id];
					}
				}
			}

			if (value != null && value.length > 0) {
				for (var i = 0; i < value.length; i++) {

					var id = value[i].id;
					var bubble = bubbles[id];
					var comp = Wisej.Core.getComponent(id);
					if (comp) {
					
						if (bubble == null) {

							// create the new bubble widget.
							bubbles[id] = bubble =
								new wisej.web.extender.bubbles.Bubble(this, comp);

							// listen to clicks to fire our "bubbleClick" event.
							bubble.addListener("click", this._onBubbleClick, this);
						}

						bubble.setValue(value[i].value);
						bubble.setStyle(value[i].style);

						hasBubbles = true;
					}
					else {
						// destroy the bubble in case the component doesn't exist anymore...
						if (bubble) {
							bubble.destroy();
							delete this.__bubbleWidgets[id];
						}
					}
				}
			}

			// start the automatic updater.
			if (hasBubbles) {
				this.__updaterId = this.__updaterId
					|| qx.event.Idle.getInstance().addListener("interval", this.__livePositionUpdater, this);
			}
			else if (this.__updaterId) {
				qx.event.Idle.getInstance().removeListenerById(this.__updaterId);
				this.__updaterId = null;
			}
		},

		/**
		 * Updates the location of the existing bubbles.
		 */
		_updateBubbles: function () {

			var bubbles = this.__bubbleWidgets;
			if (bubbles != null) {

				for (var id in bubbles) {
					bubbles[id].updatePosition();
				}
			}
		},

		// fire "bubbleClick" returning the component
		// associated with the bubble.
		_onBubbleClick: function (e) {

			var bubble = e.getTarget();
			this.fireDataEvent("bubbleClick", bubble.getComponent());

		},

		/**
		 * Keeps the visible error icons aligned with their assigned target component.
		 */
		__livePositionUpdater: function () {

			this._updateBubbles();
		}
	},

	destruct: function () {

		this.__bubbleWidgets = null;
	}
});


/**
 * wisej.web.extender.bubbles.Bubble
 *
 * This is the single bubble notification widget.
 *
 * It's hooked to the relative widget and follows it around
 * the screen. When the widget is hidden, the bubble is hidden, when
 * it moves the bubble moves.
 */
qx.Class.define("wisej.web.extender.bubbles.Bubble", {

	extend: qx.ui.basic.Label,

	construct: function (owner, component) {

		this.base(arguments);

		if (!owner)
			throw new Error("Cannot create a bubble without a valid owner");

		if (!component)
			throw new Error("Cannot create a bubble without a valid component");

		// default animation
		var animation = owner.getAnimation() ||
		{
			"duration": 1000,
			"keep": "0",
			"timing": "cubic-bezier(0,1,1,0)",
			"keyFrames": {
				"0": { transform: "scale(1)" },
				"100": { transform: "scale(1.7)" }
			}
		};

		// find the target component for composite widgets.
		// retrieve the TabPage button, if the opener is a TabPage.
		if (component instanceof wisej.web.tabcontrol.TabPage)
			component = component.getButton();

		// retrieve the column header widget.
		if (component instanceof wisej.web.datagrid.ColumnHeader)
			component = component.getHeaderWidget();

		this.__owner = owner;
		this.__animation = animation;
		this.__component = component;

		this.setRich(true);
		this.setWrap(false);
		this.setAllowGrowX(true);
		this.setAllowGrowY(false);
		this.setTextAlign("center");

		// start hidden.
		this.exclude();

		// hook our handlers to follow the owner component.
		component.addListener("appear", this._onComponentAppear, this);
		component.addListener("disappear", this._onComponentDisappear, this);

		// finally create the bubble in the the top container.
		var container = this.getContainer();
		if (container)
			container.add(this);
	},

	properties: {

		// overridden appearance key.
		appearance: { init: "bubble", refine: true },

		/**
		 * A style string that is applied to the widget as a state.
		 * When the style is changed, the previous style is removed from the states.
		 * This value is used in the theme to change the appearance of the bubble
		 * according to the style. I.e.: "alert", "warning", "critical", "custom".
		 */
		style: { init: "alert", check: ["alert", "warning", "critical", "custom"], apply: "_applyStyle", themeable: true }
	},

	members: {

		// the bubble manager that owns this bubble.
		__owner: null,

		// the component that is attached to this bubble.
		__component: null,

		// animation for the bubble component.
		__animation: null,

		// return the component associated with this bubble.
		getComponent: function () {

			return this.__component;
		},

		// returns the container for the bubble placement.
		getContainer: function () {

			var component = this.getComponent();
			for (container = component.getLayoutParent(); container != null; container = container.getLayoutParent()) {
				if (container.isWisejControl && container.isTopLevel()) {
					return container;
				}
			}
		},

		// applies the value. every time the
		// value changes we restart the animation.
		_applyValue: function (value, old) {

			this.base(arguments, value, old);

			// hide when the value is 0 or null.
			if (!value) {

				this.exclude();
			}
			else if (this.__component.getBounds() != null) {

				this.show();
				this.animate();
			}
		},

		/**
		 * Animates the bubble using the defined animation.
		 */
		animate: function () {

			if (!this.__animation)
				return;

			var dom = this.getContentElement().getDomElement();
			if (!dom)
				return;

			qx.bom.element.Animation.animate(dom, this.__animation);
		},

		/**
		 * Updates the positon of the bubble.
		 */
		updatePosition: function () {

			var component = this.getComponent();
			var container = this.getLayoutParent();
			var componentBounds = component.getBounds();
			var componentLoc = component.getContentLocation();
			var containerLoc = container.getContentLocation();

			if (componentLoc == null || componentBounds == null || containerLoc == null)
				return;

			var sizeHint = this.getSizeHint();
			var bounds = {
				left: 0, top: 0,
				width: Math.max(sizeHint.width, sizeHint.height), height: sizeHint.height
			};

			var x = componentLoc.left;
			var y = componentLoc.top;

			var margin = this.__owner.getMargin();
			var alignment = this.__owner.getAlignment();

			switch (alignment) {
				case "topLeft":
					x = componentLoc.left;
					y = componentLoc.top;
					break;

				case "topCenter":
					x = componentLoc.left + componentBounds.width / 2;
					y = componentLoc.top;
					break;

				case "topRight":
					x = componentLoc.left + componentBounds.width;
					y = componentLoc.top;
					break;

				case "middleLeft":
					x = componentLoc.left;
					y = componentLoc.top + componentBounds.height / 2;
					break;

				case "middleCenter":
					x = componentLoc.left + componentBounds.width / 2;
					y = componentLoc.top + componentBounds.height / 2;
					break;

				case "middleRight":
					x = componentLoc.left + componentBounds.width;
					y = componentLoc.top + componentBounds.height / 2;
					break;

				case "bottomLeft":
					x = componentLoc.left;
					y = componentLoc.top + componentBounds.height;
					break;

				case "bottomCenter":
					x = componentLoc.left + componentBounds.width / 2;
					y = componentLoc.top + componentBounds.height;
					break;

				case "bottomRight":
					x = componentLoc.left + componentBounds.width;
					y = componentLoc.top + componentBounds.height;
					break;
			}

			x -= bounds.width / 2;
			y -= bounds.height / 2;
			x += margin.left - margin.right;
			y += margin.top - margin.bottom;
			x -= containerLoc.left;
			y -= containerLoc.top;

			this.setUserBounds(x, y, bounds.width, bounds.height);
		},

		// hide/show with the component.
		_onComponentAppear: function (e) {

			if (this.getValue()) {

				// make sure the bubble has a parent.
				if (this.getLayoutParent() == null) {
					var container = this.getContainer();
					if (container)
						container.add(this);
				}

				this.show();
				this.animate();
				this.updatePosition();
			}
		},

		// hide/show with the component.
		_onComponentDisappear: function (e) {

			this.exclude();
		},

		/**
		 * Applies the Style property.
		 */
		_applyStyle: function (value, old) {

			if (old)
				this.removeState(old.toLowerCase());

			if (value)
				this.addState(value.toLowerCase());
			else if (value == null)
				this.resetStyle();
		}
	},

	destruct: function () {

		// unhook our handlers to follow the owner component.
		var component = this.getComponent();
		if (component) {
			component.removeListener("appear", this._onComponentAppear, this);
			component.removeListener("disappear", this._onComponentDisappear, this);
		}

		this.__owner = null;
		this.__component = null;
	},


});
