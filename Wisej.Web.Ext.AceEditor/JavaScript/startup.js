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

	this.editor = this.widget = ace.edit(this.container, options);

	if (options.text)
		this.editor.setValue(options.text);

	this.editor.clearSelection();

	this.widget.on("blur", function () {
		me.fireWidgetEvent("change", me.editor.getValue());
	});

	this.widget.on("change", function () {

		// don't schedule an update for text applied from the server.
		if (me.__updatingText)
			return;

		clearTimeout(me.updateTimer);

		var delay = me.autoUpdateDelay;
		if (delay) {
			me.updateTimer = setTimeout(function () {
				me.fireWidgetEvent("change", me.editor.getValue());
			}, delay);
		}
	});

	this.addListener("resize", function (e) {
		me.widget.resize();
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

		var text = options.text;
		delete options.text;

		this.widget.setOptions(options);

		if (text != null && this.widget.getValue() != text) {

			// replace the full document instead of using setValue() +
			// clearSelection(), which parks the caret at the end of the
			// document and resets the scroll position (QA-2706). a full-range
			// replace goes through the document as a regular edit: the undo
			// stack is preserved and identical text is a no-op.
			var editor = this.editor;
			var session = editor.getSession();
			var focused = editor.isFocused();
			var cursor = editor.getCursorPosition();
			var scrollTop = session.getScrollTop();
			var scrollLeft = session.getScrollLeft();

			this.__updatingText = true;
			try {
				var Range = ace.require("ace/range").Range;
				var lastRow = session.getLength() - 1;
				session.replace(new Range(0, 0, lastRow, session.getLine(lastRow).length), text);
			} finally {
				this.__updatingText = false;
			}

			if (focused) {
				// restore the caret and scroll position (clipped to the
				// new document) when the user is editing.
				editor.moveCursorToPosition(cursor);
				editor.clearSelection();
				session.setScrollTop(scrollTop);
				session.setScrollLeft(scrollLeft);
			}
			else {
				editor.moveCursorToPosition({ row: 0, column: 0 });
				editor.clearSelection();
				session.setScrollTop(0);
				session.setScrollLeft(0);
			}
		}
	}
}
