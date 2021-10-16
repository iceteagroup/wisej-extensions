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

using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Wisej.Notify
{
	/// <summary>
	/// Provides methods to assist in communicating with the Firebase Cloud Messaging Server (FCM).
	/// </summary>
	/// <remarks>
	/// Read: https://firebase.google.com/docs/cloud-messaging.
	/// </remarks>
	public class FcmManager
	{

		#region Properties

		private static HttpClient _client;
		private static readonly string _fcmServerUrl = "https://fcm.googleapis.com/fcm/send";

		#endregion

		#region Methods

		/// <summary>
		/// Creates a new instance of <see cref="HttpClient"/>.
		/// </summary>
		public static void CreateManager()
		{
			var handler = new HttpClientHandler();

			_client = new HttpClient(handler);
			if (_client == null) throw new ArgumentNullException(nameof(_client));
		}

		/// <summary>
		/// Sends the given message payload using FCM.
		/// </summary>
		/// <param name="serverKey">The FCM server auth key.</param>
		/// <param name="payload">The Android message payload.</param>
		/// <returns></returns>
		public static string Send(string serverKey, object payload)
		{
			var result = "";
			var json = JsonConvert.SerializeObject(payload, Formatting.None);

			if (_client == null)
				CreateManager();

			using (var request = new HttpRequestMessage(HttpMethod.Post, _fcmServerUrl))
			{
				request.Content = new StringContent(json);

				request.Headers.TryAddWithoutValidation("Authorization", $"key={serverKey}");

				using var response = _client.SendAsync(request).Result;
				result = response.ReasonPhrase;
			}

			return result;
		}

		/// <summary>
		/// Disposes of the <see cref="HttpClient"/>.
		/// </summary>
		public static void Dispose()
		{
			_client.Dispose();
		}

		#endregion

	}
}
