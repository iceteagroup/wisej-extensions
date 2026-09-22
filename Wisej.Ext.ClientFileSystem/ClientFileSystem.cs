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
	/// <para>
	/// It also allows the application to read or save changes directly to files and folders on the user's device
	/// and it also offers the ability to open a directory and enumerate its contents.
	/// </para>
	/// <para>
	/// Every entry point here opens a picker, because nothing in this API can reach the user's
	/// disk without the user choosing what to expose. The browser opens a picker only in response
	/// to a user gesture and only in a secure context (HTTPS, or localhost during development).
	/// The File System Access API is implemented in Chromium-based browsers; Firefox does not
	/// support it, and Safari supports only the origin-private file system, so the pickers below
	/// are unavailable there.
	/// </para>
	/// <para>
	/// The <see cref="File"/> and <see cref="Directory"/> objects returned here each hold a handle
	/// in the browser and implement <see cref="IDisposable"/>. Dispose them when they are no
	/// longer needed; a session that opens pickers repeatedly without disposing accumulates
	/// handles on the client.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// // Let the user pick a folder, then list the text files in it.
	/// using (var directory = await ClientFileSystem.ShowDirectoryPickerAsync())
	/// {
	///     var files = await directory.GetFilesAsync("*.txt");
	///     foreach (var file in files)
	///     {
	///         AlertBox.Show($"{file.Name} ({file.Size} bytes)");
	///         file.Dispose();
	///     }
	/// }
	/// ]]></code>
	/// </example>
	public static class ClientFileSystem
	{
		internal const string TARGET = "wisej.ext.ClientFileSystem";

		/// <summary>
		/// Opens a file picker that lets the user select one or more files, and invokes
		/// <paramref name="callback"/> with the result.
		/// </summary>
		/// <param name="multiple">
		/// <see langword="true"/> to let the user select more than one file; otherwise
		/// <see langword="false"/>.
		/// </param>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="File"/> objects,
		/// or with <see langword="null"/> if the user dismissed the picker or the call failed. Each
		/// <see cref="File"/> holds a handle in the browser and should be disposed when no longer
		/// needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The browser only opens a picker in response to a user
		/// gesture, so call it from a control event rather than during application startup. A
		/// <see langword="null"/> or empty <paramref name="filter"/> does not throw here: it faults
		/// the underlying operation, which reaches the callback as <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowOpenFilePicker(true, false, "Text files|text/plain|.txt;.log", files =>
		/// {
		///     if (files == null)
		///     {
		///         AlertBox.Show("No file was selected.");
		///         return;
		///     }
		///
		///     foreach (var file in files)
		///     {
		///         AlertBox.Show($"{file.Name} ({file.Size} bytes)");
		///         file.Dispose();
		///     }
		/// });
		/// ]]></code>
		/// </example>
		public static void ShowOpenFilePicker(bool multiple, bool excludeAcceptAllOption, string filter, Action<File[]> callback)
			=> ShowOpenFilePicker(multiple, excludeAcceptAllOption, filter, WellKnownFolder.None, callback);

		/// <summary>
		/// Opens a file picker in the specified folder that lets the user select one or more files,
		/// and invokes <paramref name="callback"/> with the result.
		/// </summary>
		/// <param name="multiple">
		/// <see langword="true"/> to let the user select more than one file; otherwise
		/// <see langword="false"/>.
		/// </param>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="File"/> objects,
		/// or with <see langword="null"/> if the user dismissed the picker or the call failed. Each
		/// <see cref="File"/> holds a handle in the browser and should be disposed when no longer
		/// needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The browser only opens a picker in response to a user
		/// gesture, so call it from a control event rather than during application startup. A
		/// <see langword="null"/> or empty <paramref name="filter"/> does not throw here: it faults
		/// the underlying operation, which reaches the callback as <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowOpenFilePicker(false, true, "Images|image/*|.png;.jpg", WellKnownFolder.Pictures, files =>
		/// {
		///     if (files == null || files.Length == 0)
		///         return;
		///
		///     var file = files[0];
		///     AlertBox.Show($"Selected {file.Name}.");
		///     file.Dispose();
		/// });
		/// ]]></code>
		/// </example>
		public static void ShowOpenFilePicker(bool multiple, bool excludeAcceptAllOption, string filter, WellKnownFolder startIn, Action<File[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowOpenFilePickerAsync(multiple, excludeAcceptAllOption, filter, startIn);

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
		/// Opens a file picker that lets the user select one or more files, asynchronously.
		/// </summary>
		/// <param name="multiple">
		/// <see langword="true"/> to let the user select more than one file; otherwise
		/// <see langword="false"/>.
		/// </param>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with the selected
		/// <see cref="File"/> objects. Each holds a handle in the browser and should be disposed when
		/// no longer needed.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// try
		/// {
		///     var files = await ClientFileSystem.ShowOpenFilePickerAsync(false, false, "Text files|text/plain|.txt");
		///
		///     using (var file = files[0])
		///     {
		///         var text = await file.ReadTextAsync();
		///         AlertBox.Show($"{file.Name} contains {text.Length} characters.");
		///     }
		/// }
		/// catch (Exception ex)
		/// {
		///     AlertBox.Show($"The picker was dismissed or failed: {ex.Message}");
		/// }
		/// ]]></code>
		/// </example>
		public static async Task<File[]> ShowOpenFilePickerAsync(bool multiple, bool excludeAcceptAllOption, string filter)
			=> await ShowOpenFilePickerAsync(multiple, excludeAcceptAllOption, filter, WellKnownFolder.None);

		/// <summary>
		/// Opens a file picker in the specified folder that lets the user select one or more files,
		/// asynchronously.
		/// </summary>
		/// <param name="multiple">
		/// <see langword="true"/> to let the user select more than one file; otherwise
		/// <see langword="false"/>.
		/// </param>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with the selected
		/// <see cref="File"/> objects. Each holds a handle in the browser and should be disposed when
		/// no longer needed.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var files = await ClientFileSystem.ShowOpenFilePickerAsync(
		///     true, false, "Documents|application/pdf|.pdf", WellKnownFolder.Documents);
		///
		/// foreach (var file in files)
		/// {
		///     var bytes = await file.ReadBytesAsync();
		///     AlertBox.Show($"{file.Name}: {bytes.Length} bytes read.");
		///     file.Dispose();
		/// }
		/// ]]></code>
		/// </example>
		public static async Task<File[]> ShowOpenFilePickerAsync(bool multiple, bool excludeAcceptAllOption, string filter, WellKnownFolder startIn)
		{
			if (String.IsNullOrEmpty(filter))
				throw new ArgumentNullException(nameof(filter));

			var result = await Application.CallAsync(
				$"{TARGET}.showOpenFilePicker",
				multiple,
				excludeAcceptAllOption,
				filter,
				startIn
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
		/// Opens a file picker that lets the user choose where to save a file, and invokes
		/// <paramref name="callback"/> with the resulting handle.
		/// </summary>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="suggestedName">
		/// The file name the picker proposes to the user. The user is free to change it.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="File"/>, or with
		/// <see langword="null"/> if the user dismissed the picker or the call failed. The
		/// <see cref="File"/> holds a handle in the browser and should be disposed when no longer
		/// needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately, and produces a handle rather than writing anything: use
		/// <see cref="File.WriteText(string, int, Action{bool})"/> or one of its siblings to fill the
		/// file. The browser only opens a picker in response to a user gesture, so call this from a
		/// control event rather than during application startup. A <see langword="null"/> or empty
		/// <paramref name="filter"/> does not throw here: it faults the underlying operation, which
		/// reaches the callback as <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowSaveFilePicker(false, "Text files|text/plain|.txt", "report.txt", file =>
		/// {
		///     if (file == null)
		///     {
		///         AlertBox.Show("The save dialog was dismissed.");
		///         return;
		///     }
		///
		///     file.WriteText($"Generated on {DateTime.Now}.", 0, success =>
		///     {
		///         AlertBox.Show(success ? $"Saved {file.Name}." : "The file could not be written.");
		///         file.Dispose();
		///     });
		/// });
		/// ]]></code>
		/// </example>
		public static void ShowSaveFilePicker(bool excludeAcceptAllOption, string filter, string suggestedName, Action<File> callback)
			=> ShowSaveFilePicker(excludeAcceptAllOption, filter, suggestedName, WellKnownFolder.None, callback);

		/// <summary>
		/// Opens a file picker in the specified folder that lets the user choose where to save a
		/// file, and invokes <paramref name="callback"/> with the resulting handle.
		/// </summary>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="suggestedName">
		/// The file name the picker proposes to the user. The user is free to change it.
		/// </param>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="File"/>, or with
		/// <see langword="null"/> if the user dismissed the picker or the call failed. The
		/// <see cref="File"/> holds a handle in the browser and should be disposed when no longer
		/// needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately, and produces a handle rather than writing anything: use
		/// <see cref="File.WriteText(string, int, Action{bool})"/> or one of its siblings to fill the
		/// file. The browser only opens a picker in response to a user gesture, so call this from a
		/// control event rather than during application startup. A <see langword="null"/> or empty
		/// <paramref name="filter"/> does not throw here: it faults the underlying operation, which
		/// reaches the callback as <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowSaveFilePicker(
		///     true, "CSV|text/csv|.csv", "export.csv", WellKnownFolder.Downloads, file =>
		///     {
		///         if (file == null)
		///             return;
		///
		///         file.WriteText(BuildCsv(), 0, success =>
		///         {
		///             AlertBox.Show(success ? $"Saved {file.Name}." : "The file could not be written.");
		///             file.Dispose();
		///         });
		///     });
		/// ]]></code>
		/// </example>
		public static void ShowSaveFilePicker(bool excludeAcceptAllOption, string filter, string suggestedName, WellKnownFolder startIn, Action<File> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowSaveFilePickerAsync(excludeAcceptAllOption, filter, suggestedName, startIn);

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
		/// Opens a file picker that lets the user choose where to save a file, asynchronously.
		/// </summary>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="suggestedName">
		/// The file name the picker proposes to the user. The user is free to change it.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with a <see cref="File"/> handle
		/// for the chosen location. The file is created empty; write to it with
		/// <see cref="File.WriteTextAsync(string, int)"/> or one of its siblings. The handle should
		/// be disposed when no longer needed.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// using (var file = await ClientFileSystem.ShowSaveFilePickerAsync(
		///     false, "Text files|text/plain|.txt", "notes.txt"))
		/// {
		///     await file.WriteTextAsync("Saved from Wisej.NET.", 0);
		///     AlertBox.Show($"Saved {file.Name}.");
		/// }
		/// ]]></code>
		/// </example>
		public static Task<File> ShowSaveFilePickerAsync(bool excludeAcceptAllOption, string filter, string suggestedName)
			=> ShowSaveFilePickerAsync(excludeAcceptAllOption, filter, suggestedName, WellKnownFolder.None);

		/// <summary>
		/// Opens a file picker in the specified folder that lets the user choose where to save a
		/// file, asynchronously.
		/// </summary>
		/// <param name="excludeAcceptAllOption">
		/// <see langword="true"/> to hide the picker's "All files" entry, so the user can only choose
		/// the types described by <paramref name="filter"/>; otherwise <see langword="false"/>.
		/// </param>
		/// <param name="filter">
		/// The file types the picker offers, in a Windows-like syntax:
		/// "Description|Mime type|Extension;Extension;...". Append another pipe and three more
		/// segments for each additional filter. The string is split on '|' and read in groups of
		/// three, so a value with fewer than three segments is ignored and no type restriction is
		/// applied.
		/// </param>
		/// <param name="suggestedName">
		/// The file name the picker proposes to the user. The user is free to change it.
		/// </param>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with a <see cref="File"/> handle
		/// for the chosen location. The file is created empty; write to it with
		/// <see cref="File.WriteTextAsync(string, int)"/> or one of its siblings. The handle should
		/// be disposed when no longer needed.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// using (var file = await ClientFileSystem.ShowSaveFilePickerAsync(
		///     false, "CSV|text/csv|.csv", "export.csv", WellKnownFolder.Documents))
		/// {
		///     await file.WriteTextAsync(BuildCsv(), 0);
		///     AlertBox.Show($"Saved {file.Name}.");
		/// }
		/// ]]></code>
		/// </example>
		public async static Task<File> ShowSaveFilePickerAsync(bool excludeAcceptAllOption, string filter, string suggestedName, WellKnownFolder startIn)
		{
			if (String.IsNullOrEmpty(filter))
				throw new ArgumentNullException(nameof(filter));

			var config = await Application.CallAsync(
				$"{TARGET}.showSaveFilePicker",
				excludeAcceptAllOption,
				filter,
				suggestedName,
				startIn
			);

			return new File(config);
		}

		/// <summary>
		/// Opens a directory picker that lets the user select a folder, and invokes
		/// <paramref name="callback"/> with the result.
		/// </summary>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="Directory"/>, or
		/// with <see langword="null"/> if the user dismissed the picker or the call failed. The
		/// <see cref="Directory"/> holds a handle in the browser and should be disposed when no
		/// longer needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The browser only opens a picker in response to a user
		/// gesture, so call it from a control event rather than during application startup. Selecting
		/// a folder grants read access to it; writing to anything inside it additionally requires
		/// <see cref="Permission.ReadWrite"/>, which
		/// <see cref="Directory.RequestPermissionAsync(Permission)"/> asks for.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowDirectoryPicker(directory =>
		/// {
		///     if (directory == null)
		///     {
		///         AlertBox.Show("No folder was selected.");
		///         return;
		///     }
		///
		///     // Keep the handle for the rest of the session, and dispose it when done.
		///     this._directory = directory;
		///     AlertBox.Show($"Selected {directory.Name}.");
		/// });
		/// ]]></code>
		/// </example>
		public static void ShowDirectoryPicker(Action<Directory> callback)
			=> ShowDirectoryPicker(WellKnownFolder.None, callback);

		/// <summary>
		/// Opens a directory picker in the specified folder that lets the user select a folder, and
		/// invokes <paramref name="callback"/> with the result.
		/// </summary>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with the selected <see cref="Directory"/>, or
		/// with <see langword="null"/> if the user dismissed the picker or the call failed. The
		/// <see cref="Directory"/> holds a handle in the browser and should be disposed when no
		/// longer needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The browser only opens a picker in response to a user
		/// gesture, so call it from a control event rather than during application startup. Selecting
		/// a folder grants read access to it; writing to anything inside it additionally requires
		/// <see cref="Permission.ReadWrite"/>, which
		/// <see cref="Directory.RequestPermissionAsync(Permission)"/> asks for.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ClientFileSystem.ShowDirectoryPicker(WellKnownFolder.Documents, directory =>
		/// {
		///     if (directory == null)
		///         return;
		///
		///     this._directory = directory;
		///     AlertBox.Show($"Selected {directory.Name}.");
		/// });
		/// ]]></code>
		/// </example>
		public static void ShowDirectoryPicker(WellKnownFolder startIn, Action<Directory> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ShowDirectoryPickerAsync(startIn);

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
		/// Opens a directory picker that lets the user select a folder, asynchronously.
		/// </summary>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with a <see cref="Directory"/>
		/// handle for the selected folder. The handle should be disposed when no longer needed.
		/// </returns>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// using (var directory = await ClientFileSystem.ShowDirectoryPickerAsync())
		/// {
		///     var files = await directory.GetFilesAsync("*");
		///     AlertBox.Show($"{directory.Name} contains {files.Length} file(s).");
		///
		///     foreach (var file in files)
		///         file.Dispose();
		/// }
		/// ]]></code>
		/// </example>
		public static Task<Directory> ShowDirectoryPickerAsync()
			=> ShowDirectoryPickerAsync(WellKnownFolder.None);

		/// <summary>
		/// Opens a directory picker in the specified folder that lets the user select a folder,
		/// asynchronously.
		/// </summary>
		/// <param name="startIn">
		/// One of the <see cref="WellKnownFolder"/> values naming the folder the picker opens in.
		/// <see cref="WellKnownFolder.None"/> leaves the choice to the browser, which normally
		/// reopens the folder the user last visited.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with a <see cref="Directory"/>
		/// handle for the selected folder. The handle should be disposed when no longer needed.
		/// </returns>
		/// <remarks>
		/// The browser only opens a picker in response to a user gesture, so call this from a control
		/// event rather than during application startup. The task faults if the user dismisses the
		/// picker, so wrap the call in a try/catch rather than testing the result for
		/// <see langword="null"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// using (var directory = await ClientFileSystem.ShowDirectoryPickerAsync(WellKnownFolder.Pictures))
		/// {
		///     var state = await directory.RequestPermissionAsync(Permission.ReadWrite);
		///     if (state != PermissionState.Granted)
		///     {
		///         AlertBox.Show($"Read-write access was not granted ({state}).");
		///         return;
		///     }
		///
		///     AlertBox.Show($"Ready to write into {directory.Name}.");
		/// }
		/// ]]></code>
		/// </example>
		public async static Task<Directory> ShowDirectoryPickerAsync(WellKnownFolder startIn)
		{
			var config = await Application.CallAsync(
				$"{TARGET}.showDirectoryPicker",
				startIn
			);

			return new Directory(config);
		}
	}
}
