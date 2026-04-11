var __wisejImageOriginalApplySource = qx.ui.basic.Image.prototype._applySource;

qx.Mixin.define("wisej.web.qx.ui.basic.Imagepatch", {

	statics : {
		__svgCache : {}
	},

	members : {

		__svgReqId : 0,

		_extractColorFromSource : function(source) {

			if (!source)
				return null;

			var queryColorIndex = source.indexOf("?color=");
			if (queryColorIndex !== -1)
				return source.substring(queryColorIndex + 7);

			var hashIndex = source.indexOf("#");
			if (hashIndex !== -1)
				return source.substring(hashIndex);

			return null;
		},

		_stripColorFromSource : function(source) {

			if (!source)
				return source;

			var hashIndex = source.indexOf("#");
			if (hashIndex !== -1)
				source = source.substring(0, hashIndex);

			return source;
		},

		_applySource : function(value, old) {

			this.__svgReqId++;

			if (!value || value.indexOf(".svg") === -1) {
				__wisejImageOriginalApplySource.call(this, value, old);
				this.__forceUpdate();
				return;
			}

			var reqId = this.__svgReqId;
			var source = this._stripColorFromSource(value);
			var originalSource = value;

			var color = this._extractColorFromSource(value);
			if (!color && this.getTextColor()) {
				var rgb = qx.util.ColorUtil.stringToRgb(this.getTextColor());
				color = qx.util.ColorUtil.rgbToHexString(rgb);
			}

			var cacheKey = source + "|" + (color || "");
			var cached = wisej.web.qx.ui.basic.Imagepatch.__svgCache[cacheKey];

			if (cached) {
				this.setSource(cached);
				this.__forceUpdate();
				return;
			}

			var me = this;
			Snap.load(source, function(svg) {

				if (reqId !== me.__svgReqId || me.isDisposed())
					return;

				if (me.getSource() !== originalSource)
					return;

				if (!svg || !svg.node) {
					__wisejImageOriginalApplySource.call(me, source, old);
					me.__forceUpdate();
					return;
				}

				var node = svg.node;
				node?.setAttribute("width", "100%");
				node?.setAttribute("height", "100%");
				node?.setAttribute("preserveAspectRatio", "xMidYMid meet");

				if (color)
					node?.style.color = color;

				var dataUri = "data:image/svg+xml;charset=utf-8," + encodeURIComponent(node?.outerHTML);
				wisej.web.qx.ui.basic.Imagepatch.__svgCache[cacheKey] = dataUri;

				if (reqId !== me.__svgReqId || me.isDisposed())
					return;

				if (me.getSource() !== originalSource)
					return;

				me.setSource(dataUri);
				me.__forceUpdate();
			});
		},

		__forceUpdate : function() {

			if (this.isDisposed())
				return;

			this.invalidateLayoutCache();
			qx.ui.core.queue.Layout.add(this);
			qx.ui.core.queue.Widget.add(this);

		}
	}
});

qx.Class.patch(qx.ui.basic.Image, wisej.web.qx.ui.basic.Imagepatch);