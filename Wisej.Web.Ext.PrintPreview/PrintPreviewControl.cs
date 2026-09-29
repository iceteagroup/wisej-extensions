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

using System.ComponentModel;
using System.Drawing.Printing;

namespace Wisej.Web.Ext.PrintPreview
{
	/// <summary>
	/// Previews the content of a <see cref="PrintDocument"/> instance.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can use this control independently from the <see cref="PrintPreviewDialog"/>, as a child
	/// of your containers.
	/// </para>
	/// 
	/// <para>
	/// Supports two preview modes: <see cref="PrintPreviewDialogViewerType.PDF"/> and
	/// <see cref="PrintPreviewDialogViewerType.WMF"/>. When using the PDF mode, it prints the
	/// document to a temporary PDF file using the "Microsoft Print to PDF" printer driver installed on the server
	/// and displays it in the embedded PDF.js viewer.
	/// When using the WMF mode, it uses the <see cref="System.Drawing.Printing.PreviewPrintController"/> to
	/// print to Windows Meta Files (WMF) images and renders them in the browser.
	/// </para>
	/// 
	/// </remarks>
	public class PrintPreviewControl : Control
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of <see cref="PrintPreviewControl"/> using the default values.
		/// </summary>
		/// <remarks>
		/// The new instance uses the <see cref="PrintPreviewDialogViewerType.PDF"/> viewer.
		/// </remarks>
		public PrintPreviewControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="PrintDocument"/> to preview.
		/// </summary>
		/// <exception cref="InvalidPrinterException">
		/// The printer driver "Microsoft Print to PDF" is not installed on the server. The check is performed
		/// when a non-null document is assigned, regardless of the <see cref="ViewerType"/>.
		/// </exception>
		/// <remarks>
		/// <para>
		/// The document is printed when the preview is displayed: in PDF mode its <see cref="PrintDocument.PrintController"/>
		/// and <see cref="PrintDocument.PrinterSettings"/> are changed to print to a temporary PDF file, in WMF mode its
		/// <see cref="PrintDocument.PrintController"/> is replaced with a <see cref="PreviewPrintController"/>.
		/// Set the <see cref="ViewerType"/> and the document before the preview is displayed.
		/// </para>
		/// <para>
		/// The document is disposed together with the control.
		/// </para>
		/// </remarks>
		/// <example>
		/// Previewing a document in a control placed on a page:
		/// <code><![CDATA[
		/// var document = new PrintDocument();
		/// document.DocumentName = "Invoice";
		/// document.PrintPage += (s, e) =>
		/// {
		///     e.Graphics.DrawString("Invoice #1001", new Font("Arial", 18), Brushes.Black, 100, 100);
		///     e.HasMorePages = false;
		/// };
		///
		/// this.printPreviewControl1.ViewerType = PrintPreviewDialogViewerType.PDF;
		/// this.printPreviewControl1.Document = document;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrintDocument Document
		{
			get { return this._document; }
			set
			{
				if (this._document != value)
				{
					if (this._document != null)
						this._document.PrintPage -= this.Document_PrintPage;

					this._document = value;

					if (this._document != null)
						this._document.PrintPage += this.Document_PrintPage;

					this.printPreviewPdf.Document = value;
					this.printPreviewWmf.Document = value;
				}
			}
		}
		private PrintDocument _document;

		/// <summary>
		/// Returns or sets the type of viewer to use to display the
		/// print preview.
		/// </summary>
		/// <remarks>
		/// <see cref="PrintPreviewDialogViewerType.PDF"/> requires the "Microsoft Print to PDF" printer driver on the server.
		/// <see cref="PrintPreviewDialogViewerType.WMF"/> renders each page as an image on the server and displays the
		/// pages in a scrollable panel.
		/// </remarks>
		[DefaultValue(PrintPreviewDialogViewerType.PDF)]
		public PrintPreviewDialogViewerType ViewerType
		{
			get { return this._viewerType; }
			set
			{
				if (this._viewerType != value)
				{
					this._viewerType = value;

					switch (this._viewerType)
					{
						case PrintPreviewDialogViewerType.WMF:
							this.printPreviewPdf.Hide();
							this.printPreviewWmf.Show();
							break;

						default:
							this.printPreviewPdf.Show();
							this.printPreviewWmf.Hide();
							break;
					}
				}
			}
		}
		private PrintPreviewDialogViewerType _viewerType = PrintPreviewDialogViewerType.PDF;

		#endregion

		#region Implementation

		private void Document_PrintPage(object sender, PrintPageEventArgs e)
		{
			if (this.Disposing || this.IsDisposed)
				e.Cancel = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._document?.Dispose();
				this._document = null;
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Wisej Form Designer generated code

		private void InitializeComponent()
		{
			this.printPreviewPdf = new Wisej.Web.Ext.PrintPreview.PrintPreviewPdf();
			this.printPreviewWmf = new Wisej.Web.Ext.PrintPreview.PrintPreviewWmf();
			this.SuspendLayout();
			// 
			// printPreviewPdf
			// 
			this.printPreviewPdf.Dock = Wisej.Web.DockStyle.Fill;
			this.printPreviewPdf.Name = "printPreviewPdf";
			this.printPreviewPdf.Size = new System.Drawing.Size(550, 394);
			this.printPreviewPdf.TabIndex = 0;
			this.printPreviewPdf.Visible = true;
			// 
			// printPreviewWmf1
			// 
			this.printPreviewWmf.Dock = Wisej.Web.DockStyle.Fill;
			this.printPreviewWmf.Name = "printPreviewWmf";
			this.printPreviewWmf.Size = new System.Drawing.Size(550, 394);
			this.printPreviewWmf.TabIndex = 0;
			this.printPreviewWmf.Visible = false;
			// 
			// PrintPreviewControl
			// 
			this.Controls.Add(this.printPreviewWmf);
			this.Controls.Add(this.printPreviewPdf);
			this.Name = "PrintPreviewControl";
			this.Size = new System.Drawing.Size(550, 394);
			this.ResumeLayout(false);

		}

		private PrintPreviewPdf printPreviewPdf;
		private PrintPreviewWmf printPreviewWmf;

		#endregion
	}
}