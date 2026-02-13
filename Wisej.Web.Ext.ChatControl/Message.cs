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
using System.Drawing;

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
		public Message() { }

		/// <summary>
		/// Creates a new instance of <see cref="Message"/> with the given configuration.
		/// </summary>
		/// <param name="user">The user associated with the message.</param>
		/// <param name="content">The content of the message.</param>
		/// <param name="contentType">The message content type.</param>
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
		public User? User { get; set; }

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

		/// <summary>
		/// The control rendered in the chat box. 
		/// Only available after the control has been added to the chatbox.
		/// </summary>
		public Control Control { get; set; }

		/// <summary>
		/// Gets or sets whether the bubble background is visible.
		/// </summary>
		public bool BubbleVisible = true;

		#endregion

		#region Events

		/// <summary>
		/// Fires when the <see cref="Message"/>'s control is requested.
		/// </summary>
		internal event RenderMessageControlEventHandler RenderMessageControl;

		/// <summary>
		/// Fires when the <see cref="Message.Control"/> is assigned.
		/// </summary>
		public event EventHandler MessageControlAssigned;

		/// <summary>
		/// Invokes the RenderMessageControl event. 
		/// Fires when the Message's control is requested.
		/// </summary>
		/// <param name="e">The event data.</param>
		internal void OnRenderMessageControl(RenderMessageControlEventArgs e)
		{
			RenderMessageControl?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the MessageControlAssigned event.
		/// Fires when the Message.Control is assigned.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnMessageControlAssigned(EventArgs e)
		{
			MessageControlAssigned?.Invoke(this, e);
		}

		#endregion

		#region Methods

		// requests a control to be rendered for the current message.
		internal Control RequestControl()
		{
			if (this.Control == null)
			{
				// request a control from the user.
				var args = new RenderMessageControlEventArgs(this);

				OnRenderMessageControl(args);

				// if the user didn't provide a control, use the default one.
				if (args.Control == null)
				{
					this.Control = new AutoSizeLabel
					{
						Selectable = true,
						Text = this.Content,
						UseMnemonic = false,
						Cursor = Cursors.Text,
						ForeColor = Color.FromName("@highlightText")
					};
				}
			}

			OnMessageControlAssigned(EventArgs.Empty);

			return this.Control;
		}

		/// <summary>
		/// Clones the current message.
		/// </summary>
		/// <returns>The cloned message.</returns>
		public virtual Message Clone()
		{
			// create a new instance of Message with the same properties.
			var message = new Message
			{
				Id = this.Id,
				User = this.User,
				Content = this.Content,
				UserData = this.UserData,
				Timestamp = this.Timestamp,
				ContentType = this.ContentType
			};

			// request a new control (duplicate) for the message. Defaults to AutoSizeLabel.
			// Handle ChatBox.RenderMessageControl to provide a custom control.
			message.RequestControl();

			return message;
		}

		#endregion

	}
}
