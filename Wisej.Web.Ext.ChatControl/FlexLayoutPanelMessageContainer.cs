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
	/// A container for a message.
	/// </summary>
	public partial class FlexLayoutPanelMessageContainer : FlexLayoutPanel
	{

		#region Constructors

		/// <summary>
		/// Creates a new instance of <see cref="FlexLayoutPanelMessageContainer"/>.
		/// </summary>
		public FlexLayoutPanelMessageContainer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new instance of <see cref="FlexLayoutPanelMessageContainer"/> with the given message and owner.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="owner"></param>
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
		/// Gets or sets the ChatBox owner for this control.
		/// </summary>
		public ChatBox ChatBox { get; set; }

		/// <summary>
		/// Gets or sets the Message for this control.
		/// </summary>
		public Message Message { get; set; }

		/// <summary>
		/// The control displayed in the message.
		/// </summary>
		public Control MessageControl { get; set; }

		/// <summary>
		/// Gets or sets the bubble color for the message.
		/// </summary>
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
