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
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Provides methods to communciate with Apple Push Notification Server (APNs) and Firebase Cloud Messenger (FCM).
	/// </summary>
	public class NotificationManager
	{
		/// <summary>
		/// The directory path of the Wisej.Notify executable file (exe).
		/// </summary>
		public static string TOOL_PATH;

		static NotificationManager()
		{
			TOOL_PATH = Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "Wisej.Notify");
		}

		/// <summary>
		/// Pushes a notification to Firebase Cloud Messenger (FCM).
		/// </summary>
		/// <param name="serverKey">The Firebase Cloud Messenger server auth Key.</param>
		/// <param name="payload">The Android Notification payload.</param>
		/// <returns></returns>
		public static NotificationResponse PushAndroid(string serverKey, object payload)
		{
			var data = new
			{
				payload,
				serverKey,
				os = "Android",
			};

			return ProcessNotificationRequest(data);
		}

		/// <summary>
		/// Pushes a notification to Apple Push Notification Server (APNs).
		/// </summary>
		/// <param name="bundleId"></param>
		/// <param name="deviceTokens"></param>
		/// <param name="certPath"></param>
		/// <param name="certPassword"></param>
		/// <param name="payload"></param>
		/// <param name="sandbox"></param>
		public static NotificationResponse PushiOS(string bundleId, string[] deviceTokens, string certPath, string certPassword, object payload, bool sandbox = false)
		{
			var data = new
			{
				sandbox,
				payload,
				bundleId,
				certPath,
				os = "iOS",
				deviceTokens,
				certPassword,
			};

			return ProcessNotificationRequest(data);
		}

		private static NotificationResponse ProcessNotificationRequest(object data)
		{
			var dataString = JSON.Stringify(data, false);
			var dataBytes = Encoding.UTF8.GetBytes(dataString);

			var info = new ProcessStartInfo
			{
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				Arguments = Convert.ToBase64String(dataBytes),
				FileName = Path.Combine(TOOL_PATH, "Wisej.Notify.exe"),
			};

			var process = Process.Start(info);
			process.WaitForExit();

			using (var reader = new StreamReader(process.StandardOutput.BaseStream))
			{
				var jsonString = reader.ReadToEnd();
				if (string.IsNullOrEmpty(jsonString))
					throw new InvalidOperationException("There was an error communicating with the Notification Manager.");

				var response = JSON.Parse(jsonString);
				return new NotificationResponse(response.Success, response.ReasonString, response.Timestamp);
			}
		}
	}
}
