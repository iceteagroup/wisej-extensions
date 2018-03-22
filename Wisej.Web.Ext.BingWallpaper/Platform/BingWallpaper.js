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
 * wisej.web.ext.BingWallpaper
 *
 * Loads bing's image of the day and assigns it to the target control's background image property.
 */
qx.Class.define("wisej.web.ext.BingWallpaper", {

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
				var target = this.__getTargetControl();
				if (target) {

					//  update the fade transition on the desktop widget.
					target.getContentElement().setStyle("transition", "background-image " + this.getFadeTime() + "ms", true);

					// get the next image index.
					this.__currentImageIndex++;
					if (this.__currentImageIndex >= images.length)
						this.__currentImageIndex = 0;

					// preload the image and fadein/out
					qx.io.ImageLoader.load(images[this.__currentImageIndex], function (url, entry) {

						if (entry.loaded) {

							if (target.isDesktop) {
								target.setWallpaper(url);
							}
							else {
								target.setBackgroundImages([{
									image: url,
									layout: "cover"
								}]);
							}
						}
					});
				}
				else {
					// if we don't have a desktop yet, reschedule.
					var me = this;
					setTimeout(function () {
						me.__setNextImage();
					}, 100);
				}
			}
		},

		// returns the control that will receive the background image.
		__getTargetControl: function () {

			return this.getControl() || Wisej.Platform.getDesktop();
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

});
