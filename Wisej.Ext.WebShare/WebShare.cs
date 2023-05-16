///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wisej.Web;

namespace Wisej.Ext.WebShare
{
    /// <summary>
    /// Provides methods for sharing text, links, files, and other content to an arbitrary share target selected by the user.
    /// </summary>
	/// <remarks>
	/// Can only be used from https or localhost.
	/// See: https://developer.mozilla.org/en-US/docs/Web/API/Web_Share_API.
	/// </remarks>
    public static class WebShare
	{
		/// <summary>
		/// Returns whether the browser is capable of performing a share operation.
		/// </summary>
		/// <returns></returns>
		public static Task<dynamic> CanShare()
		{
			return Application.EvalAsync("navigator['share'] != null");
		}

        /// <summary>
        /// The <see cref="CanShareAsync"/> method of the Web Share API returns true if the equivalent call to <see cref="ShareAsync"/> would succeed.
        /// </summary>
        /// <param name="url">A string representing a URL to be shared.</param>
        /// <param name="text">A string representing text to be shared.</param>
        /// <param name="title">A string representing the title to be shared.</param>
        /// <param name="fileStreams">An array of files representing files to be shared.</param>
        /// <returns>The result from the client.</returns>
		public static Task<dynamic> CanShareAsync(string url = "", string text = "", string title = "", FileStream[] fileStreams=null)
		{
			return InternalShareOperationAsync("canShare", url, text, title, fileStreams);
		}

        /// <summary>
        /// The <see cref="ShareAsync"/> method of the Web Share API invokes the native sharing mechanism of the device to share data such as text, URLs, or files.
        /// </summary>
        /// <param name="url">A string representing a URL to be shared.</param>
        /// <param name="text">A string representing text to be shared.</param>
        /// <param name="title">A string representing the title to be shared.</param>
        /// <param name="fileStreams">An array of files representing files to be shared.</param>
        /// <returns>The result from the client.</returns>
		public static Task<dynamic> ShareAsync(string url="", string text="", string title="", FileStream[] fileStreams=null)
		{
			return InternalShareOperationAsync("share", url, text, title, fileStreams);
        }

		private static Task<dynamic> InternalShareOperationAsync(string operation, string url = "", string text = "", string title = "", FileStream[] fileStreams = null)
        {
			var files = new Dictionary<string, string>();
			if (fileStreams != null)
			{
				foreach (var file in fileStreams)
					files.Add(file.Name, GetFileStreamBase64(file));
			}

			return Application.EvalAsync($@"
				(async function () {{
					var config = { new { url, text, title, files }.ToJSON()};
					var base64Files = config.files;
					var files = [];
					var keys = Object.keys(base64Files);
					for (var i = 0; i < keys.length; i++) {{
						var key = keys[i];
						var base64 = base64Files[key];
						var blob = await fetch(base64);
						var file = new File([blob], key, {{ type: base64.match(/[^:]\w+\/[\w-+\d.]+(?=;|,)/)[0] }});

						files.push(file);						
					}};

				config.files = files;

				return {(operation == "share" ? "await" : "")} navigator.{operation}(config);
			}})()");
		}

		/// <summary>
		/// Gets a base64 representation of the given <see cref="FileStream"/>.
		/// </summary>
		/// <param name="fileStream"></param>
		/// <returns></returns>
		private static string GetFileStreamBase64(FileStream fileStream)
		{
			var mime = GetMimeTypeForFileExtension(fileStream.Name);
			using (var ms = new MemoryStream())
            {
				fileStream.CopyTo(ms);

				var base64 = Convert.ToBase64String(ms.ToArray());

				return $"data:{mime};base64,{base64}";
			}
		}

		/// <summary>
		/// Gets the mime type for a given file.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private static string GetMimeTypeForFileExtension(string file)
		{
			var DefaultContentType = "application/octet-stream";
			var provider = new FileExtensionContentTypeProvider();

			if (!provider.TryGetContentType(file, out string contentType))
				contentType = DefaultContentType;

			return contentType;
		}
	}
}
