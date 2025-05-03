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
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Wisej.Base;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Provides a control with chat functionality.
	/// </summary>
	[ToolboxItem(true)]
	[DefaultEvent("SentMessage")]
	public partial class ChatBox : UserControl
	{

		#region Constructor

		/// <summary>
		/// Creates a new instance of <see cref="ChatBox"/>.
		/// </summary>
		public ChatBox()
		{
			InitializeComponent();

			// Forward the TextBox tools to this Tools collection.
			this.textBoxMessage.Tools.AddRange(this.Tools.ToArray());
		}

		#endregion

		#region Events

		/// <summary>
		/// Fires before a message is sent.
		/// </summary>
		[Description("Fires before a user-typed message is sent.")]
		public event SendingMessageEventHandler SendingMessage;

		/// <summary>
		/// Fires after a message has been sent.
		/// </summary>
		[Description("Fires after a user-typed message has been sent.")]
		public event MessageEventHandler SentMessage;

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
		public event RenderMessageControlEventHandler RenderMessageControl;

		/// <summary>
		/// Fires when a message is posted to the <see cref="ChatBox"/>.
		/// </summary>
		/// <remarks>
		/// Use this event to save information related to the type of control to render.
		/// </remarks>
		[Description("Fires when the current users posts to the ChatBox.")]
		public event FormatMessageEventHandler FormatMessage;

		#endregion

		#region Overridden Properties

		/// <summary>
		/// Returns or sets the type of scroll bars to display for the <see cref="ScrollableControl" />.
		/// </summary>
		[ResponsiveProperty]
		[DefaultValue(ScrollBars.Both)]
		[SRCategory("CatAppearance")]
		[SRDescription("ScrollableControlScrollBarsDescr")]
		public new ScrollBars ScrollBars
		{
			get
			{
				return this.flexLayoutPanelMessages.ScrollBars;
			}
			set
			{
				this.flexLayoutPanelMessages.ScrollBars = value;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the data source for the chat box.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		[DefaultValue("HH:mmm")]
		[Description("Gets or sets the current timestamp format.")]
		public string TimestampFormat
		{
			get
			{
				return this._TimestampFormat;
			}
			set
			{
				if (this._TimestampFormat != value)
				{
					this._TimestampFormat = value;

					UpdateTimestampFormat(value);
				}
			}
		}
		private string _TimestampFormat = "HH:mm";

		private void UpdateTimestampFormat(string value)
		{
			//foreach (var message in this.DataSource)
			//{
			//	var infoPanel = message.Control?.Parent;
			//	if (infoPanel is MessageInfoControl control)
			//		control.UpdateTimestamp();
			//}
		}

		/// <summary>
		/// Gets or sets the color of the message text box.
		/// </summary>
		[Description("Gets or sets the color of the message text box.")]
		public override Color ForeColor
		{
			get => this.textBoxMessage.BackColor;
			set => this.textBoxMessage.BackColor = value;
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
		[DefaultValue(null)]
		[Description("Gets or sets the current user.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		private User _user = new User(Guid.NewGuid().ToString(), "User", "resource.wx/Wisej.Web.Ext.ChatControl/Images/person-fill.svg");

		/// <summary>
		/// Gets or sets whether the message avatar is visible.
		/// </summary>
		[DefaultValue(true)]
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
		[DefaultValue(true)]
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
		[DefaultValue(true)]
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
		[DefaultValue(null)]
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
		[DefaultValue(false)]
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
		/// <param name="message">The message to remove.</param>
		private void RemoveInternal(Message message)
		{
			var containers = this.flexLayoutPanelMessages.Controls;
			var container = containers.FirstOrDefault(c => ((FlexLayoutPanelMessageContainer)c).Message.Id == message.Id);

			// dispose of the message container.
			if (container != null)
			{
				container.Controls.Clear();
				container.Dispose();
			}
		}

		private void SendCurrentMessage()
		{
			var text = this.textBoxMessage.Text;
			if (!String.IsNullOrEmpty(text))
			{
				// clear text in textbox.
				this.textBoxMessage.Clear();

				// create a new message.
				var message = new Message
				{
					User = this.User,
					Content = text,
				};

				// add it to the datasource.
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
				throw new ArgumentNullException("message");

			// if the message doesn't have a user, assign it to the current user.
			message.User ??= this.User;

			// if the message doesn't have a timestamp, set the timestamp to now.
			message.Timestamp = message.Timestamp == null ? DateTime.Now : message.Timestamp;

			var isChatBoxUser = this.User == message.User;

			// pre-format messages.
			this.FormatMessage?.Invoke(new MessageEventArgs(isChatBoxUser, message));

			message.RenderMessageControl += Message_RenderMessageControl;

			// allow the container to cancel sending the message..
			if (message.Timestamp == null)
			{
				var args = new SendingMessageEventArgs(isChatBoxUser, message);
				SendingMessage?.Invoke(this, args);

				if (args.Cancel)
					return;
			}

			var container = new FlexLayoutPanelMessageContainer(message, this);
			var alignment = GetAlignment(message.User);
			AddToContainer(container, alignment);

			// the message has been sent.
			SentMessage?.Invoke(this, new MessageEventArgs(isChatBoxUser, message));
		}

		private HorizontalAlignment GetAlignment(User user)
		{
			return user.Id == this.User.Id ? HorizontalAlignment.Right : HorizontalAlignment.Left;
		}

		private void Message_RenderMessageControl(RenderMessageControlEventArgs e)
		{
			this.RenderMessageControl?.Invoke(e);
		}

		// adds the container to the list.
		private void AddToContainer(FlexLayoutPanelMessageContainer container, HorizontalAlignment alignment)
		{
			container.HorizontalAlign = alignment;
			this.flexLayoutPanelMessages.Controls.Add(container);
		}

		#endregion

		#region DataSource

		private void DataSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.SuspendLayout();

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					if (e.NewItems != null)
						ProcessAdd(e.NewItems);
					break;

				case NotifyCollectionChangedAction.Remove:
					if (e.OldItems != null)
						ProcessRemove(e.OldItems);
					break;

				case NotifyCollectionChangedAction.Reset:
					ProcessReset();
					break;

				case NotifyCollectionChangedAction.Replace:
					if (e.OldItems != null && e.NewItems != null)
						ProcessReplace(e.OldItems, e.NewItems);
					break;

				case NotifyCollectionChangedAction.Move:
					ProcessMove(e.OldStartingIndex, e.NewStartingIndex);
					break;
			}

			this.ResumeLayout();
		}

		private void ProcessAdd(IList newItems)
		{
			foreach (Message message in newItems)
				AddInternal(message);

			// scroll the last message into view.
			if (this.flexLayoutPanelMessages.Controls.Count > 0)
				this.flexLayoutPanelMessages.ScrollControlIntoView(this.flexLayoutPanelMessages.Controls.LastOrDefault());
		}

		private void ProcessRemove(IList removedItems)
		{
			foreach (Message item in removedItems)
				RemoveInternal(item);
		}

		private void ProcessReset()
		{
			this.flexLayoutPanelMessages.Controls.Clear();
		}

		private void ProcessReplace(IList oldItems, IList newItems)
		{

		}

		private void ProcessMove(int oldStartingIndex, int newStartingIndex)
		{
			var controls = this.flexLayoutPanelMessages.Controls;
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
