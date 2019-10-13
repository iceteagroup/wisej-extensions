//# sourceURL=wisej.web.ext.CKEditor.startup.js

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
	var id = this.getId() + "_ckeditor";

	// create the dom child.
	this.container.innerHTML = "<textarea id=\"" + id + "\"></textarea>";

	// create the CKEditor instance using the options map generated on the server.
	var options = $options;
	var config = options.config;
	config.resize_enabled = false;
	config.removePlugins = config.removePlugins || "";
	CKEDITOR_BASEPATH = options.basePath;

	// hide the toolbar.
	if (!options.showToolbar)
		config.removePlugins += ",toolbar";

	// hide the status bar.
	if (!options.showFooter)
		config.removePlugins += ",elementspath";

	// add the list of fonts, if defined.
	if (options.fonts && options.fonts.length > 0)
		config.font_names = options.fonts.join(";");

	// allow the assignment of HTML code to the editor by default.
	if (config.allowedContent == null)
		config.allowedContent = true;

	// register the external plugins, if any.
	if (options.externalPlugins)
		this.__registerPlugins(options.externalPlugins);

	// destroy the previous instance, tinyMCE cannot be altered after creation.
	if (this.editor) {

		this.editor.destroy();
		this.editor = null;

	}
	else {

		// perform stuff that has to be done only once.

		// resize the tinyMCE editor when the widget is resized.
		this.addListener("resize", function (e) {
			this.__resizeEditor();
		}, this);

		// add the text property to the state variables returned to the server with any event.
		this.setStateProperties(this.getStateProperties().concat(["text"]));
	}

	// create the editor instance.
	me.editor = CKEDITOR.replace(id, config);

	// complete the initialization once the editor is fully loaded.
	me.editor.once("instanceReady", function (e) {

		// resize it to fit the container.
		me.__resizeEditor();

		// inform the server widget that the editor is ready.
		me.fireWidgetEvent("load");

		// inform the designer that we are ready to be rendered.
		if (wisej.web.DesignMode) {
			me.fireEvent("render");
		}

		me.fireEvent("initialized");
	});

	// mark the widget as "dirty" when it loses the focus in order to send back the content with the state.
	me.editor.on("blur", function () {
		me.setDirty(true);
	});
	me.editor.on("change", function () {
		me.setDirty(true);
	});

	// focus the wrapper wisej widget.
	// IFrame editors cannot propagate pointer events to their container.
	me.editor.on('focus', function (e) {
		me.fireWidgetEvent("focus");
	});

	// fire the "command" event on the server, when the users presses a toolbar button.
	me.editor.on("afterCommandExec", function (e) {

		// skip "applyFormatting", it's generated too often.
		if (e.data && e.data.name != "applyFormatting") {
			var name = e.data.name;
			me.fireWidgetEvent("command", e.data.name);
		}
	});

}

/**
 * Text property.
 *
 * Returns or sets the html text in the editable area.
 */
this.getText = function () {
	try {
		return this.editor.getData();
	} catch (e) { }
}
this.setText = function (value) {
	try {
		this.editor.setData(value);
		this.updateState();
	} catch (e) { }
}

/**
 * Replace the default implementation not to fire "render"
 * here, but after the editor has been rendered in the "loaded" event handler.
 */
this._onInitialized = function () {

	// do nothing here.
}

/**
 * Clean up when the widget is destroyed.
 */
this._onDestroyed = function () {

	if (this.editor) {
		this.editor.destroy();
		this.editor = null;
	}
}

// Resizes tinyMCE to fit the widget container.
this.__resizeEditor = function (second_pass) {

	var me = this;
	var insets = me.getInsets();
	this.editor.resize(me.getWidth() - insets.left - insets.right, me.getHeight(), false);
}

// Registers the external plugins with the CKEditor library.
this.__registerPlugins = function (plugins) {

	if (plugins.length > 0) {
		for (var i = 0; i < plugins.length; i++) {
			var p = plugins[i];
			if (p && p.name && p.url && p.fileName) {

				var url = p.url;
				if (!qx.lang.String.endsWith(url, "/"))
					url += "/";

				var url = qx.util.Uri.getAbsolute(url);
				CKEDITOR.plugins.addExternal(p.name, url, p.fileName);
			}
		}
	}
}

/**
 * Executes commands to manipulate the contents of the editable region.
 *
 * @param command {String} The name of the command to execute.
 * @param argument {String} For commands which require an input argument, this is it.
 */
this.execCommand = function (command, argument) {

	if (!this.editor) {
		this.addListenerOnce("initialized", function (e) {
			this.execCommand(command, argument);
		});
		return;
	}

	try {

		this.editor.execCommand(command, argument);

		// update the text property on the server with the resulting HTML.
		// it's a delayed notification since the app may be executing a series of commands.
		var me = this;
		clearTimeout(this.__changeTextTimeout);
		this.__changeTextTimeout = setTimeout(function () {
			me.fireWidgetEvent("changeText", me.editor.getData());
		}, 10);

	} catch (e) { }
}

/**
 * Enables or disables a command in the toolbar.
 *
 * @param name {String | Array} name of the command to enable/disable.
 * @param enable {Boolean} true to enable or false to disable the command.
 */
this.enableCommand = function (name, enable) {

	if (!this.editor) {
		this.addListenerOnce("initialized", function (e) {
			this.enableCommand(name, enable);
		});
		return;
	}

	if (name instanceof Array) {
		var names = name;
		for (var i = 0; i < names.length; i++) {

			var command = this.editor.commands[names[i]];
			if (command && command.enable && command.disable) {
				enable ? command.enable() : command.disable();
			}
		}
	}
	else {
		var command = this.editor.commands[name];
		if (command && command.enable && command.disable) {
			enable ? command.enable() : command.disable();
		}
	}
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
		this.editor.focus();
}

/**
 * Focus this widget.
 */
this.focus = function () {
	if (this.editor)
		this.editor.focus();
}