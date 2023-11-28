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
			Wisej.Web.ComponentTool componentTool1 = new Wisej.Web.ComponentTool();
			Wisej.Web.ComponentTool componentTool2 = new Wisej.Web.ComponentTool();
			this.textBoxMessage = new Wisej.Web.TextBox();
			this.panelMessages = new Wisej.Web.Panel();
			this.upload1 = new Wisej.Web.Upload();
			this.SuspendLayout();
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.AllowDrop = true;
			this.textBoxMessage.AppearanceKey = "messageTextBox";
			this.textBoxMessage.AutoSize = false;
			this.textBoxMessage.BackColor = System.Drawing.Color.FromName("@toolbar");
			this.textBoxMessage.BorderStyle = Wisej.Web.BorderStyle.None;
			this.textBoxMessage.CssStyle = "border-radius: 18px;";
			this.textBoxMessage.Dock = Wisej.Web.DockStyle.Bottom;
			this.textBoxMessage.Location = new System.Drawing.Point(16, 539);
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Padding = new Wisej.Web.Padding(8, 0, 0, 0);
			this.textBoxMessage.Size = new System.Drawing.Size(466, 43);
			this.textBoxMessage.TabIndex = 0;
			this.textBoxMessage.LostFocus += TextBoxMessage_LostFocus;
			componentTool1.ImageSource = "icon-upload";
			componentTool1.Name = "File";
			componentTool1.Position = Wisej.Web.LeftRightAlignment.Left;
			componentTool2.ImageSource = "icon-right";
			componentTool2.Name = "Post";
			this.textBoxMessage.Tools.AddRange(new Wisej.Web.ComponentTool[] {
            componentTool1,
            componentTool2});
			this.textBoxMessage.Watermark = "Type a message...";
			this.textBoxMessage.ToolClick += new Wisej.Web.ToolClickEventHandler(this.textBoxMessage_ToolClick);
			this.textBoxMessage.KeyUp += new Wisej.Web.KeyEventHandler(this.textBoxMessage_KeyUp);
			// 
			// panelMessages
			// 
			this.panelMessages.AutoScroll = true;
			this.panelMessages.AutoScrollMargin = new System.Drawing.Size(0, 20);
			this.panelMessages.CssStyle = "border-radius:0px;";
			this.panelMessages.Dock = Wisej.Web.DockStyle.Fill;
			this.panelMessages.Location = new System.Drawing.Point(16, 16);
			this.panelMessages.Name = "panelMessages";
			this.panelMessages.ScrollBars = Wisej.Web.ScrollBars.Vertical;
			this.panelMessages.Size = new System.Drawing.Size(466, 523);
			this.panelMessages.TabIndex = 1;
			// 
			// upload1
			// 
			this.upload1.Location = new System.Drawing.Point(110, 44);
			this.upload1.Name = "upload1";
			this.upload1.Size = new System.Drawing.Size(200, 30);
			this.upload1.TabIndex = 0;
			this.upload1.Text = "upload1";
			this.upload1.Visible = false;
			this.upload1.Uploaded += new Wisej.Web.UploadedEventHandler(this.upload1_Uploaded);
			// 
			// ChatBox
			// 
			this.BackColor = System.Drawing.Color.FromName("@window");
			this.BorderStyle = Wisej.Web.BorderStyle.Solid;
			this.Controls.Add(this.panelMessages);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.upload1);
			this.Name = "ChatBox";
			this.Padding = new Wisej.Web.Padding(16);
			this.ScrollBars = Wisej.Web.ScrollBars.Vertical;
			this.Size = new System.Drawing.Size(500, 600);
			this.ResumeLayout(false);

		}

		#endregion

		private TextBox textBoxMessage;
		private Panel panelMessages;
		private Upload upload1;
	}
}
