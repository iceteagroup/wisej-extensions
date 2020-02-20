//# sourceURL=wisej.web.ext.Odometer.startup.js

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

	// create the inner odometer element.
	var id = this.getId() + "_odometer";
	this.container.innerHTML = "<div id=\"" + id + "\"></div>";

	// create the odometer using the options map generated on the server.
	options.el = this.container.firstChild;
	this.odometer = new Odometer(options);

	// force the odometer not to wrap.
	options.el.style.whiteSpace = "nowrap";

	// fire our "odometerDone" event.
	var me = this;
	qx.bom.Event.addNativeListener(options.el, "odometerdone", function () {
		me.fireEvent("odometerDone");
	});

	// use the size property in the options to set the font size.
	if (options.fontSize)
		this.odometer.el.style.fontSize = options.fontSize + "px";
}

/**
 * Called when the options change. It lets the 
 * widget decide whether to update an existing
 * third-party control or to create a new one.
 */
this.update = function (options) {

	// update the existing odometer.
	this.odometer.update(options.value);

	// use the size property in the options to set the font size.
	if (options.fontSize)
		this.odometer.el.style.fontSize = options.fontSize + "px";
}