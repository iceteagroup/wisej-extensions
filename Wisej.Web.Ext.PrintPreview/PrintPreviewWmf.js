
/**
 * print
 * 
 * Creates a temporary IFRAME with one IMG element for each page to print.
 * 
 * @param {any} callbackUrl URL used to retrieve the page image.
 */

this.print = function (callbackUrl) {

	// show the loader while preparing for printing.
	this.showLoader();

	// create the print iframe.
	var iframe = document.createElement("iframe");
	document.body.appendChild(iframe);

	var me = this;
	var imageLoadedCallback = this.__onImageLoaded.bind(this, iframe);

	// need to work with the iframe in a timeout or Firefox doesn't work.
	setTimeout(function () {

		// hide the browser's footer and header (may not work in IE).
		var iframeStyle = iframe.contentDocument.createElement("style");
		iframeStyle.innerHTML = "@media print {	@page { margin: 0; }}";
		iframe.contentDocument.head.appendChild(iframeStyle);

		// create the img elements in the printing iframe.
		var pages = me.pages.getChildren();
		var pageImages = [];
		me.__imagesToLoad = pages.length;
		for (var i = 0; i < pages.length; i++) {

			var pageImg = iframe.contentDocument.createElement("img");
			iframe.contentDocument.body.appendChild(pageImg);
			qx.bom.Event.addNativeListener(pageImg, "load", imageLoadedCallback);
			qx.bom.Event.addNativeListener(pageImg, "error", imageLoadedCallback);
			pageImages.push(pageImg);
		}

		// set the "src" after having created the IMG elements and attached 
		// to  the "load" an "error" events, otherwise Firefox may fires the "load" event too soon.
		for (var i = 0; i < pageImages.length; i++) {
			var pageImg = pageImages[i];
			pageImg.setAttribute("src", callbackUrl + "&page=" + i);
			pageImg.style.pageBreakAfter = "always";
		}

	}, 1);
};

// Handles the "load" and "error" event for each "IMG" element
// used to print the pages. When all the pages are loaded or timed out,
// it starts the browser's print process and removes the temporary IFRAME.
this.__onImageLoaded = function (iframe) {

	this.__imagesToLoad--;
	if (this.__imagesToLoad == 0) {

		this.hideLoader();
		qx.ui.core.queue.Manager.flush();

		// remove the TABID_KEY or a refresh will create a new session.
		window.sessionStorage.removeItem(Wisej.Core.TABID_KEY);

		iframe.contentWindow.focus();
		iframe.contentWindow.print();
		document.body.removeChild(iframe);
	}
};
