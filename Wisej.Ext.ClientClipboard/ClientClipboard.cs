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
    /// Represents the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Clipboard">Clipboard API</see>
    /// implementation, enabling access to the browser's clipboard.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Clipboard interface implements the Clipboard API, providing — if the user grants permission — both 
    /// read and write access to the contents of the system clipboard.
    /// The Clipboard API can be used to implement cut, copy, and paste features within a web application.
    /// </para>
    /// <para>
    /// Access to the system clipboard is made available through the global <c>Navigator.clipboard</c> property.
    /// </para>
    /// <para>
    /// Invoking methods of the <see cref="ClientClipboard"/> class will fail unless the user has approved the
	/// required permissions via the Permissions API, specifically the "clipboard-read" and "clipboard-write" permissions as necessary.
    /// </para>
    /// </remarks>
    public static class ClientClipboard
	{
		/// <summary>
		/// Fired when the user copies, cuts, or pastes content to or from the clipboard.
		/// </summary>
		public static event ClientClipboardEventHandler ClipboardChange;

        /// <summary>
        /// Raises an event to notify that the clipboard content has changed.
        /// </summary>
        /// <param name="type">The type of the clipboard change (e.g., "copy", "cut", "paste").</param>
        /// <param name="target">The control that triggered the clipboard change.</param>
        /// <param name="content">The content of the clipboard.</param>
        /// <remarks>
        /// This method should be called to update the clipboard state in the application.
        /// </remarks>
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
        /// Asynchronously retrieves the textual content from the client's clipboard.
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
        /// Asynchronously retrieves the textual contents from the system clipboard.
        /// </summary>
        /// <returns>
		/// A task representing the asynchronous operation, containing the string value
		/// of the clipboard's contents. If the clipboard is empty or contains non-text data,
		/// an empty string is returned.
		/// </returns>
        public static async Task<string> ReadTextAsync()
		{
			return await Instance.CallAsync("readText");
		}

        /// <summary>
        /// Asynchronously writes the specified <paramref name="text"/> string to the client clipboard.
        /// </summary>
        /// <param name="text">The text content to be copied to the client's clipboard.</param>
        /// <param name="callback">An optional callback method that is invoked when the client's clipboard has been successfully updated.</param>
        /// <exception cref="Exception">Thrown when the client's clipboard cannot be updated due to an error.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="callback"/> parameter is null.</exception>
        /// <example>
        /// The following code copies the content of a text box to the client's clipboard and
        /// notifies the user once the browser confirms that the clipboard has been updated.
        /// <![CDATA[
        /// private void copyButton_Click(object sender, EventArgs e)
        /// {
        ///     ClientClipboard.WriteText(this.textBox1.Text, () =>
        ///     {
        ///         AlertBox.Show("Copied to the clipboard.");
        ///     });
        /// }
        /// ]]>
        /// </example>
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
        /// Writes the specified <paramref name="text"/> string to the client clipboard,
		/// allowing the user to copy text from the application directly to their clipboard for use in other applications.
        /// </summary>
        /// <param name="text">The string of text that will be copied to the client's clipboard.</param>
		/// <example>
		/// <code><![CDATA[
		/// ClientClipboard.WriteText("Hello, World!");
		/// ]]></code>
		/// </example>
        public static void WriteText(string text)
		{
			Instance.Call("writeText", text);
		}

        /// <summary>
        /// Asynchronously writes the specified <paramref name="text"/> to the client's clipboard.
        /// </summary>
        /// <param name="text">A <see cref="string"/> containing the text to be copied to the client's clipboard.
		/// The text should be non-null and can include any characters supported by the clipboard.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation. The task will complete once the text
		/// has been successfully written to the clipboard.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// await ClientClipboard.WriteTextAsync("Hello, World!");
		/// ]]></code>
		/// </example>
        public static async Task WriteTextAsync(string text)
		{
			await Instance.CallAsync("writeText", text);
		}

        /// <summary>
        /// Asynchronously retrieves the image content from the client's clipboard
        /// and invokes the specified callback with the resulting <see cref="Image"/>.
        /// </summary>
        /// <param name="callback">Callback method that receives the <see cref="Image"/> result from the client's clipboard.</param>
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
        /// <example>
        /// <code><![CDATA[
        /// private void pasteButton_Click(object sender, EventArgs e)
        /// {
        ///     ClientClipboard.ReadImage(image =>
        ///     {
        ///         if (image != null)
        ///             this.pictureBox1.Image = image;
        ///         else
        ///             AlertBox.Show("No image found in the clipboard.");
        ///     });
        /// }
        /// ]]></code>
        /// </example>
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
        /// <returns>The <see cref="Image"/> in the client's clipboard, or null if the clipboard doesn't contain an image.</returns>
        /// <example>
        /// <![CDATA[
        /// private async void pasteButton_Click(object sender, EventArgs e)
        /// {
        ///     var image = await ClientClipboard.ReadImageAsync();
        ///
        ///     if (image != null)
        ///         this.pictureBox1.Image = image;
        ///     else
        ///         AlertBox.Show("No image found in the clipboard.");
        /// }
        /// ]]>
        /// </example>
        public static async Task<Image> ReadImageAsync()
		{
			return ImageFromBase64(await Instance.CallAsync("readImage"));
		}

        /// <summary>
        /// Writes the specified <paramref name="image"/> to the client clipboard.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to write to the client's clipboard.</param>
        /// <param name="callback">An optional callback action that is invoked when the client's clipboard has been successfully updated. This may be used to perform additional actions after the write operation completes.</param>
        /// <exception cref="Exception">Thrown when the client's clipboard cannot be updated due to an internal error.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is null or <paramref name="callback"/> is null.</exception>
        /// <example>
        /// The following code copies the image displayed in a <see cref="PictureBox"/> to the
        /// client's clipboard and notifies the user once the browser confirms the update.
        /// <![CDATA[
        /// private void copyImageButton_Click(object sender, EventArgs e)
        /// {
        ///     ClientClipboard.WriteImage(this.pictureBox1.Image, () =>
        ///     {
        ///         AlertBox.Show("Image copied to the clipboard.");
        ///     });
        /// }
        /// ]]>
        /// </example>
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
        /// Asynchronously writes the specified <paramref name="image"/> to the client's clipboard
        /// in the <see cref="ImageFormat.Png"/> format.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> instance to be written to the client's clipboard.
        /// This parameter must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is null.</exception>
        /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
        /// /// <example>
        /// The following code copies the image displayed in a <see cref="PictureBox"/> to the
        /// client's clipboard and notifies the user once the browser has completed the operation.
        /// <![CDATA[
        /// private async void copyImageButton_Click(object sender, EventArgs e)
        /// {
        ///     await ClientClipboard.WriteImageAsync(this.pictureBox1.Image);
        ///
        ///     AlertBox.Show("Image copied to the clipboard.");
        /// }
        /// ]]>
        /// </example>
        /// <seealso cref="WriteImageAsync(Image, ImageFormat)"/>
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

        /// <summary>
        /// Converts a given <see cref="Image"/> to its Base64 string representation using the specified <see cref="ImageFormat"/>.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <param name="format">The format to use for the conversion (e.g., PNG, JPEG).</param>
        /// <returns>A Base64 string representation of the image.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="image"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="format"/> is not a valid <see cref="ImageFormat"/>.</exception>
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

        /// <summary>
        /// Converts a Base64 encoded string to an <see cref="Image"/> object.
        /// </summary>
        /// <param name="base64">The Base64 encoded string representing the image.</param>
        /// <returns>An <see cref="Image"/> object created from the specified Base64 string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="base64"/> is null or empty.</exception>
        /// <exception cref="FormatException">Thrown when <paramref name="base64"/> is not a valid Base64 string.</exception>
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
