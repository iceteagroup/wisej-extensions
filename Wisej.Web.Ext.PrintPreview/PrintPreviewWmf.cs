///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Web;
using Wisej.Core;

namespace Wisej.Web.Ext.PrintPreview
{
	/// <summary>
	/// Previews the WMF pages in a collection of <see cref="PrintPreviewWmfPage"/> child controls.
	/// </summary>
	internal class PrintPreviewWmf : Panel, IWisejHandler
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="PrintPreviewWmf"/>.
		/// </summary>
		public PrintPreviewWmf()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the page orientation.
		/// </summary>
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation
		{
			get { return this._orientation; }
			set
			{
				if (this._orientation != value)
				{
					this._orientation = value;

					this.pages.PerformLayout();
					ScrollPageIntoView((int)this.pageNumber.Value);
				}
			}
		}
		private Orientation _orientation = Orientation.Vertical;

		/// <summary>
		/// Returns or sets the zoom level.
		/// </summary>
		[DefaultValue(1)]
		public float ZoomLevel
		{
			get { return this._zoomLevel; }
			set
			{
				if (this._zoomLevel != value)
				{
					this._zoomLevel = value;

					this.pages.PerformLayout();
					ScrollPageIntoView((int)this.pageNumber.Value);
				}
			}
		}
		private float _zoomLevel = 1;

		/// <summary>
		/// Returns or sets the document to preview.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrintDocument Document
		{
			get { return this._document; }
			set
			{
				if (this._document != value)
				{
					this._document = value;
				}
			}
		}
		private PrintDocument _document;

		#endregion

		#region Methods

		/// <summary>
		/// Opens the preview window on the browser.
		/// </summary>
		public void Print()
		{
			this.Call("print", this.GetPostbackURL());
		}

		#endregion

		#region Implementation

		private void ScrollPageIntoView(int pageIndex)
		{
			var controls = this.pages.Controls;
			if (pageIndex > 0 && pageIndex <= controls.Count)
			{
				this.pages.ScrollControlIntoView(controls[pageIndex - 1]);
			}
		}

		protected override void OnCreateControl()
		{
			EnsureWmfPages();

			base.OnCreateControl();
		}

		private void EnsureWmfPages()
		{
			if (this._document != null && this.printController == null)
			{
				this.pages.Controls.Clear(true);

				this.printController = new PreviewPrintController() { UseAntiAlias = true };
				this._document.PrintController = this.printController;
				this._document.Print();

				var pages = this.printController.GetPreviewPageInfo();
				this.pages.SuspendLayout();
				try
				{
					foreach (var p in pages)
					{
						var wmf = new PrintPreviewWmfPage()
						{
							PageInfo = p
						};
						this.pages.Controls.Add(wmf);
					}
				}
				finally
				{
					this.pages.ResumeLayout(true);

					if (pages.Length == 0)
					{
						this.pageNumber.Minimum = 0;
						this.pageNumber.Maximum = 0;
						pageNumber.Value = 0;
					}
					else
					{
						this.pageNumber.Minimum = 1;
						this.pageNumber.Maximum = pages.Length;
						pageNumber.Value = 1;
					}
				}
			}
		}
		private PreviewPrintController printController;

		private void pages_Layout(object sender, LayoutEventArgs e)
		{
			var location = Point.Empty;
			var margin = this.pages.AutoScrollMargin;
			var availableWidth = this.pages.DisplayRectangle.Width;
			var zoomLevel = this.ZoomLevel;


			if (this.Orientation == Orientation.Vertical)
			{
				availableWidth = availableWidth - margin.Width - margin.Width;

				foreach (PrintPreviewWmfPage page in this.pages.Controls)
				{
					var pageSize = page.GetPageSize(96);

					// auto
					if (zoomLevel == 0)
					{
						float ratio = pageSize.Width / (float)pageSize.Height;
						page.Width = availableWidth;
						page.Height = (int)Math.Round(page.Width / ratio);
					}
					else
					{
						page.Width = (int)Math.Round(pageSize.Width * zoomLevel);
						page.Height = (int)Math.Round(pageSize.Height * zoomLevel);
					}

					location.Y += margin.Height;
					location.X = Math.Max((availableWidth - page.Width) / 2, margin.Width);
					page.Location = location;
					location.Y += page.Height;
				}
			}
			else
			{
				location.Y = margin.Height;

				foreach (PrintPreviewWmfPage page in this.pages.Controls)
				{
					var pageSize = page.GetPageSize(96);

					// auto
					if (zoomLevel == 0)
					{
						float ratio = pageSize.Width / (float)pageSize.Height;
						page.Width = (availableWidth - margin.Width) / 2;
						page.Height = (int)Math.Round(page.Width / ratio);
					}
					else
					{
						page.Width = (int)Math.Round(pageSize.Width * zoomLevel);
						page.Height = (int)Math.Round(pageSize.Height * zoomLevel);
					}

					location.X += margin.Width;
					
					// wrap?
					if ((location.X + page.Width + margin.Width + margin.Width) > availableWidth)
					{
						location.X = margin.Width;
						location.Y += page.Height + margin.Height;
					}
					page.Location = location;
					location.X += page.Width;
				}

			}
		}

		private void print_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void zoom_ItemClicked(object sender, MenuButtonItemClickedEventArgs e)
		{
			var zoom = 1;
			var option = e.Item.Tag as string;
			int.TryParse(option, out zoom);
			this.ZoomLevel = zoom / 100f;
			this.zoom.Text = e.Item.Text;
		}

		private void pageNumber_ValueChanged(object sender, EventArgs e)
		{
			ScrollPageIntoView((int)this.pageNumber.Value);
		}

		private void vertical_CheckedChanged(object sender, EventArgs e)
		{
			if (this.vertical.Checked)
				this.Orientation = Orientation.Vertical;
		}

		private void horizontal_CheckedChanged(object sender, EventArgs e)
		{
			if (this.horizontal.Checked)
				this.Orientation = Orientation.Horizontal;
		}

		#endregion

		#region Wisej Form Designer generated code

		private IContainer components;

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tools = new Wisej.Web.Panel();
			this.horizontal = new Wisej.Web.RadioButton();
			this.vertical = new Wisej.Web.RadioButton();
			this.pageNumber = new Wisej.Web.NumericUpDown();
			this.zoom = new Wisej.Web.Button();
			this.menuItemZoomAuto = new Wisej.Web.MenuItem();
			this.menuItemZoom500 = new Wisej.Web.MenuItem();
			this.menuItemZoom200 = new Wisej.Web.MenuItem();
			this.menuItemZoom150 = new Wisej.Web.MenuItem();
			this.menuItemZoom100 = new Wisej.Web.MenuItem();
			this.menuItemZoom75 = new Wisej.Web.MenuItem();
			this.menuItemZoom50 = new Wisej.Web.MenuItem();
			this.menuItemZoom25 = new Wisej.Web.MenuItem();
			this.menuItemZoom10 = new Wisej.Web.MenuItem();
			this.print = new Wisej.Web.Button();
			this.pages = new Wisej.Web.FlexLayoutPanel();
			this.javaScript = new Wisej.Web.JavaScript(this.components);
			this.tools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pageNumber)).BeginInit();
			this.SuspendLayout();
			// 
			// tools
			// 
			this.tools.BackColor = System.Drawing.Color.FromName("@window");
			this.tools.BorderStyle = Wisej.Web.BorderStyle.Solid;
			this.tools.Controls.Add(this.horizontal);
			this.tools.Controls.Add(this.vertical);
			this.tools.Controls.Add(this.pageNumber);
			this.tools.Controls.Add(this.zoom);
			this.tools.Controls.Add(this.print);
			this.tools.Dock = Wisej.Web.DockStyle.Top;
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Size = new System.Drawing.Size(644, 57);
			this.tools.TabIndex = 0;
			this.tools.TabStop = true;
			// 
			// horizontal
			// 
			this.horizontal.Appearance = Wisej.Web.Appearance.Button;
			this.horizontal.AutoSize = false;
			this.horizontal.ForeColor = System.Drawing.Color.FromName("@buttonText");
			this.horizontal.ImageSource = "resource.wx\\Wisej.Web.Ext.PrintPreview\\Icons\\view_column-24px.svg";
			this.horizontal.Location = new System.Drawing.Point(189, 11);
			this.horizontal.Name = "horizontal";
			this.horizontal.Size = new System.Drawing.Size(32, 32);
			this.horizontal.TabIndex = 4;
			this.horizontal.CheckedChanged += new System.EventHandler(this.horizontal_CheckedChanged);
			// 
			// vertical
			// 
			this.vertical.Appearance = Wisej.Web.Appearance.Button;
			this.vertical.AutoSize = false;
			this.vertical.Checked = true;
			this.vertical.ForeColor = System.Drawing.Color.FromName("@buttonText");
			this.vertical.ImageSource = "resource.wx\\Wisej.Web.Ext.PrintPreview\\Icons\\table_rows-24px.svg";
			this.vertical.Location = new System.Drawing.Point(151, 11);
			this.vertical.Name = "vertical";
			this.vertical.Size = new System.Drawing.Size(32, 32);
			this.vertical.TabIndex = 3;
			this.vertical.TabStop = true;
			this.vertical.CheckedChanged += new System.EventHandler(this.vertical_CheckedChanged);
			// 
			// pageNumber
			// 
			this.pageNumber.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Right)));
			this.pageNumber.AutoSize = false;
			this.pageNumber.Location = new System.Drawing.Point(510, 11);
			this.pageNumber.Name = "pageNumber";
			this.pageNumber.Size = new System.Drawing.Size(116, 32);
			this.pageNumber.TabIndex = 2;
			this.pageNumber.TextAlign = Wisej.Web.HorizontalAlignment.Center;
			this.pageNumber.UpDownAlign = Wisej.Web.HorizontalAlignment.Center;
			this.pageNumber.ValueChanged += new System.EventHandler(this.pageNumber_ValueChanged);
			// 
			// zoom
			// 
			this.zoom.ImageSource = "icon-search";
			this.zoom.Location = new System.Drawing.Point(51, 11);
			this.zoom.MenuItems.AddRange(new Wisej.Web.MenuItem[] {
            this.menuItemZoomAuto,
            this.menuItemZoom500,
            this.menuItemZoom200,
            this.menuItemZoom150,
            this.menuItemZoom100,
            this.menuItemZoom75,
            this.menuItemZoom50,
            this.menuItemZoom25,
            this.menuItemZoom10});
			this.zoom.Name = "zoom";
			this.zoom.Size = new System.Drawing.Size(94, 32);
			this.zoom.TabIndex = 1;
			this.zoom.Text = "100%";
			this.zoom.ItemClicked += new Wisej.Web.MenuButtonItemClickedEventHandler(this.zoom_ItemClicked);
			// 
			// menuItemZoomAuto
			// 
			this.menuItemZoomAuto.Index = 0;
			this.menuItemZoomAuto.Name = "menuItemZoomAuto";
			this.menuItemZoomAuto.Tag = "0";
			this.menuItemZoomAuto.Text = "Auto";
			// 
			// menuItemZoom500
			// 
			this.menuItemZoom500.Index = 1;
			this.menuItemZoom500.Name = "menuItemZoom500";
			this.menuItemZoom500.Tag = "500";
			this.menuItemZoom500.Text = "500%";
			// 
			// menuItemZoom200
			// 
			this.menuItemZoom200.Index = 2;
			this.menuItemZoom200.Name = "menuItemZoom200";
			this.menuItemZoom200.Tag = "200";
			this.menuItemZoom200.Text = "200%";
			// 
			// menuItemZoom150
			// 
			this.menuItemZoom150.Index = 3;
			this.menuItemZoom150.Name = "menuItemZoom150";
			this.menuItemZoom150.Tag = "150";
			this.menuItemZoom150.Text = "150%";
			// 
			// menuItemZoom100
			// 
			this.menuItemZoom100.Index = 4;
			this.menuItemZoom100.Name = "menuItemZoom100";
			this.menuItemZoom100.Tag = "100";
			this.menuItemZoom100.Text = "100%";
			// 
			// menuItemZoom75
			// 
			this.menuItemZoom75.Index = 5;
			this.menuItemZoom75.Name = "menuItemZoom75";
			this.menuItemZoom75.Tag = "75";
			this.menuItemZoom75.Text = "75%";
			// 
			// menuItemZoom50
			// 
			this.menuItemZoom50.Index = 6;
			this.menuItemZoom50.Name = "menuItemZoom50";
			this.menuItemZoom50.Tag = "50";
			this.menuItemZoom50.Text = "50%";
			// 
			// menuItemZoom25
			// 
			this.menuItemZoom25.Index = 7;
			this.menuItemZoom25.Name = "menuItemZoom25";
			this.menuItemZoom25.Tag = "25";
			this.menuItemZoom25.Text = "25%";
			// 
			// menuItemZoom10
			// 
			this.menuItemZoom10.Index = 8;
			this.menuItemZoom10.Name = "menuItemZoom10";
			this.menuItemZoom10.Tag = "10";
			this.menuItemZoom10.Text = "10%";
			// 
			// print
			// 
			this.print.Display = Wisej.Web.Display.Icon;
			this.print.ImageSource = "icon-print";
			this.print.Location = new System.Drawing.Point(13, 11);
			this.print.Name = "print";
			this.print.Size = new System.Drawing.Size(32, 32);
			this.print.TabIndex = 0;
			this.print.Click += new System.EventHandler(this.print_Click);
			// 
			// pages
			// 
			this.pages.AutoScroll = true;
			this.pages.AutoScrollMargin = new Size(20, 20);
			this.pages.Dock = Wisej.Web.DockStyle.Fill;
			this.pages.Location = new System.Drawing.Point(0, 57);
			this.pages.Name = "pages";
			this.pages.Size = new System.Drawing.Size(644, 413);
			this.pages.TabIndex = 1;
			this.pages.TabStop = true;
			this.pages.Layout += new Wisej.Web.LayoutEventHandler(this.pages_Layout);
			// 
			// PrintPreviewWmf
			// 
			this.Controls.Add(this.pages);
			this.Controls.Add(this.tools);
			this.javaScript.SetJavaScriptSource(this, "PrintPreviewWmf.js");
			this.Name = "PrintPreviewWmf";
			this.Size = new System.Drawing.Size(644, 470);
			this.tools.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pageNumber)).EndInit();
			this.ResumeLayout(false);

		}

		private Panel tools;
		private NumericUpDown pageNumber;
		private Button zoom;
		private MenuItem menuItemZoomAuto;
		private MenuItem menuItemZoom500;
		private MenuItem menuItemZoom200;
		private MenuItem menuItemZoom150;
		private MenuItem menuItemZoom100;
		private MenuItem menuItemZoom75;
		private MenuItem menuItemZoom50;
		private MenuItem menuItemZoom25;
		private MenuItem menuItemZoom10;
		private Button print;
		private RadioButton vertical;
		private RadioButton horizontal;
		private FlexLayoutPanel pages;
		private JavaScript javaScript;

		#endregion

		#region IWisejHandler

		bool IWisejHandler.Compress
		{
			get { return false; }
		}

		void IWisejHandler.ProcessRequest(HttpContext context)
		{

			// returns the WMF page at full size for the 
			// print IFRAME create on the browser in the PrintPreviewWmf.js code.

			var request = context.Request;
			var response = context.Response;

			var pageIndex = 0;
			if (int.TryParse(request["page"], out pageIndex))
			{
				response.ContentType = "image/png";
				
				var page = (PrintPreviewWmfPage)this.pages.Controls[pageIndex];
				var pageSize = page.GetPageSize(96);
				using (var image = new Bitmap(pageSize.Width, pageSize.Height, PixelFormat.Format24bppRgb))
				using (var g = Graphics.FromImage(image))
				{
					g.FillRectangle(Brushes.White, 0, 0, pageSize.Width, pageSize.Height);
					g.DrawImage(page.PageInfo.Image, 0, 0, pageSize.Width, pageSize.Height);
					image.Save(response.OutputStream, ImageFormat.Png);
				}
			}
		}

		#endregion
	}
}