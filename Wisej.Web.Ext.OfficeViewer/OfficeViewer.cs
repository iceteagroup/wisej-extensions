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
using Wisej.Core;
using Wisej.Base;
using System.Text;
using System.IO;
using System.Web;
using System.Net.Mime;
using System.ComponentModel;

namespace Wisej.Web.Ext.OfficeViewer
{
	/// <summary>
	/// Microsoft Office Viewer panel. Uses https://products.office.com/en-us/office-online/view-office-documents-online.
	/// </summary>
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
		/// It can be a relative or absolute URL.
		/// </summary>
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
		/// This property is required when using <see cref="FileStream"/> instead of <see cref="FileSource"/>.
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
						OFFICEAPPS_URL + HttpUtility.UrlEncode(source);

					return;
				}
			}

			var startUpUrl = Application.StartupUri.ToString();
			if (!startUpUrl.EndsWith("/"))
				startUpUrl += "/";

			// build a callback URL without parameters, the office viewer
			// is not capable of using URLs with arguments.
			//
			// this URL will be processed by OfficeViewerModule and
			// rewritten to match the arguments used by Wisej ResourceManager.
			var postback = this.GetPostbackURL() + "&v=" + DateTime.Now.Ticks;
			var arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(postback));

			this.Url =
				OFFICEAPPS_URL +
				HttpUtility.UrlEncode(startUpUrl + "postback.wx/" + arg);
		}

		#endregion

		#region IWisejHandler

		/// <summary>
		/// Don't compress the output. PDF files are already compressed.
		/// </summary>
		bool IWisejHandler.Compress { get { return false; } }

		/// <summary>
		/// Process the http request.
		/// </summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			var args = new HandledEventArgs(false);
			OnFileRequested(args);
			if (args.Handled)
				return;

			HttpRequest request = context.Request;
			HttpResponse response = context.Response;

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
					string filePath = this.FileSource;
					if (!Path.IsPathRooted(filePath))
						filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.FileSource);

					response.WriteFile(filePath);
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
