namespace Wisej.Web.Ext.NavigationBar
{
	partial class NavigationBarItem
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
			this.icon = new Wisej.Web.PictureBox();
			this.title = new Wisej.Web.Label();
			this.shortcut = new Wisej.Web.PictureBox();
			this.info = new Wisej.Web.Label();
			this.open = new Wisej.Web.PictureBox();
			this.items = new Wisej.Web.FlexLayoutPanel();
			this.header.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.shortcut)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.open)).BeginInit();
			this.SuspendLayout();
			// 
			// header
			// 
			this.header.AppearanceKey = "navbar-item/header";
			this.header.Controls.Add(this.icon);
			this.header.Controls.Add(this.title);
			this.header.Controls.Add(this.shortcut);
			this.header.Controls.Add(this.info);
			this.header.Controls.Add(this.open);
			this.header.Cursor = Wisej.Web.Cursors.Hand;
			this.header.LayoutStyle = Wisej.Web.FlexLayoutStyle.Horizontal;
			this.header.Location = new System.Drawing.Point(0, 0);
			this.header.Margin = new Wisej.Web.Padding(0);
			this.header.Name = "header";
			this.header.Padding = new Wisej.Web.Padding(25, 0, 20, 0);
			this.header.Size = new System.Drawing.Size(502, 45);
			this.header.Spacing = 12;
			this.header.TabIndex = 1;
			this.header.TabStop = true;
			this.header.Click += new System.EventHandler(this.header_Click);
			// 
			// icon
			// 
			this.header.SetAlignY(this.icon, Wisej.Web.VerticalAlignment.Middle);
			this.icon.Anonymous = true;
			this.icon.Location = new System.Drawing.Point(25, 12);
			this.icon.Margin = new Wisej.Web.Padding(0);
			this.icon.MaximumSize = new System.Drawing.Size(20, 20);
			this.icon.Name = "icon";
			this.icon.Size = new System.Drawing.Size(20, 20);
			this.icon.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
			// 
			// title
			// 
			this.title.Anonymous = true;
			this.title.AutoEllipsis = true;
			this.header.SetFillWeight(this.title, 1);
			this.title.Location = new System.Drawing.Point(57, 0);
			this.title.Margin = new Wisej.Web.Padding(0);
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size(351, 45);
			this.title.TabIndex = 1;
			this.title.Text = "label1";
			this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// shortcut
			// 
			this.header.SetAlignY(this.shortcut, Wisej.Web.VerticalAlignment.Middle);
			this.shortcut.ImageSource = "spinner-plus";
			this.shortcut.Location = new System.Drawing.Point(420, 12);
			this.shortcut.Margin = new Wisej.Web.Padding(0);
			this.shortcut.MaximumSize = new System.Drawing.Size(20, 20);
			this.shortcut.Name = "shortcut";
			this.shortcut.Size = new System.Drawing.Size(18, 20);
			this.shortcut.Visible = false;
			this.shortcut.Click += new System.EventHandler(this.shortcut_Click);
			// 
			// info
			// 
			this.header.SetAlignY(this.info, Wisej.Web.VerticalAlignment.Middle);
			this.info.AppearanceKey = "navbar-item/info";
			this.info.AutoSize = true;
			this.info.Location = new System.Drawing.Point(450, 12);
			this.info.Margin = new Wisej.Web.Padding(0);
			this.info.MaximumSize = new System.Drawing.Size(0, 20);
			this.info.Name = "info";
			this.info.Size = new System.Drawing.Size(4, 20);
			this.info.TabIndex = 3;
			this.info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.info.Visible = false;
			this.info.Click += new System.EventHandler(this.info_Click);
			// 
			// open
			// 
			this.header.SetAlignY(this.open, Wisej.Web.VerticalAlignment.Middle);
			this.open.AppearanceKey = "navbar-item/open";
			this.open.Location = new System.Drawing.Point(466, 12);
			this.open.Margin = new Wisej.Web.Padding(0);
			this.open.MaximumSize = new System.Drawing.Size(20, 20);
			this.open.Name = "open";
			this.open.Size = new System.Drawing.Size(16, 20);
			this.open.Visible = false;
			this.open.Click += new System.EventHandler(this.open_Click);
			// 
			// items
			// 
			this.items.AppearanceKey = "navbar-item/items";
			this.items.AutoSize = true;
			this.items.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.items.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
			this.items.Location = new System.Drawing.Point(0, 45);
			this.items.Margin = new Wisej.Web.Padding(0);
			this.items.Name = "items";
			this.items.Size = new System.Drawing.Size(502, 0);
			this.items.Spacing = 0;
			this.items.TabIndex = 0;
			this.items.TabStop = true;
			this.items.Visible = false;
			this.items.ControlAdded += new Wisej.Web.ControlEventHandler(this.items_ControlAdded);
			this.items.ControlRemoved += new Wisej.Web.ControlEventHandler(this.items_ControlRemoved);
			// 
			// NavigationBarItem
			// 
			this.AppearanceKey = "navbar-item";
			this.AutoSize = true;
			this.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.header);
			this.Controls.Add(this.items);
			this.Cursor = Wisej.Web.Cursors.Hand;
			this.LayoutStyle = Wisej.Web.FlexLayoutStyle.Vertical;
			this.Margin = new Wisej.Web.Padding(0);
			this.Name = "NavigationBarItem";
			this.Size = new System.Drawing.Size(502, 45);
			this.Spacing = 0;
			this.header.ResumeLayout(false);
			this.header.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.shortcut)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.open)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected FlexLayoutPanel items;
		protected FlexLayoutPanel header;
		protected PictureBox shortcut;
		protected Label info;
		protected PictureBox open;
		protected Label title;
		protected PictureBox icon;
	}
}
