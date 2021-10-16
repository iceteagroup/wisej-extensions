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
using System.Text;

namespace Wisej.Notify
{
	/// <summary>
	/// Processes a notification request from the arguments.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				ProcessResponse("Invalid number of arguments.");
				return;
			}
				
			try
			{
				var dataBytes = Convert.FromBase64String(args[0]);
				var dataString = Encoding.UTF8.GetString(dataBytes);
				var data = (dynamic)JsonConvert.DeserializeObject(dataString);

				var os = data.os.ToObject<string>();
				var payload = data.payload.ToObject<dynamic>();

				switch (os)
				{
					case "iOS":

						var sandbox = data.sandbox.ToObject<bool>();
						var certPath = data.certPath.ToObject<string>();
						var bundleId = data.bundleId.ToObject<string>();
						var certPassword = data.certPassword.ToObject<string>();
						var deviceTokens = data.deviceTokens.ToObject<string[]>();

						foreach (var token in deviceTokens)
						{
							var iosResult = ApnsManager.Send(bundleId, certPath, token, payload, sandbox, certPassword);
							ProcessResponse(iosResult.ToString());
						}
						ApnsManager.Dispose();
						break;

					case "Android":

						var serverKey = data.serverKey.ToObject<string>();

						var androidResult = FcmManager.Send(serverKey, payload);
						ProcessResponse(androidResult.ToString());
						break;
				}
			}
			catch (Exception ex)
			{
				ProcessResponse(ex.ToString());
			}
		}

		/// <summary>
		/// Writes the given response to the output.
		/// </summary>
		/// <param name="response"></param>
		private static void ProcessResponse(string response)
		{
			Console.WriteLine(JsonConvert.SerializeObject(new
			{
				Success = response.Equals("OK"),
				Timestamp = DateTime.Now,
				ReasonString = response
			}));
		}
	}
}
