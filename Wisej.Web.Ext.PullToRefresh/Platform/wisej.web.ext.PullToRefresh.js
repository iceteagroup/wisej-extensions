///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.PullToRefresh
 *
 * Loads bing's image of the day and rotates them over the target control's background.
 */
qx.Class.define("wisej.web.ext.PullToRefresh", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);
	},

	properties: {

		/**
		 * BackColor property.
		 * 
		 * The background color of the loader.
		 */
		backColor: { init: null, nullable: true, check: "Color", apply: "_applyBackColor" },

		/**
		 * DropDownHeight property.
		 * 
		 * The drop down height of the refresh component.
		 */
		dropDownHeight: { init: 50, nullable: false, check: "Integer", apply: "_applyDropDownHeight" },

		/**
		 * ImageSource property.
		 * 
		 * The image to display on the refresh component.
		 */
		imageSource: { init: null, check: "String", apply: "_applyImageSource" },

		/**
		 * Containers property.
		 * 
		 * The collection of scrollable controls that will receive pull-to-refresh functionality.
		 */
		scrollContainers: { init: null, nullable: true, check: "Array", apply: "_applyPullToRefresh" },

	},

	members: {

		// collection of pull to refresh widgets, the key is the id of the
		// associated control.
		__scrollableControls: null,

		// the selected and focused control.
		_focusedControl: null,
		_startY: null,
		_endY: null,

		_applyBackColor: function (value, old) {

			this.__scrollableControls?.forEach((value, key) => {
				value.style.backgroundColor = value;
			});
		},

		_applyDropDownHeight: function (value, old) {
			// recreate the pull-to-refresh components with the new height.
			this._applyPullToRefresh(this.getScrollContainers(), null);
		},

		_applyImageSource: function (value, old) {

			this.__scrollableControls?.forEach((value, key) => {
				value.style.backgroundImage = `url(${value})`;
			});
		},

		// applies pull to refresh on the given scrollable controls.
		_applyPullToRefresh: function (value, old) {

			// clear old scroll controls.
			this._clear();

			// add scroll controls.
			if (value) {

				for (var i = 0; i < value.length; i++) {

					var comp = Wisej.Core.getComponent(value[i].id);

					if (this._isCreated(comp)) {
						this._addPullToRefresh(comp);
					} else {
						comp.addListenerOnce("appear", (e) => {
							var control = e.getTarget();
							this._addPullToRefresh(control);
						});
					}
				}
			}
		},

		// determines if a control is created in the dom.
		_isCreated: function (control) {
			return control.getContentElement().getDomElement();
		},

		// clears the existing pull-to-refresh functionality.
		_clear: function () {

			this.__scrollableControls?.forEach((value, key) => {
				value.remove();
				key.removeListener('pointerdown', this._onPointerDown, this);
			});
		},

		// returns the control that will receive the pull to refresh.
		__getTargetControl: function (control) {

			return control.getChildrenContainer().getContentElement().getDomElement();
		},

		// creates and returns a visual loader element.
		_getLoaderElement: function () {

			var height = this.getDropDownHeight();
			var loader = document.createElement("div");
			
			loader.style.width = "100%";
			loader.style.top = `-${height}px`;
			loader.style.position = "relative";
			loader.style.height = `${height}px`;
			loader.style.backgroundPosition = "center";
			loader.style.backgroundRepeat = "no-repeat";
			loader.style.backgroundSize = `${height - 20}px`;
			loader.style.backgroundColor = this.getBackColor();
			loader.style.backgroundImage = `url(${this.getImageSource()})`;

			return loader;
		},

		// adds pull to refresh to a given scrollable control.
		_addPullToRefresh: function (control) {

			var container = this.__getTargetControl(control);
			var loader = this._getLoaderElement();

			if (!this.__scrollableControls)
				this.__scrollableControls = new Map();

			this.__scrollableControls.set(control, loader);

			container.parentElement.insertBefore(loader, container.parentElement.first);

			control.addListener('pointerdown', this._onPointerDown, this);
		},

		// starts listening for pointer "drag" events.
		_onPointerDown: function (e) {

			var target = e.getCurrentTarget();

			this._focusedControl = target;
			this._startY = e.getDocumentTop();
			
			target.getApplicationRoot().addListener("pointerup", this._onPointerUp, this);
			target.getApplicationRoot().addListener("pointermove", this._onPointerMove, this);
		},

		// adjusts the container and loader position.
		_onPointerMove: function (e) {

			this._endY = e.getDocumentTop();

			var diff = this._startY - this._endY;
			var height = this.getDropDownHeight();

			if (this._startY < this._endY && diff > -height) {

				var container = this.__getTargetControl(this._focusedControl);
				container.style.transform = 'translateY(' + (this._endY - this._startY) + 'px)';
				container.style.transition = 'none';

				var loader = this.__scrollableControls.get(this._focusedControl);
				loader.style.transform = 'translateY(' + (this._endY - this._startY) + 'px)';
				loader.style.transition = 'none';
			}
		},

		// performs the "refresh" and restores the position.
		_onPointerUp: function (e) {

			var target = this._focusedControl;

			target.getApplicationRoot().removeListener("pointerup", this._onPointerUp, this);
			target.getApplicationRoot().removeListener("pointermove", this._onPointerMove, this);

			if (this._endY > this._startY) {

				var container = this.__getTargetControl(this._focusedControl);
				container.style.transform = 'translateY(0)';
				container.style.transition = 'transform 0.3s';

				var loader = this.__scrollableControls.get(this._focusedControl);
				loader.style.transform = 'translateY(0)';
				loader.style.transition = 'transform 0.3s';

				this.fireDataEvent("refresh", this._focusedControl);

				this._focusedControl = null;
			}
		}
	},

	destruct: function () {

	}
});
