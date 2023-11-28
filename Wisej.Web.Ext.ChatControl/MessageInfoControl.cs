using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Wisej.Web.Ext.ChatControl
{
	public partial class MessageInfoControl : UserControl
	{

		private Message message;

		public MessageInfoControl()
		{
			InitializeComponent();
		}

		public MessageInfoControl(Message message)
		{
			InitializeComponent();

			this.message = message;

			Initialize();
		}

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

					UpdateAlignment(value);
				}
			}
		}
		private Alignment _alignment;

		private void UpdateAlignment(Alignment alignment)
		{
			switch (alignment)
			{
				case Alignment.Left:
					this.Left = 8;
					this.Anchor = AnchorStyles.Left;
					break;

				case Alignment.Right:
					this.Anchor = AnchorStyles.Right;
					this.Left = this.Parent.Width - this.Width - 8;
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Gets or sets the bubble color.
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

					this.panelData.BackColor = value;
				}
			}
		}
		private Color _bubbleColor;

		/// <summary>
		/// Gets or sets the current timestamp format.
		/// </summary>
		public string TimestampFormat
		{
			get
			{
				return this._timestampFormat;
			}
			set
			{
				if (this._timestampFormat != value) 
				{
					this._timestampFormat = value;

					UpdateTimestampFormat(value);
				}
			}
		}
		private string _timestampFormat = "HH:mm";

		private void UpdateTimestampFormat(string format)
		{
			this.labelTime.Text = this.message.DateTime?.ToString(format);
		}

		private void Initialize()
		{
			this.labelName.Text = this.message.User.Name;

			this.labelTime.Text = this.message.DateTime?.ToString(this.TimestampFormat);

			if (this.message.User.Image != null)
				this.pictureBoxUser.Image = this.message.User.Image;

			this.message.Control.Margin = new Padding(0);

			this.panelData.Controls.Add(this.message.Control);
		}

		private void panelData_Resize(object sender, EventArgs e)
		{
			// resize this container to fit it's contents.
			this.Height = this.panelData.Height + this.labelName.Height + 16;
		}
	}
}
