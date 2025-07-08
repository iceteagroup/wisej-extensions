///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
///
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Wisej.Application
{
	/// <summary>
	/// Extracts GeckoFx resources and loads GeckoFx assemblies.
	/// </summary>
	internal static class GeckoFxLoader
	{
		/// <summary>
		/// Initializes the GeckoFx required assemblies, modules and resources.
		/// </summary>
		public static void Initialize()
		{
			ExtractGeckoFx();
		}

		/// <summary>
		/// Returns the path where the GeckoFx files have been extracted to.
		/// </summary>
		private static string GeckoFxPath { get; set; }

		/// <summary>
		/// Extract all the GeckoFx modules and resources.
		/// </summary>
		private static void ExtractGeckoFx()
		{
			ExtractGeckoFxResources("Wisej.Application.GeckoFx.x64.", "");
			ExtractGeckoFxResources("Wisej.Application.GeckoFx.Firefox64.", "Firefox64");
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			var name = args.Name;
			if (name.StartsWith("Geckofx-Core") || name.StartsWith("Geckofx-Winforms"))
			{
				string assemblyPath = Path.Combine(GeckoFxPath, new AssemblyName(name).Name + ".dll");
				if (File.Exists(assemblyPath))
					return Assembly.LoadFrom(assemblyPath);
			}

			return null;
		}

		/// <summary>
		/// Extract all the GeckoFx modules and resources.
		/// </summary>
		/// <param name="prefix">Namespace prefix filter.</param>
		/// <param name="folder">Sub folder name in the temp directory.</param>
		private static void ExtractGeckoFxResources(string prefix, string folder)
		{
			Debug.Assert(prefix != null);

			var assembly = typeof(GeckoFxLoader).Assembly;
			var resources = assembly.GetManifestResourceNames();

			// extract CefSharp resources and modules into the temp directory
			// not to clog the hosting Wisej application folder.
			GeckoFxPath = Path.Combine(Path.GetTempPath(), "Wisej", "GeckoFx");
			Directory.CreateDirectory(GeckoFxPath);
			Directory.SetCurrentDirectory(GeckoFxPath);

			if (!String.IsNullOrEmpty(folder))
				Directory.CreateDirectory(folder);

			// verify the version of the CefSharp library.
			var update = UpdateGeckoFx(assembly);

			foreach (var r in resources)
			{
				if (r.StartsWith(prefix))
				{
					var filePath = Path.Combine(folder, r.Substring(prefix.Length));
					if (update || !File.Exists(filePath))
					{
						using (var stream = assembly.GetManifestResourceStream(r))
						using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
						{
							stream.CopyTo(file);
						}
					}
				}
			}
		}

		private static bool UpdateGeckoFx(Assembly assembly)
		{
			string currentVersion = null;
			string embeddedVersion = null;
			var versionFile = Path.Combine(GeckoFxPath, "version.txt");
			var versionStream = assembly.GetManifestResourceStream("Wisej.Application.GeckoFx.version.txt");

			if (versionStream != null)
			{
				if (File.Exists(versionFile))
				{
					using (var reader1 = new StreamReader(versionFile))
					using (var reader2 = new StreamReader(versionStream))
					{
						currentVersion = reader1.ReadToEnd();
						embeddedVersion = reader2.ReadToEnd();

						if (currentVersion == embeddedVersion)
							return false;
					}
				}

				using (var file = new StreamWriter(versionFile))
				{
					file.Write(embeddedVersion);
				}
			}

			return true;
		}

	}
}
