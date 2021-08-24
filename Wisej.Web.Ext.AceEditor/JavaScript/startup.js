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

	ace.config.set("basePath", "resource.wx/Wisej.Web.Ext.AceEditor/");

	if (this.widget)
		this.widget.destroy();

	var editor = this.widget = ace.edit(this.container, options);

	this.widget.on("blur", function () {
		me.fireWidgetEvent("blur", editor.getValue());
	});
}

/**
 * Updates the widget when the options object changes.
 *
 * @param options {Map} Options map (optional).
 * @param old {Map} Previous options map (optional).
 */
this.update = function (options, old) {

	if (this.widget)
		this.widget.setOptions(options);
}
