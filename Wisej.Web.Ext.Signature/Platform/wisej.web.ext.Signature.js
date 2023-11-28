///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.web.ext.Signature
 *
 * The Signature control can be used to draw on a canvas, import, and export signatures.
 */
qx.Class.define("wisej.web.ext.Signature", {

	extend: wisej.web.Canvas,

	construct: function () {

		this.base(arguments);

		this.addListener("tap", this._onTap);
		this.addListener("pointerdown", this._onPointerDown);
		this.getApplicationRoot().addListener("pointerup", this._onPointerUp, this);
	},

	properties:	{

		/**
		 * The canvas line color.
		 */
		lineColor : { init: "#000000", check: "String" },

		/**
		 * The canvas line width.
		 */
		lineWidth: { init: 1, check: "Number" },

		/**
		 * Read only.
		 */
		readOnly: { init: false, check: "Boolean" }
	},

	members: {

		// canvas context.
		_context: null,

		// redo image list.
		_redoList: [],

		// undo image list.
		_undoList: [],

		_isDrawing: false,

		_getContext: function () {

			if (!this._context) {
				var domElement = this.getContentElement().getDomElement();
				this._context = domElement.getContext("2d");
			}

			return this._context;
		},

		_onTap: function (e) {

			if (this.getReadOnly()) {
				return;
			}

			var context = this._getContext();
			var offset = this._getTargetOffset(e);

			context.beginPath();

			context.fillStyle = this.getLineColor();

			context.arc(offset.x, offset.y, this.getLineWidth(), 0, 2 * Math.PI);
			context.fill();

			this.fireWidgetEvent("signatureChange");
		},

		_onPointerDown: function (e) {

			if (this.getReadOnly() || this._isDrawing) {
				return;
			}

			this._isDrawing = true;

			var context = this._getContext();
			var offset = this._getTargetOffset(e);

			this._saveState();

			context.beginPath();

			context.moveTo(offset.x, offset.y);

			this.addListener("pointermove", this._onPointerMove);
		},

		_onPointerMove: function (e) {
			this._paint(e);
		},

		_onPointerUp: function (e) {

			if (this._isDrawing) {

				this._isDrawing = false;

				this.removeListener("pointermove", this._onPointerMove);
				this._paint(e);

				this.fireWidgetEvent("signatureChange");
			}
		},

		_getTargetOffset: function (e) {

			var target = e.getTarget();
			var location = target.getContentLocation();

			var offsetY = e.getDocumentTop() - location.top;
			var offsetX = e.getDocumentLeft() - location.left;
			
			return { x: offsetX, y: offsetY }
		},

		_getDataUrl: function () {
			return this.getContentElement().getDomElement().toDataURL()
		},

		_paint: function (e) {

			var context = this._getContext();
			var offset = this._getTargetOffset(e);

			context.lineTo(offset.x, offset.y);

			// apply styles.
			context.lineWidth = this.getLineWidth();
			context.strokeStyle = this.getLineColor();

			context.stroke();
		},

		/**
		 * Clears the canvas.
		 */
		clear: function () {

			var context = this._getContext();

			context.clearRect(0, 0, this.getWidth(), this.getHeight());

			this.fireWidgetEvent("signatureChange");
		},

		/**
		 * Gets the image base64 string.
		 * @returns image base64 string.
		 */
		getImage: function () {

			var imageUrl = this._getDataUrl();

			return imageUrl.replace("data:image/png;base64,", "");
		},

		/**
		 * 
		 * @returns
		 */
		isEmpty: function () {
			var context = this._getContext();
			var data = context.getImageData(0, 0, this.getWidth(), this.getHeight()).data;

			return !data.some(channel => channel !== 0);
		},

		/**
		 * Loads an image with the given url into the canvas.
		 * @param {any} base64Url The image url.
		 */
		loadImage: function (base64Url) {

			var me = this;
			var image = new Image();

			image.onload = function () {

				var context = me._getContext();

				context.drawImage(image, 0, 0);
			}

			image.src = base64Url
		},

		// -------------- History Implementation --------------

		canRedo: function () {
			return this._redoList.length > 0;
		},

		canUndo: function () {
			return this._undoList.length > 0;
		},

		// removes the last operation from the canvas.
		undo: function () {
			if (this._undoList.length > 0) {
				this._restoreState(this._undoList, this._redoList);

				this.fireWidgetEvent("signatureChange");
			}
		},

		// appends the last operation to the canvas.
		redo: function () {
			if (this._redoList.length > 0) {
				this._restoreState(this._redoList, this.undoList);

				this.fireWidgetEvent("signatureChange");
			}
		},

		_restoreState: function (pop, push) {

			if (pop.length) {

				this._saveState(push, true);
				var restoreState = pop.pop();

				this.clear();
				this.loadImage(restoreState);
			}
		},

		_saveState: function (list, keepRedo) {

			keepRedo = keepRedo || false;

			if (!keepRedo) {
				this._redoList = [];
			}

			(list || this._undoList).push(this._getDataUrl());
		},
	}
});