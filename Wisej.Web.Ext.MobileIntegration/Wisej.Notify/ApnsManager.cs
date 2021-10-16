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
using System.Security.Cryptography.X509Certificates;

namespace Wisej.Notify
{
	/// <summary>
	/// Provides methods to assist in communicating with the Apple Push Notification Server (APNs).
	/// </summary>
	/// <remarks>
	/// Read: https://developer.apple.com/documentation/usernotifications/setting_up_a_remote_notification_server/establishing_a_token-based_connection_to_apns.
	/// </remarks>
	public sealed class ApnsManager
	{

		#region Properties

		private static HttpClient _client;
		private static readonly string _apnProdBaseUrl = "https://api.push.apple.com:2197";
		private static readonly string _apnDevBaseUrl = "https://api.sandbox.push.apple.com:2197";

		#endregion

		#region Methods

		/// <summary>
		/// Creates a new instance of <see cref="HttpClient"/> with the given apns certificate.
		/// </summary>
		/// <param name="certPath"></param>
		/// <param name="certPassword"></param>
		public static void CreateManager(string certPath, string certPassword)
		{
			if (string.IsNullOrWhiteSpace(certPath))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(certPath));

			var cert = new X509Certificate2(certPath, certPassword);
			if (cert == null) throw new ArgumentNullException(nameof(cert));

			var handler = new HttpClientHandler();
			handler.ClientCertificates.Add(cert);
			handler.ClientCertificateOptions = ClientCertificateOption.Manual;

			_client = new HttpClient(handler);
			if (_client == null) throw new ArgumentNullException(nameof(_client));
		}

		/// <summary>
		/// Sends the given message payload using APNs.
		/// </summary>
		/// <param name="certPath">The p12 certificate path.</param>
		/// <param name="deviceToken">The remote notification token of the device.</param>
		/// <param name="payload">The message payload.</param>
		/// <param name="sandbox">The APNs environment.</param>
		/// <param name="certPassword">The password to access the certificate file.</param>
		/// <returns></returns>
		public static string Send(string bundleId, string certPath, string deviceToken, object payload, bool sandbox = false, string certPassword = null)
		{
			var result = "";
			var url = sandbox ? _apnDevBaseUrl : _apnProdBaseUrl;
			var json = JsonConvert.SerializeObject(payload, Formatting.None);

			if (_client == null)
				CreateManager(certPath, certPassword);

			using (var request = new HttpRequestMessage(HttpMethod.Post, $"{url}/3/device/{deviceToken}"))
			{
				request.Version = new Version(2, 0);
				request.Content = new StringContent(json);

				request.Headers.Add("apns-priority", "10");
				request.Headers.Add("apns-expiration", "0");
				request.Headers.Add("apns-topic", bundleId);

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
