///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Wisej.Web.Ext.AspNetControl
{
    /// <summary>
    /// Custom module that installs a virtual path provider for the AspNetControl wrapper.
    /// </summary>
    /// <remarks>
    /// The virtual path provider is used to "feed" a blank ASP.NET page that
    /// hosts the ASP.NET control that we are wrapping.
    /// </remarks>
	[ApiCategory("ASPNetControl")]
    public class HttpModule : IHttpModule
	{
		private const string ASPNETHOSTPAGE = "/Wisej.AspNetHost.aspx";
		private const string ASPNETHOSTPAGERESOURCE = "Wisej.Web.Ext.AspNetControl.Wisej.AspNetHost.aspx";

		/// <summary>
		/// Initializes the AspNetControl module.
		/// </summary>
		/// <param name="app"></param>
		public void Init(HttpApplication app)
		{
			HostingEnvironment.RegisterVirtualPathProvider(new WisejVirtualPathProvider());
		}

		/// <summary>
		/// Disposes resources associated with the module.
		/// </summary>
		public void Dispose()
		{
			// nothing to dispose so far.
		}

		/// <summary>
		/// Our VirtualPathProvider, used to return the embedded AspNetHost page instead of using
		/// a temporary file or wrapping the default aspx handler.
		/// </summary>
		private class WisejVirtualPathProvider : VirtualPathProvider
		{
			public override bool FileExists(string virtualPath)
			{
				return
					IsAspNetHostPage(virtualPath)
					|| base.FileExists(virtualPath);
			}

			public override VirtualFile GetFile(string virtualPath)
			{
				return
					IsAspNetHostPage(virtualPath)
						? new WisejVirtualFile(virtualPath)
						: base.GetFile(virtualPath);
			}

			public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
			{
				return
					IsAspNetHostPage(virtualPath)
						? virtualPath
						: base.GetFileHash(virtualPath, virtualPathDependencies);
			}

			public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
			{
				if (IsAspNetHostPage(virtualPath))
				{
					return null;
				}

				return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
			}

			private bool IsAspNetHostPage(string virtualPath)
			{
				return virtualPath.EndsWith(ASPNETHOSTPAGE);
			}
		}

		/// <summary>
		/// Our VirtualFile override, returns the embedded AspBetHost stream.
		/// </summary>
		private class WisejVirtualFile : VirtualFile
		{
			public WisejVirtualFile(string virtualPath)
				: base(virtualPath)
			{
			}

			public override System.IO.Stream Open()
			{
				using (Stream stream = typeof(HttpModule).Assembly.GetManifestResourceStream(ASPNETHOSTPAGERESOURCE))
				{
					MemoryStream fileContent = new MemoryStream();
					stream.CopyTo(fileContent, 1024);
					stream.Flush();

					fileContent.Position = 0;
					return fileContent;
				}
			}
		}
	}
}