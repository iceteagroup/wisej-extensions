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
 * wisej.web.ext.CustomWallpaper
 *
 * Loads a list of custom images and rotates them over the target control's background.
 */
qx.Class.define("wisej.web.ext.CustomWallpaper", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		this.initRotationInterval();
	},

	properties: {

		/**
		 * Images property.
		 *
		 * List of images to rotate.
		 */
		images: { init: [], check: "Array", apply: "_applyImages" },

		/**
		 * FadeTime property.
		 *
		 * Determines the fade in/out time in milliseconds.
		 */
		fadeTime: { init: 1000, check: "PositiveInteger" },

		/**
		 * RotationInterval property.
		 *
		 * Determines the interval in milliseconds between each image rotation: 0 = no rotation.
		 */
		rotationInterval: { init: 60000, check: "PositiveInteger", apply: "_applyRotationInterval" },

		/**
		 * EnableAnimation property.
		 *
		 * Enables or disables a simple zoom animation when rotating images.
		 */
		enableAnimation: { init: true, check: "Boolean" },

		/**
		 * Control property.
		 *
		 * Determines the control that will receive the background images.
		 * When null, it uses the current desktop control.
		 */
		control: { init: null, check: "qx.ui.core.Widget", transform: "_transformComponent" },

	},

	members: {

		__rotationTimer: 0,
		__currentImageIndex: -1,
		__stylesheet: null,
		__stylesheetAfterStyle: null,
		__stylesheetBeforeStyle: null,

		/**
		 * Shows the first image immediately after the image list is updated.
		 */
		_applyImages: function (value, old) {

			this.__setNextImage();

		},

		// updates the image on the desktop.
		__setNextImage: function () {

			var images = this.getImages();
			if (images && images.length > 0) {

				var me = this;
				var target = this.__getTargetControl();
				if (!target) {

					// if we don't have a target yet, the desktop may have not been created in time.
					// delay and retry.
					setTimeout(function () {
						me.__setNextImage();
					}, 100);

					return;
				}

				// if the target is not visible, wait.
				if (!target.getBounds()) {
					target.addListenerOnce("appear", function () {
						me.__setNextImage();
					}, this);
					return;
				}

				// get the next image index.
				this.__currentImageIndex++;
				if (this.__currentImageIndex >= images.length)
					this.__currentImageIndex = 0;

				var fadeTime = this.getFadeTime();
				var nextImageUrl = images[this.__currentImageIndex];

				// load the image, but it may be preloaded already and return immediately.
				// check for the fading animation to be terminated before fading in the new image.
				qx.io.ImageLoader.load(nextImageUrl, function (url, entry) {

					if (entry.loaded) {

						me.__assignImage(target, url);
					}
				});
			}
		},

		// assign the image to the target.
		__assignImage: function (target, url) {
			
			this.__createStylesheet(target);

			var fadeTime = this.getFadeTime();

			var next = this.__stylesheetAfterStyle.opacity == 1
				? this.__stylesheetBeforeStyle
				: this.__stylesheetAfterStyle;

			var prev = this.__stylesheetAfterStyle.opacity == 0
				? this.__stylesheetBeforeStyle
				: this.__stylesheetAfterStyle;

			next.backgroundImage = "url(" + url + ")";
			prev.transition = next.transition = "opacity " + fadeTime + "ms, transform " + (fadeTime * 5) + "ms";

			if (this.getEnableAnimation()) {
				prev.transform = "scale(1)";
				next.transform = "scale(1.05)";
			}

			prev.opacity = 0;
			next.opacity = 1;
		},

		// creates the stylesheet and pseudo elements
		// used to swap images and fading.
		__createStylesheet: function (target) {

			if (this.__stylesheet)
				return;

			var fadeTime = this.getFadeTime();

			var className = "wisej-customwallpaper-" + this.$$hash;
			var css = "content:\"\";display:block;position:absolute;top:0px;left:0px;right:0px;bottom:0px;background-size:cover;" +
					  "opacity:0;transition:opacity " + fadeTime + "ms, transform " + (fadeTime * 5) + "ms";
			this.__stylesheet = qx.bom.Stylesheet.createElement("");
			qx.bom.Stylesheet.addRule(this.__stylesheet, "." + className + "::after", css); // __stylesheetAfterStyle
			qx.bom.Stylesheet.addRule(this.__stylesheet, "." + className + "::before", css); // __stylesheetBeforeStyle

			var rules = this.__stylesheet.cssRules;
			this.__stylesheetAfterStyle = rules[rules.length - 2].style;
			this.__stylesheetBeforeStyle = rules[rules.length - 1].style;

			target.getContentElement().addClass(className);
			qx.html.Element.flush();
		},

		// returns the control that will receive the background image.
		__getTargetControl: function () {

			var target = this.getControl();
			if (target && target.__getTarget)
				target = target.__getTarget();
			else
				target = Wisej.Platform.getDesktop() || Wisej.Platform.getMainPage();

			return target;
		},

		/**
		 * Applies the rotationInterval property.
		 */
		_applyRotationInterval: function (value, old) {

			clearInterval(this.__rotationTimer);

			if (value > 0) {
				var me = this;
				this.__rotationTimer = setInterval(function () {

					me.__setNextImage();

				}, value);
			}
		},
	},

	destruct: function () {


		if (this.__stylesheet) {
			qx.bom.Stylesheet.removeSheet(this.__stylesheet);
		}
	}


});
