using System;
using System.Drawing;
using System.Linq;

namespace Wisej.Web.Ext.ChatControl
{
	public enum Alignment
	{
		None,
		Left,
		Right
	}

	public partial class MessageContainer : UserControl
	{

		#region Constructors

		public MessageContainer()
		{
			InitializeComponent();
		}

		public MessageContainer(Message message)
		{
			InitializeComponent();

			SetMessage(message);
		}

		#endregion

		#region Properties

		private MessageInfoControl messageInfo;

		/// <summary>
		/// Gets or sets the alignment of the message.
		/// </summary>
		public Alignment Alignment
		{
			get
			{
				return this._alignment;
			}
			set
			{
				if (this._alignment != value) 
				{
					this._alignment = value;

					this.messageInfo.Alignment = value;
				}
			}
		}
		private Alignment _alignment;

		/// <summary>
		/// Gets or sets the bubble color for the message.
		/// </summary>
		public Color BubbleColor
		{
			get
			{
				return this._bubbleColor;
			}
			set
			{
				if (this._bubbleColor != value)
				{
					this._bubbleColor = value;

					UpdateBubbleColor(value);
				}
			}
		}
		private Color _bubbleColor;

		private void UpdateBubbleColor(Color bubbleColor)
		{
			this.messageInfo.BubbleColor = bubbleColor;
		}

		#endregion

		private void AlignVertical(Control insidePanel, Control outsidePanel)
		{
			insidePanel.Location = new Point(
				insidePanel.Left, (outsidePanel.Height - insidePanel.Height) / 2);
		}

		internal void SetMessage(Message message)
		{
			this.messageInfo = new MessageInfoControl(message);
			this.messageInfo.Resize += MessageItem_Resize;

			this.Controls.Add(this.messageInfo);

			this.messageInfo.CenterToParent();
		}

		private void MessageItem_Resize(object? sender, EventArgs e)
		{
			// resize this container to fit the child it contains.
			this.Height = this.messageInfo.Height + 16;
		}
	}
}
