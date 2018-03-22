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
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

namespace Wisej.HostService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			// uncomment to debug the service.
			// System.Diagnostics.Debugger.Launch();

			// install?
			if (ProcessInstallService(args))
				return;

			// run service?
			if (RunAsService(args))
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new Wisej.HostService.Service.Service()
				};
				ServiceBase.Run(ServicesToRun);
				return;
			}

			// stop process with the same name?
			if (StopProcess(args))
				return;

			// run standalone?
			StartProcess(args);
		}

		// Stops the process with the same name when the command line
		// specified -stop.
		private static bool StopProcess(string[] args)
		{
			Debug.Assert(args != null);

			if (args.Length > 0)
			{
				foreach (var a in args)
				{
					if (String.Equals(a, "-stop", StringComparison.InvariantCultureIgnoreCase))
					{
						var process = FindProcess();
						if (process != null)
						{
							process.Kill();
						}
						return true;
					}
				}
			}

			return false;
		}

		// Starts the standalone process, unless it's already started.
		private static bool StartProcess(string[] args)
		{
			Debug.Assert(args != null);

			var me = FindProcess();
			if (me != null)
			{
				Trace.TraceInformation(Service.Service.GetHostName(args) + " is already running.");
				return true;
			}

			// start and wait forever, until killed.
			me = Process.GetCurrentProcess();
			new Service.Service().Start();
			me.WaitForExit();
			return true;
		}

		// Starts the process as a service when there command line
		// specifies -s or -service (added by the service installer).
		private static bool RunAsService(string[] args)
		{
			Debug.Assert(args != null);

			if (args.Length > 0)
			{
				foreach (var a in args)
				{
					if (String.Equals(a, "-s", StringComparison.InvariantCultureIgnoreCase))
						return true;

					if (String.Equals(a, "-service", StringComparison.InvariantCultureIgnoreCase))
						return true;
				}
			}

			return false;
		}

		// Installs or Uninstalls the host process as a service
		// when the command line specifies -i, -install, -u, -uninstall.
		private static bool ProcessInstallService(string[] args)
		{
			Debug.Assert(args != null);

			if (args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "-i":
					case "-install":
						InstallService(args);
						return true;

					case "-u":
					case "-uninstall":
						RemoveService(args);
						return true;
				}
			}

			return false;
		}

		private static void InstallService(string[] args)
		{
			try
			{
				args[0] = Assembly.GetExecutingAssembly().Location;
				ManagedInstallerClass.InstallHelper(args);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
					ex = ex.InnerException;

				Trace.TraceError(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		private static void RemoveService(string[] args)
		{
			try
			{
				args = new string[2];
				args[0] = "/u";
				args[1] = Assembly.GetExecutingAssembly().Location;
				ManagedInstallerClass.InstallHelper(args);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
					ex = ex.InnerException;

				Trace.TraceError(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Finds a running process that matches the name of this application.
		private static Process FindProcess()
		{
			var me = Process.GetCurrentProcess();
			foreach (var p in Process.GetProcessesByName(me.ProcessName))
			{
				if (p.Id != me.Id && p.MainModule.FileName == me.MainModule.FileName)
					return p;
			}

			return null;
		}

	}
}
