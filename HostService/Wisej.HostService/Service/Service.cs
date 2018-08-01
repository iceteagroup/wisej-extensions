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
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using WinForms = System.Windows.Forms;
using Wisej.HostService.Owin;
using System.Web;

namespace Wisej.HostService.Service
{
	/// <summary>
	/// Starts the Wisej.HostService service.
	/// </summary>
	public partial class Service : ServiceBase
	{
		// the default server port, can be changed adding "-p:{port}" to the arguments.
		// when zero, the system will pick the first available port.
		private const int DEFAULT_PORT = 8080;

		// the default server domain., can be changed adding "-d:{domain}" to the arguments.
		private const string DEFAULT_DOMAIN = "*";

		// keep a reference to the running host, needed to shut down in OnStop.
		private WisejHost host = null;

		public Service()
		{
		}

		protected override void OnStart(string[] args)
		{
			Start();
		}

		protected override void OnStop()
		{
			this.host = null;
		}

		public void Start()
		{
			try
			{
				string[] args = Environment.GetCommandLineArgs();

				this.host = WisejHost.Create();
				this.host.Shutdown += Host_Shutdown;
				this.host.Start(GetDomainName(args), GetPortNumber(args));
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
					ex = ex.InnerException;

				if (Environment.UserInteractive)
					WinForms.MessageBox.Show(ex.Message, ex.GetType().Name, WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Error);

				throw;
			}
		}

		private void Host_Shutdown(object sender, EventArgs e)
		{
			switch (this.host.ShutdownReason)
			{
				case ApplicationShutdownReason.ChangeInGlobalAsax:
				case ApplicationShutdownReason.ConfigurationChange:
				case ApplicationShutdownReason.UnloadAppDomainCalled:
				case ApplicationShutdownReason.BinDirChangeOrDirectoryRename:
					{
						// restart the process when there is a configuration or bin change.
						Start();
					}
					break;

				default:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}

		/// <summary>
		/// Returns the port number specified in the command line.
		/// </summary>
		/// <returns></returns>
		internal static int GetPortNumber(string[] args)
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
		internal static string GetDomainName(string[] args)
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

		/// <summary>
		/// Returns the name of the service. It's either the specified in the 
		/// command line using -n: or the name of the directory of the web application being hosted.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		internal static string GetServiceName(string[] args)
		{
			return "Wisej.HostService:" + GetHostName(args);
		}

		/// <summary>
		/// Returns the name of the host either using the name of the folder
		/// where the executable is located or parsing the -n argument in the command line. 
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		internal static string GetHostName(string[] args)
		{
			Debug.Assert(args != null);

			foreach (var a in args)
			{
				if (a.StartsWith("-n:") && a.Length > 3)
					return a.Substring(3);
				if (a.StartsWith("-name:") && a.Length > 3)
					return a.Substring(6);
			}

			// if the name was not specified, use the application directory name,
			var location = Assembly.GetExecutingAssembly().Location;
			var filePath = RemoveTrailingBinName(Path.GetDirectoryName(location));
			return Path.GetFileName(filePath);
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
	}
}
