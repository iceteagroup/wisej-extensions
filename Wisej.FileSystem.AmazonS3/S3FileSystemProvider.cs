///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using Amazon;
using Amazon.S3;
using Amazon.S3.IO;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Wisej.Core;

namespace Wisej.Ext.FileSystem
{

	/// <summary>
	/// Implementation of the Amazon S3 file system.
	/// Provides access to the S3 storage as a file system.
	/// </summary>
	public class S3FileSystemProvider : IFileSystemProvider
	{
		/// <summary>
		/// Initializes a blank instance of <see cref="S3FileSystemProvider"/>.
		/// </summary>
		public S3FileSystemProvider()
		{
			this.IconSource = "resource.wx/icon-cloud-storage.svg";
		}

		/// <summary>
		/// Creates a new instance of the <see cref="S3FileSystemProvider"/> class with the 
		/// specified <paramref name="root"/> and <paramref name="name"/>.
		/// The root is added in front of all  path arguments passed to all methods in this class.
		/// </summary>
		/// <param name="root">The root path of this file system.</param>
		/// <param name="name">The name of this file system.</param>
		/// <remarks>
		/// The root path is also the current directory path.
		/// </remarks>
		public S3FileSystemProvider(string root, string name)
			: this()
		{
			if (root == null || root == "")
				throw new ArgumentNullException("root");

			if (name == null || name == "")
				throw new ArgumentNullException("name");

			this.Root = root;
			this.Name = name;
		}

		/// <summary>
		/// Returns or sets the root path for the file system.
		/// All file system operations in the implementation class
		/// are expected to be limited to the root.
		/// </summary>
		/// <remarks>
		/// For the S3 file system, the first name in the root path is the bucket name.
		/// i.e.: this.Root = "BUCKET_NAME\folder1\folder2";
		/// </remarks>
		public string Root
		{
			get { return this._root; }
			set
			{
				if (value == null || value == "")
					throw new ArgumentNullException("value");

				this._root = value;
				this._root = this._root.Replace('\\', '/');

				if (!this._root.EndsWith("/"))
					this._root += "/";

				var parts = this._root.Split('/');

				// set the bucket name.
				this.BucketName = parts[0];

				// set the prefix.
				if (parts.Length > 1)
					this.Prefix = String.Join("/", parts, 1, parts.Length - 1);
				else
					this.Prefix = "";
			}
		}
		private string _root;

		/// <summary>
		/// Returns or sets the name of this root.
		/// This is the name that should be shown to the user.
		/// </summary>
		public string Name
		{
			get { return this._name; }
			set
			{
				if (value == null || value == "")
					throw new ArgumentNullException("value");

				this._name = value;
			}
		}
		private string _name;

		/// <summary>
		/// Returns or sets the icon that represents the
		/// file system.
		/// </summary>
		[DefaultValue(null)]
		public Image Icon
		{
			get { return this._icon; }
			set
			{
				if (this._icon != value)
				{
					this._icon = value;
					this.IconSource = null;
				}
			}
		}
		private Image _icon;

		/// <summary>
		/// Returns or sets the icon name or URL that represents the
		/// file system.
		/// </summary>
		[DefaultValue("resource.wx/icon-cloud-storage.svg")]
		public string IconSource
		{
			get { return this._iconSource; }
			set
			{
				value = value == "" ? null : value;
				if (this._iconSource != value)
				{
					this._iconSource = value;
					this.Icon = null;
				}
			}
		}
		private string _iconSource;

		/// <summary>
		/// Creates the specified directory and sub-directories.
		/// </summary>
		/// <param name="path">Path of the directory to create.</param>
		public void CreateDirectory(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1) + "/";
			var putRequest = new PutObjectRequest()
			{
				Key = path,
				BucketName = this.BucketName,
				ContentBody = ""
			};
			S3.PutObject(putRequest);
		}

		/// <summary>
		/// Deletes the specified directory and, optionally, sub-directories.
		/// </summary>
		/// <param name="path">Path of the directory to delete.</param>
		/// <param name="recursive">Indicates whether to delete sub directories.</param>
		public void DeleteDirectory(string path, bool recursive)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1) + "/";

			var deleteRequest = new DeleteObjectRequest()
			{
				Key = path,
				BucketName = this.BucketName,
			};
			S3.DeleteObject(deleteRequest);
		}

		/// <summary>
		/// Deletes the specified file.
		/// </summary>
		/// <param name="path">Path of the file to delete.</param>
		public void DeleteFile(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);
			var deleteRequest = new DeleteObjectRequest()
			{
				Key = path,
				BucketName = this.BucketName,
			};
			S3.DeleteObject(deleteRequest);
		}

		/// <summary>
		/// Returns the file's creation time.
		/// </summary>
		/// <param name="path">Path of the file to query.</param>
		/// <returns>A <see cref="T:Syste.DateTime"/> representing the timestamp of the file creation.</returns>
		/// <remarks>
		/// The Amazon S3 file system only returns the LastModified date.
		/// </remarks>
		public DateTime GetCreationTime(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			return GetLastWriteTime(path);
		}

		/// <summary>
		/// Returns the last write timestamp for the specified file.
		/// </summary>
		/// <param name="path">Path of the file to query.</param>
		/// <returns>A <see cref="T:Syste.DateTime"/> representing the timestamp of the last time the file was written.</returns>
		public DateTime GetLastWriteTime(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);

			// check the cache first.
			object data = null;
			if (this.cache.TryGetValue(path, out data))
			{
				if (data is S3Object)
					return ((S3Object)data).LastModified;
				else if (data is GetObjectMetadataResponse)
					return ((GetObjectMetadataResponse)data).LastModified;
			}

			var request = new GetObjectMetadataRequest()
			{
				Key = path,
				BucketName = this.BucketName
			};
			var response = S3.GetObjectMetadata(request);
			this.cache[path] = response;

			return response.LastModified;
		}

		/// <summary>
		/// Returns whether the specified file or directory exists.
		/// </summary>
		/// <param name="path">Path of the file or directory to check.</param>
		/// <returns>True of the file or directory exists.</returns>
		public bool Exists(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);
			var info = new S3FileInfo(S3, this.BucketName, path);
			return info.Exists;
		}

		/// <summary>
		/// Returns the <see cref="FileAttributes"/> for the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path">File path for which to retrieve the <see cref="FileAttributes"/>.</param>
		/// <returns>An instance of <see cref="FileAttributes"/> with the relevant flags set.</returns>
		public FileAttributes GetAttributes(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			if (path.EndsWith("/"))
				return FileAttributes.Normal | FileAttributes.Directory;
			else
				return FileAttributes.Normal;
		}

		/// <summary>
		/// Returns the size of the file.
		/// </summary>
		/// <param name="path">Path of the file to query.</param>
		/// <returns></returns>
		public long GetFileSize(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);

			// check the cache first.
			object data = null;
			if (this.cache.TryGetValue(path, out data))
			{
				if (data is S3Object)
					return ((S3Object)data).Size;
				else if (data is GetObjectMetadataResponse)
					return ((GetObjectMetadataResponse)data).ContentLength;
			}

			var request = new GetObjectMetadataRequest()
			{
				Key = path,
				BucketName = this.BucketName
			};
			var response = S3.GetObjectMetadata(request);
			this.cache[path] = response;

			return response.Headers.ContentLength;
		}

		/// <summary>
		/// Renames the specified file.
		/// </summary>
		/// <param name="path">Path of the file to rename.</param>
		/// <param name="newName">The new file name.</param>
		public void RenameFile(string path, string newName)
		{
			if (path == null)
				throw new ArgumentNullException("path");
			if (newName == null)
				throw new ArgumentNullException("newName");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);
			var newPath = Path.GetDirectoryName(path) + "/" + newName;

			var info = new S3FileInfo(S3, this.BucketName, path);
			info.MoveTo(this.BucketName, newPath);
		}

		/// <summary>
		/// Renames the specified directory.
		/// </summary>
		/// <param name="path">Path of the directory to rename.</param>
		/// <param name="newName">The new directory name.</param>
		public void RenameDirectory(string path, string newName)
		{
			if (path == null)
				throw new ArgumentNullException("path");
			if (newName == null)
				throw new ArgumentNullException("newName");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1) + "/";
			var newPath = Path.GetDirectoryName(path) + "/" + newName + "/";

			var info = new S3FileInfo(S3, this.BucketName, path);
			info.MoveTo(this.BucketName, newPath);
		}

		/// <summary>
		/// Returns a list of directory paths that match the pattern and search options
		/// in the specified path.
		/// </summary>
		/// <param name="path">Path to search into.</param>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <param name="searchOption">One of the <see cref="T:System.IO.SearchOption"/> options.</param>
		/// <returns>A <see cref="T:ystem.Array"/> containing the full path of the directories that match the search pattern and search options.</returns>
		public string[] GetDirectories(string path, string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			var list = GetObjects(path, pattern, searchOption != SearchOption.TopDirectoryOnly);

			this.cache.Clear();

			return list

				// directories only.
				.Where(f => f.Key.EndsWith("/"))

				// fix the returned path.
				.Select((f) =>
				{
					this.cache[f.Key] = f;

					var fileName = f.Key.Substring(this.Prefix.Length);
					fileName = fileName.Substring(0, fileName.Length - 1);
					fileName = fileName.Replace('/', '\\');
					fileName = this.Name + "\\" + fileName;
					return fileName;

				}).ToArray();
		}

		/// <summary>
		/// Returns a list of file paths that match the pattern and search options
		/// in the specified path.
		/// </summary>
		/// <param name="path">Path to search into.</param>
		/// <param name="pattern">Wild card pattern to match.</param>
		/// <param name="searchOption">One of the <see cref="T:System.IO.SearchOption"/> options.</param>
		/// <returns>A <see cref="T:ystem.Array"/> containing the full path of the files that match the search pattern and search options.</returns>
		public string[] GetFiles(string path, string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			var list = GetObjects(path, pattern, searchOption != SearchOption.TopDirectoryOnly);

			this.cache.Clear();

			return list

				// files only.
				.Where(f => !f.Key.EndsWith("/"))

				// fix the returned path.
				.Select((f) =>
				{
					this.cache[f.Key] = f;

					var fileName = f.Key.Substring(this.Prefix.Length);
					fileName = fileName.Replace('/', '\\');
					fileName = this.Name + "\\" + fileName;
					return fileName;

				}).ToArray();
		}

		/// <summary>
		/// Opens the specified file for reading or writing.
		/// </summary>
		/// <param name="path">The path of the file to open.</param>
		/// <param name="mode">Specified if the file should be opened, created, overwritten or truncated.</param>
		/// <param name="access">Specified if the stream should be opened for reading or writing.</param>
		/// <returns>A <see cref="T:Systen.IO.Stream"/> that can be used to read or write the content of the file. </returns>
		public Stream OpenFileStream(string path, FileMode mode, FileAccess access)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			path = MapPath(path);
			path = path.Substring(this.BucketName.Length + 1);
			if (mode == FileMode.Open && access == FileAccess.Read)
			{
				var info = new S3FileInfo(S3, this.BucketName, path);
				return info.OpenRead();
			}
			else if (mode == FileMode.Open && access == FileAccess.Write)
			{
				var info = new S3FileInfo(S3, this.BucketName, path);
				return info.OpenWrite();
			}
			else if (mode == FileMode.Create || mode == FileMode.CreateNew && access == FileAccess.Write)
			{
				var info = new S3FileInfo(S3, this.BucketName, path);
				return info.Create();
			}

			throw new NotSupportedException();
		}

		/// <summary>
		/// Checks whether the specified <paramref name="path"/> starts with the
		/// <see cref="Name"/> of this file system provider.
		/// </summary>
		/// <param name="path">Path of the file to check.</param>
		/// <returns>True if the specified path starts with the name of this file system provider.</returns>
		public bool Contains(string path)
		{
			if (String.IsNullOrEmpty(path))
				return false;

			return path.StartsWith(this.Name + Path.DirectorySeparatorChar);
		}

		/// <summary>
		/// Maps the virtual path to the corresponding physical path
		/// on the specific <see cref="IFileSystemProvider"/> implementation.
		/// </summary>
		/// <param name="path">Virtual path to map to the corresponding physical path.</param>
		/// <returns>The physical path for the <see cref="IFileSystemProvider"/> implementation.</returns>
		public string MapPath(string path)
		{
			path = path ?? "";

			if (path != "")
			{

				// already mapped?
				if (path.StartsWith(this.Root))
					return path;

				if (path.Equals(this.Name))
					path = "";
				else if (Contains(path))
					path = path.Substring(this.Name.Length + 1);

				path = path.Replace("\\", "/");

				if (path.StartsWith("/"))
					path = path.Substring(1);
				if (path.EndsWith("/"))
					path = path.Substring(0, path.Length - 1);
			}

			path = this.Root + path;
			return path;
		}

		#region S3


		// keeps a reference to the objects returned by the last request.
		private Dictionary<string, object> cache = new Dictionary<string, object>();


		// Returns the bucket name extracted from the root.
		private string BucketName
		{
			get;
			set;
		}

		// Returns the prefix key extracted from the root.
		private string Prefix
		{
			get;
			set;
		}

		// Returns a list of S3Objects that match the requested arguments.
		private IEnumerable<S3Object> GetObjects(string path, string pattern, bool deep)
		{
			path = path.Substring(this.Root.Length);
			var prefix = this.Prefix + path;
			if (prefix != "" && !prefix.EndsWith("/"))
				prefix += "/";

			var request = new ListObjectsRequest()
			{
				Prefix = prefix,
				BucketName = this.BucketName
			};
			var response = S3.ListObjects(request);

			// filter the response.
			var wildcard = WildcardToRegex(pattern);
			var levelMatch = prefix.Count(c => c == '/');
			var list  = response.S3Objects.Where((o)=>
				{
					// same as prefix?
					if (o.Key == prefix)
						return false;

					var name = o.Key;

					// top level?
					if (!deep)
					{
						var level = name.Count( c=> c=='/');
						level = level + (name.EndsWith("/") ? -1 : 0);

						if (level != levelMatch)
							return false;
					}

					// check wildcard.
					if (wildcard != null)
					{
						if (!wildcard.IsMatch(name))
							return false;
					}

					return true;
				});

			return list;
		}

		// Converts a wildcard string to a regular expression string.
		private static Regex WildcardToRegex(string pattern)
		{
			if (String.IsNullOrEmpty(pattern))
				return null;
			if (pattern == "*" || pattern == "*.*")
				return null;

			var regex = new Regex("^" + Regex.Escape(pattern).
				Replace("\\*", ".*").
				Replace("\\?", ".") + "$");

			return regex;
		}

		/// <summary>
		/// Returns or sets the S3 Access Key.
		/// </summary>
		[DefaultValue(null)]
		public string AccessKey
		{
			get { return this._accessKey; }
			set
			{
				if (this._accessKey != value)
				{
					this._accessKey = value;
					this._s3 = null;
				}
			}
		}
		private string _accessKey;

		/// <summary>
		/// Returns or sets the S3 Access Secret string.
		/// </summary>
		[DefaultValue(null)]
		public string AccessSecret
		{
			get { return this._accessSecret; }
			set
			{
				if (this._accessSecret != value)
				{
					this._accessSecret = value;
					this._s3 = null;
				}
			}
		}
		private string _accessSecret;

		// Returns the S3 singleton.
		internal AmazonS3Client S3
		{
			get
			{
				if (_s3 == null)
				{
					_s3 = new AmazonS3Client(this.AccessKey, this.AccessSecret, RegionEndpoint.USEast1);
				}
				return _s3;
			}
		}
		private AmazonS3Client _s3;

		#endregion
	}
}
