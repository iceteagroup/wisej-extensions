//# sourceURL=wisej.web.ext.Mermaid.startup.js

/**
 * Mermaid widget integration.
 *
 * This function is called when the InitScript property of wisej.web.Widget changes.
 * "this" refers to the container which is a wisej.web.Widget instance.
 */

this.init = function (options) {

	options = options || this.options || {};

	// Create/refresh the inner element.
	this.container.innerHTML = '<pre class="mermaid" style="margin:0;"></pre>';
	this.__target = this.container.firstElementChild;

	// One-time resize handler.
	if (!this.__mermaidResizeHooked) {
		this.__mermaidResizeHooked = true;
		this.addListener("resize", this.__onResize);
	}

	this.__applyUi(options);
	this.__applyDiagram(options);

	if (options.autoRender !== false && this.__diagram) {
		this.__scheduleRender(options);
	}
};

this.update = function (options) {

	options = options || this.options || {};

	var uiChanged = this.__applyUi(options);
	var diagramChanged = this.__applyDiagram(options);
	var configChanged = this.__applyConfigKey(options);

	if (options.autoRender === false)
		return;

	if (diagramChanged || configChanged || uiChanged) {
		if (this.__diagram)
			this.__scheduleRender(options);
	}
};

this._onDestroyed = function () {

	if (this.__renderTimer) {
		clearTimeout(this.__renderTimer);
		this.__renderTimer = null;
	}
	if (this.__debounceTimer) {
		clearTimeout(this.__debounceTimer);
		this.__debounceTimer = null;
	}
};

this.__onResize = function (e) {

	var size = e.getData();
	if (!size)
		return;

	// Prevent resize storms.
	var w = size.width | 0;
	var h = size.height | 0;
	if (this.__lastW === w && this.__lastH === h)
		return;

	this.__lastW = w;
	this.__lastH = h;

	if (this.__diagram && (this.options || {}).autoRender !== false) {
		this.__scheduleRender(this.options || {});
	}
};

this.__applyUi = function (options) {

	var padding = (typeof options.padding === "number") ? options.padding : 16;
	var overflow = (typeof options.overflow === "string") ? options.overflow : "auto";
	var useMaxWidth = options.useMaxWidth !== false;

	var changed = (
		this.__padding !== padding ||
		this.__overflow !== overflow ||
		this.__useMaxWidth !== useMaxWidth
	);

	this.__padding = padding;
	this.__overflow = overflow;
	this.__useMaxWidth = useMaxWidth;

	if (this.container)
		this.container.style.overflow = overflow;

	if (this.__target) {
		this.__target.style.padding = padding + "px";
		this.__target.style.maxWidth = useMaxWidth ? "100%" : "";
	}

	return changed;
};

this.__applyDiagram = function (options) {

	var diagramRaw = (typeof options.diagram === "string") ? options.diagram : "";
	var diagram = diagramRaw.trim();

	if (diagram === this.__diagram)
		return false;

	this.__diagram = diagram;

	if (this.__target)
		this.__target.textContent = diagram;

	// force re-process on next render.
	this.__renderedDiagram = null;
	return true;
};

this.__applyConfigKey = function (options) {

	var rawConfig = (options && typeof options.config === "object" && options.config) ? options.config : {};
	var config = this.__clone(rawConfig);
	config.startOnLoad = false;

	var key = JSON.stringify(config);
	if (key === this.__configKey)
		return false;

	this.__configKey = key;
	return true;
};

this.__scheduleRender = function (options) {

	var me = this;

	if (this.__renderTimer) {
		clearTimeout(this.__renderTimer);
		this.__renderTimer = null;
	}

	var debounce = (typeof options.renderDebounceMs === "number") ? options.renderDebounceMs : 0;
	if (debounce > 0) {
		if (this.__debounceTimer)
			clearTimeout(this.__debounceTimer);

		this.__debounceTimer = setTimeout(function () {
			me.__debounceTimer = null;
			me.__scheduleRender(Object.assign({}, options, { renderDebounceMs: 0 }));
		}, debounce);
		return;
	}

	this.__renderTimer = setTimeout(function () {
		me.__renderTimer = null;
		me.__renderOnce(options);
	}, 0);
};

this.__renderOnce = function (options) {

	var me = this;

	if (!this.__target || !this.__diagram)
		return;

	// Mermaid not loaded yet, retry.
	if (!window.mermaid) {
		this.__renderTimer = setTimeout(function () {
			me.__renderTimer = null;
			me.__renderOnce(options);
		}, 50);
		return;
	}

	// Avoid re-rendering the same content unless required.
	if (this.__renderedDiagram === this.__diagram && this.__renderedConfigKey === this.__configKey)
		return;

	try {
		var rawConfig = (options && typeof options.config === "object" && options.config) ? options.config : {};
		var config = this.__clone(rawConfig);
		config.startOnLoad = false;

		// Re-initialize only when config changed.
		if (this.__renderedConfigKey !== this.__configKey) {
			mermaid.initialize(config);
		}

		this.__target.removeAttribute("data-processed");

		var done = function () {
			me.__renderedDiagram = me.__diagram;
			me.__renderedConfigKey = me.__configKey;
			if (wisej && wisej.web && wisej.web.DesignMode) {
				me.fireEvent("render");
			}
		};

		if (mermaid.run) {
			var p = mermaid.run({ nodes: [this.__target] });
			if (p && typeof p.then === "function")
				p.then(done).catch(function () { });
			else
				done();
		} else if (mermaid.init) {
			mermaid.init(undefined, this.__target);
			done();
		}
	}
	catch (e) {
		// ignore render errors.
	}
};

this.__clone = function (obj) {

	if (!obj)
		return {};

	// Mermaid expects plain data - JSON clone is sufficient.
	return JSON.parse(JSON.stringify(obj));
};

