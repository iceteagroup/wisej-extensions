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
 * wisej.web.ext.JssorSlider
 */
qx.Class.define("wisej.web.ext.JssorSlider", {

	extend: wisej.web.Control,

	include: [
		wisej.mixin.MBorderStyle
	],

	properties: {

		appearance: { init: "jssorslider", refine: true },

		/**
		 * Images.
		 *
		 * Array of image definitions to display in the slider.
		 *
		 * Each image is defined by a map of options:
		 *
		 *	{
		 *		source		:"" // image source.
		 *		captions	:[{
		 *						html: "", // html for the caption.
		 *						x: #px,
		 *						y: #px,
		 *						width: #px,
		 *						height: #px,
		 *						transition: "" // transition code.
		 *					}],
		 *		transition	:"", // transition code
		 *	}
		 */
		images: { check: "Array", apply: "_applyImages" },

		/**
		 * LeftArrowImage.
		 *
		 * Sets the image to use for the left arrow button.
		 */
		leftArrowImage: { check: "String", apply: "_applyArrowImage", themeable: true },

		/**
		 * RightArrowImage property.
		 *
		 * Sets the image to use for the right arrow button.
		 */
		rightArrowImage: { check: "String", apply: "_applyArrowImage", themeable: true },

		/**
		 * BulletImage.
		 *
		 * Sets the image to use for the right arrow button.
		 */
		bulletImage: { check: "String", apply: "_applyBulletImage", themeable: true },

		/**
		 * FillMode.
		 *
		 * The way to fill the image in the slide.
		 */
		fillMode: { init: "stretch", check: ["stretch", "contain", "cover", "actual", "containOrActual"], apply: "_applyProperty" },

		/**
		 * Orientation.
		 *
		 * The orientation of the slider.
		 */
		orientation: { init: "horizontal", check: ["horizontal", "vertical"], apply: "_applyProperty" },

		/**
		 * StartIndex.
		 *
		 * The first slide to show when the slider is loaded the first time.
		 */
		startIndex: { init: 0, check: "PositiveInteger", apply: "_applyProperty" },

		/**
		 * AutoPlay.
		 *
		 * Whether to start the slideshow automatically.
		 */
		autoPlay: { init: true, check: "Boolean", apply: "_applyProperty" },

		/**
		 * WrapMode.
		 *
		 * How to wrap the slideshow when it reaches the last slide.
		 */
		wrapMode: { init: "wrap", check: ["none", "wrap", "rewind"], apply: "_applyProperty" },

		/**
		 * IdleTime.
		 *
		 * Idle time in milliseconds between slides.
		 */
		idleTime: { init: 3000, check: "PositiveInteger", apply: "_applyProperty", themeable: true },

		/**
		 * SlideDuration.
		 *
		 * Default transition duration in milliseconds.
		 */
		slideDuration: { init: 500, check: "PositiveInteger", apply: "_applyProperty", themeable: true },

		/**
		 * Columns.
		 *
		 * Number of slides to fit in the slider container.
		 */
		columns: { init: 1, check: "PositiveInteger", apply: "_applyProperty", themeable: true },

		/**
		 * SlideSpacing.
		 *
		 * Space between slides in pixels.
		 */
		slideSpacing: { init: 0, check: "PositiveInteger", apply: "_applyProperty", themeable: true },

		/**
		 * SlideSize.
		 *
		 * Size of the slide image inside the container.
		 */
		slideSize: { init: { width: 0, height: 0 }, check: "Map", apply: "_applyProperty" },

		/**
		 * SlideOffset.
		 *
		 * Offset position in pixels.
		 */
		slideOffset: { init: 0, check: "PositiveInteger", apply: "_applyProperty" },

		/**
		 * ShowArrows.
		 *
		 * Shows the arrow navigation buttons.
		 */
		showArrows: { init: true, check: "Boolean", apply: "_applyProperty", themeable: true },

		/**
		 * ShowBullets.
		 *
		 * Shows the bullets navigation buttons.
		 */
		showBullets: { init: true, check: "Boolean", apply: "_applyProperty", themeable: true },

		/**
		 * ShowThumbnails.
		 *
		 * Shows the thumbnail navigation panel.
		 */
		showThumbnails: { init: true, check: "Boolean", apply: "_applyProperty", themeable: true },

		/**
		 * ThumbnailOptions.
		 *
		 * Sets the thumbnail options:
		 *
		 * {
		 *		wrap		:true|false,
		 *		showMode	:["never", "mouseOver", "always"],
		 *		alignment	:["none", "horizontal", "vertical", "both"],
		 *		columns		:#, // number of items to display in the thumbnail navigator container.
		 *		rows		:#, // specify lanes to arrange thumbnails in.
		 *		spacing		:{x: #pixels, y: #pixels}, // space in pixels between thumbnails.
		 *		orientation	:["horizontal", "vertical"],
		 *		scale:		:true|false,
		 * }
		 */
		thumbnailOptions: { check: "Map", apply: "_applyOptions" },

		/**
		 * ArrowOptions.
		 *
		 * Sets the navigation arrow options:
		 *
		 * {
		 *		showMode	:["never", "mouseOver", "always"],
		 *		alignment	:["none", "horizontal", "vertical", "both"],
		 *		margin		:{top, left, right, bottom},
		 *		scale:		:true|false,
		 * }
		 */
		arrowOptions: { check: "Map", apply: "_applyOptions" },

		/**
		 * BulletOptions.
		 *
		 * Sets the navigation bullet options:
		 *
		 * {
		 *		showMode	:["never", "mouseOver", "always"],
		 *		alignment	:["none", "horizontal", "vertical", "both"],
		 *		rows		:#, // rows to arrange bullets.
		 *		spacing		:{x: #pixels, y: #pixels}, // space in pixels between bullets.
		 *		orientation	:["horizontal", "vertical"],
		 *		margin		:{top, left, right, bottom},
		 *		scale:		:true|false,
		 * }
		 */
		bulletOptions: { check: "Map", apply: "_applyOptions" },
	},

	construct: function () {

		this.base(arguments);

		// resize the inner slider when the widget is resized.
		this.addListener("resize", this.__onResize);

		// build the slider when the widget is finally created.
		this.addListenerOnce("appear", function (e) {

			this.__buildSlider();
		});
	},

	members: {

		// reference to the jssor slider.
		jssorSlider: null,

		/**
		 * Applies the images to the slideshow.
		 */
		_applyImages: function (value, old) {

			// resolve the source for each image in the list
			// and compile the transitions.
			if (value) {
				for (var i = 0; i < value.length; i++) {

					var image = values[i];
					image.source = this._resolveImageSource(image.source);
					image.transition = image.transition ? eval(image.transition) : null;

					// compile the transitions for the captions.
					if (image.captions != null && image.captions.length > 0) {
						for (var j = 0; j < image.captions.length; j++) {
							var caption = captions[j];
							caption.transition = caption.transition ? eval(caption.transition) : null;
						}
					}
				}
			}

			if (this.jssorSlider == null)
				return;

			qx.ui.core.queue.Widget.add(this, "updateSlider");
		},

		/**
		 * Applies the image to the arrow button.
		 */
		_applyArrowImage: function (value, old, name) {

			if (this.jssorSlider == null)
				return;

			switch (name) {
				case "leftArrowImage":
				case "rightArrowImage":
					break;
			}
		},

		/**
		 * Applies the image to the bullet button.
		 */
		_applyBulletImage: function (value, old, name) {

			if (this.jssorSlider == null)
				return;

		},

		/**
		 * Applies the options map to the slider.
		 */
		_applyOptions: function (value, old, name) {

			if (this.jssorSlider == null)
				return;

			qx.ui.core.queue.Widget.add(this, "updateSlider");

		},

		/**
		 * Schedules the slider to be updated with the new property values.
		 */
		_applyProperty: function (value, old, name) {

			if (this.jssorSlider == null)
				return;

			qx.ui.core.queue.Widget.add(this, "updateSlider");

		},

		syncWidget: function (jobs) {

			this.base(arguments, jobs);

			if (!jobs["updateSlider"])
				return;

			if (this.jssorSlider == null)
				return;

			this.__buildSlider();
		},

		/**
		 * Resize the slider when the widget is resized.
		 */
		__onResize: function (e) {

			if (this.jssorSlider) {

				var size = e.getData();
				this.jssorSlider.$ScaleWidth(size.width);
				this.jssorSlider.$ScaleHeight(size.height);
			}

		},

		/**
		 * Builds the option properties for the specified option type.
		 */
		__buildSliderOptions: function (name) {

			var jssor = {};
			var options = null;

			switch (name) {
				case "arrowOptions":
					options = this.getArrowOptions();
					{
						jssor.$Class = $JssorArrowNavigator$;
						jssor.$ChanceToShow = this.__transformTo$ChanceToShow(options.showMode);
						jssor.$AutoCenter = this.__transformTo$AutoCenter(options.alignment);
						jssor.$Scale = options.scale;
					}
					break;

				case "bulletOptions":
					options = this.getBulletOptions();
					{
						jssor.$Class = $JssorBulletNavigator$;
						jssor.$ChanceToShow = this.__transformTo$ChanceToShow(options.showMode);
						jssor.$AutoCenter = this.__transformTo$AutoCenter(options.alignment);
						jssor.$Orientation = this.__transformTo$Orientation(options.orientation);
						jssor.$Scale = options.scale;
						jssor.$Rows = options.rows || 1;
						jssor.$SpacingX = options.spacing ? options.spacing.x || 0 : 0;
						jssor.$SpacingY = options.spacing ? options.spacing.y || 0 : 0;
					}
					break;

				case "thumbnailOptions":
					options = this.getThumbnailOptions();
					{
						jssor.$Class = $JssorThumbnailNavigator$;
						jssor.$ChanceToShow = this.__transformTo$ChanceToShow(options.showMode);
						jssor.$AutoCenter = this.__transformTo$AutoCenter(options.alignment);
						jssor.$Orientation = this.__transformTo$Orientation(options.orientation);
						jssor.$Scale = options.scale;
						jssor.$Rows = options.rows || 1;
						jssor.$Loop = options.wrap ? 1 : 0;
						jssor.$Columns = options.rows || 1;
						jssor.$SpacingX = options.spacing ? options.spacing.x || 0 : 0;
						jssor.$SpacingY = options.spacing ? options.spacing.y || 0 : 0;
					}
					break;
			}

			return jssor;
		},

		/**
		 * Transforms the showMode enumeration to the
		 * equivalement $ChanceToShow value.
		 */
		__transformTo$ChanceToShow: function (value) {

			switch (value) {
				case "never": return 0;
				case "mouseOver": return 1;
				case "always": return 2;
			}

			return 0;
		},

		/**
		 * Transforms the alignment enumeration to the
		 * equivalement $AutoCenter value.
		 */
		__transformTo$AutoCenter: function (value) {

			switch (value) {
				case "none": return 0;
				case "horizontal": return 1;
				case "vertical": return 2;
				case "both": return 3;
			}

			return 0;
		},

		/**
		 * Transforms the orientation enumeration to the
		 * equivalement $Orientation value.
		 */
		__transformTo$Orientation: function (value) {

			switch (value) {
				case "horizontal": return 1;
				case "vertical": return 2;
			}

			return 1;
		},

		/**
		 * Transforms the wrapMode enumeration to the
		 * equivalement $Loop value.
		 */
		__transformTo$Loop: function (value) {

			switch (value) {
				case "none": return 0;
				case "wrap": return 1;
				case "rewind": return 2;
			}

			return 0;
		},

		/**
		 * Builds the internal html code for the jssor slider, creates the
		 * slider instance and saves a reference to the jssor object.
		 */
		__buildSlider: function () {

			var slideTransitions = [];
			var captionTransitions = [];

			// build the html code for the images including the captions.
			var imagesHtml = [];
			var images = this.getImages();
			for (var i = 0; i < images.length; i++) {

				var image = images[i];

				imagesHtml.push("<div>");
				imagesHtml.push("<img data-u='image' class='image' src='");
				imagesHtml.push(image.source);
				imagesHtml.push("' />");

				// includes the captions, if any.
				var captions = image.captions;
				if (captions && captions.length > 0) {
					for (var j = 0; j < captions.length; j++) {

						var caption = captions[i];
						if (caption.html) {
							imagesHtml.push("<div data-u='caption' class='caption' ");

							// add and collect the transition.
							if (caption.transition) {
								var name = "transition_" + i;
								imagesHtml.push("t='");
								imagesHtml.push(name);
								imagesHtml.push("' ");

								captionTransitions[name] = caption.transition;
							}

							imagesHtml.push("style='position:absolute;");
							if (caption.y != null)
								imagesHtml.push("top:" + caption.y + "px;");
							if (caption.x != null)
								imagesHtml.push("left:" + caption.x + "px;");
							if (caption.width != null)
								imagesHtml.push("width:" + caption.width + "px;");
							if (caption.height != null)
								imagesHtml.push("height:" + caption.height + "px;");
							imagesHtml.push("'>");
							imagesHtml.push(caption.html);
							imagesHtml.push("</div>");
						}
					}
				}

				imagesHtml.push("</div>");

				// collect the transition for the slide.
				slideTransitions.push(image.transition);
			}

			// build the html code for the thumbnail navigation panel.
			var thumbnailsHtml = [
				"<div data-u='thumbnavigator' class='thumbnails' style='position:absolute;",
					"width:", this.getWidth(), "px;",
					"height:", this.getHeight(), "px;'>",

					"<div data-u='slides' class='thumbnail'>",
	            		"<div data-u='prototype' class='prototype'>",
                			"<div class='w'>",
								"<div data-u='thumbnailtemplate' class='t'></div>",
							"</div>",
	               			"<div class='c'></div>",
            			"</div>",
            		"</div>",
				"</div>",
			];

			// build the html code for the bullet navigation panel.
			var bulletsHtml = [
				"<div data-u='navigator' class='bullets' style='position:absolute'>",
					"<div data-u='prototype' class='prototype' style='width:12px;height:12px;'></div>",
				"</div>"
			];

			// build the html code for the navigation arrows;
			var arrowsHtml = [
				"<div class='arrow-left' data-u='arrowleft' style='left: 0px;position:absolute;",
				"' ",
				"src='",
				this.getLeftArrowImage(),
				"'>",
				"</div>",
				"<div class='arrow-right' data-u='arrowRight' style='right: 0px;position:absolute;",
				"' ",
				"src='",
				this.getRightArrowImage(),
				"'>",
				"</div>"
			];

			// build the html code for the jssor slider.
			var html = [
				"<div class='jssor-slider' style='position: absolute;",
					"width:", this.getWidth(), "px;",
					"height:", this.getHeight(), "px;'>",

					// images.
					"<div data-u='slides' style='position: absolute; overflow: hidden; left: 0px; top: 0px; width:100%; height:100%'>",
						imagesHtml.join(""),
					"</div>",

					// navigation arrows.
  					this.isShowArrows() ? arrows.join("") : "",

					// thumbnails.
					this.isShowThumbnails() ? thumbnailsHtml.join("") : "",

					// bullets.
					this.isShowBullets() ? bulletsHtml.join("") : "",

				"</div>"
			];

			// prepare the jssor options.
			var options = {

				$FillMode: null,
				$LazyLoading: true,
				$StartIndex: this.getStartIndex(),
				$AutoPlay: this.getAutoPlay(),
				$Loop: this.__transformTo$Loop(this.getWrapMode()),
				$Idle: this.getIdleTime(),
				$Cols: this.getColumns(),
				$Align: this.getSlideOffset(),
				$SlideDuration: this.getSlideDuration(),
				$SlideSpacing: this.getSlideSpacing(),
				$SlideWidth: this.getSlideSize().width || this.getWidth(),
				$SlideHeight: this.getSlideSize().height || this.getHeight(),
				$PlayOrientation: this.__transformTo$Orientation(this.getOrientation()),
				$ArrowNavigatorOptions: this.__buildSliderOptions("arrowOptions"),
				$BulletNavigatorOptions: this.__buildSliderOptions("bulletOptions"),
				$ThumbnailNavigatorOptions: this.__buildSliderOptions("thumbnailOptions"),

			};

			// create the instance of the slider.
			var dom = this.getContentElement().getDomElement();
			dom.innerHTML = html.join("");
			this.jssorSlider = $JssorSlider$(dom.firstChild, options);

		},
	},

});
