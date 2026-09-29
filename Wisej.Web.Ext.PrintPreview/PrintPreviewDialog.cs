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
	/// Represents a dialog box form that contains a <see cref="PrintPreviewControl"/> 
	/// for printing from a Wisej application using a <see cref="PrintDocument"/> instance.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Supports two preview modes: <see cref="PrintPreviewDialogViewerType.PDF"/> and
	/// <see cref="PrintPreviewDialogViewerType.WMF"/>. When using the PDF mode, it prints the
	/// document to a temporary PDF file using the "Microsoft Print to PDF" printer driver installed on the server
	/// and displays it in the embedded PDF.js viewer.
	/// When using the WMF mode, it uses the <see cref="System.Drawing.Printing.PreviewPrintController"/> to
	/// print to Windows Meta Files (WMF) images and renders them in the browser.
	/// </para>
	/// </remarks>
	public class PrintPreviewDialog : Form
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="PrintPreviewDialog"/>.
		/// </summary>
		/// <remarks>
		/// The new instance uses the <see cref="PrintPreviewDialogViewerType.PDF"/> viewer.
		/// </remarks>
		public PrintPreviewDialog()
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
		/// The document is disposed together with the dialog.
		/// </para>
		/// </remarks>
		/// <example>
		/// Previewing a document in a modal dialog:
		/// <code><![CDATA[
		/// private void buttonPreview_Click(object sender, EventArgs e)
		/// {
		///     var document = new PrintDocument();
		///     document.DocumentName = "Report";
		///     document.PrintPage += this.Report_PrintPage;
		///
		///     using (var dialog = new PrintPreviewDialog())
		///     {
		///         dialog.ViewerType = PrintPreviewDialogViewerType.WMF;
		///         dialog.Document = document;
		///         dialog.ShowDialog();
		///     }
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrintDocument Document
		{
			get { return this.previewControl.Document; }
			set { this.previewControl.Document = value; }
		}

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
			get { return this.previewControl.ViewerType; }
			set { this.previewControl.ViewerType = value; }
		}

		#endregion

		#region Wisej Form Designer generated code

		private void InitializeComponent()
		{
			Wisej.Resources.ComponentResourceManager resources = new Wisej.Resources.ComponentResourceManager(typeof(PrintPreviewDialog));
			this.previewControl = new PrintPreviewControl();
			this.SuspendLayout();
			// 
			// previewControl
			// 
			resources.ApplyResources(this.previewControl, "previewControl");
			this.previewControl.Name = "previewControl";
			this.previewControl.TabStop = true;
			// 
			// PrintPreviewDialog
			// 
			this.Controls.Add(this.previewControl);
			this.Name = "PrintPreviewDialog";
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.MinimumSize = new System.Drawing.Size(350, 200);
			resources.ApplyResources(this, "$this");
			this.ResumeLayout(false);

		}

		private PrintPreviewControl previewControl;

		#endregion
	}
}