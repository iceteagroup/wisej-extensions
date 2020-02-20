//# sourceURL=wisej.web.ext.CoolClock.startup.js

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

	var id = this.getId() + "_canvas";

	options.canvasId = id;

	// create the canvas dom child.
	this.container.innerHTML = "<canvas id=\"" + id + "\"/>";

	// center the inner canvas.
	this.container.style.textAlign = "center";

	this.coolClock = new CoolClock(options);
}

/**
 * Called when the options change. It lets the 
 * widget decide whether to update an existing
 * third-party control or to create a new one.
 */
this.update = function (options) {

    // recreate the clock.
    this.init(options);
}