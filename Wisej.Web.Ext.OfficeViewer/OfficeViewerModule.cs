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
using System.IO;
using System.Text;
using System.Web;

namespace Wisej.Web.Ext.OfficeViewer
{
	/// <summary>
	/// OfficeViewer module. Filters postback.wx requests
	/// that have been modified by the <see cref="OfficeViewer"/> control
	/// to pack the arguments into the URL.
	/// </summary>	
	class OfficeViewerModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += (sender, e) =>
			{
				var app = (HttpApplication)sender;
				var request = app.Request;
				var name = Path.GetFileName(request.PhysicalPath);
				if (name == "postback.wx")
				{
					// the real postback.wx URL is a base64 string
					// in the path after /postback.wx.

					var data = request.PathInfo;
					if (data.StartsWith("/"))
						data = data.Substring(1);

					// decode and redirect.
					try
					{
						byte[] bytes = Convert.FromBase64String(data);
						string postback = Encoding.UTF8.GetString(bytes);
						app.Context.RewritePath(postback);
					}
					catch { }
				}
			};
		}

		void IHttpModule.Dispose()
		{
		}
	}

	/// <summary>
	/// Self registration for <see cref="OfficeViewerModule"/>.
	/// </summary>
	/// <exclude/>
	public class OfficeViewerModuleStartup
	{
		public static void Start()
		{
			HttpApplication.RegisterModule(typeof(OfficeViewerModule));
		}
	}
}
