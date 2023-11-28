namespace Wisej.Web.Ext.ChatControl
{
	partial class MessageInfoControl
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
			this.labelName = new Wisej.Web.Label();
			this.panelData = new Wisej.Web.Panel();
			this.pictureBoxUser = new Wisej.Web.PictureBox();
			this.labelTime = new Wisej.Web.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).BeginInit();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("@defaultBold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.labelName.Location = new System.Drawing.Point(54, 3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(63, 18);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Unknown";
			// 
			// panelData
			// 
			this.panelData.AutoSize = true;
			this.panelData.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.panelData.Location = new System.Drawing.Point(54, 38);
			this.panelData.Margin = new Wisej.Web.Padding(0);
			this.panelData.MinimumSize = new System.Drawing.Size(30, 15);
			this.panelData.Name = "panelData";
			this.panelData.Size = new System.Drawing.Size(30, 15);
			this.panelData.TabIndex = 1;
			this.panelData.Resize += new System.EventHandler(this.panelData_Resize);
			// 
			// pictureBoxUser
			// 
			this.pictureBoxUser.CssStyle = "border-radius: 50px;";
			this.pictureBoxUser.ImageSource = "file-pdf";
			this.pictureBoxUser.Location = new System.Drawing.Point(8, 9);
			this.pictureBoxUser.Name = "pictureBoxUser";
			this.pictureBoxUser.Size = new System.Drawing.Size(40, 40);
			this.pictureBoxUser.SizeMode = Wisej.Web.PictureBoxSizeMode.Cover;
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.Font = new System.Drawing.Font("default", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.labelTime.Location = new System.Drawing.Point(54, 18);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(46, 14);
			this.labelTime.TabIndex = 2;
			this.labelTime.Text = "Unknown";
			// 
			// MessageInfoControl
			// 
			this.AutoSize = true;
			this.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.labelTime);
			this.Controls.Add(this.pictureBoxUser);
			this.Controls.Add(this.panelData);
			this.Controls.Add(this.labelName);
			this.Margin = new Wisej.Web.Padding(3, 3, 0, 3);
			this.Name = "MessageInfoControl";
			this.Size = new System.Drawing.Size(120, 53);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label labelName;
		private Panel panelData;
		private PictureBox pictureBoxUser;
		private Label labelTime;
	}
}
