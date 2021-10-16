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

using System;
using System.ComponentModel;

namespace Wisej.Web.Ext.MobileIntegration
{
	public sealed partial class Device
	{
		/// <summary>
		/// Provides functionality for interacting with the device's native NFC reader / writer.
		/// </summary>
		[ApiCategory("API")]
		public class NFC
		{
			/// <summary>
			/// Reads messages from NFC devices.
			/// </summary>
			public static string ReadNFC()
			{
				var result = PostModalMessage("nfc.read");
				if (result.ErrorCode != 0)
					throw new Exception(result.Value);

				return result.Value;
			}

			/// <summary>
			/// Writes the specified message to the NFC device.
			/// </summary>
			/// <param name="message">The message to write to the NFC device.</param>
			public static string WriteNFC(string message)
			{
				var result = PostModalMessage("nfc.write", message);
				var success = result.ErrorCode == 0;

				if (!success)
					throw new Exception(result.Value);

				return result.Value;
			}
		}
	}
}
