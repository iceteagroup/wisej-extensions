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

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Wisej.Web;
using System.ComponentModel;

namespace Wisej.Ext.WebShare
{
	/// <summary>
	/// Provides methods for sharing text, links, files, and other content to an arbitrary share target selected by the user.
	/// </summary>
	/// <remarks>
	/// Can only be used from https or localhost.
	/// See: <a href="https://developer.mozilla.org/en-US/docs/Web/API/Web_Share_API">Web Share API.</a>
	/// </remarks>
	/// <example>
	/// The following example checks whether the browser supports sharing and, if so, shares a link:
	/// <code><![CDATA[
	/// private async void buttonShare_Click(object sender, EventArgs e)
	/// {
	///     if (await WebShare.CanShare())
	///     {
	///         await WebShare.ShareAsync(
	///             url: "https://wisej.com",
	///             text: "Check out Wisej.NET!",
	///             title: "Wisej.NET");
	///     }
	///     else
	///     {
	///         AlertBox.Show("Sharing is not supported by this browser.");
	///     }
	/// }
	/// ]]></code>
	/// </example>
	[ApiCategory("WebShare")]
	public static class WebShare
	{
		/// <summary>
		/// Returns whether the browser is capable of performing a share operation.
		/// </summary>
		/// <remarks>
		/// This method only checks whether the <c>navigator.share</c> function exists in the browser.
		/// To verify that a specific set of data can be shared, use <see cref="CanShareAsync"/>.
		/// </remarks>
		/// <returns>A task that resolves to <c>true</c> when the browser supports the Web Share API; otherwise <c>false</c>.</returns>
		/// <example>
		/// The following example hides a share button when the browser does not support sharing:
		/// <code><![CDATA[
		/// protected override async void OnLoad(EventArgs e)
		/// {
		///     base.OnLoad(e);
		///
		///     bool supported = await WebShare.CanShare();
		///     this.buttonShare.Visible = supported;
		/// }
		/// ]]></code>
		/// </example>
		public static Task<dynamic> CanShare()
		{
			return Application.EvalAsync("navigator['share'] != null");
		}

		/// <summary>
		/// The <see cref="CanShareAsync"/> method of the Web Share API returns true if the equivalent call to <see cref="ShareAsync"/> would succeed.
		/// </summary>
		/// <remarks>
		/// Use this method to validate the data before calling <see cref="ShareAsync"/>, for example to check whether the
		/// browser allows sharing the given file types. The method returns <c>false</c> if the data cannot be shared.
		/// See: <a href="https://developer.mozilla.org/en-US/docs/Web/API/Navigator/canShare">Navigator.canShare().</a>
		/// </remarks>
		/// <param name="url">A string representing a URL to be shared.</param>
		/// <param name="text">A string representing text to be shared.</param>
		/// <param name="title">A string representing the title to be shared.</param>
		/// <param name="fileStreams">An array of files representing files to be shared.</param>
		/// <returns>The result from the client: <c>true</c> if the data can be shared; otherwise <c>false</c>.</returns>
		/// <example>
		/// The following example checks whether a PDF file can be shared before sharing it:
		/// <code><![CDATA[
		/// private async void buttonShareReport_Click(object sender, EventArgs e)
		/// {
		///     var path = Application.MapPath("Reports/report.pdf");
		///
		///     using (var check = File.OpenRead(path))
		///     {
		///         if (!await WebShare.CanShareAsync(fileStreams: new[] { check }))
		///         {
		///             AlertBox.Show("This file cannot be shared from this browser.");
		///             return;
		///         }
		///     }
		///
		///     using (var file = File.OpenRead(path))
		///     {
		///         await WebShare.ShareAsync(title: "Monthly Report", fileStreams: new[] { file });
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public static Task<dynamic> CanShareAsync(string url = "", string text = "", string title = "", FileStream[] fileStreams=null)
		{
			return InternalShareOperationAsync("canShare", url, text, title, fileStreams);
		}

		/// <summary>
		/// The <see cref="ShareAsync"/> method of the Web Share API invokes the native sharing mechanism of the device to share data such as text, URLs, or files.
		/// </summary>
		/// <remarks>
		/// The browser requires the share operation to be triggered by a user action, such as a button click.
		/// The returned task fails if the user cancels the share dialog or if the data cannot be shared.
		/// See: <a href="https://developer.mozilla.org/en-US/docs/Web/API/Navigator/share">Navigator.share().</a>
		/// </remarks>
		/// <param name="url">A string representing a URL to be shared.</param>
		/// <param name="text">A string representing text to be shared.</param>
		/// <param name="title">A string representing the title to be shared.</param>
		/// <param name="fileStreams">An array of files representing files to be shared.</param>
		/// <returns>The result from the client.</returns>
		/// <example>
		/// The following example shares a link and an image from a button click:
		/// <code><![CDATA[
		/// private async void buttonShare_Click(object sender, EventArgs e)
		/// {
		///     try
		///     {
		///         using (var image = File.OpenRead(Application.MapPath("Images/photo.png")))
		///         {
		///             await WebShare.ShareAsync(
		///                 url: "https://wisej.com",
		///                 text: "Look at this photo!",
		///                 title: "My Photo",
		///                 fileStreams: new[] { image });
		///         }
		///     }
		///     catch (Exception ex)
		///     {
		///         // The user canceled the share dialog or the data could not be shared.
		///         AlertBox.Show(ex.Message);
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
		/// <param name="fileStream">File stream to get the 64 representation of.</param>
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
		/// <param name="file">File to get the mime type for.</param>
		/// <returns></returns>
		private static string GetMimeTypeForFileExtension(string file)
		{
			return Wisej.Core.MimeTypes.GetMimeType(file);
		}
	}
}
