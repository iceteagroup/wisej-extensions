///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.IO;
using System.Net;
using System.Net.Mime;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.OfficeViewer
{
	/// <summary>
	/// Represents a panel that displays Microsoft Office documents (Word, Excel, PowerPoint) using the
	/// Microsoft Office Online viewer. See <see href="https://products.office.com/en-us/office-online/view-office-documents-online"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The document is rendered inside an IFrame that loads <c>https://view.officeapps.live.com/op/view.aspx</c>.
	/// Microsoft's servers download the file from the URL passed to the viewer, therefore the file (or the Wisej
	/// application when using a relative <see cref="FileSource"/> or a <see cref="FileStream"/>) must be reachable
	/// from the public internet. Documents served from <c>localhost</c> or an intranet cannot be displayed.
	/// </para>
	/// <para>
	/// Relative paths and streams are served by the control itself through its Wisej service URL; handle
	/// <see cref="FileRequested"/> to write the response yourself.
	/// </para>
	/// </remarks>
	/// <example>
	/// Displaying a public document:
	/// <code><![CDATA[
	/// var viewer = new OfficeViewer
	/// {
	///     Dock = DockStyle.Fill,
	///     FileSource = "https://www.example.com/files/report.docx"
	/// };
	/// this.Controls.Add(viewer);
	/// ]]></code>
	/// </example>
	[ApiCategory("OfficeViewer")]
	public class OfficeViewer : IFramePanel, IWisejHandler
	{
		private const string OFFICEAPPS_URL = "https://view.officeapps.live.com/op/view.aspx?src=";

		#region Events

		/// <summary>
		/// Fired when the file is requested.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The application can handle this event and override the default functionality
		/// by setting the <see cref="HandledEventArgs.Handled"/> property to true.
		/// </para>
		/// <para>
		/// The current response is available using <see cref="HttpContext.Current"/>.
		/// </para>
		/// </remarks>
		[SRDescription("Fired when the file is requested..")]
		public event HandledEventHandler FileRequested
		{
			add { base.AddHandler(nameof(FileRequested), value); }
			remove { base.RemoveHandler(nameof(FileRequested), value); }
		}

		/// <summary>
		/// Fires the <see cref="FileRequested" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.HandledEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFileRequested(HandledEventArgs e)
		{
			((HandledEventHandler)base.Events[nameof(FileRequested)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the FileSource property is changed.
		/// </summary>
		[SRDescription("Fired when the FileSource property is changed.")]
		public event EventHandler FileSourceChanged
		{
			add { base.AddHandler(nameof(FileSourceChanged), value); }
			remove { base.RemoveHandler(nameof(FileSourceChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="FileSourceChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFileSourceChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(FileSourceChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the path of the Office file to view.
		/// It can be a relative path or an absolute URL.
		/// </summary>
		/// <remarks>
		/// <para>
		/// An absolute <c>http:</c> or <c>https:</c> URL is passed directly to the Office Online viewer.
		/// Any other value is treated as a path relative to the application's root folder (resolved with
		/// <see cref="Application.MapPath"/>) and the file is streamed to the viewer by this control.
		/// </para>
		/// <para>
		/// Setting this property fires <see cref="FileSourceChanged"/>, clears <see cref="FileStream"/> and
		/// updates <see cref="Url"/>. Setting it to null is the same as setting it to an empty string.
		/// </para>
		/// </remarks>
		/// <example>
		/// Showing a file stored in the application's folder, or a file hosted elsewhere:
		/// <code><![CDATA[
		/// // relative to the application root, served by the Wisej application.
		/// this.officeViewer1.FileSource = "Documents/Budget.xlsx";
		///
		/// // absolute URL, downloaded directly by the Office Online viewer.
		/// this.officeViewer1.FileSource = "https://www.example.com/files/Presentation.pptx";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the path of the Office file to view.")]
		public string FileSource
		{
			get { return this._fileSource; }
			set
			{
				value = value ?? string.Empty;

				if (this._fileSource != value)
				{
					this._fileSource = value;

					OnFileSourceChanged(EventArgs.Empty);

					// reset the stream last, to let the app
					// retrieve it handling FileSourceChanged.
					this._fileStream = null;

					UpdateUrl();
				}
			}
		}
		private string _fileSource = string.Empty;

		/// <summary>
		/// Returns or sets the stream of the Office file to view.
		/// </summary>
		/// <exception cref="InvalidOperationException">The value is not null and <see cref="FileName"/> is empty.</exception>
		/// <remarks>
		/// <para>
		/// <see cref="FileName"/> must be set, including the file extension, before assigning a stream;
		/// otherwise an <see cref="InvalidOperationException"/> is thrown. The extension is used by the
		/// Office Online viewer to detect the document type.
		/// </para>
		/// <para>
		/// Assigning a stream clears <see cref="FileSource"/>. The stream must remain open and readable:
		/// it is rewound (when seekable) and copied to the response each time the viewer requests the file.
		/// </para>
		/// </remarks>
		/// <example>
		/// Displaying a document generated in memory:
		/// <code><![CDATA[
		/// var stream = new MemoryStream();
		/// CreateInvoice(stream);
		///
		/// this.officeViewer1.FileName = "Invoice.docx";
		/// this.officeViewer1.FileStream = stream;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream FileStream
		{
			get { return this._fileStream; }
			set
			{
				if (this._fileStream != value)
				{
					if (value != null && String.IsNullOrEmpty(this.FileName))
						throw new InvalidOperationException("A valid FileName with extension is required when using FileStream.");


					// reset the FileSource when assigning a stream.
					this.FileSource = null;

					this._fileStream = value;
					UpdateUrl();
				}
			}
		}
		private Stream _fileStream = null;

		/// <summary>
		/// Returns or sets the file name with extension to return to the office viewer.
		/// </summary>
		/// <remarks>
		/// This property is required when using <see cref="FileStream"/> instead of <see cref="FileSource"/>,
		/// and must be set before assigning the stream. When <see cref="FileSource"/> is set, the file name
		/// is taken from <see cref="FileSource"/> and this property is ignored.
		/// </remarks>
		[DefaultValue("")]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the file name with extension of the office file to view.")]
		public string FileName
		{
			get { return this._fileName; }
			set { this._fileName = value; }
		}
		private string _fileName;

		/// <summary>
		/// Returns or sets the source URL of the IFrame.
		/// </summary>
		/// <remarks>
		/// This property is managed by the control: it is overwritten with the Office Online viewer URL
		/// whenever <see cref="FileSource"/> or <see cref="FileStream"/> change. Use those properties instead.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Url
		{
			get { return base.Url; }
			set { base.Url = value; }
		}

		#endregion

		#region Methods

		private void UpdateUrl()
		{
			if (String.IsNullOrEmpty(this.FileSource) && this.FileStream == null)
				return;

			if (this.FileStream != null && !this.FileStream.CanRead)
				return;

			// full URL?
			if (!string.IsNullOrEmpty(this.FileSource))
			{
				var source = this.FileSource.Trim();
				if (source.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase)
					|| source.StartsWith("https:", StringComparison.InvariantCultureIgnoreCase))
				{
					this.Url =
						OFFICEAPPS_URL + WebUtility.UrlEncode(source);

					return;
				}
			}

			// this URL will be processed by OfficeViewerModule
			var startUpUrl = Application.StartupUrl;
			if (!startUpUrl.EndsWith("/"))
				startUpUrl += "/";

			this.Url =
				OFFICEAPPS_URL +
				WebUtility.UrlEncode(startUpUrl + this.GetServiceURL() + "?v=" + DateTime.Now.Ticks);
		}

		#endregion

		#region IWisejHandler

		/// <summary>
		/// Don't compress the output. PDF files are already compressed.
		/// </summary>
		bool IWisejHandler.Compress { get { return false; } }

		/// <summary>
		/// Process the HTTP request.
		/// </summary>
		/// <param name="context">The current <see cref="HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			var args = new HandledEventArgs(false);
			OnFileRequested(args);
			if (args.Handled)
				return;

			var request = context.Request;
			var response = context.Response;

			var fileName =
				String.IsNullOrEmpty(this.FileSource)
					? this.FileName
					: Path.GetFileName(this.FileSource);

			response.ContentType = "text/plain";
			response.AppendHeader("Content-Disposition", new ContentDisposition() { DispositionType = "attachment", FileName =  fileName}.ToString());

			if (this._fileStream != null)
			{
				try
				{
					if (this._fileStream.CanRead)
					{
						try
						{
							this._fileStream.Position = 0;
						}
						catch { }

						this._fileStream.CopyTo(response.OutputStream);
					}
				}
				catch (Exception ex)
				{
					LogManager.Log(ex);
				}
			}
			else
			{
				try
				{
					var filePath = Application.MapPath(this.FileSource);
					response.TransmitFile(filePath);
				}
				catch (Exception ex)
				{
					LogManager.Log(ex);
				}
			}

			response.Flush();
		}

		#endregion

	}
}
