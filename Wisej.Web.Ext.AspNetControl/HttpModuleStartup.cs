///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.ComponentModel;
using System.Web;

namespace Wisej.Web.Ext.AspNetControl
{
	/// <summary>
	/// Self registration for <see cref="HttpModule"/>.
	/// </summary>
	/// <exclude/>
	[ApiCategory("ASPNetControl")]
	public class HttpModuleStartup
	{
		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <remarks>
		/// Registers the <see cref="HttpModule"/> with the ASP.NET application. ASP.NET calls this method automatically
		/// before the application starts, through the <see cref="T:System.Web.PreApplicationStartMethodAttribute"/>
		/// declared in this assembly, so you don't need to call it or add the module to Web.config.
		/// </remarks>
		/// <example>
		/// The following example shows the assembly attribute that runs this method on startup:
		/// <code><![CDATA[
		/// [assembly: PreApplicationStartMethod(typeof(Wisej.Web.Ext.AspNetControl.HttpModuleStartup), "Start")]
		/// ]]></code>
		/// </example>
		/// <exclude/>
		public static void Start()
		{
			HttpApplication.RegisterModule(typeof(HttpModule));
		}
	}
}