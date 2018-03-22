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

using Microsoft.Owin;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;

namespace Wisej.HostService.Owin
{

	using WebSocketAccept =
		Action<
			IDictionary<string, object>, // options
			Func<IDictionary<string, object>, Task> // callback
		>;

	/// <summary>
	/// Wisej middleware for the Owin stack.
	/// </summary>
	internal class WisejMiddleware : OwinMiddleware
	{

		/// <summary>
		/// Process all requests.
		/// </summary>
		/// <param name="next"></param>
		public WisejMiddleware(OwinMiddleware next) : base(next)
		{
		}

		/// <summary>
		/// Process an individual request.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task Invoke(IOwinContext context)
		{
			var fileExtension = GetFileExtension(context.Request.Path.Value);

			switch (fileExtension)
			{
				case "":
					return ProcessSubApplication(context);

				case ".wx":
					return ProcessWisejRequest(context);

				default:
					return ProcessAspNetRequest(context);
			}
		}

		/// <summary>
		/// Processes requests without an extension checking if the name corresponds
		/// to a Wisej application json file.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		Task ProcessSubApplication(IOwinContext context)
		{
			var file = context.Request.Path.Value;
			if (file.StartsWith("/"))
				file = file.Substring(1);

			file += ".json";
			var path = Path.Combine(HttpRuntime.AppDomainAppPath, file);
			if (File.Exists(path))
			{
				var config = Wisej.Core.Configuration.GetInstance(path);
				if (config != null && !String.IsNullOrEmpty(config.Url))
				{
					string url = config.Url;
					if (!url.StartsWith("/"))
						url = "/" + url;

					context.Request.Path = new PathString(url);

					// restart the pipeline from the top.
					return Invoke(context);
				}
			}

			return this.Next.Invoke(context);
		}

		/// <summary>
		/// Processes all .wx requests calling the Wisej handler directly.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		Task ProcessWisejRequest(IOwinContext context)
		{
			var request = new WisejWorkerRequest(context);
			var httpContext = new HttpContext(request);
			HttpContext.Current = httpContext;

			// try to upgrade the connection to WebSocket.
			var task = UpgradeToWebSockets(context);
			if (task != null)
				return task;

			// process the wisej http request and return the async task
			// pegged to the async wisej handler EndProcessRequest.
			var handler = new Wisej.Core.HttpHandler();
			var async = handler.BeginProcessRequest(
				httpContext,
				r =>
				{
					// flush any custom filter on HttpResponse.
					FilterOutput(httpContext.Response);

					// complete the response.
					httpContext.Response.End();

				},
				null);

			return Task.Factory.FromAsync(async, handler.EndProcessRequest);
		}

		// HttpResponse.FilterOutput is internal.
		// We call it using the super fast expression compiler, it's the same as
		// if the method was public and linked directly.
		private void FilterOutput(HttpResponse response)
		{
			if (_filterOutput == null)
			{
				lock (this)
				{
					if (_filterOutput == null)
					{
						try
						{
							var target = Expression.Parameter(typeof(HttpResponse), "x");
							var call = Expression.Call(target, "FilterOutput", null);
							_filterOutput = Expression.Lambda<Action<HttpResponse>>(call, target).Compile();
						}
						catch {
							return;
						}
					}
				}
			}

			_filterOutput(response);
		}
		private static Action<HttpResponse> _filterOutput;

		/// <summary>
		/// Processes all ASP.NET requests passing  the context to the HttpRuntime.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		Task ProcessAspNetRequest(IOwinContext context)
		{
			var request = new WisejWorkerRequest(context);

			// process the classic pipeline and return the async task to the owin pipeline.
			try
			{
				var task = request.Task;
				HttpRuntime.ProcessRequest(request);
				return task;
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.Message + "\r\n" + ex.StackTrace);
				return Task.FromResult(0);
			}
		}

		private static string GetFileExtension(string url)
		{
			string[] parts = url.Split('/');
			for (int i = 0; i < parts.Length; i++)
			{
				if (Path.HasExtension(parts[i]))
				{
					url = String.Join("/", parts, 0, i + 1);
					break;
				}
			}

			return Path.GetExtension(url);
		}

		private Task UpgradeToWebSockets(IOwinContext context)
		{
			WebSocketAccept accept = context.Get<WebSocketAccept>("websocket.Accept");
			if (accept == null)
				return null;

			// WebSocket request?
			accept(null, ProcessWisejWebSocketRequest);
			return Task.FromResult<object>(null);
		}

		private async Task ProcessWisejWebSocketRequest(IDictionary<string, object> environment)
		{
			var handler = new Wisej.Core.HttpHandler();
			var context = new WisejWebSocketContext(environment);
			await handler.ProcessWebSocketRequest(context);
		}
	}
}
