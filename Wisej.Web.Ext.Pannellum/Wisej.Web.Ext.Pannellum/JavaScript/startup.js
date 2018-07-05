//# sourceURL=wisej.web.ext.Pannellum.startup.js

/**
 * Initializes the widget.
 *
 * This function is called when the InitScript property of
 * wisej.web.Widget changes.
 *
 * "this" refers to the container which is a wisej.web.Widget instance.
 *
 * The widget has an inner container with id = "container" that can
 * be used referring to this.container.
 *
 */
this.init = function (options) { 

	var me = this;
	options = options || {};

	if (wisej.web.DesignMode) {
		options.autoLoad = false;
	}

	if (this.viewer)
		this.viewer.destroy();

	// resize when the widget is resized.
	if (this.viewer == null) {
		this.addListener("resize", function (e) {
			if (me.viewer && me.viewer.getRenderer())
				setTimeout(me.viewer.getRenderer().resize, 10);
		});
	}

	// add hotspot click handler.
	if (options.hotSpots) {
		var handler = this._onHotSpotClick.bind(this);
		for (var i = 0; i < options.hotSpots.length; i++) {
			var hs = options.hotSpots[i];
			hs.clickHandlerArgs = qx.lang.Object.clone(hs);
			hs.clickHandlerFunc = handler;
		}
	}

	this.viewer = pannellum.viewer(this.container, options);
	this.viewer.on("load", function () { me.fireWidgetEvent("load"); });
	this.viewer.on("scenechange", function (id) { me.fireWidgetEvent("scenechange", id); });
	this.viewer.on("error", function (error) { me.fireWidgetEvent("error", error); });
	this.viewer.on("errorcleared", function () { me.fireWidgetEvent("errorcleared"); });

	if (wisej.web.DesignMode) {
		var loadBtn = this.container.querySelector(".pnlm-load-button");
		if (loadBtn)
			loadBtn.parentElement.removeChild(loadBtn);
	}

};

/**
 * Click handler for the hotspots.
 */
this._onHotSpotClick = function (e, hotspot) {

	this.fireWidgetEvent("hotspot", hotspot);
}

/**
 * Called when the options change. It lets the 
 * widget decide whether to update an existing
 * third-party control or to create a new one.
 */
this.update = function (options) {

	// recreate the viewer.
	this.init(options);
}