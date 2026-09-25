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
	/// Represents the method that will handle the <see cref="ChatBox.SentMessage"/> event, fired after a message
	/// has been added to the <see cref="ChatBox"/>.
	/// </summary>
	/// <param name="sender">The source of the event, the <see cref="ChatBox"/>.</param>
	/// <param name="e">An instance of <see cref="MessageEventArgs"/> with the message data.</param>
	/// <example>
	/// <code><![CDATA[
	/// chatBox.SentMessage += (object sender, MessageEventArgs e) =>
	/// {
	///     // reply to messages typed by the current user.
	///     if (e.IsChatBoxUser)
	///         chatBox.DataSource.Add(new Message("You said: " + e.Message.Content, null, new User("bot", "Echo")));
	/// };
	/// ]]></code>
	/// </example>
	public delegate void MessageEventHandler(object sender, MessageEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="ChatBox.SentMessage"/> and <see cref="ChatBox.FormatMessage"/> events.
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// private void chatBox_SentMessage(object sender, MessageEventArgs e)
	/// {
	///     if (!e.IsChatBoxUser)
	///         AlertBox.Show("New message from " + e.Message.User?.Name);
	/// }
	/// ]]></code>
	/// </example>
	public class MessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageEventArgs"/> class with the specified message.
		/// </summary>
		/// <param name="isChatBoxUser"><c>true</c> if the message is from the <see cref="ChatBox.User"/>; otherwise <c>false</c>.</param>
		/// <param name="message">The <see cref="ChatControl.Message"/> the event refers to.</param>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Hello", null, chatBox.User);
		/// var args = new MessageEventArgs(message.User == chatBox.User, message);
		/// ]]></code>
		/// </example>
		public MessageEventArgs(bool isChatBoxUser, Message message)
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
		/// private void chatBox_SentMessage(object sender, MessageEventArgs e)
		/// {
		///     if (e.IsChatBoxUser)
		///         e.Message.UserData = "outgoing";
		/// }
		/// ]]></code>
		/// </example>
		public bool IsChatBoxUser { get; private set; }

		/// <summary>
		/// Returns the <see cref="ChatControl.Message"/> the event refers to.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// private void chatBox_FormatMessage(object sender, MessageEventArgs e)
		/// {
		///     e.Message.Content = e.Message.Content.Replace(":)", "&#128578;");
		/// }
		/// ]]></code>
		/// </example>
		public Message Message { get; private set; }
	}
}
