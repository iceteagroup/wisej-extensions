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
		private Size _maxSize;

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
			this._maxSize = proposedSize;

			return base.GetPreferredSize(proposedSize);
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
