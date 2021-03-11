///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using Microsoft.Owin;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace Wisej.HostService.Owin
{
	/// <summary>
	/// Implementation of System.Web.HttpWorkerRequest for
	/// Owin Wisej Middleware.
	/// </summary>
	internal class WisejWorkerRequest : HttpWorkerRequest
	{
		static bool isWin8OrLater = false;

		string verbName = "GET";
		IOwinContext context = null;

		string appPath = null;
		string filePath = null;
		string installDir = null;
		string webConfigPath = null;
		string machineConfigPath = null;
		string filePathTranslated = null;
		string protocol = "HTTP/1.0";
		TaskCompletionSource<bool> task = null;
		MemoryStream buffer = new MemoryStream();

		bool restoreWisejSession;
		object endOfSendExtraData;
		EndOfSendNotification endOfSendCallback;

		static WisejWorkerRequest()
		{
			try
			{
				WisejWorkerRequest.isWin8OrLater = Environment.OSVersion.Version >= new Version(6, 2);
			}
			catch { }
		}

		public WisejWorkerRequest(IOwinContext context)
		{
			this.context = context;
			this.verbName = context.Request.Method;
			this.appPath = HttpRuntime.AppDomainAppPath;
			this.installDir = HttpRuntime.AspInstallDirectory;
			this.filePath = GetFilePath(context.Request.Path.Value);
			this.filePathTranslated = MapPath(this.filePath);
			this.webConfigPath = Path.Combine(HttpRuntime.MachineConfigurationDirectory, "web.config");
			this.machineConfigPath = Path.Combine(HttpRuntime.MachineConfigurationDirectory, "machine.config");
			this.restoreWisejSession = Path.GetExtension(this.filePath) != ".wx";
		}

		private static string GetFilePath(string url)
		{
			string[] parts = url.Split('/');
			for (int i = 0; i < parts.Length; i++)
			{
				if (Path.HasExtension(parts[i]))
					return String.Join("/", parts, 0, i + 1);
			}

			return url;
		}

		/// <summary>
		/// Gets the full physical path to the Machine.config file.
		/// </summary>
		/// <returns>The physical path to the Machine.config file.</returns>
		public override string MachineConfigPath
		{
			get { return this.machineConfigPath; }
		}

		/// <summary>
		/// Gets the physical path to the directory where the ASP.NET binaries are installed.
		/// </summary>
		/// <returns>The physical directory to the ASP.NET binary files.</returns>
		public override string MachineInstallDirectory
		{
			get { return this.installDir; }
		}

		/// <summary>
		/// Gets the full physical path to the root Web.config file.
		/// </summary>
		/// <returns>The physical path to the root Web.config file.</returns>
		public override string RootWebConfigPath
		{
			get { return this.webConfigPath; }
		}

		/// <summary>
		/// Returns a Task that a called can await until the request is completed.
		/// </summary>
		public Task Task
		{
			get
			{
				this.task = this.task ?? new TaskCompletionSource<bool>();
				return this.task.Task;
			}
		}

		/// <summary>
		/// Notifies the <see cref="T:System.Web.HttpWorkerRequest" /> that request processing for the current request is complete.
		/// </summary>
		public override void EndOfRequest()
		{
			this.task?.SetResult(true);
			this.endOfSendCallback?.Invoke(this, this.endOfSendExtraData);
		}

		/// <summary>
		/// Registers for an optional notification when all the response data is sent.
		/// </summary>
		/// <param name="callback">The notification callback that is called when all data is sent (out-of-band). </param>
		/// <param name="extraData">An additional parameter to the callback. </param>
		public override void SetEndOfSendNotification(EndOfSendNotification callback, object extraData)
		{
			this.endOfSendCallback = callback;
			this.endOfSendExtraData = extraData;
		}

		/// <summary>
		/// Sends all pending response data to the client.
		/// </summary>
		/// <param name="finalFlush">true if this is the last time response data will be flushed; otherwise, false. </param>
		public override void FlushResponse(bool finalFlush)
		{
			this.context.Response.Body.Flush();
		}

		/// <summary>
		/// Adds a Content-Length HTTP header to the response for message bodies.
		/// </summary>
		/// <param name="contentLength">The length of the response, in bytes.</param>
		public override void SendCalculatedContentLength(int contentLength)
		{
			SendKnownResponseHeader(HeaderContentLength, contentLength.ToString());
		}

		/// <summary>
		/// Adds a Content-Length HTTP header to the response for message bodies.
		/// </summary>
		/// <param name="contentLength">The length of the response, in bytes.</param>
		public override void SendCalculatedContentLength(long contentLength)
		{
			SendKnownResponseHeader(HeaderContentLength, contentLength.ToString());
		}

		/// <summary>
		/// Returns the virtual path to the currently executing server application.
		/// </summary>
		/// <returns>The virtual path of the current application.</returns>
		public override string GetAppPath()
		{
			return "/";
		}

		/// <summary>
		/// Returns the UNC-translated path to the currently executing server application.
		/// </summary>
		/// <returns>The physical path of the current application.</returns>
		public override string GetAppPathTranslated()
		{
			return this.appPath;
		}

		/// <summary>
		/// Returns the physical path to the requested URI.
		/// </summary>
		/// <returns>The physical path to the requested URI.</returns>
		public override string GetFilePath()
		{
			return this.filePath;
		}

		/// <summary>
		/// Returns the physical file path to the requested URI
		/// (and translates it from virtual path to physical path: for example, "/proj1/page.aspx" to "c:\dir\page.aspx")
		/// </summary>
		/// <returns>The translated physical file path to the requested URI.</returns>
		public override string GetFilePathTranslated()
		{
			return this.filePathTranslated;
		}

		/// <summary>
		/// Returns the HTTP request verb.
		/// </summary>
		/// <returns>The HTTP verb for this request.</returns>
		public override string GetHttpVerbName()
		{
			return this.verbName;
		}

		/// <summary>
		/// Returns the HTTP version string of the request (for example, "HTTP/1.1").
		/// </summary>
		/// <returns>The HTTP version string returned in the request header.</returns>
		public override string GetHttpVersion()
		{
			return this.protocol;
		}

		/// <summary>
		/// Returns the server IP address of the interface on which the request was received.
		/// </summary>
		/// <returns>The server IP address of the interface on which the request was received.</returns>
		public override string GetLocalAddress()
		{
			return this.context.Request.LocalIpAddress;
		}

		/// <summary>
		/// Returns the port number on which the request was received.
		/// </summary>
		/// <returns>The server port number on which the request was received.</returns>
		public override int GetLocalPort()
		{
			return this.context.Request.LocalPort ?? 0;
		}

		/// <summary>
		/// Returns additional path information for a resource with a URL extension. 
		/// That is, for the path /virdir/page.html/tail, the return value is /tail.
		/// </summary>
		/// <returns>Additional path information for a resource.</returns>
		public override string GetPathInfo()
		{
			return this.context.Request.Path.Value.Substring(this.filePath.Length);
		}

		/// <summary>
		/// Returns the query string specified in the request URL.
		/// </summary>
		/// <returns>The request query string.</returns>
		public override string GetQueryString()
		{
			return this.context.Request.Uri.GetComponents(UriComponents.Query, UriFormat.Unescaped);
		}

		/// <summary>
		/// Returns the URL path contained in the header with the query string appended.
		/// </summary>
		/// <returns>
		/// The raw URL path of the request header.
		/// Note: The returned URL is not normalized. Using the URL for access control, or security-sensitive decisions can expose your application to canonicalization security vulnerabilities.</returns>
		public override string GetRawUrl()
		{
			return this.context.Request.Uri.PathAndQuery;
		}

		/// <summary>
		/// Returns the IP address of the client.
		/// </summary>
		/// <returns>The client's IP address.</returns>
		public override string GetRemoteAddress()
		{
			return this.context.Request.RemoteIpAddress;
		}

		/// <summary>
		/// Returns the name of the client computer.
		/// </summary>
		/// <returns>The name of the client computer.</returns>
		public override string GetRemoteName()
		{
			var name = GetServerVariable("HTTP_HOST");
			if (String.IsNullOrEmpty(name))
				return GetRemoteAddress();

			var pos = name.IndexOf(':');
			name = pos > 0 ? name.Substring(0, pos) : name;
			return name;
		}

		/// <summary>
		/// Returns the client's port number.
		/// </summary>
		/// <returns>The client's port number.</returns>
		public override int GetRemotePort()
		{
			return this.context.Request.RemotePort ?? 0;
		}

		/// <summary>
		/// Get all nonstandard HTTP header name-value pairs.
		/// </summary>
		/// <returns>An array of header name-value pairs.</returns>
		public override string[][] GetUnknownRequestHeaders()
		{
			if (WisejWorkerRequest.isWin8OrLater)
			{
				return new string[1][] {
					new string[] { "WEBSOCKET_VERSION", "13" }
				};
			}

			return null;
		}

		/// <summary>
		/// Returns a single server variable from a dictionary of server variables associated with the request.
		/// </summary>
		/// <returns>The requested server variable.</returns>
		/// <param name="name">The name of the requested server variable. </param>
		public override string GetServerVariable(string name)
		{
			// restore the Wisej session for non-wisej requests here because we need
			// to have a valid HttpContext.
			if (this.restoreWisejSession)
			{
				this.restoreWisejSession = false;
				Wisej.Web.Application.RestoreSession(HttpContext.Current);
			}

			switch (name)
			{
				case "HTTP_ACCEPT": name = "Accept"; break;
				case "HTTP_CONNECTION": name = "Connection"; break;
				case "HTTP_ACCEPT_ENCODING": name = "Accept-Encoding"; break;
				case "HTTP_ACCEPT_LANGUAGE": name = "Accept-Language"; break;
				case "HTTP_HOST": name = "Host"; break;
				case "HTTP_USER_AGENT": name = "User-Agent"; break;
				case "HTTP_REFERER": name = "Referer"; break;

				case "HTTP_URL": return GetRawUrl();
				case "HTTP_VERSION": return GetHttpVersion();
				case "REQUEST_METHOD": return this.verbName;
				case "HTTP_METHOD": return this.GetHttpVerbName();

				case "QUERY_STRING": return GetQueryString();
				case "SERVER_PROTOCOL": return GetProtocol();
				case "SERVER_NAME": return GetServerName();
				case "SERVER_PORT": return GetLocalPort().ToString();
				case "REMOTE_HOST": return GetRemoteName();
				case "REMOTE_ADDR": return GetRemoteAddress();
				case "LOCAL_ADDR": return GetLocalAddress();
				case "REMOTE_PORT": return GetRemotePort().ToString();
				case "SERVER_SOFTWARE":  return GetType().Assembly.GetName().Name;
			}

			string[] value = null;
			this.context.Request.Headers.TryGetValue(name, out value);
			return value != null && value.Length > 0 ? value[0] : null;
		}

		/// <summary>
		/// Returns the virtual path to the requested URI.
		/// </summary>
		/// <returns>The path to the requested URI.</returns>
		public override string GetUriPath()
		{
			return this.context.Request.Uri.AbsolutePath;
		}

		/// <summary>
		/// Returns the client's impersonation token.
		/// </summary>
		/// <returns>A value representing the client's impersonation token. The default is <see cref="F:System.IntPtr.Zero" />.</returns>
		public override IntPtr GetUserToken()
		{
			return IntPtr.Zero;
		}

		/// <summary>
		/// Returns the physical path corresponding to the specified virtual path.
		/// </summary>
		/// <returns>The physical path that corresponds to the virtual path specified in the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The virtual path. </param>
		public override string MapPath(string path)
		{
			if (String.IsNullOrEmpty(path))
				throw new ArgumentNullException("path");

			if (path.StartsWith("/"))
				path = path.Substring(1);
			else if (path.StartsWith("~/"))
				path = path.Substring(2);

			return Path.GetFullPath(Path.Combine(this.appPath, path));
		}

		/// <summary>
		/// Returns a value indicating whether all request data is available and no further reads from the client are required.
		/// </summary>
		/// <returns>true if all request data is available; otherwise, false.</returns>
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return false;
		}

		/// <summary>
		/// Reads request data from the client (when not preloaded).
		/// </summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to read data into. </param>
		/// <param name="size">The maximum number of bytes to read. </param>
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			return this.context.Request.Body.Read(buffer, 0, size);
		}

		/// <summary>
		/// Reads request data from the client (when not preloaded) by using the specified buffer to read from, byte offset, 
		/// and maximum bytes.
		/// </summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to read data into.</param>
		/// <param name="offset">The byte offset at which to begin reading.</param>
		/// <param name="size">The maximum number of bytes to read.</param>
		public override int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			return this.context.Request.Body.Read(buffer, offset, size);
		}

		/// <summary>
		/// Returns the standard HTTP request header that corresponds to the specified index.
		/// </summary>
		/// <returns>The HTTP request header.</returns>
		/// <param name="index">The index of the header. For example, the <see cref="F:System.Web.HttpWorkerRequest.HeaderAllow" /> field. </param>
		public override string GetKnownRequestHeader(int index)
		{
			string name = HttpWorkerRequest.GetKnownRequestHeaderName(index);
			string[] value = null;
			this.context.Request.Headers.TryGetValue(name, out value);
			return value != null && value.Length > 0 ? value[0] : null;
		}

		/// <summary>Returns a nonstandard HTTP request header value.</summary>
		/// <returns>The header value.</returns>
		/// <param name="name">The header name. </param>
		public override string GetUnknownRequestHeader(string name)
		{
			string[] value = null;
			this.context.Request.Headers.TryGetValue(name, out value);
			return value != null && value.Length > 0 ? value[0] : null;
		}

		/// <summary>
		/// Adds a standard HTTP header to the response.
		/// </summary>
		/// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />. </param>
		/// <param name="value">The header value. </param>
		public override void SendKnownResponseHeader(int index, string value)
		{
			string name = HttpWorkerRequest.GetKnownResponseHeaderName(index);
			this.context.Response.Headers[name] = value;
		}

		/// <summary>
		/// Adds the contents of the file with the specified name to the response and specifies 
		/// the starting position in the file and the number of bytes to send.
		/// </summary>
		/// <param name="filename">The name of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
			this.context.Response.ContentType = MimeTypes.GetMimeType(filename);

			using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				SendResponseFromFileStream(fileStream, offset, length);
			}
		}

		/// <summary>
		/// Adds the contents of the file with the specified handle to the response and specifies 
		/// the starting position in the file and the number of bytes to send.
		/// </summary>
		/// <param name="handle">The handle of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
			using (var fileStream = new FileStream(new SafeFileHandle(handle, false), FileAccess.Read))
			{
				SendResponseFromFileStream(fileStream, offset, length);
			}
		}

		private void SendResponseFromFileStream(FileStream stream, long offset, long length)
		{
			var response = this.context.Response;

			long size = stream.Length;
			if (length == -1)
				length = size - offset;

			if (offset < 0 || length > size - offset)
				throw new HttpException("Invalid_range");

			if (length > 0)
			{
				if (offset > 0)
					stream.Seek(offset, SeekOrigin.Begin);

				response.ContentLength = length;
				stream.CopyTo(response.Body, 1024);
			}
		}

		/// <summary>
		/// Adds the contents of a byte array to the response and specifies 
		/// the number of bytes to send.
		/// </summary>
		/// <param name="data">The byte array to send. </param>
		/// <param name="length">The number of bytes to send. </param>
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			this.context.Response.Body.Write(data, 0, length);
		}

		/// <summary>
		/// Specifies the HTTP status code and status description of the response; for example, SendStatus(200, "Ok").
		/// </summary>
		/// <param name="statusCode">The status code to send </param>
		/// <param name="statusDescription">The status description to send. </param>
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this.context.Response.StatusCode = statusCode;
		}

		/// <summary>
		/// Adds a nonstandard HTTP header to the response.
		/// </summary>
		/// <param name="name">The name of the header to send.</param>
		/// <param name="value">The value of the header.</param>
		public override void SendUnknownResponseHeader(string name, string value)
		{
			this.context.Response.Headers[name] = value;
		}
	}
}
