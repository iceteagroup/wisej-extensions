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
	/// document to a temporary PDF file using the "Microsoft Print to PDF" printer drivers.
	/// When using the WMF mode, it uses the <see cref="System.Drawing.Printing.PreviewPrintController"/> to
	/// print to Windows Meta Files (WMF) images and renders then on the browser.
	/// </para>
	/// </remarks>
	public class PrintPreviewDialog : Form
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="PrintPreviewDialog"/>.
		/// </summary>
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
		/// When <see cref="ViewerType"/> is set to <see cref="PrintPreviewDialogViewerType.PDF"/>
		/// and the printer driver "Microsoft Print to PDF" is not found.
		/// </exception>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPreviewDialog));
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