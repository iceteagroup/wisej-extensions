using System;
using System.Drawing;
using Wisej.Web.Ext.ChatControl.Messages;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Represents a message in a <see cref="ChatBox"/>.
	/// </summary>
	public class Message
	{

		#region Constructor

		/// <summary>
		/// Creates a new instance of <see cref="Message"/>.
		/// </summary>
		public Message()
		{
		}

		/// <summary>
		/// Creates a new instance of <see cref="Message"/> with the given configuration.
		/// </summary>
		/// <param name="user"></param>
		/// <param name="content"></param>
		public Message(string content, string contentType = null, User user = null)
		{
			this.User = user;
			this.Content = content;
			this.ContentType = contentType;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a unique identifier for this message.
		/// </summary>
		public string Id = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the user associated with the message.
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Gets or sets the timestamp of the message.
		/// </summary>
		public DateTime? Timestamp = null;

		/// <summary>
		/// Gets or sets the message data.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the content type of the message.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets or sets user data associated with the message.
		/// </summary>
		public string UserData { get; set; }

		// the control rendered in the chat box.
		internal Control? Control { get; set; }

		#endregion

		#region Events

		internal event RenderMessageContentEventHandler MessageControlNeeded;

		#endregion

		#region Methods

		// requests a control to be rendered for the current message.
		internal Control RequestControl()
		{
			var args = new RenderMessageContentEventArgs(this);

			MessageControlNeeded?.Invoke(args);

			if (args.Control == null)
				this.Control = new ClientAutoSizeLabel { AllowHtml = true, Text = this.Content, ForeColor = Color.White };
			else
				this.Control = args.Control;

			return this.Control;
		}

		/// <summary>
		/// Clones the given message.
		/// </summary>
		/// <returns>The cloned message.</returns>
		public Message Clone()
		{
			return new Message
			{
				Content = this.Content,
				ContentType = this.ContentType,
				Control = this.Control,
				Timestamp = this.Timestamp,
				Id = this.Id,
				User = this.User,
				UserData = this.UserData
			};
		}

		#endregion

	}
}
