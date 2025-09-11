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
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the files within a <see cref="Directory"/> object.
		/// </summary>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <param name="callback">Callback method that receives an Array of <see cref="Directory"/> object.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
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
		/// Gets the Directories within a <see cref="Wisej.Ext.ClientFileSystem.Directory"/> object.
		/// </summary>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <param name="callback">Callback method that receives an array of <see cref="Wisej.Ext.ClientFileSystem.Directory"/> object.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
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
		/// Returns or creates a file in the <see cref="Directory"/> asynchronously.
		/// </summary>
		/// <param name="name">Name of the file to open.</param>
		/// <param name="create">Optional flag to create the specified file.</param>
		/// <returns></returns>
		public async Task<File> GetFileAsync(string name, bool create = false){

			var result = await CallAsync("getFile", name, create);
			return new File(result);
		}

		/// <summary>
		/// Gets the Files within a <see cref="Directory"/> object asynchronously.
		/// </summary>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <returns>An array of <see cref="File"/> containing the files in a <see cref="Directory"/>.</returns>
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
		/// Gets the Directories within a <see cref="Directory"/> object asynchronously.
		/// </summary>
		/// <param name="pattern"></param>
		/// <returns>An array of <see cref="Directory"/> containing the directories in a <see cref="Directory"/>.</returns>
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
		/// Requests read or read-write permissions for the <see cref="Directory"/> asynchronously.
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
		/// Removes a file and/or a directory.
		/// </summary>
		/// <param name="name">Represents the file or directory's name.</param>
		/// <param name="recursive">If <paramref name="name"/> is a directory, set to true to delete recursively; otherwise false.</param>
		/// <param name="callback">Optional callback method that receives a boolean indicating the success or failure of the delete operation.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
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
		/// Removes a file and/or a directory.
		/// </summary>
		/// <param name="name">Represents the file or directory's name.</param>
		/// <param name="recursive">If <paramref name="name"/> is a directory, set to true to delete recursively; otherwise false.</param>
		/// <returns>An awaitable <see cref="Task"/> that represents the asynchronous operation.</returns>
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
		/// Dispose the <see cref="Directory"/> object.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			Application.Call($"{ClientFileSystem.TARGET}.dispose", this.Hash);
		}
	}
}
