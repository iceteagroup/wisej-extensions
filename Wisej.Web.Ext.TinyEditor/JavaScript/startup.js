//# sourceURL=wisej.web.ext.TinyEditor.startup.js

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

	// get the unique editor id.
	// replace - with _ to make it a valid identified when used
	// directly.
	var id = this.getId() + "_tinyeditor";

	// create the dom child.
	this.container.innerHTML = "<textarea id=\"" + id + "\"></textarea>";

	// create the tinyeditor instance using the options map generated on the server.
	var options = $options;

	options.id = id;
	options.width = "100%";
	options.height = "100%";
	options.cssclass = 'tinyeditor';
	options.controlclass = 'tinyeditor-control';
	options.rowclass = 'tinyeditor-header';
	options.dividerclass = 'tinyeditor-divider';
	options.xhtml = true;
	options.bodyid = 'editor';
	options.footerclass = 'tinyeditor-footer';
	options.toggle = { cssclass: 'toggle' };

	// if the tinyEditor has not been created, perform stuff that has to be done only once.
	if (!this.editor) {

		// resize the tinyeditor when the widget is resized.
		this.addListener("resize", function (e) {

			if (this.editor) {

				this.editor.i.height =
					this.editor.t.style.height =
						(this.getHeight() - gap) + "px";
			}

		}, this);

		// preload the editor's images to display them in the designer.
		qx.io.ImageLoader.load("resource.wx/Wisej.Web.Ext.TinyEditor/icons.png");
		qx.io.ImageLoader.load("resource.wx/Wisej.Web.Ext.TinyEditor/header-bg.gif");

		// add the text property to the state variables returned to the server with any event.
		this.setStateProperties(this.getStateProperties().concat(["text"]));
	}

	// create the tiny editor instance.
	this.editor = new TINY.editor.edit(id, options);

	// save the gap to calculate the new iframe size when resizing.
	var i = this.editor.i;
	// var insets = this.getInsets();
	var gap = i.parentNode.parentNode.offsetHeight - i.offsetHeight;
	this.editor.i.height =
		this.editor.t.style.height =
			(this.getHeight() - gap) + "px";


	var savedText = this.getText();

	// hookup the blur event in the child iframe to fire onEditorBlur in the owner window.
	if (!wisej.web.DesignMode) {

		this.editor.e.body.addEventListener("blur", function () {

			var newText = me.getText();
			if (savedText != newText) {
				me.setDirty(true);
				savedText = newText;
			}

		});

		this.editor.e.body.addEventListener("focus", function () {
			me.fireWidgetEvent("focus");
		});
	}

	// inform the server widget that the editor is ready.
	this.fireWidgetEvent("load");
	this.fireEvent("initialized");
}

/**
 * Text property.
 *
 * Returns or sets the html text in the editable area.
 */
this.getText = function () {
	try {
		return this.editor.e.body.innerHTML;
	} catch (e) { }
}
this.setText = function (value) {
	try {
		if (this.editor) {
			this.editor.e.body.innerHTML = value;
			this.updateState();
		} else {

			var me = this;
			this.addListenerOnce("initialized", function () {
				me.setText(value);
			});
		}
		
	} catch (e) { }
}

/**
 * Executes commands to manipulate the contents of the editable region.
 *
 * @param command {String} The name of the command to execute.
 * @param showDefaultUI {Boolean} Indicates whether the default user interface should be shown.
 * @param argument {String} For commands which require an input argument, this is it.
 */
this.execCommand = function (command, showDefaultUI, argument) {

	try {

		this.editor.e.execCommand(command, showDefaultUI || false, argument);

		// update the text property on the server with the resulting HTML.
		// it's a delayed notification since the app may be executing a series of commands.
		var me = this;
		clearTimeout(this.__changeTextTimeout);
		this.__changeTextTimeout = setTimeout(function () {
			me.fireWidgetEvent("changeText", me.getText());
		}, 10);

	} catch (e) { }
}

/**
 * Focus this widget when using the keyboard. This is
 * mainly thought for the advanced qooxdoo keyboard handling
 * and should not be used by the application developer.
 *
 * @internal
 */
this.tabFocus = function () {
	if (this.editor)
		this.editor.e.body.focus();
}

/**
 * Focus this widget.
 */
this.focus = function () {
	if (this.editor)
		this.editor.e.body.focus();
}

/**
 * Sets the widget to be ReadOnly.
 */
this.setEditable = function (editable) {
	if (this.editor)
		if (this.editor.e.body)
			this.editor.e.body.setAttribute('contenteditable', editable);
}