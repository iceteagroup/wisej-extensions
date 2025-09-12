namespace Wisej.Web.Ext.ChatControl
{
	partial class FlexLayoutPanelMessageContainer
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
			this.components = new System.ComponentModel.Container();
			this.animation1 = new Wisej.Web.Animation(this.components);
			this.pictureBoxUser = new Wisej.Web.PictureBox();
			this.flexLayoutPanelContent = new Wisej.Web.FlexLayoutPanel();
			this.labelName = new Wisej.Web.Label();
			this.labelTime = new Wisej.Web.Label();
			this.panelContent = new Wisej.Web.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).BeginInit();
			this.flexLayoutPanelContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxUser
			// 
			this.pictureBoxUser.CssStyle = "border-radius: 50px;";
			this.pictureBoxUser.ImageSource = "file-pdf";
			this.pictureBoxUser.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxUser.MaximumSize = new System.Drawing.Size(0, 40);
			this.pictureBoxUser.Name = "pictureBoxUser";
			this.pictureBoxUser.Size = new System.Drawing.Size(40, 40);
			this.pictureBoxUser.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
			// 
			// flexLayoutPanelContent
			// 
			this.flexLayoutPanelContent.AutoSize = true;
			this.flexLayoutPanelContent.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.flexLayoutPanelContent.Controls.Add(this.labelName);
			this.flexLayoutPanelContent.Controls.Add(this.labelTime);
			this.flexLayoutPanelContent.Controls.Add(this.panelContent);
			this.flexLayoutPanelContent.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
			this.flexLayoutPanelContent.Location = new System.Drawing.Point(59, 3);
			this.flexLayoutPanelContent.Name = "flexLayoutPanelContent";
			this.flexLayoutPanelContent.Size = new System.Drawing.Size(61, 54);
			this.flexLayoutPanelContent.Spacing = 0;
			this.flexLayoutPanelContent.TabIndex = 6;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("@defaultBold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.labelName.Location = new System.Drawing.Point(3, 3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(55, 13);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Unknown";
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.Font = new System.Drawing.Font("default", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.labelTime.Location = new System.Drawing.Point(3, 22);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(55, 13);
			this.labelTime.TabIndex = 2;
			this.labelTime.Text = "Unknown";
			// 
			// panelContent
			// 
			this.panelContent.AutoSize = true;
			this.panelContent.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.panelContent.CssStyle = "border-radius: 4px;";
			this.panelContent.Location = new System.Drawing.Point(0, 38);
			this.panelContent.Margin = new Wisej.Web.Padding(0);
			this.panelContent.MinimumSize = new System.Drawing.Size(30, 15);
			this.panelContent.Name = "panelContent";
			this.panelContent.Padding = new Wisej.Web.Padding(8);
			this.panelContent.Size = new System.Drawing.Size(61, 16);
			this.panelContent.TabIndex = 1;
			// 
			// FlexLayoutPanelMessageContainer
			// 
			this.animation1.GetAnimation(this).Duration = 150;
			this.animation1.GetAnimation(this).Event = "appear";
			this.animation1.GetAnimation(this).Name = "slideUpIn";
			this.AutoSize = true;
			this.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pictureBoxUser);
			this.Controls.Add(this.flexLayoutPanelContent);
			this.LayoutStyle = Wisej.Web.FlexLayoutStyle.Horizontal;
			this.Name = "FlexLayoutPanelMessageContainer";
			this.Size = new System.Drawing.Size(123, 60);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).EndInit();
			this.flexLayoutPanelContent.ResumeLayout(false);
			this.flexLayoutPanelContent.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Animation animation1;
		private PictureBox pictureBoxUser;
		private FlexLayoutPanel flexLayoutPanelContent;
		private Label labelName;
		private Label labelTime;
		private Panel panelContent;
	}
}
