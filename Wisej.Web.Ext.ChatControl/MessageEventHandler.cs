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
	/// Event handler for message content.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">An instance of <see cref="MessageEventArgs"/> with the message data.</param>
	public delegate void MessageEventHandler(object sender, MessageEventArgs e);

	/// <summary>
	/// Provides data for the event after a message has been sent.
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageEventArgs"/> class with the specified message.
		/// </summary>
		/// <param name="isChatBoxUser">True if the message is from the <see cref="ChatBox.User"/></param>
		/// <param name="message">The sent message.</param>
		public MessageEventArgs(bool isChatBoxUser, Message message)
		{
			this.IsChatBoxUser = isChatBoxUser;
			this.Message = message;
		}

		/// <summary>
		/// Returns true if the message is from the <see cref="ChatBox.User"/>>.
		/// </summary>
		public bool IsChatBoxUser { get; private set; }

		/// <summary>
		/// Gets the sent message.
		/// </summary>
		public Message Message { get; private set; }
	}
}
