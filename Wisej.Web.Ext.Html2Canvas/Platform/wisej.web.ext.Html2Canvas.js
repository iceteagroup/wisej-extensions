///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

/**
 * wisej.web.ext.Html2Canvas
 *
 * This component uses the html2canvas library (http://html2canvas.hertzen.com)
 * to take a screenshot of a widget and send it back to the server.
 */
qx.Class.define("wisej.web.ext.Html2Canvas", {

	extend: qx.core.Object,
	type: "singleton",

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	members: {

		/**
		 * Renders the HTMl of the widget into an image and sends
		 * it to the server component.
		 *
		 * @param widget {Widget?} The widget to capture. If null, it captures the entire body.
		 * @param options {Map?} Set of options, defined here http://html2canvas.hertzen.com/configuration.
		 */
		screenshot: function (widget, options) {

			var dom = document.body;
			if (widget)
				dom = wisej.utils.Widget.ensureDomElement(widget);

			options = options || {};

			// need allowTaint to render svg icons.
			// https://github.com/niklasvh/html2canvas/issues/95
			options.useCORS = true;

			var result = new Promise(function(resolve, reject) {

				// make sure the html2canvas library is loaded.
				wisej.utils.Loader.load([
					{
						id: "html2canvas.js",
						url: "resource.wx/Wisej.Web.Ext.Html2Canvas.JavaScript.Html2Canvas.js"
					}], function () {

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
	}
});
