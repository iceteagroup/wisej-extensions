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
		animation: { init: null, nullable: true, check: "Map" }

	},

	members: {

		// collection of bubble widgets, the key is the id of the
		// associated control.
		__bubbleWidgets: {},

		_applyBubbles: function (value, old) {

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

					var bubble = this.__bubbleWidgets[id];
					if (bubble) {
						bubble.destroy();
						delete this.__bubbleWidgets[id];
					}
				}
			}

			if (value != null && value.length > 0) {
				for (var i = 0; i < value.length; i++) {

					var id = value[i].id;
					var bubble = this.__bubbleWidgets[id];
					var comp = Wisej.Core.getComponent(id);
					if (comp) {
						if (bubble == null) {

							// create the new bubble widget.
							this.__bubbleWidgets[id] = bubble = new wisej.web.extender.bubbles.Bubble(comp, this.getAnimation());

							// apply the custom style.
							if (value[i].style)
								bubble.setStyle(value[i].style);

							// listen to clicks to fire our "bubbleClick" event.
							bubble.addListener("click", this._onBubbleClick, this);
						}

						bubble.setValue(value[i].value);
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
		},

		// fire "bubbleClick" returning the component
		// associated with the bubble.
		_onBubbleClick: function (e) {

			var bubble = e.getTarget();
			this.fireDataEvent("bubbleClick", bubble.getComponent());

		},
	},

	destruct: function () {

		this.__bubbleWidgets = null;
	},


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

	construct: function (component, animation) {

		this.base(arguments);

		// default animation
		animation = animation ||
			{
				"duration": 1000,
				"keep": "0",
				"timing": "cubic-bezier(0,1,1,0)",
				"keyFrames": {
					"0": { transform: "scale(1)" },
					"100": { transform: "scale(1.7)" }
				}
			};

		// save a reference to the owner.
		this.__component = component;
		this.__animation = animation;

		if (!component)
			throw new Error("Cannot create a bubble without a valid component");

		// start hidden.
		this.exclude();

		// add this widget to the same layout parent.
		component.getLayoutParent()._add(this);

		// hook our handlers to follow the owner component.
		component.addListener("move", this.__onComponentMove, this);
		component.addListener("resize", this.__onComponentResize, this);
		component.addListener("changeVisibility", this.__onComponentChangeVisibility, this);

		// show the animation the first time the dom is created.
		this.addListenerOnce("appear", function (e) {
			this.__updatePosition();
			setTimeout(this.animate.bind(this), 300);
		});
	},

	properties: {

		// overridden appearance key.
		appearance: { init: "bubble", refine: true },

		/**
		 * A style string that is applied to the widget as a state.
		 * When the style is changed, the previous style is removed from the states.
		 * This value is used in the theme to change the appearance of the bubble
		 * according to the style. I.e.: "warning", "error", "critical", ...
		 */
		style: { init: "", check: "String", apply: "_applyStyle", themeable: true }
	},

	members: {

		// the component that owns this bubble.
		__component: null,

		// return the component associated with this bubble.
		getComponent: function () {

			return this.__component;
		},

		// applies the value. every time the
		// value changes we restart the animation.
		_applyValue: function (value, old) {

			this.base(arguments, value, old);

			// hide when the value is 0 or null.
			if (!value) {
				this.exclude();
			}
			else if (this.__component.isVisible()) {

				this.show();
				this.animate();
			}
		},

		// expands the bubble performing a simple
		// scaling animation.
		animate: function () {

			if (!this.__animation)
				return;

			var dom = this.getContentElement().getDomElement();
			if (!dom)
				return;

			qx.bom.element.Animation.animate(dom, this.__animation);
		},

		_applyStyle: function (value, old) {

			if (old)
				this.removeState(old.toLowerCase());

			if (value)
				this.addState(value.toLowerCase());
		},

		// follow the component position.
		__onComponentMove: function (e) {

			this.__updatePosition();

		},

		// adjust to the component size.
		__onComponentResize: function (e) {

			this.__updatePosition();

		},

		// hide/show with the component.
		__onComponentChangeVisibility: function (e) {

			if (this.getValue() > 0) {
				this.__component.isVisible()
					? show()
					: exclude();
			}
		},

		// updates this bubble widget position.
		__updatePosition: function () {

			var bounds = this.__component.getBounds();
			if (bounds) {

				var mySize = this.getSizeHint();
				if (mySize) {
					var y = bounds.top - mySize.height;
					var x = bounds.left + bounds.width - mySize.width;
					this.setLayoutProperties({ left: x, top: y });
				}
			}
		},
	},

	destruct: function () {

		this.__component = null;
	},


});
