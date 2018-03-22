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

using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Wisej.HostService.Owin;

namespace Wisej.ServiceFabricHost
{
	/// <summary>
	/// Starts the WisejHost application upon request from the servic fabric runtime.
	/// </summary>
	class WisejCommunicationListener : ICommunicationListener
	{
		private WisejHost wisejHost;
		private StatelessServiceContext context;

		public WisejCommunicationListener(StatelessServiceContext context)
		{
			this.context = context;
		}

		public Task<string> OpenAsync(CancellationToken cancellationToken)
		{
			var endpoint = this.context.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");

			this.wisejHost = WisejHost.Create();
			this.wisejHost.Start(this.context.NodeContext.IPAddressOrFQDN, endpoint.Port);
			return Task.FromResult(this.wisejHost.Url);
		}

		public void Abort()
		{
			StopWebServer();
		}

		public Task CloseAsync(CancellationToken cancellationToken)
		{
			StopWebServer();
			return Task.FromResult(true);
		}

		private void StopWebServer()
		{
			if (this.wisejHost != null)
			{
				try
				{
					this.wisejHost.Dispose();
				}
				catch (ObjectDisposedException)
				{
					// no-op
				}
			}
		}
	}
}
