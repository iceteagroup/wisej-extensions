using System;

namespace Wisej.Web.Ext.ChatControl
{

	public delegate void SendMessageEventHandler(SendMessageEventArgs e);

	public class SendMessageEventArgs : EventArgs
	{
		public SendMessageEventArgs(Message message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Gets or sets whether to cancel processing the message.
		/// </summary>
		public bool Cancel { get; set; } = false;

		/// <summary>
		/// Gets or sets the message to format.
		/// </summary>
		public Message Message { get; private set; }
	}
}
