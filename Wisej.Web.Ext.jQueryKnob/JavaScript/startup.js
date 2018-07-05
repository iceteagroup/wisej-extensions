//# sourceURL=wisej.web.ext.jQueryKnob.startup.js

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

	var me = this;

	// prepare the configuration map.
	// [$]options is a placeholder that is replaced with the options
	// map configured in Wisej.Web.Ext.jQueryKnob.
	var options = $options;

	// convert fonts and colors from Wisej maps to
	// the appropriate field in the options map.
	this.__setFontAndColors(options);

	// center the inner knob horizontally.
	this.container.style.textAlign = "center";

	// create the jQueryKnob widget.
	if (this.knob == null) {

		// create the inner input control.
		var el = this.container;
		var id = this.getId() + "_dial";
		el.innerHTML = "<input type\"text\" style:\"display:none\" id=\"" + id + "\" />";

		// save a reference to knob widget.
		this.knob = $("#" + id);

		// fire our events.
		options.change = function (value) {
			me.fireWidgetEvent("valueChanged", Math.round(value));
		};

		// initialize the jQuery widget.
		this.knob.knob(options);
		this.knob.val(options.value);
		this.knob.trigger("change");

		// autoresize it to fill the widget container.
		this.addListener("resize", function (e) {

			var size = e.getData();
			setTimeout(function () {
				size = Math.min(size.width, size.height);
				me.knob.trigger("configure", { height: size, width: size });
			}, 1);
		});
	}
	else {

		this.knob.trigger("configure", options);
		this.knob.val(options.value);
		this.knob.trigger("change");

	}
}

/**
 * Resolves themed fonts and colors.
 */
this.__setFontAndColors = function (options) {

	// translate themed fonts.
	var fontMgr = qx.theme.manager.Font.getInstance();
	var font = fontMgr.resolve(options.font);
	options.font = font.getFamily().join(",");
	options.fontWeight = font.isBold() ? "bold" : "normal";

	// translate themed colors.
	var colorMgr = qx.theme.manager.Color.getInstance();
	options.fgColor = colorMgr.resolve(options.fgColor);
	options.bgColor = colorMgr.resolve(options.bgColor);
}


