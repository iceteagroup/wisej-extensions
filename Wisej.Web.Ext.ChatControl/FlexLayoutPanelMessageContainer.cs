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
	/// A <see cref="FlexLayoutPanel"/> that displays a single <see cref="ChatControl.Message"/> inside a <see cref="ChatControl.ChatBox"/>,
	/// including the user's avatar, name, timestamp and the message bubble.
	/// </summary>
	/// <remarks>
	/// The <see cref="ChatControl.ChatBox"/> creates one container for each message added to its
	/// <see cref="ChatControl.ChatBox.DataSource"/>. The container requests the message control
	/// (see <see cref="ChatControl.ChatBox.RenderMessageControl"/>), sizes it to at most half the width of the
	/// chat box (up to 600 pixels), and applies the bubble color from <see cref="User.BubbleColor"/> or,
	/// when not set, the theme colors <c>@highlight</c> (current user) or <c>@controlDark</c> (other users).
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var message = new Message("Hello!", null, chatBox.User);
	/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
	/// container.BubbleColor = Color.LightBlue;
	/// ]]></code>
	/// </example>
	public partial class FlexLayoutPanelMessageContainer : FlexLayoutPanel
	{

		#region Constructors

		/// <summary>
		/// Creates a new, empty instance of <see cref="FlexLayoutPanelMessageContainer"/>.
		/// </summary>
		/// <remarks>
		/// This constructor only initializes the child controls; it does not bind a message or owner.
		/// Use <see cref="FlexLayoutPanelMessageContainer(ChatControl.Message, ChatControl.ChatBox)"/> to create a fully initialized container.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var container = new FlexLayoutPanelMessageContainer();
		/// ]]></code>
		/// </example>
		public FlexLayoutPanelMessageContainer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new instance of <see cref="FlexLayoutPanelMessageContainer"/> with the given message and owner.
		/// </summary>
		/// <param name="message">The <see cref="ChatControl.Message"/> to display. Its <see cref="ChatControl.Message.User"/> must not be <c>null</c>.</param>
		/// <param name="owner">The <see cref="ChatControl.ChatBox"/> that owns this container. Its <see cref="ChatControl.ChatBox.User"/> must not be <c>null</c>.</param>
		/// <remarks>
		/// The constructor sets the timestamp (formatted with <see cref="ChatControl.ChatBox.TimestampFormat"/>),
		/// the avatar and timestamp visibility, the bubble color, the user name and image, and requests
		/// the message control, which is added to the bubble. The message control is resized whenever the owner is resized.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var message = new Message("How can I help?", null, new User("bot", "Assistant"));
		/// message.Timestamp = DateTime.Now;
		/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
		/// ]]></code>
		/// </example>
		public FlexLayoutPanelMessageContainer(Message message, ChatBox owner)
		{
			InitializeComponent();

			this.Message = message;
			this.ChatBox = owner;

			Initialize();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="ChatControl.ChatBox"/> that owns this container.
		/// </summary>
		/// <value>The owner <see cref="ChatControl.ChatBox"/>, or <c>null</c> when created with the parameterless constructor.</value>
		/// <example>
		/// <code><![CDATA[
		/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
		/// var format = container.ChatBox.TimestampFormat;
		/// ]]></code>
		/// </example>
		public ChatBox ChatBox { get; set; }

		/// <summary>
		/// Returns or sets the <see cref="ChatControl.Message"/> displayed by this container.
		/// </summary>
		/// <value>The displayed <see cref="ChatControl.Message"/>, or <c>null</c> when created with the parameterless constructor.</value>
		/// <example>
		/// <code><![CDATA[
		/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
		/// var sender = container.Message.User?.Name;
		/// ]]></code>
		/// </example>
		public Message Message { get; set; }

		/// <summary>
		/// Returns or sets the control displayed inside the message bubble.
		/// </summary>
		/// <value>
		/// The control returned by the <see cref="ChatControl.ChatBox.RenderMessageControl"/> event, or an
		/// <see cref="AutoSizeLabel"/> showing <see cref="ChatControl.Message.Content"/> by default.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
		/// container.MessageControl.ForeColor = Color.White;
		/// ]]></code>
		/// </example>
		public Control MessageControl { get; set; }

		/// <summary>
		/// Returns or sets the background color of the message bubble.
		/// </summary>
		/// <value>
		/// The back color of the bubble panel. Initialized from <see cref="User.BubbleColor"/>, or
		/// <c>@highlight</c> / <c>@controlDark</c> when not set, or <see cref="Color.Transparent"/>
		/// when <see cref="ChatControl.Message.BubbleVisible"/> is <c>false</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var container = new FlexLayoutPanelMessageContainer(message, chatBox);
		/// container.BubbleColor = Color.LightGreen;
		/// ]]></code>
		/// </example>
		public Color BubbleColor
		{
			get => this.panelContent.BackColor;
			set => this.panelContent.BackColor = value;
		}
		private Color _bubbleColor;

		#endregion

		private void Initialize()
		{
			UpdateTimestamp();
			UpdateVisibility();
			UpdateBubbleColor();

			this.ChatBox.Resize += (s, e) => UpdatePreferredSize();

			var message = this.Message;
			var user = message.User;
			
			this.labelName.Text = user?.Name;
			this.pictureBoxUser.ImageSource = user?.ImageSource;
			this.MessageControl = this.Message.RequestControl();
			this.MessageControl.Location = new Point(8, 8);

			this.MessageControl.TextChanged += (s, e) => UpdatePreferredSize();

			UpdatePreferredSize();

			this.panelContent.Controls.Add(this.MessageControl);
		}

		private void UpdatePreferredSize()
		{
			var maxSize = new Size(Math.Min(this.ChatBox.Size.Width / 2, 600), 0);

			this.MessageControl.Size = this.MessageControl.GetPreferredSize(maxSize);
		}

		private void UpdateVisibility()
		{
			this.labelTime.Visible = this.ChatBox.TimestampVisible;
			this.pictureBoxUser.Visible = this.ChatBox.AvatarVisible;
		}

		/// <summary>
		/// Updates the timestamp of this message.
		/// </summary>
		private void UpdateTimestamp()
		{
			this.labelTime.Text = this.Message.Timestamp?.ToString(this.ChatBox.TimestampFormat);
		}

		private void UpdateBubbleColor()
		{
			// hide the bubble color?
			if (!this.Message.BubbleVisible)
			{
				this.BubbleColor = Color.Transparent;
				return;
			}

			// apply bubble color.
			var bubbleColor = this.Message.User.BubbleColor;
			if (bubbleColor != null)
			{
				this.BubbleColor = bubbleColor.Value;
			}
			else // otherwise use default colors.
			{
				if (this.ChatBox.User.Id == this.Message.User.Id)
				{
					this.BubbleColor = Color.FromName("@highlight");
				}
				else
				{
					this.BubbleColor = Color.FromName("@controlDark");
				}
			}
		}
	}
}
