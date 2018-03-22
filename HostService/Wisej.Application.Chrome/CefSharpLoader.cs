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
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Wisej.Application
{
	/// <summary>
	/// Extracts CefShap resources and loads CefSharp assemblies.
	/// </summary>
	internal static class CefSharpLoader
	{
		/// <summary>
		/// Initializes the CefSharp required assemblies, modules and resources.
		/// </summary>
		public static void Initialize()
		{
			ExtractCefSharp();
		}

		/// <summary>
		/// Returns the path where the CefSharp files have been extracted to.
		/// </summary>
		private static string CefSharpPath { get; set; }

		/// <summary>
		/// Extract all the CefSharp modules and resources.
		/// </summary>
		private static void ExtractCefSharp()
		{
			ExtractCefSharpResources("Wisej.Application.CefSharp.x64.", "");
			ExtractCefSharpResources("Wisej.Application.CefSharp.locales.", "locales");

			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			string assemblyPath = Path.Combine(CefSharpPath, new AssemblyName(args.Name).Name + ".dll");

			if (File.Exists(assemblyPath))
				return Assembly.LoadFrom(assemblyPath);

			return null;
		}

		/// <summary>
		/// Extract all the CefSharp modules and resources.
		/// </summary>
		/// <param name="prefix">Namespace prefix filter.</param>
		/// <param name="target">Target sub folder, can be empty.</param>
		private static void ExtractCefSharpResources(string prefix, string target)
		{
			Debug.Assert(prefix != null);
			Debug.Assert(target != null);

			var assembly = Assembly.GetEntryAssembly();
			var resources = assembly.GetManifestResourceNames();

			// extract CefSharp resources and modules into the temp directory
			// not to clog the hosting Wisej application folder.
			CefSharpPath = Path.Combine(Path.GetTempPath(), "CefSharp");
			Directory.CreateDirectory(CefSharpPath);
			Directory.SetCurrentDirectory(CefSharpPath);

			// create the target sub-directory.
			if (target != "" && !Directory.Exists(target))
				Directory.CreateDirectory(target);

			foreach (var r in resources)
			{
				if (r.StartsWith(prefix))
				{
					var name = Path.Combine(target, r.Substring(prefix.Length));
					if (!File.Exists(name))
					{
						using (var stream = assembly.GetManifestResourceStream(r))
						using (var file = new FileStream(name, FileMode.Create, FileAccess.ReadWrite))
						{
							stream.CopyTo(file);
						}
					}
				}
			}
		}
	}
}