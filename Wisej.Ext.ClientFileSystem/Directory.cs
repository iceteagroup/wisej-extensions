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
	/// Represents a Directory of a <see cref="T:Wisej.Ext.ClientFileSystem.ClientFileSystem"/>.
	/// </summary>
	public class Directory : IDisposable
	{
		/// <summary>
		/// Destroys an instance of <see cref="T:Wisej.Ext.ClientFileSystem.Directory"/>.
		/// </summary>
		~Directory()
		{
			Dispose();
		}

		/// <summary>
		/// Creates a new instance of <see cref="T:Wisej.Ext.ClientFileSystem.Directory"/>.
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
		/// Gets the files within a <see cref="T:Wisej.Ext.ClientFileSystem.Directory"/> object.
		/// </summary>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <param name="callback">Callback method that receives an Array of <see cref="Wisej.Ext.ClientFileSystem.Directory"/> object.</param>
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
		/// Gets the Files within a <see cref="T:Wisej.Ext.ClientFileSystem.Directory"/> object asynchronously.
		/// </summary>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <returns>An array of <see cref="T:Wisej.Ext.ClientFileSystem.File"/> containing the files in a <see cref="Wisej.Ext.ClientFileSystem.Directory"/>.</returns>
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
		/// Gets the Directories within a <see cref="T:Wisej.Ext.ClientFileSystem.Directory"/> object asynchronously.
		/// </summary>
		/// <param name="pattern"></param>
		/// <returns>An array of <see cref="Wisej.Ext.ClientFileSystem.Directory"/> containing the directories in a <see cref="Wisej.Ext.ClientFileSystem.Directory"/>.</returns>
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
		/// Removes a file and/or a directory.
		/// </summary>
		/// <param name="name">Represents the file or directory's name.</param>
		/// <param name="recursive">If <paramref name="name"/> is a directory, set to true to delete recursively; otherwise false.</param>
		/// <param name="callback">Optional callback method that receives a boolean indicating the success or failure of the delete operation.</param>
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
		/// Dispose the <see cref="Wisej.Ext.ClientFileSystem.Directory"/> object.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			Application.Call($"{ClientFileSystem.TARGET}.dispose", this.Hash);
		}
	}
}
