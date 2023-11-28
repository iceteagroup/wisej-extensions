using System;
using System.Drawing;
using System.IO;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A simple message control for displaying files.
	/// </summary>
	public class FileMessageControl : MessageControl
	{

		private Stream _stream;

		#region Constructors

		public FileMessageControl() 
		{ 
			InitializeComponent();
		}

		public FileMessageControl(Stream file, string fileName, string contentType="")
		{
			InitializeComponent();

			this._stream = file;
			this.labelFileName.Text = $"{fileName} ({FormatFileSize(file.Length)})";

			if (!string.IsNullOrEmpty(contentType))
			{
				switch (contentType) 
				{
					case "image/png":
					case "image/jpg":
					case "image/jpeg":
					case "image/gif":
						this.pictureBox1.Image = Image.FromStream(file);
						this.pictureBox1.SizeMode = PictureBoxSizeMode.Cover;
						break;

					case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
						this.pictureBox1.ImageSource = "file-excel";
						break;

					case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
						this.pictureBox1.ImageSource = "file-word";
						break;

					case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
						this.pictureBox1.ImageSource = "file-powerpoint";
						break;

					case "application/pdf":
						this.pictureBox1.ImageSource = "file-pdf";
						break;

					default:
						break;
				}
			}
		}

		#endregion

		#region Initialization

		private PictureBox pictureBox1;
		private System.ComponentModel.IContainer components;
		private Label labelFileName;

		private void InitializeComponent()
		{
			this.pictureBox1 = new Wisej.Web.PictureBox();
			this.labelFileName = new Wisej.Web.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anonymous = true;
			this.pictureBox1.AppearanceKey = "icon-light";
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.CssStyle = "border: 1px solid lightgray;";
			this.pictureBox1.Dock = Wisej.Web.DockStyle.Fill;
			this.pictureBox1.ImageSource = "resource.wx/Wisej.Web.Ext.ChatControl/Images/Link.svg?color=#9B9B9B";
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new Wisej.Web.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(200, 110);
			this.pictureBox1.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
			// 
			// labelFileName
			// 
			this.labelFileName.Anonymous = true;
			this.labelFileName.Dock = Wisej.Web.DockStyle.Bottom;
			this.labelFileName.ForeColor = System.Drawing.Color.White;
			this.labelFileName.Location = new System.Drawing.Point(0, 110);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Padding = new Wisej.Web.Padding(16, 0, 0, 0);
			this.labelFileName.Size = new System.Drawing.Size(200, 40);
			this.labelFileName.TabIndex = 3;
			this.labelFileName.Text = "Undefined";
			this.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FileMessageControl
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelFileName);
			this.Cursor = Wisej.Web.Cursors.Hand;
			this.MinimumSize = new System.Drawing.Size(150, 0);
			this.Name = "FileMessageControl";
			this.Size = new System.Drawing.Size(200, 150);
			this.Click += new System.EventHandler(this.FileMessageControl_Click);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Event Handlers

		private void contextMenu1_MenuItemClicked(object sender, MenuItemEventArgs e)
		{
			if (e.MenuItem.Text == "Download")
			{
				this._stream.Position = 0;
				Application.Download(this._stream, this.labelFileName.Text);
			}
		}

		#endregion

		#region Methods

		public string FormatFileSize(long bytes)
		{
			var scale = 1024;
			string[] orders = { "GB", "MB", "KB", "Bytes" };
			var max = (long)Math.Pow(scale, orders.Length - 1);

			foreach (var order in orders)
			{
				if (bytes > max)
					return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

				max /= scale;
			}
			return "0 Bytes";
		}

		#endregion

		private void FileMessageControl_Click(object sender, EventArgs e)
		{
			this.SetMessageResult(this._stream);
		}
	}
}
