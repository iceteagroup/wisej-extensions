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

			if (!value || value.indexOf("data:") === 0 || value.indexOf(".svg") === -1) {
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
			var me = this;

			if (cached) {
				if (cached instanceof Array) {
					// Currently loading, append to the callbacks
					cached.push(function(uri) {
						if (me.isDisposed() || me.getSource() !== originalSource || reqId !== me.__svgReqId)
							return;
						
						if (uri) {
							me.setSource(uri);
						} else {
							__wisejImageOriginalApplySource.call(me, originalSource, old);
						}
						me.__forceUpdate();
					});
					return;
				} else if (cached === "failed") {
					__wisejImageOriginalApplySource.call(this, originalSource, old);
					this.__forceUpdate();
					return;
				} else {
					this.setSource(cached);
					this.__forceUpdate();
					return;
				}
			}

			// Mark as loading by setting it into an array to push callbacks onto
			wisej.web.qx.ui.basic.Imagepatch.__svgCache[cacheKey] = [];

			var fetchSource = source;
			if (qx.util.AliasManager && qx.util.AliasManager.getInstance) {
				fetchSource = qx.util.AliasManager.getInstance().resolve(fetchSource);
			}
			if (qx.util.ResourceManager && qx.util.ResourceManager.getInstance) {
				if (qx.util.ResourceManager.getInstance().has(fetchSource)) {
					fetchSource = qx.util.ResourceManager.getInstance().toUri(fetchSource);
				}
			}

			Snap.load(fetchSource, function(svg) {

				var callbacks = wisej.web.qx.ui.basic.Imagepatch.__svgCache[cacheKey];
				var finish = function(uri) {
					wisej.web.qx.ui.basic.Imagepatch.__svgCache[cacheKey] = uri || "failed";

					if (!me.isDisposed() && me.getSource() === originalSource && reqId === me.__svgReqId) {
						if (uri) {
							me.setSource(uri);
						} else {
							__wisejImageOriginalApplySource.call(me, originalSource, old);
						}
						me.__forceUpdate();
					}

					if (callbacks && callbacks.length) {
						for (var i = 0; i < callbacks.length; i++) {
							callbacks[i](uri);
						}
					}
				};

				if (!svg) {
					finish(null);
					return;
				}

				var node = svg.node || svg;

				if (node) {
					if (node.nodeType === 11) { // Node.DOCUMENT_FRAGMENT_NODE
						node = node.querySelector("svg") || node.firstElementChild || node.firstChild;
					} else if (node.nodeName && node.nodeName.toLowerCase() !== "svg") {
						node = node.querySelector("svg") || node;
					}
				}

				if (!node || !node.nodeName || node.nodeName.toLowerCase() !== "svg" || typeof node.setAttribute !== "function") {
					finish(null);
					return;
				}

				try {
					if (!node.getAttribute("xmlns")) {
						node.setAttribute("xmlns", "http://www.w3.org/2000/svg");
					}

					node.setAttribute("width", "100%");
					node.setAttribute("height", "100%");
					node.setAttribute("preserveAspectRatio", "xMidYMid meet");

					if (color)
						node.style.color = color;

					var svgContent = node.outerHTML || new XMLSerializer().serializeToString(node);

					if (color) {
						svgContent = svgContent.replace(/currentColor/gi, color);
					}

					var dataUri = "data:image/svg+xml;charset=utf-8," + encodeURIComponent(svgContent);
					finish(dataUri);
				} catch (e) {
					finish(null);
				}
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