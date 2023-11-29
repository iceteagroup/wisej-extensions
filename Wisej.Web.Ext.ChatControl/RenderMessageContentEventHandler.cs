using System;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Event handler for providing message controls.
	/// </summary>
	/// <param name="e"></param>
	public delegate void RenderMessageContentEventHandler(RenderMessageContentEventArgs e);

	/// <summary>
	/// Event args for providing message controls.
	/// </summary>
	public class RenderMessageContentEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new intance of <see cref="RenderMessageContentEventArgs"/> with the given Message.
		/// </summary>
		/// <param name="message"></param>
		public RenderMessageContentEventArgs(Message message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Gets the Message that is requesting a control.
		/// </summary>
		public Message Message { get; private set; }

		/// <summary>
		/// Gets or sets the <see cref="Web.Control"/> to use with the message.
		/// </summary>
		public Control Control { get; set; }
	}
}
