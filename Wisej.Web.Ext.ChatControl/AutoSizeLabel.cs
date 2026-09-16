///////////////////////////////////////////////////////////////////////////////
//
// (C) 2024 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A label that sizes itself on the client.
	/// </summary>
	public class AutoSizeLabel : Label
	{
		#region Client Implementation

		// Client side script that resizes the label when the HTML text contains images.
		//
		// The label measures its HTML text on the client using a hidden element. The
		// browser reports a size of 0x0 for the images that haven't been downloaded yet,
		// making the label too small and cutting off the images.
		//
		// This script preloads the images, writes their size (scaled down to fit the
		// maximum width of the label, when set) back to the <img> tags and re-measures
		// the label. Once the <img> tags declare their size, the measurement is correct
		// regardless of whether the images have been downloaded.
		private const string AutoSizeImagesScript = @"
if (!this.__autoSizeImages) {

	// Returns the maximum width available to the images, 0 when unlimited.
	this.__getImageMaxWidth = function () {

		var maxWidth = this.getMaxWidth();
		if (!maxWidth)
			return 0;

		var insets = this.getInsets();
		return Math.max(0, maxWidth - insets.left - insets.right);
	};

	// Preloads the images in the HTML text and writes back their size.
	this.__autoSizeImages = function () {

		var html = this.getValue();
		if (!html || html.indexOf('<img') === -1)
			return;

		// parse the HTML text to find the images that haven't been measured yet.
		var container = document.createElement('div');
		container.innerHTML = html;

		var images = container.getElementsByTagName('img');
		var pending = [];

		for (var i = 0; i < images.length; i++) {
			if (images[i].getAttribute('data-autosize') !== 'true')
				pending.push(images[i]);
		}

		if (pending.length === 0)
			return;

		var me = this;
		var count = pending.length;
		var maxWidth = this.__getImageMaxWidth();

		// Called when an image has been loaded (or failed to load).
		// Updates the HTML text when all the images have been processed.
		var update = function () {

			if (--count > 0)
				return;

			var changed = false;

			for (var i = 0; i < pending.length; i++) {

				var image = pending[i];
				var size = qx.io.ImageLoader.getSize(image.src);

				// couldn't load the image, leave it alone.
				if (!size || !size.width || !size.height)
					continue;

				// keep the width requested in the HTML text, if any.
				var width = parseInt(image.getAttribute('width'), 10);
				if (!(width > 0))
					width = size.width;

				var height = Math.round(size.height * width / size.width);

				// scale the image down to fit the width of the label.
				if (maxWidth > 0 && width > maxWidth) {
					height = Math.round(height * maxWidth / width);
					width = maxWidth;
				}

				image.setAttribute('width', width);
				image.setAttribute('height', height);
				image.setAttribute('data-autosize', 'true');

				changed = true;
			}

			if (!changed)
				return;

			// update the HTML text and measure the label again now
			// that the size of the images is known.
			me.setValue(container.innerHTML);
			me.invalidateLayoutCache();
			qx.ui.core.queue.Layout.add(me);
		};

		for (var i = 0; i < pending.length; i++)
			qx.io.ImageLoader.load(pending[i].src, update);
	};

	// process the images every time the HTML text changes.
	this.addListener('changeValue', function () {
		this.__autoSizeImages();
	}, this);

	this.__autoSizeImages();
}
";

		#endregion

		private Size _maxSize;

		/// <summary>
		/// Initializes a new instance of <see cref="AutoSizeLabel"/>.
		/// </summary>
		public AutoSizeLabel()
		{
			this.AllowHtml = true;
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			this._maxSize = proposedSize;

			return base.GetPreferredSize(proposedSize);
		}

		/// <summary>
		/// Installs the client side script that resizes the label
		/// when the HTML text contains images.
		/// </summary>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			if (!this.DesignMode)
				Eval(AutoSizeImagesScript);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.height = null;
			config.width = null;

			if (this._maxSize.Width > 0)
				config.maxWidth = this._maxSize.Width;

			if (this._maxSize.Height > 0)
				config.maxHeight = this._maxSize.Height;

			config.wiredEvents.Add("resize(Size)");
		}
	}
}
