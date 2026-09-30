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

using System;
using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A <see cref="Label"/> that sizes itself on the client to fit its content.
	/// </summary>
	/// <remarks>
	/// <see cref="AutoSizeLabel"/> is the default control used by <see cref="Message"/> to display its
	/// <see cref="Message.Content"/> when no custom control is supplied through the
	/// <see cref="ChatBox.RenderMessageControl"/> event. HTML content is allowed by default
	/// (<see cref="Label.AllowHtml"/> is set to <c>true</c>). The width and height are computed by the browser,
	/// constrained by the maximum size last passed to <see cref="GetPreferredSize(Size)"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var label = new AutoSizeLabel
	/// {
	///     Text = "<b>Hello</b> from the chat!"
	/// };
	/// label.Size = label.GetPreferredSize(new Size(400, 0));
	/// ]]></code>
	/// </example>
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

        // maximum size to send to the client, taken from the constraints
        // received by GetPreferredSize(). 0 = unconstrained.
        private Size _maxSize;

        // size measured by the client, reported by the "resize" event.
        // Size.Empty until the client has measured the label.
        private Size _clientSize;

		/// <summary>
		/// Initializes a new instance of <see cref="AutoSizeLabel"/> with <see cref="Label.AllowHtml"/> enabled.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var label = new AutoSizeLabel();
		/// label.Text = "Welcome to the <i>chat</i>.";
		/// ]]></code>
		/// </example>
		public AutoSizeLabel()
		{
			this.AllowHtml = true;
		}

		/// <summary>
		/// Retrieves the size of a rectangular area into which the label can be fitted and
		/// stores <paramref name="proposedSize"/> as the maximum size used on the client.
		/// </summary>
		/// <param name="proposedSize">The custom-sized area for the label. A <see cref="Size.Width"/> or
		/// <see cref="Size.Height"/> greater than 0 is sent to the client as the maximum width or height;
		/// a value of 0 means unconstrained.</param>
		/// <returns>A <see cref="Size"/> representing the preferred width and height of the label.</returns>
		/// <remarks>
		/// The label's final size is determined by the browser; the returned value is the server-side
		/// estimate computed by the base <see cref="Label"/> implementation.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var label = new AutoSizeLabel { Text = "A long message that may wrap." };
		/// // limit the label to 300 pixels wide, unconstrained height.
		/// label.Size = label.GetPreferredSize(new Size(300, 0));
		/// ]]></code>
		/// </example>
		public override Size GetPreferredSize(Size proposedSize)
		{
			// the layout engines pass 0 or 1 for an unconstrained dimension
			// (see Control.GetPreferredSize). Don't send 1 to the client as
			// maxWidth/maxHeight, it collapses the label to 1 pixel.
			this._maxSize = new Size(
				proposedSize.Width > 1 ? proposedSize.Width : 0,
				proposedSize.Height > 1 ? proposedSize.Height : 0);

			return GetMeasuredSize(proposedSize);
		}

		/// <summary>
		/// Returns the size measured on the client, when available.
		/// </summary>
		/// <remarks>
		/// Read by <see cref="Label"/> with an empty proposed size when the label sizes itself (AutoSize
		/// in a container using the default layout). It doesn't change the constraints sent to the client.
		/// </remarks>
		public override Size PreferredSize
		{
			get { return GetMeasuredSize(Size.Empty); }
		}

		// Returns the size measured on the client, or the size measured
		// on the server when the client hasn't reported a size yet. The server
		// can only measure plain text; it doesn't know about the HTML content
		// (images, paragraphs, wrapping) rendered by this label.
		private Size GetMeasuredSize(Size proposedSize)
		{
			if (!this._clientSize.IsEmpty)
				return this._clientSize;

			return base.GetPreferredSize(proposedSize);
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			// keep the size measured by the client: it's the preferred size of the label
			// and it must survive Label.SetBoundsCore(), which otherwise replaces the
			// size reported by the client with the size measured on the server.
			if (e.Type == "resize")
			{
				dynamic size = e.Parameters.Size;
				if (size != null)
				{
					this._clientSize = new Size(
						Convert.ToInt32(size.width),
						Convert.ToInt32(size.height));
				}
			}

			base.OnWebEvent(e);
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
