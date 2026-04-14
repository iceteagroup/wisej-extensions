//# sourceURL=wisej.web.ext.Mermaid.startup.js

/**
 * Mermaid widget integration.
 *
 * This function is called when the InitScript property of wisej.web.Widget changes.
 * "this" refers to the container which is a wisej.web.Widget instance.
 */

this.init = function (options) {

  this.options = options;

  this.__render();

  // this.addListener("resize", (e) => this.__render(options));

  this.addListener("pointerdown", this.__onElementPointerDown);
};

this.update = function (options) {

  this.options = options;
  this.__render();
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

  var target = e.getOriginalTarget();
  if (!target)
	return;

  // Helper to traverse up the DOM to find a parent with specific attributes or classes
  var findMermaidParent = function (el, maxDepth) {
	let depth = 0;
	let current = el;
	while (current && depth < (maxDepth || 10)) {
	  // Look for Mermaid-specific containers (nodes, edges, subgraphs, etc.)
	  var classList = current.classList || [];
	  for (let i = 0; i < classList.length; i++) {
		var cls = classList[i];
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
  var data = {
	tagName: target.tagName ? target.tagName.toLowerCase() : null,
	text: getTextContent(target),
	id: target.id || null,
	dataId: target.getAttribute ? (target.getAttribute('data-id') || null) : null,
	className: target.className || null
  };

  // Try to find parent Mermaid element (node, edge, etc.)
  var parent = findMermaidParent(target);
  if (parent) {
	data.elementType = parent.type;
	data.parentId = parent.element.id || null;
	data.parentDataId = parent.element.getAttribute ? (parent.element.getAttribute('data-id') || null) : null;

	// For edges, try to extract source/target information
	if (parent.type && parent.type.indexOf('edge') === 0) {
	  var ariaLabel = parent.element.getAttribute ? parent.element.getAttribute('aria-label') : null;
	  if (ariaLabel)
		data.ariaLabel = ariaLabel;
	}

	// If we didn't get text from the clicked element, try the parent
	if (!data.text) {
	  var parentText = getTextContent(parent.element);
	  if (parentText)
		data.text = parentText;
	}
  }

  // Add standard mouse event arguments
  if (e.getButton) {
	var button = 0;
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

// Render the diagram using the Mermaid library. 
// If Mermaid is not loaded yet, wait for the "load" event and try again.
this.__render = function () {

  if (!this.options || !this.options.diagram)
	return;

  if (!window.mermaid) {
	this.addListenerOnce("load", () => this.__render());
	return;
  }

  try {

	var me = this;
	var container = this.container

	// Clear the container to force a fresh render
	container.innerHTML = '';

	// Initialize Mermaid with current optionss
	mermaid.initialize(this.options);

	// Use a unique ID for each render to avoid Mermaid's cache
	var renderId = this.getId() + "_mermaid_" + Date.now();

	mermaid.render(renderId, this.options.diagram)
	  .then(({ svg, bindFunctions }) => {

		container.innerHTML = svg;
		if (bindFunctions) bindFunctions(container);

		me.fireEvent("render");

		if (me.options.enablePanZoom) {

		  var scale = (me.options.zoomLevel || 1.0);
		  var svgElement = container.querySelector('svg');
		  this.__enablePanZoom(svgElement, scale, 0.01, 0.02, 2);
		}

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

  var me = this;
  var svg = this.container.firstChild;

  return new Promise(async function (resolve, reject) {

	try {
	  await mermaid.parse(diagram);
	  resolve({ valid: true, error: "" });
	}
	catch (err) {
	  resolve({ valid: false, error: err.message });
	}
  });
};

/**
 * Downloads an SVG file containing the rendered Mermaid diagram. 
 * The file name can be specified, and the method will trigger a download of the SVG content as a file on the user's device.
 */
this.downloadSvg = function (fileName) {

  var svg = this.container.firstChild;
  var me = this;

  var svgText = me.__serializeSvg(svg);
  var blob = new Blob([svgText], { type: "image/svg+xml;charset=utf-8" });
  this.__downloadBlob(blob, fileName);
};

/**
 * Returns a promise that resolves to a data URL containing a PNG image of the rendered Mermaid diagram.
 */
this.getImage = function () {

  var options = {};
  var element = this.container;
  var me = this;

  // need allowTaint to render svg icons.
  // https://github.com/niklasvh/html2canvas/issues/95
  options.useCORS = true;
  options.allowTaint = true;

  var result = new Promise(function (resolve, reject) {

	// Store current pan/zoom values
	var savedZoom = me.__tvZoom;
	var savedPanX = me.__tvPanX;
	var savedPanY = me.__tvPanY;

	// Reset pan/zoom to default for clean export
	var svg = element.firstChild;
	if (svg && savedZoom !== undefined) {
	  me.__tvZoom = 1;
	  me.__tvPanX = 0;
	  me.__tvPanY = 0;
	  me.__tvApplyTransform();
	}

	// make sure the html2canvas library is loaded.
	wisej.utils.Loader.load([
	  {
		id: "html2canvas.js",
		url: "resource.wx/Wisej.Web.Ext.Html2Canvas.JavaScript.Html2Canvas.js?v=" + Wisej.Core.version
	  }], function () {

		var svg = element.querySelector("svg");
		var rect = svg.getBoundingClientRect()
		svg.setAttribute("width", rect.width);
		svg.setAttribute("height", rect.height);

		html2canvas(element, options).then(function (canvas) {

		  try {
			var dataUrl = canvas.toDataURL("image/png");

			// Restore previous pan/zoom values
			if (savedZoom !== undefined) {
			  me.__tvZoom = savedZoom;
			  me.__tvPanX = savedPanX;
			  me.__tvPanY = savedPanY;
			  me.__tvApplyTransform();
			}

			resolve(dataUrl);
		  }
		  catch (error) {
			// Restore previous pan/zoom values even on error
			if (savedZoom !== undefined) {
			  me.__tvZoom = savedZoom;
			  me.__tvPanX = savedPanX;
			  me.__tvPanY = savedPanY;
			  me.__tvApplyTransform();
			}
			reject(error);
		  }
		}).catch(function (error) {
		  // Restore previous pan/zoom values on canvas error
		  if (savedZoom !== undefined) {
			me.__tvZoom = savedZoom;
			me.__tvPanX = savedPanX;
			me.__tvPanY = savedPanY;
			me.__tvApplyTransform();
		  }
		  reject(error);
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
  var me = this;
  var svg = this.container.firstChild;

  var svgText = this.__serializeSvg(svg);

  // get dimensions from SVG.
  var rect = svg.getBoundingClientRect();
  var width = rect ? Math.max(1, Math.ceil(rect.width)) : 800;
  var height = rect ? Math.max(1, Math.ceil(rect.height)) : 600;

  // apply scale factor for better quality (default 2x for high DPI).
  var scale = (typeof options.scale === "number" && options.scale > 0) ? options.scale : 2;
  var scaledWidth = width * scale;
  var scaledHeight = height * scale;

  // create canvas for rasterization.
  var canvas = document.createElement("canvas");
  canvas.width = scaledWidth;
  canvas.height = scaledHeight;
  var ctx = canvas.getContext("2d");

  // fill with background color.
  var bgColor = options.backgroundColor || "#ffffff";
  ctx.fillStyle = bgColor;
  ctx.fillRect(0, 0, scaledWidth, scaledHeight);

  // convert SVG to data URI to avoid CORS/tainted canvas issues.
  var svgDataUri = "data:image/svg+xml;base64," + btoa(decodeURIComponent(encodeURIComponent(svgText)));

  var img = new Image();
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
  var me = this;
  var svg = this.container.firstChild;
  var svgText = me.__serializeSvg(svg);

  return new Promise(function (resolve, reject) {

	try {
	  // get dimensions from SVG.
	  var rect = svg.getBoundingClientRect();
	  var width = rect ? Math.max(1, Math.ceil(rect.width)) : 800;
	  var height = rect ? Math.max(1, Math.ceil(rect.height)) : 600;

	  // apply scale factor for better quality (default 2x for high DPI).
	  var scale = options.scale || 2;
	  var scaledWidth = width * scale;
	  var scaledHeight = height * scale;

	  // Create canvas for rasterization.
	  var canvas = document.createElement("canvas");
	  canvas.width = scaledWidth;
	  canvas.height = scaledHeight;
	  var ctx = canvas.getContext("2d");

	  // Fill with background color.
	  var bgColor = options.backgroundColor || "#ffffff";
	  ctx.fillStyle = bgColor;
	  ctx.fillRect(0, 0, scaledWidth, scaledHeight);

	  // Convert SVG to data URI to avoid CORS/tainted canvas issues.
	  var svgDataUri = "data:image/svg+xml;base64," + btoa(decodeURIComponent(encodeURIComponent(svgText)));

	  var img = new Image();
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
  var svg = svgElement.cloneNode(true);

  // Ensure required namespaces.
  if (!svg.getAttribute("xmlns"))
	svg.setAttribute("xmlns", "http://www.w3.org/2000/svg");
  if (!svg.getAttribute("xmlns:xlink"))
	svg.setAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");

  // Ensure width/height to make rasterization deterministic.
  var rect = svgElement.getBoundingClientRect ? svgElement.getBoundingClientRect() : null;
  var width = rect ? Math.max(1, Math.ceil(rect.width)) : null;
  var height = rect ? Math.max(1, Math.ceil(rect.height)) : null;

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

  var me = this;
  options = options || {};

  // PDF dimensions in points (72 points = 1 inch).
  var margin = (typeof options.margin === "number") ? options.margin : 20;
  var pageWidth = width + (margin * 2);
  var pageHeight = height + (margin * 2);

  // convert canvas to JPEG for embedding (good compression).
  var quality = (typeof options.quality === "number") ? options.quality : 0.95;
  var imgData = canvas.toDataURL("image/jpeg", quality);

  // build a minimal PDF document structure.
  var pdfBytes = me.__buildPdfDocument(imgData, width, height, pageWidth, pageHeight, margin);

  var blob = new Blob([pdfBytes], { type: "application/pdf" });
  me.__downloadBlob(blob, fileName);
};

// Builds a minimal PDF document containing the image. 
// This is a very basic implementation and may not be fully compliant with all PDF viewers, but it should work for simple images.
// For production use, consider using a library like jsPDF or PDFKit for more robust PDF generation.
this.__buildPdfDocument = function (imgDataUrl, imgWidth, imgHeight, pageWidth, pageHeight, margin) {

  // Extract base64 image data.
  var base64Data = imgDataUrl.split(",")[1];
  var binaryData = atob(base64Data);
  var imgBytes = new Uint8Array(binaryData.length);
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
  var streamContent = "q\n" + imgWidth.toFixed(2) + " 0 0 " + imgHeight.toFixed(2) + " " + margin.toFixed(2) + " " + margin.toFixed(2) + " cm\n/Im0 Do\nQ\n";
  addObject("4 0 obj\n<< /Length " + streamContent.length + " >>\nstream\n" + streamContent + "endstream\nendobj\n");

  // Object 5: Image XObject (JPEG image with DCT encoding).
  var imgObjHeader = "5 0 obj\n<< /Type /XObject /Subtype /Image /Width " + Math.round(imgWidth * 2) + " /Height " + Math.round(imgHeight * 2) + " /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length " + imgBytes.length + " >>\nstream\n";
  var imgObjFooter = "\nendstream\nendobj\n";

  // Calculate byte offset for cross-reference table.
  var xrefOffset = content.length + imgObjHeader.length + imgBytes.length + imgObjFooter.length;

  // Build cross-reference (xref) table.
  let xref = "xref\n0 6\n0000000000 65535 f \n";
  for (let j = 0; j < offsets.length; j++) {
	var offset = String(offsets[j]);
	xref += "0000000000".substring(0, 10 - offset.length) + offset + " 00000 n \n";
  }

  // Trailer (points to catalog and xref location).
  var trailer = "trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n" + xrefOffset + "\n%%EOF\n";

  // Assemble final PDF as byte array.
  var encoder = new TextEncoder();
  var headerBytes = encoder.encode(content + imgObjHeader);
  var footerBytes = encoder.encode(imgObjFooter + xref + trailer);

  var totalLength = headerBytes.length + imgBytes.length + footerBytes.length;
  var pdfBytes = new Uint8Array(totalLength);
  pdfBytes.set(headerBytes, 0);
  pdfBytes.set(imgBytes, headerBytes.length);
  pdfBytes.set(footerBytes, headerBytes.length + imgBytes.length);

  return pdfBytes;
};

// Triggers a download of the given Blob with the specified file name. 
// This is used to download the generated PDF file containing the diagram.
// It creates a temporary URL for the Blob and simulates a click on a hidden link to start the download, then cleans up the URL and link element afterward.
this.__downloadBlob = function (blob, fileName) {
  // Create a temporary URL for the Blob
  var url = URL.createObjectURL(blob);

  // Create a temporary <a> to trigger download
  var a = document.createElement("a");
  a.href = url;
  a.download = fileName;
  document.body.appendChild(a);
  a.click();

  // Clean up
  document.body.removeChild(a);
  URL.revokeObjectURL(url);
};

/**
 * Mermaid pan and zoom integration.
 */
this.__enablePanZoom = function (svg, scale, zoomStep, minZoom, maxZoom) {

  var ctx = this;
  var currentSvg = svg;
  var panSurface = ctx.container;

  ctx.__tvZoom = scale;
  ctx.__tvPanX = 0;
  ctx.__tvPanY = 0;

  ctx.__tvApplyTransform = function () {
	currentSvg.style.transformOrigin = '0 0';
	currentSvg.style.transform = 'translate(' + ctx.__tvPanX + 'px,' + ctx.__tvPanY + 'px) scale(' + ctx.__tvZoom + ')';
	currentSvg.style.maxWidth = 'none';
	currentSvg.style.height = 'auto';
	return true;
  };

  if (!panSurface.__tvPanBound) {
	panSurface.__tvPanBound = true;
	panSurface.style.cursor = 'grab';
	panSurface.style.userSelect = 'none';
	var dragging = false;
	var lastX = 0;
	var lastY = 0;
	panSurface.addEventListener('mousedown', function (e) {

	  if (e.button !== 0) return;
	  dragging = true;
	  lastX = e.clientX;
	  lastY = e.clientY;
	  panSurface.style.cursor = 'grabbing';
	  e.preventDefault();

	});
	window.addEventListener('mousemove', function (e) {

	  if (!dragging) return;
	  var dx = e.clientX - lastX;
	  var dy = e.clientY - lastY;
	  lastX = e.clientX;
	  lastY = e.clientY;
	  ctx.__tvPanX += dx;
	  ctx.__tvPanY += dy;
	  ctx.__tvApplyTransform();

	});
	window.addEventListener('mouseup', function () {

	  if (!dragging) return;
	  dragging = false;
	  panSurface.style.cursor = 'grab';

	});
	panSurface.addEventListener('mouseleave', function () {

	  if (dragging) panSurface.style.cursor = 'grabbing';

	});
	panSurface.addEventListener('wheel', function (e) {
	  if (!e) return;
	  var delta = (typeof e.deltaY === 'number') ? e.deltaY : 0;
	  if (delta === 0) return;
	  e.preventDefault();
	  var next = ctx.__tvZoom + (delta < 0 ? zoomStep : -zoomStep);
	  if (next < minZoom) next = minZoom;
	  if (next > maxZoom) next = maxZoom;
	  ctx.__tvZoom = next;
	  ctx.fireWidgetEvent('zoom', ctx.__tvZoom);
	  ctx.__tvApplyTransform();
	});
  }

  ctx.__tvApplyTransform();
}
