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

namespace Owin
{
	/// <summary>
	/// Adds "UseWisej" to the Owin system.
	/// </summary>
	public static class WisejExtensions
	{
		/// <summary>
		/// Captures *.wx requests and passes them to Wisej for processing.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IAppBuilder UseWisej(this IAppBuilder builder)
		{
			if (builder == null)
				throw new ArgumentNullException("builder");

			return builder.Use<Wisej.HostService.Owin.WisejMiddleware>();
		}
	}
}
