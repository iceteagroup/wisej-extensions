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
using static Wisej.Web.Ext.MobileIntegration.DeviceResponse;

namespace Wisej.Web.Ext.MobileIntegration
{
	public sealed partial class Device
	{
		/// <summary>
		/// Provides methods for interacting with the device's sensors.
		/// </summary>
		[ApiCategory("API")]
		public class Sensors
		{

			#region Methods

			/// <summary>
			/// Starts accelerometer updates.
			/// </summary>
			/// <exception cref="DeviceException">
			/// Occurs when the device cannot start the accelerometer.
			/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
			/// </exception>
			public static void StartAccelerometer()
			{
				var result = PostModalMessage("accelerometer.start");
				if (result.Status != StatusCode.Success)
					ThrowDeviceException(result);
			}

			/// <summary>
			/// Stops accelerometer updates.
			/// </summary>
			public static void StopAccelerometer()
			{
				PostMessage("accelerometer.stop");
			}

			/// <summary>
			/// Starts gyroscope updates.
			/// </summary>
			/// <exception cref="DeviceException">
			/// Occurs when the device cannot start the gyroscope.
			/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
			/// </exception>
			public static void StartGyro()
			{
				var result = PostModalMessage("gyroscope.start");
				if (result.Status != StatusCode.Success)
					ThrowDeviceException(result);
			}

			/// <summary>
			/// Stops gyroscope updates.
			/// </summary>
			public static void StopGyro()
			{
				PostMessage("gyroscope.stop");
			}

			/// <summary>
			/// Starts magnetometer updates.
			/// </summary>
			/// <exception cref="DeviceException">
			/// Occurs when the device cannot start the magnetometer.
			/// See <see cref="DeviceException.ErrorCode"/> and <see cref="DeviceException.Reason"/>.
			/// </exception>
			public static void StartMagnetometer()
			{
				var result = PostModalMessage("magnetometer.start");
				if (result.Status != StatusCode.Success)
					ThrowDeviceException(result);
			}

			/// <summary>
			/// Stops magnetometer updates.
			/// </summary>
			public static void StopMagnetometer()
			{
				PostMessage("magnetometer.stop");
			}

			#endregion

		}
	}
}
