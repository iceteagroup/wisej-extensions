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
	/// Provides a control with chat functionality: a scrollable list of message bubbles
	/// and an input box with a send button.
	/// </summary>
	/// <remarks>
	/// Messages are managed through the <see cref="DataSource"/> collection: adding a
	/// <see cref="Message"/> to it renders the message, removing it removes the rendered control.
	/// Messages typed by the user are added automatically when the user presses Enter or clicks
	/// the send button. The default event is <see cref="SentMessage"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chatBox = new ChatBox { Dock = DockStyle.Fill };
	/// chatBox.User = new User("1", "Alice");
	/// chatBox.SentMessage += (s, e) => AlertBox.Show(e.Message.Content);
	/// chatBox.DataSource.Add(new Message("Hello!", null, new User("2", "Bot")));
	/// this.Controls.Add(chatBox);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[DefaultEvent("SentMessage")]
	public partial class ChatBox : UserControl
	{
		private int _multilineInputListenerId = -1;

		#region Constructor

		/// <summary>
		/// Creates a new instance of <see cref="ChatBox"/>.
		/// </summary>
		/// <remarks>
		/// The current <see cref="User"/> is initialized to a default user named "User"
		/// with a generated id.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chatBox = new ChatBox();
		/// chatBox.User = new User { Id = "1", Name = "Alice" };
		/// ]]></code>
		/// </example>
		public ChatBox()
		{
			InitializeComponent();

			// Keep the message input from acting as a nested native drop target.
			// File drops should be handled by the ChatBox as a whole.
			this.textBoxMessage.AllowDrop = false;

			// Forward the TextBox tools to this Tools collection.
			this.textBoxMessage.Tools.AddRange(this.Tools.ToArray());
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired before a message is sent.
		/// </summary>
		/// <remarks>
		/// Set <see cref="SendingMessageEventArgs.Cancel"/> to <c>true</c> to prevent the message
		/// from being rendered and <see cref="SentMessage"/> from firing. The event is raised only
		/// for messages added without a <see cref="Message.Timestamp"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.SendingMessage += (s, e) =>
		/// {
		///     if (String.IsNullOrWhiteSpace(e.Message.Content))
		///         e.Cancel = true;
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fires before a user-typed message is sent.")]
		public event SendingMessageEventHandler SendingMessage;

		/// <summary>
		/// Fired after a message has been sent and rendered in the <see cref="ChatBox"/>.
		/// </summary>
		/// <remarks>
		/// Fires for every message added to <see cref="DataSource"/>, whether typed by the user or
		/// added in code. Use <see cref="MessageEventArgs.IsChatBoxUser"/> to determine whether the
		/// message belongs to the current <see cref="User"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var botUser = new User("bot", "Echo Bot");
		/// chatBox.SentMessage += (s, e) =>
		/// {
		///     if (e.IsChatBoxUser)
		///         chatBox.DataSource.Add(new Message("Echo: " + e.Message.Content, null, botUser));
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fires after a user-typed message has been sent.")]
		public event MessageEventHandler SentMessage;

		/// <summary>
		/// Fired when the user starts typing in the message input box.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.TypingStart += (s, e) => labelStatus.Text = "Typing...";
		/// ]]></code>
		/// </example>
		[Description("Fires when the user starts typing.")]
		public event EventHandler TypingStart;

		/// <summary>
		/// Fired when the user stops typing.
		/// </summary>
		/// <remarks>
		/// Fires when the message is sent with Enter or when the input box loses focus while typing.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.TypingEnd += (s, e) => labelStatus.Text = "";
		/// ]]></code>
		/// </example>
		[Description("Fires when the user stops typing.")]
		public event EventHandler TypingEnd;

		/// <summary>
		/// Fired when the user performs an action on a message.
		/// </summary>
		/// <remarks>
		/// The event data is a <c>dynamic</c> object describing the action.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.MessageActionInvoke += (s, e) => AlertBox.Show("Message action invoked.");
		/// ]]></code>
		/// </example>
		[Description("Fires when the user performs an action on a message.")]
		public event EventHandler<dynamic> MessageActionInvoke;

		/// <summary>
		/// Fired when a <see cref="ComponentTool"/> in the <see cref="Tools"/> collection is clicked.
		/// </summary>
		/// <remarks>
		/// Handlers are attached to the <c>ToolClick</c> event of the inner message input text box.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.Tools.Add(new ComponentTool { Name = "attach", ImageSource = "icon-attach" });
		/// chatBox.ToolClick += (s, e) =>
		/// {
		///     if (e.Tool.Name == "attach")
		///         AlertBox.Show("Attach clicked");
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fires when a tool item is clicked.")]
		public event ToolClickEventHandler ToolClick
		{
			add { this.textBoxMessage.ToolClick += value; }
			remove { this.textBoxMessage.ToolClick -= value; }
		}

		/// <summary>
		/// Fired when a <see cref="Message"/> needs a control to display its content.
		/// </summary>
		/// <remarks>
		/// Set <see cref="RenderMessageControlEventArgs.Control"/> to provide a custom control.
		/// When no control is provided, a selectable label showing <see cref="Message.Content"/> is used.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.RenderMessageControl += (s, e) =>
		/// {
		///     if (e.Message.ContentType == "image")
		///         e.Control = new PictureBox { ImageSource = e.Message.Content };
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fires when a Message control is needed.")]
		public event RenderMessageControlEventHandler RenderMessageControl;

		/// <summary>
		/// Fired when a message is posted to the <see cref="ChatBox"/>, before it is rendered.
		/// </summary>
		/// <remarks>
		/// Use this event to save information related to the type of control to render, for example
		/// by setting <see cref="Message.ContentType"/> or <see cref="Message.UserData"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.FormatMessage += (s, e) =>
		/// {
		///     if (e.Message.Content.StartsWith("http"))
		///         e.Message.ContentType = "link";
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fires when the current users posts to the ChatBox.")]
		public event FormatMessageEventHandler FormatMessage;

		/// <summary>
		/// Invokes the SendingMessage event. Fires before a message is sent.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnSendingMessage(SendingMessageEventArgs e)
		{
			SendingMessage?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the SentMessage event. Fires after a message has been sent.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnSentMessage(MessageEventArgs e)
		{
			SentMessage?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the TypingStart event. Fires when the user starts typing.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnTypingStart(EventArgs e)
		{
			TypingStart?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the TypingEnd event. Fires when the user stops typing.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnTypingEnd(EventArgs e)
		{
			TypingEnd?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the MessageActionInvoke event. Fires when the user performs an action on a message.
		/// </summary>
		/// <param name="e">The dynamic event data.</param>
		protected virtual void OnMessageActionInvoke(dynamic e)
		{
			MessageActionInvoke?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the RenderMessageControl event. Fires when a Message control is needed.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnRenderMessageControl(RenderMessageControlEventArgs e)
		{
			RenderMessageControl?.Invoke(this, e);
		}

		/// <summary>
		/// Invokes the FormatMessage event. Fires when a message is posted to the ChatBox.
		/// </summary>
		/// <param name="e">The event data.</param>
		protected virtual void OnFormatMessage(MessageEventArgs e)
		{
			FormatMessage?.Invoke(this, e);
		}
		#endregion

		#region Overridden Properties

		/// <summary>
		/// Returns or sets the type of scroll bars to display in the messages area of the <see cref="ChatBox"/>.
		/// </summary>
		/// <value>
		/// One of the <see cref="Wisej.Web.ScrollBars"/> values. The default is <see cref="Wisej.Web.ScrollBars.Both"/>.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="ScrollableControl.ScrollBars"/> and applies the value to the
		/// inner panel that hosts the messages.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.ScrollBars = ScrollBars.Vertical;
		/// ]]></code>
		/// </example>
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
		/// Returns the collection of messages displayed in the chat box.
		/// </summary>
		/// <value>
		/// An <see cref="ObservableCollection{T}"/> of <see cref="Message"/> objects, created on first access.
		/// </value>
		/// <remarks>
		/// Changes to the collection are reflected in the control: adding a message renders it
		/// (and scrolls it into view), removing it disposes its control, clearing removes all messages,
		/// and moving reorders them. Messages without a <see cref="Message.User"/> are assigned the
		/// current <see cref="User"/>, and messages without a <see cref="Message.Timestamp"/> are
		/// stamped with the current time.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bot = new User("bot", "Assistant");
		/// chatBox.DataSource.Add(new Message("Hi, how can I help?", null, bot));
		/// chatBox.DataSource.RemoveAt(0);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the format string used to display message timestamps.
		/// </summary>
		/// <value>
		/// A standard or custom <see cref="DateTime"/> format string. The initial value is <c>"HH:mm"</c>.
		/// </value>
		/// <remarks>
		/// Note that the designer <see cref="DefaultValueAttribute"/> is declared as <c>"HH:mmm"</c>,
		/// which differs from the initial value <c>"HH:mm"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.TimestampFormat = "dd/MM HH:mm";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the color of the message input text box.
		/// </summary>
		/// <value>
		/// A <see cref="Color"/> applied to the message input text box.
		/// </value>
		/// <remarks>
		/// Unlike the inherited <see cref="Control.ForeColor"/>, this property reads and writes the
		/// <c>BackColor</c> of the inner message input text box.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.ForeColor = Color.WhiteSmoke;
		/// ]]></code>
		/// </example>
		[Description("Gets or sets the color of the message text box.")]
		public override Color ForeColor
		{
			get => this.textBoxMessage.BackColor;
			set => this.textBoxMessage.BackColor = value;
		}

		/// <summary>
		/// Returns the collection of tools displayed in the message input box of the <see cref="ChatBox"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ComponentToolCollection"/> of the inner message input text box.
		/// </value>
		/// <remarks>
		/// Handle <see cref="ToolClick"/> to respond to clicks on the tools.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.Tools.Add(new ComponentTool { Name = "emoji", ImageSource = "icon-emoji", ToolTipText = "Emoji" });
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the current user of the <see cref="ChatBox"/>.
		/// </summary>
		/// <value>
		/// The <see cref="Wisej.Web.Ext.ChatControl.User"/> that authors messages typed in the input box.
		/// The initial value is a user named "User" with a generated id and a default avatar.
		/// </value>
		/// <remarks>
		/// Messages from this user are aligned to the right; messages from other users are aligned to the left.
		/// Changing the user does not update messages already displayed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.User = new User("42", "Alice", "Images/alice.png");
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the message avatar is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> to show the user avatar next to messages; otherwise <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Changing this value does not update messages already displayed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.AvatarVisible = false;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether to display the message timestamp.
		/// </summary>
		/// <value>
		/// <c>true</c> to show the timestamp of each message; otherwise <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Changing this value does not update messages already displayed. See also <see cref="TimestampFormat"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.TimestampVisible = false;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether to show the message input panel (text box and send button).
		/// </summary>
		/// <value>
		/// <c>true</c> to show the input panel; otherwise <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Hide the input panel to use the <see cref="ChatBox"/> as a read-only message viewer.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.InputVisible = false;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the watermark text to show when the message input text box is empty.
		/// </summary>
		/// <value>
		/// The watermark text. The default is <c>"Type a message..."</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.Watermark = "Ask me anything...";
		/// ]]></code>
		/// </example>
		[DefaultValue("Type a message...")]
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
		/// Returns or sets whether the message input text box is multiline.
		/// </summary>
		/// <value>
		/// <c>true</c> to allow multiple lines of input; otherwise <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// When <c>true</c>, the text box accepts returns and automatically resizes its height to fit
		/// the text; pressing Enter without modifiers still sends the message.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.Multiline = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		public bool Multiline
		{
			get => this.textBoxMessage.Multiline;
			set => SetupMultilineTextBox(value);
		}

		/// <summary>
		/// Returns or sets whether the chat control is in read-only mode.
		/// </summary>
		/// <value>
		/// <c>true</c> to make the message input read-only and disable the send button; otherwise
		/// <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Messages can still be added in code through <see cref="DataSource"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.ReadOnly = true;
		/// ]]></code>
		/// </example>
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

		private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
			{
				SendCurrentMessage();

				this._isTyping = false;
				OnTypingEnd(EventArgs.Empty);
			}
			else
			{
				if (!this._isTyping)
				{
					this._isTyping = true;

					OnTypingStart(EventArgs.Empty);
				}
			}
		}
		private bool _isTyping = false;

		private void textBoxMessage_LostFocus(object sender, EventArgs e)
		{
			if (this._isTyping)
			{
				this._isTyping = false;

				OnTypingEnd(EventArgs.Empty);
			}
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			SendCurrentMessage();
		}

		private void SetupMultilineTextBox(bool value)
		{
			if (value == this.textBoxMessage.Multiline)
				return;

			if (value)
			{
				this.textBoxMessage.Multiline = true;
				this.textBoxMessage.AcceptsReturn = true;

				// Re-size the input panel when the text changes.
				if (this._multilineInputListenerId > 0)
					this.textBoxMessage.RemoveClientEventListener(this._multilineInputListenerId);

				this._multilineInputListenerId = this.textBoxMessage.AddClientEventListener("input",
	@"
	let text = this.getValue();
	text = text.replace(/\n/g, '<br/>');
	const font = this.getFont();
	const width = this.getWidth();
	const insets = this.getInsets();
	const size = Wisej.Core.measureText(text + '.', font, width);

	const parent = this.getParent();
	const height = Math.max(30, size.height + insets.top + insets.bottom);
	this.setHeight(height);
	parent.setHeight(height + 20);
");

				// fix for "When TextBox.AcceptsReturn is true, the Enter key doesn't raise KeyDown or KeyUp events on the server. #3575"
				this.textBoxMessage.Eval(
@"
		this.processAccelerator = function (e) {

			if (this.getAcceptsReturn()) {
				if (e.getModifiers() === 0 && e.getKeyIdentifier() === 'Enter') {
					return true;
				}
			}
			else {

				if (e.getModifiers() === qx.event.type.Dom.SHIFT_MASK && e.getKeyIdentifier() === 'Enter') {
					return true;
				}
			}
		}
");

			}
			else
			{
				this.textBoxMessage.Multiline = false;
				this.textBoxMessage.AcceptsReturn = false;

				if (this._multilineInputListenerId > 0)
				{
					this.textBoxMessage.RemoveClientEventListener(this._multilineInputListenerId);
					this._multilineInputListenerId = -1;
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Clears the chat box messages.
		/// </summary>
		/// <remarks>
		/// Clears the <see cref="DataSource"/> collection, which removes and disposes all message controls.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chatBox.Clear();
		/// ]]></code>
		/// </example>
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
				this.panelMessageInput.Height = 50;

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
		/// <param name="index">Index of the new message.</param>
		/// <exception cref="ArgumentNullException"></exception>
		internal void AddInternal(Message message, int index)
		{
			if (message == null)
				throw new ArgumentNullException("message");

			// if the message doesn't have a user, assign it to the current user.
			message.User ??= this.User;

			// if the message doesn't have a timestamp, set the timestamp to now.
			message.Timestamp = message.Timestamp == null ? DateTime.Now : message.Timestamp;

			var isChatBoxUser = this.User == message.User;

			// pre-format messages.
			OnFormatMessage(new MessageEventArgs(isChatBoxUser, message));

			message.RenderMessageControl += Message_RenderMessageControl;

			// allow the container to cancel sending the message..
			if (message.Timestamp == null)
			{
				var args = new SendingMessageEventArgs(isChatBoxUser, message);
				OnSendingMessage(args);

				if (args.Cancel)
					return;
			}

			var messageContainer = new FlexLayoutPanelMessageContainer(message, this)
			{
				HorizontalAlign = GetAlignment(message.User)
			};
			AddToContainer(messageContainer, index);

			// the message has been sent.
			OnSentMessage(new MessageEventArgs(isChatBoxUser, message));
		}

		private HorizontalAlignment GetAlignment(User user)
		{
			return user.Id == this.User.Id ? HorizontalAlignment.Right : HorizontalAlignment.Left;
		}

		private void Message_RenderMessageControl(object sender, RenderMessageControlEventArgs e)
		{
			OnRenderMessageControl(e);
		}

		// adds the container to the list.
		private void AddToContainer(FlexLayoutPanelMessageContainer messageContainer, int index)
		{
			this.flexLayoutPanelMessages.Controls.Add(messageContainer);
			this.flexLayoutPanelMessages.Controls.SetChildIndex(messageContainer, index);
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
						ProcessAdd(e.NewItems, e.NewStartingIndex);
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

		private void ProcessAdd(IList newItems, int index)
		{
			foreach (Message message in newItems)
				AddInternal(message, index);

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
			this.flexLayoutPanelMessages.Controls.Clear(true);
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
		/// Exports the chat history as a JSON string.
		/// </summary>
		/// <returns>A JSON string containing the serialized messages in <see cref="DataSource"/>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// string json = chatBox.ExportAsJson();
		/// Application.Session.ChatHistory = json;
		/// ]]></code>
		/// </example>
		public string ExportAsJson()
		{
			return JSON.Stringify(this.DataSource);
		}

		#endregion

	}
}
