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

using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;

namespace Wisej.HostService.Owin
{
	/// <summary>
	/// Host implementation. An instance of this class is registered with the ASP.NET application manager.
	/// When created, it starts an Owin host process in the ASP.NET domain and processes all incoming request
	/// using the Asp.Net pipeline.
	/// </summary>
	internal class WisejHost : MarshalByRefObject, IRegisteredObject, IDisposable
	{
		// the instance of the owin host.
		private IDisposable webApp;

		/// <summary>
		/// Creates a new instance of WisejHost in the ASP.NET application domain.
		/// </summary>
		/// <param name="baseDirectory">
		/// Base directory of the web application, where web.config is located.
		/// If omitted, it uses the directory of the executable or the directory
		/// specified using "-debug:" in the command line when in debug mode.
		/// </param>
		/// <returns></returns>
		public static WisejHost Create(string baseDirectory = null)
		{
#if DEBUG
			baseDirectory = baseDirectory ?? GetDebugDirectory();
#endif

			string virtualDir = "/";
			string physicalDir = baseDirectory ?? AppDomain.CurrentDomain.BaseDirectory;

			return WisejHost.CreateApplicationHost(virtualDir, physicalDir);

		}

		private static string GetDebugDirectory()
		{
			var args = Environment.GetCommandLineArgs();
			foreach (var a in args)
			{
				if (a.StartsWith("-debug:"))
				{
					var path = a.Substring("-debug:".Length);
					if (path != "" && path.Length > 2 && path.StartsWith("\""))
						path = path.Substring(1, path.Length - 2);

					return path;
				}
			}

			return null;
		}

		/// <summary>
		/// Occurs when the <see cref="WisejHost"/> instance is terminated. The reason
		/// for the termination is enumerate in the <see cref="ShutdownReason"/> property.
		/// </summary>
		public event EventHandler Shutdown;

		/// <summary>
		/// Returns the URL (with port) to the hosted application.
		/// </summary>
		public string Url
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the domain name that this host is listening to.
		/// </summary>
		public string Domain
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the port number that this host is listening to.
		/// </summary>
		public int Port
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the reason why this instance of <see cref="WisejHost"/> was terminated.
		/// </summary>
		public ApplicationShutdownReason ShutdownReason
		{
			get;
			private set;
		}

		/// <summary>
		/// Stops the host.
		/// </summary>
		/// <param name="immediate"></param>
		public void Stop(bool immediate)
		{
			Trace.TraceInformation("Stopping Wisej.Host: Domain={0}, Port={1}, Reason={2}", this.Domain, this.Port, HostingEnvironment.ShutdownReason);

			try
			{
				HostingEnvironment.UnregisterObject(this);
			}
			catch { };

			this.webApp?.Dispose();

			this.ShutdownReason = HostingEnvironment.ShutdownReason;

			this.Shutdown?.Invoke(null, EventArgs.Empty);
		}

		/// <summary>
		/// Stops the host.
		/// </summary>
		public void Dispose()
		{
			Stop(true);
		}

		/// <summary>
		/// Starts the Owin host at the specified port.
		/// </summary>
		/// <param name="domain">The domain recognized by the server. Use * for all domains.</param>
		/// <param name="port">The port to listen to. If set to 0 it will use the first available port.</param>
		public void Start(string domain, int port)
		{
			if (domain == null || domain == "")
				throw new ArgumentException("Invalid domain: " + domain);

			if (port < 0)
				throw new ArgumentException("Invalid port: " + port);

			if (port == 0)
				port = GetAvailablePort();

			// save the domain we are listening to.
			this.Port = port;
			this.Domain = domain;
			this.Url = "http://" + domain + ":" + port;

			Trace.TraceInformation("Starting Wisej.Host: Domain={0}, Port={1}, Reason={2}", this.Domain, this.Port, HostingEnvironment.ShutdownReason);

			// register with the .NET hosting system.
			HostingEnvironment.RegisterObject(this);

			// determine if this service is running in an ILMerged self contained executable.
			var mergedFactoryType
				= "Microsoft.Owin.Host.HttpListener.OwinServerFactory, " +
				Assembly.GetExecutingAssembly().FullName;

			// create the options needed by the owin host.
			StartOptions options = new StartOptions(this.Url);

			if (Type.GetType(mergedFactoryType, false) != null)
				options.ServerFactory = mergedFactoryType;


			this.webApp = WebApp.Start(options, (builder) =>
			{

				/** TODO: Add additional middleware handlers here. */

				// =============================================
				// FileServer middleware processes all static
				// resources: html, png, etc...
				//
				// It should run before the Wisej middleware to
				// serve known static files.
				// ---------------------------------------------
				builder.UseFileServer(new FileServerOptions
				{
					EnableDefaultFiles = true,
					EnableDirectoryBrowsing = true,
					FileSystem = new PhysicalFileSystem(HttpRuntime.AppDomainAppPath)
				});

				// =============================================
				// Wisej Middleware processes all .wx, .aspx requests
				// through the System.Web pipeline.
				//
				// It will process all files not handled by the
				// FileServer middleware using the classic
				// pipeline and any handler/module added to
				// web.config.
				// ---------------------------------------------
				builder.UseWisej();

			});
		}

		/// <summary>
		/// Returns the first available port.
		/// </summary>
		/// <returns></returns>
		private static int GetAvailablePort()
		{
			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
				return ((IPEndPoint)socket.LocalEndPoint).Port;
			}
		}

		/// <summary>
		/// Creates an instance of this class in the ASP.NET AppDomain.
		/// </summary>
		/// <param name="virtualDir">Name of the Virtual Directory that hosts this application. Not really used, other than on error messages and ASP Server Variable return values.</param>
		/// <param name="physicalDir">The physical location of the Virtual Directory for the application</param>
		/// <returns>object instance to the wwAspRuntimeProxy class you can call ProcessRequest on. Note this instance returned
		/// is a remoting proxy
		/// </returns>	
		private static WisejHost CreateApplicationHost(string virtualDir, string physicalDir)
		{
			if (!(physicalDir.EndsWith("\\")))
				physicalDir = physicalDir + "\\";

			// NOTE: We have two solutions to make this assembly discoverable by the application host's AppDomain.
			// 
			// 1) Copy this assembly to the application's /bin.
			// 2) Register this assembly's path with the application host's AppDomain.

			/*
			// copy this hosting app into the /bin directory of the application or ApplicationHost will not be able to load
			// the assembly when it created a new AppDomain. Apparently /bin is hardwired.
			string exePath = Assembly.GetExecutingAssembly().Location;
			try
			{
				if (!Directory.Exists(physicalDir + "bin\\"))
					Directory.CreateDirectory(physicalDir + "bin\\");

				File.Copy(exePath, physicalDir + "bin\\" + Path.ChangeExtension(Path.GetFileName(exePath), "dll"), true);
			}
			catch { }

			return (WisejHost)ApplicationHost.CreateApplicationHost(typeof(WisejHost), virtualDir, physicalDir);
			*/

			// register this assembly in the application host's AppDomain using a hack
			// from the Cassini source code. not sure what are the consequences of one method versus the other.
			var hostType = typeof(WisejHost);
			var appManager = ApplicationManager.GetApplicationManager();
			var _appId = (String.Concat(virtualDir, physicalDir).GetHashCode()).ToString("x");
			var buildManagerHostType = typeof(HttpRuntime).Assembly.GetType("System.Web.Compilation.BuildManagerHost");

			IRegisteredObject buildManagerHost = appManager.CreateObject(_appId, buildManagerHostType, virtualDir, physicalDir, false);

			// make our host type loadable outside of the hard coded /bin path.
			buildManagerHostType.InvokeMember("RegisterAssembly",
											  BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
											  null,
											  buildManagerHost,
											  new object[] { hostType.Assembly.FullName, hostType.Assembly.Location });

			return (WisejHost)appManager.CreateObject(_appId, hostType, virtualDir, physicalDir, false);
		}

		/// <summary>
		/// Obtains a lifetime service object to control the lifetime policy for this instance. In our case
		/// we return null let this object live forever.
		/// </summary>
		/// <returns></returns>
		[SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override Object InitializeLifetimeService()
		{
			return null;
		}
	}
}
