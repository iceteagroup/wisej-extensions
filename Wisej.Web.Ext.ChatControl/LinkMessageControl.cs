using System;
using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A simple message control for displaying links.
	/// </summary>
	public class LinkMessageControl : MessageControl
	{

		#region Constructors

		public LinkMessageControl() 
		{
			InitializeComponent();
		}

		public LinkMessageControl(string text, string link, string imageSource=null, Image image=null)
		{
			InitializeComponent();

			this._link = link;
			this.labelLinkName.Text = text;

			if (image != null)
			{
				this.pictureBoxPreview.Image = image;
				this.pictureBoxPreview.SizeMode = PictureBoxSizeMode.Cover;
			}
	
			if (!string.IsNullOrEmpty(imageSource))
			{
				this.pictureBoxPreview.ImageSource = imageSource;
				this.pictureBoxPreview.SizeMode = PictureBoxSizeMode.Cover;
			}
		}

		#endregion

		#region Event Handler

		private void LinkMessageControl_Click(object sender, EventArgs e)
		{
			if (this.AutoOpen)
				Application.Navigate(this._link, this.Target);

			this.SetMessageResult(this._link);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether links are automatically opened when clicked.
		/// </summary>
		public bool AutoOpen
		{
			get
			{
				return this._autoOpen;
			}
			set
			{
				if (this._autoOpen != value) 
				{
					this._autoOpen = value;
				}
			}
		}
		private bool _autoOpen = true;

		private string _link = "";

		/// <summary>
		/// Gets or sets the navigation target.
		/// </summary>
		public string Target
		{
			get
			{
				return this._target;
			}
			set
			{
				if (this._target != value) 
				{
					this._target = value;
				}
			}
		}
		private string _target = "_blank";

		#endregion

		#region Initialization

		private PictureBox pictureBoxPreview;
		private Label labelLinkName;

		private void InitializeComponent()
		{
			this.pictureBoxPreview = new Wisej.Web.PictureBox();
			this.labelLinkName = new Wisej.Web.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.Anonymous = true;
			this.pictureBoxPreview.BackColor = System.Drawing.Color.White;
			this.pictureBoxPreview.CssStyle = "border: 1px solid lightgray;";
			this.pictureBoxPreview.Dock = Wisej.Web.DockStyle.Fill;
			this.pictureBoxPreview.ImageSource = "resource.wx/Wisej.Web.Ext.ChatControl/Images/Link.svg?color=#A1A1A1";
			this.pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Padding = new Wisej.Web.Padding(0, 10, 0, 16);
			this.pictureBoxPreview.Size = new System.Drawing.Size(200, 100);
			this.pictureBoxPreview.SizeMode = Wisej.Web.PictureBoxSizeMode.Zoom;
			// 
			// labelLinkName
			// 
			this.labelLinkName.Anonymous = true;
			this.labelLinkName.Dock = Wisej.Web.DockStyle.Bottom;
			this.labelLinkName.ForeColor = System.Drawing.Color.FromName("@activeCaptionText");
			this.labelLinkName.Location = new System.Drawing.Point(0, 100);
			this.labelLinkName.Name = "labelLinkName";
			this.labelLinkName.Padding = new Wisej.Web.Padding(16, 0, 0, 0);
			this.labelLinkName.Size = new System.Drawing.Size(200, 50);
			this.labelLinkName.TabIndex = 1;
			this.labelLinkName.Text = "Undefined";
			this.labelLinkName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LinkMessageControl
			// 
			this.Controls.Add(this.pictureBoxPreview);
			this.Controls.Add(this.labelLinkName);
			this.Cursor = Wisej.Web.Cursors.Hand;
			this.MinimumSize = new System.Drawing.Size(150, 0);
			this.Name = "LinkMessageControl";
			this.Size = new System.Drawing.Size(200, 150);
			this.Click += new System.EventHandler(this.LinkMessageControl_Click);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

	}
}
