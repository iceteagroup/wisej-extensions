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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Wisej.Web;

namespace Wisej.Ext.ClientClipboard
{
	/// <summary>
	/// Implementation of the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Clipboard">Clipboard API</see>.
	/// Provides access to the browser's clipboard.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The Clipboard interface implements the Clipboard API, providing — if the user grants permission — both 
	/// read and write access to the contents of the system clipboard.
	/// The Clipboard API can be used to implement cut, copy, and paste features within a web application.
	/// </para>
	/// <para>
	/// The system clipboard is exposed through the global Navigator.clipboard property.
	/// </para>
	/// <para>
	/// Calls to the methods of the Clipboard object will not succeed if the user hasn't granted the needed 
	/// permissions using the Permissions API and the "clipboard-read" or "clipboard-write" permission as appropriate
	/// </para>
	/// </remarks>
	public static class ClientClipboard
	{
		/// <summary>
		/// Fired when the user copies, cuts, or pastes content to or from the clipboard.
		/// </summary>
		public static event ClientClipboardEventHandler ClipboardChange;

		// Raises the ClipboardChange event.
		private static void RaiseClipboardChange(string type, Control target, string content)
		{
			if (ClipboardChange != null)
			{
				var eventType = ClientClipboardChangeType.Copy;
				switch (type)
				{
					case "cut":
						eventType = ClientClipboardChangeType.Cut;
						break;
					case "copy":
						eventType = ClientClipboardChangeType.Copy;
						break;
					case "paste":
						eventType = ClientClipboardChangeType.Paste;
						break;
				}

				ClipboardChange(target, new ClientClipboardEventArgs(target, eventType, content));
			}
		}

		/// <summary>
		/// Returns the textual content of the client clipboard.
		/// </summary>
		/// <param name="callback">Callback method that receives the string result from the client's clipboard.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void ReadText(Action<string> callback)
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
		/// Returns the textual contents of the system clipboard.
		/// </summary>
		/// <returns>The string result from the client's clipboard</returns>
		public static async Task<string> ReadTextAsync()
		{
			return await Instance.CallAsync("readText");
		}

		/// <summary>
		/// Writes the specified <paramref name="text"/> string to the client clipboard.
		/// </summary>
		/// <param name="text">The text to write to the client's clipboard.</param>
		/// <param name="callback">Optional callback method, invoked the client's clipboard has been updated successfully.</param>
		/// <exception cref="Exception">The client's cliboard couldn't be updated.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void WriteText(string text, Action callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = WriteTextAsync(text);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						throw t.Exception;
					else
						callback();
				});
			});
		}

		/// <summary>
		/// Writes the specified <paramref name="text"/> string to the client clipboard.
		/// </summary>
		/// <param name="text">The text to write to the client's clipboard.</param>
		public static void WriteText(string text)
		{
			Instance.Call("writeText", text);
		}

		/// <summary>
		/// Writes the specified <paramref name="text"/> string to the client's clipboard.
		/// </summary>
		/// <param name="text">The text to write to the client's clipboard.</param>
		public static async Task WriteTextAsync(string text)
		{
			await Instance.CallAsync("writeText", text);
		}

		/// <summary>
		/// Returns the image content of the client clipboard.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> result from the client's clipboard.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		public static void ReadImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			var context = Application.Current;
			var task = ReadImageAsync();

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
		/// Returns the image content of the client clipboard.
		/// </summary>
		/// <returns>The string result from the client's clipboard</returns>
		public static async Task<Image> ReadImageAsync()
		{
			return ImageFromBase64(await Instance.CallAsync("readImage"));
		}

		/// <summary>
		/// Writes the specified <paramref name="image"/> string to the client clipboard.
		/// </summary>
		/// <param name="image">The text to write to the client's clipboard.</param>
		/// <param name="callback">Optional callback method, invoked the client's clipboard has been updated successfully.</param>
		/// <exception cref="Exception">The client's cliboard couldn't be updated.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null, or <paramref name="image"/> is null.</exception>
		public static void WriteImage(Image image, Action callback = null)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));
			if (image is null)
				throw new ArgumentNullException(nameof(image));

			var context = Application.Current;
			var task = WriteImageAsync(image);

			task.ContinueWith((t) =>
			{
				Application.Update(context, () =>
				{
					if (t.IsFaulted)
						throw t.Exception;
					else
						callback();
				});
			});
		}

		/// <summary>
		/// Writes the specified <paramref name="image"/> to the client's clipboard as a <see cref="ImageFormat.Png"/> image.
		/// </summary>
		/// <param name="image">The <see cref="Image"/> to write to the client's clipboard.</param>
		/// <exception cref="ArgumentNullException"><paramref name="image"/> is null.</exception>
		public static async Task WriteImageAsync(Image image)
		{
			if (image is null)
				throw new ArgumentNullException(nameof(image));

			await Instance.CallAsync("writeImage", ImageToBase64(image, ImageFormat.Png));
		}

		/// <summary>
		/// Writes the specified <paramref name="image"/> to the client's clipboard using the
		/// specified <paramref name="format"/> (default is png).
		/// </summary>
		/// <param name="image">The <see cref="Image"/> to write to the client's clipboard.</param>
		/// <param name="format">The <see cref="ImageFormat"/> to use for the image encoding.</param>
		/// <exception cref="ArgumentNullException"><paramref name="image"/> is null.</exception>
		public static async Task WriteImageAsync(Image image, ImageFormat format)
		{
			if (image is null)
				throw new ArgumentNullException(nameof(image));

			await Instance.CallAsync("writeImage", ImageToBase64(image, format));
		}

		// Returns the base64 encoding of the image.
		private static string ImageToBase64(Image image, ImageFormat format)
		{
			// save the image to memory.
			using (var mem = new MemoryStream())
			{
				var mediaType = "image/png";
				if (format.Equals(ImageFormat.Png))
					mediaType = "image/png";
				else if (format.Equals(ImageFormat.Gif))
					mediaType = "image/gif";
				else if (format.Equals(ImageFormat.Jpeg))
					mediaType = "image/jpeg";
				else if (format.Equals(ImageFormat.Bmp))
					mediaType = "image/png";

				try
				{
					image.Save(mem, format);
				}
				catch { }

				// return the buffer converted to a base64 string.
				string base64 = Convert.ToBase64String(mem.GetBuffer(), 0, (int)mem.Length);
				return String.Concat(new[] { "data:", mediaType, ";base64,", base64 });
			}
		}

		// Returns the Image encoded in a base64 string.
		private static Image ImageFromBase64(string base64)
		{
			// data:image/gif;base64,R0lGODlhCQAJAIABAAAAAAAAACH5BAEAAAEALAAAAAAJAAkAAAILjI+py+0NojxyhgIAOw==
			try
			{
				if (String.IsNullOrEmpty(base64))
					return null;

				var pos = base64.IndexOf("base64,");
				if (pos < 0)
					return null;

				base64 = base64.Substring(pos + 7);
				var buffer = Convert.FromBase64String(base64);
				var stream = new MemoryStream(buffer);
				return new Bitmap(stream);
			}
			catch { }

			return null;
		}

		#region Wisej Implementation

		private const string INSTANCE_KEY = "Wisej.Ext.ClientClipboard";

		private static ClipboardComponent Instance
		{
			get
			{
				var instance = Application.Session[INSTANCE_KEY];
				if (instance == null)
				{
					instance = new ClipboardComponent();
					Application.Session[INSTANCE_KEY] = instance;
				}
				return instance;
			}
		}

		// Connection to the client component.
		private class ClipboardComponent : Wisej.Base.Component
		{
			private void ProcessClipboardChangeWebEvent(Core.WisejEventArgs e)
			{
				var data = e.Parameters.Data;
				string type = data.type;
				Control target = data.target;
				string content = data.content;

				ClientClipboard.RaiseClipboardChange(type, target, content);
			}

			protected override void OnWebRender(dynamic config)
			{
				base.OnWebRender((object)config);

				config.className = "wisej.ext.ClientClipboard";
				config.wiredEvents = new[] {"clipboardchange(Data)"};
			}

			protected override void OnWebEvent(Core.WisejEventArgs e)
			{
				switch (e.Type)
				{
					case "clipboardchange":
						ProcessClipboardChangeWebEvent(e);
						break;

					default:
						base.OnWebEvent(e);
						break;
				}
			}
		}

		#endregion
	}
}
