namespace Wisej.Web.Ext.NavigationBar
{
	partial class NavigationBar
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
            this.header = new Wisej.Web.FlexLayoutPanel();
            this.logo = new Wisej.Web.PictureBox();
            this.title = new Wisej.Web.Label();
            this.user = new Wisej.Web.FlexLayoutPanel();
            this.avatar = new Wisej.Web.PictureBox();
            this.userInfo = new Wisej.Web.FlexLayoutPanel();
            this.userName = new Wisej.Web.Label();
            this.userStatus = new Wisej.Web.FlexLayoutPanel();
            this.userStatusColor = new Wisej.Web.Label();
            this.userStatusName = new Wisej.Web.Label();
            this.items = new Wisej.Web.FlexLayoutPanel();
            this.slideBar = new Wisej.Web.SlideBar();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.user.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.userInfo.SuspendLayout();
            this.userStatus.SuspendLayout();
            this.slideBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Controls.Add(this.logo);
            this.header.Controls.Add(this.title);
            this.header.Cursor = Wisej.Web.Cursors.Hand;
            this.header.LayoutStyle = Wisej.Web.FlexLayoutStyle.Horizontal;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Margin = new Wisej.Web.Padding(0);
            this.header.Name = "header";
            this.header.Padding = new Wisej.Web.Padding(20, 0, 20, 0);
            this.header.Size = new System.Drawing.Size(282, 64);
            this.header.TabIndex = 0;
            this.header.TabStop = true;
            this.header.Click += new System.EventHandler(this.header_Click);
            // 
            // logo
            // 
            this.logo.Anonymous = true;
            this.logo.Location = new System.Drawing.Point(23, 20);
            this.logo.Margin = new Wisej.Web.Padding(3, 20, 3, 20);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(24, 24);
            this.logo.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
            // 
            // title
            // 
            this.title.Anonymous = true;
            this.title.AutoSize = true;
            this.header.SetFillWeight(this.title, 1);
            this.title.Font = new System.Drawing.Font("defaultBold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.title.Location = new System.Drawing.Point(63, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(196, 58);
            this.title.TabIndex = 1;
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // user
            // 
            this.user.Controls.Add(this.avatar);
            this.user.Controls.Add(this.userInfo);
            this.user.Cursor = Wisej.Web.Cursors.Hand;
            this.user.LayoutStyle = Wisej.Web.FlexLayoutStyle.Horizontal;
            this.user.Location = new System.Drawing.Point(0, 506);
            this.user.Margin = new Wisej.Web.Padding(0);
            this.user.Name = "user";
            this.user.Padding = new Wisej.Web.Padding(20, 12, 20, 12);
            this.user.Size = new System.Drawing.Size(282, 72);
            this.user.TabIndex = 1;
            this.user.TabStop = true;
            this.user.Click += new System.EventHandler(this.user_Click);
            // 
            // avatar
            // 
            this.user.SetAlignY(this.avatar, Wisej.Web.VerticalAlignment.Middle);
            this.avatar.Anonymous = true;
            this.avatar.AppearanceKey = "navbar/user-avatar";
            this.avatar.Location = new System.Drawing.Point(23, 16);
            this.avatar.MaximumSize = new System.Drawing.Size(40, 40);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(40, 40);
            this.avatar.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
            // 
            // userInfo
            // 
            this.userInfo.Anonymous = true;
            this.userInfo.Controls.Add(this.userName);
            this.userInfo.Controls.Add(this.userStatus);
            this.user.SetFillWeight(this.userInfo, 1);
            this.userInfo.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
            this.userInfo.Location = new System.Drawing.Point(79, 15);
            this.userInfo.Name = "userInfo";
            this.userInfo.Size = new System.Drawing.Size(180, 42);
            this.userInfo.Spacing = 0;
            this.userInfo.TabIndex = 1;
            this.userInfo.TabStop = true;
            // 
            // userName
            // 
            this.userName.Anonymous = true;
            this.userName.AutoEllipsis = true;
            this.userInfo.SetFillWeight(this.userName, 1);
            this.userName.Font = new System.Drawing.Font("@defaultBold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Margin = new Wisej.Web.Padding(0, 0, 0, 5);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(180, 19);
            this.userName.TabIndex = 1;
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userStatus
            // 
            this.userStatus.Anonymous = true;
            this.userStatus.Controls.Add(this.userStatusColor);
            this.userStatus.Controls.Add(this.userStatusName);
            this.userInfo.SetFillWeight(this.userStatus, 1);
            this.userStatus.LayoutStyle = Wisej.Web.FlexLayoutStyle.Horizontal;
            this.userStatus.Location = new System.Drawing.Point(0, 24);
            this.userStatus.Margin = new Wisej.Web.Padding(0);
            this.userStatus.Name = "userStatus";
            this.userStatus.Size = new System.Drawing.Size(180, 18);
            this.userStatus.TabIndex = 2;
            this.userStatus.TabStop = true;
            // 
            // userStatusColor
            // 
            this.userStatus.SetAlignY(this.userStatusColor, Wisej.Web.VerticalAlignment.Middle);
            this.userStatusColor.Anonymous = true;
            this.userStatusColor.AppearanceKey = "navbar/user-status";
            this.userStatusColor.BackColor = System.Drawing.Color.Chartreuse;
            this.userStatusColor.Location = new System.Drawing.Point(0, 2);
            this.userStatusColor.Margin = new Wisej.Web.Padding(0);
            this.userStatusColor.MaximumSize = new System.Drawing.Size(14, 14);
            this.userStatusColor.Name = "userStatusColor";
            this.userStatusColor.Size = new System.Drawing.Size(14, 14);
            this.userStatusColor.TabIndex = 0;
            // 
            // userStatusName
            // 
            this.userStatusName.Anonymous = true;
            this.userStatus.SetFillWeight(this.userStatusName, 1);
            this.userStatusName.Location = new System.Drawing.Point(27, 3);
            this.userStatusName.Name = "userStatusName";
            this.userStatusName.Size = new System.Drawing.Size(150, 12);
            this.userStatusName.TabIndex = 1;
            this.userStatusName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // items
            // 
            this.items.AutoSize = true;
            this.items.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
            this.items.Dock = Wisej.Web.DockStyle.Top;
            this.items.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
            this.items.Location = new System.Drawing.Point(3, 3);
            this.items.Name = "items";
            this.items.Size = new System.Drawing.Size(276, 0);
            this.items.Spacing = 0;
            this.items.TabIndex = 2;
            this.items.TabStop = true;
            this.items.ControlAdded += new Wisej.Web.ControlEventHandler(this.items_ControlAdded);
            // 
            // slideBar
            // 
            this.slideBar.Controls.Add(this.items);
            this.SetFillWeight(this.slideBar, 1);
            this.slideBar.Location = new System.Drawing.Point(0, 74);
            this.slideBar.Margin = new Wisej.Web.Padding(0);
            this.slideBar.Name = "slideBar";
            this.slideBar.Orientation = Wisej.Web.Orientation.Vertical;
            this.slideBar.Size = new System.Drawing.Size(282, 422);
            this.slideBar.TabIndex = 3;
            // 
            // NavigationBar
            // 
            this.AppearanceKey = "navbar";
            this.Controls.Add(this.header);
            this.Controls.Add(this.slideBar);
            this.Controls.Add(this.user);
            this.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
            this.Name = "NavigationBar";
            this.Size = new System.Drawing.Size(282, 578);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.user.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.userInfo.ResumeLayout(false);
            this.userStatus.ResumeLayout(false);
            this.slideBar.ResumeLayout(false);
            this.slideBar.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private FlexLayoutPanel user;
		private Label userStatusColor;
		protected FlexLayoutPanel header;
		protected PictureBox logo;
		protected Label title;
		protected FlexLayoutPanel items;
		protected PictureBox avatar;
		protected FlexLayoutPanel userInfo;
		protected Label userName;
		protected FlexLayoutPanel userStatus;
		protected Label userStatusName;
		private SlideBar slideBar;
	}
}
