namespace Wisej.Web.Ext.ChatControl
{
	partial class ChatBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Wisej Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxMessage = new Wisej.Web.TextBox();
			this.flexLayoutPanelMessages = new Wisej.Web.FlexLayoutPanel();
			this.panelMessageInput = new Wisej.Web.Panel();
			this.buttonSend = new Wisej.Web.Button();
			this.panelMessageInput.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.AutoSize = false;
			this.textBoxMessage.AllowDrop = true;
			this.textBoxMessage.Anchor = AnchorStyles.None;
			this.textBoxMessage.AppearanceKey = "messageTextBox";
			this.textBoxMessage.BackColor = System.Drawing.Color.FromName("@toolbar");
			this.textBoxMessage.BorderStyle = Wisej.Web.BorderStyle.None;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Padding = new Wisej.Web.Padding(8, 0, 0, 0);
			this.textBoxMessage.TabIndex = 0;
			this.textBoxMessage.Dock = Wisej.Web.DockStyle.Fill;
			this.textBoxMessage.Watermark = "Type a message...";
			this.textBoxMessage.LostFocus += new System.EventHandler(this.textBoxMessage_LostFocus);
			this.textBoxMessage.Size  = new System.Drawing.Size(466, 30);
			this.textBoxMessage.KeyDown += new Wisej.Web.KeyEventHandler(this.textBoxMessage_KeyDown);
			// 
			// flexLayoutPanelMessages
			// 
			this.flexLayoutPanelMessages.AutoScroll = true;
			this.flexLayoutPanelMessages.AutoScrollMargin = new System.Drawing.Size(0, 20);
			this.flexLayoutPanelMessages.CssStyle = "border-radius:0px;";
			this.flexLayoutPanelMessages.Dock = Wisej.Web.DockStyle.Fill;
			this.flexLayoutPanelMessages.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
			this.flexLayoutPanelMessages.Location = new System.Drawing.Point(16, 16);
			this.flexLayoutPanelMessages.Name = "flexLayoutPanelMessages";
			this.flexLayoutPanelMessages.ScrollBars = Wisej.Web.ScrollBars.Vertical;
			this.flexLayoutPanelMessages.Size = new System.Drawing.Size(466, 516);
			this.flexLayoutPanelMessages.TabIndex = 1;
			// 
			// panelMessageInput
			// 
			this.panelMessageInput.BackColor = System.Drawing.Color.FromName("@toolbar");
			this.panelMessageInput.Controls.Add(this.textBoxMessage);
			this.panelMessageInput.Controls.Add(this.buttonSend);
			this.panelMessageInput.CssStyle = "border-radius: 18px;";
			this.panelMessageInput.Dock = Wisej.Web.DockStyle.Bottom;
			this.panelMessageInput.Location = new System.Drawing.Point(16, 532);
			this.panelMessageInput.Name = "panelMessageInput";
			this.panelMessageInput.Size = new System.Drawing.Size(466, 50);
			this.panelMessageInput.TabIndex = 2;
			this.panelMessageInput.Padding = new Wisej.Web.Padding(10);
			this.panelMessageInput.MinimumSize = new System.Drawing.Size(0, 50);
			this.panelMessageInput.MaximumSize = new System.Drawing.Size(0, 150);
			// 
			// buttonSend
			// 
			this.buttonSend.BackColor = System.Drawing.Color.FromName("@toolbar");
			this.buttonSend.BorderStyle = Wisej.Web.BorderStyle.None;
			this.buttonSend.CssStyle = "border-radius: 0px;";
			this.buttonSend.Dock = Wisej.Web.DockStyle.Right;
			this.buttonSend.Focusable = false;
			this.buttonSend.ImageSource = "icon-right";
			this.buttonSend.Location = new System.Drawing.Point(416, 0);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(50, 50);
			this.buttonSend.TabIndex = 1;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// ChatBox
			// 
			this.BackColor = System.Drawing.Color.FromName("@window");
			this.BorderStyle = Wisej.Web.BorderStyle.Solid;
			this.Controls.Add(this.flexLayoutPanelMessages);
			this.Controls.Add(this.panelMessageInput);
			this.Name = "ChatBox";
			this.Padding = new Wisej.Web.Padding(16);
			this.ScrollBars = Wisej.Web.ScrollBars.Vertical;
			this.Size = new System.Drawing.Size(500, 600);
			this.panelMessageInput.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TextBox textBoxMessage;
		private FlexLayoutPanel flexLayoutPanelMessages;
		private Panel panelMessageInput;
		private Button buttonSend;
	}
}
