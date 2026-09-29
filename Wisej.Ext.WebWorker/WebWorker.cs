///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Reflection;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Ext.WebWorker
{
	/// <summary>
	/// The WebWorker component represents a JavaScript WebWorker instance that can run on the client and fire sever events
	/// and receive updates from the server.
	/// </summary>
	/// <remarks>
	/// The worker script is set using either the <see cref="JavaScript"/> or the <see cref="JavaScriptSource"/> property
	/// and runs in a background thread in the browser. Data sent from the server with <see cref="SendMessage"/> is received
	/// in the worker's <c>onmessage</c> handler, and data sent by the worker with <c>postMessage</c> fires the
	/// <see cref="PostMessage"/> event on the server.
	/// See: <a href="https://developer.mozilla.org/en-US/docs/Web/API/Web_Workers_API">Web Workers API.</a>
	/// </remarks>
	/// <example>
	/// The following example creates a worker that doubles the numbers it receives and sends the result back to the server:
	/// <code><![CDATA[
	/// private WebWorker webWorker;
	///
	/// private void Page1_Load(object sender, EventArgs e)
	/// {
	///     this.webWorker = new WebWorker(this.components);
	///     this.webWorker.JavaScript = @"
	///         onmessage = function (e) {
	///             postMessage(e.data * 2);
	///         };";
	///
	///     this.webWorker.PostMessage += webWorker_PostMessage;
	/// }
	///
	/// private void buttonCalculate_Click(object sender, EventArgs e)
	/// {
	///     this.webWorker.SendMessage(21);
	/// }
	///
	/// private void webWorker_PostMessage(object sender, WebWorkerPostMessageEventArgs e)
	/// {
	///     AlertBox.Show($"Result: {e.Data}"); // Result: 42
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(WebWorker))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The WebWorker component represents a JavaScript WebWorker instance that can run on the client and fire sever events and receive updates from the server.")]
	[ApiCategory("WebWorker")]
	public class WebWorker : Web.Component, IWisejHandler
	{
		// version counter, used to update the source code when it changes.
		private int version = 0;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.WebWorker.WebWorker" /> class.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var worker = new WebWorker();
		/// worker.JavaScriptSource = "Scripts/worker.js";
		/// worker.PostMessage += (s, e) => AlertBox.Show(e.Data?.ToString());
		/// ]]></code>
		/// </example>
		public WebWorker()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.WebWorker.WebWorker" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <remarks>
		/// Adding the component to a container ensures that it's disposed together with the container,
		/// for example when the owning page or form is disposed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // "components" is the container created by the designer for the page or form.
		/// var worker = new WebWorker(this.components);
		/// worker.JavaScript = "onmessage = function (e) { postMessage('Received: ' + e.data); };";
		/// ]]></code>
		/// </example>
		public WebWorker(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the WebWorker calls "postMessage".
		/// </summary>
		public event WebWorkerPostMessageEventHandler PostMessage;

		/// <summary>
		/// Fires the PostMessage event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPostMessage(WebWorkerPostMessageEventArgs e)
		{
			if (this.PostMessage != null)
				PostMessage(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns true if the client browser supports the JavaScript Worker class.
		/// </summary>
		public static bool IsSupported
		{
			get
			{
				bool? supported = Wisej.Web.Application.Browser?.Features?.worker;
				return supported == null || supported.Value == true;
			}
		}

		/// <summary>
		/// Returns or sets the JavaScript code to execute in the WebWorker process.
		/// </summary>
		[DefaultValue("")]
		[Editor("Wisej.Design.JavaScriptEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string JavaScript
		{
			get { return this._javaScript; }
			set
			{
				value = value ?? string.Empty;

				if (this._javaScript != value)
				{
					this._javaScript = value;
					Update();
				}
			}
		}
		private string _javaScript = string.Empty;

		/// <summary>
		/// Returns or sets the JavaScript file with the source code to execute in the WebWorker process.
		/// </summary>
		[DefaultValue("")]
		[Editor("Wisej.Design.JsFileSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string JavaScriptSource
		{
			get { return this._javaScriptSource; }
			set
			{
				value = value ?? string.Empty;

				if (this._javaScriptSource != value)
				{
					this._javaScriptSource = value;
					Update();
				}
			}
		}
		private string _javaScriptSource = string.Empty;

		#endregion

		#region Methods

		/// <summary>
		/// Terminates the current WebWorker.
		/// </summary>
		/// <remarks>
		/// The worker process in the browser is stopped immediately, without letting it finish its current work.
		/// A new worker is started automatically the next time <see cref="SendMessage"/> is called
		/// or when the <see cref="JavaScript"/> or <see cref="JavaScriptSource"/> property changes.
		/// </remarks>
		/// <example>
		/// The following example stops a long running worker when the user clicks a cancel button:
		/// <code><![CDATA[
		/// private void buttonCancel_Click(object sender, EventArgs e)
		/// {
		///     this.webWorker.Terminate();
		///     this.labelStatus.Text = "Canceled.";
		/// }
		/// ]]></code>
		/// </example>
		public void Terminate()
		{
			Call("terminate");
		}

		/// <summary>
		/// Sends the data object to the current WebWorker, if it's running.
		/// </summary>
		/// <remarks>
		/// If the worker is not running yet, it's started before the data is sent. The worker receives the
		/// data in the <c>data</c> property of the event passed to its <c>onmessage</c> handler.
		/// </remarks>
		/// <param name="data">The data object to send to the WebWorker. It's serialized to JSON before it's sent to the client.</param>
		/// <example>
		/// The following example sends an object to a worker that sums an array of numbers:
		/// <code><![CDATA[
		/// // worker script:
		/// // onmessage = function (e) {
		/// //     var sum = e.data.values.reduce(function (a, b) { return a + b; }, 0);
		/// //     postMessage({ name: e.data.name, sum: sum });
		/// // };
		///
		/// private void buttonSum_Click(object sender, EventArgs e)
		/// {
		///     this.webWorker.SendMessage(new
		///     {
		///         name = "Totals",
		///         values = new[] { 1, 2, 3, 4, 5 }
		///     });
		/// }
		///
		/// private void webWorker_PostMessage(object sender, WebWorkerPostMessageEventArgs e)
		/// {
		///     dynamic result = e.Data;
		///     AlertBox.Show($"{result.name}: {result.sum}"); // Totals: 15
		/// }
		/// ]]></code>
		/// </example>
		public void SendMessage(object data)
		{
			Call("sendMessage", data);
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		/// <remarks>
		/// Calling this method reloads the worker's source code on the client, which terminates the running
		/// worker and starts a new one. It's called automatically when the <see cref="JavaScript"/> or
		/// <see cref="JavaScriptSource"/> property changes.
		/// </remarks>
		/// <example>
		/// The following example restarts the worker after the script file has been changed on the server:
		/// <code><![CDATA[
		/// private void buttonReload_Click(object sender, EventArgs e)
		/// {
		///     File.WriteAllText(
		///         Path.Combine(Application.StartupPath, "Scripts/worker.js"),
		///         "onmessage = function (e) { postMessage(e.data.toUpperCase()); };");
		///
		///     this.webWorker.Update();
		/// }
		/// ]]></code>
		/// </example>
		public override void Update()
		{
			this.version++;
			if (this.version == int.MaxValue)
				this.version = 0;

			base.Update();
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "postMessage":
					OnPostMessage(new WebWorkerPostMessageEventArgs(e));
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			IWisejComponent me = this;
			base.OnWebRender((object)config);

			config.className = "wisej.ext.WebWorker";

			if (!me.DesignMode)
			{
				config.sourceUrl = this.GetPostbackURL() + "&v=" + this.version;

				WiredEvents events = new WiredEvents();
				events.Add("postMessage(Data)");
				config.wiredEvents = events;
			}
		}

		#endregion

		#region IWisejHandler

		/// <summary>
		/// Compress the output.
		/// </summary>
		bool IWisejHandler.Compress { get { return true; } }

		/// <summary>
		/// Process the HTTP request.
		/// </summary>
		/// <param name="context">The current <see cref="HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(HttpContext context)
		{
			string source = !String.IsNullOrEmpty(this.JavaScript)
					? this.JavaScript
					: GetJavaScriptFromFile(this.JavaScriptSource);

			context.Response.ContentType = "text/plain";
			context.Response.Write(source);
		}

		private string GetJavaScriptFromFile(string fileName)
		{
			try
			{
				if (String.IsNullOrEmpty(fileName))
					return null;

				// return the file in the application's directory, if present.
				var filePath = Path.Combine(Wisej.Web.Application.StartupPath, fileName);
				if (File.Exists(filePath))
				{
					using (StreamReader reader = new StreamReader(filePath))
					{
						return reader.ReadToEnd();
					}
				}

				// otherwise look for embedded resources in the calling assembly, which should be the app...
				var assembly = Assembly.GetCallingAssembly();
				string fullName = Array.Find(assembly.GetManifestResourceNames(), o => o.EndsWith(fileName));
				if (fullName != null)
				{
					using (Stream stream = assembly.GetManifestResourceStream(fullName))
					using (StreamReader reader = new StreamReader(stream))
					{
						return reader.ReadToEnd();
					}
				}
			}
			catch { }

			return null;
		}

		#endregion
	}
}
