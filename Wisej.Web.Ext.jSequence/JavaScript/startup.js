//# sourceURL=wisej.web.ext.jSequence.startup.js

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

	// prepare the configuration map.
	// [$]options is a placeholder that is replaced with the options map.
	var options = $options || {};

	this.container.innerHTML = "";
	var diagram = Diagram.parse(options.uml);
	diagram.drawSVG(this.container, { theme: options.theme || "simple" });

	if (!this.__initialized)
	{
		this.__initialized = true;

		this.addListener("pointerdown", this.__onElementPointerDown);
	}
}

this.__onElementPointerDown = function (e) {

	var target = e.getOriginalTarget();
	if (target) {
		if (target.tagName == "tspan") {

			var data = {
				element: target.innerHTML
			};

			// add standard arguments
			if (e.getButton) {
				var button = 0
				switch (e.getButton()) {
					case "right": button = 2; break;
					case "middle": button = 1; break;
				}
				data.button = button;
			}
			if (e.getDocumentTop) {
				data.y = e.getDocumentTop() | 0;
				data.x = e.getDocumentLeft() | 0;
			}

			this.fireWidgetEvent("elementClick", data);
		}
	}
}

this.getImage = function () {
	debugger;

	var svg = this.container.firstChild;
	if (svg == null)
		return;
	var img = new Image();
	var serializer = new XMLSerializer();
	var svgStr = serializer.serializeToString(svg);
	var me = this;

	return new Promise(function (resolve, reject) {
		img.onload = function () {
			var canvas = document.createElement('canvas');
			var w = me.getWidth();
			var h = me.getHeight();
			canvas.width = w;
			canvas.height = h;
			canvas.getContext('2d').drawImage(img, 0, 0, w, h);
			resolve(canvas.toDataURL("image/png"));
		};
		img.src = "data:image/svg+xml;base64," + window.btoa(svgStr);
	});
}
