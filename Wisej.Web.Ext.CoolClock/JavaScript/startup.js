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

	this.setSelectable(false);

	// stop the previous clock (if any) before re-creating it on the same canvas id,
	// otherwise its timer keeps running and redraws the new clock.
	if (this.coolClock)
		this.coolClock.stop();

	var id = this.getId() + "_canvas";

	options.canvasId = id;

	// create the canvas dom child.
	// display:block removes the line-box gap an inline canvas leaves below itself,
	// and margin:0 auto centers it horizontally.
	this.container.innerHTML = "<canvas id=\"" + id + "\" style=\"display:block;margin:0 auto\"></canvas>";

	this.coolClock = this.widget = new CoolClock(options);
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