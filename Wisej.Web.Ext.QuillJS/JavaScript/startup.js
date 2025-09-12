//# sourceURL=wisej.web.ext.QuillJS.startup.js

/**
 * Initializes the widget.
 */
this.init = function (options) {

	this._initQuill(options);

	var me = this;

	// handle keyboard events
	this.editor.root.addEventListener("keypress", function (e) {

		me.fireEvent("keypress");
	});

	this.container.addEventListener("keydown", function (e) {

		if (e.ctrlKey && e.key === "a") {
			e.preventDefault();
			me.editor.focus();
			me.editor.setSelection(0, me.editor.getLength() - 1);
		}
	});

	this.editor.root.addEventListener("keydown", function (e) {

		// prevent tab from moving focus if editor has focus
		if (e.key === "Tab") {
			e.preventDefault();
			e.stopPropagation();
			this.focus();
		}
	});

	this.editor.root.addEventListener("keyup", function (e) {

		me.fireEvent("keypress");
	});
};

/**
 * Updates the widget.
 */
this.update = function (options) {

	if (options.html != undefined)
		this.setHtml(options.html);
	else if (options.text != undefined)
		this.setText(options.text);
};

this._initQuill = function (options) {

	this.editor = new Quill(this.container, options);

	if (options.html != undefined)
		this.setHtml(options.html);
	else if (options.text != undefined)
		this.setText(options.text);

	var me = this;
	// handle text changes
	this.editor.on("text-change", function () {

		me.fireWidgetEvent("textChanged",
			{
				text: me.getText(),
				html: me.getHtml()
			});
	});

	// handle selection changes
	this.editor.on("selection-change", function (range) {
		if (range) {
			me.fireWidgetEvent("selectionChange", {
				index: range.index,
				length: range.length
			});
		}
	});

	// fire initialization events
	this.fireWidgetEvent("load");

	// fire render event for the designer
	if (wisej.web.DesignMode) {
		this.fireEvent("render");
	}

	// handle container clicks to focus editor
	this.container.addEventListener("click", this._onClick.bind(this));
};

this._onClick = function (e) {

	// handle link clicks
	var link = e.target.closest("a");
	if (link) {

		this.handleLinkClick(link);
		return;
	}

	// force focus for Chrome
	if (/Chrome/.test(navigator.userAgent)) {

		// check if link tooltip is visible
		var linkTooltip = this.container.querySelector(".ql-tooltip.ql-editing");
		if (linkTooltip) {
			return;
		}

		// prevent any default behavior
		e.preventDefault();

		// stop event from bubbling
		e.stopPropagation();

		// force blur and refocus
		this.editor.blur();

		// force focus with a slight delay to ensure blur
		// is processed before focus is set
		setTimeout(() => {

			this.editor.focus();

			// only if clicking the editor area, not toolbar
			if (!e.target.closest(".ql-toolbar")) {

				// get click coordinates
				const bounds = this.editor.root.getBoundingClientRect();
				const relativeX = e.clientX - bounds.left;
				const relativeY = e.clientY - bounds.top;

				// check if click is outside current selection
				const selection = this.editor.getSelection();
				if (selection && selection.length > 0) {
					const range = this.editor.getBounds(selection.index, selection.length);
					// if click is outside the selection bounds, clear it
					if (relativeY < range.top || relativeY > range.bottom ||
						relativeX < range.left || relativeX > range.right) {
						this.editor.setSelection(null);
					}
				}
			}
		}, 25);
	} else {

		// non-Chrome browsers can use normal focus
		this.editor.focus();
	}
}
/**
 * Handles the click event on a link within the editor.
 * Positions the tooltip below the clicked link and fires a linkClick event.
 * @param {HTMLAnchorElement} e The clicked link element.
 */
this.handleLinkClick = function (e) {

	// get link bounds
	const linkBounds = e.getBoundingClientRect();
	const editorBounds = this.editor.root.getBoundingClientRect();
	const tooltip = this.container.querySelector(".ql-tooltip");

	// position tooltip below the link
	const tooltipLeft = linkBounds.left - editorBounds.left;
	const tooltipTop = linkBounds.bottom - editorBounds.top + 5;

	tooltip.style.left = tooltipLeft + "px";
	tooltip.style.top = tooltipTop + "px";
	tooltip.style.transform = "none";

	this.fireWidgetEvent("linkClick", { url: e.href });
};

/**
 * Gets the Delta content
 */
this.getDelta = function () {

	if (this.editor) {
		return this.editor.getContents();
	}
};

/**
 * Sets the Delta content
 */
this.setDelta = function (delta) {

	if (!this.editor) {
		this.addListenerOnce("loaded", () => {
			this.setDelta(delta);
		});
		return;
	}

	if (this.editor && delta) {
		this.editor.setContents(delta);
	}
};

/**
 * Updates the content of the editor with the specified delta.
 * @param {any} delta The delta object representing the changes to apply to the editor content.
 */
this.updateContent = function (delta) {

	if (!this.editor) {
		this.addListenerOnce("loaded", () => {
			this.updateContent(delta);
		});
		return;
	}

	if (this.editor && delta) {
		this.editor.updateContents(delta);
	}
};

/**
 * Gets the current selection
 */
this.getSelection = function () {

	return this.editor ? this.editor.getSelection() : null;
};

/**
 * Sets the selection
 */
this.setSelection = function (index, length, source) {

	if (this.editor) {
		this.editor.setSelection(index, length, source);
	}
};

/**
 * Formats text at current selection. Uses Quill"s format() API which applies formats
 * to the user"s selection. If no selection exists, format will be applied to the cursor.
 * @param {string} format Format name like "bold", "italic", etc.
 * @param {any} value Value of the format
 */
this.format = function (format, value) {

	if (this.editor) {
		this.editor.format(format, value);
	}
};

/**
 * Formats text in the editor.
 * @param {Integer} index The starting position of the text to format.
 * @param {Integer} length The number of characters to format.
 * @param {string} format Format name like "bold", "italic", etc.
 * @param {any} value Value of the format.
 */
this.formatText = function (index, length, format, value) {

	if (this.editor) {
		this.editor.formatText(index, length, format, value);
	}
};

/**
 * Formats all lines in the given range.
 * Has no effect when called with inline formats.
 * @param {Integer} index The starting position of the range to format.
 * @param {Integer} length The number of characters in the range to format.
 * @param {string} format Format name like "bold", "italic", etc.
 * @param {any} value Value of the format.
 */
this.formatLine = function (index, length, format, value) {

	if (this.editor) {
		this.editor.formatLine(index, length, format, value);
	}
};

/**
 * Removes formatting from current selection
 */
this.removeFormat = function () {

	if (this.editor) {
		var range = this.editor.getSelection();
		if (range) {
			this.editor.removeFormat(range.index, range.length);
		}
	}
};

/**
 * Removes formatting from specified range
 */
this.removeFormatRange = function (index, length) {

	if (this.editor) {
		this.editor.removeFormat(index, length);
	}
};

/**
 * Gets the editor instance
 */
this.getEditor = function () {

	return this.editor;
};

/**
 * Sets the editor content from HTML
 */
this.setHtml = function (html) {

	if (this.editor) {
		if (this.getHtml() != html)
			this.editor.root.innerHTML = html;
	}
};

/**
 * Gets the editor content as HTML
 */
this.getHtml = function () {

	return this.editor ? this.editor.root.innerHTML : "";
};

/**
 * Sets the text content
 */
this.setText = function (text) {

	if (this.editor) {
		if (this.getText() != text)
			this.editor.setText(text);
	}
};

/**
 * Gets the text content
 */
this.getText = function () {

	return this.editor ? this.editor.getText() : "";
};

/**
 * Sets the read-only state
 */
this.setReadOnly = function (value) {

	if (this.editor) {
		this.editor.enable(!value);
	}
};

/**
 * Clean up when the widget is destroyed
 */
this._onDestroyed = function () {

	if (this.editor) {
		this.editor = null;
	}
};

/**
 * Focus the editor
 */
this.focus = function () {

	if (this.editor) {
		this.editor.focus();
	}
};

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
