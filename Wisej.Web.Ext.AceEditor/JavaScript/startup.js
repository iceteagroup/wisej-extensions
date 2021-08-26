//# sourceURL=wisej.web.ext.AceEditor.startup.js

/**
 * Initializes the widget.
 *
 * This function is called when the InitScript property of
 * wisej.web.Widget changes.
 *
 * 'this' refers to the container which is a wisej.web.Widget instance.
 *
 * 'this.container' refers to the DOM element and can be used to initialize
 * the third party javascript widget.
 *
 * @param options {Map} Options map (optional).
 */
this.init = function (options) {

	var me = this;

	// store the auto update delay.
	this.autoUpdateDelay = options.autoUpdateDelay;
	delete options.autoUpdateDelay;

	ace.config.set("basePath", "resource.wx/Wisej.Web.Ext.AceEditor/");

	if (this.widget)
		this.widget.destroy();

	var editor = this.widget = ace.edit(this.container, options);

	this.widget.on("blur", function () {
		me.fireWidgetEvent("change", editor.getValue());
	});

	this.widget.on("change", function () {

		clearTimeout(me.updateTimer);

		var delay = me.autoUpdateDelay;
		if (delay) {
			me.updateTimer = setTimeout(function () {
				me.fireWidgetEvent("change", editor.getValue());
			}, delay);
		}
	});

	this.addListener("resize", function (e) {
		me.editor.resize();
	});
}

/**
 * Updates the widget when the options object changes.
 *
 * @param options {Map} Options map (optional).
 * @param old {Map} Previous options map (optional).
 */
this.update = function (options, old) {

	// update the auto update delay.
	if (options.autoUpdateDelay !== undefined) {
		this.autoUpdateDelay = options.autoUpdateDelay;
		delete options.autoUpdateDelay;
	}

	if (this.widget) {

		this.widget.setOptions(options);
	}
}
