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
	/// <remarks>
	/// Messages are displayed by adding them to <see cref="ChatBox.DataSource"/>. When a message is added
	/// without a <see cref="User"/> or <see cref="Timestamp"/>, the <see cref="ChatBox"/> assigns its own
	/// <see cref="ChatBox.User"/> and the current time. The control that displays the message is created
	/// when the message is added: handle <see cref="ChatBox.RenderMessageControl"/> to supply a custom control,
	/// otherwise an <see cref="AutoSizeLabel"/> showing <see cref="Content"/> is used.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var message = new Message("Hello, how can I help you?")
	/// {
	///     User = new User("bot", "Assistant"),
	///     Timestamp = DateTime.Now
	/// };
	/// chatBox.DataSource.Add(message);
	/// ]]></code>
	/// </example>
	public class Message
	{

		#region Constructor

		/// <summary>
		/// Creates a new, empty instance of <see cref="Message"/>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message
		/// {
		///     User = chatBox.User,
		///     Content = "Good morning!"
		/// };
		/// chatBox.DataSource.Add(message);
		/// ]]></code>
		/// </example>
		public Message() { }

		/// <summary>
		/// Creates a new instance of <see cref="Message"/> with the given content, content type and user.
		/// </summary>
		/// <param name="user">The user associated with the message. When <c>null</c>, the <see cref="ChatBox"/>
		/// assigns its own <see cref="ChatBox.User"/> when the message is added.</param>
		/// <param name="content">The content of the message.</param>
		/// <param name="contentType">The message content type, for example <c>"text/plain"</c> or <c>"text/html"</c>; can be <c>null</c>.</param>
		/// <example>
		/// <code><![CDATA[
		/// var alice = new User("1", "Alice");
		/// var message = new Message("Hi everyone!", "text/plain", alice);
		/// chatBox.DataSource.Add(message);
		/// ]]></code>
		/// </example>
		public Message(string content, string contentType = null, User user = null)
		{
			this.User = user;
			this.Content = content;
			this.ContentType = contentType;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the unique identifier for this message.
		/// </summary>
		/// <remarks>
		/// This is a public field initialized to a new <see cref="Guid"/> string. The <see cref="ChatBox"/> uses it
		/// to locate the message's container when the message is removed; <see cref="Clone"/> preserves it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Hello");
		/// var removed = chatBox.DataSource.FirstOrDefault(m => m.Id == message.Id);
		/// ]]></code>
		/// </example>
		public string Id = Guid.NewGuid().ToString();

		/// <summary>
		/// Returns or sets the user associated with the message.
		/// </summary>
		/// <value>The <see cref="ChatControl.User"/> that sent the message. When <c>null</c>, the <see cref="ChatBox"/>
		/// assigns its own <see cref="ChatBox.User"/> when the message is added.</value>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Hello");
		/// message.User = new User("2", "Bob");
		/// ]]></code>
		/// </example>
		public User? User { get; set; }

		/// <summary>
		/// Returns or sets the timestamp of the message.
		/// </summary>
		/// <remarks>
		/// This is a public field; the default is <c>null</c>. When the message is added to a <see cref="ChatBox"/>
		/// without a timestamp, it is set to <see cref="DateTime.Now"/>. The displayed value is formatted using
		/// <see cref="ChatBox.TimestampFormat"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Sent yesterday");
		/// message.Timestamp = DateTime.Now.AddDays(-1);
		/// ]]></code>
		/// </example>
		public DateTime? Timestamp = null;

		/// <summary>
		/// Returns or sets the content of the message.
		/// </summary>
		/// <value>The message text. The default <see cref="AutoSizeLabel"/> control allows HTML content.</value>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message();
		/// message.Content = "Your order has <b>shipped</b>.";
		/// ]]></code>
		/// </example>
		public string Content { get; set; }

		/// <summary>
		/// Returns or sets the content type of the message.
		/// </summary>
		/// <value>An application-defined string, for example <c>"text/plain"</c> or <c>"image/png"</c>; the default is <c>null</c>.</value>
		/// <remarks>
		/// The <see cref="ChatBox"/> doesn't interpret this value. Use it in the <see cref="ChatBox.RenderMessageControl"/>
		/// or <see cref="ChatBox.FormatMessage"/> handlers to decide how to render or format the message.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("https://example.com/cat.png", "image");
		/// chatBox.DataSource.Add(message);
		/// ]]></code>
		/// </example>
		public string ContentType { get; set; }

		/// <summary>
		/// Returns or sets custom user data associated with the message.
		/// </summary>
		/// <value>An application-defined string; the default is <c>null</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Order #1234 confirmed.");
		/// message.UserData = "order:1234";
		/// ]]></code>
		/// </example>
		public string UserData { get; set; }

		/// <summary>
		/// Returns or sets the control rendered in the chat box for this message.
		/// </summary>
		/// <value>
		/// The control displaying the message, by default an <see cref="AutoSizeLabel"/>. It is <c>null</c> until
		/// the message has been added to a <see cref="ChatBox"/>.
		/// </value>
		/// <remarks>
		/// The <see cref="MessageControlAssigned"/> event is fired when the control is assigned.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.DataSource.Add(message);
		/// if (message.Control != null)
		///     message.Control.ForeColor = Color.White;
		/// ]]></code>
		/// </example>
		public Control Control { get; set; }

		/// <summary>
		/// Returns or sets whether the bubble background is visible.
		/// </summary>
		/// <remarks>
		/// This is a public field; the default is <c>true</c>. When <c>false</c>, the bubble is rendered with a
		/// transparent background. Set it before the message is added to the <see cref="ChatBox"/>, or in the
		/// <see cref="ChatBox.FormatMessage"/> event.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var notice = new Message("Alice joined the chat.") { BubbleVisible = false };
		/// chatBox.DataSource.Add(notice);
		/// ]]></code>
		/// </example>
		public bool BubbleVisible = true;

		#endregion

		#region Events

		/// <summary>
		/// Fires when the <see cref="Message"/>'s control is requested.
		/// </summary>
		internal event RenderMessageControlEventHandler RenderMessageControl;

		/// <summary>
		/// Fired when the <see cref="Message.Control"/> is assigned.
		/// </summary>
		/// <remarks>
		/// The event is fired each time the control is requested for display, after <see cref="Control"/> has been set,
		/// so handlers can safely customize the control.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("Important!");
		/// message.MessageControlAssigned += (s, e) =>
		/// {
		///     ((Message)s).Control.Font = new Font("Arial", 14, FontStyle.Bold);
		/// };
		/// chatBox.DataSource.Add(message);
		/// ]]></code>
		/// </example>
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
		/// Creates a copy of the current message.
		/// </summary>
		/// <returns>A new <see cref="Message"/> with the same <see cref="Id"/>, <see cref="User"/>, <see cref="Content"/>,
		/// <see cref="UserData"/>, <see cref="Timestamp"/> and <see cref="ContentType"/>.</returns>
		/// <remarks>
		/// The clone gets its own new <see cref="Control"/> (an <see cref="AutoSizeLabel"/> displaying <see cref="Content"/>)
		/// and does not share the original's control. <see cref="BubbleVisible"/> is not copied.
		/// Derived classes can override this method to copy additional state.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var original = new Message("Forward me", null, new User("1", "Alice"));
		/// var copy = original.Clone();
		/// otherChatBox.DataSource.Add(copy);
		/// ]]></code>
		/// </example>
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
