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
this.init = function () {

	var id = this.getId() + "_canvas";

	// prepare the configuration map.
	// [$]options is a placeholder that is replaced with the options
	// map configured in Wisej.Web.Ext.CoolClock.
	var options = $options;
	options.canvasId = id;

	// create the canvas dom child.
	this.container.innerHTML = "<canvas id=\"" + id + "\"/>";

	this.coolClock = new CoolClock(options);
}