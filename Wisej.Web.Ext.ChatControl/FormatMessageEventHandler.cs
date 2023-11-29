using System;

namespace Wisej.Web.Ext.ChatControl
{

	public delegate void FormatMessageEventHandler(FormatMessageEventArgs e);

	public class FormatMessageEventArgs : EventArgs
	{
		public FormatMessageEventArgs(Message message)
		{
			this.Message = message;
		}
		
		/// <summary>
		/// Gets or sets the message to format.
		/// </summary>
		public Message Message { get; private set; }
	}
}
