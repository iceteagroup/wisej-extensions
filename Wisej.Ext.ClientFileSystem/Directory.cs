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
	/// Represents a Directory of a <see cref="ClientFileSystem"/>.
	/// </summary>
	public class Directory : IDisposable
	{
		/// <summary>
		/// Destroys an instance of <see cref="Directory"/>.
		/// </summary>
		~Directory()
		{
			Dispose();
		}

		/// <summary>
		/// Creates a new instance of <see cref="Directory"/>.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		public Directory(dynamic config)
		{
			if (config == null)
				throw new ArgumentNullException(nameof(config));

			this.Hash = config.hash;
			this.Name = config.name;
		}

		/// <summary>
		/// Returns the file system directory's hash.
		/// </summary>
		internal string Hash
		{
			get;
			private set;
		}

        /// <summary>
        /// Returns the file system directory's name.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> containing the directory's name, without any path information.
        /// </value>
        /// <remarks>
        /// The value reflects the name of the underlying directory handle picked by the user
        /// in the browser. The browser never exposes the full path of a directory to the
        /// application, so only the last segment of the path is available. The name is read
        /// once when the <see cref="Directory"/> is created and is not updated if the directory
        /// is renamed afterwards on the client.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// ClientFileSystem.ShowDirectoryPicker((directory) =>
        /// {
        ///     if (directory != null)
        ///         AlertBox.Show($"Selected folder: {directory.Name}");
        /// });
        /// ]]></code>
        /// </example>
        public string Name
		{
			get;
			private set;
		}

        /// <summary>
        /// Begins retrieving the files in the current <see cref="Directory"/> that match the
        /// specified pattern and invokes <paramref name="callback"/> when the operation completes.
        /// </summary>
        /// <param name="pattern">
        /// A wildcard pattern matched against each file name, where '*' matches any sequence of
        /// characters and '?' matches any single character. The pattern is matched as an unanchored,
        /// case-sensitive expression, so "*.txt" also matches "report.txt.bak" and does not match
        /// "REPORT.TXT". The pattern applies to the file name only, not to the file's MIME type.
        /// </param>
        /// <param name="callback">
        /// A method invoked on the application context with the matching <see cref="File"/> objects,
        /// or with <see langword="null"/> if the operation failed — for example when read permission
        /// on the directory has not been granted or has been revoked, or when
        /// <paramref name="pattern"/> is <see langword="null"/>. Always test the argument before
        /// enumerating it. Each <see cref="File"/> holds a handle in the browser and should be
        /// disposed when no longer needed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method returns immediately. Subdirectories are not included; use
        /// <see cref="GetDirectories(string, Action{Directory[]})"/> for those. To await the
        /// result instead, use <see cref="GetFilesAsync(string)"/>, which propagates the failure
        /// as an exception rather than as a <see langword="null"/> array.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// directory.GetFiles("*.txt", files =>
        /// {
        ///     if (files == null)
        ///     {
        ///         AlertBox.Show("Could not read the folder.");
        ///         return;
        ///     }
        ///
        ///     foreach (var file in files)
        ///     {
        ///         AlertBox.Show(file.Name);
        ///         file.Dispose();
        ///     }
        /// });
        /// ]]></code>
        /// </example>
        public void GetFiles(string pattern, Action<File[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = GetFilesAsync(pattern);

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
        /// Begins retrieving the subdirectories of the current
        /// <see cref="Wisej.Ext.ClientFileSystem.Directory"/> that match the specified pattern and
        /// invokes <paramref name="callback"/> when the operation completes.
        /// </summary>
        /// <param name="pattern">
        /// A wildcard pattern matched against each subdirectory name, where '*' matches any sequence
        /// of characters and '?' matches any single character. The pattern is matched as an unanchored,
        /// case-sensitive expression, so "src*" also matches "mysrcfolder" and does not match "SRC".
        /// </param>
        /// <param name="callback">
        /// A method invoked on the application context with the matching
        /// <see cref="Wisej.Ext.ClientFileSystem.Directory"/> objects, or with <see langword="null"/>
        /// if the operation failed — for example when read permission on the directory has not been
        /// granted or has been revoked, or when <paramref name="pattern"/> is <see langword="null"/>.
        /// Always test the argument before enumerating it. Each
        /// <see cref="Wisej.Ext.ClientFileSystem.Directory"/> holds a handle in the browser and should
        /// be disposed when no longer needed.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method returns immediately. Only immediate subdirectories are returned: the search does
        /// not recurse, and files are excluded — use
        /// <see cref="GetFiles(string, Action{File[]})"/> for those. To await the result instead, use
        /// <see cref="GetDirectoriesAsync(string)"/>, which propagates the failure as an exception
        /// rather than as a <see langword="null"/> array.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// directory.GetDirectories("*", directories =>
        /// {
        ///     if (directories == null)
        ///     {
        ///         AlertBox.Show("Could not read the folder.");
        ///         return;
        ///     }
        ///
        ///     foreach (var dir in directories)
        ///     {
        ///         AlertBox.Show($"Found directory: {dir.Name}");
        ///         dir.Dispose();
        ///     }
        /// });
        /// ]]></code>
        /// </example>
        public void GetDirectories(string pattern, Action<Directory[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = GetDirectoriesAsync(pattern);

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
        /// Returns the file with the specified name from the <see cref="Directory"/>, optionally
        /// creating it when it does not exist.
        /// </summary>
        /// <param name="name">
        /// The name of the file to open or create, relative to this directory. Path separators are
        /// not allowed; the browser rejects a name containing '/' or '\'. The argument is not
        /// validated on the server and is passed to the browser as given.
        /// </param>
        /// <param name="create">
        /// <see langword="true"/> to create the file if it does not exist; otherwise
        /// <see langword="false"/>. Creating a file requires that
        /// <see cref="Permission.ReadWrite"/> has been granted on this directory — see
        /// <see cref="RequestPermissionAsync(Permission)"/>.
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> that completes with the <see cref="File"/>.
        /// The returned object holds a handle in the browser and should be disposed when no longer
        /// needed.
        /// </returns>
        /// <remarks>
        /// The task faults if the file does not exist and <paramref name="create"/> is
        /// <see langword="false"/>, if the required permission has not been granted, or if
        /// <paramref name="name"/> is not a valid file name.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// using (var file = await directory.GetFileAsync("example.txt", true))
        /// {
        ///     AlertBox.Show($"File {file.Name} is ready for use.");
        /// }
        /// ]]></code>
        /// </example>
        public async Task<File> GetFileAsync(string name, bool create = false){

			var result = await CallAsync("getFile", name, create);
			return new File(result);
		}

        /// <summary>
        /// Returns the files in the current <see cref="Directory"/> that match the specified pattern,
        /// asynchronously.
        /// </summary>
        /// <param name="pattern">
        /// A wildcard pattern matched against each file name, where '*' matches any sequence of
        /// characters and '?' matches any single character. The pattern is matched as an unanchored,
        /// case-sensitive expression, so "*.txt" also matches "report.txt.bak" and does not match
        /// "REPORT.TXT". The pattern applies to the file name only, not to the file's MIME type.
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> that completes with an array of
        /// <see cref="File"/> objects, or an empty array if no file matches. Each
        /// <see cref="File"/> holds a handle in the browser and should be disposed when no longer
        /// needed.
        /// </returns>
        /// <example><![CDATA[
        /// var files = await directory.GetFilesAsync("*.txt");
        /// foreach (var file in files)
        /// {
        ///     AlertBox.Show(file.Name);
        ///     file.Dispose();
        /// }
        /// ]]></example>
        public async Task<File[]> GetFilesAsync(string pattern)
		{
			var result = await CallAsync("getFiles", pattern);

			var config = (dynamic[])result;
			var array = new File[config.Length];
			for (var i = 0; i < config.Length; i++)
			{
				array[i] = new File(config[i]);
			}
			return array;
		}

        /// <summary>
        /// Returns the subdirectories of the current <see cref="Directory"/> that match the specified
        /// pattern, asynchronously.
        /// </summary>
        /// <param name="pattern">
        /// A wildcard pattern matched against each subdirectory name, where '*' matches any sequence
        /// of characters and '?' matches any single character. The pattern is matched as an unanchored,
        /// case-sensitive expression, so "src*" also matches "mysrcfolder" and does not match "SRC".
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> that completes with an array of
        /// <see cref="Directory"/> objects, or an empty array if no subdirectory matches. Each
        /// <see cref="Directory"/> holds a handle in the browser and should be disposed when no longer
        /// needed.
        /// </returns>
        /// <remarks>
        /// Only immediate subdirectories are returned: the search does not recurse, and files are
        /// excluded — use <see cref="GetFilesAsync(string)"/> for those. The task faults if read
        /// permission on the directory has not been granted or has been revoked, or if
        /// <paramref name="pattern"/> is <see langword="null"/>. Unlike
        /// <see cref="GetDirectories(string, Action{Directory[]})"/>, which reports failure by passing
        /// <see langword="null"/> to its callback, this overload surfaces the error as an exception.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var directories = await directory.GetDirectoriesAsync("*");
        /// foreach (var dir in directories)
        /// {
        ///     AlertBox.Show(dir.Name);
        ///     dir.Dispose();
        /// }
        /// ]]></code>
        /// </example>
        public async Task<Directory[]> GetDirectoriesAsync(string pattern)
		{
			var result = await CallAsync("getDirectories", pattern);

			var config = (dynamic[])result;
			var array = new Directory[config.Length];
			for (var i = 0; i < config.Length; i++)
			{
				array[i] = new Directory(config[i]);
			}
			return array;
		}

        /// <summary>
        /// Requests read or read-write permission on the <see cref="Directory"/>, asynchronously.
        /// </summary>
        /// <param name="mode">
        /// One of the <see cref="Wisej.Ext.ClientFileSystem.Permission"/> values indicating the level
        /// of access to request.
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> that completes with one of the
        /// <see cref="Wisej.Ext.ClientFileSystem.PermissionState"/> values:
        /// <see cref="PermissionState.Granted"/> if access is allowed,
        /// <see cref="PermissionState.Denied"/> if the user refused, or
        /// <see cref="PermissionState.Prompt"/> if the browser has not decided and will ask when the
        /// directory is next accessed.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// The browser returned a permission state that is not one of the recognized values.
        /// </exception>
        /// <remarks>
        /// Treat <see cref="PermissionState.Prompt"/> as "not yet granted" rather than as a refusal.
        /// Requesting a permission that has already been granted returns
        /// <see cref="PermissionState.Granted"/> without prompting the user again. The browser shows
        /// a prompt only in response to a user gesture, so call this from a control event rather than
        /// during application startup.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var state = await directory.RequestPermissionAsync(Permission.ReadWrite);
        /// switch (state)
        /// {
        ///     case PermissionState.Granted:
        ///         AlertBox.Show("Permission granted.");
        ///         break;
        ///
        ///     case PermissionState.Prompt:
        ///         AlertBox.Show("The browser will ask for permission on the next access.");
        ///         break;
        ///
        ///     default:
        ///         AlertBox.Show("Permission denied.");
        ///         break;
        /// }
        /// ]]></code>
        /// </example>
        public async Task<PermissionState> RequestPermissionAsync(Permission mode)
		{
			var result = await CallAsync("requestPermission", mode.ToString().ToLower());

			var state = (string)result;
			switch (state)
			{
				case "granted":
					return PermissionState.Granted;
				case "denied":
					return PermissionState.Denied;
				case "prompt":
					return PermissionState.Prompt;
			}

			throw new NotSupportedException($"State {state} is not supported.");
		}

        /// <summary>
        /// Begins removing a file or subdirectory from this <see cref="Directory"/> and invokes
        /// <paramref name="callback"/> when the operation completes.
        /// </summary>
        /// <param name="name">
        /// The name of the file or subdirectory to remove, relative to this directory. The argument is
        /// not validated on the server and is passed to the browser as given.
        /// </param>
        /// <param name="recursive">
        /// <see langword="true"/> to remove a subdirectory together with everything it contains;
        /// <see langword="false"/> to remove only a file or an empty subdirectory. Removing a
        /// non-empty subdirectory with <see langword="false"/> fails.
        /// </param>
        /// <param name="callback">
        /// An optional method invoked on the application context with <see langword="true"/> if the
        /// removal completed, or <see langword="false"/> if it failed — for example when the entry does
        /// not exist, when read-write permission has not been granted, or when the subdirectory is not
        /// empty and <paramref name="recursive"/> is <see langword="false"/>. Pass
        /// <see langword="null"/> to ignore the result.
        /// </param>
        /// <remarks>
        /// This method returns immediately. The deletion is permanent: the entry is not moved to the
        /// recycle bin or trash and cannot be recovered from the application. Removing an entry
        /// requires that <see cref="Permission.ReadWrite"/> has been granted on this directory — see
        /// <see cref="RequestPermissionAsync(Permission)"/>. The callback reports only success or
        /// failure, with no indication of the cause; use <see cref="RemoveAsync(string, bool)"/> to
        /// observe the underlying exception.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// directory.Remove("example.txt", false, success =>
        /// {
        ///     AlertBox.Show(success ? "File removed." : "Failed to remove the file.");
        /// });
        /// ]]></code>
        /// </example>
        public void Remove(string name, bool recursive, Action<bool> callback)
		{
			var context = Application.Current;
			var task = RemoveAsync(name, recursive);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (callback != null)
					{
						if (t.IsFaulted)
							callback(false);
						else
							callback(true);
					}
				});
			});
		}

        /// <summary>
        /// Removes a file or subdirectory from this <see cref="Directory"/>, asynchronously.
        /// </summary>
        /// <param name="name">
        /// The name of the file or subdirectory to remove. This is a single entry name, not a path:
        /// the browser rejects a name containing a path separator. To remove an entry nested deeper,
        /// obtain a handle on its parent with <see cref="GetDirectoriesAsync(string)"/> and call this
        /// method on that <see cref="Directory"/>. The argument is not validated on the server and is
        /// passed to the browser as given.
        /// </param>
        /// <param name="recursive">
        /// <see langword="true"/> to remove a subdirectory together with everything it contains;
        /// <see langword="false"/> to remove only a file or an empty subdirectory.
        /// </param>
        /// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// The deletion is permanent: the entry is not moved to the recycle bin or trash and cannot be
        /// recovered from the application. The task faults if the entry does not exist, if
        /// <see cref="Permission.ReadWrite"/> has not been granted on this directory — see
        /// <see cref="RequestPermissionAsync(Permission)"/> — or if the subdirectory is not empty and
        /// <paramref name="recursive"/> is <see langword="false"/>. Unlike
        /// <see cref="Remove(string, bool, Action{bool})"/>, which reports only a boolean, this
        /// overload surfaces the cause of the failure.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// await directory.RemoveAsync("exampleDir", true);
        /// ]]></code>
        /// </example>
        public async Task RemoveAsync(string name, bool recursive)
		{
			await CallAsync("remove", name, recursive);
		}

		/// <summary>
		/// Asynchronously runs the JavaScript within the component's context
		/// in the browser and returns an awaitable <see cref="Task"/> containing the value returned by the
		/// remote call.
		/// </summary>
		/// <param name="name">Represents the function name.</param>
		/// <param name="args">The arguments to pass to the function.</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		protected Task<dynamic> CallAsync(string name, params object[] args)
		{
			return Application.CallAsync($"{ClientFileSystem.TARGET}.invoke", this.Hash, name, args);
		}

        /// <summary>
        /// Releases the client-side handle that this <see cref="Directory"/> represents.
        /// </summary>
        /// <remarks>
        /// The handle is an entry in the browser's object registry, not an unmanaged resource: this
        /// method sends a single fire-and-forget request to release it and does not wait for
        /// confirmation. Disposing does not delete anything from the user's disk, and it does not
        /// dispose the <see cref="File"/> and <see cref="Directory"/> objects obtained from this one —
        /// each holds a handle of its own.
        /// <para>
        /// The instance is not guarded after disposal: properties keep returning their cached values,
        /// and further calls are sent with a handle the browser no longer recognizes, failing with
        /// "Invalid file system handle". No <see cref="ObjectDisposedException"/> is raised.
        /// </para>
        /// <para>
        /// An undisposed <see cref="Directory"/> releases its handle from the finalizer instead, which
        /// runs at a time the application does not control and may not run at all before the session
        /// ends. A session that enumerates folders repeatedly will accumulate handles in the browser
        /// until then, so dispose each one as soon as it is no longer needed.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// using (var directory = await ClientFileSystem.ShowDirectoryPickerAsync())
        /// {
        ///     var files = await directory.GetFilesAsync("*.txt");
        ///     foreach (var file in files)
        ///     {
        ///         AlertBox.Show(file.Name);
        ///         file.Dispose();
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        public void Dispose()
		{
			GC.SuppressFinalize(this);

			Application.Call($"{ClientFileSystem.TARGET}.dispose", this.Hash);
		}
	}
}
