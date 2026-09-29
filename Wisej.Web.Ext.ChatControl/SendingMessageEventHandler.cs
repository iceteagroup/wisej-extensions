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
	/// Represents the method that will handle the <see cref="ChatBox.SendingMessage"/> event, fired when a message
	/// is about to be added to the <see cref="ChatBox"/>.
	/// </summary>
	/// <param name="sender">The source of the event, the <see cref="ChatBox"/>.</param>
	/// <param name="e">An instance of <see cref="SendingMessageEventArgs"/> containing event data. Set
	/// <see cref="SendingMessageEventArgs.Cancel"/> to <c>true</c> to prevent the message from being displayed.</param>
	/// <example>
	/// <code><![CDATA[
	/// chatBox.SendingMessage += (object sender, SendingMessageEventArgs e) =>
	/// {
	///     // block empty or whitespace-only messages.
	///     if (String.IsNullOrWhiteSpace(e.Message.Content))
	///         e.Cancel = true;
	/// };
	/// ]]></code>
	/// </example>
	public delegate void SendingMessageEventHandler(object sender, SendingMessageEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="ChatBox.SendingMessage"/> event.
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// private void chatBox_SendingMessage(object sender, SendingMessageEventArgs e)
	/// {
	///     if (e.IsChatBoxUser && e.Message.Content.Contains("password"))
	///         e.Cancel = true;
	/// }
	/// ]]></code>
	/// </example>
	public class SendingMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SendingMessageEventArgs"/> class with the specified message.
		/// </summary>
		/// <param name="isChatBoxUser"><c>true</c> if the message is from the <see cref="ChatBox.User"/>; otherwise <c>false</c>.</param>
		/// <param name="message">The <see cref="ChatControl.Message"/> being sent.</param>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Hello", null, chatBox.User);
		/// var args = new SendingMessageEventArgs(true, message);
		/// if (!args.Cancel)
		///     chatBox.DataSource.Add(args.Message);
		/// ]]></code>
		/// </example>
		public SendingMessageEventArgs(bool isChatBoxUser, Message message)
		{
			this.IsChatBoxUser = isChatBoxUser;
			this.Message = message;
		}

		/// <summary>
		/// Returns whether the message is from the <see cref="ChatBox.User"/>.
		/// </summary>
		/// <value><c>true</c> if the message's <see cref="ChatControl.Message.User"/> is the <see cref="ChatBox.User"/>; otherwise <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_SendingMessage(object sender, SendingMessageEventArgs e)
		/// {
		///     // only allow messages typed by the current user.
		///     e.Cancel = !e.IsChatBoxUser;
		/// }
		/// ]]></code>
		/// </example>
		public bool IsChatBoxUser { get; private set; }

		/// <summary>
		/// Returns or sets a value indicating whether to cancel processing the message.
		/// </summary>
		/// <value><c>true</c> to cancel the message; the default is <c>false</c>.</value>
		/// <remarks>
		/// When this value is set to true, the related message will not be added
		/// to the chat container and the <see cref="ChatBox.SentMessage"/> event is not fired.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_SendingMessage(object sender, SendingMessageEventArgs e)
		/// {
		///     if (e.Message.Content.Length > 500)
		///         e.Cancel = true;
		/// }
		/// ]]></code>
		/// </example>
		public bool Cancel { get; set; } = false;

		/// <summary>
		/// Returns the <see cref="ChatControl.Message"/> to be sent.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_SendingMessage(object sender, SendingMessageEventArgs e)
		/// {
		///     e.Message.Content = e.Message.Content.Trim();
		/// }
		/// ]]></code>
		/// </example>
		public Message Message { get; private set; }
	}
}
