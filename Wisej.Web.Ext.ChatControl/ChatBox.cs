using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	public partial class ChatBox : UserControl
	{

		#region Constructor

		public ChatBox()
		{
			InitializeComponent();

			this.panelMessages.Focusable = false;
		}

		#endregion

		#region Events

		/// <summary>
		/// Fires when a message is sent.
		/// </summary>
		public event EventHandler<string> SendMessage;

		/// <summary>
		/// Fires when the user starts typing.
		/// </summary>
		public event EventHandler TypingStart;

		/// <summary>
		/// Fires when the user stops typing.
		/// </summary>
		public event EventHandler TypingEnd;

		/// <summary>
		/// Fires when the user performs an action on a message.
		/// </summary>
		public event EventHandler<dynamic> MessageActionInvoke;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether to enable the file upload tool option.
		/// </summary>
		[DefaultValue(true)]
		public bool AllowFileUpload
		{
			get
			{
				return this._allowFileUpload;
			}
			set
			{
				if (this._allowFileUpload != value) 
				{ 
					this._allowFileUpload = value;

					this.textBoxMessage.Tools["File"].Visible = value;
				}
			}
		}
		private bool _allowFileUpload = true;

		/// <summary>
		/// Gets or sets the color of the left bubble.
		/// </summary>
		public Color BubbleLeftColor
		{
			get
			{
				return this._bubbleLeftColor;
			}
			set
			{
				if (this._bubbleLeftColor != value)
				{
					this._bubbleLeftColor = value;

					UpdateBubbleColors(Alignment.Left, value);
				}
			}
		}
		private Color _bubbleLeftColor = Color.FromName("secondary");

		/// <summary>
		/// Gets or sets the color of the right bubble.
		/// </summary>
		public Color BubbleRightColor
		{
			get
			{
				return this._bubbleRightColor;
			}
			set
			{
				if (this._bubbleRightColor != value) 
				{
					this._bubbleRightColor = value;

					UpdateBubbleColors(Alignment.Right, value);
				}
			}
		}
		private Color _bubbleRightColor = Color.FromName("primary");

		private void UpdateBubbleColors(Alignment alignment, Color value)
		{
			this.panelMessages.Controls.ForEach(e =>
			{
				var container = ((MessageContainer)e);

				if (container.Alignment == alignment)
					container.BubbleColor = value;
			});
		}

		public List<Message> Messages = new List<Message>();

		/// <summary>
		/// Gets or sets the current timestamp format.
		/// </summary>
		[Description("Gets or sets the current timestamp format.")]
		public string TimestampFormat
		{
			get
			{
				return this._TimestampFormat;
			}
			set
			{
				if (this._TimestampFormat  != value) 
				{
					this._TimestampFormat = value;

					UpdateTimestampFormat(value);
				}
			}
		}
		private string _TimestampFormat = "HH:mm";

		private void UpdateTimestampFormat(string value)
		{
			foreach (var message in this.Messages)
			{
				var infoPanel = message.Control?.Parent?.Parent;
				if (infoPanel is MessageInfoControl control)
					control.TimestampFormat = value;
			}
		}

		private bool _typing = false;

		/// <summary>
		/// Gets or sets the color of the message text box.
		/// </summary>
		public Color ForeColor
		{
			get
			{
				return this.textBoxMessage.BackColor;
			}
			set
			{
				if (this.textBoxMessage.BackColor != value) 
				{
					this.textBoxMessage.BackColor = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the current user.
		/// </summary>
		public User User { get; set; }

		#endregion

		#region Event Handlers

		private void textBoxMessage_ToolClick(object sender, ToolClickEventArgs e)
		{
			switch (e.Tool.Name)
			{
				case "Post":
					ProcessSend();
					break;

				case "File":
					ProcessFile();
					break;

				default:
					break;
			}
		}

		private void textBoxMessage_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				ProcessSend();

				this._typing = false;
				this.TypingEnd?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				if (!this._typing)
				{
					this._typing = true;
					this.TypingStart?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void upload1_Uploaded(object sender, UploadedEventArgs e)
		{
			var files = e.Files;

			for (var i = 0; i < files.Count; i++)
			{
				var file = files[i];
				var control = new FileMessageControl(file.InputStream, file.FileName, file.ContentType);

				Post(new Message
				{
					DateTime = DateTime.Now,
					Control = control,
					User = this.User
				});
			}
		}

		private void TextBoxMessage_LostFocus(object sender, System.EventArgs e)
		{
			if (this._typing)
			{
				this._typing = false;

				this.TypingEnd?.Invoke(this, EventArgs.Empty);
			}			
		}

		#endregion

		#region Methods

		public void Clear()
		{
			this.panelMessages.Controls.Clear();
		}

		/// <summary>
		/// Deletes a given message from the chat box.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool DeleteMessage(Message message)
		{
			var container = message.Control?.Parent?.Parent?.Parent;

			container?.Dispose();

			return this.Messages.Remove(message);
		}

		private void ProcessSend()
		{
			var message = this.textBoxMessage.Text;
			if (!string.IsNullOrEmpty(message))
			{
				this.textBoxMessage.Clear();

				Post(this.User, message);
			}
		}

		private void ProcessFile()
		{
			this.upload1.UploadFiles();
		}

		/// <summary>
		/// Posts a message for the user provided with the given message.
		/// </summary>
		/// <param name="user">The user posting the message</param>
		/// <param name="message">The message</param>
		public void Post(User user, string message)
		{
			Post(new Message {
				User = user,
				DateTime = DateTime.Now,
				Control = new TextMessageControl(message)
			});
		}

		/// <summary>
		/// Posts a message to the chat box with the provided message.
		/// </summary>
		/// <param name="message">The message to post</param>
		/// <exception cref="ArgumentNullException"></exception>
		public void Post(Message message)
		{
			if (message == null)
				throw new ArgumentNullException("Message cannot be null.");

			this.Messages.Add(message);

			message.Control.ActionInvoke += Message_ActionInvoke;

			var container = new MessageContainer(message);

			ConfigureContainer(message.User, container);
		}

		private void Message_ActionInvoke(object? sender, dynamic e)
		{
			this.MessageActionInvoke?.Invoke(sender, e);
		}

		private void ConfigureContainer(User user, MessageContainer container)
		{
			container.Dock = DockStyle.Top;

			this.panelMessages.Controls.Add(container);

			container.BringToFront();

			// apply user settings.
			var me = user.Id == this.User.Id;
			var appearance = me ? Alignment.Right : Alignment.Left;
			var color = me ? this.BubbleRightColor : this.BubbleLeftColor;

			container.BubbleColor = color;
			container.Alignment = appearance;

			// scroll to the bottom.
			this.panelMessages.ScrollControlIntoView(container);
		}

		#endregion

	}
}
