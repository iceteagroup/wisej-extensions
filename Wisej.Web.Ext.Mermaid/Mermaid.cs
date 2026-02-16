///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Integrates <a href="https://mermaid.js.org/">Mermaid</a> diagrams as a Wisej widget.
	/// </summary>
	/// <remarks>
	/// The widget renders Mermaid diagrams on the client and exposes a simple .NET API to provide the diagram text and
	/// initialization options. Use <see cref="Diagram"/> for the diagram source and <see cref="Config"/> to customize the
	/// Mermaid initialization options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // Basic usage: render a flowchart.
	/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid()
	/// {
	///     Dock = Wisej.Web.DockStyle.Fill,
	///     Diagram =
	/// @"flowchart TD
	///     A[Start] --> B{Valid?}
	///     B -- Yes --> C[Continue]
	///     B -- No  --> D[Stop]"
	/// };
	///
	/// // Optional: tweak Mermaid initialization options.
	/// mermaid.Config.Theme = "dark";
	/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Strict;
	///
	/// this.Controls.Add(mermaid);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[DefaultEvent("ElementClick")]
	[ApiCategory("Mermaid")]
	public class Mermaid : Widget
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Mermaid"/> widget.
		/// </summary>
		/// <remarks>
		/// The constructor initializes required script packages and applies the initial options.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid();
		/// mermaid.Diagram = "flowchart TD; A-->B";
		/// ]]></code>
		/// </example>
		public Mermaid() : this("")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Mermaid"/> widget.
		/// </summary>
		/// <param name="diagram">Initial Mermaid diagram source text.</param>
		/// <remarks>
		/// The constructor initializes required script packages and applies the initial options.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid("flowchart TD; A-->B");
		/// mermaid.Diagram = "flowchart TD; A-->B";
		/// ]]></code>		/// </example>
		public Mermaid(string diagram)
		{
			this.Diagram = diagram ?? "";
			this.DiagramPadding = 16;
			this.UseMaxWidth = true;
			this.Overflow = "auto";
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the diagram is modified, signaling that its state has changed.
		/// </summary>
		/// <remarks>Subscribe to this event to be notified of changes to the diagram. This can be used to update the
		/// user interface, save changes, or perform other actions in response to modifications.</remarks>
		public event EventHandler DiagramChanged
		{
			add { base.Events.AddHandler(nameof(DiagramChanged), value); }
			remove { base.Events.RemoveHandler(nameof(DiagramChanged), value); }
		}

		/// <summary>
		/// Occurs when an element in the Mermaid diagram is clicked.
		/// </summary>
		/// <remarks>
		/// This event provides information about the clicked element, including its text content,
		/// type (node, edge, cluster, etc.), IDs, and mouse button/location data.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.ElementClick += (s, e) =>
		/// {
		///     MessageBox.Show($"Clicked: {e.Element}\nType: {e.Data?.elementType}");
		/// };
		/// ]]></code>
		/// </example>
		[Description("Occurs when an element in the Mermaid diagram is clicked.")]
		public event ElementClickEventHandler ElementClick
		{
			add { base.Events.AddHandler(nameof(ElementClick), value); }
			remove { base.Events.RemoveHandler(nameof(ElementClick), value); }
		}

		/// <summary>
		/// Occurs when the current diagram fails Mermaid validation/parsing in the browser.
		/// </summary>
		/// <remarks>
		/// This event is raised from a client-side error notification when Mermaid cannot parse/validate the diagram.
		/// If you want to proactively validate without rendering, use <see cref="ValidateAsync(string, MermaidConfiguration)"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Error += (s, e) =>
		/// {
		///     // e.Message contains a human-readable message.
		///     Wisej.Web.MessageBox.Show(e.Message, "Mermaid error");
		/// };
		/// ]]></code>
		/// </example>
		public event MermaidErrorEventHandler Error
		{
			add { base.Events.AddHandler(nameof(Error), value); }
			remove { base.Events.RemoveHandler(nameof(Error), value); }
		}

		/// <summary>
		/// Raises the <see cref="ElementClick"/> event.
		/// </summary>
		/// <param name="e">Event data.</param>
		/// <remarks>
		/// Override this method to intercept element clicks before subscribers are notified.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public class MyMermaid : Wisej.Web.Ext.Mermaid.Mermaid
		/// {
		///     protected override void OnElementClick(Wisej.Web.Ext.Mermaid.ElementClickEventArgs e)
		///     {
		///         // Add logging, then forward to base to raise the event.
		///         System.Diagnostics.Debug.WriteLine($"Clicked: {e.Element}");
		///         base.OnElementClick(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		protected virtual void OnElementClick(ElementClickEventArgs e)
		{
			((ElementClickEventHandler)base.Events[nameof(ElementClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="Error"/> event.
		/// </summary>
		/// <param name="e">Event data.</param>
		/// <remarks>
		/// Override this method to intercept Mermaid errors before subscribers are notified.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public class MyMermaid : Wisej.Web.Ext.Mermaid.Mermaid
		/// {
		///     protected override void OnError(Wisej.Web.Ext.Mermaid.MermaidErrorEventArgs e)
		///     {
		///         // Add logging, then forward to base to raise the event.
		///         System.Diagnostics.Debug.WriteLine(e?.Message);
		///         base.OnError(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		protected virtual void OnError(MermaidErrorEventArgs e)
		{
			((MermaidErrorEventHandler)base.Events[nameof(Error)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="DiagramChanged"/> event.
		/// </summary>
		/// <remarks>This method is called to notify subscribers that the diagram has changed.  Derived classes can
		/// override this method to provide additional behavior  when the <see cref="DiagramChanged"/> event is raised. When
		/// overriding,  ensure to call the base implementation to maintain event invocation.</remarks>
		/// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
		protected virtual void OnDiagramChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(DiagramChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Mermaid diagram source text.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Diagram =
		/// @"sequenceDiagram
		///     participant A as Alice
		///     participant B as Bob
		///     A->>B: Hello Bob
		///     B-->>A: Hello Alice";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[DesignerActionList]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Diagram
		{
			get => _diagram;
			set
			{
				if (_diagram != value)
				{
					_diagram = value ?? "";
					this.Options.diagram = _diagram;
					OnDiagramChanged(EventArgs.Empty);
				}
			}
		}
		string _diagram = "";

		/// <summary>
		/// Gets the Mermaid initialization options passed to <c>mermaid.initialize(...)</c>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Theme = "forest";
		/// mermaid.Config.Flowchart.HtmlLabels = true;
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidConfiguration Config
		{
			get => this.Options.config ??= new MermaidConfiguration(this);
		}

		/// <summary>
		/// Gets or sets the padding (pixels) applied around the diagram.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.DiagramPadding = 8;
		/// ]]></code>
		/// </example>
		[DefaultValue(16)]
		public int DiagramPadding
		{
			get => this.Options.padding ?? 16;
			set => this.Options.padding = value < 0 ? 0 : value;
		}

		/// <summary>
		/// Gets or sets the CSS <c>overflow</c> applied to the container element.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// // Hide scrollbars and clip content.
		/// mermaid.Overflow = "hidden";
		/// ]]></code>
		/// </example>
		[DefaultValue("auto")]
		public string Overflow
		{
			get => this.Options.overflow ?? "auto";
			set => this.Options.overflow = value;
		}

		/// <summary>
		/// Gets or sets whether the diagram SVG is constrained to <c>max-width: 100%</c>.
		/// </summary>
		/// <remarks>
		/// Use this to make diagrams responsive within their container.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.UseMaxWidth = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		public bool UseMaxWidth
		{
			get => this.Options.useMaxWidth ?? true;
			set => this.Options.useMaxWidth = value;
		}

		/// <summary>
		/// Gets the script packages required by the widget.
		/// </summary>
		/// <remarks>
		/// This override ensures that resource resolution happens in the correct calling assembly.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override System.Collections.Generic.List<Package> Packages
		{
			// disable inlining or we lose the calling assembly in GetResourceString()/GetResourceURL().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					base.Packages.Add(new Package()
					{
						Name = "mermaid.js",
						Source = GetResourceURL("Wisej.Web.Ext.Mermaid.JavaScript.mermaid.min.js")
					});
					base.Packages.Add(new Package()
					{
						Name = "panzoom.js",
						Source = GetResourceURL("Wisej.Web.Ext.Mermaid.JavaScript.panzoom.min.js")
					});
				}

				return base.Packages;
			}
		}

		/// <inheritdoc/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.Mermaid.JavaScript.startup.js"); }
			set { }
		}

		/// <inheritdoc/>
		[Browsable(false)]
		[WisejSerializerOptions(WisejSerializerOptions.CamelCase)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get => base.Options;
			set => base.Options = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Downloads the currently rendered diagram as an SVG file.
		/// </summary>
		/// <param name="fileName">Optional file name used by the browser download.</param>
		/// <remarks>
		/// This method invokes a client-side download of the last successfully rendered SVG.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.DownloadSvg("order-process.svg");
		/// ]]></code>
		/// </example>
		public void DownloadSvg(string fileName = "diagram.svg")
		{
			this.Call("downloadSvg", fileName ?? "diagram.svg");
		}

		/// <summary>
		/// Converts the current object to an image representation asynchronously.
		/// </summary>
		/// <remarks>This method performs the conversion operation asynchronously, allowing the caller to await its
		/// completion. Ensure that the object being converted contains valid data required for the image
		/// generation.</remarks>
		/// <returns>A task that represents the asynchronous operation. The task result contains the generated <see cref="Image"/>.</returns>
		public async Task<Image> ExportToImageAsync()
		{
			string result = await CallAsync("getImage");
			byte[] buffer = Convert.FromBase64String(result.Substring("data:image/png;base64,".Length));
			using (var ms = new MemoryStream(buffer))
			{
				return Image.FromStream(ms);
			}
		}

		/// <summary>
		/// Downloads the currently rendered diagram as a PDF file.
		/// </summary>
		/// <param name="fileName">Optional file name used by the browser download.</param>
		/// <param name="scale">Optional scale factor for image quality (default is 2 for high DPI).</param>
		/// <param name="backgroundColor">Optional background color for the PDF (default is white).</param>
		/// <param name="margin">Optional margin in points around the diagram (default is 20).</param>
		/// <param name="quality">Optional JPEG quality from 0.0 to 1.0 (default is 0.95).</param>
		/// <remarks>
		/// This method converts the SVG diagram to a canvas, then generates a PDF with the image embedded.
		/// The PDF is created client-side and downloaded directly in the browser.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Basic usage with default options.
		/// mermaid.DownloadPdf("flowchart.pdf");
		/// 
		/// // Custom options for higher quality and different background.
		/// mermaid.DownloadPdf("diagram.pdf", scale: 3, backgroundColor: "#f5f5f5", margin: 30);
		/// ]]></code>
		/// </example>
		public void DownloadPdf(
			string fileName = "diagram.pdf",
			double? scale = null,
			string backgroundColor = null,
			double? margin = null,
			double? quality = null)
		{
			this.Call("downloadPdf", fileName, new
			{
				scale,
				backgroundColor,
				margin,
				quality
			});
		}

		/// <summary>
		/// Exports the currently rendered diagram as a PDF and returns it as a <see cref="MemoryStream"/>.
		/// </summary>
		/// <param name="scale">Optional scale factor for image quality (default is 2 for high DPI).</param>
		/// <param name="backgroundColor">Optional background color for the PDF (default is white).</param>
		/// <param name="margin">Optional margin in points around the diagram (default is 20).</param>
		/// <param name="quality">Optional JPEG quality from 0.0 to 1.0 (default is 0.95).</param>
		/// <returns>A <see cref="MemoryStream"/> containing the PDF bytes. The caller is responsible for disposing the stream.</returns>
		/// <remarks>
		/// This method converts the SVG diagram to a canvas, generates a PDF with the image embedded,
		/// and returns the PDF as a memory stream. The PDF generation happens client-side in the browser,
		/// and the bytes are transferred back to the server.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Export to memory stream for further processing.
		/// using (var pdfStream = await mermaid.ExportToPdfAsync())
		/// {
		///     // Save to file
		///     using (var fileStream = File.Create("diagram.pdf"))
		///     {
		///         pdfStream.CopyTo(fileStream);
		///     }
		/// }
		/// 
		/// // Export with custom options.
		/// using (var pdfStream = await mermaid.ExportToPdfAsync(scale: 3, backgroundColor: "#f5f5f5"))
		/// {
		///     // Send via email, save to database, etc.
		///     byte[] pdfBytes = pdfStream.ToArray();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<MemoryStream> ExportToPdfAsync(
			double? scale = null, 
			string backgroundColor = null, 
			double? margin = null, 
			double? quality = null)
		{
			// call the JavaScript function that generates PDF and returns base64.
			string result = await this.CallAsync("getPdf", new
			{
				scale,
				backgroundColor,
				margin,
				quality
			});

			try
			{
				return new MemoryStream(Convert.FromBase64String(result));
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to generate PDF: Invalid base64 data.", ex);
			}
		}

		/// <summary>
		/// Validates Mermaid syntax in the browser without rendering.
		/// </summary>
		/// <param name="diagram">Mermaid source text.</param>
		/// <returns>A <see cref="ValidationResult"/> containing the validation outcome and optional error details.</returns>
		/// <remarks>
		/// Validation happens in the browser using Mermaid's parser. The returned <see cref="ValidationResult.Error"/>
		/// is a raw, JSON-serializable payload from the client (when available).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var result = await mermaid.ValidateAsync("flowchart TD; A-->B");
		/// if (!result.Ok)
		///     Wisej.Web.MessageBox.Show(result.Message ?? "Diagram is invalid.");
		/// ]]></code>
		/// </example>
		public async Task<ValidationResult> ValidateAsync(string diagram)
			=> new ValidationResult(await this.CallAsync("validate", diagram));

		#endregion

		#region Wisej Implementation

		/// <inheritdoc/>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			if (e == null)
				return;

			switch (e.Type)
			{
				case "elementClick":
					ProcessElementClickWebEvent(e);
					break;

				case "mermaidError":
					ProcessMermaidErrorWebEvent(e);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		// Handles clicks on the inner elements of the Mermaid diagram.
		private void ProcessElementClickWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			var element = data?.text ?? data?.element ?? "";

			if (!string.IsNullOrEmpty(element))
			{
				int x = data?.x ?? 0;
				int y = data?.y ?? 0;
				var location = PointToClient(new Point(x, y));
				var button = (data?.button ?? 0) switch
				{
					0 => MouseButtons.Left,
					1 => MouseButtons.Middle,
					2 => MouseButtons.Right,
					_ => MouseButtons.None
				};

				OnElementClick(new ElementClickEventArgs(element, button, 1, location, data));
			}
		}

		// Handles clicks on the inner elements of the Mermaid diagram.
		private void ProcessMermaidErrorWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			string message = data?.message;
			object error = data?.error;

			if (string.IsNullOrWhiteSpace(message))
				message = "Invalid Mermaid diagram.";

			OnError(new MermaidErrorEventArgs(message, error));
		}

		#endregion
	}
}
