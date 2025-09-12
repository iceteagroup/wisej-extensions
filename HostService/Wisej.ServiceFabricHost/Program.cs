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

using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Diagnostics;
using System.Threading;

namespace Wisej.ServiceFabricHost
{
	internal static class Program
	{
		/// <summary>
		/// This is the entry point of the service host process.
		/// </summary>
		private static void Main()
		{
			try
			{
				// The ServiceManifest.XML file defines one or more service type names.
				// Registering a service maps a service type name to a .NET type.
				// When Service Fabric creates an instance of this service type,
				// an instance of the class is created in this host process.

				ServiceRuntime.RegisterServiceAsync("ServiceFabricHostType",
					context => new ServiceFabricHost(context)).GetAwaiter().GetResult();

				ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ServiceFabricHost).Name);

				// Prevents this host process from terminating so services keep running.
				Thread.Sleep(Timeout.Infinite);
			}
			catch (Exception e)
			{
				ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
				throw;
			}
		}
	}
}
