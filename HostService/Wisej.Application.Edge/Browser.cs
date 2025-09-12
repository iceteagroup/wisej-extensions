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

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Wisej.Application.Properties;

namespace Wisej.Application
{
	/// <summary>
	/// Browser wrapper control.
	/// </summary>
	internal class Browser : Control
	{
		private string url;
		private Control webView;

		static Browser()
		{
			ExtractEdgeNativeLoader();
		}

		/// <summary>
		/// Creates a new instance of the browser.
		/// Tries with Edge first, if not supported falls back to IE.
		/// </summary>
		/// <param name="url">Startup url.</param>
		public Browser(string url)
		{
			this.url = url;

			CreateEdge();
		}

		/// <summary>
		/// Fired when the browser has finished loading the document.
		/// </summary>
		public EventHandler DocumentCompleted;

		/// <summary>
		/// Returns the title of the loaded document.
		/// </summary>
		public string DocumentTitle
		{
			get;
			private set;
		}

		#region Edge

		private void CreateEdge()
		{
			var edge = new WebView2();

			var path = Path.Combine(Path.GetTempPath(), "Wisej2", "Edge");
			edge.CreationProperties = new CoreWebView2CreationProperties()
			{
				UserDataFolder = path
			};

			edge.KeyDown += this.Edge_KeyDown;
			edge.NavigationCompleted += Edge_NavigationCompleted;
			edge.CoreWebView2InitializationCompleted += Edge_CoreWebView2InitializationCompleted;

			var current = Directory.GetCurrentDirectory();
			try
			{
				Directory.SetCurrentDirectory(path);

				edge.Location = new Point(0, 0);
				edge.Dock = DockStyle.Fill;
				edge.Parent = this;
				edge.Source = new Uri(this.url);
				this.webView = edge;
			}
			finally
			{
				Directory.SetCurrentDirectory(current);
			}
		}

		private static void ExtractEdgeNativeLoader()
		{
			var tempPath = Path.Combine(Path.GetTempPath(), "Wisej2", "Edge");
			var filePath = Path.Combine(tempPath, "WebView2Loader.dll");

			Directory.CreateDirectory(tempPath);
			using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			using (var stream = typeof(Browser).Assembly.GetManifestResourceStream("Wisej.Application.Edge.WebView2Loader.dll"))
			{
				stream.CopyTo(file);
			}
		}

		private void Edge_CoreWebView2InitializationCompleted(object sender, EventArgs e)
		{
			var settings = ((WebView2)this.webView).CoreWebView2.Settings;

			settings.AreDevToolsEnabled = false;
		}

		private void Edge_KeyDown(object sender, KeyEventArgs e)
		{
			OnPreviewKeyDown(new PreviewKeyDownEventArgs(e.KeyCode));
		}

		private void Edge_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			this.DocumentTitle = ((WebView2)this.webView).CoreWebView2.DocumentTitle + " (Edge)";
			this.DocumentCompleted?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}
}
