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
	/// Represents a File of a <see cref="T:Wisej.Ext.ClientFileSystem.ClientFileSystem"/>.
	/// </summary>
	public class File : IDisposable
	{
		/// <summary>
		/// Destroys an instance of <see cref="T:Wisej.Ext.ClientFileSystem.File"/>.
		/// </summary>
		~File()
		{
			Dispose();
		}

		/// <summary>
		/// Creates a new instance of <see cref="T:Wisej.Ext.ClientFileSystem.File"/>.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
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
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file's last modification date.
		/// </summary>
		public DateTime LastModified
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file size.
		/// </summary>
		public int Size
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the file type.
		/// </summary>
		public string Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Opens a text file, reads all the text in the file into a string, and then closes the file.
		/// </summary>
		/// <param name="callback"></param>
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
		/// <returns>A string containing all text in the file.</returns>
		public async Task<string> ReadTextAsync() {

			string text = await CallAsync("readText");
			return text;
		}

		/// <summary>
		/// Reads the specified number of bytes from <see cref="T:Wisej.Ext.ClientFileSystem.File"/>
		/// </summary>
		/// <param name="callback">Callback method that receives an array of <see cref="byte"/> object.</param>
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
		/// Reads the specified number of bytes from <see cref="T:Wisej.Ext.ClientFileSystem.File"/> asynchronously.
		/// </summary>
		/// <returns>A byte array containing data read from <see cref="T:Wisej.Ext.ClientFileSystem.File"/></returns>
		public async Task<byte[]> ReadBytesAsync()
		{
			int[] buffer = await CallAsync("readBytes");

			var bytes = new byte[buffer.Length];
			for (int i = 0, l = buffer.Length; i < l; i++)
			{
				bytes[i] = (byte)buffer[i];
			}
			
			return bytes;
		}

		/// <summary>
		/// Opens a text file, writes all the text into the file, and then closes the file.
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="position">The cursor's position</param>
		/// <param name="callback">Callback method that receives a <see cref="bool"/> object.</param>
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
		/// Opens a text file, writes all the text into the file, and then closes the file asynchronously.
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="position">The cursor's position</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		public async Task WriteTextAsync(string text, int position)
		{
			await CallAsync("writeText", text, position);
		}

		/// <summary>
		/// Writes an array of <see cref="byte"/> starting from a position in the file.
		/// </summary>
		/// <param name="bytes">The <see cref="T:byte"/>[] to write</param>
		/// <param name="position">The cursor's position</param>
		/// <param name="callback">Callback method that receives a <see cref="bool"/> object.</param>
		public void WriteBytes(byte[] bytes, int position, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = WriteBytesAsync(bytes, position);

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
		/// Writes an array of <see cref="byte"/> starting from a position in the file asynchronously.
		/// </summary>
		/// <param name="bytes">The <see cref="T:byte"/>[] to write</param>
		/// <param name="position">The cursor's position</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		public async Task WriteBytesAsync(byte[] bytes, int position)
		{
			await CallAsync("writeBytes", bytes, position);
		}

		/// <summary>
		/// Resizes the file associated with stream to be size bytes long. If size is larger than the current file size this pads the file with null bytes, otherwise it truncates the file.
		/// </summary>
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
		/// </remarks>
		/// <param name="size">The new length of the stream.</param>
		/// <param name="callback">Callback method that receives a <see cref="bool"/> object.</param>
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
		/// </remarks>
		/// <param name="size">The new length of the stream.</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
		public async Task TruncateAsync(int size)
		{
			await CallAsync("truncate");
		}

		/// <summary>
		/// Queries the current state of the read permission of the <see cref="Wisej.Ext.ClientFileSystem.File"/>.
		/// </summary>
		/// <param name="mode">One of the <see cref="Wisej.Ext.ClientFileSystem.Permission" /> values.</param>
		/// <param name="callback">Callback method that receives one of the <see cref="Wisej.Ext.ClientFileSystem.PermissionState"/> values.</param>
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
		/// Queries the current state of the read permission of the <see cref="Wisej.Ext.ClientFileSystem.File"/> asynchronously.
		/// </summary>
		/// <param name="mode">One of the <see cref="Wisej.Ext.ClientFileSystem.Permission" /> values.</param>
		/// <returns>One of the <see cref="Wisej.Ext.ClientFileSystem.PermissionState" /> values.</returns>
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
		/// Requests read or readwrite permissions for the <see cref="Wisej.Ext.ClientFileSystem.File"/>.
		/// </summary>
		/// <param name="mode">One of the <see cref="Wisej.Ext.ClientFileSystem.Permission" /> values.</param>
		/// <param name="callback">Callback method that receives one of the <see cref="Wisej.Ext.ClientFileSystem.PermissionState"/> values.</param>
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
		/// Requests read or readwrite permissions for the <see cref="Wisej.Ext.ClientFileSystem.File"/> asynchronously.
		/// </summary>
		/// <param name="mode">One of the <see cref="Wisej.Ext.ClientFileSystem.Permission" /> values.</param>
		/// <returns>One of the <see cref="Wisej.Ext.ClientFileSystem.PermissionState" /> values.</returns>
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
		/// Dispose the <see cref="Wisej.Ext.ClientFileSystem.Directory"/> object.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			Application.Call($"{ClientFileSystem.TARGET}.dispose", this.Hash);
		}
	}
}
