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
using static Wisej.Web.Ext.MobileIntegration.DeviceResponse;

namespace Wisej.Web.Ext.MobileIntegration
{
	public sealed partial class Device
	{
		/// <summary>
		/// Provides methods for interacting with the device's NFC reader and writer.
		/// </summary>
		[ApiCategory("API")]
		public class NFC
		{
			/// <summary>
			/// Reads messages from NFC devices.
			/// </summary>
			/// <example>
			/// <code>
			/// try 
			///	{
			///		var json = Device.NFC.ReadNFC();
			///		var data = JSON.Parse(json);
			///
			///		AlertBox.Show(data.records);
			///	} 
			///	catch (DeviceException ex) 
			///	{
			///		AlertBox.Show(ex.Reason);
			///	}
			/// </code>
			/// </example>
			/// <exception cref="DeviceException">
			/// Occurs when the device is not able to read the data from the NFC-enabled device.
			/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
			/// </exception>
			public static string ReadNFC()
			{
				var result = PostModalMessage("nfc.read");
				if (result.Status != StatusCode.Success)
					ThrowDeviceException(result);

				return result.Value;
			}

			/// <summary>
			/// Writes the specified message to the NFC device.
			/// </summary>
			/// <param name="message">The message to write to the NFC device.</param>
			/// <example>
			/// <code>
			/// try
			///	{
			///		var response = Device.NFC.WriteNFC(this.textBoxWritable.Text);
			///	}
			///	catch (DeviceException ex)
			///	{
			///		AlertBox.Show(ex.Reason);
			///	}
			/// </code>
			/// </example>
			/// <exception cref="DeviceException">
			/// Occurs when the device is not able to write the data to the NFC-enabled device.
			/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
			/// </exception>
			public static string WriteNFC(string message)
			{
				var result = PostModalMessage("nfc.write", message);
				if (result.Status != StatusCode.Success)
					ThrowDeviceException(result);

				return result.Value;
			}
		}
	}
}
