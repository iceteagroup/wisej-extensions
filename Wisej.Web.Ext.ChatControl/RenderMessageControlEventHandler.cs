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

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Represents the method that will handle the <see cref="ChatBox.RenderMessageControl"/> event, fired when a
	/// <see cref="Message"/> needs a control to be displayed in the <see cref="ChatBox"/>.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An instance of <see cref="RenderMessageControlEventArgs"/> containing the message that requires a control.</param>
	/// <remarks>
	/// Assign <see cref="RenderMessageControlEventArgs.Control"/> to supply a custom control. When no control is
	/// supplied, the message is displayed using an <see cref="AutoSizeLabel"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chatBox.RenderMessageControl += (object sender, RenderMessageControlEventArgs e) =>
	/// {
	///     if (e.Message.ContentType == "image")
	///         e.Control = new PictureBox { ImageSource = e.Message.Content, Size = new Size(200, 150) };
	/// };
	/// ]]></code>
	/// </example>
	public delegate void RenderMessageControlEventHandler(object sender, RenderMessageControlEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="ChatBox.RenderMessageControl"/> event.
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// private void chatBox_RenderMessageControl(object sender, RenderMessageControlEventArgs e)
	/// {
	///     if (e.Message.ContentType == "link")
	///         e.Control = new LinkLabel { Text = e.Message.Content, AutoSize = true };
	/// }
	/// ]]></code>
	/// </example>
	public class RenderMessageControlEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new instance of <see cref="RenderMessageControlEventArgs"/> with the given <see cref="ChatControl.Message"/>.
		/// </summary>
		/// <param name="message">The <see cref="ChatControl.Message"/> that requires a control.</param>
		/// <example>
		/// <code><![CDATA[
		/// var args = new RenderMessageControlEventArgs(new Message("Hello"));
		/// args.Control = new Label { Text = args.Message.Content };
		/// ]]></code>
		/// </example>
		public RenderMessageControlEventArgs(Message message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Returns the <see cref="ChatControl.Message"/> that is requesting a control.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_RenderMessageControl(object sender, RenderMessageControlEventArgs e)
		/// {
		///     var text = e.Message.Content;
		///     e.Control = new Label { Text = text.ToUpper(), AutoSize = true };
		/// }
		/// ]]></code>
		/// </example>
		public Message Message { get; }

		/// <summary>
		/// Returns or sets the <see cref="Web.Control"/> to use to display the message.
		/// </summary>
		/// <value>The custom control for the message; the default is <c>null</c>, in which case an
		/// <see cref="AutoSizeLabel"/> displaying <see cref="ChatControl.Message.Content"/> is used.</value>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_RenderMessageControl(object sender, RenderMessageControlEventArgs e)
		/// {
		///     if (e.Message.ContentType == "button")
		///         e.Control = new Button { Text = e.Message.Content };
		/// }
		/// ]]></code>
		/// </example>
		public Control Control { get; set; }
	}
}