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

using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Wisej.Application
{
	/// <summary>
	/// IE WebBrowser configured to behave as the newest IE installer version.
	/// </summary>
	internal class Browser : WebBrowser
	{
		static Browser()
		{
			SetEmulationVersion(GetInternetExplorerMajorVersion());
		}

		public Browser(string url)
		{
			this.Url = new Uri(url);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			SetUserAgent("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)");
		}

		/// <summary>
		/// Changes the user agent for the embedded IE.
		/// </summary>
		/// <param name="ua"></param>
		private static void SetUserAgent(string ua)
		{
			UrlMkSetSessionOption(0x10000002, null, 0, 0);
			UrlMkSetSessionOption(0x10000001, ua, ua.Length, 0);
		}

		/// <summary>
		/// Sets the IE emulation version for the embedded browser.
		/// </summary>
		private static void SetEmulationVersion(int version)
		{
			try
			{
				string programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
				RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
				key.SetValue(programName, version * 1000, RegistryValueKind.DWord);

				key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_NINPUT_LEGACYMODE");
				key.SetValue(programName, 0, RegistryValueKind.DWord);

				// 64 bit
				if (IntPtr.Size == 8)
				{
					key = Registry.CurrentUser.CreateSubKey(@"Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
					key.SetValue(programName, version * 1000, RegistryValueKind.DWord);

					key = Registry.CurrentUser.CreateSubKey(@"Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_NINPUT_LEGACYMODE");
					key.SetValue(programName, 0, RegistryValueKind.DWord);
				}
			}
			catch { }
		}

		/// <summary>
		/// Determine the version if the IE that is installed.
		/// </summary>
		/// <returns></returns>
		private static int GetInternetExplorerMajorVersion()
		{
			int result = 10;
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer");
				if (key != null)
				{
					object value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

					if (value != null)
					{
						string version = value.ToString();
						int separator = version.IndexOf('.');
						if (separator != -1)
							int.TryParse(version.Substring(0, separator), out result);
					}
				}
			}
			catch { }

			return result;
		}

		#region UrlMkSetSessionOption

		[DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
		private static extern int UrlMkSetSessionOption(
			int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

		#endregion

	}
}
