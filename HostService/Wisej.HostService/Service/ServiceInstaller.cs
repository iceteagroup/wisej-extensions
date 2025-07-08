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
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;

namespace Wisej.HostService.Service
{
	/// <summary>
	/// Run by C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.
	/// 
	/// Installs the Wisej.HostService as a service adding the additional custom arguments
	/// to the service ImagePath property.
	/// 
	/// The name of the service is set to include the name of the Wisej application being served.
	/// 
	/// </summary>
	[RunInstaller(true)]
	public class ServiceInstaller : Installer
	{
		private string serviceName;
		private string[] serviceArgs;
		private bool startAfterInstall;

		public ServiceInstaller()
		{
			var args = Environment.GetCommandLineArgs();
			var hostName = Service.GetHostName(args);
			var serviceName = Service.GetServiceName(args);

			this.serviceArgs = GetServiceArgs(args);
			this.startAfterInstall = GetStartService(args);
			this.serviceName = serviceName;

			// add startup arguments.
			if (this.serviceArgs != null && this.serviceArgs.Length > 0)
				this.BeforeInstall += ServiceInstaller_BeforeInstall;

			// start after install.
			if (this.startAfterInstall)
				this.AfterInstall += ServiceInstaller_AfterInstall;

			// install/uninstall the service.
			var installer = new System.ServiceProcess.ServiceInstaller();
			var installer2 = new System.ServiceProcess.ServiceProcessInstaller();
			installer.ServiceName = serviceName;
			installer.DisplayName = serviceName;
			installer.Description = "Wisej self hosting service for the " + hostName + " application.";
			installer.StartType = ServiceStartMode.Automatic;
			base.Installers.Add(installer);
			installer2.Account = ServiceAccount.LocalSystem;
			installer2.Password = null;
			installer2.Username = null;
			base.Installers.Add(installer2);
		}

		private void ServiceInstaller_BeforeInstall(object sender, InstallEventArgs e)
		{
			Context.Parameters["assemblypath"] = AppendPathParameters(Context.Parameters["assemblypath"], this.serviceArgs);
		}

		private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
		{
			using (ServiceController sc = new ServiceController(this.serviceName))
			{
				sc.Start();
			}
		}

		protected virtual string AppendPathParameters(string path, string[] parameters)
		{
			if (path.Length > 0 && path[0] != '"')
				path = "\"" + path + "\"";

			// additional arguments.
			foreach (var p in parameters)
				path += " " + p;

			return path;
		}

		
		private bool GetStartService(string[] args)
		{
			Debug.Assert(args != null);

			foreach (var a in args)
			{
				if (String.Equals(a, "-start", StringComparison.InvariantCultureIgnoreCase))
					return true;
			}

			return false;
		}

		private string[] GetServiceArgs(string[] args)
		{
			Debug.Assert(args != null);

			List<string> parameters = new List<string>();

			// mandatory argument to recognize service startup.
			parameters.Add("-s");

			foreach (var a in args)
			{
				// port to listen to.
				if (a.StartsWith("-p:", StringComparison.InvariantCultureIgnoreCase))
					parameters.Add(a);
				else if (a.StartsWith("-port:", StringComparison.InvariantCultureIgnoreCase))
					parameters.Add(a);

				// allowed domain.
				else if (a.StartsWith("-d:", StringComparison.InvariantCultureIgnoreCase))
					parameters.Add(a);
				else if (a.StartsWith("-domain:", StringComparison.InvariantCultureIgnoreCase))
					parameters.Add(a);

				/** TODO: Anything else goes here. */
			}

			return parameters.ToArray();
		}

	}
}
