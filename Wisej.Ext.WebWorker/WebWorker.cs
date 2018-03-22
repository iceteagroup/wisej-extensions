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
using System.Drawing.Design;
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
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(WebWorker))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The WebWorker component represents a JavaScript WebWorker instance that can run on the client and fire sever events and receive updates from the server.")]
	public class WebWorker : Wisej.Base.Component, IComponent, IWisejHandler
	{
		// version counter, used to update the source code when it changes.
		private int version = 0;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.WebWorker.WebWorker" /> class.
		/// </summary>
		public WebWorker()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.WebWorker.WebWorker" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
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
				bool? supported = ApplicationBase.Browser?.Features?.worker;
				return supported == null || supported.Value == true;
			}
		}

		/// <summary>
		/// Returns or sets the JavaScript code to execute in the WebWorker process.
		/// </summary>
		[DefaultValue("")]
		[Editor("Wisej.Web.Design.CodeEditor, Wisej.Web.Design", typeof(UITypeEditor))]
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
		[Editor("Wisej.Web.Design.JsFileSourceEditor, Wisej.Web.Design", typeof(UITypeEditor))]
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
		public void Terminate()
		{
			Call("terminate");
		}

		/// <summary>
		/// Sends the data object to the current WebWorker, if it's running.
		/// </summary>
		/// <param name="data"></param>
		public void SendMessage(object data)
		{
			Call("sendMessage", data);
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		public override void Update()
		{
			this.version++;
			if (this.version == int.MaxValue)
				this.version = 0;

			base.Update();
		}

		#endregion

		#region IComponent

		/// <summary>
		/// Returns or sets the <see cref="T:System.ComponentModel.ISite" /> associated with 
		/// the <see cref="T:System.ComponentModel.IComponent" />.
		/// </summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get { return this._site; }
			set
			{
				this._site = value;
				((IWisejComponent)this).DesignMode = value == null ? false : value.DesignMode;
			}
		}
		private ISite _site;

		/// <summary>
		/// Returns a value that indicates whether the <see cref="T:System.ComponentModel.IComponent" /> is currently in design mode.
		/// </summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IComponent" /> is in design mode; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get { return this._site != null && this._site.DesignMode; }
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
		/// Process the http request.
		/// </summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext"/>.</param>
		void IWisejHandler.ProcessRequest(System.Web.HttpContext context)
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
				var filePath = Path.Combine(ApplicationBase.StartupPath, fileName);
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
