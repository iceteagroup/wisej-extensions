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
	/// Represents a File of a <see cref="ClientFileSystem"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// An instance is a handle on a file the user chose in the browser, not a path on the server.
	/// Handles come from <see cref="ClientFileSystem.ShowOpenFilePickerAsync(bool, bool, string)"/>,
	/// <see cref="ClientFileSystem.ShowSaveFilePickerAsync(bool, string, string)"/> and
	/// <see cref="Directory.GetFilesAsync(string)"/>; application code never constructs one
	/// directly.
	/// </para>
	/// <para>
	/// <see cref="Name"/>, <see cref="Size"/>, <see cref="Type"/> and <see cref="LastModified"/>
	/// are captured when the handle is created and are not refreshed afterwards, so they describe
	/// the file as it was at that moment.
	/// </para>
	/// <para>
	/// Each instance pins an object in the browser's registry and implements
	/// <see cref="IDisposable"/>. Dispose it when it is no longer needed rather than waiting for
	/// the finalizer, which runs at a time the application does not control.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var files = await ClientFileSystem.ShowOpenFilePickerAsync(false, false, "Text files|text/plain|.txt");
	///
	/// using (var file = files[0])
	/// {
	///     AlertBox.Show($"{file.Name}, {file.Size} bytes, last modified {file.LastModified:d}.");
	///
	///     var text = await file.ReadTextAsync();
	///     AlertBox.Show(text);
	/// }
	/// ]]></code>
	/// </example>
	public class File : IDisposable
	{
		/// <summary>
		/// Destroys an instance of <see cref="File"/>.
		/// </summary>
		~File()
		{
			Dispose();
		}

		/// <summary>
		/// Creates a new instance of <see cref="File"/> from the handle description returned by the
		/// browser.
		/// </summary>
		/// <param name="config">
		/// The dynamic configuration object sent by the client, carrying the handle's hash, name,
		/// size, type and last-modified date.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="config"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This constructor exists for the extension itself. Application code obtains a
		/// <see cref="File"/> from a picker or from
		/// <see cref="Directory.GetFilesAsync(string)"/> instead of calling it.
		/// </remarks>
		public File(dynamic config)
		{
			if (config == null)
				throw new ArgumentNullException(nameof(config));

			this.Hash = config.hash;
			this.Name = config.name;
			this.Size = config.size;
			this.Type = config.type;
			this.LastModified = config.lastModified;
		}

		/// <summary>
		/// Returns the file's hash.
		/// </summary>
		internal string Hash
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file's name.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> containing the file name, without any path information. The browser
		/// never exposes the full path of a file to the application.
		/// </value>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file's last modification date.
		/// </summary>
		/// <value>
		/// A <see cref="DateTime"/> holding the modification date reported by the browser when this
		/// handle was created. It is a snapshot: writing through this instance does not update it.
		/// </value>
		public DateTime LastModified
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file size.
		/// </summary>
		/// <value>
		/// The length of the file in bytes when this handle was created. It is a snapshot: writing
		/// through this instance does not update it. Useful as the starting
		/// <c>position</c> for an append.
		/// </value>
		public int Size
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file type.
		/// </summary>
		/// <value>
		/// The MIME type the browser inferred for the file, such as <c>text/plain</c>. Empty when the
		/// browser cannot determine a type from the file's extension.
		/// </value>
		public string Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Opens a text file, reads all the text in the file into a string, and then closes the file.
		/// </summary>
		/// <param name="callback">
		/// A method invoked on the application context with the file's contents, or with
		/// <see langword="null"/> if the read failed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The content is decoded as UTF-8.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// file.ReadText(text =>
		/// {
		///     if (text == null)
		///     {
		///         AlertBox.Show("The file could not be read.");
		///         return;
		///     }
		///
		///     AlertBox.Show($"{text.Length} characters read.");
		/// });
		/// ]]></code>
		/// </example>
		public void ReadText(Action<string> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ReadTextAsync();

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
		/// Opens a text file, reads all the text in the file into a string, and then closes the file asynchronously.
		/// </summary>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with all the text in the file,
		/// decoded as UTF-8.
		/// </returns>
		/// <remarks>
		/// The task faults if the file can no longer be read: the handle was disposed, the user
		/// revoked permission, or the file was removed since it was picked.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// try
		/// {
		///     var text = await file.ReadTextAsync();
		///     AlertBox.Show($"{file.Name} contains {text.Length} characters.");
		/// }
		/// catch (Exception ex)
		/// {
		///     AlertBox.Show($"Could not read {file.Name}: {ex.Message}");
		/// }
		/// ]]></code>
		/// </example>
		public async Task<string> ReadTextAsync() {

			string text = await CallAsync("readText");
			return text;
		}

		/// <summary>
		/// Reads the whole content of the <see cref="File"/> as raw bytes.
		/// </summary>
		/// <param name="callback">
		/// A method invoked on the application context with the file's contents. A failed read
		/// reports an empty array rather than <see langword="null"/>, so an empty result does not by
		/// itself mean the file is empty.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. The whole file is buffered in memory on the way through,
		/// so prefer it for files small enough to hold comfortably.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// file.ReadBytes(bytes =>
		/// {
		///     AlertBox.Show($"{bytes.Length} bytes read from {file.Name}.");
		/// });
		/// ]]></code>
		/// </example>
		public void ReadBytes(Action<byte[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ReadBytesAsync();

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
		/// Reads the whole content of the <see cref="File"/> as raw bytes, asynchronously.
		/// </summary>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with the file's contents.
		/// </returns>
		/// <remarks>
		/// Unlike <see cref="ReadTextAsync"/>, this method swallows a failed read and returns an
		/// empty array instead of faulting, so an empty result does not distinguish an empty file from
		/// a file that could not be read. Check <see cref="Size"/> when that distinction matters. The
		/// whole file is buffered in memory on the way through.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bytes = await file.ReadBytesAsync();
		///
		/// if (bytes.Length == 0 && file.Size > 0)
		///     AlertBox.Show($"{file.Name} could not be read.");
		/// else
		///     AlertBox.Show($"{bytes.Length} bytes read.");
		/// ]]></code>
		/// </example>
		public async Task<byte[]> ReadBytesAsync()
		{
			int[] buffer = new int[0];
			
			try
            {
				buffer = await CallAsync("readBytes");
			} catch { }
			
			var bytes = new byte[buffer.Length];
			for (int i = 0, l = buffer.Length; i < l; i++)
			{
				bytes[i] = (byte)buffer[i];
			}
			
			return bytes;
		}

		/// <summary>
		/// Writes text into the file starting at the specified position.
		/// </summary>
		/// <param name="text">The text to write, encoded as UTF-8.</param>
		/// <param name="position">
		/// The byte offset in the file at which the write starts. Pass 0 to write from the beginning,
		/// or <see cref="Size"/> to append.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with <see langword="true"/> if the operation
		/// completed, or <see langword="false"/> if it failed. The callback carries no indication of
		/// the cause; the asynchronous overload surfaces the underlying exception.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// Writing requires that <see cref="Permission.ReadWrite"/> has been granted on the file or on
		/// the directory it came from; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first. The underlying stream is opened
		/// with the existing content preserved, so a write shorter than the current file replaces only
		/// the bytes it covers and leaves the remainder in place. Call
		/// <see cref="TruncateAsync(int)"/> first when the file should be replaced outright.
		/// <see cref="Size"/> is not updated by a write; re-enumerate the folder to observe the new
		/// length.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Append a line to the end of the file.
		/// file.WriteText($"Logged at {DateTime.Now}.\r\n", file.Size, success =>
		/// {
		///     AlertBox.Show(success ? "Saved." : "The file could not be written.");
		/// });
		/// ]]></code>
		/// </example>
		public void WriteText(string text, int position, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = WriteTextAsync(text, position);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(false);
					else
						callback(true);
				});
			});
		}

		/// <summary>
		/// Writes text into the file starting at the specified position, asynchronously.
		/// </summary>
		/// <param name="text">The text to write, encoded as UTF-8.</param>
		/// <param name="position">
		/// The byte offset in the file at which the write starts. Pass 0 to write from the beginning,
		/// or <see cref="Size"/> to append.
		/// </param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		/// <remarks>
		/// Writing requires that <see cref="Permission.ReadWrite"/> has been granted on the file or on
		/// the directory it came from; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first. The underlying stream is opened
		/// with the existing content preserved, so a write shorter than the current file replaces only
		/// the bytes it covers and leaves the remainder in place. Call
		/// <see cref="TruncateAsync(int)"/> first when the file should be replaced outright.
		/// <see cref="Size"/> is not updated by a write; re-enumerate the folder to observe the new
		/// length.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var state = await file.RequestPermissionAsync(Permission.ReadWrite);
		/// if (state == PermissionState.Granted)
		/// {
		///     await file.TruncateAsync(0);
		///     await file.WriteTextAsync("Replaced contents.", 0);
		/// }
		/// ]]></code>
		/// </example>
		public async Task WriteTextAsync(string text, int position)
		{
			await CallAsync("writeText", text, position);
		}

		/// <summary>
		/// Writes an array of <see cref="byte"/> starting from a position in the file.
		/// </summary>
		/// <param name="bytes">The <see cref="byte"/> array to write.</param>
		/// <param name="type">
		/// One of the <see cref="WritableType"/> values describing the action to perform.
		/// <see cref="WritableType.Write"/> is the usual choice.
		/// </param>
		/// <param name="keepExistingData">
		/// <see langword="true"/> to copy the current contents into the stream before writing, so
		/// bytes outside the written range survive; <see langword="false"/> to start from an empty
		/// file, discarding everything not written by this call.
		/// </param>
		/// <param name="position">
		/// The byte offset in the file at which the write starts. Pass 0 to write from the beginning,
		/// or <see cref="Size"/> to append.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with <see langword="true"/> if the operation
		/// completed, or <see langword="false"/> if it failed. The callback carries no indication of
		/// the cause; the asynchronous overload surfaces the underlying exception.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// This method returns immediately. Writing requires that
		/// <see cref="Permission.ReadWrite"/> has been granted; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first. Note that this overload takes its
		/// arguments in a different order from
		/// <see cref="WriteBytesAsync(byte[], int, WritableType, bool)"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bytes = System.Text.Encoding.UTF8.GetBytes("binary payload");
		///
		/// file.WriteBytes(bytes, WritableType.Write, false, 0, success =>
		/// {
		///     AlertBox.Show(success ? "Saved." : "The file could not be written.");
		/// });
		/// ]]></code>
		/// </example>
		public void WriteBytes(byte[] bytes, WritableType type, bool keepExistingData, int position, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = WriteBytesAsync(bytes, position, type, keepExistingData);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(false);
					else
						callback(true);
				});
			});
		}

		/// <summary>
		/// Writes an array of <see cref="byte"/> starting from a position in the file, asynchronously.
		/// </summary>
		/// <param name="bytes">The <see cref="byte"/> array to write.</param>
		/// <param name="position">
		/// The byte offset in the file at which the write starts. Pass 0 to write from the beginning,
		/// or <see cref="Size"/> to append.
		/// </param>
		/// <param name="type">
		/// One of the <see cref="WritableType"/> values describing the action to perform.
		/// <see cref="WritableType.Write"/> is the usual choice.
		/// </param>
		/// <param name="keepExistingData">
		/// <see langword="true"/> to copy the current contents into the stream before writing, so
		/// bytes outside the written range survive; <see langword="false"/> to start from an empty
		/// file, discarding everything not written by this call.
		/// </param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		/// <remarks>
		/// Writing requires that <see cref="Permission.ReadWrite"/> has been granted; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first. Note that this overload takes its
		/// arguments in a different order from
		/// <see cref="WriteBytes(byte[], WritableType, bool, int, Action{bool})"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bytes = await BuildImageAsync();
		///
		/// await file.WriteBytesAsync(bytes, 0, WritableType.Write, false);
		/// AlertBox.Show($"Wrote {bytes.Length} bytes to {file.Name}.");
		/// ]]></code>
		/// </example>
		public async Task WriteBytesAsync(byte[] bytes, int position, WritableType type, bool keepExistingData)
		{
			await CallAsync("writeBytes", bytes, position, keepExistingData, type);
		}

		/// <summary>
		/// Resizes the file associated with stream to be size bytes long. If size is larger than the current file size this pads the file with null bytes, otherwise it truncates the file.
		/// </summary>
		/// <param name="size">The new length of the stream.</param>
		/// <param name="callback">
		/// A method invoked on the application context with <see langword="true"/> if the operation
		/// completed, or <see langword="false"/> if it failed. The callback carries no indication of
		/// the cause; the asynchronous overload surfaces the underlying exception.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// <para>
		/// The file cursor is updated when truncate is called. If the offset is smaller than offset, it remains unchanged.
		/// </para>
		/// <para>
		/// If the offset is larger than size, the offset is set to size to ensure that subsequent writes do not error.
		/// </para>
		/// <para>
		/// No changes are written to the actual file on disk until the stream has been closed. Changes are typically written to a temporary file instead.
		/// </para>
		/// <para>
		/// Resizing requires that <see cref="Permission.ReadWrite"/> has been granted; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Empty the file before rewriting it.
		/// file.Truncate(0, success =>
		/// {
		///     if (success)
		///         file.WriteText("Fresh contents.", 0, written => { });
		/// });
		/// ]]></code>
		/// </example>
		public void Truncate(int size, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = TruncateAsync(size);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(false);
					else
						callback(true);
				});
			});
		}

		/// <summary>
		/// Resizes the file associated with stream to be size bytes long. If size is larger than the current file size this pads the file with null bytes, otherwise it truncates the file.
		/// </summary>
		/// <param name="size">The new length of the stream.</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		/// <remarks>
		/// <para>
		/// The file cursor is updated when truncate is called. If the offset is smaller than offset, it remains unchanged.
		/// </para>
		/// <para>
		/// If the offset is larger than size, the offset is set to size to ensure that subsequent writes do not error.
		/// </para>
		/// <para>
		/// No changes are written to the actual file on disk until the stream has been closed. Changes are typically written to a temporary file instead.
		/// </para>
		/// <para>
		/// Resizing requires that <see cref="Permission.ReadWrite"/> has been granted; request it with
		/// <see cref="RequestPermissionAsync(Permission)"/> first.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // Replace the file outright: empty it, then write from the beginning.
		/// await file.TruncateAsync(0);
		/// await file.WriteTextAsync("Fresh contents.", 0);
		/// ]]></code>
		/// </example>
		public async Task TruncateAsync(int size)
		{
			await CallAsync("truncate");
		}

		/// <summary>
		/// Queries the current state of the specified permission on the <see cref="File"/>, without
		/// prompting the user.
		/// </summary>
		/// <param name="mode">
		/// One of the <see cref="Permission"/> values: <see cref="Permission.Read"/> to read the
		/// file, <see cref="Permission.ReadWrite"/> to also modify it.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with one of the
		/// <see cref="PermissionState"/> values. A failed call reports
		/// <see cref="PermissionState.Denied"/>, which is indistinguishable here from a real refusal;
		/// use the asynchronous overload to tell the two apart.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// Treat <see cref="PermissionState.Prompt"/> as "not yet granted" rather than as a refusal:
		/// the browser has not decided and will ask when the file is next accessed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// file.QueryPermission(Permission.ReadWrite, state =>
		/// {
		///     if (state != PermissionState.Granted)
		///         AlertBox.Show($"Write access is {state}.");
		/// });
		/// ]]></code>
		/// </example>
		public void QueryPermission(Permission mode, Action<PermissionState> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = QueryPermissionAsync(mode);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(PermissionState.Denied);
					else
						callback(t.Result);
				});
			});
		}

		/// <summary>
		/// Queries the current state of the specified permission on the <see cref="File"/>
		/// asynchronously, without prompting the user.
		/// </summary>
		/// <param name="mode">
		/// One of the <see cref="Permission"/> values: <see cref="Permission.Read"/> to read the
		/// file, <see cref="Permission.ReadWrite"/> to also modify it.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with one of the
		/// <see cref="PermissionState"/> values.
		/// </returns>
		/// <exception cref="NotSupportedException">
		/// The browser returned a permission state that is not one of the recognized values.
		/// </exception>
		/// <remarks>
		/// Treat <see cref="PermissionState.Prompt"/> as "not yet granted" rather than as a refusal:
		/// the browser has not decided and will ask when the file is next accessed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var state = await file.QueryPermissionAsync(Permission.ReadWrite);
		///
		/// if (state != PermissionState.Granted)
		///     state = await file.RequestPermissionAsync(Permission.ReadWrite);
		/// ]]></code>
		/// </example>
		public async Task<PermissionState> QueryPermissionAsync(Permission mode)
		{
			var result = await CallAsync("queryPermission", mode.ToString().ToLower());

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
		/// Requests the specified permission on the <see cref="File"/>, prompting the user if the
		/// browser has not already decided.
		/// </summary>
		/// <param name="mode">
		/// One of the <see cref="Permission"/> values: <see cref="Permission.Read"/> to read the
		/// file, <see cref="Permission.ReadWrite"/> to also modify it.
		/// </param>
		/// <param name="callback">
		/// A method invoked on the application context with one of the
		/// <see cref="PermissionState"/> values. A failed call reports
		/// <see cref="PermissionState.Denied"/>, which is indistinguishable here from a real refusal;
		/// use the asynchronous overload to tell the two apart.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <see langword="null"/>.</exception>
		/// <remarks>
		/// The browser shows its prompt only in response to a user gesture, so call this from a
		/// control event rather than during application startup. Requesting a permission that has
		/// already been granted returns <see cref="PermissionState.Granted"/> without prompting again.
		/// Treat <see cref="PermissionState.Prompt"/> as "not yet granted" rather than as a refusal.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// file.RequestPermission(Permission.ReadWrite, state =>
		/// {
		///     if (state == PermissionState.Granted)
		///         file.WriteText("Saved.", 0, success => { });
		///     else
		///         AlertBox.Show($"Write access is {state}.");
		/// });
		/// ]]></code>
		/// </example>
		public void RequestPermission(Permission mode, Action<PermissionState> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = RequestPermissionAsync(mode);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						callback(PermissionState.Denied);
					else
						callback(t.Result);
				});
			});
		}

		/// <summary>
		/// Requests the specified permission on the <see cref="File"/> asynchronously, prompting the
		/// user if the browser has not already decided.
		/// </summary>
		/// <param name="mode">
		/// One of the <see cref="Permission"/> values: <see cref="Permission.Read"/> to read the
		/// file, <see cref="Permission.ReadWrite"/> to also modify it.
		/// </param>
		/// <returns>
		/// An awaitable <see cref="Task{TResult}"/> that completes with one of the
		/// <see cref="PermissionState"/> values.
		/// </returns>
		/// <exception cref="NotSupportedException">
		/// The browser returned a permission state that is not one of the recognized values.
		/// </exception>
		/// <remarks>
		/// The browser shows its prompt only in response to a user gesture, so call this from a
		/// control event rather than during application startup. Requesting a permission that has
		/// already been granted returns <see cref="PermissionState.Granted"/> without prompting again.
		/// Treat <see cref="PermissionState.Prompt"/> as "not yet granted" rather than as a refusal.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var state = await file.RequestPermissionAsync(Permission.ReadWrite);
		/// if (state != PermissionState.Granted)
		/// {
		///     AlertBox.Show($"Cannot write to {file.Name}: permission is {state}.");
		///     return;
		/// }
		///
		/// await file.WriteTextAsync("Saved.", 0);
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
		/// Releases the client-side handle that this <see cref="File"/> represents.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The handle is an entry in the browser's object registry, not an unmanaged resource: this
		/// method sends a single fire-and-forget request to release it and does not wait for
		/// confirmation. Disposing does not delete the file from the user's disk.
		/// </para>
		/// <para>
		/// The instance is not guarded after disposal: the properties keep returning their cached
		/// values, and further calls are sent with a handle the browser no longer recognizes, failing
		/// with "Invalid file system handle". No <see cref="ObjectDisposedException"/> is raised.
		/// </para>
		/// <para>
		/// An undisposed <see cref="File"/> releases its handle from the finalizer instead, which runs
		/// at a time the application does not control and may not run at all before the session ends.
		/// A session that enumerates folders repeatedly will accumulate handles in the browser until
		/// then, so dispose each one as soon as it is no longer needed.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var files = await directory.GetFilesAsync("*.txt");
		///
		/// foreach (var file in files)
		/// {
		///     AlertBox.Show(file.Name);
		///     file.Dispose();
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
