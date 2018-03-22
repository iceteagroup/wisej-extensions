//# sourceURL=wisej.web.ext.TinyMCE.startup.js

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
	var id = this.getId() + "_tinymce";

	// create the dom child.
	this.container.innerHTML = "<textarea id=\"" + id + "\"></textarea>";

	// create the tinyMCE instance using the options map generated on the server.
	var options = $options;
	var config = options.config;

	config.selector = "#" + id;
	config.resize = false;

	if (!options.showToolbar)
		config.toolbar = false;
	if (!options.showFooter)
		config.statusbar = false;
	if (!options.showMenubar)
		config.menubar = false;

	// register the external plugins, if any.
	if (options.externalPlugins)
		this.__registerPlugins(options.externalPlugins);

	// destroy all instance when in design mode, otherwise the library holds on the previously created instance.
	if (wisej.web.DesignMode) {
		tinymce.remove();
	}

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

		// focus the editor when his widget is focused.
		this.addListener("focusin", function (e) {
			if (this.editor)
				this.editor.focus();
		});

		// add the text property to the state variables returned to the server with any event.
		this.setStateProperties(this.getStateProperties().concat(["text"]));
	}

	// create the editor instance.
	tinymce.init(config).then(function (editors) {

		me.editor = editors[0];

		me.__resizeEditor();

		// inform the server widget that the editor is ready.
		me.fireWidgetEvent("load");

		me.editor.once('init', function (e) {
			me.fireEvent("initialized");
		});

		// mark the widget as "dirty" when it loses the focus in order to send back the content with the state.
		// fire the "command" event on the server, when the users presses a toolbar button.
		me.editor.on('blur', function (e) {
			me.setDirty(true);
		});
		me.editor.on('change', function (e) {
			me.setDirty(true);
		});

		// focus the wrapper wisej widget.
		me.editor.on('focus', function (e) {
			me.fireWidgetEvent("focus");
		});

		// fire the "command" event on the server, when the users presses a toolbar button.
		me.editor.on('ExecCommand', function (e) {
			me.fireWidgetEvent("command", e.command);
		});

		// inform the designer that we are ready to be rendered.
		if (wisej.web.DesignMode) {
			me.fireEvent("render");
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
		return this.editor.getContent();
	} catch (e) { }
}
this.setText = function (value) {
	try {
		this.editor.setContent(value);
		this.updateState();
	} catch (e) { }
}

/**
 * Replace the default implementation not to fire "render"
 * here, but after the tinyMCE editor has been rendered in the promise call (then).
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
	setTimeout(function () {

		if (me.editor) {
			var id = me.getId() + "_tinymce";
			var insets = me.getInsets();
			var hgap = me.editor.getContainer().offsetHeight - tinyMCE.DOM.get(id + '_ifr').offsetHeight;
			me.editor.theme.resizeTo(me.getWidth() - insets.left - insets.right - 2, me.getHeight() - hgap);

			if (!second_pass)
				me.__resizeEditor(true);
		}

	}, 1);
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
				url += p.fileName;

				var url = qx.util.Uri.getAbsolute(url);
				tinymce.PluginManager.load(p.name, url);
			}
		}
	}
}

/**
 * Executes commands to manipulate the contents of the editable region.
 *
 * @param command {String} The name of the command to execute.
 * @param showDefaultUI {Boolean} Indicates whether the default user interface should be shown.
 * @param argument {String} For commands which require an input argument, this is it.
 */
this.execCommand = function (command, showDefaultUI, argument) {

	if (!this.editor) {
		this.addListenerOnce("initialized", function (e) {
			this.execCommand(command, showDefaultUI, argument);
		});
		return;
	}

	try {

		this.editor.execCommand(command, showDefaultUI, argument);

		// update the text property on the server with the resulting HTML.
		// it's a delayed notification since the app may be executing a series of commands.
		var me = this;
		clearTimeout(this.__changeTextTimeout);
		this.__changeTextTimeout = setTimeout(function () {
			me.fireWidgetEvent("changeText", me.editor.getContent());
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

			var command = this.__findCommand(names[i]);
			if (command && command.disabled) {
				command.disabled(!enable);
			}
		}
	}
	else {
		var command = this.__findCommand(name);
		if (command && command.disabled) {
			command.disabled(!enable);
		}
	}
}

this.__findCommand = function (name) {

	var items = this.editor.theme.panel.find("*");
	for (var i = 0, l = items.length; i < l; i++) {
		var item = items[i];
		if (item && item.settings && (item.settings.cmd == name || item.settings.text == name))
			return item;
	}
	return null;
}