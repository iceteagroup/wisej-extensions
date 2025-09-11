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
	/// Represents the method that will handle the event when a message is being sent.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">An instance of <see cref="SendingMessageEventArgs"/> containing event data.</param>
	public delegate void SendingMessageEventHandler(object sender, SendingMessageEventArgs e);

	/// <summary>
	/// Provides data for the event when a message is being sent.
	/// </summary>
	public class SendingMessageEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SendingMessageEventArgs"/> class with the specified message.
		/// </summary>
		/// <param name="isChatBoxUser">True if the message is from the <see cref="ChatBox.User"/></param>
		/// <param name="message">The message to format.</param>
		public SendingMessageEventArgs(bool isChatBoxUser, Message message)
		{
			this.IsChatBoxUser = isChatBoxUser;
			this.Message = message;
		}

		/// <summary>
		/// Returns true if the message is from the <see cref="ChatBox.User"/>>.
		/// </summary>
		public bool IsChatBoxUser { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether to cancel processing the message.
		/// </summary>
		/// <remarks>
		/// When this value is set to true, the related message will not be added
		/// to the chat container.
		/// </remarks>
		public bool Cancel { get; set; } = false;

		/// <summary>
		/// The message to be sent.
		/// </summary>
		public Message Message { get; private set; }
	}
}
