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

			#region Events

			/// <summary>
			/// Fired when there's an update from the device's accelerometers.
			/// </summary>
			public event DeviceEventHandler AccelerometerUpdate
			{
				add { this._accelerometerUpdate += value; }
				remove { this._accelerometerUpdate -= value; }
			}
			private event DeviceEventHandler _accelerometerUpdate;

			/// <summary>
			/// Fired when there's an update from the device's magnetometer.
			/// </summary>
			public event DeviceEventHandler MagnetometerUpdate
			{
				add { this._magnetometerUpdate += value; }
				remove { this._magnetometerUpdate -= value; }
			}
			private event DeviceEventHandler _magnetometerUpdate;

			#endregion

			#region Methods

			/// <summary>
			/// Starts accelerometer updates.
			/// </summary>
			/// <returns>When true, the device enabled accelerometer updates.</returns>
			public static bool StartAccelerometer()
			{
				var result = Device.PostModalMessage("accelerometer.start");
				return result.ErrorCode == 0;
			}

			/// <summary>
			/// Stops accelerometer updates.
			/// </summary>
			public static void StopAccelerometer()
			{
				Device.PostMessage("accelerometer.stop");
			}

			/// <summary>
			/// Starts gyroscope updates.
			/// </summary>
			/// <returns>When true, the device enabled gyroscope updates.</returns>
			public static bool StartGyro()
			{
				var result = Device.PostModalMessage("gyroscope.start");
				return result.ErrorCode == 0;
			}

			/// <summary>
			/// Stops gyroscope updates.
			/// </summary>
			public static void StopGyro()
			{
				Device.PostMessage("gyroscope.stop");
			}

			/// <summary>
			/// Starts magnetometer updates.
			/// </summary>
			/// <returns>When true, the device enabled magnetometer updates.</returns>
			public static bool StartMagnetometer()
			{
				var result = Device.PostModalMessage("magnetometer.start");
				return result.ErrorCode == 0;
			}

			/// <summary>
			/// Stops magnetometer updates.
			/// </summary>
			public static void StopMagnetometer()
			{
				Device.PostMessage("magnetometer.stop");
			}

			#endregion

		}
	}
}
