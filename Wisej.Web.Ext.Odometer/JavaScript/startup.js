/**
 * Initialize the odometer wisej widget where
 * the variable name "widget" refers to the container QX widget
 * and "widget.container" refers to the actual dom element.
 */

var options = $options;

// create the inner odometer element.
if (widget.odometer == null) {
	var id = widget.getId() + "_odometer";
	widget.container.innerHTML = "<div id=\"" + id + "\"></div>";

	// create the odometer using the options map generated on the server.
	options.el = widget.container.firstChild;
	widget.odometer = new Odometer(options);
	
	// force the odometer not to wrap.
	options.el.style.whiteSpace = "nowrap";

	// fire our "odometerDone" event.
	qx.bom.Event.addNativeListener(options.el, "odometerdone", function () {
		widget.fireEvent("odometerDone");
	});
}
else {

	// update the existing odometer.
	widget.odometer.update(options.value);

}

// use the size property in the options to set the font size.
if (options.fontSize)
	widget.odometer.el.style.fontSize = options.fontSize + "px";