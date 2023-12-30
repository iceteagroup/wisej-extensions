using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Provides a control with chat-like functionality.
	/// </summary>
	[ToolboxItem(true)]
	public partial class ChatBox : UserControl
	{

		#region Constructor

		/// <summary>
		/// Creates a new instance of <see cref="ChatBox"/>.
		/// </summary>
		public ChatBox()
		{
			InitializeComponent();

			this.textBoxMessage.Tools.AddRange(this.Tools.ToArray());
		}

		#endregion

		#region Events

		/// <summary>
		/// Fires when a message is sent.
		/// </summary>
		[Description("Fires when a message is sent.")]
		public event SendMessageEventHandler SendMessage;

		/// <summary>
		/// Fires when the user starts typing.
		/// </summary>
		[Description("Fires when the user starts typing.")]
		public event EventHandler TypingStart;

		/// <summary>
		/// Fires when the user stops typing.
		/// </summary>
		[Description("Fires when the user stops typing.")]
		public event EventHandler TypingEnd;

		/// <summary>
		/// Fires when the user performs an action on a message.
		/// </summary>
		[Description("Fires when the user performs an action on a message.")]
		public event EventHandler<dynamic> MessageActionInvoke;

		/// <summary>
		/// Fires when a <see cref="ComponentTool"/> is clicked.
		/// </summary>
		[Description("Fires when a tool item is clicked.")]
		public event ToolClickEventHandler ToolClick
		{
			add { this.textBoxMessage.ToolClick += value; }
			remove { this.textBoxMessage.ToolClick -= value; }
		}

		/// <summary>
		/// Fires when a <see cref="Message"/> control is needed.
		/// </summary>
		[Description("Fires when a Message control is needed.")]
		public event RenderMessageContentEventHandler RenderMessageContent;

		/// <summary>
		/// Fires when a message is posted to the <see cref="ChatBox"/>.
		/// </summary>
		/// <remarks>
		/// Use this event to save information related to the type of control to render.
		/// </remarks>
		[Description("Fires when the current users posts to the ChatBox.")]
		public event FormatMessageEventHandler FormatMessage;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the data source for the chat box.
		/// </summary>
		public ObservableCollection<Message> DataSource
		{
			get
			{
				if (this._dataSource == null)
				{
					this._dataSource = new ObservableCollection<Message>();
					this._dataSource.CollectionChanged += DataSource_CollectionChanged;
				}
	
				return this._dataSource;
			}
		}
		private ObservableCollection<Message> _dataSource;

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
			foreach (var message in this.DataSource)
			{
				var infoPanel = message.Control?.Parent?.Parent;
				if (infoPanel is MessageInfoControl control)
					control.UpdateTimestamp();
			}
		}

		/// <summary>
		/// Gets or sets the color of the message text box.
		/// </summary>
		[Description("Gets or sets the color of the message text box.")]
		public override Color ForeColor
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
		/// Gets the tools collection for the ChatBox.
		/// </summary>
		[Browsable(true)]
		[MergableProperty(false)]
		[Description("Gets the tools collection for the ChatBox.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ComponentToolCollection Tools
		{
			get
			{
				return this.textBoxMessage.Tools;
			}
		}

		/// <summary>
		/// Gets or sets the current user.
		/// </summary>
		[Description("Gets or sets the current user.")]
		public User User 
		{ 
			get
			{
				return this._user;
			}
			set
			{
				if (this._user != value)
				{
					this._user = value;

					// TODO: update existing messages.
				}
			}
		}
		private User _user = new User("1", "User", "resource.wx/Wisej.Web.Ext.ChatControl/Images/Wisej.png");

		/// <summary>
		/// Gets or sets whether the message avatar is visible.
		/// </summary>
		public bool AvatarVisible
		{
			get
			{
				return this._avatarVisible;
			}
			set
			{
				if (this._avatarVisible != value)
				{
					this._avatarVisible = value;

					//TODO: update existing messages.
				}
			}
		}
		private bool _avatarVisible = true;

		/// <summary>
		/// Gets or sets whether to display the timestamp.
		/// </summary>
		public bool TimestampVisible
		{
			get
			{
				return this._timestampVisible;
			}
			set
			{
				if (this._timestampVisible != value)
				{
					this._timestampVisible = value;

					// TODO: update existing messages.
				}
			}
		}
		private bool _timestampVisible = true;

		/// <summary>
		/// Gets or sets whether to show the input text box.
		/// </summary>
		public bool InputVisible
		{
			get
			{
				return this.panelMessageInput.Visible;
			}
			set
			{
				this.panelMessageInput.Visible = value;
			}
		}

		/// <summary>
		/// Gets or sets the text to show when the Chat's TextBox is empty.
		/// </summary>
		public string Watermark
		{
			get
			{
				return this.textBoxMessage.Watermark;
			}
			set
			{
				this.textBoxMessage.Watermark = value;
			}
		}

		/// <summary>
		/// Returns or sets whether the chat control is in read-only mode.
		/// </summary>
		public bool ReadOnly
		{
			get
			{
				return this.textBoxMessage.ReadOnly;
			}
			set
			{
				this.buttonSend.Enabled = !value;
				this.textBoxMessage.ReadOnly = value;
			}
		}

		#endregion

		#region Event Handlers

		private void textBoxMessage_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendCurrentMessage();

				this._isTyping = false;
				this.TypingEnd?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				if (!this._isTyping)
				{
					this._isTyping = true;

					this.TypingStart?.Invoke(this, EventArgs.Empty);
				}
			}
		}
		private bool _isTyping = false;

		private void textBoxMessage_LostFocus(object sender, EventArgs e)
		{
			if (this._isTyping)
			{
				this._isTyping = false;

				this.TypingEnd?.Invoke(this, EventArgs.Empty);
			}
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			SendCurrentMessage();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Clears the chat box messages.
		/// </summary>
		public void Clear()
		{
			this.DataSource.Clear();
		}

		/// <summary>
		/// Removes the control with the corresponding message.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private void RemoveInternal(Message message)
		{
			var containers = this.panelMessages.Controls;
			var container = containers.FirstOrDefault(c => ((MessageContainer)c).Message.Id == message.Id);

			container?.Dispose();
		}

		private void SendCurrentMessage()
		{
			var text = this.textBoxMessage.Text;

			// post the message.
			if (!string.IsNullOrEmpty(text))
			{
				this.textBoxMessage.Clear();

				var message = new Message
				{
					User = this.User,
					Content = text,
				};

				this.DataSource.Add(message);
			}
		}

		/// <summary>
		/// Posts a message to the chat box with the provided message.
		/// </summary>
		/// <param name="message">The message to post</param>
		/// <exception cref="ArgumentNullException"></exception>
		internal void AddInternal(Message message)
		{
			if (message == null)
				throw new ArgumentNullException("Message cannot be null.");

			// if the message doesn't have a user, it belongs to the current user.
			if (message.User == null)
				message.User = this.User;

			var timestamp = message.Timestamp;
			if (timestamp == null)
				message.Timestamp = DateTime.Now;

			// pre-format user messages.
			if (message.User.Id == this.User.Id)
			{
				var args = new FormatMessageEventArgs(message);
				this.FormatMessage?.Invoke(args);
			}

			message.MessageControlNeeded += Message_MessageControlNeeded;

			// fire SendMessage for user messages.
			if (message.User.Id == this.User.Id && timestamp == null)
			{
				var args = new SendMessageEventArgs(message);
				
				SendMessage?.Invoke(args);

				if (args.Cancel)
					return;
			}

			var container = new MessageContainer(message, this);

			var alignment = GetAlignment(message.User);

			AddToContainer(container, alignment);
		}

		private void Message_MessageControlNeeded(RenderMessageContentEventArgs e)
		{
			this.RenderMessageContent?.Invoke(e);
		}

		// determines whether the given user should be on the left or right.
		private Alignment GetAlignment(User user)
		{
			return user.Id == this.User.Id ? Alignment.Right : Alignment.Left;
		}

		// adds the container to the list.
		private void AddToContainer(MessageContainer container, Alignment alignment)
		{
			container.Dock = DockStyle.Top;

			this.panelMessages.Controls.Add(container);

			container.BringToFront();

			container.Alignment = alignment;
		}

		#endregion

		#region DataSource

		private void DataSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					ProcessAdd(e.NewItems);
					break;

				case NotifyCollectionChangedAction.Remove:
					ProcessRemove(e.OldItems);
					break;

				case NotifyCollectionChangedAction.Reset:
					ProcessReset();
					break;

				case NotifyCollectionChangedAction.Replace:
					ProcessReplace(e.OldItems, e.NewItems);
					break;

				case NotifyCollectionChangedAction.Move:
					ProcessMove(e.OldStartingIndex, e.NewStartingIndex);
					break;
			}
		}

		private void ProcessAdd(IList newItems)
		{
			foreach (Message message in newItems)
				AddInternal(message);

			if (this.panelMessages.Controls.Count > 0)
				this.panelMessages.ScrollControlIntoView(this.panelMessages.Controls.First());
		}

		private void ProcessRemove(IList removedItems) 
		{
			foreach (Message item in removedItems)
				RemoveInternal(item);
		}

		private void ProcessReset() 
		{
			this.panelMessages.Controls.Clear();
		}

		private void ProcessReplace(IList oldItems, IList newItems) 
		{ 
		
		}

		private void ProcessMove(int oldStartingIndex, int newStartingIndex) 
		{
			var controls = this.panelMessages.Controls;
			var control = controls[oldStartingIndex];
			
			controls.SetChildIndex(control, newStartingIndex);
		}

		#endregion

		#region Export

		/// <summary>
		/// Exports the chat history as a json string.
		/// </summary>
		/// <returns></returns>
		public string ExportAsJson()
		{
			return JSON.Stringify(this.DataSource);
		}

		#endregion

	}
}
