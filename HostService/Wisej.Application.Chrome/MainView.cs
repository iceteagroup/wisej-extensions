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

using CefSharp;
using CefSharp.WinForms;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Wisej.HostService.Owin;
using WinForms = System.Windows.Forms;

namespace Wisej.Application
{
	/// <summary>
	/// Main form hosting the Browser control.
	/// </summary>
	public partial class MainView : Form
	{
		// Chromium.
		Browser browser;

		// reference to the host serving the Wisej application.
		WisejHost host;

		// names of splash images to replace the built-in splash image.
		string[] SplashImages = new string[] { "splash.png", "splash.gif", "splash.jpg" };

		/// <summary>
		/// Initializes the MainView.
		/// </summary>
		public MainView()
		{
			InitializeComponent();
		}

		#region Properties

		/// <summary>
		/// Returns or sets a value indicating whether the user can switch to full screen mode pressing F11.
		/// </summary>
		[DefaultValue(false)]
		[Description("Returns or sets a value indicating whether the user can switch to full screen mode pressing F11.")]
		public bool AllowFullScreenMode
		{
			get;
			set;
		}

		#endregion

		#region Implementation

		protected override void OnLoad(EventArgs e)
		{
			LoadSplashImage();
			RestoreUserBounds();

			base.OnLoad(e);
		}

		protected override void OnShown(EventArgs e)
		{
			FullScreenMode();
			WinForms.Application.DoEvents();

			try
			{
				StartServer();
				CreateBrowser();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.GetBaseException().Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			base.OnShown(e);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			if (!e.Cancel)
			{
				SaveUserBounds();
				try
				{
					Cef.Shutdown();
				}
				catch { }
			}
		}

		private void LoadSplashImage()
		{
			try
			{
				foreach (var file in SplashImages)
				{
					var path = Path.Combine(WinForms.Application.StartupPath, file);
					if (File.Exists(path))
					{
						Image image = Image.FromFile(path);
						this.pictureBoxLogo.Image = image;
						WinForms.Application.DoEvents();
						break;
					}
				}
			}
			catch { }
		}

		private void CreateBrowser()
		{
			Control.CheckForIllegalCrossThreadCalls = false;

			CefSettings settings = new CefSettings()
			{
				LogSeverity = LogSeverity.Disable,
				PersistSessionCookies = true,
				CachePath = Path.Combine(Path.GetTempPath(), "CefSharp"),
				BrowserSubprocessPath = Path.Combine(CefSharpLoader.CefSharpPath, "CefSharp.BrowserSubprocess.exe")

				/** TODO: Add custom settings here. */
			};
			Cef.Initialize(settings);

			this.browser = new Browser(this.host.Url + "?standalone=true");
			this.browser.DownloadHandler = new DownloadHandler();
			this.browser.Dock = DockStyle.Fill;
			this.browser.TitleChanged += this.Browser_TitleChanged;

			this.Controls.Add(this.browser);
			this.browser.BringToFront();

			if (this.AllowFullScreenMode)
				this.browser.PreviewKeyDown += Browser_PreviewKeyDown;
		}

		private void Browser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.F11)
				FullScreenMode(true);
			if (e.KeyCode == Keys.Escape)
				FullScreenMode(false);
		}

		private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
		{
			this.Text = e.Title;
			this.browser.TitleChanged -= this.Browser_TitleChanged;
		}

		private void StartServer()
		{
			string[] args = Environment.GetCommandLineArgs();

			this.host = WisejHost.Create();
			this.host.Start(GetDomainName(args), GetPortNumber(args));
		}

		#endregion

		#region Utility

		// the default server port, can be changed adding "-p:{port}" to the arguments.
		// when zero, the system will pick the first available port.
		private const int DEFAULT_PORT = 0;

		// the default server domain., can be changed adding "-d:{domain}" to the arguments.
		private const string DEFAULT_DOMAIN = "localhost";

		/// <summary>
		/// Makes the window go into full screen mode.
		/// </summary>
		/// <param name="fullScreen">Optional flag to force full screen mode: true or false. Default is null, in which case
		/// it searches the command line for the -fullscreen switch.
		/// </param>
		private void FullScreenMode(bool? fullScreen = null)
		{
			bool topMost = false;

			if (!fullScreen.HasValue)
			{
				string[] args = Environment.GetCommandLineArgs();

				foreach (var a in args)
				{
					if (a.Equals("-fullscreen:topmost", StringComparison.InvariantCultureIgnoreCase))
					{
						fullScreen = true;
						topMost = true;
						break;
					}
					if (a.Equals("-fullscreen", StringComparison.InvariantCultureIgnoreCase))
					{
						fullScreen = true;
						break;
					}
				}
			}

			if (fullScreen.HasValue)
			{
				if (fullScreen == true)
				{
					this.FormBorderStyle = FormBorderStyle.None;
					this.WindowState = FormWindowState.Maximized;
					this.TopMost = topMost;
				}
				else
				{
					this.FormBorderStyle = FormBorderStyle.Sizable;
					this.WindowState = FormWindowState.Normal;
					this.TopMost = false;
				}
			}
		}

		/// <summary>
		/// Returns the port number specified in the command line.
		/// </summary>
		/// <returns></returns>
		private static int GetPortNumber(string[] args)
		{
			Debug.Assert(args != null);

			int port = DEFAULT_PORT;

			foreach (var a in args)
			{
				if (a.StartsWith("-p:", StringComparison.InvariantCultureIgnoreCase))
				{
					int.TryParse(a.Substring(3), out port);
					break;
				}
				if (a.StartsWith("-port:", StringComparison.InvariantCultureIgnoreCase))
				{
					int.TryParse(a.Substring(6), out port);
					break;
				}
			}

			return port;
		}

		/// <summary>
		/// Returns the domain name specified in the command line or "localhost".
		/// </summary>
		/// <returns></returns>
		private static string GetDomainName(string[] args)
		{
			Debug.Assert(args != null);

			string domain = DEFAULT_DOMAIN;

			foreach (var a in args)
			{
				if (a.StartsWith("-d:", StringComparison.InvariantCultureIgnoreCase))
				{
					domain = a.Substring(3);
					break;
				}
				if (a.StartsWith("-domain:", StringComparison.InvariantCultureIgnoreCase))
				{
					domain = a.Substring(8);
					break;
				}
			}

			return domain;
		}

		private static string RemoveTrailingBinName(string path)
		{
			var removeNames = new[] { "\\bin", "\\debug", "\\release" };

			bool found = false;
			do
			{
				found = false;
				foreach (var r in removeNames)
				{
					if (path.EndsWith(r, StringComparison.InvariantCultureIgnoreCase))
					{
						found = true;
						path = path.Substring(0, path.Length - r.Length);
					}
				}
			} while (found);

			return path;
		}

		private RegistryKey AppRegistryKey
		{
			get
			{
				if (this._appRegistryKey == null)
					this._appRegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\IceTeaGroup\\Wisej\\Application");

				return this._appRegistryKey;
			}
		}
		private RegistryKey _appRegistryKey;

		private void RestoreUserBounds()
		{
			try
			{
				string size = (string)AppRegistryKey.GetValue("size");
				string location = (string)AppRegistryKey.GetValue("location");
				string windowState = (string)AppRegistryKey.GetValue("windowState");

				var bounds = this.Bounds;

				if (size != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Size));
					var dimensions = (Size)converter.ConvertFromString(null, CultureInfo.InvariantCulture, size);
					if (dimensions.Width > 100 && dimensions.Height > 100)
						bounds.Size = dimensions;
					else
						size = null;
				}

				if (location != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Point));
					var point = (Point)converter.ConvertFromString(null, CultureInfo.InvariantCulture, location);
					if (point.X > -10000 && point.Y > -10000)
						bounds.Location = point;
					else
						location = null;
				}

				if (size == null || location == null)
				{
					CenterToScreen();
				}
				else
				{
					// ensure that there is a valid monitor at the location.
					foreach (var screen in Screen.AllScreens)
					{
						if (screen.Bounds.Contains(bounds))
						{
							this.Bounds = bounds;
							break;
						}
					}
				}

				if (windowState != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(FormWindowState));
					this.WindowState = (FormWindowState)converter.ConvertFromString(null, CultureInfo.InvariantCulture, windowState);
				}

			}
			catch
			{
				CenterToScreen();
			}
		}

		private void SaveUserBounds()
		{
			try
			{
				Rectangle bounds =
					this.WindowState == FormWindowState.Normal
						? this.Bounds
						: this.RestoreBounds;

				// size
				if (!bounds.Size.IsEmpty)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Size));
					AppRegistryKey.SetValue("size", converter.ConvertToString(null, CultureInfo.InvariantCulture, bounds.Size), Microsoft.Win32.RegistryValueKind.String);
				}

				// location
				if (bounds.Location.X > -10000 && bounds.Location.Y > -10000)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Point));
					AppRegistryKey.SetValue("location", converter.ConvertToString(null, CultureInfo.InvariantCulture, bounds.Location), Microsoft.Win32.RegistryValueKind.String);
				}

				// window state
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(FormWindowState));
					AppRegistryKey.SetValue("windowState", converter.ConvertToString(this.WindowState), Microsoft.Win32.RegistryValueKind.String);
				}
			}
			catch { }
		}

		#endregion
	}
}
