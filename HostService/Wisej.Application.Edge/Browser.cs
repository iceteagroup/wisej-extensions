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

using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Win32.UI.Controls.WinForms;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Wisej.Application
{
	/// <summary>
	/// Browser wrapper control. Works either as IE or Edge if supported.
	/// </summary>
	internal class Browser : Control
	{
		private string url;
		private Control webView;

		static Browser()
		{
			SetEmulationVersion(GetInternetExplorerMajorVersion());
		}

		/// <summary>
		/// Creates a new instance of the browser.
		/// Tries with Edge first, if not supported falls back to IE.
		/// </summary>
		/// <param name="url">Startup url.</param>
		public Browser(string url)
		{
			this.url = url;

			try
			{
				// get ready to load Microsoft.Toolkit.Win32.UI.Controls.dll
				AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

				// allow Win32WebViewHost to use localhost.
				var pi = new ProcessStartInfo()
				{
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					FileName = "checknetisolation",
					Arguments = "LoopbackExempt -a -n=Microsoft.Win32WebViewHost_cw5n1h2txyewy"
				};
				Process.Start(pi);

				CreateEdge();
			}
			catch
			{
				CreateIE();
			}
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
			var edge = new WebView();
			((ISupportInitialize)edge).BeginInit();
			edge.NavigationCompleted += Edge_NavigationCompleted;
			edge.AcceleratorKeyPressed += Edge_AcceleratorKeyPressed;
			edge.IsPrivateNetworkClientServerCapabilityEnabled = true;
			((ISupportInitialize)edge).EndInit();

			edge.Location = new Point(0, 0);
			edge.Dock = DockStyle.Fill;
			edge.Parent = this;
			edge.Navigate(this.url);

			this.webView = edge;
		}

		// Need to extract and load Microsoft.Toolkit.Win32.UI.Controls.dll.
		// it cannot be ILMerged.
		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
		{
			if (e.Name.StartsWith("Microsoft.Toolkit.Win32.UI.Controls"))
			{
				var path = Path.Combine(Path.GetTempPath(), "Microsoft.Toolkit.Win32.UI.Controls.dll");
				using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
				using (var stream = typeof(Browser).Assembly.GetManifestResourceStream("Wisej.Application.Edge.Microsoft.Toolkit.Win32.UI.Controls.dll"))
				{
					stream.CopyTo(file);
				}
				return Assembly.LoadFrom(path);
			}

			return null;
		}

		private void Edge_AcceleratorKeyPressed(object sender, WebViewControlAcceleratorKeyPressedEventArgs e)
		{
			OnPreviewKeyDown(new PreviewKeyDownEventArgs((Keys)e.VirtualKey));
		}

		private void Edge_NavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
		{
			this.DocumentTitle = ((WebView)this.webView).DocumentTitle + " (Edge)";
			this.DocumentCompleted?.Invoke(this, EventArgs.Empty);
		}

		#endregion

		#region Explorer

		private void CreateIE()
		{
			var explorer = new WebBrowser();
			explorer.DocumentCompleted += Explorer_DocumentCompleted;
			explorer.PreviewKeyDown += Explorer_PreviewKeyDown;

			explorer.Location = new Point(0, 0);
			explorer.Dock = DockStyle.Fill;
			explorer.Parent = this;
			explorer.Navigate(this.url);

			this.webView = explorer;

			if (GetInternetExplorerMajorVersion() == 11)
				SetUserAgent("Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
			else
				SetUserAgent("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)");
		}

		private void Explorer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			OnPreviewKeyDown(e);
		}

		private void Explorer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			this.DocumentTitle = ((WebBrowser)this.webView).DocumentTitle + " (IE)";
			this.DocumentCompleted?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Changes the user agent for the embedded IE.
		/// </summary>
		/// <param name="ua"></param>
		private static void SetUserAgent(string ua)
		{
			UrlMkSetSessionOption(0x10000002, null, 0, 0);
			UrlMkSetSessionOption(0x10000001, ua, ua.Length, 0);
		}

		/// <summary>
		/// Sets the IE emulation version for the embedded browser.
		/// </summary>
		private static void SetEmulationVersion(int version)
		{
			try
			{
				string programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
				RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
				key.SetValue(programName, version * 1000, RegistryValueKind.DWord);

				key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_NINPUT_LEGACYMODE");
				key.SetValue(programName, 0, RegistryValueKind.DWord);

				// 64 bit
				if (IntPtr.Size == 8)
				{
					key = Registry.CurrentUser.CreateSubKey(@"Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
					key.SetValue(programName, version * 1000, RegistryValueKind.DWord);

					key = Registry.CurrentUser.CreateSubKey(@"Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_NINPUT_LEGACYMODE");
					key.SetValue(programName, 0, RegistryValueKind.DWord);
				}
			}
			catch { }
		}

		/// <summary>
		/// Determine the version if the IE that is installed.
		/// </summary>
		/// <returns></returns>
		private static int GetInternetExplorerMajorVersion()
		{
			int result = 10;
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer");
				if (key != null)
				{
					object value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

					if (value != null)
					{
						string version = value.ToString();
						int separator = version.IndexOf('.');
						if (separator != -1)
							int.TryParse(version.Substring(0, separator), out result);
					}
				}
			}
			catch { }

			return result;
		}

		#region UrlMkSetSessionOption

		[DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
		private static extern int UrlMkSetSessionOption(
			int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

		#endregion

		#endregion
	}
}
