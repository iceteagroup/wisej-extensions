//# sourceURL=wisej.web.ext.Mermaid.startup.js

/**
 * Mermaid widget integration.
 *
 * This function is called when the InitScript property of wisej.web.Widget changes.
 * "this" refers to the container which is a wisej.web.Widget instance.
 */

this.init = function (options) {

	this.options = options;

	this.__applyStyles(options);
	this.__render(options);

	this.addListener("resize", (e) => this.__render(options));
	this.addListener("pointerdown", this.__onElementPointerDown);
};

this.update = function (options) {

	this.options = options;

	this.__applyStyles(options);
	this.__render(options);
};

/**
 * Replace the default implementation not to fire "render"
 * here, but after the ChartJS widget has been fully rendered.
 */
this._onInitialized = function () {

	// do nothing here.
}

// Handle pointer down events on the diagram elements to fire "elementClick" with details about the clicked element and its parents (nodes, edges, etc.). 
// This allows server - side event handlers to react to clicks on specific parts of the diagram.
this.__onElementPointerDown = function (e) {

	const target = e.getOriginalTarget();
	if (!target)
		return;

	// Helper to traverse up the DOM to find a parent with specific attributes or classes
	const findMermaidParent = function (el, maxDepth) {
		let depth = 0;
		let current = el;
		while (current && depth < (maxDepth || 10)) {
			// Look for Mermaid-specific containers (nodes, edges, subgraphs, etc.)
			var classList = current.classList || [];
			for (let i = 0; i < classList.length; i++) {
				const cls = classList[i];
				if (cls && (cls.indexOf('node') === 0 || 
							cls.indexOf('edge') === 0 || 
							cls.indexOf('cluster') === 0 ||
							cls === 'flowchart-link' ||
							cls === 'actor' ||
							cls === 'labelBox')) {
					return { element: current, type: cls };
				}
			}
			current = current.parentElement;
			depth++;
		}
		return null;
	};

	// Extract text content from element
	getTextContent = function (el) {
		if (!el)
			return null;
		var text = el.textContent || el.innerText || el.innerHTML;
		return text ? text.trim() : null;
	};

	// Build data object with element information
	const data = {
		tagName: target.tagName ? target.tagName.toLowerCase() : null,
		text: getTextContent(target),
		id: target.id || null,
		dataId: target.getAttribute ? (target.getAttribute('data-id') || null) : null,
		className: target.className || null
	};

	// Try to find parent Mermaid element (node, edge, etc.)
	const parent = findMermaidParent(target);
	if (parent) {
		data.elementType = parent.type;
		data.parentId = parent.element.id || null;
		data.parentDataId = parent.element.getAttribute ? (parent.element.getAttribute('data-id') || null) : null;
		
		// For edges, try to extract source/target information
		if (parent.type && parent.type.indexOf('edge') === 0) {
			const ariaLabel = parent.element.getAttribute ? parent.element.getAttribute('aria-label') : null;
			if (ariaLabel)
				data.ariaLabel = ariaLabel;
		}

		// If we didn't get text from the clicked element, try the parent
		if (!data.text) {
			const parentText = getTextContent(parent.element);
			if (parentText)
				data.text = parentText;
		}
	}

	// Add standard mouse event arguments
	if (e.getButton) {
		const button = 0;
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

// Applies styles to the container. 
// The options can include padding, overflow, and whether to use max - width for responsive diagrams.
// This allows the server - side code to control the layout and scrolling behavior of the diagram container.
this.__applyStyles = function (options) {

	var padding = options.padding || 16;
	var overflow = options.overflow || "auto";
	var useMaxWidth = options.useMaxWidth !== false;

	this.container.style.overflow = overflow;
	this.container.style.padding = padding + "px";
	this.container.style.maxWidth = useMaxWidth ? "100%" : "";
};

// Render the diagram using the Mermaid library. 
// If Mermaid is not loaded yet, wait for the "load" event and try again.
this.__render = function (options) {

	if (!window.mermaid) {
		this.addListenerOnce("load", () => this.__render(options));
		return;
	}

	try {

		const me = this;
		const container = this.container

		mermaid.initialize(options.config);
		mermaid.render(this.getId() + "_mermaid", options.diagram)
			.then(({ svg }) => {

				container.innerHTML = svg;
				me.fireEvent("render");

				if (me.panzoom) {
					container.removeEventListener("wheel", me.panzoom.zoomWithWheel);
					me.panzoom.destroy();
				}

				//const panzoom = me.panzoom = Panzoom(container.firstChild, {
				//	maxScale: 5,
				//	minScale: 0.3,
				//	contain: "none"
				//});

				//// mouse wheel zooming
				//container.addEventListener("wheel", panzoom.zoomWithWheel);

			}).catch((err) => {

				me.container.innerHTML = err.message;
				me.fireWidgetEvent("mermaidError", { message: err.message, hash: err.hash });
				me.fireEvent("render");
			});
	}
	catch (e) {

	}
};

/**
 * Validates the diagram syntax using Mermaid's internal parser. 
 * If the syntax is invalid, it can throw an error or return validation results. 
 * This allows server-side code to check the diagram before attempting to render it.
 */
this.validate = function (diagram) {

	const me = this;
	const svg = this.container.firstChild;

	return new Promise(async function (resolve, reject) {

		try {
			await mermaid.parse(diagram);
			resolve({ valid: true, error: "" });
		}
		catch(err) {
			resolve({ valid: false, error: err.message });
		}
	});
};

/**
 * Downloads an SVG file containing the rendered Mermaid diagram. 
 * The file name can be specified, and the method will trigger a download of the SVG content as a file on the user's device.
 */
this.downloadSvg = function (fileName) {

	options = options || {};
	const svg = this.container.firstChild;

	var svgText = me.__serializeSvg(svg);
	var blob = new Blob([svgText], { type: "image/svg+xml;charset=utf-8" });
	this.__downloadBlob(blob, fileName || "diagram.svg");
};

/**
 * Returns a promise that resolves to a data URL containing a PNG image of the rendered Mermaid diagram.
 */
this.getImage = function () {

	const options = {};
	const svg = this.container.firstChild;

	// need allowTaint to render svg icons.
	// https://github.com/niklasvh/html2canvas/issues/95
	options.useCORS = true;
	options.allowTaint = true;

	const result = new Promise(function (resolve, reject) {

		// make sure the html2canvas library is loaded.
		wisej.utils.Loader.load([
			{
				id: "html2canvas.js",
				url: "resource.wx/Wisej.Web.Ext.Html2Canvas.JavaScript.Html2Canvas.js?v=" + Wisej.Core.version
			}], function () {

				const rect = svg.getBoundingClientRect()
				svg.setAttribute("width", rect.width);
				svg.setAttribute("height", rect.height);

				html2canvas(dom, options).then(function (canvas) {

					try {
						resolve(canvas.toDataURL());
					}
					catch (error) {
						reject(error);
					}
				});
			});
	});

	return result;
}

/**
 * Downloads a PDF file containing the rendered Mermaid diagram. T
 * he options can include scale for better quality, background color, margin, and image quality for the embedded JPEG.
 */
this.downloadPdf = function (fileName, options) {

	options = options || {};
	const me = this;
	const svg = this.container.firstChild;

	const svgText = this.__serializeSvg(svg);

	// get dimensions from SVG.
	const rect = svgElement.getBoundingClientRect();
	const width = rect ? Math.max(1, Math.ceil(rect.width)) : 800;
	const height = rect ? Math.max(1, Math.ceil(rect.height)) : 600;

	// apply scale factor for better quality (default 2x for high DPI).
	const scale = (typeof options.scale === "number" && options.scale > 0) ? options.scale : 2;
	const scaledWidth = width * scale;
	const scaledHeight = height * scale;

	// create canvas for rasterization.
	const canvas = document.createElement("canvas");
	canvas.width = scaledWidth;
	canvas.height = scaledHeight;
	const ctx = canvas.getContext("2d");

	// fill with background color.
	const bgColor = options.backgroundColor || "#ffffff";
	ctx.fillStyle = bgColor;
	ctx.fillRect(0, 0, scaledWidth, scaledHeight);

	// convert SVG to data URI to avoid CORS/tainted canvas issues.
	const svgDataUri = "data:image/svg+xml;base64," + btoa(decodeURIComponent(encodeURIComponent(svgText)));

	const img = new Image();
	img.onload = function () {
		try {
			ctx.drawImage(img, 0, 0, scaledWidth, scaledHeight);

			// generate PDF with the canvas image.
			me.__generatePdf(canvas, width, height, fileName || "diagram.pdf", options);
		}
		catch (e) {
			console.error("Failed to draw SVG to canvas for PDF export:", e);
		}
	};

	img.onerror = function () {
		console.error("Failed to load SVG for PDF export.");
	};

	img.src = svgDataUri;
};

/**
 * Returns a promise that resolves to a base64-encoded PDF document containing the rendered Mermaid diagram.
 */
this.getPdf = async function (options) {

	options = options || {};
	const me = this;
	const svg = this.container.firstChild;
	const svgText = me.__serializeSvg(svg);

	return new Promise(function (resolve, reject) {

		try {
			// get dimensions from SVG.
			const rect = svg.getBoundingClientRect();
			const width = rect ? Math.max(1, Math.ceil(rect.width)) : 800;
			const height = rect ? Math.max(1, Math.ceil(rect.height)) : 600;

			// apply scale factor for better quality (default 2x for high DPI).
			const scale = options.scale || 2;
			const scaledWidth = width * scale;
			const scaledHeight = height * scale;

			// Create canvas for rasterization.
			const canvas = document.createElement("canvas");
			canvas.width = scaledWidth;
			canvas.height = scaledHeight;
			const ctx = canvas.getContext("2d");

			// Fill with background color.
			const bgColor = options.backgroundColor || "#ffffff";
			ctx.fillStyle = bgColor;
			ctx.fillRect(0, 0, scaledWidth, scaledHeight);

			// Convert SVG to data URI to avoid CORS/tainted canvas issues.
			const svgDataUri = "data:image/svg+xml;base64," + btoa(decodeURIComponent(encodeURIComponent(svgText)));

			const img = new Image();
			img.onload = function () {
				try {
					ctx.drawImage(img, 0, 0, scaledWidth, scaledHeight);

					// PDF dimensions in points (72 points = 1 inch).
					var margin = options.margin || 20;
					var pageWidth = width + (margin * 2);
					var pageHeight = height + (margin * 2);

					// Convert canvas to JPEG for embedding (good compression).
					var quality = options.quality || 0.95;
					var imgData = canvas.toDataURL("image/jpeg", quality);

					// Build a minimal PDF document structure.
					var pdfBytes = me.__buildPdfDocument(imgData, width, height, pageWidth, pageHeight, margin);

					// Convert Uint8Array to base64 for transmission to server.
					var base64 = btoa(String.fromCharCode.apply(null, pdfBytes));
					resolve(base64);
				}
				catch (e) {
					reject(e);
				}
			};

			img.onerror = function () {
				reject(new Error("Failed to load SVG for PDF export"));
			};

			img.src = svgDataUri;
		}
		catch (e) {
			reject(e);
		}
	});
};


// Helper to wait for the SVG element to be rendered before trying to export it.
this.__serializeSvg = function (svgElement) {

	if (!svgElement)
		return null;

	// Clone to avoid mutating the live DOM.
	const svg = svgElement.cloneNode(true);

	// Ensure required namespaces.
	if (!svg.getAttribute("xmlns"))
		svg.setAttribute("xmlns", "http://www.w3.org/2000/svg");
	if (!svg.getAttribute("xmlns:xlink"))
		svg.setAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

	// Ensure width/height to make rasterization deterministic.
	const rect = svgElement.getBoundingClientRect ? svgElement.getBoundingClientRect() : null;
	const width = rect ? Math.max(1, Math.ceil(rect.width)) : null;
	const height = rect ? Math.max(1, Math.ceil(rect.height)) : null;

	if ((!svg.getAttribute("width") || !svg.getAttribute("height")) && svg.getAttribute("viewBox")) {
		var vb = (svg.getAttribute("viewBox") || "").trim().split(/\s+|,/).map(parseFloat);
		if (vb.length === 4 && isFinite(vb[2]) && isFinite(vb[3])) {
			width = width || Math.max(1, Math.ceil(vb[2]));
			height = height || Math.max(1, Math.ceil(vb[3]));
		}
	}

	if (width && height) {
		svg.setAttribute("width", String(width));
		svg.setAttribute("height", String(height));
	}

	try {
		return new XMLSerializer().serializeToString(svg);
	}
	catch (e) {
		return svg.outerHTML || null;
	}
};

// Generates a PDF document containing the diagram image and triggers a download. 
// The PDF is built manually as a minimal structure with the image embedded as a JPEG XObject.
// For more complex documents or better compatibility, consider using a dedicated PDF library.
this.__generatePdf = function (canvas, width, height, fileName, options) {

	const me = this;
	options = options || {};

	// PDF dimensions in points (72 points = 1 inch).
	const margin = (typeof options.margin === "number") ? options.margin : 20;
	const pageWidth = width + (margin * 2);
	const pageHeight = height + (margin * 2);

	// convert canvas to JPEG for embedding (good compression).
	const quality = (typeof options.quality === "number") ? options.quality : 0.95;
	const imgData = canvas.toDataURL("image/jpeg", quality);

	// build a minimal PDF document structure.
	const pdfBytes = me.__buildPdfDocument(imgData, width, height, pageWidth, pageHeight, margin);

	const blob = new Blob([pdfBytes], { type: "application/pdf" });
	me.__downloadBlob(blob, fileName);
};

// Builds a minimal PDF document containing the image. 
// This is a very basic implementation and may not be fully compliant with all PDF viewers, but it should work for simple images.
// For production use, consider using a library like jsPDF or PDFKit for more robust PDF generation.
this.__buildPdfDocument = function (imgDataUrl, imgWidth, imgHeight, pageWidth, pageHeight, margin) {

	// Extract base64 image data.
	const base64Data = imgDataUrl.split(",")[1];
	const binaryData = atob(base64Data);
	const imgBytes = new Uint8Array(binaryData.length);
	for (let i = 0; i < binaryData.length; i++) {
		imgBytes[i] = binaryData.charCodeAt(i);
	}

	// PDF structure - track byte offsets for cross-reference table.
	let offsets = [];
	let content = "";

	var addObject = function (obj) {
		offsets.push(content.length);
		content += obj;
	};

	// PDF Header (version 1.4).
	content = "%PDF-1.4\n%\xE2\xE3\xCF\xD3\n";

	// Object 1: Catalog (root of PDF structure).
	addObject("1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");

	// Object 2: Pages (collection of pages).
	addObject("2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");

	// Object 3: Page (single page with MediaBox and content).
	addObject("3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 " + pageWidth.toFixed(2) + " " + pageHeight.toFixed(2) + "] /Contents 4 0 R /Resources << /XObject << /Im0 5 0 R >> >> >>\nendobj\n");

	// Object 4: Content stream (PDF commands to draw the image).
	const streamContent = "q\n" + imgWidth.toFixed(2) + " 0 0 " + imgHeight.toFixed(2) + " " + margin.toFixed(2) + " " + margin.toFixed(2) + " cm\n/Im0 Do\nQ\n";
	addObject("4 0 obj\n<< /Length " + streamContent.length + " >>\nstream\n" + streamContent + "endstream\nendobj\n");

	// Object 5: Image XObject (JPEG image with DCT encoding).
	const imgObjHeader = "5 0 obj\n<< /Type /XObject /Subtype /Image /Width " + Math.round(imgWidth * 2) + " /Height " + Math.round(imgHeight * 2) + " /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length " + imgBytes.length + " >>\nstream\n";
	const imgObjFooter = "\nendstream\nendobj\n";

	// Calculate byte offset for cross-reference table.
	const xrefOffset = content.length + imgObjHeader.length + imgBytes.length + imgObjFooter.length;

	// Build cross-reference (xref) table.
	let xref = "xref\n0 6\n0000000000 65535 f \n";
	for (let j = 0; j < offsets.length; j++) {
		const offset = String(offsets[j]);
		xref += "0000000000".substring(0, 10 - offset.length) + offset + " 00000 n \n";
	}

	// Trailer (points to catalog and xref location).
	const trailer = "trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n" + xrefOffset + "\n%%EOF\n";

	// Assemble final PDF as byte array.
	const encoder = new TextEncoder();
	const headerBytes = encoder.encode(content + imgObjHeader);
	const footerBytes = encoder.encode(imgObjFooter + xref + trailer);

	const totalLength = headerBytes.length + imgBytes.length + footerBytes.length;
	const pdfBytes = new Uint8Array(totalLength);
	pdfBytes.set(headerBytes, 0);
	pdfBytes.set(imgBytes, headerBytes.length);
	pdfBytes.set(footerBytes, headerBytes.length + imgBytes.length);

	return pdfBytes;
};

// Triggers a download of the given Blob with the specified file name. 
// This is used to download the generated PDF file containing the diagram.
// It creates a temporary URL for the Blob and simulates a click on a hidden link to start the download, then cleans up the URL and link element afterward.
this.__downloadBlob = function (blob) {
	// Create a temporary URL for the Blob
	const url = URL.createObjectURL(blob);

	// Create a temporary <a> to trigger download
	const a = document.createElement("a");
	a.href = url;
	a.download = fileName;
	document.body.appendChild(a);
	a.click();

	// Clean up
	document.body.removeChild(a);
	URL.revokeObjectURL(url);
};