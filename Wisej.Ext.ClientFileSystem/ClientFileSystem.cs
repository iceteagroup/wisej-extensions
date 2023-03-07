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
using System.Threading.Tasks;
using Wisej.Web;

namespace Wisej.Ext.ClientFileSystem
{
	/// <summary>
	/// Implementation of <see href="https://developer.mozilla.org/en-US/docs/Web/API/File_System_Access_API">File System Access API</see>.
	/// Provides access to files and directories on client machines.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This extension enables developers to build powerful apps that interact the user's device via the device's 
	/// file system.
	/// </para>
	///	<para>
	///	It also allows the application to read or save changes directly to files and folders on the user's device 
	///	and it also offers the ability to open a directory and enumerate its contents.
	/// </para>
	/// </remarks>
	public static class ClientFileSystem
	{
		internal const string TARGET = "wisej.ext.ClientFileSystem";

		/// <summary>
		/// Opens a file picker that allows a user to select a file or multiple files.
		/// </summary>
		/// <param name="multiple">Set this to true to select multiple files; otherwise false.</param>
		/// <param name="excludeAcceptAllOption">True if there's a pattern to apply; otherwise false.</param>
		/// <param name="filter">
		///		Represents the MIME type and the file extension.
		///		Uses a similar syntax as Windows: "Description|Mime type|File Extension;FileExtension;...".
		///		Can specify multiple filters separates by a pipe.
		/// </param>
		/// <param name="callback">Callback method that receives a <see cref="File"/>[] object</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void ShowOpenFilePicker(bool multiple, bool excludeAcceptAllOption, string filter, Action<File[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowOpenFilePickerAsync(multiple, excludeAcceptAllOption, filter);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(null);
					else
						callback(t.Result);
				});
			});
		}

		/// <summary>
		/// Opens a client file picker that allows a user to select a file or multiple files asynchronously.
		/// </summary>
		/// <param name="multiple">Set this to true to select multiple files; otherwise false.</param>
		/// <param name="excludeAcceptAllOption">True if there's a pattern to apply; otherwise false.</param>
		/// <param name="filter">
		///		Represents the MIME type and the file extension.
		///		Uses a similar syntax as Windows: "Description|Mime type|File Extension;FileExtension;...".
		///		Can specify multiple filters separates by a pipe.
		/// </param>
		/// <returns>Returns a <see cref="File"/>[] that represents a handle for a file system entry.</returns>
		public static async Task<File[]> ShowOpenFilePickerAsync(bool multiple, bool excludeAcceptAllOption, string filter)
		{
			if (String.IsNullOrEmpty(filter))
				throw new ArgumentNullException(nameof(filter));

			var result = await Application.CallAsync(
				$"{TARGET}.showOpenFilePicker",
				multiple, 
				excludeAcceptAllOption, 
				filter
			);

			var config = (dynamic[])result;
			var array = new File[config.Length];
			for (var i = 0; i < config.Length; i++)
			{
				array[i] = new File(config[i]);
			}
			return array;
		}

		/// <summary>
		/// Opens a client file picker that allows a user to save a file.
		/// </summary>
		/// <param name="excludeAcceptAllOption">True if there's a pattern to apply; otherwise false.</param>
		/// <param name="filter">
		///		Represents the MIME type and the file extension.
		///		Uses a similar syntax as Windows: "Description|Mime type|File Extension;FileExtension;...".
		///		Can specify multiple filters separates by a pipe.
		/// </param>
		/// <param name="callback">Callback method that receives a <see cref="File"/> object.</param>
		/// <param name="suggestedName">A name to associate with the file.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void ShowSaveFilePicker(bool excludeAcceptAllOption, string filter, string suggestedName, Action<File> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowSaveFilePickerAsync(excludeAcceptAllOption, filter, suggestedName);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(null);
					else
						callback(t.Result);
				});
			});
		}

		/// <summary>
		/// Opens a client file picker that allows a user to save a file asynchronously.
		/// </summary>
		/// <param name="excludeAcceptAllOption">True if there's a pattern to apply; otherwise false.</param>
		/// <param name="filter">
		///		Represents the MIME type and the file extension.
		///		Uses a similar syntax as Windows: "Description|Mime type|File Extension;FileExtension;...".
		///		Can specify multiple filters separates by a pipe.
		/// </param>
		/// <param name="suggestedName">A name to associate with the file</param>
		/// <returns>Returns a <see cref="File"/> that represents a handle for a file system entry.</returns>
		public async static Task<File> ShowSaveFilePickerAsync(bool excludeAcceptAllOption, string filter, string suggestedName)
		{
			if (String.IsNullOrEmpty(filter))
				throw new ArgumentNullException(nameof(filter));

			var config = await Application.CallAsync(
				$"{TARGET}.showSaveFilePicker",
				excludeAcceptAllOption,
				filter,
				suggestedName
			);

			return new File(config);
		}

		/// <summary>
		/// Opens a client directory picker that allows the user to select a directory
		/// </summary>
		/// <param name="callback">Callback method that receives a <see cref="Directory"/> object</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void ShowDirectoryPicker(Action<Directory> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowDirectoryPickerAsync();

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(null);
					else
						callback(t.Result);
				});
			});
		}

		/// <summary>
		/// Opens a client directory picker that allows the user to select a directory asynchronously
		/// </summary>
		/// <returns>Returns a <see cref="Directory"/> that represents a handle for a file system directory</returns>
		public async static Task<Directory> ShowDirectoryPickerAsync()
		{
			var config = await Application.CallAsync(
				$"{TARGET}.showDirectoryPicker"
			);

			return new Directory(config);
		}
	}
}
